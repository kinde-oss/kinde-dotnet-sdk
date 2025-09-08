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

        public Permissions(KindeClient client, IKindeAccountsClient accountsClient, ILogger logger = null) : base(logger)
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
                // Use API call for permission checking
                _logger?.LogDebug("Checking permission via API: {PermissionKey}", permissionKey);
                var accountsClient = GetAccountsClient();
                if (accountsClient != null)
                {
                    var response = await accountsClient.GetPermissionsAsync();
                    var hasPermission = response?.Permissions?.Any(p => p.Name == permissionKey) ?? false;
                    return hasPermission;
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
        public async Task<bool> HasAnyPermissionAsync(IEnumerable<string> permissionKeys)
        {
            if (permissionKeys == null || !permissionKeys.Any())
            {
                _logger?.LogWarning("Permission keys cannot be null or empty");
                return false;
            }

            try
            {
                // Use API call for permission checking
                _logger?.LogDebug("Checking any permission via API");
                var accountsClient = GetAccountsClient();
                if (accountsClient != null)
                {
                    var response = await accountsClient.GetPermissionsAsync();
                    var hasAnyPermission = permissionKeys.Any(key => response?.Permissions?.Any(p => p.Name == key) ?? false);
                    return hasAnyPermission;
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
        public async Task<bool> HasAllPermissionsAsync(IEnumerable<string> permissionKeys)
        {
            if (permissionKeys == null || !permissionKeys.Any())
            {
                _logger?.LogWarning("Permission keys cannot be null or empty");
                return false;
            }

            try
            {
                // Use API call for permission checking
                _logger?.LogDebug("Checking all permissions via API");
                var accountsClient = GetAccountsClient();
                if (accountsClient != null)
                {
                    var response = await accountsClient.GetPermissionsAsync();
                    var hasAllPermissions = permissionKeys.All(key => response?.Permissions?.Any(p => p.Name == key) ?? false);
                    return hasAllPermissions;
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
                // Use API call for permission retrieval
                _logger?.LogDebug("Retrieving permissions via API");
                var accountsClient = GetAccountsClient();
                if (accountsClient != null)
                {
                    var response = await accountsClient.GetPermissionsAsync();
                    var permissions = response?.Permissions?.Select(p => p.Name).ToList() ?? new List<string>();
                    _logger?.LogDebug("Retrieved {Count} permissions from API", permissions.Count);
                    return permissions;
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
    }
}
