#nullable enable
using System.Threading;
using System.Threading.Tasks;

namespace Kinde.Api.Agents
{
    /// <summary>
    /// Verifies an inbound access token. Implement this to plug in a different strategy, for
    /// example a cache in front of introspection.
    /// </summary>
    public interface IKindeTokenVerifier
    {
        /// <summary>
        /// Verifies <paramref name="accessToken"/> and returns the result. Does not throw for a bad
        /// token: an invalid token is a normal outcome, reported through the returned context so the
        /// caller can map it to the right status code.
        /// </summary>
        /// <param name="accessToken">The raw bearer token, without the "Bearer " prefix.</param>
        /// <param name="cancellationToken">Cancellation from the caller, which is propagated.</param>
        Task<KindeAuthContext> VerifyAsync(string accessToken, CancellationToken cancellationToken = default);
    }
}
