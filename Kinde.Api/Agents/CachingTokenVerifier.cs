#nullable enable
using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kinde.Api.Agents
{
    /// <summary>
    /// Caches successful verifications for a short while, so a chatty session does not call Kinde
    /// on every request.
    /// </summary>
    /// <remarks>
    /// Introspection costs one call to Kinde per authenticated request, which adds latency to each
    /// one and exposes a busy server to rate limiting. This trades a bounded amount of revocation
    /// latency for that: an entry lives for at most <c>lifetime</c>, and never past the token's own
    /// expiry. Failures are never cached, so a rejected token is re-checked immediately and an
    /// outage does not stick.
    /// </remarks>
    public class CachingTokenVerifier : IKindeTokenVerifier
    {
        private readonly IKindeTokenVerifier _inner;
        private readonly TimeSpan _lifetime;
        private readonly ConcurrentDictionary<string, Entry> _entries = new(StringComparer.Ordinal);

        /// <summary>Wraps <paramref name="inner"/> with a cache.</summary>
        /// <param name="inner">The verifier to call on a miss.</param>
        /// <param name="lifetime">
        /// How long a successful result is reused. This is the longest a revoked token can keep
        /// working, so keep it short; one minute by default.
        /// </param>
        public CachingTokenVerifier(IKindeTokenVerifier inner, TimeSpan? lifetime = null)
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
            _lifetime = lifetime ?? TimeSpan.FromMinutes(1);

            if (_lifetime <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(lifetime), "The cache lifetime must be greater than zero.");
            }
        }

        /// <inheritdoc />
        public async Task<KindeAuthContext> VerifyAsync(
            string accessToken, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                return KindeAuthContext.MissingToken();
            }

            // Hashed, so the cache never holds a usable bearer token in memory.
            var key = Hash(accessToken);
            var now = DateTimeOffset.UtcNow;

            if (_entries.TryGetValue(key, out var cached) && cached.ExpiresAt > now)
            {
                return cached.Context;
            }

            var context = await _inner.VerifyAsync(accessToken, cancellationToken)
                .ConfigureAwait(false);

            if (context.IsAuthenticated)
            {
                // Never past the token's own expiry, or the cache would keep an expired token
                // alive beyond the point Kinde would reject it.
                var until = now.Add(_lifetime);
                if (context.ExpiresAt.HasValue && context.ExpiresAt.Value < until)
                {
                    until = context.ExpiresAt.Value;
                }

                if (until > now)
                {
                    _entries[key] = new Entry(context, until);
                }
            }
            else
            {
                // A previously good token that has just been rejected must not stay cached.
                _entries.TryRemove(key, out _);
            }

            Prune(now);
            return context;
        }

        /// <summary>Forgets a token, for use after an explicit revocation.</summary>
        public void Invalidate(string accessToken)
        {
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                _entries.TryRemove(Hash(accessToken), out _);
            }
        }

        /// <summary>Forgets everything.</summary>
        public void Clear() => _entries.Clear();

        private const int PruneThreshold = 1024;

        private long _lastPruneTicks = DateTimeOffset.UtcNow.UtcTicks;

        private void Prune(DateTimeOffset now)
        {
            // Entries are only evicted on access, so a server seeing many distinct tokens would
            // otherwise grow without bound.
            if (_entries.Count < PruneThreshold)
            {
                return;
            }

            // At most one sweep per lifetime window. Without this, a cache sitting above the
            // threshold with every entry still live would walk the whole dictionary on every
            // single request and never remove anything.
            var last = Interlocked.Read(ref _lastPruneTicks);
            if (now.UtcTicks - last < _lifetime.Ticks)
            {
                return;
            }

            if (Interlocked.CompareExchange(ref _lastPruneTicks, now.UtcTicks, last) != last)
            {
                // Another request is already sweeping.
                return;
            }

            foreach (var pair in _entries)
            {
                if (pair.Value.ExpiresAt <= now)
                {
                    _entries.TryRemove(pair.Key, out _);
                }
            }

            // Still oversized once the expired entries are gone: every one is live, so the only
            // way to stay bounded is to drop some. They are re-verified on next use.
            if (_entries.Count >= PruneThreshold * 2)
            {
                _entries.Clear();
            }
        }

        private static string Hash(string token) =>
            Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));

        private readonly record struct Entry(KindeAuthContext Context, DateTimeOffset ExpiresAt);
    }
}
