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
    /// Auto-generated integration tests for APIsApi with both mock and real API support
    /// </summary>
    public class APIsApiIntegrationTests : BaseIntegrationTest, IClassFixture<TestResourceFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly TestResourceFixture _fixture;

        public APIsApiIntegrationTests(ITestOutputHelper output, TestResourceFixture fixture) : base()
        {
            _output = output;
            _fixture = fixture;
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetAPIs_Mock_Test()
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

                var mockResponse = new GetApisResponse();
                mockHandler.AddResponse("GET", "/api/v1/apis", mockResponse);

                var api = CreateApi((client, config) => new APIsApi(client, config));

                // Act
                var response = await api.GetAPIsAsync();

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetAPIs test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetAPIs_Real_Test()
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
                var api = CreateApi((client, config) => new APIsApi(client, config));

                var response = await api.GetAPIsAsync();

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetAPIs test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task AddAPIs_Mock_Test()
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

                var mockResponse = new CreateApisResponse();
                mockHandler.AddResponse("POST", "/api/v1/apis", mockResponse);
                var request = new AddAPIsRequest(name: "test-name", audience: "test-audience");

                var api = CreateApi((client, config) => new APIsApi(client, config));

                // Act
                var response = await api.AddAPIsAsync(request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in AddAPIs test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task AddAPIs_Real_Test()
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
                var request = new AddAPIsRequest(name: "test-name", audience: "test-audience");

                var api = CreateApi((client, config) => new APIsApi(client, config));

                var response = await api.AddAPIsAsync(request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in AddAPIs test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetAPI_Mock_Test()
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

                var api_id = "test-api_id";
                var mockResponse = new GetApiResponse();
                mockHandler.AddResponse("GET", $"/api/v1/apis/{api_id}", mockResponse);

                var api = CreateApi((client, config) => new APIsApi(client, config));

                // Act
                var response = await api.GetAPIAsync(api_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetAPI test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetAPI_Real_Test()
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
                // Note: This test uses a placeholder api_id and may fail if the resource doesn't exist
                var api_id = "test-api_id";

                var api = CreateApi((client, config) => new APIsApi(client, config));

                var response = await api.GetAPIAsync(api_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetAPI test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteAPI_Mock_Test()
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

                var api_id = "test-api_id";
                var mockResponse = new DeleteApiResponse();
                mockHandler.AddResponse("DELETE", $"/api/v1/apis/{api_id}", mockResponse);

                var api = CreateApi((client, config) => new APIsApi(client, config));

                // Act
                var response = await api.DeleteAPIAsync(api_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteAPI test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task DeleteAPI_Real_Test()
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
                // WARNING: Real API test - This operation requires existing api_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // WARNING: Using placeholder api_id - test will likely fail without real resource ID
                var api_id = "test-api_id";

                var api = CreateApi((client, config) => new APIsApi(client, config));

                var response = await api.DeleteAPIAsync(api_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteAPI test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetAPIScopes_Mock_Test()
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

                var api_id = "test-api_id";
                var mockResponse = new GetApiScopesResponse();
                mockHandler.AddResponse("GET", $"/api/v1/apis/{api_id}/scopes", mockResponse);

                var api = CreateApi((client, config) => new APIsApi(client, config));

                // Act
                var response = await api.GetAPIScopesAsync(api_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetAPIScopes test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetAPIScopes_Real_Test()
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
                // Note: This test uses a placeholder api_id and may fail if the resource doesn't exist
                var api_id = "test-api_id";

                var api = CreateApi((client, config) => new APIsApi(client, config));

                var response = await api.GetAPIScopesAsync(api_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetAPIScopes test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task AddAPIScope_Mock_Test()
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

                var api_id = "test-api_id";
                var mockResponse = new CreateApiScopesResponse();
                mockHandler.AddResponse("POST", $"/api/v1/apis/{api_id}/scopes", mockResponse);
                var request = new AddAPIScopeRequest(key: "test-key");

                var api = CreateApi((client, config) => new APIsApi(client, config));

                // Act
                var response = await api.AddAPIScopeAsync(api_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in AddAPIScope test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task AddAPIScope_Real_Test()
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
                var api_id = "test-api_id";
                var request = new AddAPIScopeRequest(key: "test-key");

                var api = CreateApi((client, config) => new APIsApi(client, config));

                var response = await api.AddAPIScopeAsync(api_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in AddAPIScope test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetAPIScope_Mock_Test()
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

                var api_id = "test-api_id";
                var scope_id = "test-scope_id";
                var mockResponse = new GetApiScopeResponse();
                mockHandler.AddResponse("GET", $"/api/v1/apis/{api_id}/scopes/{scope_id}", mockResponse);

                var api = CreateApi((client, config) => new APIsApi(client, config));

                // Act
                var response = await api.GetAPIScopeAsync(api_id, scope_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetAPIScope test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetAPIScope_Real_Test()
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
                // Note: This test uses a placeholder api_id and may fail if the resource doesn't exist
                var api_id = "test-api_id";
                // Note: This test uses a placeholder scope_id and may fail if the resource doesn't exist
                var scope_id = "test-scope_id";

                var api = CreateApi((client, config) => new APIsApi(client, config));

                var response = await api.GetAPIScopeAsync(api_id, scope_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetAPIScope test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateAPIScope_Mock_Test()
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

                var api_id = "test-api_id";
                var scope_id = "test-scope_id";
                var mockResponse = new { };
                mockHandler.AddResponse("PATCH", $"/api/v1/apis/{api_id}/scopes/{scope_id}", mockResponse);
                var request = new UpdateAPIScopeRequest();

                var api = CreateApi((client, config) => new APIsApi(client, config));

                // Act
                await api.UpdateAPIScopeAsync(api_id, scope_id, request);
                // Void method - no response to check
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateAPIScope test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task UpdateAPIScope_Real_Test()
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
                // WARNING: Real API test - This operation requires existing api_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // WARNING: Using placeholder api_id - test will likely fail without real resource ID
                var api_id = "test-api_id";
                // WARNING: Using placeholder scope_id - test will likely fail without real resource ID
                var scope_id = "test-scope_id";
                var request = new UpdateAPIScopeRequest();

                var api = CreateApi((client, config) => new APIsApi(client, config));

                await api.UpdateAPIScopeAsync(api_id, scope_id, request);
                // Void method - no response to check
                _output.WriteLine($"Void method completed successfully");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateAPIScope test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteAPIScope_Mock_Test()
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

                var api_id = "test-api_id";
                var scope_id = "test-scope_id";
                var mockResponse = new { };
                mockHandler.AddResponse("DELETE", $"/api/v1/apis/{api_id}/scopes/{scope_id}", mockResponse);

                var api = CreateApi((client, config) => new APIsApi(client, config));

                // Act
                await api.DeleteAPIScopeAsync(api_id, scope_id);
                // Void method - no response to check
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteAPIScope test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task DeleteAPIScope_Real_Test()
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
                // WARNING: Real API test - This operation requires existing api_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // WARNING: Using placeholder api_id - test will likely fail without real resource ID
                var api_id = "test-api_id";
                // WARNING: Using placeholder scope_id - test will likely fail without real resource ID
                var scope_id = "test-scope_id";

                var api = CreateApi((client, config) => new APIsApi(client, config));

                await api.DeleteAPIScopeAsync(api_id, scope_id);
                // Void method - no response to check
                _output.WriteLine($"Void method completed successfully");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteAPIScope test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateAPIApplications_Mock_Test()
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

                var api_id = "test-api_id";
                var mockResponse = new AuthorizeAppApiResponse();
                mockHandler.AddResponse("PATCH", $"/api/v1/apis/{api_id}/applications", mockResponse);
                var request = new UpdateAPIApplicationsRequest(applications: new System.Collections.Generic.List<UpdateAPIApplicationsRequestApplicationsInner>());

                var api = CreateApi((client, config) => new APIsApi(client, config));

                // Act
                var response = await api.UpdateAPIApplicationsAsync(api_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateAPIApplications test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task UpdateAPIApplications_Real_Test()
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
                // WARNING: Real API test - This operation requires existing api_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // WARNING: Using placeholder api_id - test will likely fail without real resource ID
                var api_id = "test-api_id";
                var request = new UpdateAPIApplicationsRequest(applications: new System.Collections.Generic.List<UpdateAPIApplicationsRequestApplicationsInner>());

                var api = CreateApi((client, config) => new APIsApi(client, config));

                var response = await api.UpdateAPIApplicationsAsync(api_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateAPIApplications test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task AddAPIApplicationScope_Mock_Test()
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

                var api_id = "test-api_id";
                var application_id = "test-application_id";
                var scope_id = "test-scope_id";
                var mockResponse = new { };
                mockHandler.AddResponse("POST", $"/api/v1/apis/{api_id}/applications/{application_id}/scopes/{scope_id}", mockResponse);

                var api = CreateApi((client, config) => new APIsApi(client, config));

                // Act
                await api.AddAPIApplicationScopeAsync(api_id, application_id, scope_id);
                // Void method - no response to check
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in AddAPIApplicationScope test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task AddAPIApplicationScope_Real_Test()
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
                var api_id = "test-api_id";
                // Using test resource from fixture: ApplicationId
                var application_id = _fixture.ApplicationId;
                var scope_id = "test-scope_id";

                var api = CreateApi((client, config) => new APIsApi(client, config));

                await api.AddAPIApplicationScopeAsync(api_id, application_id, scope_id);
                // Void method - no response to check
                _output.WriteLine($"Void method completed successfully");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in AddAPIApplicationScope test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteAPIApplicationScope_Mock_Test()
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

                var api_id = "test-api_id";
                var application_id = "test-application_id";
                var scope_id = "test-scope_id";
                var mockResponse = new { };
                mockHandler.AddResponse("DELETE", $"/api/v1/apis/{api_id}/applications/{application_id}/scopes/{scope_id}", mockResponse);

                var api = CreateApi((client, config) => new APIsApi(client, config));

                // Act
                await api.DeleteAPIAppliationScopeAsync(api_id, application_id, scope_id);
                // Void method - no response to check
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteAPIApplicationScope test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task DeleteAPIApplicationScope_Real_Test()
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
                // WARNING: Real API test - This operation requires existing api_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // WARNING: Using placeholder api_id - test will likely fail without real resource ID
                var api_id = "test-api_id";
                // Using test resource from fixture: ApplicationId
                var application_id = _fixture.ApplicationId;
                // WARNING: Using placeholder scope_id - test will likely fail without real resource ID
                var scope_id = "test-scope_id";

                var api = CreateApi((client, config) => new APIsApi(client, config));

                await api.DeleteAPIAppliationScopeAsync(api_id, application_id, scope_id);
                // Void method - no response to check
                _output.WriteLine($"Void method completed successfully");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteAPIApplicationScope test: {ex.Message}");
                throw;
            }
        }

    }
}
