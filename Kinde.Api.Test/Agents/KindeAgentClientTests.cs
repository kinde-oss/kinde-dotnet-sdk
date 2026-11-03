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

            // Scoped to the client as well as the resource, so a second client cannot read or
            // delete this one's token. The empty segment before "token" is the session key, which
            // separates two users of one client against one resource; these options leave it unset.
            var tokenKey =
                $"{Options().NormalizedDomain}|{AppId}|{RedirectUri}|{ResourceUrl}||token";
            var winner = @"{""access_token"":""at_winner"",""expires_in"":3600,
                            ""refresh_token"":""rt_rotated"",""obtained_at"":""" +
                         DateTimeOffset.UtcNow.ToString("o") + @"""}";

            // The expired token stays in the store, so the call genuinely enters the refresh path.
            // The winner lands only while that refresh is in flight, which is the race: our
            // refresh then fails with invalid_grant for a token the winner already rotated.
            handler.Map("/token", @"{""error"":""invalid_grant""}", HttpStatusCode.BadRequest);
            handler.OnRequest = fragment =>
            {
                if (fragment == "/token")
                {
                    store.SetAsync(tokenKey, winner).GetAwaiter().GetResult();
                }
            };

            var result = await client.GetCurrentTokenAsync();

            Assert.NotNull(result);
            Assert.Equal("at_winner", result!.AccessToken);

            // And the winner must survive: deleting it is what this guard exists to prevent.
            var stillStored = await store.GetAsync(tokenKey);
            Assert.NotNull(stillStored);
            Assert.Contains("at_winner", stillStored);
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
        public async Task TokensAreScopedToTheClient_NotJustTheResource()
        {
            // Two clients on the same domain, application and resource must not share a token
            // entry. Reading another client's token would eventually refresh it with the wrong
            // client_id, and the invalid_grant cleanup would end the owning client's session.
            var store = new InMemoryStore();
            var handler = new RouteHandler();
            handler.Map("/.well-known/oauth-authorization-server/", DiscoveryJson);
            handler.Map("/register", @"{""client_id"":""dyn_123""}");
            handler.Map("/token",
                @"{""access_token"":""at_for_first"",""expires_in"":3600,""refresh_token"":""rt_1""}");

            var first = new KindeAgentClient(Options(), new HttpClient(handler), store);
            var request = await first.CreateAuthorizationRequestAsync();
            await first.ExchangeCodeAsync("the-code", request.State, request);

            // Same domain, application and resource; a different client.
            var otherOptions = Options();
            otherOptions.ClientId = "https://other-agent.example.com/.well-known/oauth-client";
            var second = new KindeAgentClient(otherOptions, new HttpClient(handler), store);

            Assert.Equal("at_for_first", (await first.GetCurrentTokenAsync())!.AccessToken);
            Assert.Null(await second.GetCurrentTokenAsync());
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
                   ""authorization_endpoint"":""https://example.kinde.com/oauth2/agents/abc123/authorize"",""token_endpoint"":""https://example.kinde.com/oauth2/agents/abc123/token"",
                   ""code_challenge_methods_supported"":[""S256""]}");
            var client = new KindeAgentClient(
                Options(), new HttpClient(handler), new InMemoryStore());

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => client.GetOrRegisterAsync());

            Assert.Contains("registration_endpoint", exception.Message);
        }

        [Theory]
        [InlineData(@"""code_challenge_methods_supported"":null")]
        [InlineData(@"""code_challenge_methods_supported"":[]")]
        [InlineData(@"""code_challenge_methods_supported"":[""plain""]")]
        public async Task GetDiscoveryDocumentAsync_RefusesWithoutS256(string methods)
        {
            // MCP requires refusing unless S256 is advertised; an absent or null list is not a
            // licence to continue, and must not throw NullReferenceException either.
            var handler = new RouteHandler();
            handler.Map("/.well-known/oauth-authorization-server/",
                @"{""issuer"":""https://example.kinde.com/oauth2/agents/abc123"",
                   ""authorization_endpoint"":""https://example.kinde.com/oauth2/agents/abc123/authorize"",""token_endpoint"":""https://example.kinde.com/oauth2/agents/abc123/token""," + methods + "}");

            var client = new KindeAgentClient(
                Options(), new HttpClient(handler), new InMemoryStore());

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => client.GetDiscoveryDocumentAsync());

            Assert.Contains("S256", exception.Message);
        }

        [Fact]
        public async Task GetOrRegisterAsync_DeclaresNativeForALoopbackRedirect()
        {
            // OIDC defaults an omitted application_type to "web", which can reject the loopback
            // redirect a desktop agent needs.
            var (client, handler) = Create();
            handler.Map("/register", @"{""client_id"":""dyn_123""}");

            await client.GetOrRegisterAsync();

            Assert.Contains("\"application_type\":\"native\"", handler.BodyFor("/register"));
        }

        [Fact]
        public async Task ExchangeCodeAsync_RejectsACallbackFromAnotherIssuer()
        {
            // RFC 9207: without this a code from one authorization server could be redeemed at
            // another client's token endpoint.
            var (client, handler) = Create();
            handler.Map("/register", @"{""client_id"":""dyn_123""}");
            handler.Map("/token", @"{""access_token"":""at_1"",""expires_in"":3600}");

            var request = await client.CreateAuthorizationRequestAsync();

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => client.ExchangeCodeAsync(
                    "the-code", request.State, request,
                    returnedIssuer: "https://attacker.kinde.com/oauth2/agents/abc123"));

            Assert.Contains("authorization server", exception.Message);
            Assert.Equal(0, handler.CountFor("/token"));
        }

        [Fact]
        public void Constructor_ValidatesTheOptions()
        {
            var options = Options();
            options.RedirectUri = "";

            Assert.Throws<ArgumentException>(
                () => new KindeAgentClient(options, new HttpClient(), new InMemoryStore()));
        }

        private const string DiscoveryWithRevocationJson = @"{
            ""issuer"": ""https://example.kinde.com/oauth2/agents/abc123"",
            ""authorization_endpoint"": ""https://example.kinde.com/oauth2/agents/abc123/authorize"",
            ""token_endpoint"": ""https://example.kinde.com/oauth2/agents/abc123/token"",
            ""registration_endpoint"": ""https://example.kinde.com/oauth2/agents/abc123/register"",
            ""revocation_endpoint"": ""https://example.kinde.com/oauth2/agents/abc123/revoke"",
            ""code_challenge_methods_supported"": [""S256""]
        }";

        private const string DiscoveryWithIssJson = @"{
            ""issuer"": ""https://example.kinde.com/oauth2/agents/abc123"",
            ""authorization_endpoint"": ""https://example.kinde.com/oauth2/agents/abc123/authorize"",
            ""token_endpoint"": ""https://example.kinde.com/oauth2/agents/abc123/token"",
            ""registration_endpoint"": ""https://example.kinde.com/oauth2/agents/abc123/register"",
            ""code_challenge_methods_supported"": [""S256""],
            ""authorization_response_iss_parameter_supported"": true
        }";

        // ===== The token key must separate users of a shared store =====

        [Fact]
        public async Task TokenKey_SeparatesSessionsSharingOneStore()
        {
            // Two users authorizing the same client, for the same resource, against one store. With
            // no session in the key both exchanges write to the same entry, so the second
            // overwrites the first and the first client then reads the second user's tokens.
            var store = new InMemoryStore();

            var first = Options();
            first.SessionKey = "user-1";
            var second = Options();
            second.SessionKey = "user-2";

            var handler = new RouteHandler();
            handler.Map("/.well-known/oauth-authorization-server/", DiscoveryJson);
            handler.Map("/register", @"{""client_id"":""dyn_123""}");

            using var firstClient = new KindeAgentClient(first, new HttpClient(handler), store);
            using var secondClient = new KindeAgentClient(second, new HttpClient(handler), store);

            handler.Map("/token",
                @"{""access_token"":""at_user_1"",""expires_in"":3600,""refresh_token"":""rt_1""}");
            var firstRequest = await firstClient.CreateAuthorizationRequestAsync();
            await firstClient.ExchangeCodeAsync("code-1", firstRequest.State, firstRequest);

            handler.Map("/token",
                @"{""access_token"":""at_user_2"",""expires_in"":3600,""refresh_token"":""rt_2""}");
            var secondRequest = await secondClient.CreateAuthorizationRequestAsync();
            await secondClient.ExchangeCodeAsync("code-2", secondRequest.State, secondRequest);

            var firstToken = await firstClient.GetCurrentTokenAsync();
            var secondToken = await secondClient.GetCurrentTokenAsync();

            Assert.NotNull(firstToken);
            Assert.NotNull(secondToken);

            // Each sees its own, and neither has been handed the other user's credentials.
            Assert.Equal("at_user_1", firstToken!.AccessToken);
            Assert.Equal("at_user_2", secondToken!.AccessToken);
            Assert.Equal("rt_1", firstToken.RefreshToken);
            Assert.Equal("rt_2", secondToken.RefreshToken);
        }

        // ===== Sign-out must win against a refresh already in flight =====

        [Fact]
        public async Task SignOutAsync_DuringAnInFlightRefresh_LeavesNoSession()
        {
            var store = new InMemoryStore();
            var handler = new RouteHandler();
            handler.Map("/.well-known/oauth-authorization-server/", DiscoveryJson);
            handler.Map("/register", @"{""client_id"":""dyn_123""}");
            handler.Map("/token",
                @"{""access_token"":""at_1"",""expires_in"":1,""refresh_token"":""rt_1""}");

            using var client = new KindeAgentClient(Options(), new HttpClient(handler), store);
            var request = await client.CreateAuthorizationRequestAsync();
            await client.ExchangeCodeAsync("the-code", request.State, request);

            // The refresh succeeds at Kinde, but the user signs out while it is in flight. The
            // refreshed token must not be written: persisting it restores a session that was
            // deliberately ended, and GetCurrentTokenAsync would report an authenticated agent
            // after sign-out had completed.
            handler.Map("/token",
                @"{""access_token"":""at_refreshed"",""expires_in"":3600,""refresh_token"":""rt_2""}");

            var signedOut = false;
            handler.OnRequest = fragment =>
            {
                if (fragment == "/token" && !signedOut)
                {
                    signedOut = true;
                    client.SignOutAsync(revoke: false).GetAwaiter().GetResult();
                }
            };

            var result = await client.GetCurrentTokenAsync();

            Assert.Null(result);

            var tokenKey = $"{Options().NormalizedDomain}|{AppId}|{RedirectUri}|{ResourceUrl}||token";
            Assert.Null(await store.GetAsync(tokenKey));
        }

        // ===== RFC 9207: a missing iss is only acceptable when the server does not send one =====

        [Fact]
        public async Task ExchangeCodeAsync_RejectsAMissingIssWhenTheServerAdvertisesIt()
        {
            var handler = new RouteHandler();
            handler.Map("/.well-known/oauth-authorization-server/", DiscoveryWithIssJson);
            handler.Map("/register", @"{""client_id"":""dyn_123""}");
            handler.Map("/token",
                @"{""access_token"":""at_1"",""expires_in"":3600}");

            using var client = new KindeAgentClient(
                Options(), new HttpClient(handler), new InMemoryStore());
            var request = await client.CreateAuthorizationRequestAsync();

            // A stripped iss must not pass: the server promised to send one, so its absence is the
            // mix-up protection being removed rather than a server that does not support it.
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => client.ExchangeCodeAsync("the-code", request.State, request));

            Assert.Contains("authorization_response_iss_parameter_supported", exception.Message);

            // And the code was never redeemed.
            Assert.Equal(0, handler.CountFor("/token"));
        }

        [Fact]
        public async Task ExchangeCodeAsync_AcceptsTheAdvertisedIssWhenItMatches()
        {
            var handler = new RouteHandler();
            handler.Map("/.well-known/oauth-authorization-server/", DiscoveryWithIssJson);
            handler.Map("/register", @"{""client_id"":""dyn_123""}");
            handler.Map("/token", @"{""access_token"":""at_1"",""expires_in"":3600}");

            using var client = new KindeAgentClient(
                Options(), new HttpClient(handler), new InMemoryStore());
            var request = await client.CreateAuthorizationRequestAsync();

            var token = await client.ExchangeCodeAsync(
                "the-code",
                request.State,
                request,
                returnedIssuer: $"{Domain}/oauth2/agents/{AppId}");

            Assert.Equal("at_1", token.AccessToken);
        }

        [Fact]
        public async Task ExchangeCodeAsync_StillAllowsAMissingIssWhenTheServerDoesNotSendOne()
        {
            // The base discovery document does not advertise the parameter, so a callback without
            // iss is legitimate and must keep working.
            var handler = new RouteHandler();
            handler.Map("/.well-known/oauth-authorization-server/", DiscoveryJson);
            handler.Map("/register", @"{""client_id"":""dyn_123""}");
            handler.Map("/token", @"{""access_token"":""at_1"",""expires_in"":3600}");

            using var client = new KindeAgentClient(
                Options(), new HttpClient(handler), new InMemoryStore());
            var request = await client.CreateAuthorizationRequestAsync();

            var token = await client.ExchangeCodeAsync("the-code", request.State, request);

            Assert.Equal("at_1", token.AccessToken);
        }

        // ===== Revocation actually reaches the advertised endpoint =====

        [Fact]
        public async Task SignOutAsync_RevokesTheTokensAtKinde()
        {
            var handler = new RouteHandler();
            handler.Map("/.well-known/oauth-authorization-server/", DiscoveryWithRevocationJson);
            handler.Map("/register", @"{""client_id"":""dyn_123""}");
            handler.Map("/token",
                @"{""access_token"":""at_1"",""expires_in"":3600,""refresh_token"":""rt_1""}");
            handler.Map("/revoke", string.Empty);

            using var client = new KindeAgentClient(
                Options(), new HttpClient(handler), new InMemoryStore());
            var request = await client.CreateAuthorizationRequestAsync();
            await client.ExchangeCodeAsync("the-code", request.State, request);

            // The handler keeps only the most recent body per route, so each revocation payload is
            // captured as it is sent.
            var revocations = new List<string>();
            handler.OnRequest = fragment =>
            {
                if (fragment == "/revoke")
                {
                    revocations.Add(handler.BodyFor("/revoke"));
                }
            };

            await client.SignOutAsync();

            // Deleting the local copy is not revocation: a copied token keeps working until it
            // expires unless the server is told. Both tokens are withdrawn, refresh included,
            // because that is the one that outlives the process.
            Assert.Equal(2, handler.CountFor("/revoke"));
            Assert.Contains(revocations, body =>
                body.Contains("token=rt_1", StringComparison.Ordinal)
                && body.Contains("token_type_hint=refresh_token", StringComparison.Ordinal));
            Assert.Contains(revocations, body =>
                body.Contains("token=at_1", StringComparison.Ordinal)
                && body.Contains("token_type_hint=access_token", StringComparison.Ordinal));
        }

        [Fact]
        public async Task RevokeAsync_ReportsWhenTheServerAdvertisesNoEndpoint()
        {
            var handler = new RouteHandler();
            handler.Map("/.well-known/oauth-authorization-server/", DiscoveryJson);

            using var client = new KindeAgentClient(
                Options(), new HttpClient(handler), new InMemoryStore());

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => client.RevokeAsync("some-token", "access_token"));

            Assert.Contains("revocation_endpoint", exception.Message);
        }

        [Fact]
        public async Task SignOutAsync_StillClearsTheLocalTokenWhenRevocationFails()
        {
            var store = new InMemoryStore();
            var handler = new RouteHandler();
            handler.Map("/.well-known/oauth-authorization-server/", DiscoveryWithRevocationJson);
            handler.Map("/register", @"{""client_id"":""dyn_123""}");
            handler.Map("/token",
                @"{""access_token"":""at_1"",""expires_in"":3600,""refresh_token"":""rt_1""}");
            handler.Map("/revoke", @"{""error"":""server_error""}", HttpStatusCode.InternalServerError);

            using var client = new KindeAgentClient(Options(), new HttpClient(handler), store);
            var request = await client.CreateAuthorizationRequestAsync();
            await client.ExchangeCodeAsync("the-code", request.State, request);

            // Best-effort: a sign-out that leaves a usable token on disk because the network
            // blipped is worse than one that leaves a token live at Kinde until it expires.
            await client.SignOutAsync();

            Assert.Null(await client.GetCurrentTokenAsync());
        }

        // ===== Protected resource metadata discovery (RFC 9728) =====

        [Fact]
        public async Task GetProtectedResourceMetadataAsync_ReadsTheResourceDocument()
        {
            var handler = new RouteHandler();
            handler.Map("/.well-known/oauth-protected-resource", @"{
                ""resource"": ""https://api.example.com/mcp"",
                ""authorization_servers"": [""https://example.kinde.com/oauth2/agents/abc123""],
                ""bearer_methods_supported"": [""header""],
                ""scopes_supported"": [""offline""],
                ""resource_name"": ""Example MCP server""
            }");

            using var client = new KindeAgentClient(
                Options(), new HttpClient(handler), new InMemoryStore());

            var metadata = await client.GetProtectedResourceMetadataAsync(
                "https://api.example.com/.well-known/oauth-protected-resource");

            Assert.Equal(ResourceUrl, metadata.Resource);
            Assert.Equal("Example MCP server", metadata.ResourceName);
            Assert.Contains("https://example.kinde.com/oauth2/agents/abc123", metadata.AuthorizationServers);
        }

        [Fact]
        public async Task GetProtectedResourceMetadataAsync_RejectsADocumentForAnotherResource()
        {
            var handler = new RouteHandler();
            handler.Map("/.well-known/oauth-protected-resource", @"{
                ""resource"": ""https://somewhere-else.example.com/mcp"",
                ""authorization_servers"": [""https://example.kinde.com/oauth2/agents/abc123""]
            }");

            using var client = new KindeAgentClient(
                Options(), new HttpClient(handler), new InMemoryStore());

            // Authorizing for one resource while calling another is how a token ends up with the
            // wrong audience, so the mismatch is refused rather than followed.
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => client.GetProtectedResourceMetadataAsync(
                    "https://api.example.com/.well-known/oauth-protected-resource"));

            Assert.Contains("somewhere-else.example.com", exception.Message);
        }

        [Fact]
        public async Task DiscoverFromResourceMetadataAsync_RefusesAnUntrustedAuthorizationServer()
        {
            var handler = new RouteHandler();
            handler.Map("/.well-known/oauth-protected-resource", @"{
                ""resource"": ""https://api.example.com/mcp"",
                ""authorization_servers"": [""https://attacker.example.net/oauth2""]
            }");
            handler.Map("/.well-known/oauth-authorization-server/", DiscoveryJson);

            using var client = new KindeAgentClient(
                Options(), new HttpClient(handler), new InMemoryStore());

            // The resource names the servers it trusts, but following that list blindly would let a
            // hostile resource point the agent at a server of its choosing and harvest a code.
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => client.DiscoverFromResourceMetadataAsync(
                    "https://api.example.com/.well-known/oauth-protected-resource"));

            Assert.Contains("attacker.example.net", exception.Message);
        }

        [Fact]
        public async Task DiscoverFromResourceMetadataAsync_ReturnsTheServerMetadataWhenTrusted()
        {
            var handler = new RouteHandler();
            handler.Map("/.well-known/oauth-protected-resource", @"{
                ""resource"": ""https://api.example.com/mcp"",
                ""authorization_servers"": [""https://example.kinde.com/oauth2/agents/abc123""]
            }");
            handler.Map("/.well-known/oauth-authorization-server/", DiscoveryJson);

            using var client = new KindeAgentClient(
                Options(), new HttpClient(handler), new InMemoryStore());

            var discovery = await client.DiscoverFromResourceMetadataAsync(
                "https://api.example.com/.well-known/oauth-protected-resource");

            Assert.Equal($"{Domain}/oauth2/agents/{AppId}", discovery.Issuer);
            Assert.Equal($"{Domain}/oauth2/agents/{AppId}/token", discovery.TokenEndpoint);
        }

        [Fact]
        public async Task GetProtectedResourceMetadataAsync_RefusesAnInsecureMetadataUrl()
        {
            using var client = new KindeAgentClient(
                Options(), new HttpClient(new RouteHandler()), new InMemoryStore());

            // The URL arrives from a challenge, so it is attacker-influenced until proven otherwise.
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => client.GetProtectedResourceMetadataAsync(
                    "http://api.example.com/.well-known/oauth-protected-resource"));
        }

        // ===== Worst-case responses =====

        [Fact]
        public async Task AnOversizedMetadataDocument_IsRefusedRatherThanRead()
        {
            // The resource metadata URL comes from a resource server's challenge, so it is not this
            // client's to trust. Reading an unbounded reply to completion would let a hostile or
            // compromised resource exhaust the agent's memory with one response.
            var handler = new RouteHandler();
            handler.Map(
                "/.well-known/oauth-protected-resource",
                "{\"resource\":\"" + ResourceUrl + "\",\"padding\":\"" + new string('x', 2_000_000) + "\"}");

            using var client = new KindeAgentClient(
                Options(), new HttpClient(handler), new InMemoryStore());

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => client.GetProtectedResourceMetadataAsync(
                    "https://api.example.com/.well-known/oauth-protected-resource"));

            Assert.Contains("limit for a metadata document", exception.Message);
        }

        [Fact]
        public async Task ATokenOfAnUnsupportedType_IsRefused()
        {
            // Everything downstream presents the value as a bearer token, so accepting a
            // sender-constrained token here would produce an unexplained 401 at the resource
            // server rather than naming the mismatch.
            var handler = new RouteHandler();
            handler.Map("/.well-known/oauth-authorization-server/", DiscoveryJson);
            handler.Map("/register", @"{""client_id"":""dyn_123""}");
            handler.Map("/token",
                @"{""access_token"":""at_1"",""token_type"":""DPoP"",""expires_in"":3600}");

            using var client = new KindeAgentClient(
                Options(), new HttpClient(handler), new InMemoryStore());
            var request = await client.CreateAuthorizationRequestAsync();

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => client.ExchangeCodeAsync("the-code", request.State, request));

            Assert.Contains("DPoP", exception.Message);
        }

        [Fact]
        public async Task ABearerTokenIsAccepted_WhateverTheCasing()
        {
            // The check must not be so strict that it rejects a legitimate response: token_type is
            // case-insensitive, and an omitted one is treated as bearer.
            foreach (var tokenType in new[] { @"""token_type"":""Bearer"",", @"""token_type"":""bearer"",", "" })
            {
                var handler = new RouteHandler();
                handler.Map("/.well-known/oauth-authorization-server/", DiscoveryJson);
                handler.Map("/register", @"{""client_id"":""dyn_123""}");
                handler.Map("/token", "{" + tokenType + @"""access_token"":""at_1"",""expires_in"":3600}");

                using var client = new KindeAgentClient(
                    Options(), new HttpClient(handler), new InMemoryStore());
                var request = await client.CreateAuthorizationRequestAsync();

                var token = await client.ExchangeCodeAsync("the-code", request.State, request);

                Assert.Equal("at_1", token.AccessToken);
            }
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

            /// <summary>Runs as a matched request is served, for simulating a concurrent change.</summary>
            public Action<string>? OnRequest { get; set; }

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

                    OnRequest?.Invoke(route.Key);

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
