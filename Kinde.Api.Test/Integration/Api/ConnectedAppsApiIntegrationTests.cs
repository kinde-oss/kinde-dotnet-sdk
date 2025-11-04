using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;
using Kinde.Api.Test.Integration;
using Xunit;

namespace Kinde.Api.Test.Integration.Api
{
    /// <summary>
    /// Integration tests for ConnectedAppsApi using the OpenAPI Mock Server.
    /// These tests validate serialization and deserialization of API requests and responses.
    /// </summary>
    public class ConnectedAppsApiIntegrationTests : IntegrationTestBase
    {
        public ConnectedAppsApiIntegrationTests(MockServerFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetConnectedAppAuthUrl_DeserializesCorrectly()
        {
            // Arrange
            var keyCodeRef = "key_code_123";
            var expectedResponse = new
            {
                url = "https://auth.example.com/authorize?key_code_ref=key_code_123",
                session_id = "session_123"
            };

            MockServer.SetMockResponse($"/api/v1/connected_apps/auth_url", "GET", expectedResponse);
            var api = CreateConnectedAppsApi();

            // Act
            var result = await api.GetConnectedAppAuthUrlAsync(keyCodeRef);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Url);
        }

        [Fact]
        public async Task GetConnectedAppToken_DeserializesCorrectly()
        {
            // Arrange
            var sessionId = "session_123";
            var expectedResponse = new
            {
                access_token = "token_123",
                expires_in = 3600,
                token_type = "Bearer"
            };

            MockServer.SetMockResponse($"/api/v1/connected_apps/token", "GET", expectedResponse);
            var api = CreateConnectedAppsApi();

            // Act
            var result = await api.GetConnectedAppTokenAsync(sessionId);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.AccessToken);
        }
    }
}

