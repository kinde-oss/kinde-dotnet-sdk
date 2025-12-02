using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;
using Xunit;

namespace Kinde.Api.Test.Integration
{
    /// <summary>
    /// Test fixture that creates and manages test resources for integration tests.
    /// Resources are created via API on first access (lazy initialization) and cleaned up via API at disposal.
    /// All operations use the Management API - no database access required.
    /// </summary>
    public class TestResourceFixture : IDisposable
    {
        private readonly TestConfiguration _config;
        private HttpClient? _httpClient;
        private Configuration? _apiConfiguration;
        private bool _disposed = false;
        private bool _initialized = false;
        private readonly object _initLock = new object();

        // Test resource IDs - populated lazily
        private string? _organizationCode;
        private string? _userId;
        private string? _permissionId;
        private string? _propertyId;
        private string? _propertyKey;
        private string? _roleId;
        private string? _applicationId;

        /// <summary>
        /// Gets the organization code. Initializes resources if not already done.
        /// </summary>
        public string OrganizationCode
        {
            get
            {
                EnsureInitialized();
                return _organizationCode ?? throw new InvalidOperationException("Organization not created");
            }
        }

        /// <summary>
        /// Gets the user ID. Initializes resources if not already done.
        /// </summary>
        public string UserId
        {
            get
            {
                EnsureInitialized();
                return _userId ?? throw new InvalidOperationException("User not created");
            }
        }

        /// <summary>
        /// Gets the permission ID. Initializes resources if not already done.
        /// </summary>
        public string PermissionId
        {
            get
            {
                EnsureInitialized();
                return _permissionId ?? throw new InvalidOperationException("Permission not created");
            }
        }

        /// <summary>
        /// Gets the property ID. Initializes resources if not already done.
        /// Returns empty string if no property is available.
        /// </summary>
        public string PropertyId
        {
            get
            {
                EnsureInitialized();
                return _propertyId ?? string.Empty;
            }
        }

        /// <summary>
        /// Gets the property key. Initializes resources if not already done.
        /// Returns empty string if no property is available.
        /// </summary>
        public string PropertyKey
        {
            get
            {
                EnsureInitialized();
                return _propertyKey ?? string.Empty;
            }
        }

        /// <summary>
        /// Gets the role ID. Initializes resources if not already done.
        /// </summary>
        public string RoleId
        {
            get
            {
                EnsureInitialized();
                return _roleId ?? throw new InvalidOperationException("Role not created");
            }
        }

        /// <summary>
        /// Gets the application ID. Initializes resources if not already done.
        /// </summary>
        public string ApplicationId
        {
            get
            {
                EnsureInitialized();
                return _applicationId ?? string.Empty;
            }
        }

        public TestResourceFixture()
        {
            _config = TestConfiguration.Instance;
            
            // Don't initialize in constructor - lazy initialization on first access
            if (!_config.UseRealApi)
            {
                Console.WriteLine("[TEST FIXTURE] Mock mode - resources will not be created");
            }
            else
            {
                Console.WriteLine("[TEST FIXTURE] Real API mode - resources will be created on first access");
            }
        }

        /// <summary>
        /// Ensures resources are initialized. Thread-safe lazy initialization.
        /// </summary>
        private void EnsureInitialized()
        {
            if (_initialized || !_config.UseRealApi)
                return;

            // Double-check pattern - acquire lock only if not initialized
            lock (_initLock)
            {
                if (_initialized)
                    return;
                _initialized = true; // Mark before work to prevent re-entry
            }

            // Initialization outside lock to avoid sync-over-async deadlock
            Console.WriteLine("[TEST FIXTURE] Initializing test resources for Real API mode...");

            try
            {
                var mgmtConfig = _config.ManagementApi;

                // Get access token
                var accessToken = GetAccessTokenAsync().GetAwaiter().GetResult();

                _apiConfiguration = new Configuration
                {
                    BasePath = mgmtConfig.Domain,
                    AccessToken = accessToken
                };

                _httpClient = new HttpClient();

                // Create all test resources via API
                CreateTestResourcesAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TEST FIXTURE] Error during initialization: {ex.Message}");
                // Reset the initialized flag on failure so retry is possible
                lock (_initLock)
                {
                    _initialized = false;
                }
                throw;
            }
        }

        private async Task<string> GetAccessTokenAsync()
        {
            var mgmtConfig = _config.ManagementApi;
            
            using var client = new HttpClient();
            var formData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", mgmtConfig.ClientId),
                new KeyValuePair<string, string>("client_secret", mgmtConfig.ClientSecret),
                new KeyValuePair<string, string>("audience", mgmtConfig.Audience)
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"{mgmtConfig.Domain}/oauth2/token")
            {
                Content = new FormUrlEncodedContent(formData)
            };

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var json = Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(content);
            return json?["access_token"]?.ToString() ?? throw new InvalidOperationException("Failed to get access token");
        }

        private async Task CreateTestResourcesAsync()
        {
            try
            {
                Console.WriteLine("[TEST FIXTURE] Creating test organization via API...");
                var orgsApi = new OrganizationsApi(_httpClient!, _apiConfiguration!);
                var orgRequest = new CreateOrganizationRequest(name: $"Test Org {Guid.NewGuid():N}");
                var orgResponse = await orgsApi.CreateOrganizationAsync(orgRequest);
                _organizationCode = orgResponse?.Organization?.Code ?? throw new InvalidOperationException("Failed to get organization code");
                Console.WriteLine($"[TEST FIXTURE] Created organization via API: {_organizationCode}");

                Console.WriteLine("[TEST FIXTURE] Creating test user via API...");
                var usersApi = new UsersApi(_httpClient!, _apiConfiguration!);
                var userEmail = $"test-{Guid.NewGuid():N}@test.kinde.com";
                var userRequest = new CreateUserRequest(
                    profile: new CreateUserRequestProfile(),
                    identities: new List<CreateUserRequestIdentitiesInner>
                    {
                        new CreateUserRequestIdentitiesInner(
                            type: CreateUserRequestIdentitiesInner.TypeEnum.Email,
                            isVerified: true,
                            details: new CreateUserRequestIdentitiesInnerDetails(email: userEmail)
                        )
                    }
                );
                var userResponse = await usersApi.CreateUserAsync(userRequest);
                _userId = userResponse.Id ?? throw new InvalidOperationException("Failed to get user ID");
                Console.WriteLine($"[TEST FIXTURE] Created user via API: {_userId}");

                Console.WriteLine("[TEST FIXTURE] Adding user to organization via API...");
                var addUserRequest = new AddOrganizationUsersRequest(
                    users: new List<AddOrganizationUsersRequestUsersInner>
                    {
                        new AddOrganizationUsersRequestUsersInner(id: _userId)
                    }
                );
                await orgsApi.AddOrganizationUsersAsync(_organizationCode, addUserRequest);
                Console.WriteLine($"[TEST FIXTURE] Added user {_userId} to organization {_organizationCode} via API");

                Console.WriteLine("[TEST FIXTURE] Creating test permission via API...");
                var permissionsApi = new PermissionsApi(_httpClient!, _apiConfiguration!);
                var permissionKey = $"test_perm_{Guid.NewGuid():N}".Substring(0, 25);
                var permissionRequest = new CreatePermissionRequest(
                    name: $"Test Permission {Guid.NewGuid():N}",
                    key: permissionKey
                );
                await permissionsApi.CreatePermissionAsync(permissionRequest);
                
                // Get the permission ID by listing permissions and finding the one we just created
                var permissionsResponse = await permissionsApi.GetPermissionsAsync();
                var createdPermission = permissionsResponse?.Permissions?.FirstOrDefault(p => p.Key == permissionKey);
                _permissionId = createdPermission?.Id ?? throw new InvalidOperationException("Failed to get permission ID");
                Console.WriteLine($"[TEST FIXTURE] Created permission via API: {_permissionId}");

                // Property creation is more complex - try to get an existing property or skip
                Console.WriteLine("[TEST FIXTURE] Looking for existing property via API...");
                try
                {
                    var propertiesApi = new PropertiesApi(_httpClient!, _apiConfiguration!);
                    var propertiesResponse = await propertiesApi.GetPropertiesAsync();
                    if (propertiesResponse?.Properties != null && propertiesResponse.Properties.Count > 0)
                    {
                        _propertyId = propertiesResponse.Properties[0].Id ?? string.Empty;
                        _propertyKey = propertiesResponse.Properties[0].Key ?? string.Empty;
                        Console.WriteLine($"[TEST FIXTURE] Using existing property: {_propertyId} (key: {_propertyKey})");
                    }
                    else
                    {
                        Console.WriteLine("[TEST FIXTURE] No properties found - property tests may be skipped");
                    }
                }
                catch (Exception propEx)
                {
                    Console.WriteLine($"[TEST FIXTURE] Warning: Could not get properties: {propEx.Message}");
                }

                Console.WriteLine("[TEST FIXTURE] Creating test role via API...");
                var rolesApi = new RolesApi(_httpClient!, _apiConfiguration!);
                var roleKey = $"test_role_{Guid.NewGuid():N}".Substring(0, 25);
                var roleRequest = new CreateRoleRequest(
                    name: $"Test Role {Guid.NewGuid():N}",
                    key: roleKey
                );
                var roleResponse = await rolesApi.CreateRoleAsync(roleRequest);
                _roleId = roleResponse?.Role?.Id ?? throw new InvalidOperationException("Failed to get role ID");
                Console.WriteLine($"[TEST FIXTURE] Created role via API: {_roleId}");

                Console.WriteLine("[TEST FIXTURE] Getting first available application via API...");
                var appsApi = new ApplicationsApi(_httpClient!, _apiConfiguration!);
                var appsResponse = await appsApi.GetApplicationsAsync();
                if (appsResponse?.Applications != null && appsResponse.Applications.Count > 0)
                {
                    _applicationId = appsResponse.Applications[0].Id ?? string.Empty;
                    Console.WriteLine($"[TEST FIXTURE] Using existing application: {_applicationId}");
                }
                else
                {
                    Console.WriteLine("[TEST FIXTURE] Warning: No applications found (this is OK - some tests may skip)");
                }

                Console.WriteLine("[TEST FIXTURE] All test resources created successfully via API!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TEST FIXTURE] Error creating test resources via API: {ex.Message}");
                Console.WriteLine($"[TEST FIXTURE] Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        /// <summary>
        /// Clean up all test resources via API
        /// </summary>
        private async Task CleanupTestResourcesAsync()
        {
            if (!_initialized || !_config.UseRealApi)
            {
                return;
            }

            try
            {
                Console.WriteLine("[TEST FIXTURE] Cleaning up test resources via API...");

                // Clean up in reverse order of creation to avoid dependency issues
                
                // Delete role via API
                if (!string.IsNullOrEmpty(_roleId))
                {
                    try
                    {
                        Console.WriteLine($"[TEST FIXTURE] Deleting role via API: {_roleId}");
                        var rolesApi = new RolesApi(_httpClient!, _apiConfiguration!);
                        await rolesApi.DeleteRoleAsync(_roleId);
                        Console.WriteLine($"[TEST FIXTURE] Deleted role via API: {_roleId}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[TEST FIXTURE] Warning: Failed to delete role {_roleId}: {ex.Message}");
                    }
                }

                // Note: Property was not created by fixture (we used existing one), so don't delete it

                // Delete permission via API
                if (!string.IsNullOrEmpty(_permissionId))
                {
                    try
                    {
                        Console.WriteLine($"[TEST FIXTURE] Deleting permission via API: {_permissionId}");
                        var permissionsApi = new PermissionsApi(_httpClient!, _apiConfiguration!);
                        await permissionsApi.DeletePermissionAsync(_permissionId);
                        Console.WriteLine($"[TEST FIXTURE] Deleted permission via API: {_permissionId}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[TEST FIXTURE] Warning: Failed to delete permission {_permissionId}: {ex.Message}");
                    }
                }

                // Delete user via API (this will also remove them from organization)
                if (!string.IsNullOrEmpty(_userId))
                {
                    try
                    {
                        Console.WriteLine($"[TEST FIXTURE] Deleting user via API: {_userId}");
                        var usersApi = new UsersApi(_httpClient!, _apiConfiguration!);
                        await usersApi.DeleteUserAsync(_userId, isDeleteProfile: false);
                        Console.WriteLine($"[TEST FIXTURE] Deleted user via API: {_userId}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[TEST FIXTURE] Warning: Failed to delete user {_userId}: {ex.Message}");
                    }
                }

                // Delete organization via API
                if (!string.IsNullOrEmpty(_organizationCode))
                {
                    try
                    {
                        Console.WriteLine($"[TEST FIXTURE] Deleting organization via API: {_organizationCode}");
                        var orgsApi = new OrganizationsApi(_httpClient!, _apiConfiguration!);
                        await orgsApi.DeleteOrganizationAsync(_organizationCode);
                        Console.WriteLine($"[TEST FIXTURE] Deleted organization via API: {_organizationCode}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[TEST FIXTURE] Warning: Failed to delete organization {_organizationCode}: {ex.Message}");
                    }
                }

                // Note: ApplicationId is from existing application, so we don't delete it

                Console.WriteLine("[TEST FIXTURE] Cleanup completed via API");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TEST FIXTURE] Error during cleanup via API: {ex.Message}");
                // Don't throw - cleanup errors shouldn't fail tests
            }
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            // Clean up resources via API
            if (_initialized && _config.UseRealApi)
            {
                CleanupTestResourcesAsync().GetAwaiter().GetResult();
            }

            _httpClient?.Dispose();
            _disposed = true;
        }
    }
}
