using Kinde.Api.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Kinde.Api.Models.Tokens
{
    /// <summary>
    /// Utility class for checking permissions, roles, and feature flags with API fallback.
    /// This implements the "hard check" functionality that falls back to the API when
    /// information is not available in the token.
    /// </summary>
    public class KindeTokenChecker
    {
        private readonly OauthToken _token;
        private readonly KindeAccountsClient _accountsClient;
        private readonly ILogger<KindeTokenChecker> _logger;

        /// <summary>
        /// Creates a new KindeTokenChecker with the provided token and accounts client.
        /// </summary>
        /// <param name="token">The OAuth token to check</param>
        /// <param name="accountsClient">The KindeAccountsClient for API fallback</param>
        /// <param name="logger">Optional logger for debugging</param>
        public KindeTokenChecker(OauthToken token, KindeAccountsClient accountsClient, ILogger<KindeTokenChecker> logger = null)
        {
            _token = token ?? throw new ArgumentNullException(nameof(token));
            _accountsClient = accountsClient ?? throw new ArgumentNullException(nameof(accountsClient));
            _logger = logger;
        }

        #region Permission Checks

        /// <summary>
        /// Checks if the user has a specific permission, falling back to API if not in token.
        /// </summary>
        /// <param name="permissionKey">The permission key to check</param>
        /// <returns>A task containing true if the user has the permission, false otherwise</returns>
        public async Task<bool> HasPermissionAsync(string permissionKey)
        {
            if (string.IsNullOrWhiteSpace(permissionKey))
                throw new ArgumentException("Permission key cannot be null or empty", nameof(permissionKey));

            try
            {
                // First, try to get permissions from token
                var tokenPermissions = _token.GetPermissions();

                if (tokenPermissions != null && tokenPermissions.Any())
                {
                    // Check if permission is in token
                    var hasPermission = tokenPermissions.Contains(permissionKey);
                    _logger?.LogDebug("Permission '{PermissionKey}' found in token: {HasPermission}", permissionKey, hasPermission);
                    return hasPermission;
                }
                else
                {
                    // Fall back to API call
                    _logger?.LogDebug("No permissions in token, falling back to API for permission: {PermissionKey}", permissionKey);
                    return await _accountsClient.HasPermissionAsync(permissionKey);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error checking permission '{PermissionKey}'", permissionKey);
                return false;
            }
        }

        /// <summary>
        /// Checks if the user has any of the specified permissions, falling back to API if not in token.
        /// </summary>
        /// <param name="permissionKeys">The permission keys to check</param>
        /// <returns>A task containing true if the user has any of the permissions, false otherwise</returns>
        public async Task<bool> HasAnyPermissionAsync(IEnumerable<string> permissionKeys)
        {
            if (permissionKeys == null || !permissionKeys.Any())
                throw new ArgumentException("Permission keys cannot be null or empty", nameof(permissionKeys));

            try
            {
                // First, try to get permissions from token
                var tokenPermissions = _token.GetPermissions();

                if (tokenPermissions != null && tokenPermissions.Any())
                {
                    // Check if any permission is in token
                    var hasAnyPermission = permissionKeys.Any(key => tokenPermissions.Contains(key));
                    _logger?.LogDebug("Any permission check in token: {HasAnyPermission}", hasAnyPermission);
                    return hasAnyPermission;
                }
                else
                {
                    // Fall back to API call
                    _logger?.LogDebug("No permissions in token, falling back to API for any permission check");
                    return await _accountsClient.HasAnyPermissionAsync(permissionKeys);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error checking any permission");
                return false;
            }
        }

        /// <summary>
        /// Checks if the user has all of the specified permissions, falling back to API if not in token.
        /// </summary>
        /// <param name="permissionKeys">The permission keys to check</param>
        /// <returns>A task containing true if the user has all of the permissions, false otherwise</returns>
        public async Task<bool> HasAllPermissionsAsync(IEnumerable<string> permissionKeys)
        {
            if (permissionKeys == null || !permissionKeys.Any())
                throw new ArgumentException("Permission keys cannot be null or empty", nameof(permissionKeys));

            try
            {
                // First, try to get permissions from token
                var tokenPermissions = _token.GetPermissions();

                if (tokenPermissions != null && tokenPermissions.Any())
                {
                    // Check if all permissions are in token
                    var hasAllPermissions = permissionKeys.All(key => tokenPermissions.Contains(key));
                    _logger?.LogDebug("All permissions check in token: {HasAllPermissions}", hasAllPermissions);
                    return hasAllPermissions;
                }
                else
                {
                    // Fall back to API call
                    _logger?.LogDebug("No permissions in token, falling back to API for all permissions check");
                    return await _accountsClient.HasAllPermissionsAsync(permissionKeys);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error checking all permissions");
                return false;
            }
        }

        #endregion

        #region Role Checks

        /// <summary>
        /// Checks if the user has a specific role, falling back to API if not in token.
        /// </summary>
        /// <param name="roleKey">The role key to check</param>
        /// <returns>A task containing true if the user has the role, false otherwise</returns>
        public async Task<bool> HasRoleAsync(string roleKey)
        {
            if (string.IsNullOrWhiteSpace(roleKey))
                throw new ArgumentException("Role key cannot be null or empty", nameof(roleKey));

            try
            {
                // First, try to get roles from token
                var tokenRoles = _token.GetRoles();

                if (tokenRoles != null && tokenRoles.Any())
                {
                    // Check if role is in token
                    var hasRole = tokenRoles.Contains(roleKey);
                    _logger?.LogDebug("Role '{RoleKey}' found in token: {HasRole}", roleKey, hasRole);
                    return hasRole;
                }
                else
                {
                    // Fall back to API call
                    _logger?.LogDebug("No roles in token, falling back to API for role: {RoleKey}", roleKey);
                    return await _accountsClient.HasRoleAsync(roleKey);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error checking role '{RoleKey}'", roleKey);
                return false;
            }
        }

        /// <summary>
        /// Checks if the user has any of the specified roles, falling back to API if not in token.
        /// </summary>
        /// <param name="roleKeys">The role keys to check</param>
        /// <returns>A task containing true if the user has any of the roles, false otherwise</returns>
        public async Task<bool> HasAnyRoleAsync(IEnumerable<string> roleKeys)
        {
            if (roleKeys == null || !roleKeys.Any())
                throw new ArgumentException("Role keys cannot be null or empty", nameof(roleKeys));

            try
            {
                // First, try to get roles from token
                var tokenRoles = _token.GetRoles();

                if (tokenRoles != null && tokenRoles.Any())
                {
                    // Check if any role is in token
                    var hasAnyRole = roleKeys.Any(key => tokenRoles.Contains(key));
                    _logger?.LogDebug("Any role check in token: {HasAnyRole}", hasAnyRole);
                    return hasAnyRole;
                }
                else
                {
                    // Fall back to API call
                    _logger?.LogDebug("No roles in token, falling back to API for any role check");
                    return await _accountsClient.HasAnyRoleAsync(roleKeys);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error checking any role");
                return false;
            }
        }

        /// <summary>
        /// Checks if the user has all of the specified roles, falling back to API if not in token.
        /// </summary>
        /// <param name="roleKeys">The role keys to check</param>
        /// <returns>A task containing true if the user has all of the roles, false otherwise</returns>
        public async Task<bool> HasAllRolesAsync(IEnumerable<string> roleKeys)
        {
            if (roleKeys == null || !roleKeys.Any())
                throw new ArgumentException("Role keys cannot be null or empty", nameof(roleKeys));

            try
            {
                // First, try to get roles from token
                var tokenRoles = _token.GetRoles();

                if (tokenRoles != null && tokenRoles.Any())
                {
                    // Check if all roles are in token
                    var hasAllRoles = roleKeys.All(key => tokenRoles.Contains(key));
                    _logger?.LogDebug("All roles check in token: {HasAllRoles}", hasAllRoles);
                    return hasAllRoles;
                }
                else
                {
                    // Fall back to API call
                    _logger?.LogDebug("No roles in token, falling back to API for all roles check");
                    return await _accountsClient.HasAllRolesAsync(roleKeys);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error checking all roles");
                return false;
            }
        }

        #endregion

        #region Feature Flag Checks

        /// <summary>
        /// Checks if a feature flag is enabled, falling back to API if not in token.
        /// </summary>
        /// <param name="flagKey">The feature flag key to check</param>
        /// <returns>A task containing true if the feature flag is enabled, false otherwise</returns>
        public async Task<bool> IsFeatureFlagEnabledAsync(string flagKey)
        {
            if (string.IsNullOrWhiteSpace(flagKey))
                throw new ArgumentException("Feature flag key cannot be null or empty", nameof(flagKey));

            try
            {
                // First, try to get feature flags from token
                var tokenFlags = _token.GetFeatureFlags();

                if (tokenFlags != null && tokenFlags.Any())
                {
                    // Check if flag is in token
                    var isEnabled = _token.IsFeatureFlagEnabled(flagKey);
                    _logger?.LogDebug("Feature flag '{FlagKey}' found in token: {IsEnabled}", flagKey, isEnabled);
                    return isEnabled;
                }
                else
                {
                    // Fall back to API call
                    _logger?.LogDebug("No feature flags in token, falling back to API for flag: {FlagKey}", flagKey);
                    return await _accountsClient.IsFeatureFlagEnabledAsync(flagKey);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error checking feature flag '{FlagKey}'", flagKey);
                return false;
            }
        }

        /// <summary>
        /// Gets a feature flag value, falling back to API if not in token.
        /// </summary>
        /// <param name="flagKey">The feature flag key</param>
        /// <returns>A task containing the feature flag value, or null if not found</returns>
        public async Task<object> GetFeatureFlagValueAsync(string flagKey)
        {
            if (string.IsNullOrWhiteSpace(flagKey))
                throw new ArgumentException("Feature flag key cannot be null or empty", nameof(flagKey));

            try
            {
                // First, try to get feature flags from token
                var tokenFlags = _token.GetFeatureFlags();

                if (tokenFlags != null && tokenFlags.Any())
                {
                    // Get flag value from token
                    var value = _token.GetFeatureFlag(flagKey);
                    _logger?.LogDebug("Feature flag '{FlagKey}' value found in token: {Value}", flagKey, value);
                    return value;
                }
                else
                {
                    // Fall back to API call
                    _logger?.LogDebug("No feature flags in token, falling back to API for flag: {FlagKey}", flagKey);
                    return await _accountsClient.GetFeatureFlagValueAsync(flagKey);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting feature flag value '{FlagKey}'", flagKey);
                return null;
            }
        }

        #endregion

        #region Complex Access Control

        /// <summary>
        /// Checks if the user has all of the specified requirements (permissions, roles, and feature flags).
        /// </summary>
        /// <param name="permissions">Optional list of required permissions</param>
        /// <param name="roles">Optional list of required roles</param>
        /// <param name="featureFlags">Optional list of required feature flags</param>
        /// <returns>A task containing true if the user has all requirements, false otherwise</returns>
        public async Task<bool> HasAllAsync(
            IEnumerable<string> permissions = null,
            IEnumerable<string> roles = null,
            IEnumerable<string> featureFlags = null)
        {
            var checks = new List<Task<bool>>();

            if (permissions != null && permissions.Any())
                checks.Add(HasAllPermissionsAsync(permissions));

            if (roles != null && roles.Any())
                checks.Add(HasAllRolesAsync(roles));

            if (featureFlags != null && featureFlags.Any())
            {
                foreach (var flag in featureFlags)
                {
                    checks.Add(IsFeatureFlagEnabledAsync(flag));
                }
            }

            if (!checks.Any())
                return true; // No requirements specified

            try
            {
                var results = await Task.WhenAll(checks);
                return results.All(result => result);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error checking all requirements");
                return false;
            }
        }

        /// <summary>
        /// Checks if the user has any of the specified requirements (permissions, roles, and feature flags).
        /// </summary>
        /// <param name="permissions">Optional list of permissions to check</param>
        /// <param name="roles">Optional list of roles to check</param>
        /// <param name="featureFlags">Optional list of feature flags to check</param>
        /// <returns>A task containing true if the user has any of the requirements, false otherwise</returns>
        public async Task<bool> HasAnyAsync(
            IEnumerable<string> permissions = null,
            IEnumerable<string> roles = null,
            IEnumerable<string> featureFlags = null)
        {
            var checks = new List<Task<bool>>();

            if (permissions != null && permissions.Any())
                checks.Add(HasAnyPermissionAsync(permissions));

            if (roles != null && roles.Any())
                checks.Add(HasAnyRoleAsync(roles));

            if (featureFlags != null && featureFlags.Any())
            {
                foreach (var flag in featureFlags)
                {
                    checks.Add(IsFeatureFlagEnabledAsync(flag));
                }
            }

            if (!checks.Any())
                return true; // No requirements specified

            try
            {
                var results = await Task.WhenAll(checks);
                return results.Any(result => result);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error checking any requirements");
                return false;
            }
        }

        #endregion

        #region Entitlements

        /// <summary>
        /// Gets all entitlements for the current user's organization.
        /// </summary>
        /// <returns>A task containing the entitlements response</returns>
        public async Task<dynamic> GetEntitlementsAsync()
        {
            try
            {
                return await _accountsClient.GetEntitlementsAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting entitlements");
                throw;
            }
        }

        /// <summary>
        /// Gets a specific entitlement by key.
        /// </summary>
        /// <param name="key">The entitlement key to retrieve</param>
        /// <returns>A task containing the entitlement response</returns>
        public async Task<dynamic> GetEntitlementAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Entitlement key cannot be null or empty", nameof(key));

            try
            {
                return await _accountsClient.GetEntitlementAsync(key);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting entitlement '{Key}'", key);
                throw;
            }
        }

        #endregion
    }
}
