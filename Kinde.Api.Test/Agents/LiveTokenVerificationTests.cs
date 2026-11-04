#nullable enable
using System;
using System.Linq;
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

        /// <summary>
        /// The configured agent token, when there is one that has not expired.
        /// </summary>
        /// <remarks>
        /// An agent token cannot be minted without a consent step, so the one in appsettings is
        /// pasted in by hand and ages out within a day. Letting that fail the suite would be a
        /// false alarm that trains people to ignore red builds, so a stale token skips instead.
        /// </remarks>
        private bool TryGetUsableToken(out string token)
        {
            token = TestConfiguration.Instance.AgentAccessToken ?? string.Empty;

            if (string.IsNullOrWhiteSpace(token))
            {
                _output.WriteLine("Skipped: set KindeAgentAuth:AccessToken to a real agent token.");
                return false;
            }

            var expiresAt = ReadExpiry(token);
            if (expiresAt.HasValue && expiresAt.Value <= DateTimeOffset.UtcNow)
            {
                _output.WriteLine(
                    $"Skipped: the configured agent token expired at {expiresAt:u}. Get a fresh "
                    + "one from the Demo and update KindeAgentAuth:AccessToken.");
                return false;
            }

            return true;
        }

        /// <summary>Reads <c>exp</c> without validating anything. Diagnostic only.</summary>
        private static DateTimeOffset? ReadExpiry(string token)
        {
            var parts = token.Split('.');
            if (parts.Length != 3)
            {
                return null;
            }

            try
            {
                var payload = parts[1].Replace('-', '+').Replace('_', '/');
                payload = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');

                using var document = System.Text.Json.JsonDocument.Parse(
                    Convert.FromBase64String(payload));

                return document.RootElement.TryGetProperty("exp", out var exp)
                       && exp.TryGetInt64(out var seconds)
                    ? DateTimeOffset.FromUnixTimeSeconds(seconds)
                    : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

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
            //
            // The garbage must be JWT-shaped. The verifier rejects anything else before it calls
            // Kinde, so a shapeless string would make this pass against a wrong secret or a wrong
            // endpoint and prove nothing.
            const string JwtShapedGarbage =
                "not-a-real.access-token.signature";

            using var client = new HttpClient();
            var context = await new IntrospectionTokenVerifier(options, client)
                .VerifyAsync(JwtShapedGarbage);

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

            if (!TryGetUsableToken(out var token))
            {
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

            if (!TryGetUsableToken(out var token))
            {
                return;
            }

            // Runs both verifiers over one token Kinde actually issued. The claim names differ
            // between the two paths -- the JWT carries scp and azp where introspection returns
            // scope and client_id -- so a synthetic token cannot prove they agree.
            using var client = new HttpClient();
            var local = await new JwtTokenVerifier(options, client).VerifyAsync(token!);
            var remote = await new IntrospectionTokenVerifier(options, client).VerifyAsync(token!);

            _output.WriteLine($"Expected issuer: {options.ExpectedTokenIssuer}");
            _output.WriteLine($"Expected audience: {options.ResourceUrl}");
            _output.WriteLine($"Local  : {local.IsAuthenticated} sub={local.Subject} "
                              + $"client={local.ClientId} scopes=[{string.Join(" ", local.Scopes)}]");
            _output.WriteLine($"Remote : {remote.IsAuthenticated} sub={remote.Subject} "
                              + $"client={remote.ClientId} scopes=[{string.Join(" ", remote.Scopes)}]");
            _output.WriteLine($"Local failure : {local.FailureKind} - {local.FailureReason}");
            _output.WriteLine($"Remote failure: {remote.FailureKind} - {remote.FailureReason}");

            Assert.True(local.IsAuthenticated, local.FailureReason);
            Assert.True(remote.IsAuthenticated, remote.FailureReason);

            // The two paths must describe the same token. Revocation is deliberately out of
            // scope here: introspection would catch it and local validation would not, which is
            // the documented trade-off between the modes rather than a disagreement.
            Assert.Equal(remote.Subject, local.Subject);
            Assert.Equal(remote.ClientId, local.ClientId);
            Assert.Equal(remote.OrganizationCode, local.OrganizationCode);
            Assert.Equal(remote.Scopes.OrderBy(s => s, StringComparer.Ordinal),
                local.Scopes.OrderBy(s => s, StringComparer.Ordinal));
            Assert.Contains(options.ResourceUrl, local.Audiences);
        }
    }
}
