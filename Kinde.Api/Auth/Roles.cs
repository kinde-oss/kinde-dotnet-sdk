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
    /// Client for accessing roles functionality.
    /// This provides simplified access to user roles with hard check fallback.
    /// </summary>
    public class Roles : BaseAuth
    {
        private readonly KindeClient _client;

        public Roles(KindeClient client = null, ILogger logger = null) : base(logger)
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
        /// Check if the user has a specific role.
        /// </summary>
        /// <param name="roleKey">The role key to check</param>
        /// <returns>True if the user has the role, false otherwise</returns>
        public async Task<bool> HasRoleAsync(string roleKey)
        {
            if (string.IsNullOrWhiteSpace(roleKey))
            {
                _logger?.LogWarning("Role key cannot be null or empty");
                return false;
            }

            try
            {
                // First, try to get roles from token
                var token = GetToken();
                if (token != null)
                {
                    var tokenRoles = token.GetRoles();
                    if (tokenRoles != null && tokenRoles.Any())
                    {
                        // Check if role is in token
                        var hasRole = tokenRoles.Contains(roleKey);
                        _logger?.LogDebug("Role '{RoleKey}' found in token: {HasRole}", roleKey, hasRole);
                        return hasRole;
                    }
                }

                // Fall back to API call
                _logger?.LogDebug("No roles in token, falling back to API for role: {RoleKey}", roleKey);
                var accountsClient = GetAccountsClient();
                if (accountsClient != null)
                {
                    return await accountsClient.HasRoleAsync(roleKey);
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
                // First, try to get roles from token
                var token = GetToken();
                if (token != null)
                {
                    var tokenRoles = token.GetRoles();
                    if (tokenRoles != null && tokenRoles.Any())
                    {
                        // Check if any role is in token
                        var hasAnyRole = roleKeys.Any(key => tokenRoles.Contains(key));
                        _logger?.LogDebug("Any role check in token: {HasAnyRole}", hasAnyRole);
                        return hasAnyRole;
                    }
                }

                // Fall back to API call
                _logger?.LogDebug("No roles in token, falling back to API for any role check");
                var accountsClient = GetAccountsClient();
                if (accountsClient != null)
                {
                    return await accountsClient.HasAnyRoleAsync(roleKeys);
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
                // First, try to get roles from token
                var token = GetToken();
                if (token != null)
                {
                    var tokenRoles = token.GetRoles();
                    if (tokenRoles != null && tokenRoles.Any())
                    {
                        // Check if all roles are in token
                        var hasAllRoles = roleKeys.All(key => tokenRoles.Contains(key));
                        _logger?.LogDebug("All roles check in token: {HasAllRoles}", hasAllRoles);
                        return hasAllRoles;
                    }
                }

                // Fall back to API call
                _logger?.LogDebug("No roles in token, falling back to API for all roles check");
                var accountsClient = GetAccountsClient();
                if (accountsClient != null)
                {
                    return await accountsClient.HasAllRolesAsync(roleKeys);
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
                // First, try to get roles from token
                var token = GetToken();
                if (token != null)
                {
                    var tokenRoles = token.GetRoles();
                    if (tokenRoles != null && tokenRoles.Any())
                    {
                        _logger?.LogDebug("Retrieved {Count} roles from token", tokenRoles.Count);
                        return tokenRoles.ToList();
                    }
                }

                // Fall back to API call
                _logger?.LogDebug("No roles in token, falling back to API");
                var accountsClient = GetAccountsClient();
                if (accountsClient != null)
                {
                    var response = await accountsClient.GetRolesAsync();
                    var roles = response.Data?.Select(r => r.Name).ToList() ?? new List<string>();
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
