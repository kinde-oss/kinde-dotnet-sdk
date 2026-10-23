#nullable enable
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Kinde.Api.Agents.Client
{
    /// <summary>
    /// The agent side of the contract: discovers Kinde, registers itself, runs the PKCE
    /// authorization code flow and keeps the resulting token fresh.
    /// </summary>
    /// <remarks>
    /// This is what an MCP client does on the agent's behalf. The registration and tokens are kept
    /// in an <see cref="IKindeAgentStore"/>, so an agent registers once rather than asking the user
    /// to consent again on every restart.
    /// </remarks>
    public class KindeAgentClient : IDisposable
    {
        private static readonly JsonSerializerOptions Json = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private readonly KindeAgentClientOptions _options;
        private readonly HttpClient _httpClient;
        private readonly IKindeAgentStore _store;

        /// <summary>Assumed lifetime when a token response omits expires_in.</summary>
        private static readonly TimeSpan DefaultTokenLifetime = TimeSpan.FromHours(1);

        private readonly SemaphoreSlim _registrationGate = new(1, 1);

        private KindeAgentDiscoveryDocument? _discovery;

        public KindeAgentClient(
            KindeAgentClientOptions options, HttpClient httpClient, IKindeAgentStore? store = null)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _options.Validate();

            // Durable by default: a cache-backed store loses the registration and costs the user
            // another consent round trip.
            _store = store ?? FileKindeAgentStore.ForCurrentUser();
        }

        /// <summary>Fetches and caches the authorization server metadata.</summary>
        public async Task<KindeAgentDiscoveryDocument> GetDiscoveryDocumentAsync(
            CancellationToken cancellationToken = default)
        {
            if (_discovery != null)
            {
                return _discovery;
            }

            var document = await GetJsonAsync<KindeAgentDiscoveryDocument>(
                _options.AuthorizationServerMetadataUrl, cancellationToken).ConfigureAwait(false);

            // RFC 8414 section 3.3: the issuer must match where the document was fetched from, or
            // a tampered or misrouted response could point the flow at another server.
            var expectedIssuer = $"{_options.NormalizedDomain}/oauth2/agents/{_options.AgentApplicationId}";
            if (!string.Equals(document.Issuer, expectedIssuer, StringComparison.Ordinal))
            {
                throw new InvalidOperationException(
                    $"The discovery document at {_options.AuthorizationServerMetadataUrl} declares "
                    + $"issuer '{document.Issuer}', but '{expectedIssuer}' was expected.");
            }

            if (string.IsNullOrEmpty(document.TokenEndpoint))
            {
                throw new InvalidOperationException(
                    $"The discovery document at {_options.AuthorizationServerMetadataUrl} has no "
                    + "token_endpoint.");
            }

            if (string.IsNullOrEmpty(document.AuthorizationEndpoint))
            {
                throw new InvalidOperationException(
                    $"The discovery document at {_options.AuthorizationServerMetadataUrl} has no "
                    + "authorization_endpoint.");
            }

            // The MCP authorization spec requires a client to refuse to proceed unless S256 is
            // advertised. An absent field is not a licence to continue: without verified support
            // the server may fall back to "plain", which leaves the code interceptable.
            if (!document.CodeChallengeMethodsSupported.Contains("S256", StringComparer.Ordinal))
            {
                throw new InvalidOperationException(
                    $"The authorization server at {expectedIssuer} does not support the S256 code "
                    + "challenge method, which is required for PKCE.");
            }

            return _discovery = document;
        }

        /// <summary>
        /// Returns the stored registration, registering with Kinde (RFC 7591) the first time.
        /// </summary>
        public async Task<KindeAgentRegistration> GetOrRegisterAsync(
            CancellationToken cancellationToken = default)
        {
            // Single-flight: two callers racing here would both register, and the later SetAsync
            // would store client B while a token had already been issued to client A. The next
            // refresh then fails invalid_grant and the user has to consent again.
            await _registrationGate.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                return await GetOrRegisterCoreAsync(cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                _registrationGate.Release();
            }
        }

        private async Task<KindeAgentRegistration> GetOrRegisterCoreAsync(
            CancellationToken cancellationToken)
        {
            var key = RegistrationKey;

            var stored = await _store.GetAsync(key, cancellationToken).ConfigureAwait(false);
            if (stored != null)
            {
                var existing = Deserialize<KindeAgentRegistration>(stored);
                if (existing != null && !string.IsNullOrEmpty(existing.ClientId)
                    && !IsSecretExpired(existing))
                {
                    return existing;
                }

                // Expired secret: registering again is the only way back, and reusing it would
                // make every token request fail with invalid_client.
                await _store.RemoveAsync(key, cancellationToken).ConfigureAwait(false);
            }

            var discovery = await GetDiscoveryDocumentAsync(cancellationToken).ConfigureAwait(false);
            if (string.IsNullOrEmpty(discovery.RegistrationEndpoint))
            {
                throw new InvalidOperationException(
                    "This Kinde application does not advertise a registration_endpoint, so dynamic "
                    + "client registration is unavailable. Enable it on the application, or supply "
                    + $"a {nameof(KindeAgentClientOptions.ClientId)} instead.");
            }

            var body = new Dictionary<string, object?>
            {
                ["client_name"] = _options.ClientName,
                ["redirect_uris"] = new[] { _options.RedirectUri },
                ["grant_types"] = new[] { "authorization_code", "refresh_token" },
                ["response_types"] = new[] { "code" },
                ["token_endpoint_auth_method"] = "client_secret_post"
            };

            using var request = new HttpRequestMessage(HttpMethod.Post, discovery.RegistrationEndpoint)
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
            };

            using var response = await _httpClient.SendAsync(request, cancellationToken)
                .ConfigureAwait(false);
            var text = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(
                    $"Dynamic client registration failed ({(int)response.StatusCode}): {text}");
            }

            var registration = Deserialize<KindeAgentRegistration>(text)
                               ?? throw new InvalidOperationException(
                                   "Dynamic client registration returned no client_id.");

            await _store.SetAsync(key, text, cancellationToken).ConfigureAwait(false);
            return registration;
        }

        /// <summary>
        /// Builds the URL to send the user to, along with the state and PKCE verifier the callback
        /// will need. Hold on to the returned object until the callback arrives.
        /// </summary>
        public async Task<KindeAgentAuthorizationRequest> CreateAuthorizationRequestAsync(
            CancellationToken cancellationToken = default)
        {
            var discovery = await GetDiscoveryDocumentAsync(cancellationToken).ConfigureAwait(false);
            var clientId = await ResolveClientIdAsync(cancellationToken).ConfigureAwait(false);

            var verifier = CreateCodeVerifier();
            var state = CreateCodeVerifier();

            var parameters = new Dictionary<string, string>
            {
                ["response_type"] = "code",
                ["client_id"] = clientId,
                ["redirect_uri"] = _options.RedirectUri,
                ["state"] = state,
                ["code_challenge"] = CreateCodeChallenge(verifier),
                ["code_challenge_method"] = "S256",
                // Always includes "offline", or Kinde issues no refresh token and the agent has to
                // ask the user to consent again as soon as the access token expires.
                ["scope"] = string.Join(" ", _options.EffectiveScopes)
            };

            // RFC 8707. Without this the token's audience is not this resource, and the resource
            // server rejects it even though it is a perfectly valid token.
            if (!string.IsNullOrWhiteSpace(_options.ResourceUrl))
            {
                parameters["resource"] = _options.ResourceUrl;
            }

            var query = string.Join("&", BuildQuery(parameters));

            return new KindeAgentAuthorizationRequest
            {
                AuthorizationUrl = $"{discovery.AuthorizationEndpoint}?{query}",
                State = state,
                CodeVerifier = verifier,
                RedirectUri = _options.RedirectUri
            };
        }

        /// <summary>
        /// Exchanges the authorization code for a token and stores it.
        /// </summary>
        /// <param name="code">The code from the callback.</param>
        /// <param name="returnedState">The state from the callback, checked against the request.</param>
        /// <param name="request">The request produced by <see cref="CreateAuthorizationRequestAsync"/>.</param>
        public async Task<KindeAgentToken> ExchangeCodeAsync(
            string code,
            string returnedState,
            KindeAgentAuthorizationRequest request,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentException("An authorization code is required.", nameof(code));
            }

            // Compared in fixed time: this is the CSRF check, and a timing-variable comparison
            // leaks how much of the state an attacker has guessed.
            if (!FixedTimeEquals(returnedState, request.State))
            {
                throw new InvalidOperationException(
                    "The state returned by Kinde does not match the request. The callback may have "
                    + "been forged, or it belongs to a different authorization attempt.");
            }

            var form = new Dictionary<string, string>
            {
                ["grant_type"] = "authorization_code",
                ["code"] = code,
                ["redirect_uri"] = request.RedirectUri,
                ["code_verifier"] = request.CodeVerifier
            };

            if (!string.IsNullOrWhiteSpace(_options.ResourceUrl))
            {
                form["resource"] = _options.ResourceUrl!;
            }

            return await RequestTokenAsync(form, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns a usable access token, refreshing it when it is close to expiry. Returns null
        /// when there is no stored token, or the refresh failed and the user must authorize again.
        /// </summary>
        public async Task<KindeAgentToken?> GetCurrentTokenAsync(
            CancellationToken cancellationToken = default)
        {
            var token = await ReadTokenAsync(cancellationToken).ConfigureAwait(false);
            if (token == null)
            {
                return null;
            }

            if (!token.IsExpired())
            {
                return token;
            }

            if (string.IsNullOrEmpty(token.RefreshToken))
            {
                // No refresh token means the "offline" scope was not granted, so there is nothing
                // to do but send the user through authorization again.
                return null;
            }

            try
            {
                return await RefreshAsync(token.RefreshToken!, cancellationToken).ConfigureAwait(false);
            }
            catch (KindeAgentTokenException exception) when (exception.IsGrantRejected)
            {
                // Two callers can refresh the same expired token at once. If Kinde rotates refresh
                // tokens, the loser gets invalid_grant for a token the winner already replaced, so
                // re-read before deleting rather than discarding the winner's fresh token.
                var current = await ReadTokenAsync(cancellationToken).ConfigureAwait(false);
                if (current != null
                    && !string.Equals(current.RefreshToken, token.RefreshToken, StringComparison.Ordinal)
                    && !current.IsExpired())
                {
                    return current;
                }

                // Otherwise a revoked or expired refresh token is a normal end of session.
                await _store.RemoveAsync(TokenKey, cancellationToken).ConfigureAwait(false);
                return null;
            }
            // A 429 or 5xx, or a failure to reach Kinde at all, leaves the refresh token intact:
            // discarding it over a transient outage would cost the user another consent.
        }

        /// <summary>Reads the stored token, or null when there is none worth using.</summary>
        private async Task<KindeAgentToken?> ReadTokenAsync(CancellationToken cancellationToken)
        {
            var stored = await _store.GetAsync(TokenKey, cancellationToken).ConfigureAwait(false);
            if (stored == null)
            {
                return null;
            }

            var token = Deserialize<KindeAgentToken>(stored);
            return token == null || string.IsNullOrEmpty(token.AccessToken) ? null : token;
        }

        /// <summary>Exchanges a refresh token for a new access token and stores it.</summary>
        public Task<KindeAgentToken> RefreshAsync(
            string refreshToken, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new ArgumentException("A refresh token is required.", nameof(refreshToken));
            }

            var form = new Dictionary<string, string>
            {
                ["grant_type"] = "refresh_token",
                ["refresh_token"] = refreshToken
            };

            if (!string.IsNullOrWhiteSpace(_options.ResourceUrl))
            {
                form["resource"] = _options.ResourceUrl!;
            }

            return RequestTokenAsync(form, cancellationToken, carryOverRefreshToken: refreshToken);
        }

        /// <summary>Releases the gate that serialises dynamic registration.</summary>
        public void Dispose()
        {
            _registrationGate.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>Forgets the stored token, leaving the registration in place.</summary>
        public Task SignOutAsync(CancellationToken cancellationToken = default) =>
            _store.RemoveAsync(TokenKey, cancellationToken);

        private async Task<KindeAgentToken> RequestTokenAsync(
            Dictionary<string, string> form,
            CancellationToken cancellationToken,
            string? carryOverRefreshToken = null)
        {
            var discovery = await GetDiscoveryDocumentAsync(cancellationToken).ConfigureAwait(false);
            var clientId = await ResolveClientIdAsync(cancellationToken).ConfigureAwait(false);

            form["client_id"] = clientId;

            var secret = await ResolveClientSecretAsync(cancellationToken).ConfigureAwait(false);
            if (!string.IsNullOrEmpty(secret))
            {
                // client_secret_post, which is what Kinde's agent token endpoint advertises.
                form["client_secret"] = secret!;
            }

            using var request = new HttpRequestMessage(HttpMethod.Post, discovery.TokenEndpoint)
            {
                Content = new FormUrlEncodedContent(form)
            };

            // Taken before the call: expires_in is relative to when Kinde issued the token, so
            // stamping it after the response would push expiry past Kinde's own clock.
            var requestedAt = DateTimeOffset.UtcNow;

            using var response = await _httpClient.SendAsync(request, cancellationToken)
                .ConfigureAwait(false);
            var text = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                throw new KindeAgentTokenException(response.StatusCode, text);
            }

            var token = Deserialize<KindeAgentToken>(text)
                        ?? throw new InvalidOperationException("The token response was empty.");

            if (string.IsNullOrEmpty(token.AccessToken))
            {
                throw new InvalidOperationException("The token response contained no access_token.");
            }

            // RFC 6749 section 6 allows a refresh response to omit refresh_token, in which case
            // the existing one stays valid. Storing the response as-is would null it out and cost
            // the user a fresh consent at the next expiry.
            if (string.IsNullOrEmpty(token.RefreshToken)
                && !string.IsNullOrEmpty(carryOverRefreshToken))
            {
                token.RefreshToken = carryOverRefreshToken;
            }

            // A response without expires_in would otherwise leave ExpiresIn at 0, making the token
            // permanently expired and sending every call through a refresh.
            if (token.ExpiresIn <= 0)
            {
                token.ExpiresIn = (int)DefaultTokenLifetime.TotalSeconds;
            }

            token.ObtainedAt = requestedAt;
            await _store.SetAsync(TokenKey, JsonSerializer.Serialize(token), cancellationToken)
                .ConfigureAwait(false);

            return token;
        }

        private async Task<string> ResolveClientIdAsync(CancellationToken cancellationToken)
        {
            // A configured client id wins: it covers both a pre-registered client and CIMD, where
            // the id is the URL of the client's own metadata document.
            if (!string.IsNullOrWhiteSpace(_options.ClientId))
            {
                return _options.ClientId!;
            }

            var registration = await GetOrRegisterAsync(cancellationToken).ConfigureAwait(false);
            return registration.ClientId;
        }

        private async Task<string?> ResolveClientSecretAsync(CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(_options.ClientSecret))
            {
                return _options.ClientSecret;
            }

            if (!string.IsNullOrWhiteSpace(_options.ClientId))
            {
                // Explicit id with no secret: a public client, which PKCE already protects.
                return null;
            }

            var registration = await GetOrRegisterAsync(cancellationToken).ConfigureAwait(false);
            return registration.ClientSecret;
        }

        /// <summary>
        /// Whether a registration's secret has lapsed. Zero means it never expires, per RFC 7591.
        /// </summary>
        private static bool IsSecretExpired(KindeAgentRegistration registration) =>
            registration.ClientSecretExpiresAt > 0
            && DateTimeOffset.FromUnixTimeSeconds(registration.ClientSecretExpiresAt)
               <= DateTimeOffset.UtcNow;

        // The redirect URI is part of the key: a registration is tied to the URI it declared, so
        // reusing one after the URI changes produces a redirect_uri mismatch at authorization.
        private string RegistrationKey =>
            $"{_options.NormalizedDomain}|{_options.AgentApplicationId}|{_options.RedirectUri}|registration";

        // The resource is part of the key: a token's audience is bound to it, so returning one
        // issued for a different resource would be rejected by that resource server as if forged.
        private string TokenKey =>
            $"{_options.NormalizedDomain}|{_options.AgentApplicationId}|{_options.ResourceUrl}|token";

        private async Task<T> GetJsonAsync<T>(string url, CancellationToken cancellationToken)
        {
            using var response = await _httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
            var text = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(
                    $"GET {url} failed ({(int)response.StatusCode}): {text}");
            }

            return Deserialize<T>(text)
                   ?? throw new InvalidOperationException($"GET {url} returned an empty document.");
        }

        private static T? Deserialize<T>(string text)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(text, Json);
            }
            catch (JsonException exception)
            {
                throw new InvalidOperationException(
                    $"Could not read the response as {typeof(T).Name}: {exception.Message}");
            }
        }

        private static IEnumerable<string> BuildQuery(Dictionary<string, string> parameters)
        {
            foreach (var pair in parameters)
            {
                // EscapeDataString rather than UrlEncode: percent-encoded spaces and uppercase hex are
                // the interoperable form for a URL query, and it drops the System.Web dependency.
                yield return Uri.EscapeDataString(pair.Key) + "=" + Uri.EscapeDataString(pair.Value);
            }
        }

        /// <summary>A 43-character URL-safe random string, per RFC 7636.</summary>
        private static string CreateCodeVerifier() =>
            Base64Url(RandomNumberGenerator.GetBytes(32));

        private static string CreateCodeChallenge(string verifier) =>
            Base64Url(SHA256.HashData(Encoding.ASCII.GetBytes(verifier)));

        private static string Base64Url(byte[] bytes) =>
            Convert.ToBase64String(bytes).TrimEnd('=').Replace('+', '-').Replace('/', '_');

        private static bool FixedTimeEquals(string? left, string? right)
        {
            if (left is null || right is null)
            {
                return false;
            }

            var a = Encoding.UTF8.GetBytes(left);
            var b = Encoding.UTF8.GetBytes(right);

            return a.Length == b.Length && CryptographicOperations.FixedTimeEquals(a, b);
        }
    }
}
