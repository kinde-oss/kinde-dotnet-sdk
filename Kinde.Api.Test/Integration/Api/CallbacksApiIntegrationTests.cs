using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;
using Kinde.Api.Test.Integration;
using Xunit;

namespace Kinde.Api.Test.Integration.Api
{
    /// <summary>
    /// Integration tests for CallbacksApi using the OpenAPI Mock Server.
    /// These tests validate serialization and deserialization of API requests and responses.
    /// </summary>
    public class CallbacksApiIntegrationTests : IntegrationTestBase
    {
        public CallbacksApiIntegrationTests(MockServerFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetCallbackURLs_DeserializesCorrectly()
        {
            // Arrange
            var appId = "app_123";
            var expectedResponse = new
            {
                redirect_urls = new[]
                {
                    "https://example.com/callback",
                    "https://example.com/callback2"
                }
            };

            MockServer.SetMockResponse($"/api/v1/applications/{appId}/auth_redirect_urls", "GET", expectedResponse);
            var api = CreateCallbacksApi();

            // Act
            var result = await api.GetCallbackURLsAsync(appId);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.RedirectUrls);
        }

        [Fact]
        public async Task GetLogoutURLs_DeserializesCorrectly()
        {
            // Arrange
            var appId = "app_123";
            var expectedResponse = new
            {
                logout_urls = new[]
                {
                    "https://example.com/logout"
                }
            };

            MockServer.SetMockResponse($"/api/v1/applications/{appId}/auth_logout_urls", "GET", expectedResponse);
            var api = CreateCallbacksApi();

            // Act
            var result = await api.GetLogoutURLsAsync(appId);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.LogoutUrls);
        }

        [Fact]
        public async Task AddRedirectCallbackURLs_SerializesAndDeserializesCorrectly()
        {
            // Arrange
            var appId = "app_123";
            var request = new ReplaceRedirectCallbackURLsRequest
            {
                Urls = new List<string> { "https://example.com/new_callback" }
            };

            var expectedResponse = new
            {
                code = "SUCCESS",
                message = "Callback URLs updated successfully"
            };

            MockServer.SetMockResponse($"/api/v1/applications/{appId}/auth_redirect_urls", "POST", expectedResponse);
            var api = CreateCallbacksApi();

            // Act
            var result = await api.AddRedirectCallbackURLsAsync(appId, request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("SUCCESS", result.Code);
        }
    }
}

