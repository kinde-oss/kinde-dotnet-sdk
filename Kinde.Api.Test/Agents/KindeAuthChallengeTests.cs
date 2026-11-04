#nullable enable
using Kinde.Api.Agents;
using Xunit;

namespace Kinde.Api.Test.Agents
{
    public class KindeAuthChallengeTests
    {
        private const string MetadataUrl = "https://api.example.com/.well-known/oauth-protected-resource/mcp";

        [Theory]
        [InlineData(KindeAuthFailureKind.MissingToken, 401)]
        [InlineData(KindeAuthFailureKind.InvalidToken, 401)]
        [InlineData(KindeAuthFailureKind.InsufficientScope, 403)]
        [InlineData(KindeAuthFailureKind.AuthorizationServerUnavailable, 503)]
        public void StatusCodeFor_MapsEachFailure(KindeAuthFailureKind kind, int expected)
        {
            Assert.Equal(expected, KindeAuthChallenge.StatusCodeFor(kind));
        }

        [Fact]
        public void Build_AlwaysPointsAtTheResourceMetadata()
        {
            // Without this parameter a client cannot discover the authorization server at all.
            var challenge = KindeAuthChallenge.Build(KindeAuthFailureKind.MissingToken, MetadataUrl);

            Assert.StartsWith("Bearer ", challenge);
            Assert.Contains($"resource_metadata=\"{MetadataUrl}\"", challenge);
        }

        [Fact]
        public void Build_OmitsAnErrorCode_WhenNoTokenWasPresented()
        {
            var challenge = KindeAuthChallenge.Build(KindeAuthFailureKind.MissingToken, MetadataUrl);

            Assert.DoesNotContain("error=", challenge);
        }

        [Fact]
        public void Build_ReportsInvalidToken()
        {
            var challenge = KindeAuthChallenge.Build(
                KindeAuthFailureKind.InvalidToken, MetadataUrl, "The token has expired.");

            Assert.Contains("error=\"invalid_token\"", challenge);
            Assert.Contains("error_description=\"The token has expired.\"", challenge);
        }

        [Fact]
        public void Build_ReportsInsufficientScope_WithTheScopesRequired()
        {
            var challenge = KindeAuthChallenge.Build(
                KindeAuthFailureKind.InsufficientScope,
                MetadataUrl,
                requiredScopes: new[] { "read:things", "write:things" });

            Assert.Contains("error=\"insufficient_scope\"", challenge);
            Assert.Contains("scope=\"read:things write:things\"", challenge);
        }

        [Fact]
        public void Build_OmitsTheScopeParameter_WhenNoScopesAreGiven()
        {
            var challenge = KindeAuthChallenge.Build(
                KindeAuthFailureKind.InsufficientScope, MetadataUrl);

            Assert.Contains("error=\"insufficient_scope\"", challenge);
            Assert.DoesNotContain("scope=", challenge);
        }

        [Fact]
        public void Build_IgnoresBlankScopes()
        {
            var challenge = KindeAuthChallenge.Build(
                KindeAuthFailureKind.InsufficientScope,
                MetadataUrl,
                requiredScopes: new[] { "", "  ", "read:things" });

            Assert.Contains("scope=\"read:things\"", challenge);
        }

        [Fact]
        public void Build_ReturnsNull_WhenKindeIsUnavailable()
        {
            // An outage is not an authentication problem; challenging would make the agent
            // restart authorization pointlessly instead of retrying.
            Assert.Null(KindeAuthChallenge.Build(
                KindeAuthFailureKind.AuthorizationServerUnavailable, MetadataUrl, "Timed out."));
        }

        [Fact]
        public void Build_EscapesQuotesAndBackslashes_SoTheHeaderIsNotTruncated()
        {
            var challenge = KindeAuthChallenge.Build(
                KindeAuthFailureKind.InvalidToken, MetadataUrl, "bad \"value\" with \\ backslash");

            Assert.Contains("error_description=\"bad \\\"value\\\" with \\\\ backslash\"", challenge);
        }

        [Theory]
        [InlineData("IDX10503: Token validation failed.\nKeys tried: '[PII]'.\nExceptions: '...'")]
        [InlineData("line one\r\nline two")]
        [InlineData("tab\tseparated")]
        public void Build_RemovesLineBreaks_SoKestrelCanWriteTheHeader(string reason)
        {
            // Kestrel throws on a header containing control characters, which would turn the 401
            // carrying resource_metadata into a 500 and break discovery for that request.
            var challenge = KindeAuthChallenge.Build(
                KindeAuthFailureKind.InvalidToken, MetadataUrl, reason);

            Assert.DoesNotContain('\r', challenge);
            Assert.DoesNotContain('\n', challenge);
            Assert.DoesNotContain('\t', challenge);
            Assert.Contains("resource_metadata", challenge);
        }

        [Fact]
        public void Build_DropsNonAsciiCharacters()
        {
            // Also invalid in a Kestrel header value.
            var challenge = KindeAuthChallenge.Build(
                KindeAuthFailureKind.InvalidToken, MetadataUrl, "café — naïve \u0001");

            Assert.All(challenge!, c => Assert.InRange(c, ' ', '~'));
            Assert.Contains("caf", challenge);
        }

        [Fact]
        public void Build_FallsBackToBareBearer_WhenThereIsNothingToAdvertise()
        {
            Assert.Equal("Bearer", KindeAuthChallenge.Build(KindeAuthFailureKind.MissingToken, ""));
        }
    }
}
