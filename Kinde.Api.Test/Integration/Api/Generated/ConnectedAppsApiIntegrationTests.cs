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
    /// Auto-generated integration tests for ConnectedAppsApi with both mock and real API support
    /// </summary>
    public class ConnectedAppsApiIntegrationTests : BaseIntegrationTest, IClassFixture<TestResourceFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly TestResourceFixture _fixture;

        public ConnectedAppsApiIntegrationTests(ITestOutputHelper output, TestResourceFixture fixture) : base()
        {
            _output = output;
            _fixture = fixture;
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetConnectedAppAuthUrl_Mock_Test()
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

                var key_code_ref = "key_code_ref";
                var mockResponse = new ConnectedAppsAuthUrl();
                mockHandler.AddResponse("GET", "/api/v1/connected_apps/auth_url", mockResponse);

                var api = CreateApi((client, config) => new ConnectedAppsApi(client, config));

                // Act
                var response = await api.GetConnectedAppAuthUrlAsync(key_code_ref);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetConnectedAppAuthUrl test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetConnectedAppAuthUrl_Real_Test()
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
                var key_code_ref = "key_code_ref";
                // Using test resource from fixture: UserId
                var user_id = _fixture.UserId;
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;

                var api = CreateApi((client, config) => new ConnectedAppsApi(client, config));

                var response = await api.GetConnectedAppAuthUrlAsync(key_code_ref, userId: user_id, orgCode: org_code);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetConnectedAppAuthUrl test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetConnectedAppToken_Mock_Test()
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

                var session_id = "session_id";
                var mockResponse = new ConnectedAppsAccessToken();
                mockHandler.AddResponse("GET", "/api/v1/connected_apps/token", mockResponse);

                var api = CreateApi((client, config) => new ConnectedAppsApi(client, config));

                // Act
                var response = await api.GetConnectedAppTokenAsync(session_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetConnectedAppToken test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetConnectedAppToken_Real_Test()
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
                var session_id = "session_id";

                var api = CreateApi((client, config) => new ConnectedAppsApi(client, config));

                var response = await api.GetConnectedAppTokenAsync(session_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetConnectedAppToken test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task RevokeConnectedAppToken_Mock_Test()
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

                var session_id = "session_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("POST", "/api/v1/connected_apps/revoke", mockResponse);

                var api = CreateApi((client, config) => new ConnectedAppsApi(client, config));

                // Act
                var response = await api.RevokeConnectedAppTokenAsync(session_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in RevokeConnectedAppToken test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task RevokeConnectedAppToken_Real_Test()
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
                var session_id = "session_id";

                var api = CreateApi((client, config) => new ConnectedAppsApi(client, config));

                var response = await api.RevokeConnectedAppTokenAsync(session_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in RevokeConnectedAppToken test: {ex.Message}");
                throw;
            }
        }

    }
}
