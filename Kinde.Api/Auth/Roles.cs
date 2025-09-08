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
    /// Client for accessing roles functionality.
    /// This provides simplified access to user roles with hard check fallback.
    /// </summary>
    public class Roles : BaseAuth
    {
        private readonly KindeClient _client;
        private readonly IKindeAccountsClient _accountsClient;

        public Roles(KindeClient client, IKindeAccountsClient accountsClient, ILogger logger = null) : base(logger)
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
        /// Check if the user has a specific role.
        /// </summary>
        /// <param name="roleKey">The role key to check</param>
        /// <returns>True if the user has the role, false otherwise</returns>
        public async Task<bool> HasRoleAsync(string roleKey)
        {
            roleKey = roleKey?.Trim();
            if (string.IsNullOrWhiteSpace(roleKey))
            {
                _logger?.LogWarning("Role key cannot be null or empty");
                return false;
            }

            try
            {
                // Note: OauthToken doesn't have GetRoles method in this implementation
                // We'll rely on the API call for role checking

                // Use API call for role checking
                _logger?.LogDebug("Checking role via API: {RoleKey}", roleKey);
                var accountsClient = GetAccountsClient();
                if (accountsClient != null)
                {
                    var response = await accountsClient.GetRolesAsync();
                    var hasRole = response?.Roles?.Any(r => string.Equals(r.Name?.Trim(), roleKey, StringComparison.OrdinalIgnoreCase)) ?? false;
                    return hasRole;
                }

                _logger?.LogWarning("No accounts client available for role check");
                return false;
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error checking role '{RoleKey}'", roleKey);
                return false;
            }
        }

        /// <summary>
        /// Check if the user has any of the specified roles.
        /// </summary>
        /// <param name="roleKeys">The role keys to check</param>
        /// <returns>True if the user has any of the roles, false otherwise</returns>
        public async Task<bool> HasAnyRoleAsync(IEnumerable<string> roleKeys)
        {
            if (roleKeys == null || !roleKeys.Any())
            {
                _logger?.LogWarning("Role keys cannot be null or empty");
                return false;
            }

            try
            {
                // Use API call for role checking
                _logger?.LogDebug("Checking any role via API");
                var accountsClient = GetAccountsClient();
                if (accountsClient != null)
                {
                    var response = await accountsClient.GetRolesAsync();
                    var hasAnyRole = roleKeys.Any(key => response?.Roles?.Any(r => string.Equals(r.Name?.Trim(), key, StringComparison.OrdinalIgnoreCase)) ?? false);
                    return hasAnyRole;
                }

                _logger?.LogWarning("No accounts client available for any role check");
                return false;
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error checking any role");
                return false;
            }
        }

        /// <summary>
        /// Check if the user has all of the specified roles.
        /// </summary>
        /// <param name="roleKeys">The role keys to check</param>
        /// <returns>True if the user has all of the roles, false otherwise</returns>
        public async Task<bool> HasAllRolesAsync(IEnumerable<string> roleKeys)
        {
            if (roleKeys == null || !roleKeys.Any())
            {
                _logger?.LogWarning("Role keys cannot be null or empty");
                return false;
            }

            try
            {
                // Use API call for role checking
                _logger?.LogDebug("Checking all roles via API");
                var accountsClient = GetAccountsClient();
                if (accountsClient != null)
                {
                    var response = await accountsClient.GetRolesAsync();
                    var hasAllRoles = roleKeys.All(key => response?.Roles?.Any(r => string.Equals(r.Name?.Trim(), key, StringComparison.OrdinalIgnoreCase)) ?? false);
                    return hasAllRoles;
                }

                _logger?.LogWarning("No accounts client available for all roles check");
                return false;
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error checking all roles");
                return false;
            }
        }

        /// <summary>
        /// Get all roles for the current user.
        /// </summary>
        /// <returns>List of role names, or empty list if none found</returns>
        public async Task<List<string>> GetRolesAsync()
        {
            try
            {
                // Use API call for role retrieval
                _logger?.LogDebug("Retrieving roles via API");
                var accountsClient = GetAccountsClient();
                if (accountsClient != null)
                {
                    var response = await accountsClient.GetRolesAsync();
                    var roles = response?.Roles?.Select(r => r.Name).ToList() ?? new List<string>();
                    _logger?.LogDebug("Retrieved {Count} roles from API", roles.Count);
                    return roles;
                }

                _logger?.LogWarning("No accounts client available for roles retrieval");
                return new List<string>();
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error retrieving roles");
                return new List<string>();
            }
        }
    }
}
