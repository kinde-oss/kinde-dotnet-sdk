#nullable enable
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
    }
}
