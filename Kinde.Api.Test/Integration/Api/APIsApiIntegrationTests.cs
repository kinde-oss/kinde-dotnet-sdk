using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;
using Kinde.Api.Test.Integration;
using Xunit;

namespace Kinde.Api.Test.Integration.Api
{
    /// <summary>
    /// Integration tests for APIsApi using the OpenAPI Mock Server.
    /// These tests validate serialization and deserialization of API requests and responses.
    /// </summary>
    public class APIsApiIntegrationTests : IntegrationTestBase
    {
        public APIsApiIntegrationTests(MockServerFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetAPIs_DeserializesCorrectly()
        {
            // Arrange
            var expectedResponse = new
            {
                apis = new[]
                {
                    new
                    {
                        id = "api_123",
                        name = "Test API",
                        audience = "https://api.example.com",
                        code = "test_api"
                    }
                }
            };

            MockServer.SetMockResponse("/api/v1/apis", "GET", expectedResponse);
            var api = CreateAPIsApi();

            // Act
            var result = await api.GetAPIsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Apis);
        }

        [Fact]
        public async Task GetAPI_DeserializesCorrectly()
        {
            // Arrange
            var apiId = "api_test_123";
            var expectedResponse = new
            {
                api = new
                {
                    id = apiId,
                    name = "Test API",
                    audience = "https://api.example.com",
                    code = "test_api"
                }
            };

            MockServer.SetMockResponse($"/api/v1/apis/{apiId}", "GET", expectedResponse);
            var api = CreateAPIsApi();

            // Act
            var result = await api.GetAPIAsync(apiId);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Api);
            Assert.Equal(apiId, result.Api.Id);
        }

        [Fact]
        public async Task AddAPIs_SerializesAndDeserializesCorrectly()
        {
            // Arrange
            var request = new AddAPIsRequest(
                name: "New API",
                audience: "https://api.example.com"
            );

            var expectedResponse = new
            {
                code = "CREATED",
                message = "API created successfully",
                api = new
                {
                    id = "api_new_123",
                    name = "New API",
                    audience = "https://api.example.com",
                    code = "new_api"
                }
            };

            MockServer.SetMockResponse("/api/v1/apis", "POST", expectedResponse);
            var api = CreateAPIsApi();

            // Act
            var result = await api.AddAPIsAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Api);
            Assert.NotNull(result.Api.Id);
        }
    }
}

