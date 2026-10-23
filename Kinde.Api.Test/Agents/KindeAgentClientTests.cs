#nullable enable
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Kinde.Api.Agents.Client;
using Xunit;

namespace Kinde.Api.Test.Agents
{
    public class KindeAgentClientTests
    {
        private const string Domain = "https://example.kinde.com";
        private const string AppId = "abc123";
        private const string ResourceUrl = "https://api.example.com/mcp";
        private const string RedirectUri = "http://localhost:5136/agent-callback";

        private const string DiscoveryJson = @"{
            ""issuer"": ""https://example.kinde.com/oauth2/agents/abc123"",
            ""authorization_endpoint"": ""https://example.kinde.com/oauth2/agents/abc123/authorize"",
            ""token_endpoint"": ""https://example.kinde.com/oauth2/agents/abc123/token"",
            ""registration_endpoint"": ""https://example.kinde.com/oauth2/agents/abc123/register"",
            ""jwks_uri"": ""https://example.kinde.com/.well-known/jwks"",
            ""code_challenge_methods_supported"": [""S256""],
            ""client_id_metadata_document_supported"": true,
            ""resource_indicators_supported"": true
        }";

        private static KindeAgentClientOptions Options() => new KindeAgentClientOptions
        {
            Domain = Domain,
            AgentApplicationId = AppId,
            RedirectUri = RedirectUri,
            ResourceUrl = ResourceUrl
        };

        private static (KindeAgentClient Client, RouteHandler Handler) Create(
            KindeAgentClientOptions? options = null)
        {
            var handler = new RouteHandler();
            handler.Map("/.well-known/oauth-authorization-server/", DiscoveryJson);

            var client = new KindeAgentClient(
                options ?? Options(), new HttpClient(handler), new InMemoryStore());

            return (client, handler);
        }

        [Fact]
        public async Task GetDiscoveryDocumentAsync_ReadsKindesEndpoints()
        {
            var (client, _) = Create();

            var discovery = await client.GetDiscoveryDocumentAsync();

            Assert.Equal("https://example.kinde.com/oauth2/agents/abc123/token", discovery.TokenEndpoint);
            Assert.Equal("https://example.kinde.com/oauth2/agents/abc123/register", discovery.RegistrationEndpoint);
            Assert.True(discovery.ClientIdMetadataDocumentSupported);
            Assert.True(discovery.ResourceIndicatorsSupported);
        }

        [Fact]
        public async Task GetDiscoveryDocumentAsync_IsFetchedOnce()
        {
            var (client, handler) = Create();

            await client.GetDiscoveryDocumentAsync();
            await client.GetDiscoveryDocumentAsync();

            Assert.Equal(1, handler.CountFor("/.well-known/oauth-authorization-server/"));
        }

        [Fact]
        public async Task GetOrRegisterAsync_RegistersDynamically_AndReusesTheResult()
        {
            var (client, handler) = Create();
            handler.Map("/register", @"{""client_id"":""dyn_123"",""client_secret"":""s3cret""}");

            var first = await client.GetOrRegisterAsync();
            var second = await client.GetOrRegisterAsync();

            Assert.Equal("dyn_123", first.ClientId);
            Assert.Equal("dyn_123", second.ClientId);
            // Registering twice would produce a second client id and a second consent prompt.
            Assert.Equal(1, handler.CountFor("/register"));
        }

        [Fact]
        public async Task GetOrRegisterAsync_SendsTheRedirectUriAndGrants()
        {
            var (client, handler) = Create();
            handler.Map("/register", @"{""client_id"":""dyn_123""}");

            await client.GetOrRegisterAsync();

            var body = handler.BodyFor("/register");
            Assert.Contains(RedirectUri, body);
            Assert.Contains("authorization_code", body);
            Assert.Contains("refresh_token", body);
        }

        [Fact]
        public async Task CreateAuthorizationRequestAsync_UsesPkceS256()
        {
            var (client, handler) = Create();
            handler.Map("/register", @"{""client_id"":""dyn_123""}");

            var request = await client.CreateAuthorizationRequestAsync();
            var query = HttpUtility.ParseQueryString(new Uri(request.AuthorizationUrl).Query);

            Assert.Equal("S256", query["code_challenge_method"]);
            Assert.Equal("code", query["response_type"]);

            // The challenge must be the SHA-256 of the verifier, base64url without padding.
            var expected = Convert.ToBase64String(
                    SHA256.HashData(Encoding.ASCII.GetBytes(request.CodeVerifier)))
                .TrimEnd('=').Replace('+', '-').Replace('/', '_');
            Assert.Equal(expected, query["code_challenge"]);
        }

        [Fact]
        public async Task CreateAuthorizationRequestAsync_AlwaysRequestsOfflineScope()
        {
            // Without "offline" Kinde issues no refresh token, so the agent must prompt the user
            // again as soon as the access token expires.
            var options = Options();
            options.Scopes = new List<string> { "read:things" };
            var (client, handler) = Create(options);
            handler.Map("/register", @"{""client_id"":""dyn_123""}");

            var request = await client.CreateAuthorizationRequestAsync();
            var scope = HttpUtility.ParseQueryString(new Uri(request.AuthorizationUrl).Query)["scope"];

            Assert.StartsWith("offline", scope);
            Assert.Contains("read:things", scope);
        }

        [Fact]
        public async Task CreateAuthorizationRequestAsync_SendsTheResourceIndicator()
        {
            // RFC 8707. Omit it and Kinde mints a token whose audience is not this resource, which
            // the resource server then rejects as if the token were forged.
            var (client, handler) = Create();
            handler.Map("/register", @"{""client_id"":""dyn_123""}");

            var request = await client.CreateAuthorizationRequestAsync();
            var query = HttpUtility.ParseQueryString(new Uri(request.AuthorizationUrl).Query);

            Assert.Equal(ResourceUrl, query["resource"]);
        }

        [Fact]
        public async Task CreateAuthorizationRequestAsync_ProducesAFreshStateAndVerifierEachTime()
        {
            var (client, handler) = Create();
            handler.Map("/register", @"{""client_id"":""dyn_123""}");

            var first = await client.CreateAuthorizationRequestAsync();
            var second = await client.CreateAuthorizationRequestAsync();

            Assert.NotEqual(first.State, second.State);
            Assert.NotEqual(first.CodeVerifier, second.CodeVerifier);
            Assert.True(first.CodeVerifier.Length >= 43, "RFC 7636 requires at least 43 characters.");
        }

        [Fact]
        public async Task CreateAuthorizationRequestAsync_UsesAConfiguredClientId_WithoutRegistering()
        {
            // Covers both a pre-registered client and CIMD, where the id is a URL.
            var options = Options();
            options.ClientId = "https://agent.example.com/.well-known/oauth-client";
            var (client, handler) = Create(options);

            var request = await client.CreateAuthorizationRequestAsync();
            var query = HttpUtility.ParseQueryString(new Uri(request.AuthorizationUrl).Query);

            Assert.Equal(options.ClientId, query["client_id"]);
            Assert.Equal(0, handler.CountFor("/register"));
        }

        [Fact]
        public async Task ExchangeCodeAsync_ReturnsAndStoresTheToken()
        {
            var (client, handler) = Create();
            handler.Map("/register", @"{""client_id"":""dyn_123"",""client_secret"":""s3cret""}");
            handler.Map("/token",
                @"{""access_token"":""at_1"",""token_type"":""Bearer"",""expires_in"":3600,
                   ""refresh_token"":""rt_1"",""scope"":""offline""}");

            var request = await client.CreateAuthorizationRequestAsync();
            var token = await client.ExchangeCodeAsync("the-code", request.State, request);

            Assert.Equal("at_1", token.AccessToken);
            Assert.Equal("rt_1", token.RefreshToken);
            Assert.False(token.IsExpired());

            var current = await client.GetCurrentTokenAsync();
            Assert.Equal("at_1", current!.AccessToken);
        }

        [Fact]
        public async Task ExchangeCodeAsync_SendsTheVerifierAndResource()
        {
            var (client, handler) = Create();
            handler.Map("/register", @"{""client_id"":""dyn_123""}");
            handler.Map("/token", @"{""access_token"":""at_1"",""expires_in"":3600}");

            var request = await client.CreateAuthorizationRequestAsync();
            await client.ExchangeCodeAsync("the-code", request.State, request);

            var body = handler.BodyFor("/token");
            Assert.Contains("grant_type=authorization_code", body);
            Assert.Contains("code=the-code", body);
            Assert.Contains("code_verifier=" + request.CodeVerifier, body);
            Assert.Contains("resource=", body);
        }

        [Fact]
        public async Task ExchangeCodeAsync_RejectsAMismatchedState()
        {
            // The CSRF check: a forged callback carries a state the client never issued.
            var (client, handler) = Create();
            handler.Map("/register", @"{""client_id"":""dyn_123""}");
            handler.Map("/token", @"{""access_token"":""at_1"",""expires_in"":3600}");

            var request = await client.CreateAuthorizationRequestAsync();

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => client.ExchangeCodeAsync("the-code", "not-the-state", request));

            Assert.Contains("state", exception.Message, StringComparison.OrdinalIgnoreCase);
            Assert.Equal(0, handler.CountFor("/token"));
        }

        [Fact]
        public async Task ExchangeCodeAsync_SurfacesKindesError()
        {
            var (client, handler) = Create();
            handler.Map("/register", @"{""client_id"":""dyn_123""}");
            handler.Map("/token", @"{""error"":""invalid_grant""}", HttpStatusCode.BadRequest);

            var request = await client.CreateAuthorizationRequestAsync();

            var exception = await Assert.ThrowsAsync<KindeAgentTokenException>(
                () => client.ExchangeCodeAsync("used-code", request.State, request));

            Assert.Contains("invalid_grant", exception.Message);
            Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
            // A rejected grant, as opposed to an outage: only this kind discards a stored token.
            Assert.True(exception.IsGrantRejected);
        }

        [Fact]
        public async Task GetCurrentTokenAsync_RefreshesAnExpiredToken()
        {
            var (client, handler) = Create();
            handler.Map("/register", @"{""client_id"":""dyn_123""}");
            handler.Map("/token",
                @"{""access_token"":""at_1"",""expires_in"":1,""refresh_token"":""rt_1""}");

            var request = await client.CreateAuthorizationRequestAsync();
            await client.ExchangeCodeAsync("the-code", request.State, request);

            handler.Map("/token",
                @"{""access_token"":""at_2"",""expires_in"":3600,""refresh_token"":""rt_2""}");

            var refreshed = await client.GetCurrentTokenAsync();

            Assert.Equal("at_2", refreshed!.AccessToken);
            Assert.Contains("grant_type=refresh_token", handler.BodyFor("/token"));
        }

        [Fact]
        public async Task GetCurrentTokenAsync_ReturnsNull_WhenNothingIsStored()
        {
            var (client, _) = Create();

            Assert.Null(await client.GetCurrentTokenAsync());
        }

        [Fact]
        public async Task RefreshAsync_KeepsTheExistingRefreshToken_WhenTheResponseOmitsIt()
        {
            // RFC 6749 section 6 lets the server omit refresh_token on refresh, and the old one
            // stays valid. Overwriting it with null costs the user a consent at the next expiry.
            var (client, handler) = Create();
            handler.Map("/register", @"{""client_id"":""dyn_123""}");
            handler.Map("/token",
                @"{""access_token"":""at_1"",""expires_in"":1,""refresh_token"":""rt_1""}");

            var request = await client.CreateAuthorizationRequestAsync();
            await client.ExchangeCodeAsync("the-code", request.State, request);

            handler.Map("/token", @"{""access_token"":""at_2"",""expires_in"":3600}");
            var refreshed = await client.RefreshAsync("rt_1");

            Assert.Equal("at_2", refreshed.AccessToken);
            Assert.Equal("rt_1", refreshed.RefreshToken);
        }

        [Fact]
        public async Task GetCurrentTokenAsync_KeepsTheToken_WhenRefreshHitsATransientFailure()
        {
            // A 503 is not a revoked grant. Discarding the refresh token here would make a brief
            // Kinde outage cost the user another consent.
            var (client, handler) = Create();
            handler.Map("/register", @"{""client_id"":""dyn_123""}");
            handler.Map("/token",
                @"{""access_token"":""at_1"",""expires_in"":1,""refresh_token"":""rt_1""}");

            var request = await client.CreateAuthorizationRequestAsync();
            await client.ExchangeCodeAsync("the-code", request.State, request);

            handler.Map("/token", "upstream unavailable", HttpStatusCode.ServiceUnavailable);

            await Assert.ThrowsAsync<KindeAgentTokenException>(() => client.GetCurrentTokenAsync());

            // Once Kinde recovers the stored refresh token still works.
            handler.Map("/token",
                @"{""access_token"":""at_3"",""expires_in"":3600,""refresh_token"":""rt_3""}");
            var recovered = await client.GetCurrentTokenAsync();

            Assert.Equal("at_3", recovered!.AccessToken);
        }

        [Fact]
        public async Task GetCurrentTokenAsync_KeepsTheToken_WhenRefreshFailsForAConfigurationError()
        {
            // invalid_client shares the 400 with invalid_grant but means our credentials are wrong,
            // not that the grant is revoked. Discarding the refresh token here would cost a consent.
            var (client, handler) = Create();
            handler.Map("/register", @"{""client_id"":""dyn_123""}");
            handler.Map("/token",
                @"{""access_token"":""at_1"",""expires_in"":1,""refresh_token"":""rt_1""}");

            var request = await client.CreateAuthorizationRequestAsync();
            await client.ExchangeCodeAsync("the-code", request.State, request);

            handler.Map("/token", @"{""error"":""invalid_client""}", HttpStatusCode.BadRequest);
            await Assert.ThrowsAsync<KindeAgentTokenException>(() => client.GetCurrentTokenAsync());

            handler.Map("/token",
                @"{""access_token"":""at_2"",""expires_in"":3600,""refresh_token"":""rt_2""}");
            var recovered = await client.GetCurrentTokenAsync();

            Assert.Equal("at_2", recovered!.AccessToken);
        }

        [Theory]
        [InlineData(@"{""error"":""invalid_grant""}", true)]
        [InlineData(@"{""error"":""invalid_client""}", false)]
        [InlineData(@"{""error"":""unauthorized_client""}", false)]
        [InlineData("<html>gateway blocked</html>", false)]
        [InlineData("", false)]
        public void IsGrantRejected_ReadsTheOAuthErrorNotTheStatus(string body, bool expected)
        {
            var exception = new KindeAgentTokenException(HttpStatusCode.BadRequest, body);

            Assert.Equal(expected, exception.IsGrantRejected);
        }

        [Fact]
        public async Task GetCurrentTokenAsync_ReturnsTheTokenAConcurrentRefreshStored()
        {
            // Two callers refresh the same expired token. With rotating refresh tokens the second
            // gets invalid_grant for a token the first already replaced; deleting then would throw
            // away the good token the first caller just stored.
            var store = new InMemoryStore();
            var handler = new RouteHandler();
            handler.Map("/.well-known/oauth-authorization-server/", DiscoveryJson);
            handler.Map("/register", @"{""client_id"":""dyn_123""}");
            handler.Map("/token",
                @"{""access_token"":""at_1"",""expires_in"":1,""refresh_token"":""rt_1""}");

            var client = new KindeAgentClient(Options(), new HttpClient(handler), store);
            var request = await client.CreateAuthorizationRequestAsync();
            await client.ExchangeCodeAsync("the-code", request.State, request);

            // Stand in for the winner: a fresh token with a rotated refresh token is already stored.
            var winner = @"{""access_token"":""at_winner"",""expires_in"":3600,
                            ""refresh_token"":""rt_rotated"",""obtained_at"":""" +
                         DateTimeOffset.UtcNow.ToString("o") + @"""}";
            await store.SetAsync(
                $"{Options().NormalizedDomain}|{AppId}|{ResourceUrl}|token", winner);

            handler.Map("/token", @"{""error"":""invalid_grant""}", HttpStatusCode.BadRequest);
            var result = await client.GetCurrentTokenAsync();

            Assert.NotNull(result);
            Assert.Equal("at_winner", result!.AccessToken);
        }

        [Fact]
        public async Task TokensAreScopedToTheResource()
        {
            // A token's audience is bound to its resource, so a client configured for resource B
            // must not reuse a token stored by a client for resource A.
            var store = new InMemoryStore();
            var handler = new RouteHandler();
            handler.Map("/.well-known/oauth-authorization-server/", DiscoveryJson);
            handler.Map("/register", @"{""client_id"":""dyn_123""}");
            handler.Map("/token",
                @"{""access_token"":""at_for_a"",""expires_in"":3600,""refresh_token"":""rt_a""}");

            var forA = new KindeAgentClient(Options(), new HttpClient(handler), store);
            var request = await forA.CreateAuthorizationRequestAsync();
            await forA.ExchangeCodeAsync("the-code", request.State, request);

            var optionsB = Options();
            optionsB.ResourceUrl = "https://api.example.com/other";
            var forB = new KindeAgentClient(optionsB, new HttpClient(handler), store);

            Assert.Equal("at_for_a", (await forA.GetCurrentTokenAsync())!.AccessToken);
            Assert.Null(await forB.GetCurrentTokenAsync());
        }

        [Fact]
        public async Task GetOrRegisterAsync_RegistersAgain_WhenTheStoredSecretHasExpired()
        {
            var (client, handler) = Create();
            var expired = DateTimeOffset.UtcNow.AddHours(-1).ToUnixTimeSeconds();
            handler.Map("/register",
                $@"{{""client_id"":""dyn_123"",""client_secret"":""old"",
                     ""client_secret_expires_at"":{expired}}}");

            await client.GetOrRegisterAsync();

            handler.Map("/register", @"{""client_id"":""dyn_456"",""client_secret"":""new""}");
            var second = await client.GetOrRegisterAsync();

            // Reusing the lapsed secret would fail every token request with invalid_client.
            Assert.Equal("dyn_456", second.ClientId);
            Assert.Equal(2, handler.CountFor("/register"));
        }

        [Fact]
        public async Task GetOrRegisterAsync_DoesNotReuseARegistrationFromAnotherRedirectUri()
        {
            var store = new InMemoryStore();
            var handler = new RouteHandler();
            handler.Map("/.well-known/oauth-authorization-server/", DiscoveryJson);
            handler.Map("/register", @"{""client_id"":""dyn_123""}");

            var first = Options();
            await new KindeAgentClient(first, new HttpClient(handler), store).GetOrRegisterAsync();

            var moved = Options();
            moved.RedirectUri = "http://localhost:9999/agent-callback";
            await new KindeAgentClient(moved, new HttpClient(handler), store).GetOrRegisterAsync();

            // A registration is bound to the redirect URI it declared.
            Assert.Equal(2, handler.CountFor("/register"));
        }

        [Fact]
        public async Task GetCurrentTokenAsync_ReturnsNull_WhenTheRefreshTokenIsRejected()
        {
            // A revoked refresh token ends the session; the agent must authorize again rather
            // than surface an exception on every call.
            var (client, handler) = Create();
            handler.Map("/register", @"{""client_id"":""dyn_123""}");
            handler.Map("/token",
                @"{""access_token"":""at_1"",""expires_in"":1,""refresh_token"":""rt_1""}");

            var request = await client.CreateAuthorizationRequestAsync();
            await client.ExchangeCodeAsync("the-code", request.State, request);

            handler.Map("/token", @"{""error"":""invalid_grant""}", HttpStatusCode.BadRequest);

            Assert.Null(await client.GetCurrentTokenAsync());
        }

        [Fact]
        public async Task SignOutAsync_ForgetsTheToken_ButKeepsTheRegistration()
        {
            var (client, handler) = Create();
            handler.Map("/register", @"{""client_id"":""dyn_123""}");
            handler.Map("/token", @"{""access_token"":""at_1"",""expires_in"":3600}");

            var request = await client.CreateAuthorizationRequestAsync();
            await client.ExchangeCodeAsync("the-code", request.State, request);

            await client.SignOutAsync();

            Assert.Null(await client.GetCurrentTokenAsync());
            // Still one registration call: signing out must not force the user to consent again.
            await client.GetOrRegisterAsync();
            Assert.Equal(1, handler.CountFor("/register"));
        }

        [Fact]
        public async Task GetOrRegisterAsync_Explains_WhenRegistrationIsNotAvailable()
        {
            var handler = new RouteHandler();
            handler.Map("/.well-known/oauth-authorization-server/",
                @"{""issuer"":""https://example.kinde.com/oauth2/agents/abc123"",
                   ""authorization_endpoint"":""a"",""token_endpoint"":""t""}");
            var client = new KindeAgentClient(
                Options(), new HttpClient(handler), new InMemoryStore());

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => client.GetOrRegisterAsync());

            Assert.Contains("registration_endpoint", exception.Message);
        }

        [Fact]
        public void Constructor_ValidatesTheOptions()
        {
            var options = Options();
            options.RedirectUri = "";

            Assert.Throws<ArgumentException>(
                () => new KindeAgentClient(options, new HttpClient(), new InMemoryStore()));
        }

        private sealed class InMemoryStore : IKindeAgentStore
        {
            private readonly Dictionary<string, string> _values = new();

            public Task<string?> GetAsync(string key, CancellationToken cancellationToken = default) =>
                Task.FromResult(_values.TryGetValue(key, out var value) ? value : null);

            public Task SetAsync(string key, string value, CancellationToken cancellationToken = default)
            {
                _values[key] = value;
                return Task.CompletedTask;
            }

            public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
            {
                _values.Remove(key);
                return Task.CompletedTask;
            }
        }

        /// <summary>Returns a canned response per URL fragment, and records what was sent.</summary>
        private sealed class RouteHandler : HttpMessageHandler
        {
            private readonly Dictionary<string, (string Body, HttpStatusCode Status)> _routes = new();
            private readonly Dictionary<string, int> _counts = new();
            private readonly Dictionary<string, string> _bodies = new();

            public void Map(string fragment, string body, HttpStatusCode status = HttpStatusCode.OK) =>
                _routes[fragment] = (body, status);

            public int CountFor(string fragment) =>
                _counts.TryGetValue(fragment, out var count) ? count : 0;

            public string BodyFor(string fragment) =>
                _bodies.TryGetValue(fragment, out var body) ? body : string.Empty;

            protected override async Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var url = request.RequestUri!.ToString();

                foreach (var route in _routes)
                {
                    if (!url.Contains(route.Key, StringComparison.Ordinal))
                    {
                        continue;
                    }

                    _counts[route.Key] = CountFor(route.Key) + 1;
                    if (request.Content != null)
                    {
                        _bodies[route.Key] = await request.Content.ReadAsStringAsync();
                    }

                    return new HttpResponseMessage(route.Value.Status)
                    {
                        Content = new StringContent(
                            route.Value.Body, Encoding.UTF8, "application/json")
                    };
                }

                return new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"no route for {url}")
                };
            }
        }
    }
}
