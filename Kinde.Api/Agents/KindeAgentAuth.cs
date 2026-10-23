#nullable enable
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kinde.Api.Agents
{
    /// <summary>
    /// Entry point for accepting agent tokens. Wraps the configured verification strategy so a
    /// resource server needs one object, and provides the protected resource metadata and the
    /// challenge header that go with it.
    /// </summary>
    /// <remarks>
    /// This type is framework-agnostic on purpose: the ASP.NET Core integration builds on it rather
    /// than reimplementing verification, and a host that is not ASP.NET Core can use it directly.
    /// </remarks>
    public class KindeAgentAuth
    {
        private const string BearerPrefix = "Bearer ";

        private readonly IKindeTokenVerifier _verifier;

        /// <summary>Creates the facade, validating the options and building the verifier.</summary>
        /// <param name="options">Options to validate and use.</param>
        /// <param name="httpClient">Client for calls to Kinde. Supply a pooled client.</param>
        public KindeAgentAuth(KindeAgentAuthOptions options, HttpClient httpClient)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
            if (httpClient is null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            Options.Validate();
            _verifier = CreateVerifier(Options, httpClient);
            Metadata = ProtectedResourceMetadata.FromOptions(Options);
        }

        /// <summary>Creates the facade with a caller-supplied verifier, for tests or a custom strategy.</summary>
        public KindeAgentAuth(KindeAgentAuthOptions options, IKindeTokenVerifier verifier)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
            _verifier = verifier ?? throw new ArgumentNullException(nameof(verifier));

            // A supplied verifier may never introspect, so a client secret is not required.
            Options.Validate(requireClientSecret: false);
            Metadata = ProtectedResourceMetadata.FromOptions(Options);
        }

        /// <summary>The validated options.</summary>
        public KindeAgentAuthOptions Options { get; }

        /// <summary>The metadata document to serve at <see cref="KindeAgentAuthOptions.ProtectedResourceMetadataPath"/>.</summary>
        public ProtectedResourceMetadata Metadata { get; }

        /// <summary>Verifies a raw bearer token.</summary>
        public Task<KindeAuthContext> VerifyTokenAsync(
            string accessToken, CancellationToken cancellationToken = default) =>
            _verifier.VerifyAsync(accessToken, cancellationToken);

        /// <summary>
        /// Verifies the credential in an <c>Authorization</c> header value, which may be null when
        /// the header is absent.
        /// </summary>
        public Task<KindeAuthContext> VerifyAuthorizationHeaderAsync(
            string? authorizationHeader, CancellationToken cancellationToken = default)
        {
            if (!TryGetBearerToken(authorizationHeader, out var token))
            {
                return Task.FromResult(KindeAuthContext.MissingToken());
            }

            return VerifyTokenAsync(token!, cancellationToken);
        }

        /// <summary>
        /// Extracts the token from an <c>Authorization</c> header value. Only the header form is
        /// accepted: the query-string and form-body forms are deprecated and are not advertised in
        /// the metadata.
        /// </summary>
        public static bool TryGetBearerToken(string? authorizationHeader, out string? token)
        {
            token = null;

            if (string.IsNullOrWhiteSpace(authorizationHeader)
                || !authorizationHeader!.StartsWith(BearerPrefix, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            var value = authorizationHeader.Substring(BearerPrefix.Length).Trim();
            if (value.Length == 0)
            {
                return false;
            }

            token = value;
            return true;
        }

        /// <summary>
        /// Builds the <c>WWW-Authenticate</c> value for a failed verification, or null when no
        /// challenge applies.
        /// </summary>
        /// <param name="context">The failed verification result.</param>
        /// <param name="includeErrorDetails">
        /// Whether to put <see cref="KindeAuthContext.FailureReason"/> in <c>error_description</c>.
        /// Off by default: that text carries validation messages, parse errors and the configured
        /// resource URL, and this header goes to the caller.
        /// </param>
        public string? BuildChallenge(KindeAuthContext context, bool includeErrorDetails = false)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var kind = context.FailureKind ?? KindeAuthFailureKind.InvalidToken;
            return KindeAuthChallenge.Build(
                kind,
                Options.ProtectedResourceMetadataUrl,
                includeErrorDetails ? context.FailureReason : null);
        }

        /// <summary>The status code to return for a failed verification.</summary>
        public static int StatusCodeFor(KindeAuthContext context) =>
            KindeAuthChallenge.StatusCodeFor(
                context?.FailureKind ?? KindeAuthFailureKind.InvalidToken);

        private static IKindeTokenVerifier CreateVerifier(
            KindeAgentAuthOptions options, HttpClient httpClient) => options.Verification switch
        {
            KindeTokenVerificationMode.Jwt => new JwtTokenVerifier(options, httpClient),
            KindeTokenVerificationMode.JwtThenIntrospection => new CompositeTokenVerifier(
                new JwtTokenVerifier(options, httpClient),
                new IntrospectionTokenVerifier(options, httpClient)),
            _ => new IntrospectionTokenVerifier(options, httpClient)
        };
    }
}
