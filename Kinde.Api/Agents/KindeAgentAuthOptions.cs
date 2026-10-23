#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kinde.Api.Agents
{
    /// <summary>How an inbound access token is checked.</summary>
    public enum KindeTokenVerificationMode
    {
        /// <summary>
        /// Ask Kinde whether the token is active. Revocation takes effect immediately, at the cost
        /// of one call to Kinde on every authenticated request: that adds latency to each one and
        /// exposes a busy server to rate limiting. Wrap the verifier in a
        /// <see cref="CachingTokenVerifier"/> to trade a bounded amount of revocation latency for
        /// far fewer calls.
        /// </summary>
        Introspection = 0,

        /// <summary>
        /// Verify the signature locally against the JWKS. No network call per request, but a
        /// revoked token stays valid until it expires (24 hours by default).
        /// </summary>
        /// <remarks>
        /// The JWKS is environment-wide and tokens carry the bare domain in <c>iss</c>, so the
        /// audience is the only thing tying a token to this application. Introspection is stricter:
        /// it is scoped to the application's own endpoint. Prefer introspection where more than
        /// one application in the environment could be issued the same resource URL.
        /// </remarks>
        Jwt = 1,

        /// <summary>Verify locally first, then confirm with introspection. Both must pass.</summary>
        JwtThenIntrospection = 2
    }

    /// <summary>
    /// Settings for accepting tokens issued by a Kinde "AI agents and MCP" application.
    /// Bound from the <see cref="SectionName"/> configuration section, following the same
    /// convention as the SDK's existing options types.
    /// </summary>
    public class KindeAgentAuthOptions
    {
        /// <summary>Configuration section these options bind from.</summary>
        public const string SectionName = "KindeAgentAuth";

        /// <summary>The scope that must be advertised, or agents never receive a refresh token.</summary>
        public const string OfflineScope = "offline";

        /// <summary>Kinde domain, for example https://example.kinde.com. A custom domain is fine.</summary>
        public string Domain { get; set; } = string.Empty;

        /// <summary>
        /// The AI agents and MCP application's client ID. This is deliberately separate from the
        /// client ID used for normal user login: a customer usually runs both.
        /// </summary>
        public string ClientId { get; set; } = string.Empty;

        /// <summary>
        /// The application's client secret, used for introspection and admin revoke.
        /// Never send it to a browser or an agent.
        /// </summary>
        public string ClientSecret { get; set; } = string.Empty;

        /// <summary>
        /// The Resource URL configured in Kinde. Compared byte for byte against the token's
        /// audience, so it must match what Kinde has exactly.
        /// </summary>
        public string ResourceUrl { get; set; } = string.Empty;

        /// <summary>Verification strategy. Introspection by default, so revocation is immediate.</summary>
        public KindeTokenVerificationMode Verification { get; set; } = KindeTokenVerificationMode.Introspection;

        /// <summary>Scopes advertised in the protected resource metadata. "offline" is always included.</summary>
        public IList<string> ScopesSupported { get; set; } = new List<string> { OfflineScope };

        /// <summary>Optional human-readable name advertised in the protected resource metadata.</summary>
        public string? ResourceName { get; set; }

        /// <summary>Leeway applied to exp and nbf.</summary>
        public TimeSpan ClockTolerance { get; set; } = TimeSpan.FromSeconds(30);

        /// <summary>Timeout for calls to Kinde (introspection, discovery).</summary>
        public TimeSpan BackchannelTimeout { get; set; } = TimeSpan.FromSeconds(5);

        // ----- Derived. Never ask the caller for these: endpoints come from the scoped discovery
        // document rather than being typed by hand. -----

        /// <summary>Domain with any trailing slash removed.</summary>
        public string NormalizedDomain => (Domain ?? string.Empty).TrimEnd('/');

        /// <summary>
        /// The application's own issuer. Note this is NOT the value tokens carry in <c>iss</c>;
        /// see <see cref="ExpectedTokenIssuer"/>.
        /// </summary>
        public string Issuer => $"{NormalizedDomain}/oauth2/agents/{ClientId}";

        /// <summary>
        /// What a token's <c>iss</c> claim actually contains: the bare domain, not <see cref="Issuer"/>.
        /// Comparing <c>iss</c> against the scoped issuer rejects every token, which is the single
        /// most common way to get this wrong.
        /// </summary>
        public string ExpectedTokenIssuer => NormalizedDomain;

        /// <summary>RFC 8414 discovery, which uses path insertion rather than appending.</summary>
        public string AuthorizationServerMetadataUrl =>
            $"{NormalizedDomain}/.well-known/oauth-authorization-server/oauth2/agents/{ClientId}";

        /// <summary>Environment-wide JWKS endpoint.</summary>
        public string JwksUrl => $"{NormalizedDomain}/.well-known/jwks";

        /// <summary>Introspection endpoint for this application.</summary>
        public string IntrospectionUrl => $"{Issuer}/introspect";

        /// <summary>Revocation endpoint for this application.</summary>
        public string RevocationUrl => $"{Issuer}/revoke";

        /// <summary>
        /// Path the protected resource metadata is served at. MCP clients try the path-suffixed form
        /// first, so /mcp becomes /.well-known/oauth-protected-resource/mcp.
        /// </summary>
        public string ProtectedResourceMetadataPath
        {
            get
            {
                var path = ResourceUrlPath;
                return path == "/" ? WellKnownProtectedResource : WellKnownProtectedResource + path.TrimEnd('/');
            }
        }

        /// <summary>Absolute URL of the protected resource metadata, used in the challenge header.</summary>
        public string ProtectedResourceMetadataUrl => ResourceUrlOrigin + ProtectedResourceMetadataPath;

        /// <summary>The advertised scopes, always including "offline".</summary>
        public IReadOnlyList<string> EffectiveScopesSupported
        {
            get
            {
                // Distinct because IConfiguration.Bind appends to a collection that already has a
                // default value rather than replacing it, so binding "offline" from configuration
                // onto the default would otherwise advertise it twice.
                var scopes = (ScopesSupported ?? new List<string>())
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Distinct(StringComparer.Ordinal)
                    .ToList();
                if (!scopes.Contains(OfflineScope, StringComparer.Ordinal))
                {
                    scopes.Insert(0, OfflineScope);
                }
                return scopes;
            }
        }

        internal const string WellKnownProtectedResource = "/.well-known/oauth-protected-resource";

        private string ResourceUrlPath => TryParseResourceUrl(out var uri) ? uri!.AbsolutePath : "/";

        private string ResourceUrlOrigin =>
            TryParseResourceUrl(out var uri) ? uri!.GetLeftPart(UriPartial.Authority) : string.Empty;

        private bool TryParseResourceUrl(out Uri? uri) =>
            Uri.TryCreate(ResourceUrl, UriKind.Absolute, out uri);

        /// <summary>
        /// Fails fast with a message naming the setting, rather than surfacing later as a confusing
        /// 401 or a request to a malformed URL.
        /// </summary>
        public void Validate() => Validate(requireClientSecret: true);

        /// <summary>
        /// Validates, optionally skipping the client secret check.
        /// </summary>
        /// <param name="requireClientSecret">
        /// False when a caller-supplied verifier is in use: it may not introspect at all, so
        /// demanding a secret would force a dummy value or a misleading verification mode.
        /// </param>
        public void Validate(bool requireClientSecret)
        {
            var problems = new List<string>();

            if (string.IsNullOrWhiteSpace(Domain))
            {
                problems.Add($"{nameof(Domain)} is required (for example https://example.kinde.com).");
            }
            else if (!Uri.TryCreate(NormalizedDomain, UriKind.Absolute, out var domainUri)
                     || domainUri.Scheme != Uri.UriSchemeHttps)
            {
                problems.Add($"{nameof(Domain)} must be an absolute https URL, but was '{Domain}'.");
            }

            if (string.IsNullOrWhiteSpace(ClientId))
            {
                problems.Add($"{nameof(ClientId)} is required. Use the AI agents and MCP application's " +
                             "client ID, not the one your web app signs users in with.");
            }

            if (requireClientSecret
                && string.IsNullOrWhiteSpace(ClientSecret)
                && Verification != KindeTokenVerificationMode.Jwt)
            {
                problems.Add($"{nameof(ClientSecret)} is required for {Verification} verification, " +
                             "because introspection authenticates with the application's credentials. " +
                             $"Set it, or use {nameof(KindeTokenVerificationMode.Jwt)} verification.");
            }

            if (string.IsNullOrWhiteSpace(ResourceUrl))
            {
                problems.Add($"{nameof(ResourceUrl)} is required and must equal the Resource URL " +
                             "configured on the Kinde application exactly.");
            }
            else if (!Uri.TryCreate(ResourceUrl, UriKind.Absolute, out var resourceUri))
            {
                problems.Add($"{nameof(ResourceUrl)} must be an absolute URL, but was '{ResourceUrl}'.");
            }
            else if (!string.IsNullOrEmpty(resourceUri.Fragment))
            {
                problems.Add($"{nameof(ResourceUrl)} must not contain a fragment, but was '{ResourceUrl}'.");
            }

            if (ClockTolerance < TimeSpan.Zero)
            {
                problems.Add($"{nameof(ClockTolerance)} cannot be negative.");
            }

            // Passed to new CancellationTokenSource(TimeSpan) on every verification, which throws
            // outside this range. Zero is rejected too: it cancels every call immediately, turning
            // each request into a 503.
            if (BackchannelTimeout <= TimeSpan.Zero
                || BackchannelTimeout.TotalMilliseconds > int.MaxValue)
            {
                problems.Add($"{nameof(BackchannelTimeout)} must be greater than zero and at most "
                             + $"{TimeSpan.FromMilliseconds(int.MaxValue)}, but was {BackchannelTimeout}.");
            }

            if (problems.Count > 0)
            {
                throw new ArgumentException(
                    $"Kinde agent authentication is not configured correctly (section '{SectionName}'):"
                    + Environment.NewLine + " - " + string.Join(Environment.NewLine + " - ", problems));
            }
        }
    }
}
