#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kinde.Api.Agents
{
    /// <summary>
    /// Runs two verifiers in order, short-circuiting if the first rejects the token. Used by
    /// <see cref="KindeTokenVerificationMode.JwtThenIntrospection"/>, and available directly for
    /// composing a custom strategy such as a cache in front of introspection.
    /// </summary>
    public class CompositeTokenVerifier : IKindeTokenVerifier
    {
        private readonly IKindeTokenVerifier _first;
        private readonly IKindeTokenVerifier _second;

        /// <summary>Creates the composite.</summary>
        /// <param name="first">The cheap check, run on every request.</param>
        /// <param name="second">The authoritative check, run only if <paramref name="first"/> passes.</param>
        public CompositeTokenVerifier(IKindeTokenVerifier first, IKindeTokenVerifier second)
        {
            _first = first ?? throw new ArgumentNullException(nameof(first));
            _second = second ?? throw new ArgumentNullException(nameof(second));
        }

        /// <inheritdoc />
        public async Task<KindeAuthContext> VerifyAsync(
            string accessToken, CancellationToken cancellationToken = default)
        {
            // A forged token never reaches the network: the local check rejects it first, so an
            // attacker cannot use this resource server to drive junk traffic at Kinde.
            var first = await _first.VerifyAsync(accessToken, cancellationToken).ConfigureAwait(false);
            if (!first.IsAuthenticated)
            {
                return first;
            }

            // The second verifier is authoritative, so its result wins: a revoked but unexpired
            // token has a valid signature and is only caught here.
            return await _second.VerifyAsync(accessToken, cancellationToken).ConfigureAwait(false);
        }
    }
}
