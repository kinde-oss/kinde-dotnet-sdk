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
    /// Auto-generated integration tests for OrganizationsApi with both mock and real API support
    /// </summary>
    public class OrganizationsApiIntegrationTests : BaseIntegrationTest, IClassFixture<TestResourceFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly TestResourceFixture _fixture;

        public OrganizationsApiIntegrationTests(ITestOutputHelper output, TestResourceFixture fixture) : base()
        {
            _output = output;
            _fixture = fixture;
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetOrganization_Mock_Test()
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

                var mockResponse = new GetOrganizationResponse();
                mockHandler.AddResponse("GET", "/api/v1/organization", mockResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.GetOrganizationAsync();

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetOrganization test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetOrganization_Real_Test()
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
                // Using test resource from fixture: OrganizationCode
                var code = _fixture.OrganizationCode;

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.GetOrganizationAsync(code: code);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetOrganization test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task CreateOrganization_Mock_Test()
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

                var mockResponse = new CreateOrganizationResponse();
                mockHandler.AddResponse("POST", "/api/v1/organization", mockResponse);
                var request = new CreateOrganizationRequest(name: "test-name");

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.CreateOrganizationAsync(request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateOrganization test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task CreateOrganization_Real_Test()
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
                var request = new CreateOrganizationRequest(name: "test-name");

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.CreateOrganizationAsync(request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateOrganization test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetOrganizations_Mock_Test()
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

                var mockResponse = new GetOrganizationsResponse();
                mockHandler.AddResponse("GET", "/api/v1/organizations", mockResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.GetOrganizationsAsync();

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetOrganizations test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetOrganizations_Real_Test()
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
                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.GetOrganizationsAsync();

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetOrganizations test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateOrganization_Mock_Test()
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

                var org_code = "test-org_code";
                var expand = "expand";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("PATCH", $"/api/v1/organization/{org_code}", mockResponse);
                var request = new UpdateOrganizationRequest();

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.UpdateOrganizationAsync(org_code, expand, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateOrganization test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task UpdateOrganization_Real_Test()
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
                // WARNING: Real API test - This operation requires existing org_code
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;
                var expand = "expand";
                var request = new UpdateOrganizationRequest();

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.UpdateOrganizationAsync(org_code, expand, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateOrganization test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteOrganization_Mock_Test()
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

                var org_code = "test-org_code";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("DELETE", $"/api/v1/organization/{org_code}", mockResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                await api.DeleteOrganizationAsync(org_code);
                // Void method - no response to check
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteOrganization test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task DeleteOrganization_Real_Test()
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
                // WARNING: Real API test - This operation requires existing org_code
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                await api.DeleteOrganizationAsync(org_code);
                // Void method - no response to check
                _output.WriteLine($"Void method completed successfully");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteOrganization test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetOrganizationUsers_Mock_Test()
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

                var org_code = "test-org_code";
                var mockResponse = new GetOrganizationUsersResponse();
                mockHandler.AddResponse("GET", $"/api/v1/organizations/{org_code}/users", mockResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.GetOrganizationUsersAsync(org_code);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetOrganizationUsers test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetOrganizationUsers_Real_Test()
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
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.GetOrganizationUsersAsync(org_code);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetOrganizationUsers test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task AddOrganizationUsers_Mock_Test()
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

                var org_code = "test-org_code";
                var mockResponse = new AddOrganizationUsersResponse();
                mockHandler.AddResponse("POST", $"/api/v1/organizations/{org_code}/users", mockResponse);
                var request = new AddOrganizationUsersRequest();

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.AddOrganizationUsersAsync(org_code, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in AddOrganizationUsers test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task AddOrganizationUsers_Real_Test()
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
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;
                var request = new AddOrganizationUsersRequest();

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.AddOrganizationUsersAsync(org_code, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in AddOrganizationUsers test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateOrganizationUsers_Mock_Test()
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

                var org_code = "test-org_code";
                var mockResponse = new UpdateOrganizationUsersResponse();
                mockHandler.AddResponse("PATCH", $"/api/v1/organizations/{org_code}/users", mockResponse);
                var request = new UpdateOrganizationUsersRequest();

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.UpdateOrganizationUsersAsync(org_code, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateOrganizationUsers test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task UpdateOrganizationUsers_Real_Test()
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
                // WARNING: Real API test - This operation requires existing org_code
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;
                var request = new UpdateOrganizationUsersRequest();

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.UpdateOrganizationUsersAsync(org_code, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateOrganizationUsers test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetOrganizationUserRoles_Mock_Test()
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

                var org_code = "test-org_code";
                var user_id = "test-user_id";
                var mockResponse = new GetOrganizationsUserRolesResponse();
                mockHandler.AddResponse("GET", $"/api/v1/organizations/{org_code}/users/{user_id}/roles", mockResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.GetOrganizationUserRolesAsync(org_code, user_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetOrganizationUserRoles test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetOrganizationUserRoles_Real_Test()
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
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;
                // Using test resource from fixture: UserId
                var user_id = _fixture.UserId;

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.GetOrganizationUserRolesAsync(org_code, user_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetOrganizationUserRoles test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task CreateOrganizationUserRole_Mock_Test()
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

                var org_code = "test-org_code";
                var user_id = "test-user_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("POST", $"/api/v1/organizations/{org_code}/users/{user_id}/roles", mockResponse);
                var request = new CreateOrganizationUserRoleRequest();

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.CreateOrganizationUserRoleAsync(org_code, user_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateOrganizationUserRole test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task CreateOrganizationUserRole_Real_Test()
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
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;
                // Using test resource from fixture: UserId
                var user_id = _fixture.UserId;
                var request = new CreateOrganizationUserRoleRequest();

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.CreateOrganizationUserRoleAsync(org_code, user_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateOrganizationUserRole test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteOrganizationUserRole_Mock_Test()
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

                var org_code = "test-org_code";
                var user_id = "test-user_id";
                var role_id = "test-role_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("DELETE", $"/api/v1/organizations/{org_code}/users/{user_id}/roles/{role_id}", mockResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.DeleteOrganizationUserRoleAsync(org_code, user_id, role_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteOrganizationUserRole test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task DeleteOrganizationUserRole_Real_Test()
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
                // WARNING: Real API test - This operation requires existing org_code
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Setup: Add role to user in organization before testing removal
                var setupApi = CreateApi((client, config) => new OrganizationsApi(client, config));
                try {{
                    var addRequest = new CreateOrganizationUserRoleRequest(roleId: _fixture.RoleId);
                    await setupApi.CreateOrganizationUserRoleAsync(_fixture.OrganizationCode, _fixture.UserId, addRequest);
                }} catch (Exception) {{ /* Role may already be assigned */ }}

                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;
                // Using test resource from fixture: UserId
                var user_id = _fixture.UserId;
                // Using test resource from fixture: RoleId
                var role_id = _fixture.RoleId;

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.DeleteOrganizationUserRoleAsync(org_code, user_id, role_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteOrganizationUserRole test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetOrganizationUserPermissions_Mock_Test()
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

                var org_code = "test-org_code";
                var user_id = "test-user_id";
                var mockResponse = new GetOrganizationsUserPermissionsResponse();
                mockHandler.AddResponse("GET", $"/api/v1/organizations/{org_code}/users/{user_id}/permissions", mockResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.GetOrganizationUserPermissionsAsync(org_code, user_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetOrganizationUserPermissions test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetOrganizationUserPermissions_Real_Test()
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
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;
                // Using test resource from fixture: UserId
                var user_id = _fixture.UserId;

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.GetOrganizationUserPermissionsAsync(org_code, user_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetOrganizationUserPermissions test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task CreateOrganizationUserPermission_Mock_Test()
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

                var org_code = "test-org_code";
                var user_id = "test-user_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("POST", $"/api/v1/organizations/{org_code}/users/{user_id}/permissions", mockResponse);
                var request = new CreateOrganizationUserPermissionRequest();

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.CreateOrganizationUserPermissionAsync(org_code, user_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateOrganizationUserPermission test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task CreateOrganizationUserPermission_Real_Test()
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
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;
                // Using test resource from fixture: UserId
                var user_id = _fixture.UserId;
                var request = new CreateOrganizationUserPermissionRequest();

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.CreateOrganizationUserPermissionAsync(org_code, user_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateOrganizationUserPermission test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteOrganizationUserPermission_Mock_Test()
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

                var org_code = "test-org_code";
                var user_id = "test-user_id";
                var permission_id = "test-permission_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("DELETE", $"/api/v1/organizations/{org_code}/users/{user_id}/permissions/{permission_id}", mockResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.DeleteOrganizationUserPermissionAsync(org_code, user_id, permission_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteOrganizationUserPermission test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task DeleteOrganizationUserPermission_Real_Test()
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
                // WARNING: Real API test - This operation requires existing org_code
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Setup: Add permission to user in organization before testing removal
                var setupApi = CreateApi((client, config) => new OrganizationsApi(client, config));
                try {{
                    var addRequest = new CreateOrganizationUserPermissionRequest(permissionId: _fixture.PermissionId);
                    await setupApi.CreateOrganizationUserPermissionAsync(_fixture.OrganizationCode, _fixture.UserId, addRequest);
                }} catch (Exception) {{ /* Permission may already be assigned */ }}

                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;
                // Using test resource from fixture: UserId
                var user_id = _fixture.UserId;
                // Using test resource from fixture: PermissionId
                var permission_id = _fixture.PermissionId;

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.DeleteOrganizationUserPermissionAsync(org_code, user_id, permission_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteOrganizationUserPermission test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task RemoveOrganizationUser_Mock_Test()
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

                var org_code = "test-org_code";
                var user_id = "test-user_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("DELETE", $"/api/v1/organizations/{org_code}/users/{user_id}", mockResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.RemoveOrganizationUserAsync(org_code, user_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in RemoveOrganizationUser test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task RemoveOrganizationUser_Real_Test()
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
                // WARNING: Real API test - This operation requires existing org_code
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;
                // Using test resource from fixture: UserId
                var user_id = _fixture.UserId;

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.RemoveOrganizationUserAsync(org_code, user_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in RemoveOrganizationUser test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task AddOrganizationUserAPIScope_Mock_Test()
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

                var org_code = "test-org_code";
                var user_id = "test-user_id";
                var api_id = "test-api_id";
                var scope_id = "test-scope_id";
                var mockResponse = new { };
                mockHandler.AddResponse("POST", $"/api/v1/organizations/{org_code}/users/{user_id}/apis/{api_id}/scopes/{scope_id}", mockResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                await api.AddOrganizationUserAPIScopeAsync(org_code, user_id, api_id, scope_id);
                // Void method - no response to check
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in AddOrganizationUserAPIScope test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task AddOrganizationUserAPIScope_Real_Test()
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
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;
                // Using test resource from fixture: UserId
                var user_id = _fixture.UserId;
                var api_id = "test-api_id";
                var scope_id = "test-scope_id";

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                await api.AddOrganizationUserAPIScopeAsync(org_code, user_id, api_id, scope_id);
                // Void method - no response to check
                _output.WriteLine($"Void method completed successfully");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in AddOrganizationUserAPIScope test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteOrganizationUserAPIScope_Mock_Test()
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

                var org_code = "test-org_code";
                var user_id = "test-user_id";
                var api_id = "test-api_id";
                var scope_id = "test-scope_id";
                var mockResponse = new { };
                mockHandler.AddResponse("DELETE", $"/api/v1/organizations/{org_code}/users/{user_id}/apis/{api_id}/scopes/{scope_id}", mockResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                await api.DeleteOrganizationUserAPIScopeAsync(org_code, user_id, api_id, scope_id);
                // Void method - no response to check
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteOrganizationUserAPIScope test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task DeleteOrganizationUserAPIScope_Real_Test()
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
                // WARNING: Real API test - This operation requires existing org_code
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;
                // Using test resource from fixture: UserId
                var user_id = _fixture.UserId;
                // WARNING: Using placeholder api_id - test will likely fail without real resource ID
                var api_id = "test-api_id";
                // WARNING: Using placeholder scope_id - test will likely fail without real resource ID
                var scope_id = "test-scope_id";

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                await api.DeleteOrganizationUserAPIScopeAsync(org_code, user_id, api_id, scope_id);
                // Void method - no response to check
                _output.WriteLine($"Void method completed successfully");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteOrganizationUserAPIScope test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetOrgUserMFA_Mock_Test()
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

                var org_code = "test-org_code";
                var user_id = "test-user_id";
                var mockResponse = new GetUserMfaResponse();
                mockHandler.AddResponse("GET", $"/api/v1/organizations/{org_code}/users/{user_id}/mfa", mockResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.GetOrgUserMFAAsync(org_code, user_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetOrgUserMFA test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetOrgUserMFA_Real_Test()
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
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;
                // Using test resource from fixture: UserId
                var user_id = _fixture.UserId;

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.GetOrgUserMFAAsync(org_code, user_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetOrgUserMFA test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task ResetOrgUserMFAAll_Mock_Test()
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

                var org_code = "test-org_code";
                var user_id = "test-user_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("DELETE", $"/api/v1/organizations/{org_code}/users/{user_id}/mfa", mockResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.ResetOrgUserMFAAllAsync(org_code, user_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in ResetOrgUserMFAAll test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task ResetOrgUserMFAAll_Real_Test()
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
                // WARNING: Real API test - This operation requires existing org_code
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;
                // Using test resource from fixture: UserId
                var user_id = _fixture.UserId;

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.ResetOrgUserMFAAllAsync(org_code, user_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in ResetOrgUserMFAAll test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task ResetOrgUserMFA_Mock_Test()
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

                var org_code = "test-org_code";
                var user_id = "test-user_id";
                var factor_id = "test-factor_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("DELETE", $"/api/v1/organizations/{org_code}/users/{user_id}/mfa/{factor_id}", mockResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.ResetOrgUserMFAAsync(org_code, user_id, factor_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in ResetOrgUserMFA test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task ResetOrgUserMFA_Real_Test()
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
                // WARNING: Real API test - This operation requires existing org_code
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;
                // Using test resource from fixture: UserId
                var user_id = _fixture.UserId;
                // WARNING: Using placeholder factor_id - test will likely fail without real resource ID
                var factor_id = "test-factor_id";

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.ResetOrgUserMFAAsync(org_code, user_id, factor_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in ResetOrgUserMFA test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetOrganizationFeatureFlags_Mock_Test()
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

                var org_code = "test-org_code";
                var mockResponse = new GetOrganizationFeatureFlagsResponse();
                mockHandler.AddResponse("GET", $"/api/v1/organizations/{org_code}/feature_flags", mockResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.GetOrganizationFeatureFlagsAsync(org_code);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetOrganizationFeatureFlags test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetOrganizationFeatureFlags_Real_Test()
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
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.GetOrganizationFeatureFlagsAsync(org_code);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetOrganizationFeatureFlags test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteOrganizationFeatureFlagOverrides_Mock_Test()
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

                var org_code = "test-org_code";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("DELETE", $"/api/v1/organizations/{org_code}/feature_flags", mockResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.DeleteOrganizationFeatureFlagOverridesAsync(org_code);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteOrganizationFeatureFlagOverrides test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task DeleteOrganizationFeatureFlagOverrides_Real_Test()
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
                // WARNING: Real API test - This operation requires existing org_code
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.DeleteOrganizationFeatureFlagOverridesAsync(org_code);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteOrganizationFeatureFlagOverrides test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteOrganizationFeatureFlagOverride_Mock_Test()
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

                var org_code = "test-org_code";
                var feature_flag_key = "test-feature_flag_key";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("DELETE", $"/api/v1/organizations/{org_code}/feature_flags/{feature_flag_key}", mockResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.DeleteOrganizationFeatureFlagOverrideAsync(org_code, feature_flag_key);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteOrganizationFeatureFlagOverride test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task DeleteOrganizationFeatureFlagOverride_Real_Test()
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
                // WARNING: Real API test - This operation requires existing org_code
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;
                // WARNING: Using placeholder feature_flag_key - test will likely fail without real resource ID
                var feature_flag_key = "test-feature_flag_key";

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.DeleteOrganizationFeatureFlagOverrideAsync(org_code, feature_flag_key);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteOrganizationFeatureFlagOverride test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateOrganizationFeatureFlagOverride_Mock_Test()
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

                var org_code = "test-org_code";
                var feature_flag_key = "test-feature_flag_key";
                var value = "value";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("PATCH", $"/api/v1/organizations/{org_code}/feature_flags/{feature_flag_key}", mockResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.UpdateOrganizationFeatureFlagOverrideAsync(org_code, feature_flag_key, value);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateOrganizationFeatureFlagOverride test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task UpdateOrganizationFeatureFlagOverride_Real_Test()
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
                // WARNING: Real API test - This operation requires existing org_code
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;
                // WARNING: Using placeholder feature_flag_key - test will likely fail without real resource ID
                var feature_flag_key = "test-feature_flag_key";
                var value = "value";

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.UpdateOrganizationFeatureFlagOverrideAsync(org_code, feature_flag_key, value);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateOrganizationFeatureFlagOverride test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateOrganizationProperty_Mock_Test()
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

                var org_code = "test-org_code";
                var property_key = "test-property_key";
                var value = "value";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("PUT", $"/api/v1/organizations/{org_code}/properties/{property_key}", mockResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.UpdateOrganizationPropertyAsync(org_code, property_key, value);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateOrganizationProperty test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task UpdateOrganizationProperty_Real_Test()
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
                // WARNING: Real API test - This operation requires existing org_code
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;
                // Using test resource from fixture: PropertyKey
                var property_key = _fixture.PropertyKey;
                var value = "value";

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.UpdateOrganizationPropertyAsync(org_code, property_key, value);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateOrganizationProperty test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetOrganizationPropertyValues_Mock_Test()
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

                var org_code = "test-org_code";
                var mockResponse = new GetPropertyValuesResponse();
                mockHandler.AddResponse("GET", $"/api/v1/organizations/{org_code}/properties", mockResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.GetOrganizationPropertyValuesAsync(org_code);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetOrganizationPropertyValues test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetOrganizationPropertyValues_Real_Test()
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
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.GetOrganizationPropertyValuesAsync(org_code);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetOrganizationPropertyValues test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateOrganizationProperties_Mock_Test()
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

                var org_code = "test-org_code";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("PATCH", $"/api/v1/organizations/{org_code}/properties", mockResponse);
                var request = new UpdateOrganizationPropertiesRequest(properties: "test-properties");

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.UpdateOrganizationPropertiesAsync(org_code, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateOrganizationProperties test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task UpdateOrganizationProperties_Real_Test()
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
                // WARNING: Real API test - This operation requires existing org_code
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;
                var request = new UpdateOrganizationPropertiesRequest(properties: "test-properties");

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.UpdateOrganizationPropertiesAsync(org_code, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateOrganizationProperties test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task ReplaceOrganizationMFA_Mock_Test()
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

                var org_code = "test-org_code";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("PUT", $"/api/v1/organizations/{org_code}/mfa", mockResponse);
                var request = new ReplaceOrganizationMFARequest(enabledFactors: new System.Collections.Generic.List<ReplaceOrganizationMFARequest.EnabledFactorsEnum>());

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.ReplaceOrganizationMFAAsync(org_code, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in ReplaceOrganizationMFA test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task ReplaceOrganizationMFA_Real_Test()
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
                // WARNING: Real API test - This operation requires existing org_code
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;
                var request = new ReplaceOrganizationMFARequest(enabledFactors: new System.Collections.Generic.List<ReplaceOrganizationMFARequest.EnabledFactorsEnum>());

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.ReplaceOrganizationMFAAsync(org_code, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in ReplaceOrganizationMFA test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteOrganizationHandle_Mock_Test()
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

                var org_code = "test-org_code";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("DELETE", $"/api/v1/organization/{org_code}/handle", mockResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.DeleteOrganizationHandleAsync(org_code);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteOrganizationHandle test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task DeleteOrganizationHandle_Real_Test()
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
                // WARNING: Real API test - This operation requires existing org_code
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.DeleteOrganizationHandleAsync(org_code);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteOrganizationHandle test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task ReadOrganizationLogo_Mock_Test()
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

                var org_code = "test-org_code";
                var mockResponse = new ReadLogoResponse();
                mockHandler.AddResponse("GET", $"/api/v1/organizations/{org_code}/logos", mockResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.ReadOrganizationLogoAsync(org_code);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in ReadOrganizationLogo test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task ReadOrganizationLogo_Real_Test()
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
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.ReadOrganizationLogoAsync(org_code);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in ReadOrganizationLogo test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task AddOrganizationLogo_Mock_Test()
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

                var org_code = "test-org_code";
                var type = "test-type";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("POST", $"/api/v1/organizations/{org_code}/logos/{type}", mockResponse);
                var logo = new FileParameter("test-file.txt", new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes("test file content")));

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.AddOrganizationLogoAsync(org_code, type, logo);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in AddOrganizationLogo test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task AddOrganizationLogo_Real_Test()
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
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;
                var type = "test-type";
                var logo = new FileParameter("test-file.txt", new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes("test file content")));

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.AddOrganizationLogoAsync(org_code, type, logo);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in AddOrganizationLogo test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteOrganizationLogo_Mock_Test()
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

                var org_code = "test-org_code";
                var type = "test-type";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("DELETE", $"/api/v1/organizations/{org_code}/logos/{type}", mockResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                await api.DeleteOrganizationLogoAsync(org_code, type);
                // Void method - no response to check
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteOrganizationLogo test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task DeleteOrganizationLogo_Real_Test()
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
                // WARNING: Real API test - This operation requires existing org_code
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;
                // WARNING: Using placeholder type - test will likely fail without real resource ID
                var type = "test-type";

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                await api.DeleteOrganizationLogoAsync(org_code, type);
                // Void method - no response to check
                _output.WriteLine($"Void method completed successfully");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteOrganizationLogo test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetOrganizationConnections_Mock_Test()
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

                var organization_code = "test-organization_code";
                var mockResponse = new GetConnectionsResponse();
                mockHandler.AddResponse("GET", $"/api/v1/organizations/{organization_code}/connections", mockResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.GetOrganizationConnectionsAsync(organization_code);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetOrganizationConnections test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetOrganizationConnections_Real_Test()
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
                // Using test resource from fixture: OrganizationCode
                var organization_code = _fixture.OrganizationCode;

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.GetOrganizationConnectionsAsync(organization_code);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetOrganizationConnections test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task EnableOrgConnection_Mock_Test()
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

                var organization_code = "test-organization_code";
                var connection_id = "test-connection_id";
                var mockResponse = new { };
                mockHandler.AddResponse("POST", $"/api/v1/organizations/{organization_code}/connections/{connection_id}", mockResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                await api.EnableOrgConnectionAsync(organization_code, connection_id);
                // Void method - no response to check
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in EnableOrgConnection test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task EnableOrgConnection_Real_Test()
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
                // Using test resource from fixture: OrganizationCode
                var organization_code = _fixture.OrganizationCode;
                var connection_id = "test-connection_id";

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                await api.EnableOrgConnectionAsync(organization_code, connection_id);
                // Void method - no response to check
                _output.WriteLine($"Void method completed successfully");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in EnableOrgConnection test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task RemoveOrgConnection_Mock_Test()
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

                var organization_code = "test-organization_code";
                var connection_id = "test-connection_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("DELETE", $"/api/v1/organizations/{organization_code}/connections/{connection_id}", mockResponse);

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.RemoveOrgConnectionAsync(organization_code, connection_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in RemoveOrgConnection test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task RemoveOrgConnection_Real_Test()
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
                // WARNING: Real API test - This operation requires existing organization_code
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: OrganizationCode
                var organization_code = _fixture.OrganizationCode;
                // WARNING: Using placeholder connection_id - test will likely fail without real resource ID
                var connection_id = "test-connection_id";

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.RemoveOrgConnectionAsync(organization_code, connection_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in RemoveOrgConnection test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateOrganizationSessions_Mock_Test()
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

                var org_code = "test-org_code";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("PATCH", $"/api/v1/organizations/{org_code}/sessions", mockResponse);
                var request = new UpdateOrganizationSessionsRequest();

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                // Act
                var response = await api.UpdateOrganizationSessionsAsync(org_code, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateOrganizationSessions test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task UpdateOrganizationSessions_Real_Test()
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
                // WARNING: Real API test - This operation requires existing org_code
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: OrganizationCode
                var org_code = _fixture.OrganizationCode;
                var request = new UpdateOrganizationSessionsRequest();

                var api = CreateApi((client, config) => new OrganizationsApi(client, config));

                var response = await api.UpdateOrganizationSessionsAsync(org_code, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateOrganizationSessions test: {ex.Message}");
                throw;
            }
        }

    }
}
