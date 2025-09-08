using Kinde.Accounts.Api;
using Kinde.Accounts.Client;
using Kinde.Accounts.Model;
using Microsoft.Extensions.Logging;
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
        private readonly BillingApi _billingApi;
        private readonly PermissionsApi _permissionsApi;
        private readonly RolesApi _rolesApi;
        private readonly FeatureFlagsApi _featureFlagsApi;
        private readonly ILogger<KindeAccountsClient> _logger;

        /// <summary>
        /// Creates a new KindeAccountsClient with the required API clients.
        /// </summary>
        /// <param name="loggerFactory">Logger factory for creating loggers</param>
        /// <param name="billingApi">Billing API client</param>
        /// <param name="permissionsApi">Permissions API client</param>
        /// <param name="rolesApi">Roles API client</param>
        /// <param name="featureFlagsApi">Feature flags API client</param>
        public KindeAccountsClient(
            ILoggerFactory loggerFactory,
            BillingApi billingApi,
            PermissionsApi permissionsApi,
            RolesApi rolesApi,
            FeatureFlagsApi featureFlagsApi)
        {
            _logger = loggerFactory.CreateLogger<KindeAccountsClient>();
            _billingApi = billingApi ?? throw new ArgumentNullException(nameof(billingApi));
            _permissionsApi = permissionsApi ?? throw new ArgumentNullException(nameof(permissionsApi));
            _rolesApi = rolesApi ?? throw new ArgumentNullException(nameof(rolesApi));
            _featureFlagsApi = featureFlagsApi ?? throw new ArgumentNullException(nameof(featureFlagsApi));
        }

        #region Entitlements

        /// <summary>
        /// Gets all entitlements for the current user's organization.
        /// </summary>
        /// <returns>A task containing the entitlements response data</returns>
        public async Task<GetEntitlementsResponseData?> GetEntitlementsAsync()
        {
            try
            {
                _logger.LogDebug("Getting entitlements for current user");
                var response = await _billingApi.GetEntitlementsAsync();
                return response.Ok()?.Data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get entitlements");
                throw new InvalidOperationException("Failed to get entitlements", ex);
            }
        }

        /// <summary>
        /// Gets a specific entitlement by key.
        /// </summary>
        /// <param name="key">The entitlement key to retrieve</param>
        /// <returns>A task containing the entitlement response data</returns>
        public async Task<GetEntitlementResponseData?> GetEntitlementAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Entitlement key cannot be null or empty", nameof(key));

            try
            {
                _logger.LogDebug("Getting entitlement: {Key}", key);
                var response = await _billingApi.GetEntitlementAsync(key);
                return response.Ok()?.Data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get entitlement: {Key}", key);
                throw new InvalidOperationException($"Failed to get entitlement: {key}", ex);
            }
        }

        #endregion

        #region Permissions

        /// <summary>
        /// Gets all permissions for the current user.
        /// </summary>
        /// <returns>A task containing the permissions response data</returns>
        public async Task<GetUserPermissionsResponseData?> GetPermissionsAsync()
        {
            try
            {
                _logger.LogDebug("Getting permissions for current user");
                var response = await _permissionsApi.GetUserPermissionsAsync();
                return response.Ok()?.Data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get permissions");
                throw new InvalidOperationException("Failed to get permissions", ex);
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
                _logger.LogDebug("Checking permission: {PermissionKey}", permissionKey);
                var permissions = await GetPermissionsAsync();
                return permissions?.Permissions?.Any(p => p.Key == permissionKey) ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to check permission: {PermissionKey}", permissionKey);
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
                _logger.LogDebug("Checking any permission from: {PermissionKeys}", string.Join(", ", permissionKeys));
                var permissions = await GetPermissionsAsync();
                var userPermissions = permissions?.Permissions?.Select(p => p.Key).ToList() ?? new List<string>();
                return permissionKeys.Any(key => userPermissions.Contains(key));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to check any permission");
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
                _logger.LogDebug("Checking all permissions from: {PermissionKeys}", string.Join(", ", permissionKeys));
                var permissions = await GetPermissionsAsync();
                var userPermissions = permissions?.Permissions?.Select(p => p.Key).ToList() ?? new List<string>();
                return permissionKeys.All(key => userPermissions.Contains(key));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to check all permissions");
                throw new InvalidOperationException("Failed to check all permissions", ex);
            }
        }

        #endregion

        #region Roles

        /// <summary>
        /// Gets all roles for the current user.
        /// </summary>
        /// <returns>A task containing the roles response data</returns>
        public async Task<GetUserRolesResponseData?> GetRolesAsync()
        {
            try
            {
                _logger.LogDebug("Getting roles for current user");
                var response = await _rolesApi.GetUserRolesAsync();
                return response.Ok()?.Data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get roles");
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
                _logger.LogDebug("Checking role: {RoleKey}", roleKey);
                var roles = await GetRolesAsync();
                return roles?.Roles?.Any(r => r.Key == roleKey) ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to check role: {RoleKey}", roleKey);
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
                _logger.LogDebug("Checking any role from: {RoleKeys}", string.Join(", ", roleKeys));
                var roles = await GetRolesAsync();
                var userRoles = roles?.Roles?.Select(r => r.Key).ToList() ?? new List<string>();
                return roleKeys.Any(key => userRoles.Contains(key));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to check any role");
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
                _logger.LogDebug("Checking all roles from: {RoleKeys}", string.Join(", ", roleKeys));
                var roles = await GetRolesAsync();
                var userRoles = roles?.Roles?.Select(r => r.Key).ToList() ?? new List<string>();
                return roleKeys.All(key => userRoles.Contains(key));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to check all roles");
                throw new InvalidOperationException("Failed to check all roles", ex);
            }
        }

        #endregion

        #region Feature Flags

        /// <summary>
        /// Gets all feature flags for the current user.
        /// </summary>
        /// <returns>A task containing the feature flags response data</returns>
        public async Task<GetFeatureFlagsResponseData?> GetFeatureFlagsAsync()
        {
            try
            {
                _logger.LogDebug("Getting feature flags for current user");
                var response = await _featureFlagsApi.GetFeatureFlagsAsync();
                return response.Ok()?.Data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get feature flags");
                throw new InvalidOperationException("Failed to get feature flags", ex);
            }
        }

        /// <summary>
        /// Checks if a feature flag is enabled for the current user.
        /// </summary>
        /// <param name="flagKey">The feature flag key to check</param>
        /// <returns>A task containing true if the feature flag is enabled, false otherwise</returns>
        public async Task<bool> IsFeatureFlagEnabledAsync(string flagKey)
        {
            if (string.IsNullOrWhiteSpace(flagKey))
                throw new ArgumentException("Feature flag key cannot be null or empty", nameof(flagKey));

            try
            {
                _logger.LogDebug("Checking feature flag: {FlagKey}", flagKey);
                var featureFlags = await GetFeatureFlagsAsync();
                var flag = featureFlags?.FeatureFlags?.FirstOrDefault(f => f.Key == flagKey);
                return flag?.Value?.ToString()?.ToLower() == "true";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to check feature flag: {FlagKey}", flagKey);
                throw new InvalidOperationException($"Failed to check feature flag: {flagKey}", ex);
            }
        }

        /// <summary>
        /// Gets the value of a feature flag for the current user.
        /// </summary>
        /// <param name="flagKey">The feature flag key to retrieve</param>
        /// <returns>A task containing the feature flag value as a string</returns>
        public async Task<string?> GetFeatureFlagValueAsync(string flagKey)
        {
            if (string.IsNullOrWhiteSpace(flagKey))
                throw new ArgumentException("Feature flag key cannot be null or empty", nameof(flagKey));

            try
            {
                _logger.LogDebug("Getting feature flag value: {FlagKey}", flagKey);
                var featureFlags = await GetFeatureFlagsAsync();
                var flag = featureFlags?.FeatureFlags?.FirstOrDefault(f => f.Key == flagKey);
                return flag?.Value?.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get feature flag value: {FlagKey}", flagKey);
                throw new InvalidOperationException($"Failed to get feature flag value: {flagKey}", ex);
            }
        }

        #endregion
    }
}
