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
    /// Auto-generated integration tests for CallbacksApi with both mock and real API support
    /// </summary>
    public class CallbacksApiIntegrationTests : BaseIntegrationTest, IClassFixture<TestResourceFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly TestResourceFixture _fixture;

        public CallbacksApiIntegrationTests(ITestOutputHelper output, TestResourceFixture fixture) : base()
        {
            _output = output;
            _fixture = fixture;
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetCallbackURLs_Mock_Test()
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

                var app_id = "test-app_id";
                var mockResponse = new RedirectCallbackUrls();
                mockHandler.AddResponse("GET", $"/api/v1/applications/{app_id}/auth_redirect_urls", mockResponse);

                var api = CreateApi((client, config) => new CallbacksApi(client, config));

                // Act
                var response = await api.GetCallbackURLsAsync(app_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetCallbackURLs test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetCallbackURLs_Real_Test()
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
                var app_id = _fixture.ApplicationId;

                var api = CreateApi((client, config) => new CallbacksApi(client, config));

                var response = await api.GetCallbackURLsAsync(app_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetCallbackURLs test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task AddRedirectCallbackURLs_Mock_Test()
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

                var app_id = "test-app_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("POST", $"/api/v1/applications/{app_id}/auth_redirect_urls", mockResponse);
                var request = new ReplaceRedirectCallbackURLsRequest();

                var api = CreateApi((client, config) => new CallbacksApi(client, config));

                // Act
                var response = await api.AddRedirectCallbackURLsAsync(app_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in AddRedirectCallbackURLs test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task AddRedirectCallbackURLs_Real_Test()
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
                var app_id = _fixture.ApplicationId;
                var request = new ReplaceRedirectCallbackURLsRequest();

                var api = CreateApi((client, config) => new CallbacksApi(client, config));

                var response = await api.AddRedirectCallbackURLsAsync(app_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in AddRedirectCallbackURLs test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task ReplaceRedirectCallbackURLs_Mock_Test()
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

                var app_id = "test-app_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("PUT", $"/api/v1/applications/{app_id}/auth_redirect_urls", mockResponse);
                var request = new ReplaceRedirectCallbackURLsRequest();

                var api = CreateApi((client, config) => new CallbacksApi(client, config));

                // Act
                var response = await api.ReplaceRedirectCallbackURLsAsync(app_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in ReplaceRedirectCallbackURLs test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task ReplaceRedirectCallbackURLs_Real_Test()
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
                // WARNING: Real API test - This operation requires existing app_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: ApplicationId
                var app_id = _fixture.ApplicationId;
                var request = new ReplaceRedirectCallbackURLsRequest();

                var api = CreateApi((client, config) => new CallbacksApi(client, config));

                var response = await api.ReplaceRedirectCallbackURLsAsync(app_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in ReplaceRedirectCallbackURLs test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteCallbackURLs_Mock_Test()
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

                var app_id = "test-app_id";
                var urls = "urls";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("DELETE", $"/api/v1/applications/{app_id}/auth_redirect_urls", mockResponse);

                var api = CreateApi((client, config) => new CallbacksApi(client, config));

                // Act
                var response = await api.DeleteCallbackURLsAsync(app_id, urls);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteCallbackURLs test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task DeleteCallbackURLs_Real_Test()
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
                // WARNING: Real API test - This operation requires existing app_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: ApplicationId
                var app_id = _fixture.ApplicationId;
                var urls = "urls";

                var api = CreateApi((client, config) => new CallbacksApi(client, config));

                var response = await api.DeleteCallbackURLsAsync(app_id, urls);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteCallbackURLs test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetLogoutURLs_Mock_Test()
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

                var app_id = "test-app_id";
                var mockResponse = new LogoutRedirectUrls();
                mockHandler.AddResponse("GET", $"/api/v1/applications/{app_id}/auth_logout_urls", mockResponse);

                var api = CreateApi((client, config) => new CallbacksApi(client, config));

                // Act
                var response = await api.GetLogoutURLsAsync(app_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetLogoutURLs test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetLogoutURLs_Real_Test()
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
                var app_id = _fixture.ApplicationId;

                var api = CreateApi((client, config) => new CallbacksApi(client, config));

                var response = await api.GetLogoutURLsAsync(app_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetLogoutURLs test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task AddLogoutRedirectURLs_Mock_Test()
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

                var app_id = "test-app_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("POST", $"/api/v1/applications/{app_id}/auth_logout_urls", mockResponse);
                var request = new ReplaceLogoutRedirectURLsRequest();

                var api = CreateApi((client, config) => new CallbacksApi(client, config));

                // Act
                var response = await api.AddLogoutRedirectURLsAsync(app_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in AddLogoutRedirectURLs test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task AddLogoutRedirectURLs_Real_Test()
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
                var app_id = _fixture.ApplicationId;
                var request = new ReplaceLogoutRedirectURLsRequest();

                var api = CreateApi((client, config) => new CallbacksApi(client, config));

                var response = await api.AddLogoutRedirectURLsAsync(app_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in AddLogoutRedirectURLs test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task ReplaceLogoutRedirectURLs_Mock_Test()
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

                var app_id = "test-app_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("PUT", $"/api/v1/applications/{app_id}/auth_logout_urls", mockResponse);
                var request = new ReplaceLogoutRedirectURLsRequest();

                var api = CreateApi((client, config) => new CallbacksApi(client, config));

                // Act
                var response = await api.ReplaceLogoutRedirectURLsAsync(app_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in ReplaceLogoutRedirectURLs test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task ReplaceLogoutRedirectURLs_Real_Test()
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
                // WARNING: Real API test - This operation requires existing app_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: ApplicationId
                var app_id = _fixture.ApplicationId;
                var request = new ReplaceLogoutRedirectURLsRequest();

                var api = CreateApi((client, config) => new CallbacksApi(client, config));

                var response = await api.ReplaceLogoutRedirectURLsAsync(app_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in ReplaceLogoutRedirectURLs test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteLogoutURLs_Mock_Test()
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

                var app_id = "test-app_id";
                var urls = "urls";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("DELETE", $"/api/v1/applications/{app_id}/auth_logout_urls", mockResponse);

                var api = CreateApi((client, config) => new CallbacksApi(client, config));

                // Act
                var response = await api.DeleteLogoutURLsAsync(app_id, urls);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteLogoutURLs test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task DeleteLogoutURLs_Real_Test()
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
                // WARNING: Real API test - This operation requires existing app_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: ApplicationId
                var app_id = _fixture.ApplicationId;
                var urls = "urls";

                var api = CreateApi((client, config) => new CallbacksApi(client, config));

                var response = await api.DeleteLogoutURLsAsync(app_id, urls);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteLogoutURLs test: {ex.Message}");
                throw;
            }
        }

    }
}
