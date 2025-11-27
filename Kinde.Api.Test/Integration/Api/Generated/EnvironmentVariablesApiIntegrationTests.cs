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
    /// Auto-generated integration tests for EnvironmentVariablesApi with both mock and real API support
    /// </summary>
    public class EnvironmentVariablesApiIntegrationTests : BaseIntegrationTest, IClassFixture<TestResourceFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly TestResourceFixture _fixture;

        public EnvironmentVariablesApiIntegrationTests(ITestOutputHelper output, TestResourceFixture fixture) : base()
        {
            _output = output;
            _fixture = fixture;
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetEnvironmentVariables_Mock_Test()
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

                var mockResponse = new GetEnvironmentVariablesResponse();
                mockHandler.AddResponse("GET", "/api/v1/environment_variables", mockResponse);

                var api = CreateApi((client, config) => new EnvironmentVariablesApi(client, config));

                // Act
                var response = await api.GetEnvironmentVariablesAsync();

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetEnvironmentVariables test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetEnvironmentVariables_Real_Test()
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
                var api = CreateApi((client, config) => new EnvironmentVariablesApi(client, config));

                var response = await api.GetEnvironmentVariablesAsync();

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetEnvironmentVariables test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task CreateEnvironmentVariable_Mock_Test()
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

                var mockResponse = new CreateEnvironmentVariableResponse();
                mockHandler.AddResponse("POST", "/api/v1/environment_variables", mockResponse);
                var request = new CreateEnvironmentVariableRequest(key: "test-key", value: "test-value");

                var api = CreateApi((client, config) => new EnvironmentVariablesApi(client, config));

                // Act
                var response = await api.CreateEnvironmentVariableAsync(request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateEnvironmentVariable test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task CreateEnvironmentVariable_Real_Test()
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
                var request = new CreateEnvironmentVariableRequest(key: "test-key", value: "test-value");

                var api = CreateApi((client, config) => new EnvironmentVariablesApi(client, config));

                var response = await api.CreateEnvironmentVariableAsync(request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateEnvironmentVariable test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetEnvironmentVariable_Mock_Test()
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

                var variable_id = "test-variable_id";
                var mockResponse = new GetEnvironmentVariableResponse();
                mockHandler.AddResponse("GET", $"/api/v1/environment_variables/{variable_id}", mockResponse);

                var api = CreateApi((client, config) => new EnvironmentVariablesApi(client, config));

                // Act
                var response = await api.GetEnvironmentVariableAsync(variable_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetEnvironmentVariable test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetEnvironmentVariable_Real_Test()
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
                // Note: This test uses a placeholder variable_id and may fail if the resource doesn't exist
                var variable_id = "test-variable_id";

                var api = CreateApi((client, config) => new EnvironmentVariablesApi(client, config));

                var response = await api.GetEnvironmentVariableAsync(variable_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetEnvironmentVariable test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateEnvironmentVariable_Mock_Test()
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

                var variable_id = "test-variable_id";
                var mockResponse = new UpdateEnvironmentVariableResponse();
                mockHandler.AddResponse("PATCH", $"/api/v1/environment_variables/{variable_id}", mockResponse);
                var request = new UpdateEnvironmentVariableRequest();

                var api = CreateApi((client, config) => new EnvironmentVariablesApi(client, config));

                // Act
                var response = await api.UpdateEnvironmentVariableAsync(variable_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateEnvironmentVariable test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task UpdateEnvironmentVariable_Real_Test()
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
                // WARNING: Real API test - This operation requires existing variable_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // WARNING: Using placeholder variable_id - test will likely fail without real resource ID
                var variable_id = "test-variable_id";
                var request = new UpdateEnvironmentVariableRequest();

                var api = CreateApi((client, config) => new EnvironmentVariablesApi(client, config));

                var response = await api.UpdateEnvironmentVariableAsync(variable_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateEnvironmentVariable test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteEnvironmentVariable_Mock_Test()
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

                var variable_id = "test-variable_id";
                var mockResponse = new DeleteEnvironmentVariableResponse();
                mockHandler.AddResponse("DELETE", $"/api/v1/environment_variables/{variable_id}", mockResponse);

                var api = CreateApi((client, config) => new EnvironmentVariablesApi(client, config));

                // Act
                var response = await api.DeleteEnvironmentVariableAsync(variable_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteEnvironmentVariable test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task DeleteEnvironmentVariable_Real_Test()
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
                // WARNING: Real API test - This operation requires existing variable_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // WARNING: Using placeholder variable_id - test will likely fail without real resource ID
                var variable_id = "test-variable_id";

                var api = CreateApi((client, config) => new EnvironmentVariablesApi(client, config));

                var response = await api.DeleteEnvironmentVariableAsync(variable_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteEnvironmentVariable test: {ex.Message}");
                throw;
            }
        }

    }
}
