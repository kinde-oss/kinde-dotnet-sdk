using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;
using Kinde.Api.Test.Integration;
using Xunit;

namespace Kinde.Api.Test.Integration.Api
{
    /// <summary>
    /// Integration tests for EnvironmentsApi using the OpenAPI Mock Server.
    /// These tests validate serialization and deserialization of API requests and responses.
    /// </summary>
    public class EnvironmentsApiIntegrationTests : IntegrationTestBase
    {
        public EnvironmentsApiIntegrationTests(MockServerFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetEnvironment_DeserializesCorrectly()
        {
            // Arrange
            var expectedResponse = new
            {
                environment = new
                {
                    id = "env_123",
                    name = "Production",
                    is_production = true,
                    timezone = "America/New_York"
                }
            };

            MockServer.SetMockResponse("/api/v1/environment", "GET", expectedResponse);
            var api = CreateEnvironmentsApi();

            // Act
            var result = await api.GetEnvironmentAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Environment);
            Assert.Equal("Production", result.Environment.Name);
        }

        [Fact]
        public async Task GetEnvironementFeatureFlags_DeserializesCorrectly()
        {
            // Arrange
            var expectedResponse = new
            {
                feature_flags = new Dictionary<string, object>
                {
                    { "feature_123", new
                        {
                            type = "bool",
                            value = "true"
                        }
                    }
                }
            };

            MockServer.SetMockResponse("/api/v1/environment/feature_flags", "GET", expectedResponse);
            var api = CreateEnvironmentsApi();

            // Act
            var result = await api.GetEnvironementFeatureFlagsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.FeatureFlags);
        }
    }
}

