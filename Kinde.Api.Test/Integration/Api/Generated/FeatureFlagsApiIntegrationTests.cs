using System;
using System.Threading.Tasks;
using Kinde.Api.Api;
using Kinde.Api.Model;
using Kinde.Api.Test.Integration;
using Xunit;
using Xunit.Abstractions;

namespace Kinde.Api.Test.Integration.Api.Generated
{
    /// <summary>
    /// Auto-generated integration tests for FeatureFlagsApi with both mock and real API support
    /// </summary>
    public class FeatureFlagsApiIntegrationTests : BaseIntegrationTest, IClassFixture<TestResourceFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly TestResourceFixture _fixture;

        public FeatureFlagsApiIntegrationTests(ITestOutputHelper output, TestResourceFixture fixture) : base()
        {
            _output = output;
            _fixture = fixture;
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task CreateFeatureFlag_Mock_Test()
        {
            // Arrange
            if (UseRealApi)
            {
                _output.WriteLine("Skipping Mock test - using real API");
                return;
            }

            // Act & Assert
            try
            {
                var mockHandler = GetMockHandler();
                if (mockHandler == null)
                {
                    _output.WriteLine("Mock handler not available");
                    return;
                }

                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("POST", "/api/v1/feature_flags", mockResponse);
                var request = new CreateFeatureFlagRequest(name: "test-name", key: "test-key", type: CreateFeatureFlagRequest.TypeEnum.Str, defaultValue: "test-default_value");

                var api = CreateApi((client, config) => new FeatureFlagsApi(client, config));

                // Act
                var response = await api.CreateFeatureFlagAsync(request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateFeatureFlag test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task CreateFeatureFlag_Real_Test()
        {
            // Arrange
            if (!UseRealApi)
            {
                _output.WriteLine("Skipping Real test - using mocks");
                return;
            }

            // Act & Assert
            try
            {
                var request = new CreateFeatureFlagRequest(name: "test-name", key: "test-key", type: CreateFeatureFlagRequest.TypeEnum.Str, defaultValue: "test-default_value");

                var api = CreateApi((client, config) => new FeatureFlagsApi(client, config));

                var response = await api.CreateFeatureFlagAsync(request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateFeatureFlag test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteFeatureFlag_Mock_Test()
        {
            // Arrange
            if (UseRealApi)
            {
                _output.WriteLine("Skipping Mock test - using real API");
                return;
            }

            // Act & Assert
            try
            {
                var mockHandler = GetMockHandler();
                if (mockHandler == null)
                {
                    _output.WriteLine("Mock handler not available");
                    return;
                }

                var feature_flag_key = "test-feature_flag_key";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("DELETE", $"/api/v1/feature_flags/{feature_flag_key}", mockResponse);

                var api = CreateApi((client, config) => new FeatureFlagsApi(client, config));

                // Act
                var response = await api.DeleteFeatureFlagAsync(feature_flag_key);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteFeatureFlag test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task DeleteFeatureFlag_Real_Test()
        {
            // Arrange
            if (!UseRealApi)
            {
                _output.WriteLine("Skipping Real test - using mocks");
                return;
            }

            // Act & Assert
            try
            {
                // WARNING: Real API test - This operation requires existing feature_flag_key
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // WARNING: Using placeholder feature_flag_key - test will likely fail without real resource ID
                var feature_flag_key = "test-feature_flag_key";

                var api = CreateApi((client, config) => new FeatureFlagsApi(client, config));

                var response = await api.DeleteFeatureFlagAsync(feature_flag_key);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteFeatureFlag test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateFeatureFlag_Mock_Test()
        {
            // Arrange
            if (UseRealApi)
            {
                _output.WriteLine("Skipping Mock test - using real API");
                return;
            }

            // Act & Assert
            try
            {
                var mockHandler = GetMockHandler();
                if (mockHandler == null)
                {
                    _output.WriteLine("Mock handler not available");
                    return;
                }

                var feature_flag_key = "test-feature_flag_key";
                var name = "name";
                var description = "description";
                var type = "type";
                var allow_override_level = "allow_override_level";
                var default_value = "default_value";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("PUT", $"/api/v1/feature_flags/{feature_flag_key}", mockResponse);

                var api = CreateApi((client, config) => new FeatureFlagsApi(client, config));

                // Act
                var response = await api.UpdateFeatureFlagAsync(feature_flag_key, name, description, type, allow_override_level, default_value);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateFeatureFlag test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task UpdateFeatureFlag_Real_Test()
        {
            // Arrange
            if (!UseRealApi)
            {
                _output.WriteLine("Skipping Real test - using mocks");
                return;
            }

            // Act & Assert
            try
            {
                // WARNING: Real API test - This operation requires existing feature_flag_key
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // WARNING: Using placeholder feature_flag_key - test will likely fail without real resource ID
                var feature_flag_key = "test-feature_flag_key";
                var name = "name";
                var description = "description";
                var type = "type";
                var allow_override_level = "allow_override_level";
                var default_value = "default_value";

                var api = CreateApi((client, config) => new FeatureFlagsApi(client, config));

                var response = await api.UpdateFeatureFlagAsync(feature_flag_key, name, description, type, allow_override_level, default_value);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateFeatureFlag test: {ex.Message}");
                throw;
            }
        }

    }
}
