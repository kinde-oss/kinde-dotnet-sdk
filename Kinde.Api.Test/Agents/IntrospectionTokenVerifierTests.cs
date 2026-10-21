#nullable enable
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Kinde.Api.Agents;
using Xunit;

namespace Kinde.Api.Test.Agents
{
    public class IntrospectionTokenVerifierTests
    {
        private const string Domain = "https://example.kinde.com";
        private const string ClientId = "abc123";
        private const string ClientSecret = "sh:colon";
        private const string ResourceUrl = "https://api.example.com/mcp";

        private static KindeAgentAuthOptions Options() => new KindeAgentAuthOptions
        {
            Domain = Domain,
            ClientId = ClientId,
            ClientSecret = ClientSecret,
            ResourceUrl = ResourceUrl
        };

        private static string ActiveResponse(
            string audience = ResourceUrl,
            string scope = "offline read:things",
            string? extra = null)
        {
            var body = new StringBuilder();
            body.Append("{\"active\":true,\"sub\":\"agent_01\",");
            body.Append("\"client_id\":\"https://agent.example.com/.well-known/oauth-client\",");
            body.Append($"\"scope\":\"{scope}\",");
            body.Append($"\"aud\":[\"{audience}\"],");
            body.Append($"\"exp\":{DateTimeOffset.UtcNow.AddMinutes(10).ToUnixTimeSeconds()}");
            if (extra != null)
            {
                body.Append(',').Append(extra);
            }
            body.Append('}');
            return body.ToString();
        }

        private static (IntrospectionTokenVerifier Verifier, StubHandler Handler) Create(
            HttpResponseMessage response, KindeAgentAuthOptions? options = null)
        {
            var handler = new StubHandler(response);
            return (new IntrospectionTokenVerifier(options ?? Options(), new HttpClient(handler)), handler);
        }

        private static HttpResponseMessage Json(HttpStatusCode status, string body) =>
            new HttpResponseMessage(status)
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            };

        [Fact]
        public async Task VerifyAsync_AcceptsAnActiveToken()
        {
            var (verifier, _) = Create(Json(HttpStatusCode.OK, ActiveResponse()));

            var context = await verifier.VerifyAsync("token");

            Assert.True(context.IsAuthenticated);
            Assert.Equal("agent_01", context.Subject);
            Assert.Equal("https://agent.example.com/.well-known/oauth-client", context.ClientId);
            Assert.True(context.HasScope("read:things"));
            Assert.NotNull(context.ExpiresAt);
        }

        [Fact]
        public async Task VerifyAsync_PostsToTheScopedIntrospectionEndpoint()
        {
            var (verifier, handler) = Create(Json(HttpStatusCode.OK, ActiveResponse()));

            await verifier.VerifyAsync("the-token");

            Assert.Equal(HttpMethod.Post, handler.Request!.Method);
            Assert.Equal(
                "https://example.kinde.com/oauth2/agents/abc123/introspect",
                handler.Request.RequestUri!.ToString());
        }

        [Fact]
        public async Task VerifyAsync_SendsTheTokenAndTypeHint_AsFormValues()
        {
            var (verifier, handler) = Create(Json(HttpStatusCode.OK, ActiveResponse()));

            await verifier.VerifyAsync("the-token");

            Assert.Contains("token=the-token", handler.Body);
            Assert.Contains("token_type_hint=access_token", handler.Body);
        }

        [Fact]
        public async Task VerifyAsync_AuthenticatesWithTheApplicationCredentials()
        {
            var (verifier, handler) = Create(Json(HttpStatusCode.OK, ActiveResponse()));

            await verifier.VerifyAsync("token");

            Assert.Equal("Basic", handler.Request!.Headers.Authorization!.Scheme);

            // Each half is URL-escaped before encoding, so a secret containing ':' still splits in
            // the right place on Kinde's side.
            var decoded = Encoding.UTF8.GetString(
                Convert.FromBase64String(handler.Request.Headers.Authorization.Parameter!));
            Assert.Equal($"{ClientId}:{Uri.EscapeDataString(ClientSecret)}", decoded);
        }

        [Fact]
        public async Task VerifyAsync_RejectsAnInactiveToken()
        {
            var (verifier, _) = Create(Json(HttpStatusCode.OK, "{\"active\":false}"));

            var context = await verifier.VerifyAsync("token");

            Assert.False(context.IsAuthenticated);
            Assert.Equal(KindeAuthFailureKind.InvalidToken, context.FailureKind);
        }

        [Fact]
        public async Task VerifyAsync_RejectsAResponseWithoutTheActiveField()
        {
            var (verifier, _) = Create(Json(HttpStatusCode.OK, "{\"sub\":\"agent_01\"}"));

            var context = await verifier.VerifyAsync("token");

            Assert.False(context.IsAuthenticated);
            Assert.Equal(KindeAuthFailureKind.InvalidToken, context.FailureKind);
        }

        [Fact]
        public async Task VerifyAsync_RejectsATokenMintedForAnotherResource()
        {
            // RFC 8707: an active token for a different resource is still not valid here.
            var (verifier, _) = Create(
                Json(HttpStatusCode.OK, ActiveResponse(audience: "https://other.example.com/mcp")));

            var context = await verifier.VerifyAsync("token");

            Assert.False(context.IsAuthenticated);
            Assert.Equal(KindeAuthFailureKind.InvalidToken, context.FailureKind);
            Assert.Contains("audience", context.FailureReason!, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task VerifyAsync_AcceptsATokenWhoseAudienceListContainsThisResource()
        {
            var body = "{\"active\":true,\"sub\":\"agent_01\","
                       + $"\"aud\":[\"https://other.example.com\",\"{ResourceUrl}\"]}}";
            var (verifier, _) = Create(Json(HttpStatusCode.OK, body));

            var context = await verifier.VerifyAsync("token");

            Assert.True(context.IsAuthenticated);
        }

        [Fact]
        public async Task VerifyAsync_ReadsPermissionsAndTheActorSubject()
        {
            var extra = "\"permissions\":[\"read:things\",\"write:things\"],"
                        + "\"act\":{\"sub\":\"agent_delegate\"},\"org_code\":\"org_1234\"";
            var (verifier, _) = Create(Json(HttpStatusCode.OK, ActiveResponse(extra: extra)));

            var context = await verifier.VerifyAsync("token");

            Assert.True(context.HasAllPermissions("read:things", "write:things"));
            Assert.True(context.HasAnyPermission("delete:things", "read:things"));
            Assert.Equal("agent_delegate", context.ActorSubject);
            Assert.Equal("org_1234", context.OrganizationCode);
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        public async Task VerifyAsync_ReportsUnavailable_WhenKindeRejectsOurOwnCredentials(
            HttpStatusCode status)
        {
            // A 401 here is about this resource server's secret, not the agent's token. Reporting it
            // as an invalid token would send the agent into a login loop over a config mistake.
            var (verifier, _) = Create(Json(status, "{\"error\":\"invalid_client\"}"));

            var context = await verifier.VerifyAsync("token");

            Assert.False(context.IsAuthenticated);
            Assert.Equal(KindeAuthFailureKind.AuthorizationServerUnavailable, context.FailureKind);
            Assert.Contains(nameof(KindeAgentAuthOptions.ClientSecret), context.FailureReason!);
        }

        [Fact]
        public async Task VerifyAsync_ReportsUnavailable_OnAServerError()
        {
            var (verifier, _) = Create(Json(HttpStatusCode.InternalServerError, "oops"));

            var context = await verifier.VerifyAsync("token");

            Assert.Equal(KindeAuthFailureKind.AuthorizationServerUnavailable, context.FailureKind);
        }

        [Fact]
        public async Task VerifyAsync_ReportsUnavailable_OnAMalformedResponse()
        {
            var (verifier, _) = Create(Json(HttpStatusCode.OK, "not json"));

            var context = await verifier.VerifyAsync("token");

            Assert.Equal(KindeAuthFailureKind.AuthorizationServerUnavailable, context.FailureKind);
        }

        [Fact]
        public async Task VerifyAsync_ReportsUnavailable_WhenTheRequestFails()
        {
            var verifier = new IntrospectionTokenVerifier(
                Options(),
                new HttpClient(new ThrowingHandler(new HttpRequestException("no route to host"))));

            var context = await verifier.VerifyAsync("token");

            Assert.Equal(KindeAuthFailureKind.AuthorizationServerUnavailable, context.FailureKind);
        }

        [Fact]
        public async Task VerifyAsync_ReportsUnavailable_WhenIntrospectionTimesOut()
        {
            var options = Options();
            options.BackchannelTimeout = TimeSpan.FromMilliseconds(50);
            var verifier = new IntrospectionTokenVerifier(options, new HttpClient(new HangingHandler()));

            var context = await verifier.VerifyAsync("token");

            Assert.Equal(KindeAuthFailureKind.AuthorizationServerUnavailable, context.FailureKind);
            Assert.Contains("timed out", context.FailureReason!, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task VerifyAsync_PropagatesCallerCancellation()
        {
            // The caller giving up is not an authorization server outage, so it must not be
            // flattened into a 503.
            var verifier = new IntrospectionTokenVerifier(Options(), new HttpClient(new HangingHandler()));
            using var cancellation = new CancellationTokenSource();
            cancellation.CancelAfter(TimeSpan.FromMilliseconds(50));

            await Assert.ThrowsAnyAsync<OperationCanceledException>(
                () => verifier.VerifyAsync("token", cancellation.Token));
        }

        [Fact]
        public async Task VerifyAsync_ReportsMissingToken_WithoutCallingKinde()
        {
            var (verifier, handler) = Create(Json(HttpStatusCode.OK, ActiveResponse()));

            var context = await verifier.VerifyAsync("  ");

            Assert.Equal(KindeAuthFailureKind.MissingToken, context.FailureKind);
            Assert.Null(handler.Request);
        }

        private sealed class StubHandler : HttpMessageHandler
        {
            private readonly HttpResponseMessage _response;

            public StubHandler(HttpResponseMessage response) => _response = response;

            public HttpRequestMessage? Request { get; private set; }

            public string Body { get; private set; } = string.Empty;

            protected override async Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request, CancellationToken cancellationToken)
            {
                Request = request;
                if (request.Content != null)
                {
                    Body = await request.Content.ReadAsStringAsync();
                }
                return _response;
            }
        }

        private sealed class ThrowingHandler : HttpMessageHandler
        {
            private readonly Exception _exception;

            public ThrowingHandler(Exception exception) => _exception = exception;

            protected override Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request, CancellationToken cancellationToken) =>
                throw _exception;
        }

        private sealed class HangingHandler : HttpMessageHandler
        {
            protected override async Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request, CancellationToken cancellationToken)
            {
                await Task.Delay(Timeout.Infinite, cancellationToken);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
        }
    }
}
