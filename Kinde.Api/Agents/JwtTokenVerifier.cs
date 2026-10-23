#nullable enable
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Kinde.Api.Agents
{
    /// <summary>
    /// Verifies tokens locally against Kinde's JWKS. No network call per request once the keys are
    /// cached, at the cost of a revoked token remaining valid until it expires.
    /// </summary>
    public class JwtTokenVerifier : IKindeTokenVerifier
    {
        private readonly KindeAgentAuthOptions _options;
        private readonly IConfigurationManager<OpenIdConnectConfiguration> _configurationManager;
        private readonly JwtSecurityTokenHandler _handler = new JwtSecurityTokenHandler
        {
            // Claims are read by their JWT names (sub, scope, permissions) rather than being
            // rewritten to the legacy SOAP URIs.
            MapInboundClaims = false
        };

        /// <summary>Creates a verifier that fetches and caches the JWKS from Kinde.</summary>
        public JwtTokenVerifier(KindeAgentAuthOptions options, HttpClient httpClient)
            : this(options, CreateConfigurationManager(
                options ?? throw new ArgumentNullException(nameof(options)),
                httpClient ?? throw new ArgumentNullException(nameof(httpClient))))
        {
        }

        /// <summary>
        /// Creates a verifier using a supplied configuration manager, which lets a test provide
        /// signing keys without an HTTP endpoint.
        /// </summary>
        public JwtTokenVerifier(
            KindeAgentAuthOptions options,
            IConfigurationManager<OpenIdConnectConfiguration> configurationManager)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _configurationManager = configurationManager
                                    ?? throw new ArgumentNullException(nameof(configurationManager));
        }

        /// <inheritdoc />
        public async Task<KindeAuthContext> VerifyAsync(
            string accessToken, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                return KindeAuthContext.MissingToken();
            }

            OpenIdConnectConfiguration configuration;

            // Bounded like introspection is. Without this a cold cache or a key rotation makes
            // every token-bearing request wait for HttpClient's 100 second default while the JWKS
            // endpoint is unreachable, where introspection mode would have returned 503 in five.
            using var timeout = new CancellationTokenSource(_options.BackchannelTimeout);
            using var linked = CancellationTokenSource.CreateLinkedTokenSource(
                cancellationToken, timeout.Token);

            try
            {
                configuration = await _configurationManager
                    .GetConfigurationAsync(linked.Token)
                    .ConfigureAwait(false);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (OperationCanceledException)
            {
                return KindeAuthContext.Unavailable(
                    $"Retrieving signing keys from {_options.JwksUrl} timed out after "
                    + $"{_options.BackchannelTimeout.TotalSeconds:0.##}s.");
            }
            catch (Exception exception)
            {
                return KindeAuthContext.Unavailable(
                    $"Could not retrieve signing keys from {_options.JwksUrl}: {exception.Message}");
            }

            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                // The bare domain. Validating against the scoped issuer rejects every token.
                ValidIssuer = _options.ExpectedTokenIssuer,
                ValidateAudience = true,
                ValidAudience = _options.ResourceUrl,
                ValidateIssuerSigningKey = true,
                IssuerSigningKeys = configuration.SigningKeys,
                ValidateLifetime = true,
                ClockSkew = _options.ClockTolerance,
                RequireSignedTokens = true,
                RequireExpirationTime = true,
                // Pinned so a token presented with "none" or a symmetric algorithm cannot be
                // validated against a key it was not meant for.
                ValidAlgorithms = new[] { SecurityAlgorithms.RsaSha256 }
            };

            try
            {
                _handler.ValidateToken(accessToken, parameters, out var validatedToken);

                if (validatedToken is not JwtSecurityToken jwt)
                {
                    return KindeAuthContext.Invalid("The token is not a JWT.");
                }

                var claims = ToClaimDictionary(jwt);
                var audiences = KindeClaimReader.GetList(claims, "aud");

                return KindeAuthContext.Success(
                    subject: KindeClaimReader.GetString(claims, "sub"),
                    actorSubject: KindeClaimReader.GetActorSubject(claims),
                    clientId: KindeClaimReader.GetClientId(claims),
                    scopes: KindeClaimReader.GetScopes(claims),
                    permissions: KindeClaimReader.GetList(claims, "permissions"),
                    audiences: audiences,
                    organizationCode: KindeClaimReader.GetString(claims, "org_code"),
                    expiresAt: KindeClaimReader.GetUnixTime(claims, "exp"),
                    claims: claims);
            }
            catch (SecurityTokenSignatureKeyNotFoundException)
            {
                // Most often a rotated key. Dropping the cache means the next request picks up the
                // new JWKS rather than failing until the refresh interval elapses.
                _configurationManager.RequestRefresh();
                return KindeAuthContext.Invalid("No signing key matched the token's key id.");
            }
            catch (SecurityTokenException exception)
            {
                return KindeAuthContext.Invalid(exception.Message);
            }
            catch (ArgumentException exception)
            {
                // Thrown for a structurally malformed token, before validation begins.
                return KindeAuthContext.Invalid($"The token is malformed: {exception.Message}");
            }
        }

        private static IConfigurationManager<OpenIdConnectConfiguration> CreateConfigurationManager(
            KindeAgentAuthOptions options, HttpClient httpClient)
        {
            // Pointed straight at the JWKS rather than at discovery: the keys are environment-wide,
            // so there is no need to resolve the scoped discovery document to find them.
            return new ConfigurationManager<OpenIdConnectConfiguration>(
                options.JwksUrl,
                new JwksRetriever(),
                new HttpDocumentRetriever(httpClient) { RequireHttps = true });
        }

        private static IReadOnlyDictionary<string, object> ToClaimDictionary(JwtSecurityToken jwt)
        {
            var claims = new Dictionary<string, object>(StringComparer.Ordinal);
            foreach (var claim in jwt.Payload)
            {
                claims[claim.Key] = claim.Value;
            }

            // The handler normalizes a single-element aud out of the payload in some versions, so
            // it is taken from the parsed token to keep audience checks reliable.
            if (!claims.ContainsKey("aud") && jwt.Audiences.Any())
            {
                claims["aud"] = jwt.Audiences.ToArray();
            }

            return claims;
        }

        /// <summary>
        /// Turns a bare JWKS document into an <see cref="OpenIdConnectConfiguration"/>, since
        /// Kinde serves the keys at a well-known path rather than inside a discovery document.
        /// </summary>
        private sealed class JwksRetriever : IConfigurationRetriever<OpenIdConnectConfiguration>
        {
            public async Task<OpenIdConnectConfiguration> GetConfigurationAsync(
                string address, IDocumentRetriever retriever, CancellationToken cancel)
            {
                var document = await retriever.GetDocumentAsync(address, cancel).ConfigureAwait(false);
                var keySet = new JsonWebKeySet(document);

                var configuration = new OpenIdConnectConfiguration { JwksUri = address };
                foreach (var key in keySet.GetSigningKeys())
                {
                    configuration.SigningKeys.Add(key);
                }

                return configuration;
            }
        }
    }
}
