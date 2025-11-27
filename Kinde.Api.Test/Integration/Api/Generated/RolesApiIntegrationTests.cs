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
    /// Auto-generated integration tests for RolesApi with both mock and real API support
    /// </summary>
    public class RolesApiIntegrationTests : BaseIntegrationTest, IClassFixture<TestResourceFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly TestResourceFixture _fixture;

        public RolesApiIntegrationTests(ITestOutputHelper output, TestResourceFixture fixture) : base()
        {
            _output = output;
            _fixture = fixture;
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetRoles_Mock_Test()
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

                var mockResponse = new GetRolesResponse();
                mockHandler.AddResponse("GET", "/api/v1/roles", mockResponse);

                var api = CreateApi((client, config) => new RolesApi(client, config));

                // Act
                var response = await api.GetRolesAsync();

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetRoles test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetRoles_Real_Test()
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
                var api = CreateApi((client, config) => new RolesApi(client, config));

                var response = await api.GetRolesAsync();

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetRoles test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task CreateRole_Mock_Test()
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

                var mockResponse = new CreateRolesResponse();
                mockHandler.AddResponse("POST", "/api/v1/roles", mockResponse);
                var request = new CreateRoleRequest();

                var api = CreateApi((client, config) => new RolesApi(client, config));

                // Act
                var response = await api.CreateRoleAsync(request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateRole test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task CreateRole_Real_Test()
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
                var request = new CreateRoleRequest();

                var api = CreateApi((client, config) => new RolesApi(client, config));

                var response = await api.CreateRoleAsync(request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in CreateRole test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetRole_Mock_Test()
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

                var role_id = "test-role_id";
                var mockResponse = new GetRoleResponse();
                mockHandler.AddResponse("GET", $"/api/v1/roles/{role_id}", mockResponse);

                var api = CreateApi((client, config) => new RolesApi(client, config));

                // Act
                var response = await api.GetRoleAsync(role_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetRole test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetRole_Real_Test()
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
                // Using test resource from fixture: RoleId
                var role_id = _fixture.RoleId;

                var api = CreateApi((client, config) => new RolesApi(client, config));

                var response = await api.GetRoleAsync(role_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetRole test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateRoles_Mock_Test()
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

                var role_id = "test-role_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("PATCH", $"/api/v1/roles/{role_id}", mockResponse);
                var request = new UpdateRolesRequest(name: "test-name", key: "test-key");

                var api = CreateApi((client, config) => new RolesApi(client, config));

                // Act
                var response = await api.UpdateRolesAsync(role_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateRoles test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task UpdateRoles_Real_Test()
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
                // WARNING: Real API test - This operation requires existing role_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: RoleId
                var role_id = _fixture.RoleId;
                var request = new UpdateRolesRequest(name: "test-name", key: "test-key");

                var api = CreateApi((client, config) => new RolesApi(client, config));

                var response = await api.UpdateRolesAsync(role_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateRoles test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteRole_Mock_Test()
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

                var role_id = "test-role_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("DELETE", $"/api/v1/roles/{role_id}", mockResponse);

                var api = CreateApi((client, config) => new RolesApi(client, config));

                // Act
                var response = await api.DeleteRoleAsync(role_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteRole test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task DeleteRole_Real_Test()
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
                var api = CreateApi((client, config) => new RolesApi(client, config));

                // Create a temporary role specifically for this delete test
                // This avoids deleting the shared fixture role which other tests depend on
                var roleKey = $"del_test_{Guid.NewGuid():N}".Substring(0, 25);
                var createRequest = new CreateRoleRequest(
                    name: $"Delete Test Role {Guid.NewGuid():N}",
                    key: roleKey
                );
                var createResponse = await api.CreateRoleAsync(createRequest);
                var role_id = createResponse?.Role?.Id ?? throw new InvalidOperationException("Failed to create test role");
                _output.WriteLine($"Created temporary role for delete test: {role_id}");

                var response = await api.DeleteRoleAsync(role_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteRole test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetRoleScopes_Mock_Test()
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

                var role_id = "test-role_id";
                var mockResponse = new RoleScopesResponse();
                mockHandler.AddResponse("GET", $"/api/v1/roles/{role_id}/scopes", mockResponse);

                var api = CreateApi((client, config) => new RolesApi(client, config));

                // Act
                var response = await api.GetRoleScopesAsync(role_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetRoleScopes test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetRoleScopes_Real_Test()
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
                // Using test resource from fixture: RoleId
                var role_id = _fixture.RoleId;

                var api = CreateApi((client, config) => new RolesApi(client, config));

                var response = await api.GetRoleScopesAsync(role_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetRoleScopes test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task AddRoleScope_Mock_Test()
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

                var role_id = "test-role_id";
                var mockResponse = new AddRoleScopeResponse();
                mockHandler.AddResponse("POST", $"/api/v1/roles/{role_id}/scopes", mockResponse);
                var request = new AddRoleScopeRequest(scopeId: "test-scope_id");

                var api = CreateApi((client, config) => new RolesApi(client, config));

                // Act
                var response = await api.AddRoleScopeAsync(role_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in AddRoleScope test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task AddRoleScope_Real_Test()
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
                // Note: This test requires a valid scope_id - skipping if not available
                _output.WriteLine("Skipping AddRoleScope test - requires valid scope_id");
                return;

                // Using test resource from fixture: RoleId
                var role_id = _fixture.RoleId;
                var request = new AddRoleScopeRequest(scopeId: "test-scope_id");

                var api = CreateApi((client, config) => new RolesApi(client, config));

                var response = await api.AddRoleScopeAsync(role_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in AddRoleScope test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteRoleScope_Mock_Test()
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

                var role_id = "test-role_id";
                var scope_id = "test-scope_id";
                var mockResponse = new DeleteRoleScopeResponse();
                mockHandler.AddResponse("DELETE", $"/api/v1/roles/{role_id}/scopes/{scope_id}", mockResponse);

                var api = CreateApi((client, config) => new RolesApi(client, config));

                // Act
                var response = await api.DeleteRoleScopeAsync(role_id, scope_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteRoleScope test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task DeleteRoleScope_Real_Test()
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
                // WARNING: Real API test - This operation requires existing role_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Setup: Add scope to role before testing deletion
                // Note: This test requires a valid scope_id - skipping if not available
                _output.WriteLine("Skipping DeleteRoleScope test - requires valid scope to be added first");
                return;

                // Using test resource from fixture: RoleId
                var role_id = _fixture.RoleId;
                // WARNING: Using placeholder scope_id - test will likely fail without real resource ID
                var scope_id = "test-scope_id";

                var api = CreateApi((client, config) => new RolesApi(client, config));

                var response = await api.DeleteRoleScopeAsync(role_id, scope_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in DeleteRoleScope test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetRolePermissions_Mock_Test()
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

                var role_id = "test-role_id";
                var mockResponse = new RolePermissionsResponse();
                mockHandler.AddResponse("GET", $"/api/v1/roles/{role_id}/permissions", mockResponse);

                var api = CreateApi((client, config) => new RolesApi(client, config));

                // Act
                var response = await api.GetRolePermissionsAsync(role_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetRolePermissions test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetRolePermissions_Real_Test()
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
                // Using test resource from fixture: RoleId
                var role_id = _fixture.RoleId;

                var api = CreateApi((client, config) => new RolesApi(client, config));

                var response = await api.GetRolePermissionsAsync(role_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in GetRolePermissions test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateRolePermissions_Mock_Test()
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

                var role_id = "test-role_id";
                var mockResponse = new UpdateRolePermissionsResponse();
                mockHandler.AddResponse("PATCH", $"/api/v1/roles/{role_id}/permissions", mockResponse);
                var request = new UpdateRolePermissionsRequest();

                var api = CreateApi((client, config) => new RolesApi(client, config));

                // Act
                var response = await api.UpdateRolePermissionsAsync(role_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateRolePermissions test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task UpdateRolePermissions_Real_Test()
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
                // WARNING: Real API test - This operation requires existing role_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Using test resource from fixture: RoleId
                var role_id = _fixture.RoleId;
                var request = new UpdateRolePermissionsRequest();

                var api = CreateApi((client, config) => new RolesApi(client, config));

                var response = await api.UpdateRolePermissionsAsync(role_id, request);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in UpdateRolePermissions test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task RemoveRolePermission_Mock_Test()
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

                var role_id = "test-role_id";
                var permission_id = "test-permission_id";
                var mockResponse = new SuccessResponse();
                mockHandler.AddResponse("DELETE", $"/api/v1/roles/{role_id}/permissions/{permission_id}", mockResponse);

                var api = CreateApi((client, config) => new RolesApi(client, config));

                // Act
                var response = await api.RemoveRolePermissionAsync(role_id, permission_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Mock test successful");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in RemoveRolePermission test: {ex.Message}");
                throw;
            }
        }


        [Fact]
        [Trait("TestMode", "Real")]
        public async Task RemoveRolePermission_Real_Test()
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
                // WARNING: Real API test - This operation requires existing role_id
                // This test may fail if the resource doesn't exist in your Kinde instance
                // Consider creating the resource first or using a test environment
                // Setup: Add permission to role before testing removal
                var setupApi = CreateApi((client, config) => new RolesApi(client, config));
                try {{
                    var permissionInner = new UpdateRolePermissionsRequestPermissionsInner(id: _fixture.PermissionId);
                    var addRequest = new UpdateRolePermissionsRequest(permissions: new System.Collections.Generic.List<UpdateRolePermissionsRequestPermissionsInner>() {{ permissionInner }});
                    await setupApi.UpdateRolePermissionsAsync(_fixture.RoleId, addRequest);
                }} catch (Exception) {{ /* Permission may already be added */ }}

                // Using test resource from fixture: RoleId
                var role_id = _fixture.RoleId;
                // Using test resource from fixture: PermissionId
                var permission_id = _fixture.PermissionId;

                var api = CreateApi((client, config) => new RolesApi(client, config));

                var response = await api.RemoveRolePermissionAsync(role_id, permission_id);

                // Assert
                Assert.NotNull(response);
                _output.WriteLine($"Response received: {response?.GetType().Name}");
                _output.WriteLine($"Test completed successfully");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error in RemoveRolePermission test: {ex.Message}");
                throw;
            }
        }

    }
}
