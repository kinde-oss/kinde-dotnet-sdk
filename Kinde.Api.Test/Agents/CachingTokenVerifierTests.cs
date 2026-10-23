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
