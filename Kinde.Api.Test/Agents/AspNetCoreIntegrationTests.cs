#nullable enable
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Kinde.Api.Agents;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Kinde.Api.Test.Agents
{
    /// <summary>
    /// Drives real HTTP requests through a real ASP.NET Core pipeline, so the status codes and
    /// headers asserted here are the ones a client would actually receive.
    /// </summary>
    public class AspNetCoreIntegrationTests
    {
        private const string Domain = "https://example.kinde.com";
        private const string ClientId = "abc123";
        private const string ResourceUrl = "https://api.example.com/mcp";
        private const string MetadataUrl =
            "https://api.example.com/.well-known/oauth-protected-resource/mcp";

        private static KindeAgentAuthOptions Options() => new KindeAgentAuthOptions
        {
            Domain = Domain,
            ClientId = ClientId,
            ClientSecret = "secret",
            ResourceUrl = ResourceUrl
        };

        /// <summary>
        /// Builds a host whose verification is stubbed, so the tests exercise the ASP.NET layer
        /// rather than re-testing the verifiers.
        /// </summary>
        private static IHost CreateHost(IKindeTokenVerifier verifier)
        {
            return new HostBuilder()
                .ConfigureWebHost(web => web
                    .UseTestServer()
                    .ConfigureServices(services =>
                    {
                        services.AddRouting();
                        services.AddAuthentication(KindeAgentAuthenticationOptions.DefaultScheme)
                            .AddKindeAgentAuth(options =>
                            {
                                options.Kinde = Options();
                                options.IncludeErrorDetails = true;
                                // Supplying the verifier keeps these tests on the ASP.NET layer
                                // rather than re-testing the verifiers themselves.
                                options.Verifier = verifier;
                            });

                        services.AddAuthorization(authorization =>
                        {
                            authorization.AddPolicy("read-scope", policy => policy
                                .RequireAuthenticatedUser()
                                .RequireKindeScope("read:things"));
                            authorization.AddPolicy("write-permission", policy => policy
                                .RequireAuthenticatedUser()
                                .RequireKindePermission("write:things"));
                            authorization.AddPolicy("any-permission", policy => policy
                                .RequireAuthenticatedUser()
                                .RequireAnyKindePermission("delete:things", "read:things"));
                        });
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();
                        app.UseAuthentication();
                        app.UseAuthorization();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapKindeProtectedResourceMetadata();

                            endpoints.MapGet("/mcp", (HttpContext context) =>
                            {
                                var auth = context.GetKindeAuthContext();
                                return Results.Json(new
                                {
                                    subject = auth?.Subject,
                                    clientId = auth?.ClientId,
                                    name = context.User.Identity?.Name
                                });
                            }).RequireAuthorization();

                            endpoints.MapGet("/open", () => Results.Text("open"));

                            endpoints.MapGet("/scoped", () => Results.Text("scoped"))
                                .RequireAuthorization("read-scope");

                            endpoints.MapGet("/permitted", () => Results.Text("permitted"))
                                .RequireAuthorization("write-permission");

                            endpoints.MapGet("/any-permitted", () => Results.Text("any"))
                                .RequireAuthorization("any-permission");
                        });
                    }))
                .Build();
        }

        private static KindeAuthContext Authenticated(
            IReadOnlyList<string>? scopes = null, IReadOnlyList<string>? permissions = null) =>
            KindeAuthContext.Success(
                subject: "agent_01",
                actorSubject: "agent_delegate",
                clientId: "https://agent.example.com/.well-known/oauth-client",
                scopes: scopes ?? new[] { "offline" },
                permissions: permissions ?? Array.Empty<string>(),
                audiences: new[] { ResourceUrl },
                organizationCode: "org_1234",
                expiresAt: DateTimeOffset.UtcNow.AddMinutes(10),
                claims: new Dictionary<string, object>());

        private static async Task<HttpResponseMessage> Get(
            IHost host, string path, string? authorization = null)
        {
            var client = host.GetTestClient();
            using var request = new HttpRequestMessage(HttpMethod.Get, path);
            if (authorization != null)
            {
                request.Headers.TryAddWithoutValidation("Authorization", authorization);
            }
            return await client.SendAsync(request);
        }

        [Fact]
        public async Task ProtectedResourceMetadata_IsServedAtTheDerivedPath_WithoutAToken()
        {
            // Requiring a token here would deadlock discovery.
            using var host = CreateHost(new StubVerifier(Authenticated()));
            await host.StartAsync();

            var response = await Get(host, "/.well-known/oauth-protected-resource/mcp");
            var body = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using var document = JsonDocument.Parse(body);
            Assert.Equal(ResourceUrl, document.RootElement.GetProperty("resource").GetString());
            Assert.Equal(
                $"{Domain}/oauth2/agents/{ClientId}",
                document.RootElement.GetProperty("authorization_servers")[0].GetString());
            Assert.Equal(
                $"{Domain}/.well-known/jwks",
                document.RootElement.GetProperty("jwks_uri").GetString());
            Assert.Equal(
                "header",
                document.RootElement.GetProperty("bearer_methods_supported")[0].GetString());
        }

        [Fact]
        public async Task ProtectedResourceMetadata_AdvertisesOfflineScope()
        {
            using var host = CreateHost(new StubVerifier(Authenticated()));
            await host.StartAsync();

            var body = await (await Get(host, "/.well-known/oauth-protected-resource/mcp"))
                .Content.ReadAsStringAsync();

            Assert.Contains("offline", body);
        }

        [Fact]
        public async Task AnonymousEndpoint_StillWorks_WithNoToken()
        {
            // NoResult on a missing credential, so registering the scheme breaks nothing.
            using var host = CreateHost(new StubVerifier(Authenticated()));
            await host.StartAsync();

            var response = await Get(host, "/open");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("open", await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task ProtectedEndpoint_Returns401AndPointsAtTheMetadata_WithNoToken()
        {
            using var host = CreateHost(new StubVerifier(Authenticated()));
            await host.StartAsync();

            var response = await Get(host, "/mcp");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

            var challenge = Assert.Single(response.Headers.WwwAuthenticate).ToString();
            Assert.Contains($"resource_metadata=\"{MetadataUrl}\"", challenge);
            // No error code when nothing was presented: there is no error to report yet.
            Assert.DoesNotContain("error=", challenge);
        }

        [Fact]
        public async Task ProtectedEndpoint_Returns401WithInvalidToken_WhenVerificationFails()
        {
            using var host = CreateHost(new StubVerifier(KindeAuthContext.Invalid("expired")));
            await host.StartAsync();

            var response = await Get(host, "/mcp", "Bearer bad-token");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

            var challenge = Assert.Single(response.Headers.WwwAuthenticate).ToString();
            Assert.Contains("error=\"invalid_token\"", challenge);
            Assert.Contains($"resource_metadata=\"{MetadataUrl}\"", challenge);
        }

        [Fact]
        public async Task ProtectedEndpoint_Returns503WithNoChallenge_WhenKindeIsUnavailable()
        {
            // An outage must not look like an auth failure, or the agent re-authorizes instead
            // of retrying.
            using var host = CreateHost(new StubVerifier(KindeAuthContext.Unavailable("timed out")));
            await host.StartAsync();

            var response = await Get(host, "/mcp", "Bearer any-token");

            Assert.Equal(HttpStatusCode.ServiceUnavailable, response.StatusCode);
            Assert.Empty(response.Headers.WwwAuthenticate);
        }

        [Fact]
        public async Task ProtectedEndpoint_SucceedsAndExposesTheAuthContext()
        {
            using var host = CreateHost(new StubVerifier(Authenticated()));
            await host.StartAsync();

            var response = await Get(host, "/mcp", "Bearer good-token");
            var body = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using var document = JsonDocument.Parse(body);
            Assert.Equal("agent_01", document.RootElement.GetProperty("subject").GetString());
            // A CIMD client's id is a URL, and must survive as one.
            Assert.Equal(
                "https://agent.example.com/.well-known/oauth-client",
                document.RootElement.GetProperty("clientId").GetString());
            // The identity's name comes from sub, not from a legacy name claim.
            Assert.Equal("agent_01", document.RootElement.GetProperty("name").GetString());
        }

        [Fact]
        public async Task BearerPrefix_IsMatchedCaseInsensitively()
        {
            using var host = CreateHost(new StubVerifier(Authenticated()));
            await host.StartAsync();

            Assert.Equal(HttpStatusCode.OK, (await Get(host, "/mcp", "bearer good")).StatusCode);
            Assert.Equal(HttpStatusCode.OK, (await Get(host, "/mcp", "BEARER good")).StatusCode);
        }

        [Fact]
        public async Task NonBearerCredential_IsTreatedAsNoCredential()
        {
            using var host = CreateHost(new StubVerifier(Authenticated()));
            await host.StartAsync();

            var response = await Get(host, "/mcp", "Basic dXNlcjpwYXNz");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task ScopePolicy_AllowsATokenCarryingTheScope()
        {
            using var host = CreateHost(
                new StubVerifier(Authenticated(scopes: new[] { "offline", "read:things" })));
            await host.StartAsync();

            var response = await Get(host, "/scoped", "Bearer good");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ScopePolicy_Returns403_WhenTheScopeIsMissing()
        {
            using var host = CreateHost(new StubVerifier(Authenticated(scopes: new[] { "offline" })));
            await host.StartAsync();

            var response = await Get(host, "/scoped", "Bearer good");

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task PermissionPolicy_AllowsATokenCarryingThePermission()
        {
            using var host = CreateHost(
                new StubVerifier(Authenticated(permissions: new[] { "write:things" })));
            await host.StartAsync();

            var response = await Get(host, "/permitted", "Bearer good");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PermissionPolicy_Returns403WithoutAChallenge_WhenThePermissionIsMissing()
        {
            // The critical one: a permission cannot be gained by re-authorizing, so challenging
            // here would loop forever.
            using var host = CreateHost(
                new StubVerifier(Authenticated(permissions: new[] { "read:things" })));
            await host.StartAsync();

            var response = await Get(host, "/permitted", "Bearer good");

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
            Assert.Empty(response.Headers.WwwAuthenticate);
        }

        [Fact]
        public async Task AnyPermissionPolicy_AllowsATokenCarryingJustOne()
        {
            using var host = CreateHost(
                new StubVerifier(Authenticated(permissions: new[] { "read:things" })));
            await host.StartAsync();

            var response = await Get(host, "/any-permitted", "Bearer good");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PermissionChecks_AreCaseSensitive()
        {
            using var host = CreateHost(
                new StubVerifier(Authenticated(permissions: new[] { "WRITE:THINGS" })));
            await host.StartAsync();

            var response = await Get(host, "/permitted", "Bearer good");

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task GetKindeAuthContext_ReturnsNull_WhenNoTokenWasVerified()
        {
            using var host = CreateHost(new StubVerifier(Authenticated()));
            await host.StartAsync();

            // /open does not authenticate, so nothing should have been stashed on the request.
            var response = await Get(host, "/open");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task TwoSchemes_EachValidateAgainstTheirOwnResource()
        {
            // One shared verifier instance would give every scheme whichever resource was
            // registered last, so a scheme would reject its own tokens and accept the other's.
            const string SecondScheme = "KindeAgentB";
            const string SecondResource = "https://api.example.com/other";

            using var host = new HostBuilder()
                .ConfigureWebHost(web => web
                    .UseTestServer()
                    .ConfigureServices(services =>
                    {
                        services.AddRouting();
                        services.AddAuthentication(KindeAgentAuthenticationOptions.DefaultScheme)
                            .AddKindeAgentAuth(options =>
                            {
                                options.Kinde = Options();
                                options.Verifier = new StubVerifier(Authenticated());
                            })
                            .AddKindeAgentAuth(SecondScheme, options =>
                            {
                                var second = Options();
                                second.ResourceUrl = SecondResource;
                                options.Kinde = second;
                                options.Verifier = new StubVerifier(Authenticated());
                            });
                        services.AddAuthorization();
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();
                        app.UseAuthentication();
                        app.UseAuthorization();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapGet("/a", () => Results.Text("a"))
                                .RequireAuthorization(new AuthorizeAttribute
                                {
                                    AuthenticationSchemes = KindeAgentAuthenticationOptions.DefaultScheme
                                });
                            endpoints.MapGet("/b", () => Results.Text("b"))
                                .RequireAuthorization(new AuthorizeAttribute
                                {
                                    AuthenticationSchemes = SecondScheme
                                });
                        });
                    }))
                .Build();

            await host.StartAsync();

            // Each scheme must advertise its own resource in the challenge it issues.
            var challengeA = Assert.Single((await Get(host, "/a")).Headers.WwwAuthenticate).ToString();
            var challengeB = Assert.Single((await Get(host, "/b")).Headers.WwwAuthenticate).ToString();

            Assert.Contains("/.well-known/oauth-protected-resource/mcp", challengeA);
            Assert.Contains("/.well-known/oauth-protected-resource/other", challengeB);
            Assert.NotEqual(challengeA, challengeB);
        }

        [Fact]
        public async Task ACustomSchemeAlone_StillServesTheResourceMetadata()
        {
            // Resolving KindeAgentAuth must not assume the default scheme name: an application
            // that registers only a custom scheme has no options under the default one, so the
            // metadata endpoint and the startup validator would fail.
            using var host = new HostBuilder()
                .ConfigureWebHost(web => web
                    .UseTestServer()
                    .ConfigureServices(services =>
                    {
                        services.AddRouting();
                        services.AddAuthentication("OnlyCustom")
                            .AddKindeAgentAuth("OnlyCustom", options =>
                            {
                                options.Kinde = Options();
                                options.Verifier = new StubVerifier(Authenticated());
                            });
                        services.AddAuthorization();
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();
                        app.UseAuthentication();
                        app.UseAuthorization();
                        app.UseEndpoints(endpoints => endpoints.MapKindeProtectedResourceMetadata());
                    }))
                .Build();

            await host.StartAsync();

            var response = await Get(host, "/.well-known/oauth-protected-resource/mcp");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains(ResourceUrl, await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task EverySchemesMetadataIsServed_WhateverTheRegistrationOrder()
        {
            // The custom scheme is registered first on purpose: mapping only one document would
            // leave the other scheme's challenge pointing at a 404, chosen by registration order.
            const string SecondResource = "https://api.example.com/other";

            using var host = new HostBuilder()
                .ConfigureWebHost(web => web
                    .UseTestServer()
                    .ConfigureServices(services =>
                    {
                        services.AddRouting();
                        services.AddAuthentication("Custom")
                            .AddKindeAgentAuth("Custom", options =>
                            {
                                var custom = Options();
                                custom.ResourceUrl = SecondResource;
                                options.Kinde = custom;
                                options.Verifier = new StubVerifier(Authenticated());
                            })
                            .AddKindeAgentAuth(options =>
                            {
                                options.Kinde = Options();
                                options.Verifier = new StubVerifier(Authenticated());
                            });
                        services.AddAuthorization();
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();
                        app.UseAuthentication();
                        app.UseAuthorization();
                        app.UseEndpoints(endpoints => endpoints.MapKindeProtectedResourceMetadata());
                    }))
                .Build();

            await host.StartAsync();

            var custom = await Get(host, "/.well-known/oauth-protected-resource/other");
            var standard = await Get(host, "/.well-known/oauth-protected-resource/mcp");

            Assert.Equal(HttpStatusCode.OK, custom.StatusCode);
            Assert.Equal(HttpStatusCode.OK, standard.StatusCode);
            Assert.Contains(SecondResource, await custom.Content.ReadAsStringAsync());
            Assert.Contains(ResourceUrl, await standard.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task SchemesOnDifferentHosts_AreBothServedAtTheUrlTheirChallengeAdvertises()
        {
            // Two resources on different hosts share the path /mcp, so both challenges advertise
            // .../.well-known/oauth-protected-resource/mcp on their own host. Serving the
            // documents at invented paths would put them where no challenge points, so the same
            // path is mapped once per scheme and told apart by the Host header.
            const string SecondResource = "https://other.example.com/mcp";
            const string Path = "/.well-known/oauth-protected-resource/mcp";

            using var host = new HostBuilder()
                .ConfigureWebHost(web => web
                    .UseTestServer()
                    .ConfigureServices(services =>
                    {
                        services.AddRouting();
                        services.AddAuthentication(KindeAgentAuthenticationOptions.DefaultScheme)
                            .AddKindeAgentAuth(options =>
                            {
                                options.Kinde = Options();
                                options.Verifier = new StubVerifier(Authenticated());
                            })
                            .AddKindeAgentAuth("Second", options =>
                            {
                                var second = Options();
                                second.ResourceUrl = SecondResource;
                                options.Kinde = second;
                                options.Verifier = new StubVerifier(Authenticated());
                            });
                        services.AddAuthorization();
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();
                        app.UseEndpoints(endpoints => endpoints.MapKindeProtectedResourceMetadata());
                    }))
                .Build();

            await host.StartAsync();

            var first = await GetWithHost(host, Path, "api.example.com");
            var second = await GetWithHost(host, Path, "other.example.com");

            Assert.Equal(HttpStatusCode.OK, first.StatusCode);
            Assert.Equal(HttpStatusCode.OK, second.StatusCode);

            var firstBody = await first.Content.ReadAsStringAsync();
            var secondBody = await second.Content.ReadAsStringAsync();

            Assert.Contains(ResourceUrl, firstBody);
            Assert.DoesNotContain(SecondResource, firstBody);
            Assert.Contains(SecondResource, secondBody);
            Assert.DoesNotContain(ResourceUrl, secondBody);
        }

        private static async Task<HttpResponseMessage> GetWithHost(
            IHost host, string path, string hostHeader)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, $"http://{hostHeader}{path}");
            return await host.GetTestClient().SendAsync(request);
        }

        [Fact]
        public async Task TwoIdenticalSchemesOnOneHost_DoNotClashWithAThirdOnAnother()
        {
            // A and B describe the same resource on the same host, so they are one document and
            // one route. Counting them as two claims on that host would reject a valid setup.
            const string OtherResource = "https://other.example.com/mcp";
            const string Path = "/.well-known/oauth-protected-resource/mcp";

            using var host = new HostBuilder()
                .ConfigureWebHost(web => web
                    .UseTestServer()
                    .ConfigureServices(services =>
                    {
                        services.AddRouting();
                        services.AddAuthentication(KindeAgentAuthenticationOptions.DefaultScheme)
                            .AddKindeAgentAuth(options =>
                            {
                                options.Kinde = Options();
                                options.Verifier = new StubVerifier(Authenticated());
                            })
                            .AddKindeAgentAuth("SameResource", options =>
                            {
                                options.Kinde = Options();
                                options.Verifier = new StubVerifier(Authenticated());
                            })
                            .AddKindeAgentAuth("OtherHost", options =>
                            {
                                var other = Options();
                                other.ResourceUrl = OtherResource;
                                options.Kinde = other;
                                options.Verifier = new StubVerifier(Authenticated());
                            });
                        services.AddAuthorization();
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();
                        app.UseEndpoints(endpoints => endpoints.MapKindeProtectedResourceMetadata());
                    }))
                .Build();

            await host.StartAsync();

            var shared = await GetWithHost(host, Path, "api.example.com");
            var other = await GetWithHost(host, Path, "other.example.com");

            Assert.Equal(HttpStatusCode.OK, shared.StatusCode);
            Assert.Equal(HttpStatusCode.OK, other.StatusCode);
            Assert.Contains(ResourceUrl, await shared.Content.ReadAsStringAsync());
            Assert.Contains(OtherResource, await other.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task MetadataPathsDifferingOnlyByCase_AreTreatedAsTheSameRoute()
        {
            // ASP.NET Core matches routes case-insensitively, so /mcp and /MCP would be two
            // equally ranked endpoints and a request would fail as an ambiguous match. The
            // collision is caught at startup instead.
            using var host = new HostBuilder()
                .ConfigureWebHost(web => web
                    .UseTestServer()
                    .ConfigureServices(services =>
                    {
                        services.AddRouting();
                        services.AddAuthentication(KindeAgentAuthenticationOptions.DefaultScheme)
                            .AddKindeAgentAuth(options =>
                            {
                                options.Kinde = Options();
                                options.Verifier = new StubVerifier(Authenticated());
                            })
                            .AddKindeAgentAuth("UpperCase", options =>
                            {
                                var upper = Options();
                                upper.ResourceUrl = "https://api.example.com/MCP";
                                options.Kinde = upper;
                                options.Verifier = new StubVerifier(Authenticated());
                            });
                        services.AddAuthorization();
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();
                        app.UseEndpoints(endpoints => endpoints.MapKindeProtectedResourceMetadata());
                    }))
                .Build();

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => host.StartAsync());

            Assert.Contains("UpperCase", exception.Message);
        }

        [Fact]
        public async Task SchemesSharingAMetadataPath_MapOnce_WhenTheyDescribeTheSameResource()
        {
            // Identical documents are not a conflict: one route serves both. Mapping two would
            // produce equally ranked endpoints and an ambiguous match at request time.
            using var host = new HostBuilder()
                .ConfigureWebHost(web => web
                    .UseTestServer()
                    .ConfigureServices(services =>
                    {
                        services.AddRouting();
                        services.AddAuthentication(KindeAgentAuthenticationOptions.DefaultScheme)
                            .AddKindeAgentAuth(options =>
                            {
                                options.Kinde = Options();
                                options.Verifier = new StubVerifier(Authenticated());
                            })
                            .AddKindeAgentAuth("Twin", options =>
                            {
                                options.Kinde = Options();
                                options.Verifier = new StubVerifier(Authenticated());
                            });
                        services.AddAuthorization();
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();
                        app.UseEndpoints(endpoints => endpoints.MapKindeProtectedResourceMetadata());
                    }))
                .Build();

            await host.StartAsync();

            var response = await Get(host, "/.well-known/oauth-protected-resource/mcp");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains(ResourceUrl, await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task AnotherSchemesPermissionClaims_DoNotSatisfyAKindePolicy()
        {
            // Kinde user tokens carry a permissions claim too, so an app running both cookie
            // login and agent auth must not let a browser session satisfy an agent policy.
            using var host = new HostBuilder()
                .ConfigureWebHost(web => web
                    .UseTestServer()
                    .ConfigureServices(services =>
                    {
                        services.AddRouting();
                        services.AddAuthentication(KindeAgentAuthenticationOptions.DefaultScheme)
                            .AddKindeAgentAuth(options =>
                            {
                                // Authenticates, but carries no permissions of its own.
                                options.Kinde = Options();
                                options.Verifier = new StubVerifier(Authenticated());
                            });

                        services.AddAuthorization(authorization =>
                            authorization.AddPolicy("write-permission", policy => policy
                                .RequireAuthenticatedUser()
                                .RequireKindePermission("write:things")));
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();
                        app.UseAuthentication();

                        // Stands in for a cookie session: a second identity granting the very
                        // permission the agent policy wants.
                        app.Use(async (context, next) =>
                        {
                            var identity = new System.Security.Claims.ClaimsIdentity("CookieLogin");
                            identity.AddClaim(new System.Security.Claims.Claim(
                                KindeClaimTypes.Permission, "write:things"));
                            context.User.AddIdentity(identity);
                            await next();
                        });

                        app.UseAuthorization();
                        app.UseEndpoints(endpoints => endpoints
                            .MapGet("/permitted", () => Results.Text("permitted"))
                            .RequireAuthorization("write-permission"));
                    }))
                .Build();

            await host.StartAsync();

            var response = await Get(host, "/permitted", "Bearer good");

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task ScopePolicy_Returns403WithInsufficientScope_NamingWhatIsMissing()
        {
            // A missing scope is recoverable, and the MCP step-up flow depends on the challenge
            // naming the scope so the client knows what to re-authorize for.
            using var host = CreateHost(new StubVerifier(Authenticated(scopes: new[] { "offline" })));
            await host.StartAsync();

            var response = await Get(host, "/scoped", "Bearer good");

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);

            var challenge = Assert.Single(response.Headers.WwwAuthenticate).ToString();
            Assert.Contains("error=\"insufficient_scope\"", challenge);
            Assert.Contains("scope=\"read:things\"", challenge);
        }

        [Fact]
        public async Task PermissionPolicy_StillSendsNoChallenge_WhenThePermissionIsMissing()
        {
            // Unlike a scope, a permission cannot be gained by re-authorizing, so challenging
            // would loop forever. This must not have changed when scopes started challenging.
            using var host = CreateHost(
                new StubVerifier(Authenticated(permissions: new[] { "read:things" })));
            await host.StartAsync();

            var response = await Get(host, "/permitted", "Bearer good");

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
            Assert.Empty(response.Headers.WwwAuthenticate);
        }

        [Fact]
        public void AddKindeAgentAuth_Throws_WhenTheConfigurationSectionIsMissing()
        {
            var services = new ServiceCollection();
            var configuration = new Microsoft.Extensions.Configuration.ConfigurationBuilder().Build();

            var exception = Assert.Throws<InvalidOperationException>(
                () => services.AddAuthentication().AddKindeAgentAuth(configuration));

            Assert.Contains(KindeAgentAuthOptions.SectionName, exception.Message);
        }

        [Fact]
        public async Task AddKindeAgentAuth_FailsTheHostStart_WhenTheOptionsAreInvalid()
        {
            // Must stop the app at boot, not 500 on the first token-bearing request.
            using var host = new HostBuilder()
                .ConfigureWebHost(web => web
                    .UseTestServer()
                    .ConfigureServices(services => services
                        .AddAuthentication(KindeAgentAuthenticationOptions.DefaultScheme)
                        .AddKindeAgentAuth(options =>
                        {
                            options.Kinde = new KindeAgentAuthOptions
                            {
                                Domain = Domain,
                                ClientId = ClientId,
                                ClientSecret = "secret"
                            };
                        }))
                    .Configure(app => app.Run(_ => Task.CompletedTask)))
                .Build();

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => host.StartAsync());
            Assert.Contains(nameof(KindeAgentAuthOptions.ResourceUrl), exception.Message);
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
