#nullable enable
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kinde.Api.Agents;
using Xunit;

namespace Kinde.Api.Test.Agents
{
    public class CachingTokenVerifierTests
    {
        private static KindeAuthContext Success(DateTimeOffset? expiresAt = null) =>
            KindeAuthContext.Success(
                subject: "agent_01",
                actorSubject: null,
                clientId: "client",
                scopes: new[] { "offline" },
                permissions: Array.Empty<string>(),
                audiences: new[] { "https://api.example.com/mcp" },
                organizationCode: null,
                expiresAt: expiresAt ?? DateTimeOffset.UtcNow.AddHours(1),
                claims: new Dictionary<string, object>());

        [Fact]
        public async Task ASecondVerificationOfTheSameToken_DoesNotCallKindeAgain()
        {
            var inner = new CountingVerifier(Success());
            var verifier = new CachingTokenVerifier(inner);

            await verifier.VerifyAsync("token-a");
            var second = await verifier.VerifyAsync("token-a");

            Assert.True(second.IsAuthenticated);
            Assert.Equal(1, inner.Calls);
        }

        [Fact]
        public async Task DifferentTokens_AreCachedSeparately()
        {
            var inner = new CountingVerifier(Success());
            var verifier = new CachingTokenVerifier(inner);

            await verifier.VerifyAsync("token-a");
            await verifier.VerifyAsync("token-b");

            Assert.Equal(2, inner.Calls);
        }

        [Fact]
        public async Task FailuresAreNeverCached()
        {
            // A rejected token must be re-checked, and an outage must not stick.
            var inner = new CountingVerifier(KindeAuthContext.Unavailable("Kinde is down"));
            var verifier = new CachingTokenVerifier(inner);

            await verifier.VerifyAsync("token-a");
            await verifier.VerifyAsync("token-a");

            Assert.Equal(2, inner.Calls);
        }

        [Fact]
        public async Task ARejectionAfterASuccess_EvictsTheCachedSuccess()
        {
            var inner = new CountingVerifier(Success());
            var verifier = new CachingTokenVerifier(inner, TimeSpan.FromMinutes(5));

            await verifier.VerifyAsync("token-a");

            inner.Result = KindeAuthContext.Invalid("revoked");
            verifier.Invalidate("token-a");

            var afterRevocation = await verifier.VerifyAsync("token-a");
            var again = await verifier.VerifyAsync("token-a");

            Assert.False(afterRevocation.IsAuthenticated);
            // The failure was not cached, so the next call asks Kinde again.
            Assert.Equal(3, inner.Calls);
        }

        [Fact]
        public async Task TheCacheNeverOutlivesTheToken()
        {
            // An entry capped at the token's own expiry, so a cached result cannot be served
            // after Kinde would have rejected it.
            var inner = new CountingVerifier(Success(DateTimeOffset.UtcNow.AddMilliseconds(80)));
            var verifier = new CachingTokenVerifier(inner, TimeSpan.FromMinutes(10));

            await verifier.VerifyAsync("token-a");
            await Task.Delay(150);
            await verifier.VerifyAsync("token-a");

            Assert.Equal(2, inner.Calls);
        }

        [Fact]
        public async Task AnExpiredCacheEntry_IsVerifiedAgain()
        {
            var inner = new CountingVerifier(Success());
            var verifier = new CachingTokenVerifier(inner, TimeSpan.FromMilliseconds(60));

            await verifier.VerifyAsync("token-a");
            await Task.Delay(140);
            await verifier.VerifyAsync("token-a");

            Assert.Equal(2, inner.Calls);
        }

        [Fact]
        public async Task ClearForgetsEverything()
        {
            var inner = new CountingVerifier(Success());
            var verifier = new CachingTokenVerifier(inner);

            await verifier.VerifyAsync("token-a");
            verifier.Clear();
            await verifier.VerifyAsync("token-a");

            Assert.Equal(2, inner.Calls);
        }

        [Fact]
        public async Task ABlankCredential_IsRejectedWithoutCalling()
        {
            var inner = new CountingVerifier(Success());
            var verifier = new CachingTokenVerifier(inner);

            var context = await verifier.VerifyAsync("   ");

            Assert.Equal(KindeAuthFailureKind.MissingToken, context.FailureKind);
            Assert.Equal(0, inner.Calls);
        }

        [Fact]
        public void TheConstructorRejectsUnusableArguments()
        {
            var inner = new CountingVerifier(Success());

            Assert.Throws<ArgumentNullException>(() => new CachingTokenVerifier(null!));
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new CachingTokenVerifier(inner, TimeSpan.Zero));
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new CachingTokenVerifier(inner, TimeSpan.FromSeconds(-1)));
        }

        [Fact]
        public async Task AnInvalidationDuringVerification_DoesNotRepopulateTheCache()
        {
            // The revocation lands while the verification is still in flight. Its result predates
            // the revocation, so storing it would authenticate a revoked token for the whole cache
            // lifetime: Invalidate had already run and had nothing left to remove.
            var gate = new TaskCompletionSource();
            var inner = new BlockingVerifier(Success(), gate.Task);
            var verifier = new CachingTokenVerifier(inner, TimeSpan.FromMinutes(10));

            var verification = verifier.VerifyAsync("the-token");

            // Revoked and invalidated while the call above is blocked.
            verifier.Invalidate("the-token");
            gate.SetResult();

            var context = await verification;
            Assert.True(context.IsAuthenticated);

            // The next request must go back to Kinde rather than being served a stale success.
            inner.Release();
            await verifier.VerifyAsync("the-token");

            Assert.Equal(2, inner.Calls);
        }

        [Fact]
        public async Task AClearDuringVerification_DoesNotRepopulateTheCache()
        {
            var gate = new TaskCompletionSource();
            var inner = new BlockingVerifier(Success(), gate.Task);
            var verifier = new CachingTokenVerifier(inner, TimeSpan.FromMinutes(10));

            var verification = verifier.VerifyAsync("the-token");

            verifier.Clear();
            gate.SetResult();
            await verification;

            inner.Release();
            await verifier.VerifyAsync("the-token");

            Assert.Equal(2, inner.Calls);
        }

        [Fact]
        public async Task ARejectionOfOneToken_DoesNotStopAnotherFromCaching()
        {
            var gate = new TaskCompletionSource();
            var inner = new PerTokenVerifier
            {
                Blocked = gate.Task,
                BlockedToken = "token-a"
            };

            var verifier = new CachingTokenVerifier(inner, TimeSpan.FromMinutes(10));

            var verifyingA = verifier.VerifyAsync("token-a");

            // B is rejected while A is still in flight.
            await verifier.VerifyAsync("token-b");

            gate.SetResult();
            Assert.True((await verifyingA).IsAuthenticated);

            // A was cached, so a second request is served without calling Kinde again.
            await verifier.VerifyAsync("token-a");

            Assert.Equal(1, inner.CallsFor("token-a"));
        }

        /// <summary>Authenticates one token, rejects everything else, and can block one of them.</summary>
        private sealed class PerTokenVerifier : IKindeTokenVerifier
        {
            private readonly Dictionary<string, int> _calls = new();

            public Task Blocked { get; set; } = Task.CompletedTask;

            public string BlockedToken { get; set; } = string.Empty;

            public int CallsFor(string token) => _calls.TryGetValue(token, out var count) ? count : 0;

            public async Task<KindeAuthContext> VerifyAsync(
                string accessToken, CancellationToken cancellationToken = default)
            {
                lock (_calls)
                {
                    _calls[accessToken] = CallsFor(accessToken) + 1;
                }

                if (accessToken == BlockedToken)
                {
                    await Blocked;
                    return Success();
                }

                return KindeAuthContext.Invalid("rejected");
            }
        }

        [Fact]
        public async Task AnInvalidationOfAnotherToken_DoesNotStopCaching()
        {
            // The guard must only discard a result a revocation overtook. An unrelated earlier
            // invalidation must not stop the cache working, or every entry would be thrown away
            // once anything had ever been revoked.
            var inner = new CountingVerifier(Success());
            var verifier = new CachingTokenVerifier(inner, TimeSpan.FromMinutes(10));

            verifier.Invalidate("some-other-token");

            await verifier.VerifyAsync("the-token");
            await verifier.VerifyAsync("the-token");

            Assert.Equal(1, inner.Calls);
        }

        [Fact]
        public async Task ABurstOfUniqueValidTokens_StaysBounded()
        {
            // Every entry is live and the lifetime window never elapses during the burst, so the
            // timed sweep alone would let the dictionary grow for the whole lifetime. The ceiling
            // has to be enforced independently of it, or a stream of unique tokens exhausts memory
            // on a busy server.
            var inner = new CountingVerifier(Success(DateTimeOffset.UtcNow.AddHours(1)));
            var verifier = new CachingTokenVerifier(inner, TimeSpan.FromHours(1));

            for (var i = 0; i < 6000; i++)
            {
                await verifier.VerifyAsync($"token-{i}");
            }

            var callsAfterBurst = inner.Calls;

            // The earliest token was dropped long before the burst ended, so it costs a fresh
            // call. Were the cache unbounded it would still be held and served without one.
            await verifier.VerifyAsync("token-0");

            Assert.Equal(callsAfterBurst + 1, inner.Calls);
        }

        /// <summary>
        /// Blocks inside VerifyAsync until released, so a test can interleave an invalidation with
        /// a verification that is already in flight.
        /// </summary>
        private sealed class BlockingVerifier : IKindeTokenVerifier
        {
            private readonly KindeAuthContext _result;
            private Task _gate;

            public BlockingVerifier(KindeAuthContext result, Task gate)
            {
                _result = result;
                _gate = gate;
            }

            public int Calls { get; private set; }

            /// <summary>Stops blocking, so later calls return immediately.</summary>
            public void Release() => _gate = Task.CompletedTask;

            public async Task<KindeAuthContext> VerifyAsync(
                string accessToken, CancellationToken cancellationToken = default)
            {
                Calls++;
                await _gate;
                return _result;
            }
        }

        private sealed class CountingVerifier : IKindeTokenVerifier
        {
            public CountingVerifier(KindeAuthContext result) => Result = result;

            public KindeAuthContext Result { get; set; }

            public int Calls { get; private set; }

            public Task<KindeAuthContext> VerifyAsync(
                string accessToken, CancellationToken cancellationToken = default)
            {
                Calls++;
                return Task.FromResult(Result);
            }
        }
    }
}
