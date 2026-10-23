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
    }
}
