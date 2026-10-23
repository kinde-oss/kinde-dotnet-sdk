#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Kinde.Api.Agents.Client
{
    /// <summary>
    /// The opt-in store: keeps registrations and tokens in an <see cref="IDistributedCache"/>.
    /// </summary>
    /// <remarks>
    /// Not the default, deliberately. A cache may evict an entry at any time, and losing a
    /// registration costs the user another consent round trip. Use this when the cache is backed
    /// by something durable, such as Redis with persistence, or when re-registering is acceptable.
    /// </remarks>
    public class DistributedCacheKindeAgentStore : IKindeAgentStore
    {
        private const string KeyPrefix = "kinde:agent:";

        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _entryOptions;

        /// <summary>Creates a store over <paramref name="cache"/>.</summary>
        /// <param name="cache">The cache to use.</param>
        /// <param name="lifetime">
        /// How long entries live. Null keeps them until the cache evicts them, which is what you
        /// want: a registration has no natural expiry.
        /// </param>
        public DistributedCacheKindeAgentStore(IDistributedCache cache, TimeSpan? lifetime = null)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _entryOptions = lifetime.HasValue
                ? new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = lifetime }
                : new DistributedCacheEntryOptions();
        }

        /// <inheritdoc />
        public Task<string?> GetAsync(string key, CancellationToken cancellationToken = default) =>
            _cache.GetStringAsync(KeyPrefix + Require(key), cancellationToken);

        /// <inheritdoc />
        public Task SetAsync(
            string key, string value, CancellationToken cancellationToken = default) =>
            _cache.SetStringAsync(
                KeyPrefix + Require(key),
                value ?? throw new ArgumentNullException(nameof(value)),
                _entryOptions,
                cancellationToken);

        /// <inheritdoc />
        public Task RemoveAsync(string key, CancellationToken cancellationToken = default) =>
            _cache.RemoveAsync(KeyPrefix + Require(key), cancellationToken);

        private static string Require(string key) =>
            string.IsNullOrWhiteSpace(key)
                ? throw new ArgumentException("A key is required.", nameof(key))
                : key;
    }
}
