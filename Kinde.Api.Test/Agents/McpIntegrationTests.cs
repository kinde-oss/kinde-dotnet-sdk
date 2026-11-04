#nullable enable
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Kinde.Api.Agents;
using Kinde.Mcp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Kinde.Api.Test.Agents
{
    /// <summary>
    /// Drives a real MCP server wired to Kinde, so the discovery document and the challenge are the
    /// ones an MCP client would actually receive.
    /// </summary>
    public class McpIntegrationTests
    {
        private const string Domain = "https://example.kinde.com";
        private const string ClientId = "abc123";
        private const string ResourceUrl = "https://api.example.com/mcp";

        private static KindeAgentAuthOptions Options() => new KindeAgentAuthOptions
        {
            Domain = Domain,
            ClientId = ClientId,
            ClientSecret = "secret",
            ResourceUrl = ResourceUrl,
            ScopesSupported = new List<string> { "read:things" }
        };

        private static IHost CreateHost(IKindeTokenVerifier verifier)
        {
            return new HostBuilder()
                .ConfigureWebHost(web => web
                    .UseTestServer()
                    .ConfigureServices(services =>
                    {
                        services.AddRouting();
                        // The stub keeps these tests on the wiring, not the verifiers.
                        services.AddKindeMcpAuth(options =>
                        {
                            options.Kinde = Options();
                            options.Verifier = verifier;
                        });
                        services.AddAuthorization(authorization =>
                            authorization.AddPolicy("read-scope", policy => policy
                                .RequireAuthenticatedUser()
                                .RequireKindeScope("read:things")));
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();
                        app.UseAuthentication();
                        app.UseAuthorization();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapGet("/mcp", (Microsoft.AspNetCore.Http.HttpContext ctx) =>
                                    Microsoft.AspNetCore.Http.Results.Json(
                                        new { subject = ctx.GetKindeAuthContext()?.Subject }))
                                .RequireAuthorization();

                            endpoints.MapGet("/scoped",
                                    () => Microsoft.AspNetCore.Http.Results.Text("scoped"))
                                .RequireAuthorization("read-scope");
                        });
                    }))
                .Build();
        }

        private static KindeAuthContext Authenticated() => KindeAuthContext.Success(
            subject: "agent_01",
            actorSubject: null,
            clientId: "https://agent.example.com/.well-known/oauth-client",
            scopes: new[] { "offline", "read:things" },
            permissions: Array.Empty<string>(),
            audiences: new[] { ResourceUrl },
            organizationCode: null,
            expiresAt: DateTimeOffset.UtcNow.AddMinutes(10),
            claims: new Dictionary<string, object>());

        private static async Task<HttpResponseMessage> Get(
            IHost host, string path, string? authorization = null)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, path);
            if (authorization != null)
            {
                request.Headers.TryAddWithoutValidation("Authorization", authorization);
            }
            return await host.GetTestClient().SendAsync(request);
        }

        [Fact]
        public async Task McpServer_ServesProtectedResourceMetadata_BuiltFromKindeOptions()
        {
            using var host = CreateHost(new StubVerifier(Authenticated()));
            await host.StartAsync();

            var response = await Get(host, "/.well-known/oauth-protected-resource");
            var body = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using var document = JsonDocument.Parse(body);
            Assert.Equal(ResourceUrl, document.RootElement.GetProperty("resource").GetString());
            // The scoped issuer, which is what the client runs discovery against.
            Assert.Equal(
                $"{Domain}/oauth2/agents/{ClientId}",
                document.RootElement.GetProperty("authorization_servers")[0].GetString());
        }

        [Fact]
        public async Task ProtectedResourceMetadata_AdvertisesOfflineScope()
        {
            // Without "offline" an agent never receives a refresh token, whatever else is listed.
            using var host = CreateHost(new StubVerifier(Authenticated()));
            await host.StartAsync();

            var body = await (await Get(host, "/.well-known/oauth-protected-resource"))
                .Content.ReadAsStringAsync();

            Assert.Contains("offline", body);
            Assert.Contains("read:things", body);
        }

        [Fact]
        public async Task ProtectedResourceMetadata_DoesNotDuplicateEntries()
        {
            // The SDK's metadata type pre-populates these collections, so appending to them
            // advertises "header" twice and would do the same to the issuer list.
            using var host = CreateHost(new StubVerifier(Authenticated()));
            await host.StartAsync();

            var body = await (await Get(host, "/.well-known/oauth-protected-resource"))
                .Content.ReadAsStringAsync();

            using var document = JsonDocument.Parse(body);
            Assert.Single(document.RootElement.GetProperty("bearer_methods_supported").EnumerateArray());
            Assert.Single(document.RootElement.GetProperty("authorization_servers").EnumerateArray());
            Assert.Equal(
                $"{Domain}/.well-known/jwks",
                document.RootElement.GetProperty("jwks_uri").GetString());
        }

        [Fact]
        public async Task ProtectedResourceMetadata_IsServedWithoutAToken()
        {
            using var host = CreateHost(new StubVerifier(KindeAuthContext.Invalid("nope")));
            await host.StartAsync();

            var response = await Get(host, "/.well-known/oauth-protected-resource");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ProtectedEndpoint_Returns401_AndPointsAtTheMetadata()
        {
            using var host = CreateHost(new StubVerifier(Authenticated()));
            await host.StartAsync();

            var response = await Get(host, "/mcp");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            var challenge = Assert.Single(response.Headers.WwwAuthenticate).ToString();
            Assert.Contains("resource_metadata", challenge);
        }

        [Fact]
        public async Task ProtectedEndpoint_AcceptsAKindeVerifiedToken()
        {
            using var host = CreateHost(new StubVerifier(Authenticated()));
            await host.StartAsync();

            var response = await Get(host, "/mcp", "Bearer good-token");
            var body = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains("agent_01", body);
        }

        [Fact]
        public async Task AMissingScope_StillGetsInsufficientScope_ThroughTheMcpScheme()
        {
            // Forbids reaching the MCP handler would come back as a bare 403, so the client
            // would never learn which scope to re-authorize for and step-up would stall.
            // Authenticates, but without read:things.
            var withoutScope = KindeAuthContext.Success(
                subject: "agent_01",
                actorSubject: null,
                clientId: "client",
                scopes: new[] { "offline" },
                permissions: Array.Empty<string>(),
                audiences: new[] { ResourceUrl },
                organizationCode: null,
                expiresAt: DateTimeOffset.UtcNow.AddMinutes(10),
                claims: new Dictionary<string, object>());

            using var host = CreateHost(new StubVerifier(withoutScope));
            await host.StartAsync();

            var response = await Get(host, "/scoped", "Bearer good-token");

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);

            var challenge = Assert.Single(response.Headers.WwwAuthenticate).ToString();
            Assert.Contains("error=\"insufficient_scope\"", challenge);
            Assert.Contains("scope=\"read:things\"", challenge);
        }

        [Fact]
        public async Task ProtectedEndpoint_Returns503_WhenKindeIsUnavailable()
        {
            // The MCP scheme owns challenges, but an outage must not come back as a 401 with
            // resource_metadata: an MCP client would restart authorization instead of retrying.
            using var host = CreateHost(new StubVerifier(KindeAuthContext.Unavailable("timed out")));
            await host.StartAsync();

            var response = await Get(host, "/mcp", "Bearer any-token");

            Assert.Equal(HttpStatusCode.ServiceUnavailable, response.StatusCode);
            Assert.Empty(response.Headers.WwwAuthenticate);
        }

        [Fact]
        public async Task ProtectedEndpoint_RejectsATokenKindeDoesNotAccept()
        {
            using var host = CreateHost(new StubVerifier(KindeAuthContext.Invalid("expired")));
            await host.StartAsync();

            var response = await Get(host, "/mcp", "Bearer bad-token");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public void AddKindeMcpAuth_ValidatesTheOptions()
        {
            var services = new ServiceCollection();

            Assert.Throws<ArgumentException>(() => services.AddKindeMcpAuth(options =>
                options.Kinde = new KindeAgentAuthOptions { Domain = Domain, ClientId = ClientId }));
        }

        [Fact]
        public void AddKindeMcpAuth_Throws_WhenTheConfigurationSectionIsMissing()
        {
            var services = new ServiceCollection();
            var configuration = new Microsoft.Extensions.Configuration.ConfigurationBuilder().Build();

            var exception = Assert.Throws<InvalidOperationException>(
                () => services.AddKindeMcpAuth(configuration));

            Assert.Contains(KindeAgentAuthOptions.SectionName, exception.Message);
        }

        private sealed class StubVerifier : IKindeTokenVerifier
        {
            private readonly KindeAuthContext _result;

            public StubVerifier(KindeAuthContext result) => _result = result;

            public Task<KindeAuthContext> VerifyAsync(
                string accessToken, CancellationToken cancellationToken = default) =>
                Task.FromResult(_result);
        }

        // ===== Tool-level authorization =====

        /// <summary>
        /// A real MCP server with one guarded tool and one open tool, running stateless so a
        /// tools/call can be issued without the session handshake.
        /// </summary>
        private static IHost CreateMcpServerHost(
            IKindeTokenVerifier verifier, bool withAuthorizationFilters)
        {
            return new HostBuilder()
                .ConfigureWebHost(web => web
                    .UseTestServer()
                    .ConfigureServices(services =>
                    {
                        services.AddRouting();
                        services.AddKindeMcpAuth(options =>
                        {
                            options.Kinde = Options();
                            options.Verifier = verifier;
                        });
                        services.AddAuthorization(authorization =>
                            authorization.AddPolicy("read-scope", policy => policy
                                .RequireAuthenticatedUser()
                                .RequireKindeScope("read:things")));

                        var mcp = services
                            .AddMcpServer()
                            .WithHttpTransport(http => http.Stateless = true)
                            .WithTools<GuardedTools>();

                        if (withAuthorizationFilters)
                        {
                            mcp.AddKindeMcpAuthorizationFilters();
                        }
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();
                        app.UseAuthentication();
                        app.UseAuthorization();
                        app.UseEndpoints(endpoints =>
                            endpoints.MapMcp("/mcp")
                                .RequireAuthorization(KindeMcpAuthExtensions.McpEndpointPolicy));
                    }))
                .Build();
        }

        private static async Task<HttpResponseMessage> CallTool(
            IHost host, string tool, string? authorization)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, "/mcp")
            {
                Content = new StringContent(
                    $@"{{""jsonrpc"":""2.0"",""id"":1,""method"":""tools/call"",
                        ""params"":{{""name"":""{tool}"",""arguments"":{{}}}}}}",
                    System.Text.Encoding.UTF8,
                    "application/json")
            };

            request.Headers.TryAddWithoutValidation("Accept", "application/json, text/event-stream");
            if (authorization != null)
            {
                request.Headers.TryAddWithoutValidation("Authorization", authorization);
            }

            return await host.GetTestClient().SendAsync(request);
        }

        [Fact]
        public async Task AGuardedTool_IsDeniedToAnAgentWithoutTheScope()
        {
            // The agent is authenticated but holds only "offline", so the tool's policy must refuse
            // it. Authentication alone does not cover this: without the MCP SDK's authorization
            // filters the [Authorize] attribute on the tool is never consulted and the call runs.
            using var host = CreateMcpServerHost(
                new StubVerifier(AuthenticatedWithScopes("offline")),
                withAuthorizationFilters: true);
            await host.StartAsync();

            var response = await CallTool(host, "guarded_tool", "Bearer token");
            var body = await response.Content.ReadAsStringAsync();

            // The tool must not have run, however the refusal is reported.
            Assert.DoesNotContain("guarded-tool-ran", body);
        }

        [Fact]
        public async Task WithoutTheAuthorizationFilters_AGuardedToolCannotBeCalledAtAll()
        {
            // Omitting AddAuthorizationFilters does not leave a guarded tool unprotected: the MCP
            // SDK fails closed. A tool carrying [Authorize] cannot be invoked at all without the
            // filter pipeline, so the cost of forgetting the call is a tool that never works rather
            // than one an under-scoped agent can reach.
            //
            // Asserted here because it is the reason AddKindeMcpAuthorizationFilters is documented
            // as required, and because it stops the two tests above passing vacuously: were the
            // guard to change to fail open in a later SDK release, this test fails and says so.
            using var host = CreateMcpServerHost(
                new StubVerifier(AuthenticatedWithScopes("offline", "read:things")),
                withAuthorizationFilters: false);
            await host.StartAsync();

            // Scoped correctly, so the only thing that can refuse the call is the missing pipeline.
            var response = await CallTool(host, "guarded_tool", "Bearer token");
            var body = await response.Content.ReadAsStringAsync();

            Assert.DoesNotContain("guarded-tool-ran", body);
            Assert.Contains("\"isError\":true", body);
        }

        [Fact]
        public async Task AGuardedTool_IsAllowedToAnAgentWithTheScope()
        {
            using var host = CreateMcpServerHost(
                new StubVerifier(AuthenticatedWithScopes("offline", "read:things")),
                withAuthorizationFilters: true);
            await host.StartAsync();

            var response = await CallTool(host, "guarded_tool", "Bearer token");
            var body = await response.Content.ReadAsStringAsync();

            Assert.Contains("guarded-tool-ran", body);
        }

        [Fact]
        public async Task AnOpenTool_IsAllowedToAnyAuthenticatedAgent()
        {
            // The filters must not refuse a tool that declares no policy.
            using var host = CreateMcpServerHost(
                new StubVerifier(AuthenticatedWithScopes("offline")),
                withAuthorizationFilters: true);
            await host.StartAsync();

            var response = await CallTool(host, "open_tool", "Bearer token");
            var body = await response.Content.ReadAsStringAsync();

            Assert.Contains("open-tool-ran", body);
        }

        [Fact]
        public async Task TheMcpEndpoint_RejectsAnUnauthenticatedToolCall()
        {
            using var host = CreateMcpServerHost(
                new StubVerifier(KindeAuthContext.MissingToken()),
                withAuthorizationFilters: true);
            await host.StartAsync();

            var response = await CallTool(host, "open_tool", authorization: null);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task TheMcpEndpointPolicy_IgnoresTheApplicationsOwnDefaultScheme()
        {
            // An application that also signs users in with cookies sets its own DefaultScheme, so
            // AddKindeMcpAuth deliberately repoints nothing. A bare RequireAuthorization() on the
            // MCP endpoint would then resolve against cookies: a bearer-only MCP client gets a
            // cookie challenge, and a cookie-authenticated browser session reaches the endpoint
            // with no agent token. The named policy pins the endpoint to the MCP scheme instead.
            using var host = new HostBuilder()
                .ConfigureWebHost(web => web
                    .UseTestServer()
                    .ConfigureServices(services =>
                    {
                        services.AddRouting();
                        services.AddKindeMcpAuth(options =>
                        {
                            options.Kinde = Options();
                            options.Verifier = new StubVerifier(AuthenticatedWithScopes("offline"));
                        });

                        // The application's own choice, made after Kinde's defaults are filled in.
                        services.PostConfigure<Microsoft.AspNetCore.Authentication.AuthenticationOptions>(
                            authentication => authentication.DefaultScheme = "Cookies");
                        services.AddAuthentication()
                            .AddCookie("Cookies");
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();
                        app.UseAuthentication();
                        app.UseAuthorization();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapGet("/pinned",
                                    () => Microsoft.AspNetCore.Http.Results.Text("pinned"))
                                .RequireAuthorization(KindeMcpAuthExtensions.McpEndpointPolicy);
                        });
                    }))
                .Build();

            await host.StartAsync();

            // A Kinde bearer token is accepted, although the application's default is cookies.
            var accepted = await Get(host, "/pinned", "Bearer token");
            Assert.Equal(HttpStatusCode.OK, accepted.StatusCode);
            Assert.Equal("pinned", await accepted.Content.ReadAsStringAsync());

            // And with no bearer token the challenge is the MCP one, not a cookie redirect.
            var challenged = await Get(host, "/pinned");
            Assert.Equal(HttpStatusCode.Unauthorized, challenged.StatusCode);
        }

        private static KindeAuthContext AuthenticatedWithScopes(params string[] scopes) =>
            KindeAuthContext.Success(
                subject: "agent_01",
                actorSubject: null,
                clientId: "https://agent.example.com/.well-known/oauth-client",
                scopes: scopes,
                permissions: Array.Empty<string>(),
                audiences: new[] { ResourceUrl },
                organizationCode: null,
                expiresAt: DateTimeOffset.UtcNow.AddMinutes(10),
                claims: new Dictionary<string, object>());

        [ModelContextProtocol.Server.McpServerToolType]
        public sealed class GuardedTools
        {
            [ModelContextProtocol.Server.McpServerTool(Name = "guarded_tool")]
            [Microsoft.AspNetCore.Authorization.Authorize(Policy = "read-scope")]
            public static string Guarded() => "guarded-tool-ran";

            [ModelContextProtocol.Server.McpServerTool(Name = "open_tool")]
            public static string Open() => "open-tool-ran";
        }
    }
}
