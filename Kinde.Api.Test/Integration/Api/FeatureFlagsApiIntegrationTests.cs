using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;
using Kinde.Api.Test.Integration;
using Xunit;

namespace Kinde.Api.Test.Integration.Api
{
    /// <summary>
    /// Integration tests for FeatureFlagsApi using the OpenAPI Mock Server.
    /// These tests validate serialization and deserialization of API requests and responses.
    /// </summary>
    public class FeatureFlagsApiIntegrationTests : IntegrationTestBase
    {
        public FeatureFlagsApiIntegrationTests(MockServerFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task CreateFeatureFlag_SerializesAndDeserializesCorrectly()
        {
            // Arrange
            var request = new CreateFeatureFlagRequest(
                name: "New Feature",
                key: "new_feature",
                type: CreateFeatureFlagRequest.TypeEnum.Bool,
                allowOverrideLevel: CreateFeatureFlagRequest.AllowOverrideLevelEnum.Env,
                defaultValue: "true"
            );

            var expectedResponse = new
            {
                code = "SUCCESS",
                message = "Feature flag created successfully"
            };

            MockServer.SetMockResponse("/api/v1/feature_flags", "POST", expectedResponse);
            var api = CreateFeatureFlagsApi();

            // Act
            var result = await api.CreateFeatureFlagAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("SUCCESS", result.Code);
        }

        [Fact]
        public async Task UpdateFeatureFlag_SerializesAndDeserializesCorrectly()
        {
            // Arrange
            var featureFlagKey = "test_feature";
            var request = new
            {
                name = "Updated Feature",
                description = "Updated description",
                type = "boolean",
                allow_override_level = "env",
                default_value = "false"
            };

            var expectedResponse = new
            {
                code = "SUCCESS",
                message = "Feature flag updated successfully"
            };

            // Set for both PUT (actual method) and PATCH (in case)
            MockServer.SetMockResponse($"/api/v1/feature_flags/{featureFlagKey}", "PUT", expectedResponse);
            MockServer.SetMockResponse($"/api/v1/feature_flags/{featureFlagKey}", "PATCH", expectedResponse);
            var api = CreateFeatureFlagsApi();

            // Act
            var result = await api.UpdateFeatureFlagAsync(
                featureFlagKey,
                "Updated Feature",
                "Updated description",
                "boolean",
                "env",
                "false"
            );

            // Assert
            Assert.NotNull(result);
            Assert.Equal("SUCCESS", result.Code);
        }
    }
}

