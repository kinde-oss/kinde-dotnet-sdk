using Kinde.Api.Client;
using Kinde.Api.Accounts;
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
        private readonly IKindeAccountsClient _accountsClient;

        public Permissions(KindeClient client, IKindeAccountsClient accountsClient, bool forceApi, ILogger logger = null) : base(forceApi, logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _accountsClient = accountsClient ?? throw new ArgumentNullException(nameof(accountsClient));
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
        /// Gets an accounts client for API calls.
        /// </summary>
        /// <returns>An accounts client instance</returns>
        public override IKindeAccountsClient GetAccountsClient()
        {
            return _accountsClient;
        }

        /// <summary>
        /// Check if the user has a specific permission.
        /// </summary>
        /// <param name="permissionKey">The permission key to check</param>
        /// <returns>True if the user has the permission, false otherwise</returns>
        public async Task<bool> HasPermissionAsync(string permissionKey)
        {
            if (string.IsNullOrWhiteSpace(permissionKey))
            {
                _logger?.LogWarning("Permission key cannot be null or empty");
                return false;
            }

            try
            {
                if (ShouldUseApi())
                {
                    // Use API call for hard check
                    _logger?.LogDebug("Checking permission via API (hard check): {PermissionKey}", permissionKey);
                    var hasPermission = await _accountsClient.HasPermissionAsync(permissionKey);
                    _logger?.LogDebug("Permission '{PermissionKey}' API check result: {HasPermission}", permissionKey, hasPermission);
                    return hasPermission;
                }
                else
                {
                    // Use KindeClient's built-in permission checking (token-based)
                    _logger?.LogDebug("Checking permission via KindeClient (token-based): {PermissionKey}", permissionKey);
                    var client = GetClient();
                    if (client != null)
                    {
                        var permission = client.GetPermission(permissionKey);
                        var hasPermission = permission != null;
                        _logger?.LogDebug("Permission '{PermissionKey}' token check result: {HasPermission}", permissionKey, hasPermission);
                        return hasPermission;
                    }

                    _logger?.LogWarning("No KindeClient available for permission check");
                    return false;
                }
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
        public async Task<bool> HasAnyPermissionAsync(IEnumerable<string> permissionKeys)
        {
            if (permissionKeys == null || !permissionKeys.Any())
            {
                _logger?.LogWarning("Permission keys cannot be null or empty");
                return false;
            }

            try
            {
                if (ShouldUseApi())
                {
                    // Use API call for hard check
                    _logger?.LogDebug("Checking any permission via API (hard check)");
                    var hasAnyPermission = await _accountsClient.HasAnyPermissionAsync(permissionKeys);
                    _logger?.LogDebug("Any permission API check result: {HasAnyPermission}", hasAnyPermission);
                    return hasAnyPermission;
                }
                else
                {
                    // Use token-based checking
                    _logger?.LogDebug("Checking any permission via token-based approach");
                    var client = GetClient();
                    if (client != null)
                    {
                        var permissions = client.GetPermissions();
                        var userPermissions = permissions?.Permissions ?? new List<string>();
                        var hasAnyPermission = permissionKeys.Any(key => userPermissions.Contains(key));
                        _logger?.LogDebug("Any permission token check result: {HasAnyPermission}", hasAnyPermission);
                        return hasAnyPermission;
                    }

                    _logger?.LogWarning("No KindeClient available for any permission check");
                    return false;
                }
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
        public async Task<bool> HasAllPermissionsAsync(IEnumerable<string> permissionKeys)
        {
            if (permissionKeys == null || !permissionKeys.Any())
            {
                _logger?.LogWarning("Permission keys cannot be null or empty");
                return false;
            }

            try
            {
                if (ShouldUseApi())
                {
                    // Use API call for hard check
                    _logger?.LogDebug("Checking all permissions via API (hard check)");
                    var hasAllPermissions = await _accountsClient.HasAllPermissionsAsync(permissionKeys);
                    _logger?.LogDebug("All permissions API check result: {HasAllPermissions}", hasAllPermissions);
                    return hasAllPermissions;
                }
                else
                {
                    // Use token-based checking
                    _logger?.LogDebug("Checking all permissions via token-based approach");
                    var client = GetClient();
                    if (client != null)
                    {
                        var permissions = client.GetPermissions();
                        var userPermissions = permissions?.Permissions ?? new List<string>();
                        var hasAllPermissions = permissionKeys.All(key => userPermissions.Contains(key));
                        _logger?.LogDebug("All permissions token check result: {HasAllPermissions}", hasAllPermissions);
                        return hasAllPermissions;
                    }

                    _logger?.LogWarning("No KindeClient available for all permissions check");
                    return false;
                }
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
                if (ShouldUseApi())
                {
                    // Use API call for hard check
                    _logger?.LogDebug("Retrieving permissions via API (hard check)");
                    var response = await _accountsClient.GetPermissionsAsync();
                    var permissions = response?.Permissions?.Select(p => p.Key).ToList() ?? new List<string>();
                    _logger?.LogDebug("Retrieved {Count} permissions from API", permissions.Count);
                    return permissions;
                }
                else
                {
                    // Use KindeClient's built-in permission retrieval (token-based)
                    _logger?.LogDebug("Retrieving permissions via KindeClient (token-based)");
                    var client = GetClient();
                    if (client != null)
                    {
                        var permissionsCollection = client.GetPermissions();
                        var permissions = permissionsCollection?.Permissions ?? new List<string>();
                        _logger?.LogDebug("Retrieved {Count} permissions from KindeClient", permissions.Count);
                        return permissions;
                    }

                    _logger?.LogWarning("No KindeClient available for permissions retrieval");
                    return new List<string>();
                }
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error retrieving permissions");
                return new List<string>();
            }
        }
    }
}
