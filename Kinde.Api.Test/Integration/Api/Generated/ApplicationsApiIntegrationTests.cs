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
    /// Auto-generated integration tests for ApplicationsApi with both mock and real API support
    /// </summary>
    public class ApplicationsApiIntegrationTests : BaseIntegrationTest, IClassFixture<TestResourceFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly TestResourceFixture _fixture;

        public ApplicationsApiIntegrationTests(ITestOutputHelper output, TestResourceFixture fixture) : base()
        {
            _output = output;
            _fixture = fixture;
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetApplications_Mock_Test()
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

                var mockResponse = new GetApplicationsResponse();
                mockHandler.AddResponse("GET", "/api/v1/applications", mockResponse);

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                // Act
                var response = await api.GetApplicationsAsync();

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetApplications test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetApplications_Real_Test()
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
                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                var response = await api.GetApplicationsAsync();

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetApplications test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task CreateApplication_Mock_Test()
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

                var mockResponse = new CreateApplicationResponse();
                mockHandler.AddResponse("POST", "/api/v1/applications", mockResponse);
                var request = new CreateApplicationRequest(name: "test-name", type: CreateApplicationRequest.TypeEnum.Reg);

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                // Act
                var response = await api.CreateApplicationAsync(request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateApplication test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task CreateApplication_Real_Test()
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
                var request = new CreateApplicationRequest(name: "test-name", type: CreateApplicationRequest.TypeEnum.Reg);

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                var response = await api.CreateApplicationAsync(request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateApplication test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetApplication_Mock_Test()
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

                var application_id = "test-application_id";
                var mockResponse = new GetApplicationResponse();
                mockHandler.AddResponse("GET", $"/api/v1/applications/{application_id}", mockResponse);

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                // Act
                var response = await api.GetApplicationAsync(application_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetApplication test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetApplication_Real_Test()
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
                // Using test resource from fixture: ApplicationId
                var application_id = _fixture.ApplicationId;

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                var response = await api.GetApplicationAsync(application_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetApplication test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateApplication_Mock_Test()
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

                var application_id = "test-application_id";
                var mockResponse = new { };
                mockHandler.AddResponse("PATCH", $"/api/v1/applications/{application_id}", mockResponse);
                var request = new UpdateApplicationRequest();

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                // Act
                await api.UpdateApplicationAsync(application_id, request);
                // Void method - no response to check
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateApplication test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task UpdateApplication_Real_Test()
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
                // WARNING: Real API test - This operation requires existing application_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: ApplicationId
                var application_id = _fixture.ApplicationId;
                var request = new UpdateApplicationRequest();

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                await api.UpdateApplicationAsync(application_id, request);
                // Void method - no response to check
                _output.WriteLine($"Void method completed successfully");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateApplication test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteApplication_Mock_Test()
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

                var application_id = "test-application_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("DELETE", $"/api/v1/applications/{application_id}", mockResponse);

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                // Act
                await api.DeleteApplicationAsync(application_id);
                // Void method - no response to check
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteApplication test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task DeleteApplication_Real_Test()
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
                // WARNING: Real API test - This operation requires existing application_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: ApplicationId
                var application_id = _fixture.ApplicationId;

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                await api.DeleteApplicationAsync(application_id);
                // Void method - no response to check
                _output.WriteLine($"Void method completed successfully");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteApplication test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetApplicationConnections_Mock_Test()
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

                var application_id = "test-application_id";
                var mockResponse = new GetConnectionsResponse();
                mockHandler.AddResponse("GET", $"/api/v1/applications/{application_id}/connections", mockResponse);

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                // Act
                var response = await api.GetApplicationConnectionsAsync(application_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetApplicationConnections test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetApplicationConnections_Real_Test()
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
                // Using test resource from fixture: ApplicationId
                var application_id = _fixture.ApplicationId;

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                var response = await api.GetApplicationConnectionsAsync(application_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetApplicationConnections test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task EnableConnection_Mock_Test()
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

                var application_id = "test-application_id";
                var connection_id = "test-connection_id";
                var mockResponse = new { };
                mockHandler.AddResponse("POST", $"/api/v1/applications/{application_id}/connections/{connection_id}", mockResponse);

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                // Act
                await api.EnableConnectionAsync(application_id, connection_id);
                // Void method - no response to check
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in EnableConnection test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task EnableConnection_Real_Test()
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
                // Using test resource from fixture: ApplicationId
                var application_id = _fixture.ApplicationId;
                var connection_id = "test-connection_id";

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                await api.EnableConnectionAsync(application_id, connection_id);
                // Void method - no response to check
                _output.WriteLine($"Void method completed successfully");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in EnableConnection test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task RemoveConnection_Mock_Test()
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

                var application_id = "test-application_id";
                var connection_id = "test-connection_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("DELETE", $"/api/v1/applications/{application_id}/connections/{connection_id}", mockResponse);

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                // Act
                var response = await api.RemoveConnectionAsync(application_id, connection_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in RemoveConnection test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task RemoveConnection_Real_Test()
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
                // WARNING: Real API test - This operation requires existing application_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: ApplicationId
                var application_id = _fixture.ApplicationId;
                // WARNING: Using placeholder connection_id - test will likely fail without real resource ID
                var connection_id = "test-connection_id";

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                var response = await api.RemoveConnectionAsync(application_id, connection_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in RemoveConnection test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetApplicationPropertyValues_Mock_Test()
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

                var application_id = "test-application_id";
                var mockResponse = new GetPropertyValuesResponse();
                mockHandler.AddResponse("GET", $"/api/v1/applications/{application_id}/properties", mockResponse);

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                // Act
                var response = await api.GetApplicationPropertyValuesAsync(application_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetApplicationPropertyValues test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetApplicationPropertyValues_Real_Test()
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
                // Using test resource from fixture: ApplicationId
                var application_id = _fixture.ApplicationId;

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                var response = await api.GetApplicationPropertyValuesAsync(application_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetApplicationPropertyValues test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateApplicationsProperty_Mock_Test()
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

                var application_id = "test-application_id";
                var property_key = "test-property_key";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("PUT", $"/api/v1/applications/{application_id}/properties/{property_key}", mockResponse);
                var request = new UpdateApplicationsPropertyRequest(value: new UpdateApplicationsPropertyRequestValue("test-value"));

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                // Act
                var response = await api.UpdateApplicationsPropertyAsync(application_id, property_key, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateApplicationsProperty test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task UpdateApplicationsProperty_Real_Test()
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
                // WARNING: Real API test - This operation requires existing application_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: ApplicationId
                var application_id = _fixture.ApplicationId;
                // Using test resource from fixture: PropertyKey
                var property_key = _fixture.PropertyKey;
                var request = new UpdateApplicationsPropertyRequest(value: new UpdateApplicationsPropertyRequestValue("test-value"));

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                var response = await api.UpdateApplicationsPropertyAsync(application_id, property_key, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateApplicationsProperty test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateApplicationTokens_Mock_Test()
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

                var application_id = "test-application_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("PATCH", $"/api/v1/applications/{application_id}/tokens", mockResponse);
                var request = new UpdateApplicationTokensRequest();

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                // Act
                var response = await api.UpdateApplicationTokensAsync(application_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateApplicationTokens test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task UpdateApplicationTokens_Real_Test()
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
                // WARNING: Real API test - This operation requires existing application_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: ApplicationId
                var application_id = _fixture.ApplicationId;
                var request = new UpdateApplicationTokensRequest();

                var api = CreateApi((client, config) => new ApplicationsApi(client, config));

                var response = await api.UpdateApplicationTokensAsync(application_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateApplicationTokens test: {ex.Message}");
                throw;
            }
        }

    }
}
