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
    /// <para>
    /// Not the default, deliberately. A cache may evict an entry at any time, and losing a
    /// registration costs the user another consent round trip. Use this when the cache is backed
    /// by something durable, such as Redis with persistence, or when re-registering is acceptable.
    /// </para>
    /// <para>
    /// <strong>Single-writer only.</strong> <see cref="IDistributedCache"/> offers no
    /// compare-and-swap, so this store inherits the two-call default implementations of
    /// <see cref="IKindeAgentStore.SetIfAbsentAsync"/> and
    /// <see cref="IKindeAgentStore.RemoveIfAsync"/> and cannot make them atomic. Two instances
    /// registering at the same moment can therefore both write, and the loser would exchange its
    /// authorization code against the stored client's credentials. Likewise a conditional removal
    /// can delete a token another instance rotated in between.
    /// </para>
    /// <para>
    /// Run a single instance per agent identity, give each instance its own
    /// <see cref="KindeAgentClientOptions.SessionKey"/>, or subclass this store and override those
    /// two methods using a primitive the backing cache provides - Redis <c>SET NX</c> and a Lua
    /// compare-and-delete, for example.
    /// </para>
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
