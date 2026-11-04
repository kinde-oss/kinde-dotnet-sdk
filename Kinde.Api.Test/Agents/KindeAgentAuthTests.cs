#nullable enable
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Kinde.Api.Agents;
using Xunit;

namespace Kinde.Api.Test.Agents
{
    public class KindeAgentAuthTests
    {
        private static KindeAgentAuthOptions Options() => new KindeAgentAuthOptions
        {
            Domain = "https://example.kinde.com",
            ClientId = "abc123",
            ClientSecret = "secret",
            ResourceUrl = "https://api.example.com/mcp"
        };

        private static KindeAuthContext SuccessContext() => KindeAuthContext.Success(
            subject: "agent_01",
            actorSubject: null,
            clientId: "https://agent.example.com/.well-known/oauth-client",
            scopes: new[] { "offline" },
            permissions: Array.Empty<string>(),
            audiences: new[] { "https://api.example.com/mcp" },
            organizationCode: null,
            expiresAt: null,
            claims: new Dictionary<string, object>());

        [Fact]
        public void Constructor_ValidatesTheOptions()
        {
            var options = Options();
            options.ResourceUrl = "";

            Assert.Throws<ArgumentException>(
                () => new KindeAgentAuth(options, new HttpClient()));
        }

        [Fact]
        public void Constructor_RejectsNullArguments()
        {
            Assert.Throws<ArgumentNullException>(() => new KindeAgentAuth(null!, new HttpClient()));
            Assert.Throws<ArgumentNullException>(() => new KindeAgentAuth(Options(), (HttpClient)null!));
            Assert.Throws<ArgumentNullException>(
                () => new KindeAgentAuth(Options(), (IKindeTokenVerifier)null!));
        }

        [Fact]
        public void Metadata_IsBuiltFromTheOptions()
        {
            var auth = new KindeAgentAuth(Options(), new StubVerifier(SuccessContext()));

            Assert.Equal("https://api.example.com/mcp", auth.Metadata.Resource);
            Assert.Contains("https://example.kinde.com/oauth2/agents/abc123", auth.Metadata.AuthorizationServers);
        }

        [Theory]
        [InlineData("Bearer abc.def.ghi", "abc.def.ghi")]
        [InlineData("bearer abc.def.ghi", "abc.def.ghi")]
        [InlineData("BEARER abc.def.ghi", "abc.def.ghi")]
        [InlineData("Bearer   abc.def.ghi  ", "abc.def.ghi")]
        public void TryGetBearerToken_AcceptsTheHeaderForm_CaseInsensitively(string header, string expected)
        {
            Assert.True(KindeAgentAuth.TryGetBearerToken(header, out var token));
            Assert.Equal(expected, token);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("Basic abc")]
        [InlineData("Bearer")]
        [InlineData("Bearer ")]
        [InlineData("abc.def.ghi")]
        public void TryGetBearerToken_RejectsAnythingElse(string? header)
        {
            Assert.False(KindeAgentAuth.TryGetBearerToken(header, out var token));
            Assert.Null(token);
        }

        [Fact]
        public async Task VerifyAuthorizationHeaderAsync_ReportsMissingToken_WhenTheHeaderIsAbsent()
        {
            var verifier = new StubVerifier(SuccessContext());
            var auth = new KindeAgentAuth(Options(), verifier);

            var context = await auth.VerifyAuthorizationHeaderAsync(null);

            Assert.False(context.IsAuthenticated);
            Assert.Equal(KindeAuthFailureKind.MissingToken, context.FailureKind);
            Assert.Equal(0, verifier.Calls);
        }

        [Fact]
        public async Task VerifyAuthorizationHeaderAsync_PassesTheTokenWithoutThePrefix()
        {
            var verifier = new StubVerifier(SuccessContext());
            var auth = new KindeAgentAuth(Options(), verifier);

            var context = await auth.VerifyAuthorizationHeaderAsync("Bearer abc.def.ghi");

            Assert.True(context.IsAuthenticated);
            Assert.Equal("abc.def.ghi", verifier.LastToken);
        }

        [Fact]
        public void BuildChallenge_PointsAtTheResourceMetadata()
        {
            var auth = new KindeAgentAuth(Options(), new StubVerifier(SuccessContext()));

            var challenge = auth.BuildChallenge(KindeAuthContext.Invalid("expired"));

            Assert.Contains(
                "resource_metadata=\"https://api.example.com/.well-known/oauth-protected-resource/mcp\"",
                challenge);
            Assert.Contains("error=\"invalid_token\"", challenge);
        }

        [Fact]
        public void BuildChallenge_OmitsTheFailureReason_ByDefault()
        {
            // FailureReason carries IdentityModel validation text, parse errors and the configured
            // resource URL. The challenge header goes to the caller, so it is opt-in.
            var auth = new KindeAgentAuth(Options(), new StubVerifier(SuccessContext()));

            var challenge = auth.BuildChallenge(
                KindeAuthContext.Invalid("audience https://internal.example.com did not match"));

            Assert.DoesNotContain("error_description", challenge);
            Assert.DoesNotContain("internal.example.com", challenge);
        }

        [Fact]
        public void BuildChallenge_IncludesTheFailureReason_WhenAsked()
        {
            var auth = new KindeAgentAuth(Options(), new StubVerifier(SuccessContext()));

            var challenge = auth.BuildChallenge(
                KindeAuthContext.Invalid("expired"), includeErrorDetails: true);

            Assert.Contains("error_description=\"expired\"", challenge);
        }

        [Fact]
        public void BuildChallenge_ReturnsNull_WhenKindeIsUnavailable()
        {
            var auth = new KindeAgentAuth(Options(), new StubVerifier(SuccessContext()));

            Assert.Null(auth.BuildChallenge(KindeAuthContext.Unavailable("timed out")));
        }

        [Fact]
        public void BuildChallenge_RejectsANullContext()
        {
            var auth = new KindeAgentAuth(Options(), new StubVerifier(SuccessContext()));

            Assert.Throws<ArgumentNullException>(() => auth.BuildChallenge(null!));
        }

        [Fact]
        public void StatusCodeFor_MapsTheFailure()
        {
            Assert.Equal(401, KindeAgentAuth.StatusCodeFor(KindeAuthContext.MissingToken()));
            Assert.Equal(401, KindeAgentAuth.StatusCodeFor(KindeAuthContext.Invalid("bad")));
            Assert.Equal(503, KindeAgentAuth.StatusCodeFor(KindeAuthContext.Unavailable("down")));
        }

        [Fact]
        public async Task JwtThenIntrospection_SkipsIntrospection_WhenTheLocalCheckFails()
        {
            // A forged token must never reach the network: that would let an attacker drive traffic
            // to Kinde with junk, and costs a round trip on every bad request.
            var local = new StubVerifier(KindeAuthContext.Invalid("bad signature"));
            var remote = new StubVerifier(SuccessContext());
            var composite = new CompositeTokenVerifier(local, remote);

            var context = await composite.VerifyAsync("token");

            Assert.False(context.IsAuthenticated);
            Assert.Equal(1, local.Calls);
            Assert.Equal(0, remote.Calls);
        }

        [Fact]
        public async Task JwtThenIntrospection_LetsIntrospectionDecide_WhenTheLocalCheckPasses()
        {
            // Introspection is authoritative for revocation, so a revoked-but-unexpired token is
            // rejected even though its signature is perfectly valid.
            var local = new StubVerifier(SuccessContext());
            var remote = new StubVerifier(KindeAuthContext.Invalid("revoked"));
            var composite = new CompositeTokenVerifier(local, remote);

            var context = await composite.VerifyAsync("token");

            Assert.False(context.IsAuthenticated);
            Assert.Equal(1, local.Calls);
            Assert.Equal(1, remote.Calls);
        }

        [Fact]
        public void CompositeTokenVerifier_RejectsNullArguments()
        {
            var verifier = new StubVerifier(SuccessContext());

            Assert.Throws<ArgumentNullException>(() => new CompositeTokenVerifier(null!, verifier));
            Assert.Throws<ArgumentNullException>(() => new CompositeTokenVerifier(verifier, null!));
        }

        private sealed class StubVerifier : IKindeTokenVerifier
        {
            private readonly KindeAuthContext _result;

            public StubVerifier(KindeAuthContext result) => _result = result;

            public int Calls { get; private set; }

            public string? LastToken { get; private set; }

            public Task<KindeAuthContext> VerifyAsync(
                string accessToken, CancellationToken cancellationToken = default)
            {
                Calls++;
                LastToken = accessToken;
                return Task.FromResult(_result);
            }
        }
    }
}
