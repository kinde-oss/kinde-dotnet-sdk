#nullable enable
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Kinde.Api.Agents;
using Kinde.Api.Test.Integration;
using Xunit;
using Xunit.Abstractions;

namespace Kinde.Api.Test.Agents
{
    /// <summary>
    /// Verifies the agent auth core against a real Kinde tenant. These tests are skipped unless
    /// a KindeAgentAuth section is configured, so the suite still runs on a
    /// machine without credentials and in CI.
    /// </summary>
    /// <remarks>
    /// Configure in Kinde.Api.Test/appsettings.json (gitignored) or via KINDE_TEST_AGENT_*
    /// environment variables. An agent access token cannot be minted without a consent step, so
    /// KindeAgentAuth:AccessToken is supplied by hand for the introspection tests.
    /// </remarks>
    public class LiveTokenVerificationTests
    {
        private readonly ITestOutputHelper _output;

        public LiveTokenVerificationTests(ITestOutputHelper output) => _output = output;

        // Gated on the section being filled in rather than on TestMode, so these can run against a
        // real tenant without also switching on the whole real-API integration suite.
        private static bool Enabled => TestConfiguration.Instance.AgentAuth != null;

        private bool SkipIfNotConfigured(out KindeAgentAuthOptions options)
        {
            options = TestConfiguration.Instance.AgentAuth!;

            if (!Enabled)
            {
                _output.WriteLine(
                    "Skipped: fill in the KindeAgentAuth section of appsettings.json.");
                return true;
            }

            return false;
        }

        [Fact]
        public async Task Jwks_IsReachable_AndServesSigningKeys()
        {
            if (SkipIfNotConfigured(out var options))
            {
                return;
            }

            using var client = new HttpClient();
            var document = await client.GetStringAsync(options.JwksUrl);

            _output.WriteLine($"JWKS: {options.JwksUrl}");
            Assert.Contains("\"keys\"", document);
        }

        [Fact]
        public async Task AuthorizationServerMetadata_IsServedAtTheInsertedPath()
        {
            if (SkipIfNotConfigured(out var options))
            {
                return;
            }

            using var client = new HttpClient();
            var response = await client.GetAsync(options.AuthorizationServerMetadataUrl);
            var body = await response.Content.ReadAsStringAsync();

            _output.WriteLine($"Discovery: {options.AuthorizationServerMetadataUrl}");
            _output.WriteLine($"Status: {(int)response.StatusCode}");
            _output.WriteLine(body);

            response.EnsureSuccessStatusCode();
            Assert.Contains("\"issuer\"", body);
            Assert.Contains("\"token_endpoint\"", body);
        }

        [Fact]
        public async Task Introspection_RejectsAGarbageToken_WithoutErroring()
        {
            if (SkipIfNotConfigured(out var options))
            {
                return;
            }

            // Confirms the endpoint and our credentials are right: a wrong secret surfaces as
            // Unavailable rather than InvalidToken, which is the distinction that matters here.
            using var client = new HttpClient();
            var context = await new IntrospectionTokenVerifier(options, client)
                .VerifyAsync("not-a-real-token");

            _output.WriteLine($"Failure: {context.FailureKind} - {context.FailureReason}");

            Assert.False(context.IsAuthenticated);
            Assert.Equal(KindeAuthFailureKind.InvalidToken, context.FailureKind);
        }

        [Fact]
        public async Task Introspection_AcceptsARealAgentToken()
        {
            if (SkipIfNotConfigured(out var options))
            {
                return;
            }

            var token = TestConfiguration.Instance.AgentAccessToken;
            if (string.IsNullOrWhiteSpace(token))
            {
                _output.WriteLine("Skipped: set KindeAgentAuth:AccessToken to a real agent token.");
                return;
            }

            using var client = new HttpClient();
            var context = await new IntrospectionTokenVerifier(options, client).VerifyAsync(token!);

            _output.WriteLine($"Authenticated: {context.IsAuthenticated}");
            _output.WriteLine($"Subject: {context.Subject}");
            _output.WriteLine($"Actor: {context.ActorSubject}");
            _output.WriteLine($"Client: {context.ClientId}");
            _output.WriteLine($"Scopes: {string.Join(", ", context.Scopes)}");
            _output.WriteLine($"Permissions: {string.Join(", ", context.Permissions)}");
            _output.WriteLine($"Audiences: {string.Join(", ", context.Audiences)}");
            _output.WriteLine($"Expires: {context.ExpiresAt}");
            _output.WriteLine($"Failure: {context.FailureKind} - {context.FailureReason}");

            Assert.True(context.IsAuthenticated, context.FailureReason);
            Assert.Contains(options.ResourceUrl, context.Audiences);
        }

        [Fact]
        public async Task LocalJwtVerification_AgreesWithIntrospection_OnARealToken()
        {
            if (SkipIfNotConfigured(out var options))
            {
                return;
            }

            var token = TestConfiguration.Instance.AgentAccessToken;
            if (string.IsNullOrWhiteSpace(token))
            {
                _output.WriteLine("Skipped: set KindeAgentAuth:AccessToken to a real agent token.");
                return;
            }

            // Proves the issuer and audience expectations are right against a token Kinde actually
            // issued, which is the part that cannot be confirmed with a synthetic token.
            using var client = new HttpClient();
            var local = await new JwtTokenVerifier(options, client).VerifyAsync(token!);

            _output.WriteLine($"Expected issuer: {options.ExpectedTokenIssuer}");
            _output.WriteLine($"Expected audience: {options.ResourceUrl}");
            _output.WriteLine($"Authenticated: {local.IsAuthenticated}");
            _output.WriteLine($"Failure: {local.FailureKind} - {local.FailureReason}");

            Assert.True(local.IsAuthenticated, local.FailureReason);
            Assert.Equal(options.ExpectedTokenIssuer, options.NormalizedDomain);
        }
    }
}
