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

        /// <summary>
        /// Guards the insert in <see cref="VerifyAsync"/> against <see cref="Invalidate"/> and
        /// <see cref="Clear"/>. Only taken on insert and invalidation; a cache hit stays lock-free.
        /// </summary>
        private readonly object _sync = new();

        /// <summary>
        /// Bumped by every invalidation. A verification captures this before calling the inner
        /// verifier and only stores its result if the value has not moved, so a result that was
        /// already in flight when the token was revoked cannot repopulate the cache afterwards.
        /// </summary>
        private long _generation;

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

            // Captured before the call, so an invalidation that happens while it is in flight is
            // visible as a change by the time the result comes back.
            var generation = Volatile.Read(ref _generation);

            EnterInFlight(key);
            try
            {
                var context = await _inner.VerifyAsync(accessToken, cancellationToken)
                    .ConfigureAwait(false);

                UpdateCache(key, context, now, generation);

                Prune(now);
                return context;
            }
            finally
            {
                LeaveInFlight(key);
            }
        }

        private void UpdateCache(
            string key, KindeAuthContext context, DateTimeOffset now, long generation)
        {
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
                    // Under the lock, so the generation check and the insert cannot be split by an
                    // invalidation. Without it, a revocation landing in that gap would be undone by
                    // this write and the revoked token would keep authenticating for the whole
                    // cache lifetime.
                    lock (_sync)
                    {
                        if (Volatile.Read(ref _generation) == generation)
                        {
                            _entries[key] = new Entry(context, until);
                        }
                    }
                }
            }
            else
            {
                // A rejected token must not stay cached. The generation is bumped only when
                // another verification of this same key is still running and could insert a
                // stale success after this removal. Bumping unconditionally would also discard
                // in-flight successes for every other token, which are not in question here.
                lock (_sync)
                {
                    if (InFlightCountFor(key) > 1)
                    {
                        Interlocked.Increment(ref _generation);
                    }

                    _entries.TryRemove(key, out _);
                }
            }
        }

        /// <summary>Verifications currently running, by token key.</summary>
        private readonly ConcurrentDictionary<string, int> _inFlight =
            new ConcurrentDictionary<string, int>(StringComparer.Ordinal);

        private void EnterInFlight(string key) =>
            _inFlight.AddOrUpdate(key, 1, (_, count) => count + 1);

        private void LeaveInFlight(string key)
        {
            _inFlight.AddOrUpdate(key, 0, (_, count) => count - 1);
            _inFlight.TryRemove(new KeyValuePair<string, int>(key, 0));
        }

        private int InFlightCountFor(string key) =>
            _inFlight.TryGetValue(key, out var count) ? count : 0;

        /// <summary>Forgets a token, for use after an explicit revocation.</summary>
        public void Invalidate(string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                return;
            }

            var key = Hash(accessToken);

            lock (_sync)
            {
                // Bumped inside the lock, so a verification that is mid-insert either wrote before
                // this removal and is removed below, or sees the new generation and does not write.
                Interlocked.Increment(ref _generation);
                _entries.TryRemove(key, out _);
            }
        }

        /// <summary>Forgets everything.</summary>
        public void Clear()
        {
            lock (_sync)
            {
                Interlocked.Increment(ref _generation);
                _entries.Clear();
            }
        }

        /// <summary>The size at which expired entries are swept.</summary>
        private const int PruneThreshold = 1024;

        /// <summary>
        /// The hard ceiling on cached entries, enforced whatever the sweep schedule says.
        /// </summary>
        private const int MaxEntries = PruneThreshold * 2;

        private long _lastPruneTicks = DateTimeOffset.UtcNow.UtcTicks;

        private int _pruning;

        private void Prune(DateTimeOffset now)
        {
            // Entries are only evicted on access, so a server seeing many distinct tokens would
            // otherwise grow without bound.
            var count = _entries.Count;
            if (count < PruneThreshold)
            {
                return;
            }

            // The ceiling is enforced regardless of when the last sweep ran. Leaving it to the
            // timed sweep alone lets a burst of unique, valid tokens grow the dictionary far past
            // the cap for a whole lifetime window, which on a busy server is a memory exhaustion
            // risk rather than a tuning detail.
            var overCap = count >= MaxEntries;

            if (!overCap)
            {
                // At most one sweep per lifetime window. Without this, a cache sitting above the
                // threshold with every entry still live would walk the whole dictionary on every
                // single request and never remove anything.
                var last = Interlocked.Read(ref _lastPruneTicks);
                if (now.UtcTicks - last < _lifetime.Ticks)
                {
                    return;
                }
            }

            // One sweep at a time, whichever path arrived here, so a burst over the cap does not
            // start a sweep per request.
            if (Interlocked.CompareExchange(ref _pruning, 1, 0) != 0)
            {
                return;
            }

            try
            {
                Interlocked.Exchange(ref _lastPruneTicks, now.UtcTicks);

                foreach (var pair in _entries)
                {
                    if (pair.Value.ExpiresAt <= now)
                    {
                        _entries.TryRemove(pair.Key, out _);
                    }
                }

                // Still oversized once the expired entries are gone: every one is live, so the only
                // way to stay bounded is to drop some. They are re-verified on next use.
                if (_entries.Count >= MaxEntries)
                {
                    _entries.Clear();
                }
            }
            finally
            {
                Volatile.Write(ref _pruning, 0);
            }
        }

        private static string Hash(string token) =>
            Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));

        private readonly record struct Entry(KindeAuthContext Context, DateTimeOffset ExpiresAt);
    }
}
