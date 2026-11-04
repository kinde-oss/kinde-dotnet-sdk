#nullable enable
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Kinde.Api.Agents;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Xunit;

namespace Kinde.Api.Test.Agents
{
    /// <summary>
    /// Exercises local JWT verification against real RSA keys, so a signature test actually fails
    /// when the signature is wrong rather than because a stub said so.
    /// </summary>
    public class JwtTokenVerifierTests
    {
        private const string Domain = "https://example.kinde.com";
        private const string ClientId = "abc123";
        private const string ResourceUrl = "https://api.example.com/mcp";

        // Created once per class: RSA key generation is slow enough to matter across this many tests.
        private static readonly RsaSecurityKey TrustedKey = CreateKey("trusted-key");
        private static readonly RsaSecurityKey AttackerKey = CreateKey("attacker-key");

        private static RsaSecurityKey CreateKey(string keyId) =>
            new RsaSecurityKey(RSA.Create(2048)) { KeyId = keyId };

        private static KindeAgentAuthOptions Options() => new KindeAgentAuthOptions
        {
            Domain = Domain,
            ClientId = ClientId,
            ClientSecret = "secret",
            ResourceUrl = ResourceUrl,
            Verification = KindeTokenVerificationMode.Jwt
        };

        private static JwtTokenVerifier CreateVerifier(KindeAgentAuthOptions? options = null) =>
            new JwtTokenVerifier(
                options ?? Options(),
                new StaticConfigurationManager(TrustedKey));

        private static string CreateToken(
            SecurityKey? signingKey = null,
            string? issuer = Domain,
            string? audience = ResourceUrl,
            string algorithm = SecurityAlgorithms.RsaSha256,
            DateTime? expires = null,
            DateTime? notBefore = null,
            IDictionary<string, object>? extraClaims = null,
            bool omitDefaultScopeClaims = false)
        {
            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                NotBefore = notBefore ?? DateTime.UtcNow.AddMinutes(-1),
                Expires = expires ?? DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(signingKey ?? TrustedKey, algorithm),
                Claims = new Dictionary<string, object>
                {
                    ["sub"] = "agent_01",
                }
            };

            // Omitted when a test needs the claim names a real Kinde token uses instead.
            if (!omitDefaultScopeClaims)
            {
                descriptor.Claims["client_id"] = "https://agent.example.com/.well-known/oauth-client";
                descriptor.Claims["scope"] = "offline read:things";
            }

            if (extraClaims != null)
            {
                foreach (var claim in extraClaims)
                {
                    descriptor.Claims[claim.Key] = claim.Value;
                }
            }

            var handler = new JwtSecurityTokenHandler { SetDefaultTimesOnTokenCreation = false };
            return handler.WriteToken(handler.CreateToken(descriptor));
        }

        [Fact]
        public async Task VerifyAsync_AcceptsAValidToken()
        {
            var context = await CreateVerifier().VerifyAsync(CreateToken());

            Assert.True(context.IsAuthenticated);
            Assert.Equal("agent_01", context.Subject);
            Assert.Equal("https://agent.example.com/.well-known/oauth-client", context.ClientId);
            Assert.Contains(ResourceUrl, context.Audiences);
            Assert.Null(context.FailureKind);
        }

        [Fact]
        public async Task VerifyAsync_ReadsScopesFromTheSpaceDelimitedClaim()
        {
            var context = await CreateVerifier().VerifyAsync(CreateToken());

            Assert.True(context.HasScope("offline"));
            Assert.True(context.HasScope("read:things"));
            Assert.False(context.HasScope("write:things"));
        }

        [Fact]
        public async Task VerifyAsync_ReadsTheClaimNamesKindeActuallyIssues()
        {
            // A real Kinde access token carries scp (an array) and azp, not scope and client_id.
            // Reading only the introspection names leaves scopes empty in Jwt mode, so every
            // RequireKindeScope check fails while introspection accepts the same token.
            var token = CreateToken(extraClaims: new Dictionary<string, object>
            {
                ["scp"] = new[] { "offline", "read:things" },
                ["azp"] = "app_client_id_0000000000000000000000",
                ["org_code"] = "org_10b6e61fc17"
            }, omitDefaultScopeClaims: true);

            var context = await CreateVerifier().VerifyAsync(token);

            Assert.True(context.IsAuthenticated);
            Assert.True(context.HasScope("offline"));
            Assert.True(context.HasScope("read:things"));
            Assert.Equal("app_client_id_0000000000000000000000", context.ClientId);
            Assert.Equal("org_10b6e61fc17", context.OrganizationCode);
        }

        [Fact]
        public async Task VerifyAsync_ReadsPermissions_FromAJsonArrayClaim()
        {
            // The handler materializes an array claim as a JsonElement, which is a struct and so is
            // neither a string nor an IEnumerable. Reading it naively yields one permission named
            // with the whole JSON text, and every permission check then fails.
            var token = CreateToken(extraClaims: new Dictionary<string, object>
            {
                ["permissions"] = new[] { "read:things", "write:things" }
            });

            var context = await CreateVerifier().VerifyAsync(token);

            Assert.True(context.IsAuthenticated);
            Assert.Equal(2, context.Permissions.Count);
            Assert.True(context.HasPermission("read:things"));
            Assert.True(context.HasPermission("write:things"));
            Assert.False(context.HasPermission("delete:things"));
        }

        [Fact]
        public async Task VerifyAsync_ReadsTheActorSubject_OfADelegatedToken()
        {
            var token = CreateToken(extraClaims: new Dictionary<string, object>
            {
                ["act"] = new Dictionary<string, object> { ["sub"] = "agent_delegate" }
            });

            var context = await CreateVerifier().VerifyAsync(token);

            Assert.True(context.IsAuthenticated);
            Assert.Equal("agent_delegate", context.ActorSubject);
        }

        [Fact]
        public async Task VerifyAsync_ReadsTheOrganizationCode()
        {
            var token = CreateToken(extraClaims: new Dictionary<string, object>
            {
                ["org_code"] = "org_1234"
            });

            var context = await CreateVerifier().VerifyAsync(token);

            Assert.Equal("org_1234", context.OrganizationCode);
        }

        [Fact]
        public async Task VerifyAsync_RejectsATokenSignedByAnotherKey()
        {
            var context = await CreateVerifier().VerifyAsync(CreateToken(signingKey: AttackerKey));

            Assert.False(context.IsAuthenticated);
            Assert.Equal(KindeAuthFailureKind.InvalidToken, context.FailureKind);
        }

        [Fact]
        public async Task VerifyAsync_RejectsATokenWhosePayloadWasTampered()
        {
            var parts = CreateToken().Split('.');
            var payload = Base64UrlEncoder.Decode(parts[1]).Replace("agent_01", "agent_99");
            var tampered = $"{parts[0]}.{Base64UrlEncoder.Encode(payload)}.{parts[2]}";

            var context = await CreateVerifier().VerifyAsync(tampered);

            Assert.False(context.IsAuthenticated);
            Assert.Equal(KindeAuthFailureKind.InvalidToken, context.FailureKind);
        }

        [Fact]
        public async Task VerifyAsync_RejectsAnUnsignedToken()
        {
            var header = Base64UrlEncoder.Encode("{\"alg\":\"none\",\"typ\":\"JWT\"}");
            var payload = Base64UrlEncoder.Encode(
                $"{{\"iss\":\"{Domain}\",\"aud\":\"{ResourceUrl}\",\"sub\":\"agent_01\"," +
                $"\"exp\":{DateTimeOffset.UtcNow.AddMinutes(10).ToUnixTimeSeconds()}}}");

            var context = await CreateVerifier().VerifyAsync($"{header}.{payload}.");

            Assert.False(context.IsAuthenticated);
            Assert.Equal(KindeAuthFailureKind.InvalidToken, context.FailureKind);
        }

        [Fact]
        public async Task VerifyAsync_RejectsASymmetricallySignedToken()
        {
            // Algorithms are pinned to RS256, so a token signed with a guessable shared secret
            // cannot be substituted for one signed with Kinde's private key.
            var symmetric = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(new string('k', 64)));
            var token = CreateToken(signingKey: symmetric, algorithm: SecurityAlgorithms.HmacSha256);

            var context = await CreateVerifier().VerifyAsync(token);

            Assert.False(context.IsAuthenticated);
            Assert.Equal(KindeAuthFailureKind.InvalidToken, context.FailureKind);
        }

        [Fact]
        public async Task VerifyAsync_RejectsAnExpiredToken()
        {
            var token = CreateToken(
                notBefore: DateTime.UtcNow.AddMinutes(-20),
                expires: DateTime.UtcNow.AddMinutes(-10));

            var context = await CreateVerifier().VerifyAsync(token);

            Assert.False(context.IsAuthenticated);
            Assert.Equal(KindeAuthFailureKind.InvalidToken, context.FailureKind);
        }

        [Fact]
        public async Task VerifyAsync_RejectsATokenFromAnotherIssuer()
        {
            var context = await CreateVerifier().VerifyAsync(
                CreateToken(issuer: "https://attacker.kinde.com"));

            Assert.False(context.IsAuthenticated);
            Assert.Equal(KindeAuthFailureKind.InvalidToken, context.FailureKind);
        }

        [Fact]
        public async Task VerifyAsync_AcceptsTheBareDomainIssuer_NotTheScopedIssuer()
        {
            // Guards the most common misconfiguration: validating iss against the scoped issuer
            // rejects every genuine token Kinde issues.
            var options = Options();

            var bareDomain = await CreateVerifier(options).VerifyAsync(CreateToken(issuer: Domain));
            var scoped = await CreateVerifier(options).VerifyAsync(CreateToken(issuer: options.Issuer));

            Assert.True(bareDomain.IsAuthenticated);
            Assert.False(scoped.IsAuthenticated);
        }

        [Fact]
        public async Task VerifyAsync_RejectsAnAudienceDifferingOnlyByATrailingSlash()
        {
            // IdentityModel ignores a trailing slash by default, so this token would pass here
            // and fail introspection, which compares ordinally. ResourceUrl is documented as
            // matched byte for byte, and the two modes must agree.
            var context = await CreateVerifier().VerifyAsync(
                CreateToken(audience: ResourceUrl + "/"));

            Assert.False(context.IsAuthenticated);
            Assert.Equal(KindeAuthFailureKind.InvalidToken, context.FailureKind);
        }

        [Fact]
        public async Task VerifyAsync_RejectsATokenMintedForAnotherResource()
        {
            var context = await CreateVerifier().VerifyAsync(
                CreateToken(audience: "https://other.example.com/mcp"));

            Assert.False(context.IsAuthenticated);
            Assert.Equal(KindeAuthFailureKind.InvalidToken, context.FailureKind);
        }

        [Fact]
        public async Task VerifyAsync_ReportsMissingToken_ForABlankCredential()
        {
            var context = await CreateVerifier().VerifyAsync("   ");

            Assert.False(context.IsAuthenticated);
            Assert.Equal(KindeAuthFailureKind.MissingToken, context.FailureKind);
        }

        [Fact]
        public async Task VerifyAsync_ReportsInvalidToken_ForAStringThatIsNotAJwt()
        {
            var context = await CreateVerifier().VerifyAsync("not-a-jwt");

            Assert.False(context.IsAuthenticated);
            Assert.Equal(KindeAuthFailureKind.InvalidToken, context.FailureKind);
        }

        [Fact]
        public async Task VerifyAsync_ReportsUnavailable_WhenTheKeysCannotBeFetched()
        {
            // A JWKS outage must not look like a bad token, or the agent re-authorizes in a loop
            // while the real fault is on the network.
            var verifier = new JwtTokenVerifier(Options(), new ThrowingConfigurationManager());

            var context = await verifier.VerifyAsync(CreateToken());

            Assert.False(context.IsAuthenticated);
            Assert.Equal(KindeAuthFailureKind.AuthorizationServerUnavailable, context.FailureKind);
        }

        [Fact]
        public async Task VerifyAsync_RequestsAKeyRefresh_WhenNoKeyMatchesTheTokensKeyId()
        {
            var manager = new StaticConfigurationManager(TrustedKey);
            var verifier = new JwtTokenVerifier(Options(), manager);
            var unknownKey = CreateKey("rotated-key");

            var context = await verifier.VerifyAsync(CreateToken(signingKey: unknownKey));

            Assert.False(context.IsAuthenticated);
            Assert.Equal(KindeAuthFailureKind.InvalidToken, context.FailureKind);
            Assert.True(manager.RefreshRequested);
        }

        [Fact]
        public async Task VerifyAsync_HonoursTheClockTolerance()
        {
            var options = Options();
            options.ClockTolerance = TimeSpan.FromMinutes(5);

            var context = await CreateVerifier(options).VerifyAsync(CreateToken(
                notBefore: DateTime.UtcNow.AddMinutes(-10),
                expires: DateTime.UtcNow.AddMinutes(-2)));

            Assert.True(context.IsAuthenticated);
        }

        [Fact]
        public async Task VerifyAsync_PropagatesCallerCancellation()
        {
            using var cancellation = new CancellationTokenSource();
            cancellation.Cancel();

            await Assert.ThrowsAnyAsync<OperationCanceledException>(
                () => new JwtTokenVerifier(Options(), new CancellingConfigurationManager())
                    .VerifyAsync(CreateToken(), cancellation.Token));
        }

        private sealed class StaticConfigurationManager : IConfigurationManager<OpenIdConnectConfiguration>
        {
            private readonly OpenIdConnectConfiguration _configuration;

            public StaticConfigurationManager(params SecurityKey[] keys)
            {
                _configuration = new OpenIdConnectConfiguration();
                foreach (var key in keys)
                {
                    _configuration.SigningKeys.Add(key);
                }
            }

            public bool RefreshRequested { get; private set; }

            public Task<OpenIdConnectConfiguration> GetConfigurationAsync(CancellationToken cancel) =>
                Task.FromResult(_configuration);

            public void RequestRefresh() => RefreshRequested = true;
        }

        private sealed class ThrowingConfigurationManager : IConfigurationManager<OpenIdConnectConfiguration>
        {
            public Task<OpenIdConnectConfiguration> GetConfigurationAsync(CancellationToken cancel) =>
                throw new InvalidOperationException("JWKS unreachable.");

            public void RequestRefresh()
            {
            }
        }

        private sealed class CancellingConfigurationManager : IConfigurationManager<OpenIdConnectConfiguration>
        {
            public Task<OpenIdConnectConfiguration> GetConfigurationAsync(CancellationToken cancel)
            {
                cancel.ThrowIfCancellationRequested();
                return Task.FromResult(new OpenIdConnectConfiguration());
            }

            public void RequestRefresh()
            {
            }
        }
    }
}
