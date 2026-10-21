#nullable enable
using System;
using System.Linq;
using Kinde.Api.Agents;
using Xunit;

namespace Kinde.Api.Test.Agents
{
    public class KindeAgentAuthOptionsTests
    {
        private static KindeAgentAuthOptions Valid() => new KindeAgentAuthOptions
        {
            Domain = "https://example.kinde.com",
            ClientId = "abc123",
            ClientSecret = "secret",
            ResourceUrl = "https://api.example.com/mcp"
        };

        [Fact]
        public void Validate_DoesNotThrow_ForCompleteOptions()
        {
            Valid().Validate();
        }

        [Theory]
        [InlineData("https://example.kinde.com/")]
        [InlineData("https://example.kinde.com")]
        public void NormalizedDomain_RemovesTrailingSlash(string domain)
        {
            var options = Valid();
            options.Domain = domain;

            Assert.Equal("https://example.kinde.com", options.NormalizedDomain);
        }

        [Fact]
        public void Issuer_IsScopedToTheApplication()
        {
            Assert.Equal("https://example.kinde.com/oauth2/agents/abc123", Valid().Issuer);
        }

        [Fact]
        public void ExpectedTokenIssuer_IsTheBareDomain_NotTheScopedIssuer()
        {
            // Tokens carry the bare domain in iss. Validating against Issuer rejects every token.
            var options = Valid();

            Assert.Equal("https://example.kinde.com", options.ExpectedTokenIssuer);
            Assert.NotEqual(options.Issuer, options.ExpectedTokenIssuer);
        }

        [Fact]
        public void AuthorizationServerMetadataUrl_InsertsThePath_RatherThanAppendingIt()
        {
            Assert.Equal(
                "https://example.kinde.com/.well-known/oauth-authorization-server/oauth2/agents/abc123",
                Valid().AuthorizationServerMetadataUrl);
        }

        [Fact]
        public void JwksUrl_IsEnvironmentWide()
        {
            Assert.Equal("https://example.kinde.com/.well-known/jwks", Valid().JwksUrl);
        }

        [Fact]
        public void IntrospectionAndRevocationUrls_HangOffTheScopedIssuer()
        {
            var options = Valid();

            Assert.Equal("https://example.kinde.com/oauth2/agents/abc123/introspect", options.IntrospectionUrl);
            Assert.Equal("https://example.kinde.com/oauth2/agents/abc123/revoke", options.RevocationUrl);
        }

        [Theory]
        [InlineData("https://api.example.com/mcp", "/.well-known/oauth-protected-resource/mcp")]
        [InlineData("https://api.example.com/mcp/", "/.well-known/oauth-protected-resource/mcp")]
        [InlineData("https://api.example.com/", "/.well-known/oauth-protected-resource")]
        [InlineData("https://api.example.com", "/.well-known/oauth-protected-resource")]
        [InlineData("https://api.example.com/a/b", "/.well-known/oauth-protected-resource/a/b")]
        public void ProtectedResourceMetadataPath_FollowsTheResourcePath(string resourceUrl, string expected)
        {
            var options = Valid();
            options.ResourceUrl = resourceUrl;

            Assert.Equal(expected, options.ProtectedResourceMetadataPath);
        }

        [Fact]
        public void ProtectedResourceMetadataUrl_IsAbsolute()
        {
            Assert.Equal(
                "https://api.example.com/.well-known/oauth-protected-resource/mcp",
                Valid().ProtectedResourceMetadataUrl);
        }

        [Fact]
        public void EffectiveScopesSupported_AlwaysIncludesOffline()
        {
            var options = Valid();
            options.ScopesSupported = new[] { "read:things" };

            Assert.Equal(new[] { "offline", "read:things" }, options.EffectiveScopesSupported);
        }

        [Fact]
        public void EffectiveScopesSupported_DoesNotDuplicateOffline()
        {
            var options = Valid();
            options.ScopesSupported = new[] { "read:things", "offline" };

            Assert.Single(options.EffectiveScopesSupported, s => s == "offline");
        }

        [Fact]
        public void EffectiveScopesSupported_IgnoresBlankEntries()
        {
            var options = Valid();
            options.ScopesSupported = new[] { "", "  ", "read:things" };

            Assert.Equal(new[] { "offline", "read:things" }, options.EffectiveScopesSupported);
        }

        [Fact]
        public void Validate_Throws_WhenDomainIsMissing()
        {
            var options = Valid();
            options.Domain = "";

            var exception = Assert.Throws<ArgumentException>(() => options.Validate());
            Assert.Contains(nameof(KindeAgentAuthOptions.Domain), exception.Message);
        }

        [Theory]
        [InlineData("example.kinde.com")]
        [InlineData("http://example.kinde.com")]
        public void Validate_Throws_WhenDomainIsNotAbsoluteHttps(string domain)
        {
            var options = Valid();
            options.Domain = domain;

            var exception = Assert.Throws<ArgumentException>(() => options.Validate());
            Assert.Contains(nameof(KindeAgentAuthOptions.Domain), exception.Message);
        }

        [Fact]
        public void Validate_Throws_WhenClientIdIsMissing()
        {
            var options = Valid();
            options.ClientId = "";

            var exception = Assert.Throws<ArgumentException>(() => options.Validate());
            Assert.Contains(nameof(KindeAgentAuthOptions.ClientId), exception.Message);
        }

        [Fact]
        public void Validate_Throws_WhenSecretIsMissing_ForIntrospection()
        {
            var options = Valid();
            options.ClientSecret = "";
            options.Verification = KindeTokenVerificationMode.Introspection;

            var exception = Assert.Throws<ArgumentException>(() => options.Validate());
            Assert.Contains(nameof(KindeAgentAuthOptions.ClientSecret), exception.Message);
        }

        [Fact]
        public void Validate_AllowsMissingSecret_ForLocalJwtVerification()
        {
            var options = Valid();
            options.ClientSecret = "";
            options.Verification = KindeTokenVerificationMode.Jwt;

            options.Validate();
        }

        [Fact]
        public void Validate_Throws_WhenResourceUrlIsMissing()
        {
            var options = Valid();
            options.ResourceUrl = "";

            var exception = Assert.Throws<ArgumentException>(() => options.Validate());
            Assert.Contains(nameof(KindeAgentAuthOptions.ResourceUrl), exception.Message);
        }

        [Fact]
        public void Validate_Throws_WhenResourceUrlHasAFragment()
        {
            var options = Valid();
            options.ResourceUrl = "https://api.example.com/mcp#tools";

            var exception = Assert.Throws<ArgumentException>(() => options.Validate());
            Assert.Contains(nameof(KindeAgentAuthOptions.ResourceUrl), exception.Message);
        }

        [Fact]
        public void Validate_Throws_WhenClockToleranceIsNegative()
        {
            var options = Valid();
            options.ClockTolerance = TimeSpan.FromSeconds(-1);

            var exception = Assert.Throws<ArgumentException>(() => options.Validate());
            Assert.Contains(nameof(KindeAgentAuthOptions.ClockTolerance), exception.Message);
        }

        [Fact]
        public void Validate_ReportsEveryProblemAtOnce()
        {
            var options = new KindeAgentAuthOptions();

            var exception = Assert.Throws<ArgumentException>(() => options.Validate());
            Assert.Contains(nameof(KindeAgentAuthOptions.Domain), exception.Message);
            Assert.Contains(nameof(KindeAgentAuthOptions.ClientId), exception.Message);
            Assert.Contains(nameof(KindeAgentAuthOptions.ResourceUrl), exception.Message);
        }

        [Fact]
        public void Metadata_AdvertisesTheScopedIssuer_AndTheHeaderBearerMethodOnly()
        {
            var metadata = ProtectedResourceMetadata.FromOptions(Valid());

            Assert.Equal("https://api.example.com/mcp", metadata.Resource);
            Assert.Equal(new[] { "https://example.kinde.com/oauth2/agents/abc123" }, metadata.AuthorizationServers);
            Assert.Equal("https://example.kinde.com/.well-known/jwks", metadata.JwksUri);
            Assert.Equal(new[] { "header" }, metadata.BearerMethodsSupported);
            Assert.Contains("offline", metadata.ScopesSupported);
        }
    }
}
