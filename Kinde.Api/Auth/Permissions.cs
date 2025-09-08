using Kinde.Api.Client;
using Kinde.Api.Models.Tokens;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kinde.Api.Auth
{
    /// <summary>
    /// Client for accessing permissions functionality.
    /// This provides simplified access to user permissions with hard check fallback.
    /// </summary>
    public class Permissions : BaseAuth
    {
        private readonly KindeClient _client;

        public Permissions(KindeClient client = null, ILogger logger = null) : base(logger)
        {
            _client = client;
        }

        /// <summary>
        /// Gets the KindeClient from the current context.
        /// </summary>
        /// <returns>The KindeClient instance if available</returns>
        protected override KindeClient GetClient()
        {
            return _client;
        }

        /// <summary>
        /// Check if the user has a specific permission.
        /// </summary>
        /// <param name="permissionKey">The permission key to check</param>
        /// <returns>True if the user has the permission, false otherwise</returns>
        public virtual async Task<bool> HasPermissionAsync(string permissionKey)
        {
            if (string.IsNullOrWhiteSpace(permissionKey))
            {
                _logger?.LogWarning("Permission key cannot be null or empty");
                return false;
            }

            try
            {
                // First, try to get permissions from token
                var token = GetToken();
                if (token != null)
                {
                    var tokenPermissions = token.GetPermissions();
                    if (tokenPermissions != null && tokenPermissions.Any())
                    {
                        // Check if permission is in token
                        var hasPermission = tokenPermissions.Contains(permissionKey);
                        _logger?.LogDebug("Permission '{PermissionKey}' found in token: {HasPermission}", permissionKey, hasPermission);
                        return hasPermission;
                    }
                }

                // Fall back to API call
                _logger?.LogDebug("No permissions in token, falling back to API for permission: {PermissionKey}", permissionKey);
                var accountsClient = GetAccountsClient();
                if (accountsClient != null)
                {
                    return await accountsClient.HasPermissionAsync(permissionKey);
                }

                _logger?.LogWarning("No accounts client available for permission check");
                return false;
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error checking permission '{PermissionKey}'", permissionKey);
                return false;
            }
        }

        /// <summary>
        /// Check if the user has any of the specified permissions.
        /// </summary>
        /// <param name="permissionKeys">The permission keys to check</param>
        /// <returns>True if the user has any of the permissions, false otherwise</returns>
        public virtual async Task<bool> HasAnyPermissionAsync(IEnumerable<string> permissionKeys)
        {
            if (permissionKeys == null || !permissionKeys.Any())
            {
                _logger?.LogWarning("Permission keys cannot be null or empty");
                return false;
            }

            try
            {
                // First, try to get permissions from token
                var token = GetToken();
                if (token != null)
                {
                    var tokenPermissions = token.GetPermissions();
                    if (tokenPermissions != null && tokenPermissions.Any())
                    {
                        // Check if any permission is in token
                        var hasAnyPermission = permissionKeys.Any(key => tokenPermissions.Contains(key));
                        _logger?.LogDebug("Any permission check in token: {HasAnyPermission}", hasAnyPermission);
                        return hasAnyPermission;
                    }
                }

                // Fall back to API call
                _logger?.LogDebug("No permissions in token, falling back to API for any permission check");
                var accountsClient = GetAccountsClient();
                if (accountsClient != null)
                {
                    return await accountsClient.HasAnyPermissionAsync(permissionKeys);
                }

                _logger?.LogWarning("No accounts client available for any permission check");
                return false;
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error checking any permission");
                return false;
            }
        }

        /// <summary>
        /// Check if the user has all of the specified permissions.
        /// </summary>
        /// <param name="permissionKeys">The permission keys to check</param>
        /// <returns>True if the user has all of the permissions, false otherwise</returns>
        public virtual async Task<bool> HasAllPermissionsAsync(IEnumerable<string> permissionKeys)
        {
            if (permissionKeys == null || !permissionKeys.Any())
            {
                _logger?.LogWarning("Permission keys cannot be null or empty");
                return false;
            }

            try
            {
                // First, try to get permissions from token
                var token = GetToken();
                if (token != null)
                {
                    var tokenPermissions = token.GetPermissions();
                    if (tokenPermissions != null && tokenPermissions.Any())
                    {
                        // Check if all permissions are in token
                        var hasAllPermissions = permissionKeys.All(key => tokenPermissions.Contains(key));
                        _logger?.LogDebug("All permissions check in token: {HasAllPermissions}", hasAllPermissions);
                        return hasAllPermissions;
                    }
                }

                // Fall back to API call
                _logger?.LogDebug("No permissions in token, falling back to API for all permissions check");
                var accountsClient = GetAccountsClient();
                if (accountsClient != null)
                {
                    return await accountsClient.HasAllPermissionsAsync(permissionKeys);
                }

                _logger?.LogWarning("No accounts client available for all permissions check");
                return false;
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error checking all permissions");
                return false;
            }
        }

        /// <summary>
        /// Get all permissions for the current user.
        /// </summary>
        /// <returns>List of permission names, or empty list if none found</returns>
        public async Task<List<string>> GetPermissionsAsync()
        {
            try
            {
                // First, try to get permissions from token
                var token = GetToken();
                if (token != null)
                {
                    var tokenPermissions = token.GetPermissions();
                    if (tokenPermissions != null && tokenPermissions.Any())
                    {
                        _logger?.LogDebug("Retrieved {Count} permissions from token", tokenPermissions.Count);
                        return tokenPermissions.ToList();
                    }
                }

                // Fall back to API call
                _logger?.LogDebug("No permissions in token, falling back to API");
                var accountsClient = GetAccountsClient();
                if (accountsClient != null)
                {
                    var permissions = await accountsClient.GetPermissionsAsync();
                    var permissionsList = permissions?.ToList() ?? new List<string>();
                    _logger?.LogDebug("Retrieved {Count} permissions from API", permissionsList.Count);
                    return permissionsList;
                }

                _logger?.LogWarning("No accounts client available for permissions retrieval");
                return new List<string>();
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error retrieving permissions");
                return new List<string>();
            }
        }

        // ========== Hard Check Methods Implementation ==========

        /// <summary>
        /// Strictly check if the user has a specific permission (hard check).
        /// This method enforces stricter validation than the basic HasPermissionAsync.
        /// </summary>
        /// <param name="permissionKey">The permission key to check</param>
        /// <returns>True if the hard-check passes, false otherwise</returns>
        public virtual async Task<bool> HasPermissionHardCheckAsync(string permissionKey)
        {
            if (string.IsNullOrWhiteSpace(permissionKey))
            {
                _logger?.LogWarning("Permission key cannot be null or empty for hard check");
                return false;
            }

            try
            {
                // First, try to get permissions from token
                var token = GetToken();
                if (token != null)
                {
                    var tokenPermissions = token.GetPermissions();
                    if (tokenPermissions != null && tokenPermissions.Any())
                    {
                        // Check if permission is in token
                        var hasPermission = tokenPermissions.Contains(permissionKey);
                        _logger?.LogDebug("Permission '{PermissionKey}' hard check in token: {HasPermission}", permissionKey, hasPermission);
                        if (hasPermission)
                        {
                            return true;
                        }
                    }
                }

                // Fall back to API call for hard check
                _logger?.LogDebug("Permission '{PermissionKey}' not in token, falling back to API for hard check", permissionKey);
                var accountsClient = GetAccountsClient();
                if (accountsClient != null)
                {
                    var hasPermission = await accountsClient.HasPermissionAsync(permissionKey);
                    _logger?.LogDebug("Permission '{PermissionKey}' hard check from API: {HasPermission}", permissionKey, hasPermission);
                    return hasPermission;
                }

                _logger?.LogWarning("No accounts client available for permission hard check");
                return false;
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error in permission hard check for '{PermissionKey}'", permissionKey);
                return false;
            }
        }

        /// <summary>
        /// Hard check: true if any key passes HasPermissionHardCheckAsync.
        /// </summary>
        /// <param name="permissionKeys">The permission keys to check</param>
        /// <returns>True if any permission passes hard check, false otherwise</returns>
        public virtual async Task<bool> HasAnyPermissionHardCheckAsync(IEnumerable<string> permissionKeys)
        {
            if (permissionKeys == null || !permissionKeys.Any())
            {
                _logger?.LogWarning("Permission keys cannot be null or empty for hard check");
                return false;
            }

            foreach (string key in permissionKeys)
            {
                if (await HasPermissionHardCheckAsync(key))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Hard check: true only if all keys pass HasPermissionHardCheckAsync.
        /// </summary>
        /// <param name="permissionKeys">The permission keys to check</param>
        /// <returns>True if all permissions pass hard check, false otherwise</returns>
        public virtual async Task<bool> HasAllPermissionsHardCheckAsync(IEnumerable<string> permissionKeys)
        {
            if (permissionKeys == null || !permissionKeys.Any())
            {
                _logger?.LogWarning("Permission keys cannot be null or empty for hard check");
                return false;
            }

            foreach (string key in permissionKeys)
            {
                if (!await HasPermissionHardCheckAsync(key))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
