using System;
using System.Threading.Tasks;
using Kinde.Api.Api;
using Kinde.Api.Model;
using Kinde.Api.Client;
using Kinde.Api.Test.Integration;
using Xunit;
using Xunit.Abstractions;

namespace Kinde.Api.Test.Integration.Api.Generated
{
    /// <summary>
    /// Auto-generated integration tests for EnvironmentsApi with both mock and real API support
    /// </summary>
    public class EnvironmentsApiIntegrationTests : BaseIntegrationTest, IClassFixture<TestResourceFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly TestResourceFixture _fixture;

        public EnvironmentsApiIntegrationTests(ITestOutputHelper output, TestResourceFixture fixture) : base()
        {
            _output = output;
            _fixture = fixture;
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetEnvironment_Mock_Test()
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

                var mockResponse = new GetEnvironmentResponse();
                mockHandler.AddResponse("GET", "/api/v1/environment", mockResponse);

                var api = CreateApi((client, config) => new EnvironmentsApi(client, config));

                // Act
                var response = await api.GetEnvironmentAsync();

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetEnvironment test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetEnvironment_Real_Test()
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
                var api = CreateApi((client, config) => new EnvironmentsApi(client, config));

                var response = await api.GetEnvironmentAsync();

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetEnvironment test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteEnvironementFeatureFlagOverrides_Mock_Test()
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
                mockHandler.AddResponse("DELETE", "/api/v1/environment/feature_flags", mockResponse);

                var api = CreateApi((client, config) => new EnvironmentsApi(client, config));

                // Act
                var response = await api.DeleteEnvironementFeatureFlagOverridesAsync();

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteEnvironementFeatureFlagOverrides test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task DeleteEnvironementFeatureFlagOverrides_Real_Test()
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
                var api = CreateApi((client, config) => new EnvironmentsApi(client, config));

                var response = await api.DeleteEnvironementFeatureFlagOverridesAsync();

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteEnvironementFeatureFlagOverrides test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetEnvironementFeatureFlags_Mock_Test()
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

                var mockResponse = new GetEnvironmentFeatureFlagsResponse();
                mockHandler.AddResponse("GET", "/api/v1/environment/feature_flags", mockResponse);

                var api = CreateApi((client, config) => new EnvironmentsApi(client, config));

                // Act
                var response = await api.GetEnvironementFeatureFlagsAsync();

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetEnvironementFeatureFlags test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetEnvironementFeatureFlags_Real_Test()
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
                var api = CreateApi((client, config) => new EnvironmentsApi(client, config));

                var response = await api.GetEnvironementFeatureFlagsAsync();

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetEnvironementFeatureFlags test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteEnvironementFeatureFlagOverride_Mock_Test()
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
                mockHandler.AddResponse("DELETE", $"/api/v1/environment/feature_flags/{feature_flag_key}", mockResponse);

                var api = CreateApi((client, config) => new EnvironmentsApi(client, config));

                // Act
                var response = await api.DeleteEnvironementFeatureFlagOverrideAsync(feature_flag_key);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteEnvironementFeatureFlagOverride test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task DeleteEnvironementFeatureFlagOverride_Real_Test()
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

                var api = CreateApi((client, config) => new EnvironmentsApi(client, config));

                var response = await api.DeleteEnvironementFeatureFlagOverrideAsync(feature_flag_key);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteEnvironementFeatureFlagOverride test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateEnvironementFeatureFlagOverride_Mock_Test()
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
                mockHandler.AddResponse("PATCH", $"/api/v1/environment/feature_flags/{feature_flag_key}", mockResponse);
                var request = new UpdateEnvironementFeatureFlagOverrideRequest(value: "test-value");

                var api = CreateApi((client, config) => new EnvironmentsApi(client, config));

                // Act
                var response = await api.UpdateEnvironementFeatureFlagOverrideAsync(feature_flag_key, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateEnvironementFeatureFlagOverride test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task UpdateEnvironementFeatureFlagOverride_Real_Test()
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
                var request = new UpdateEnvironementFeatureFlagOverrideRequest(value: "test-value");

                var api = CreateApi((client, config) => new EnvironmentsApi(client, config));

                var response = await api.UpdateEnvironementFeatureFlagOverrideAsync(feature_flag_key, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateEnvironementFeatureFlagOverride test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task ReadLogo_Mock_Test()
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

                var mockResponse = new ReadEnvLogoResponse();
                mockHandler.AddResponse("GET", "/api/v1/environment/logos", mockResponse);

                var api = CreateApi((client, config) => new EnvironmentsApi(client, config));

                // Act
                var response = await api.ReadLogoAsync();

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in ReadLogo test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task ReadLogo_Real_Test()
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
                var api = CreateApi((client, config) => new EnvironmentsApi(client, config));

                var response = await api.ReadLogoAsync();

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in ReadLogo test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task AddLogo_Mock_Test()
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

                var type = "test-type";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("PUT", $"/api/v1/environment/logos/{type}", mockResponse);
                var logo = new FileParameter("test-file.txt", new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes("test file content")));

                var api = CreateApi((client, config) => new EnvironmentsApi(client, config));

                // Act
                var response = await api.AddLogoAsync(type, logo);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in AddLogo test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task AddLogo_Real_Test()
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
                // Logo type must be "light" or "dark" per API requirements
                var type = "light";
                
                // Create a minimal valid 1x1 PNG image
                byte[] pngBytes = new byte[]
                {
                    0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A,
                    0x00, 0x00, 0x00, 0x0D, 0x49, 0x48, 0x44, 0x52,
                    0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01,
                    0x08, 0x02, 0x00, 0x00, 0x00, 0x90, 0x77, 0x53, 0xDE,
                    0x00, 0x00, 0x00, 0x0C, 0x49, 0x44, 0x41, 0x54,
                    0x08, 0xD7, 0x63, 0xF8, 0xCF, 0xC0, 0x00, 0x00, 0x01, 0x01, 0x01, 0x00,
                    0x18, 0xDD, 0x8D, 0xB4,
                    0x00, 0x00, 0x00, 0x00, 0x49, 0x45, 0x4E, 0x44, 0xAE, 0x42, 0x60, 0x82
                };
                var logo = new FileParameter("test-logo.png", "image/png", new System.IO.MemoryStream(pngBytes));

                var api = CreateApi((client, config) => new EnvironmentsApi(client, config));

                var response = await api.AddLogoAsync(type, logo);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in AddLogo test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteLogo_Mock_Test()
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

                var type = "test-type";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("DELETE", $"/api/v1/environment/logos/{type}", mockResponse);

                var api = CreateApi((client, config) => new EnvironmentsApi(client, config));

                // Act
                await api.DeleteLogoAsync(type);
                // Void method - no response to check
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteLogo test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task DeleteLogo_Real_Test()
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
                // Logo type must be "light" or "dark" per API requirements
                var type = "light";

                var api = CreateApi((client, config) => new EnvironmentsApi(client, config));

                // First, add a logo so we have something to delete
                byte[] pngBytes = new byte[]
                {
                    0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A,
                    0x00, 0x00, 0x00, 0x0D, 0x49, 0x48, 0x44, 0x52,
                    0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01,
                    0x08, 0x02, 0x00, 0x00, 0x00, 0x90, 0x77, 0x53, 0xDE,
                    0x00, 0x00, 0x00, 0x0C, 0x49, 0x44, 0x41, 0x54,
                    0x08, 0xD7, 0x63, 0xF8, 0xCF, 0xC0, 0x00, 0x00, 0x01, 0x01, 0x01, 0x00,
                    0x18, 0xDD, 0x8D, 0xB4,
                    0x00, 0x00, 0x00, 0x00, 0x49, 0x45, 0x4E, 0x44, 0xAE, 0x42, 0x60, 0x82
                };
                var logo = new FileParameter("test-logo.png", "image/png", new System.IO.MemoryStream(pngBytes));
                
                try
                {
                    await api.AddLogoAsync(type, logo);
                    _output.WriteLine($"Added logo of type '{type}' for delete test");
                }
                catch (Exception)
                {
                    // Logo might already exist or adding might fail - proceed with delete attempt
                    _output.WriteLine("Note: Could not add logo (may already exist or not supported)");
                }

                await api.DeleteLogoAsync(type);
                // Void method - no response to check
                _output.WriteLine($"Void method completed successfully");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteLogo test: {ex.Message}");
                throw;
            }
        }

    }
}
