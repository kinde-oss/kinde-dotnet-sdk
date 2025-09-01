using Kinde.Api.Models.Configuration;
using Kinde.Api.Models.Tokens;
using Kinde.Api.Models.User;
using Kinde.Accounts.Api;
using Kinde.Accounts.Client;
using Kinde.Accounts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kinde.Api.Accounts
{
    /// <summary>
    /// Client for accessing Kinde Accounts API functionality.
    /// This client provides methods to query the current user's permissions, roles, entitlements, and feature flags.
    /// </summary>
    public class KindeAccountsClient : IKindeAccountsClient
    {
        private readonly DefaultApi _apiClient;
        private readonly Kinde.Api.Client.KindeClient _kindeClient;

        /// <summary>
        /// Creates a new KindeAccountsClient using the provided KindeClient.
        /// </summary>
        /// <param name="kindeClient">The KindeClient instance to use for authentication</param>
        public KindeAccountsClient(Kinde.Api.Client.KindeClient kindeClient)
        {
            _kindeClient = kindeClient ?? throw new ArgumentNullException(nameof(kindeClient));
            _apiClient = new DefaultApi();
            ConfigureApiClient();
        }

        /// <summary>
        /// Creates a new KindeAccountsClient using the provided configuration and token.
        /// </summary>
        /// <param name="configuration">The application configuration</param>
        /// <param name="token">The OAuth token</param>
        public KindeAccountsClient(IApplicationConfiguration configuration, OauthToken token)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (token == null) throw new ArgumentNullException(nameof(token));

            _apiClient = new DefaultApi();
            ConfigureApiClient(configuration, token);
        }

        private void ConfigureApiClient()
        {
            if (_kindeClient?.Token == null)
                throw new InvalidOperationException("No valid token available");

            var configuration = _kindeClient.IdentityProviderConfiguration;
            var token = _kindeClient.Token;

            ConfigureApiClient(configuration, token);
        }

        private void ConfigureApiClient(IApplicationConfiguration configuration, OauthToken token)
        {
            if (string.IsNullOrWhiteSpace(configuration?.Domain))
                throw new ArgumentException("configuration.Domain cannot be null or empty", nameof(configuration));
            if (string.IsNullOrWhiteSpace(token?.AccessToken))
                throw new ArgumentException("token.AccessToken cannot be null or empty", nameof(token));

            var basePath = $"{configuration.Domain.TrimEnd('/')}/account_api/v1";
            var config = new Configuration
            {
                BasePath = basePath,
                AccessToken = token.AccessToken
            };
            
            _apiClient.Configuration = config;
        }

        #region Entitlements

        /// <summary>
        /// Gets all entitlements for the current user's organization.
        /// </summary>
        /// <returns>A task containing the entitlements response</returns>
        public async Task<EntitlementsResponse> GetEntitlementsAsync()
        {
            try
            {
                return await _apiClient.GetEntitlementsAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to get entitlements", ex);
            }
        }

        /// <summary>
        /// Gets a specific entitlement by key.
        /// </summary>
        /// <param name="key">The entitlement key to retrieve</param>
        /// <returns>A task containing the entitlement response</returns>
        public async Task<EntitlementResponse> GetEntitlementAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Entitlement key cannot be null or empty", nameof(key));

            try
            {
                return await _apiClient.GetEntitlementAsync(key);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to get entitlement: {key}", ex);
            }
        }

        #endregion

        #region Permissions

        /// <summary>
        /// Gets all permissions for the current user.
        /// </summary>
        /// <returns>A task containing the permissions response</returns>
        public async Task<PermissionsResponse> GetPermissionsAsync()
        {
            try
            {
                return await _apiClient.GetPermissionsAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to get permissions", ex);
            }
        }

        /// <summary>
        /// Gets a specific permission by key.
        /// </summary>
        /// <param name="key">The permission key to retrieve</param>
        /// <returns>A task containing the permission response</returns>
        public async Task<PermissionResponse> GetPermissionAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Permission key cannot be null or empty", nameof(key));

            try
            {
                return await _apiClient.GetPermissionAsync(key);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to get permission: {key}", ex);
            }
        }

        /// <summary>
        /// Checks if the user has a specific permission.
        /// </summary>
        /// <param name="permissionKey">The permission key to check</param>
        /// <returns>A task containing true if the user has the permission, false otherwise</returns>
        public async Task<bool> HasPermissionAsync(string permissionKey)
        {
            if (string.IsNullOrWhiteSpace(permissionKey))
                throw new ArgumentException("Permission key cannot be null or empty", nameof(permissionKey));

            try
            {
                var permissions = await GetPermissionsAsync();
                return permissions.Data?.Any(p => p.Name == permissionKey) ?? false;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to check permission: {permissionKey}", ex);
            }
        }

        /// <summary>
        /// Checks if the user has any of the specified permissions.
        /// </summary>
        /// <param name="permissionKeys">The permission keys to check</param>
        /// <returns>A task containing true if the user has any of the permissions, false otherwise</returns>
        public async Task<bool> HasAnyPermissionAsync(IEnumerable<string> permissionKeys)
        {
            if (permissionKeys == null || !permissionKeys.Any())
                throw new ArgumentException("Permission keys cannot be null or empty", nameof(permissionKeys));

            try
            {
                var permissions = await GetPermissionsAsync();
                var userPermissions = permissions.Data?.Select(p => p.Name).ToList() ?? new List<string>();
                return permissionKeys.Any(key => userPermissions.Contains(key));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to check any permission", ex);
            }
        }

        /// <summary>
        /// Checks if the user has all of the specified permissions.
        /// </summary>
        /// <param name="permissionKeys">The permission keys to check</param>
        /// <returns>A task containing true if the user has all of the permissions, false otherwise</returns>
        public async Task<bool> HasAllPermissionsAsync(IEnumerable<string> permissionKeys)
        {
            if (permissionKeys == null || !permissionKeys.Any())
                throw new ArgumentException("Permission keys cannot be null or empty", nameof(permissionKeys));

            try
            {
                var permissions = await GetPermissionsAsync();
                var userPermissions = permissions.Data?.Select(p => p.Name).ToList() ?? new List<string>();
                return permissionKeys.All(key => userPermissions.Contains(key));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to check all permissions", ex);
            }
        }

        #endregion

        #region Roles

        /// <summary>
        /// Gets all roles for the current user.
        /// </summary>
        /// <returns>A task containing the roles response</returns>
        public async Task<RolesResponse> GetRolesAsync()
        {
            try
            {
                return await _apiClient.GetRolesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to get roles", ex);
            }
        }

        /// <summary>
        /// Checks if the user has a specific role.
        /// </summary>
        /// <param name="roleKey">The role key to check</param>
        /// <returns>A task containing true if the user has the role, false otherwise</returns>
        public async Task<bool> HasRoleAsync(string roleKey)
        {
            if (string.IsNullOrWhiteSpace(roleKey))
                throw new ArgumentException("Role key cannot be null or empty", nameof(roleKey));

            try
            {
                var roles = await GetRolesAsync();
                return roles.Data?.Any(r => r.Name == roleKey) ?? false;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to check role: {roleKey}", ex);
            }
        }

        /// <summary>
        /// Checks if the user has any of the specified roles.
        /// </summary>
        /// <param name="roleKeys">The role keys to check</param>
        /// <returns>A task containing true if the user has any of the roles, false otherwise</returns>
        public async Task<bool> HasAnyRoleAsync(IEnumerable<string> roleKeys)
        {
            if (roleKeys == null || !roleKeys.Any())
                throw new ArgumentException("Role keys cannot be null or empty", nameof(roleKeys));

            try
            {
                var roles = await GetRolesAsync();
                var userRoles = roles.Data?.Select(r => r.Name).ToList() ?? new List<string>();
                return roleKeys.Any(key => userRoles.Contains(key));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to check any role", ex);
            }
        }

        /// <summary>
        /// Checks if the user has all of the specified roles.
        /// </summary>
        /// <param name="roleKeys">The role keys to check</param>
        /// <returns>A task containing true if the user has all of the roles, false otherwise</returns>
        public async Task<bool> HasAllRolesAsync(IEnumerable<string> roleKeys)
        {
            if (roleKeys == null || !roleKeys.Any())
                throw new ArgumentException("Role keys cannot be null or empty", nameof(roleKeys));

            try
            {
                var roles = await GetRolesAsync();
                var userRoles = roles.Data?.Select(r => r.Name).ToList() ?? new List<string>();
                return roleKeys.All(key => userRoles.Contains(key));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to check all roles", ex);
            }
        }

        #endregion

        #region Feature Flags

        /// <summary>
        /// Gets all feature flags for the current user.
        /// </summary>
        /// <returns>A task containing the feature flags response</returns>
        public async Task<FeatureFlagsResponse> GetFeatureFlagsAsync()
        {
            try
            {
                return await _apiClient.GetFeatureFlagsAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to get feature flags", ex);
            }
        }

        /// <summary>
        /// Gets a specific feature flag by key.
        /// </summary>
        /// <param name="key">The feature flag key to retrieve</param>
        /// <returns>A task containing the feature flag response</returns>
        public async Task<FeatureFlagResponse> GetFeatureFlagAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Feature flag key cannot be null or empty", nameof(key));

            try
            {
                return await _apiClient.GetFeatureFlagAsync(key);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to get feature flag: {key}", ex);
            }
        }

        /// <summary>
        /// Gets the value of a specific feature flag.
        /// </summary>
        /// <param name="flagKey">The feature flag key</param>
        /// <returns>A task containing the feature flag value</returns>
        public async Task<object> GetFeatureFlagValueAsync(string flagKey)
        {
            if (string.IsNullOrWhiteSpace(flagKey))
                throw new ArgumentException("Feature flag key cannot be null or empty", nameof(flagKey));

            try
            {
                var response = await GetFeatureFlagAsync(flagKey);
                return response.Data?.Value;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to get feature flag value: {flagKey}", ex);
            }
        }

        /// <summary>
        /// Checks if a feature flag is enabled (boolean value).
        /// </summary>
        /// <param name="flagKey">The feature flag key</param>
        /// <returns>A task containing true if the feature flag is enabled, false otherwise</returns>
        public async Task<bool> IsFeatureFlagEnabledAsync(string flagKey)
        {
            if (string.IsNullOrWhiteSpace(flagKey))
                throw new ArgumentException("Feature flag key cannot be null or empty", nameof(flagKey));

            try
            {
                var value = await GetFeatureFlagValueAsync(flagKey);
                return value is bool boolValue && boolValue;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to check feature flag: {flagKey}", ex);
            }
        }

        #endregion

        #region User Information

        /// <summary>
        /// Gets all organizations for the current user.
        /// </summary>
        /// <returns>A task containing the user organizations response</returns>
        public async Task<UserOrganizationsResponse> GetUserOrganizationsAsync()
        {
            try
            {
                return await _apiClient.GetUserOrganizationsAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to get user organizations", ex);
            }
        }

        /// <summary>
        /// Gets the current user's profile information.
        /// </summary>
        /// <returns>A task containing the user profile response</returns>
        public async Task<UserProfileResponse> GetUserProfileAsync()
        {
            try
            {
                return await _apiClient.GetUserProfileAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to get user profile", ex);
            }
        }

        /// <summary>
        /// Gets the current organization information.
        /// </summary>
        /// <returns>A task containing the current organization response</returns>
        public async Task<CurrentOrganizationResponse> GetCurrentOrganizationAsync()
        {
            try
            {
                return await _apiClient.GetCurrentOrganizationAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to get current organization", ex);
            }
        }

        #endregion
    }
}
