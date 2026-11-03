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

        /// <summary>
        /// Serialises token persistence against sign-out, so the two cannot interleave.
        /// </summary>
        private readonly SemaphoreSlim _tokenGate = new(1, 1);

        /// <summary>
        /// Bumped by every sign-out. A token request captures this before its HTTP call and
        /// abandons the write if it has moved, so a refresh that was already in flight cannot
        /// resurrect a session that has since been ended.
        /// </summary>
        private long _signOutGeneration;

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

            // Each endpoint must be an absolute https URL. The issuer check above proves the
            // document names the right server, not that the endpoints inside it do: an http value
            // would downgrade the flow, and a relative one would produce a malformed request.
            RequireHttpsEndpoint(document.TokenEndpoint, "token_endpoint", required: true);
            RequireHttpsEndpoint(
                document.AuthorizationEndpoint, "authorization_endpoint", required: true);
            RequireHttpsEndpoint(
                document.RegistrationEndpoint, "registration_endpoint", required: false);

            // The MCP authorization spec requires a client to refuse to proceed unless S256 is
            // advertised. An absent field is not a licence to continue: without verified support
            // the server may fall back to "plain", which leaves the code interceptable.
            // Null-safe: a response carrying "code_challenge_methods_supported": null leaves the
            // list null, and dereferencing it would throw NullReferenceException instead of the
            // explanatory error below.
            if (document.CodeChallengeMethodsSupported?.Contains("S256", StringComparer.Ordinal)
                != true)
            {
                throw new InvalidOperationException(
                    $"The authorization server at {expectedIssuer} does not support the S256 code "
                    + "challenge method, which is required for PKCE.");
            }

            return _discovery = document;
        }

        /// <summary>
        /// Fetches and validates a protected resource's metadata document (RFC 9728).
        /// </summary>
        /// <remarks>
        /// This is the other half of discovery. A resource server answers an unauthenticated call
        /// with <c>401</c> and a <c>WWW-Authenticate</c> header carrying
        /// <c>resource_metadata="..."</c>; this reads that document, so a client can learn which
        /// authorization servers can issue tokens for the resource instead of being told in
        /// advance.
        /// </remarks>
        /// <param name="resourceMetadataUrl">
        /// The <c>resource_metadata</c> URL from the challenge.
        /// </param>
        /// <param name="cancellationToken">Cancels the fetch.</param>
        /// <exception cref="InvalidOperationException">
        /// The URL is not https, or the document is self-inconsistent.
        /// </exception>
        public async Task<KindeProtectedResourceMetadata> GetProtectedResourceMetadataAsync(
            string resourceMetadataUrl, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(resourceMetadataUrl))
            {
                throw new ArgumentException(
                    "A resource metadata URL is required.", nameof(resourceMetadataUrl));
            }

            // The URL arrives from a challenge, so it is attacker-influenced until proven
            // otherwise; an http value would leak the request and allow it to be rewritten.
            RequireHttpsEndpoint(resourceMetadataUrl, "resource_metadata", required: true);

            var metadata = await GetJsonAsync<KindeProtectedResourceMetadata>(
                resourceMetadataUrl, cancellationToken).ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(metadata.Resource))
            {
                throw new InvalidOperationException(
                    $"The resource metadata at {resourceMetadataUrl} declares no 'resource'.");
            }

            if (metadata.AuthorizationServers.Count == 0)
            {
                throw new InvalidOperationException(
                    $"The resource metadata at {resourceMetadataUrl} lists no "
                    + "'authorization_servers', so there is no server to authorize against.");
            }

            // A configured resource must match what the document describes, or the agent would be
            // authorizing for one resource while calling another.
            if (!string.IsNullOrWhiteSpace(_options.ResourceUrl)
                && !string.Equals(metadata.Resource, _options.ResourceUrl, StringComparison.Ordinal))
            {
                throw new InvalidOperationException(
                    $"The resource metadata at {resourceMetadataUrl} describes resource "
                    + $"'{metadata.Resource}', but this client is configured for "
                    + $"'{_options.ResourceUrl}'.");
            }

            return metadata;
        }

        /// <summary>
        /// Runs the full protected-resource discovery flow: reads the resource's metadata, checks
        /// that it trusts this client's Kinde authorization server, and returns that server's
        /// metadata.
        /// </summary>
        /// <remarks>
        /// The authorization server is not taken from the document. A resource names the servers
        /// it trusts, but following that blindly would let a hostile or compromised resource point
        /// the agent at a server of its choosing and harvest a code. The advertised list is instead
        /// required to contain the issuer this client is already configured for.
        /// </remarks>
        /// <param name="resourceMetadataUrl">
        /// The <c>resource_metadata</c> URL from the resource server's 401 challenge.
        /// </param>
        /// <param name="cancellationToken">Cancels the fetches.</param>
        public async Task<KindeAgentDiscoveryDocument> DiscoverFromResourceMetadataAsync(
            string resourceMetadataUrl, CancellationToken cancellationToken = default)
        {
            var metadata = await GetProtectedResourceMetadataAsync(
                resourceMetadataUrl, cancellationToken).ConfigureAwait(false);

            var expectedIssuer =
                $"{_options.NormalizedDomain}/oauth2/agents/{_options.AgentApplicationId}";

            if (!metadata.AuthorizationServers.Contains(expectedIssuer, StringComparer.Ordinal))
            {
                throw new InvalidOperationException(
                    $"The resource at '{metadata.Resource}' does not list '{expectedIssuer}' among "
                    + "its authorization_servers, so a token from this Kinde application would not "
                    + "be accepted. It advertises: "
                    + string.Join(", ", metadata.AuthorizationServers) + ".");
            }

            return await GetDiscoveryDocumentAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Whether a redirect URI is the loopback or custom-scheme kind a native agent uses.
        /// </summary>
        private static bool IsNativeRedirect(string redirectUri)
        {
            if (!Uri.TryCreate(redirectUri, UriKind.Absolute, out var uri))
            {
                return false;
            }

            if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
            {
                // A custom scheme, such as my-agent://callback.
                return true;
            }

            return uri.IsLoopback;
        }

        /// <summary>
        /// Rejects a discovery endpoint that is missing, relative, or not https.
        /// </summary>
        private void RequireHttpsEndpoint(string? value, string name, bool required)
        {
            if (string.IsNullOrEmpty(value))
            {
                if (!required)
                {
                    return;
                }

                throw new InvalidOperationException(
                    $"The discovery document at {_options.AuthorizationServerMetadataUrl} has no "
                    + $"{name}.");
            }

            if (!Uri.TryCreate(value, UriKind.Absolute, out var uri)
                || uri.Scheme != Uri.UriSchemeHttps)
            {
                throw new InvalidOperationException(
                    $"The discovery document at {_options.AuthorizationServerMetadataUrl} declares "
                    + $"{name} '{value}', which is not an absolute https URL.");
            }
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
                ["token_endpoint_auth_method"] = "client_secret_post",
                // OIDC registration defaults an omitted application_type to "web", which can
                // reject the loopback and custom-scheme redirects a desktop agent needs.
                ["application_type"] = IsNativeRedirect(_options.RedirectUri) ? "native" : "web"
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

            var registration = Deserialize<KindeAgentRegistration>(text);

            // Deserialization of "{}" yields an object with an empty ClientId, so a null check
            // alone would cache a useless registration and send client_id= on every request.
            if (registration == null || string.IsNullOrEmpty(registration.ClientId))
            {
                throw new InvalidOperationException(
                    "Dynamic client registration returned no client_id.");
            }

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
        /// <param name="returnedIssuer">
        /// The <c>iss</c> parameter from the callback, when the authorization server sent one.
        /// Checking it (RFC 9207) stops a code issued by one server being redeemed at another's
        /// token endpoint, which is the authorization-server mix-up attack.
        /// </param>
        public async Task<KindeAgentToken> ExchangeCodeAsync(
            string code,
            string returnedState,
            KindeAgentAuthorizationRequest request,
            CancellationToken cancellationToken = default,
            string? returnedIssuer = null)
        {
            ArgumentNullException.ThrowIfNull(request);

            var discovery = await GetDiscoveryDocumentAsync(cancellationToken).ConfigureAwait(false);
            var expectedIssuer =
                $"{_options.NormalizedDomain}/oauth2/agents/{_options.AgentApplicationId}";

            if (!string.IsNullOrEmpty(returnedIssuer))
            {
                if (!string.Equals(returnedIssuer, expectedIssuer, StringComparison.Ordinal))
                {
                    throw new InvalidOperationException(
                        $"The callback declares issuer '{returnedIssuer}', but this authorization "
                        + $"request went to '{expectedIssuer}'. The code may belong to a different "
                        + "authorization server.");
                }
            }
            else if (discovery.AuthorizationResponseIssParameterSupported)
            {
                // RFC 9207: once a server advertises that it returns iss, a response without one
                // must be refused. Skipping the check when the parameter is absent would let an
                // attacker strip it and so disable the mix-up protection the server promised.
                throw new InvalidOperationException(
                    $"The authorization server at {expectedIssuer} advertises "
                    + "authorization_response_iss_parameter_supported, but the callback carried no "
                    + "'iss' parameter. Pass the iss value from the callback, or treat the "
                    + "response as forged.");
            }

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
                var refreshed = await RefreshCoreAsync(token.RefreshToken!, cancellationToken)
                    .ConfigureAwait(false);

                // A sign-out overtook the refresh. The session is over, so report no token rather
                // than the one that just arrived.
                return refreshed.Persisted ? refreshed.Token : null;
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
        public async Task<KindeAgentToken> RefreshAsync(
            string refreshToken, CancellationToken cancellationToken = default)
        {
            var result = await RefreshCoreAsync(refreshToken, cancellationToken).ConfigureAwait(false);
            return result.Token;
        }

        private Task<TokenRequestResult> RefreshCoreAsync(
            string refreshToken, CancellationToken cancellationToken)
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

            return RequestTokenCoreAsync(
                form, cancellationToken, carryOverRefreshToken: refreshToken);
        }

        /// <summary>Releases the gates that serialise registration and token persistence.</summary>
        public void Dispose()
        {
            _registrationGate.Dispose();
            _tokenGate.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Ends the session: revokes the tokens at Kinde when the server advertises a revocation
        /// endpoint, then forgets them locally. The registration is left in place.
        /// </summary>
        public Task SignOutAsync(CancellationToken cancellationToken = default) =>
            SignOutAsync(revoke: true, cancellationToken);

        /// <summary>
        /// Ends the session, optionally without contacting Kinde.
        /// </summary>
        /// <param name="revoke">
        /// Whether to revoke the tokens server-side first. Revocation is best-effort: a failure to
        /// reach Kinde still removes the local token, because a sign-out that leaves a usable token
        /// on disk because the network blipped is worse than one that leaves a token live at Kinde
        /// until it expires.
        /// </param>
        /// <param name="cancellationToken">Cancels the revocation call.</param>
        public async Task SignOutAsync(bool revoke, CancellationToken cancellationToken = default)
        {
            var token = revoke
                ? await ReadTokenAsync(cancellationToken).ConfigureAwait(false)
                : null;

            // Taken before the removal and held across it, so a refresh that is already in flight
            // cannot persist its result afterwards: RequestTokenAsync compares the generation it
            // captured before its own HTTP call and abandons the write when it has moved. Without
            // this, a refresh finishing just after sign-out writes a fresh token to the same key
            // and GetCurrentTokenAsync reports an authenticated session again.
            await _tokenGate.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                Interlocked.Increment(ref _signOutGeneration);
                await _store.RemoveAsync(TokenKey, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                _tokenGate.Release();
            }

            if (token == null)
            {
                return;
            }

            // The refresh token first: revoking it is what actually ends the session, since an
            // access token expires on its own but a refresh token outlives this process.
            if (!string.IsNullOrEmpty(token.RefreshToken))
            {
                await TryRevokeAsync(token.RefreshToken!, "refresh_token", cancellationToken)
                    .ConfigureAwait(false);
            }

            if (!string.IsNullOrEmpty(token.AccessToken))
            {
                await TryRevokeAsync(token.AccessToken, "access_token", cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Revokes a token at Kinde (RFC 7009).
        /// </summary>
        /// <remarks>
        /// Deleting the local copy is not revocation: anything that already holds the token keeps
        /// working until it expires. This calls the <c>revocation_endpoint</c> the authorization
        /// server advertises, so a leaked or copied token can actually be withdrawn.
        /// </remarks>
        /// <param name="token">The access or refresh token to revoke.</param>
        /// <param name="tokenTypeHint">
        /// <c>access_token</c> or <c>refresh_token</c>. Optional, but it lets the server look in
        /// the right place first.
        /// </param>
        /// <param name="cancellationToken">Cancels the call.</param>
        /// <exception cref="InvalidOperationException">
        /// The authorization server advertises no revocation endpoint.
        /// </exception>
        /// <exception cref="KindeAgentTokenException">The server rejected the request.</exception>
        public async Task RevokeAsync(
            string token,
            string? tokenTypeHint = null,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("A token is required.", nameof(token));
            }

            var discovery = await GetDiscoveryDocumentAsync(cancellationToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(discovery.RevocationEndpoint))
            {
                throw new InvalidOperationException(
                    "The authorization server advertises no revocation_endpoint, so this token "
                    + "cannot be revoked server-side.");
            }

            RequireHttpsEndpoint(discovery.RevocationEndpoint, "revocation_endpoint", required: true);

            var form = new Dictionary<string, string>
            {
                ["token"] = token
            };

            if (!string.IsNullOrWhiteSpace(tokenTypeHint))
            {
                form["token_type_hint"] = tokenTypeHint!;
            }

            form["client_id"] = await ResolveClientIdAsync(cancellationToken).ConfigureAwait(false);

            var secret = await ResolveClientSecretAsync(cancellationToken).ConfigureAwait(false);
            if (!string.IsNullOrEmpty(secret))
            {
                form["client_secret"] = secret!;
            }

            using var request = new HttpRequestMessage(HttpMethod.Post, discovery.RevocationEndpoint)
            {
                Content = new FormUrlEncodedContent(form)
            };

            using var response = await _httpClient.SendAsync(request, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                var text = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new KindeAgentTokenException(response.StatusCode, text);
            }
        }

        /// <summary>
        /// Revokes without letting a failure break sign-out. RFC 7009 treats an unknown token as
        /// success, so the cases swallowed here are a missing endpoint or an unreachable server.
        /// </summary>
        private async Task TryRevokeAsync(
            string token, string tokenTypeHint, CancellationToken cancellationToken)
        {
            try
            {
                await RevokeAsync(token, tokenTypeHint, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception exception) when (
                exception is InvalidOperationException
                || exception is KindeAgentTokenException
                || exception is HttpRequestException
                || exception is TaskCanceledException)
            {
                // The local token is already gone, which is the part the caller depends on.
            }
        }

        private async Task<KindeAgentToken> RequestTokenAsync(
            Dictionary<string, string> form,
            CancellationToken cancellationToken,
            string? carryOverRefreshToken = null)
        {
            var result = await RequestTokenCoreAsync(form, cancellationToken, carryOverRefreshToken)
                .ConfigureAwait(false);

            return result.Token;
        }

        private async Task<TokenRequestResult> RequestTokenCoreAsync(
            Dictionary<string, string> form,
            CancellationToken cancellationToken,
            string? carryOverRefreshToken = null)
        {
            // Captured before the call, so a sign-out that lands while it is in flight is visible
            // as a change by the time there is a token to store.
            var generation = Volatile.Read(ref _signOutGeneration);

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

            // token_type was parsed and never checked. Everything downstream sends the value as a
            // bearer token, so a response of any other type - a sender-constrained DPoP token, say -
            // would be presented in a way the resource server is entitled to reject, and the
            // failure would surface as an unexplained 401 rather than as the mismatch it is.
            if (!string.IsNullOrEmpty(token.TokenType)
                && !string.Equals(token.TokenType, "Bearer", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    $"Kinde returned a '{token.TokenType}' token, but this client only knows how to "
                    + "present a Bearer token.");
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

            // Under the gate, so the generation check and the write cannot be split by a sign-out.
            await _tokenGate.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                if (Volatile.Read(ref _signOutGeneration) != generation)
                {
                    // Signed out while this was in flight. The session was ended deliberately, so
                    // the token is handed back to the caller that asked for it but is not
                    // persisted: writing it would restore a session the user just ended.
                    return new TokenRequestResult(token, Persisted: false);
                }

                await _store.SetAsync(TokenKey, JsonSerializer.Serialize(token), cancellationToken)
                    .ConfigureAwait(false);
            }
            finally
            {
                _tokenGate.Release();
            }

            return new TokenRequestResult(token, Persisted: true);
        }

        /// <summary>
        /// A token and whether it reached the store. Not persisted means a sign-out overtook the
        /// request.
        /// </summary>
        private readonly record struct TokenRequestResult(KindeAgentToken Token, bool Persisted);

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

        // Scoped like RegistrationKey, and for the same reason. The resource matters because a
        // token's audience is bound to it. The client identity matters because a token belongs to
        // the client it was issued to: sharing a key lets one instance return another client's
        // token, and when refreshing that token fails with invalid_grant the cleanup path would
        // delete it, ending the owning client's session.
        //
        // The session key matters because none of the above identifies the authorizing user. Two
        // users of one client, against one resource, produce the same key: the second exchange
        // overwrites the first user's entry and the first client then reads the second user's
        // access and refresh tokens. KindeAgentClientOptions.SessionKey separates them, and is
        // required of any host whose store is shared between users.
        private string TokenKey =>
            $"{_options.NormalizedDomain}|{_options.AgentApplicationId}|{ClientIdentity}"
            + $"|{_options.ResourceUrl}|{_options.SessionKey}|token";

        /// <summary>
        /// What identifies this client's own credentials: its configured id, or the redirect URI
        /// its dynamic registration is bound to.
        /// </summary>
        private string ClientIdentity =>
            string.IsNullOrWhiteSpace(_options.ClientId)
                ? _options.RedirectUri
                : _options.ClientId!;

        /// <summary>
        /// The most a metadata document may be. These documents are a few hundred bytes; the cap
        /// exists because <see cref="GetProtectedResourceMetadataAsync"/> fetches a URL taken from
        /// a resource server's challenge, which is not under this client's control. Reading such a
        /// response to completion would let a hostile or compromised resource exhaust the agent's
        /// memory with a single reply.
        /// </summary>
        private const int MaxDocumentBytes = 1024 * 1024;

        private async Task<T> GetJsonAsync<T>(string url, CancellationToken cancellationToken)
        {
            using var response = await _httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
            var text = await ReadCappedAsync(response, url, cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(
                    $"GET {url} failed ({(int)response.StatusCode}): {text}");
            }

            return Deserialize<T>(text)
                   ?? throw new InvalidOperationException($"GET {url} returned an empty document.");
        }

        /// <summary>
        /// Reads a response body, refusing anything over <see cref="MaxDocumentBytes"/>.
        /// </summary>
        /// <remarks>
        /// The declared Content-Length is checked first, and the read is capped regardless, because
        /// a chunked response declares no length at all.
        /// </remarks>
        private static async Task<string> ReadCappedAsync(
            HttpResponseMessage response, string url, CancellationToken cancellationToken)
        {
            if (response.Content.Headers.ContentLength > MaxDocumentBytes)
            {
                throw new InvalidOperationException(
                    $"GET {url} returned {response.Content.Headers.ContentLength} bytes, over the "
                    + $"{MaxDocumentBytes}-byte limit for a metadata document.");
            }

            using var stream = await response.Content.ReadAsStreamAsync(cancellationToken)
                .ConfigureAwait(false);

            var buffer = new byte[8192];
            using var collected = new MemoryStream();

            while (true)
            {
                var read = await stream.ReadAsync(buffer, cancellationToken).ConfigureAwait(false);
                if (read == 0)
                {
                    break;
                }

                if (collected.Length + read > MaxDocumentBytes)
                {
                    throw new InvalidOperationException(
                        $"GET {url} returned more than {MaxDocumentBytes} bytes, which is over the "
                        + "limit for a metadata document.");
                }

                collected.Write(buffer, 0, read);
            }

            return Encoding.UTF8.GetString(collected.ToArray());
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
