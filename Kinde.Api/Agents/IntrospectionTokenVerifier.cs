#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Kinde.Api.Agents
{
    /// <summary>
    /// Verifies tokens by calling Kinde's introspection endpoint (RFC 7662). This is the default
    /// because it honours revocation immediately: a locally validated JWT stays valid until it
    /// expires, which for an agent token is up to 24 hours after it was revoked.
    /// </summary>
    public class IntrospectionTokenVerifier : IKindeTokenVerifier
    {
        private readonly KindeAgentAuthOptions _options;
        private readonly HttpClient _httpClient;
        private readonly string _basicAuthHeader;

        /// <summary>Creates a verifier using the supplied client.</summary>
        /// <param name="options">Validated options. Requires a client secret.</param>
        /// <param name="httpClient">Client used for the introspection call.</param>
        public IntrospectionTokenVerifier(KindeAgentAuthOptions options, HttpClient httpClient)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            // Escaped before encoding: a secret containing ':' would otherwise split in the wrong
            // place and Kinde would reject the credentials.
            var credentials =
                $"{Uri.EscapeDataString(_options.ClientId)}:{Uri.EscapeDataString(_options.ClientSecret)}";
            _basicAuthHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
        }

        /// <inheritdoc />
        public async Task<KindeAuthContext> VerifyAsync(
            string accessToken, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                return KindeAuthContext.MissingToken();
            }

            HttpResponseMessage response;
            string body;

            using var timeout = new CancellationTokenSource(_options.BackchannelTimeout);
            using var linked = CancellationTokenSource.CreateLinkedTokenSource(
                cancellationToken, timeout.Token);

            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Post, _options.IntrospectionUrl)
                {
                    Content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("token", accessToken),
                        new KeyValuePair<string, string>("token_type_hint", "access_token")
                    })
                };
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", _basicAuthHeader);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                response = await _httpClient.SendAsync(request, linked.Token).ConfigureAwait(false);
                body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                // The caller gave up; this is not an authorization server problem.
                throw;
            }
            catch (OperationCanceledException)
            {
                return KindeAuthContext.Unavailable(
                    $"Introspection timed out after {_options.BackchannelTimeout.TotalSeconds:0.##}s.");
            }
            catch (HttpRequestException exception)
            {
                return KindeAuthContext.Unavailable($"Introspection request failed: {exception.Message}");
            }

            using (response)
            {
                // A 401 or 403 here means Kinde rejected OUR credentials, not the agent's token.
                // Reporting that as an invalid token would send the agent into a login loop while
                // the real fault is a misconfigured secret, so it is surfaced as unavailable.
                if (response.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden)
                {
                    return KindeAuthContext.Unavailable(
                        $"Kinde rejected this resource server's credentials ({(int)response.StatusCode}). "
                        + $"Check {nameof(KindeAgentAuthOptions.ClientId)} and "
                        + $"{nameof(KindeAgentAuthOptions.ClientSecret)} in the "
                        + $"'{KindeAgentAuthOptions.SectionName}' section.");
                }

                if (!response.IsSuccessStatusCode)
                {
                    return KindeAuthContext.Unavailable(
                        $"Introspection returned {(int)response.StatusCode}.");
                }
            }

            Dictionary<string, object> claims;
            try
            {
                claims = ParseClaims(body);
            }
            catch (JsonException exception)
            {
                return KindeAuthContext.Unavailable(
                    $"Introspection returned a malformed response: {exception.Message}");
            }

            if (!IsActive(claims))
            {
                return KindeAuthContext.Invalid("The token is not active (expired or revoked).");
            }

            var audiences = KindeClaimReader.GetList(claims, "aud");
            if (!audiences.Contains(_options.ResourceUrl, StringComparer.Ordinal))
            {
                // RFC 8707: a token minted for another resource must not be accepted here, even
                // though it is a genuine, active token from the same tenant.
                return KindeAuthContext.Invalid(
                    $"The token's audience does not include this resource ('{_options.ResourceUrl}').");
            }

            return KindeAuthContext.Success(
                subject: KindeClaimReader.GetString(claims, "sub"),
                actorSubject: KindeClaimReader.GetActorSubject(claims),
                clientId: KindeClaimReader.GetString(claims, "client_id"),
                scopes: KindeClaimReader.GetSpaceDelimited(claims, "scope"),
                permissions: KindeClaimReader.GetList(claims, "permissions"),
                audiences: audiences,
                organizationCode: KindeClaimReader.GetString(claims, "org_code"),
                expiresAt: KindeClaimReader.GetUnixTime(claims, "exp"),
                claims: claims);
        }

        private static Dictionary<string, object> ParseClaims(string body)
        {
            var claims = new Dictionary<string, object>(StringComparer.Ordinal);
            if (string.IsNullOrWhiteSpace(body))
            {
                return claims;
            }

            using var document = JsonDocument.Parse(body);
            if (document.RootElement.ValueKind != JsonValueKind.Object)
            {
                throw new JsonException("Expected a JSON object.");
            }

            foreach (var property in document.RootElement.EnumerateObject())
            {
                // Cloned because the JsonDocument is disposed before the claims are read.
                claims[property.Name] = property.Value.Clone();
            }

            return claims;
        }

        private static bool IsActive(IReadOnlyDictionary<string, object> claims)
        {
            if (!claims.TryGetValue("active", out var raw))
            {
                return false;
            }

            return raw switch
            {
                bool value => value,
                JsonElement { ValueKind: JsonValueKind.True } => true,
                JsonElement { ValueKind: JsonValueKind.False } => false,
                JsonElement { ValueKind: JsonValueKind.String } element =>
                    bool.TryParse(element.GetString(), out var parsed) && parsed,
                string text => bool.TryParse(text, out var parsed) && parsed,
                _ => false
            };
        }
    }
}
