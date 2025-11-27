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
    /// Auto-generated integration tests for IdentitiesApi with both mock and real API support
    /// </summary>
    public class IdentitiesApiIntegrationTests : BaseIntegrationTest, IClassFixture<TestResourceFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly TestResourceFixture _fixture;

        public IdentitiesApiIntegrationTests(ITestOutputHelper output, TestResourceFixture fixture) : base()
        {
            _output = output;
            _fixture = fixture;
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetIdentity_Mock_Test()
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

                var identity_id = "test-identity_id";
                var mockResponse = new Identity();
                mockHandler.AddResponse("GET", $"/api/v1/identities/{identity_id}", mockResponse);

                var api = CreateApi((client, config) => new IdentitiesApi(client, config));

                // Act
                var response = await api.GetIdentityAsync(identity_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetIdentity test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetIdentity_Real_Test()
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
                // Note: This test uses a placeholder identity_id and may fail if the resource doesn't exist
                var identity_id = "test-identity_id";

                var api = CreateApi((client, config) => new IdentitiesApi(client, config));

                var response = await api.GetIdentityAsync(identity_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetIdentity test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateIdentity_Mock_Test()
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

                var identity_id = "test-identity_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("PATCH", $"/api/v1/identities/{identity_id}", mockResponse);
                var request = new UpdateIdentityRequest();

                var api = CreateApi((client, config) => new IdentitiesApi(client, config));

                // Act
                var response = await api.UpdateIdentityAsync(identity_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateIdentity test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task UpdateIdentity_Real_Test()
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
                // WARNING: Real API test - This operation requires existing identity_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // WARNING: Using placeholder identity_id - test will likely fail without real resource ID
                var identity_id = "test-identity_id";
                var request = new UpdateIdentityRequest();

                var api = CreateApi((client, config) => new IdentitiesApi(client, config));

                var response = await api.UpdateIdentityAsync(identity_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateIdentity test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteIdentity_Mock_Test()
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

                var identity_id = "test-identity_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("DELETE", $"/api/v1/identities/{identity_id}", mockResponse);

                var api = CreateApi((client, config) => new IdentitiesApi(client, config));

                // Act
                var response = await api.DeleteIdentityAsync(identity_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteIdentity test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task DeleteIdentity_Real_Test()
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
                // WARNING: Real API test - This operation requires existing identity_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // WARNING: Using placeholder identity_id - test will likely fail without real resource ID
                var identity_id = "test-identity_id";

                var api = CreateApi((client, config) => new IdentitiesApi(client, config));

                var response = await api.DeleteIdentityAsync(identity_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteIdentity test: {ex.Message}");
                throw;
            }
        }

    }
}
