#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kinde.Api.Agents.Client
{
    /// <summary>
    /// Persists what an agent client learns once and must not lose: its dynamic client
    /// registration, and the tokens it was issued.
    /// </summary>
    /// <remarks>
    /// Losing a registration is not merely inconvenient. The agent has to register again, which
    /// produces a new client id, which means the user is asked to consent again. The default
    /// implementation is therefore durable; a cache-backed store is available for callers who
    /// would rather trade that for speed.
    /// </remarks>
    public interface IKindeAgentStore
    {
        /// <summary>Reads a stored value, or null when it is absent.</summary>
        Task<string?> GetAsync(string key, CancellationToken cancellationToken = default);

        /// <summary>Writes a value, replacing any existing one.</summary>
        Task SetAsync(string key, string value, CancellationToken cancellationToken = default);

        /// <summary>Removes a value. Succeeds whether or not the key was present.</summary>
        Task RemoveAsync(string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes a value only if it still equals <paramref name="expectedValue"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Used to clean up after a rejected refresh. Two clients sharing a store can refresh the
        /// same expired token at once; the loser is told <c>invalid_grant</c> for a token the
        /// winner has already rotated, and an unconditional delete would then throw away the
        /// winner's perfectly good session and send the user back through consent.
        /// </para>
        /// <para>
        /// The default implementation compares and then removes, which closes the window within a
        /// process but not between processes. A store whose backing service supports an atomic
        /// compare-and-delete should override this; <see cref="FileKindeAgentStore"/> does.
        /// </para>
        /// </remarks>
        /// <returns>True when the value was removed, false when it had already changed.</returns>
        async Task<bool> RemoveIfAsync(
            string key, string expectedValue, CancellationToken cancellationToken = default)
        {
            var current = await GetAsync(key, cancellationToken).ConfigureAwait(false);
            if (!string.Equals(current, expectedValue, StringComparison.Ordinal))
            {
                return false;
            }

            await RemoveAsync(key, cancellationToken).ConfigureAwait(false);
            return true;
        }

        /// <summary>
        /// Writes a value only if the key is absent, returning what is stored either way.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Used when a dynamic client registration completes. Two processes sharing a store can
        /// register at the same time and receive different client ids; whichever writes last would
        /// otherwise replace the other's, and the loser would go on to exchange its authorization
        /// code using credentials belonging to a client that never made the request.
        /// </para>
        /// <para>
        /// Returning the stored value lets the loser adopt the winner's registration instead, so
        /// the two converge on one client rather than fighting over the key.
        /// </para>
        /// </remarks>
        /// <returns>
        /// Null when <paramref name="value"/> was written, otherwise the value already present.
        /// </returns>
        async Task<string?> SetIfAbsentAsync(
            string key, string value, CancellationToken cancellationToken = default)
        {
            var current = await GetAsync(key, cancellationToken).ConfigureAwait(false);
            if (current != null)
            {
                return current;
            }

            await SetAsync(key, value, cancellationToken).ConfigureAwait(false);
            return null;
        }
    }
}
