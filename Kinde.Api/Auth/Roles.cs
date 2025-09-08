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
        public virtual async Task<bool> HasRoleAsync(string roleKey)
        {
            roleKey = roleKey?.Trim();
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
                        var hasRole = tokenRoles.Any(r => string.Equals(r?.Trim(), roleKey, StringComparison.OrdinalIgnoreCase));
                        _logger?.LogDebug("Role '{RoleKey}' found in token: {HasRole}", roleKey, hasRole);
                        return hasRole;
                    }
                }

                // Fall back to API call
                _logger?.LogDebug("No roles in token, falling back to API for role: {RoleKey}", roleKey);
                var accountsClient = GetAccountsClient();
                return await accountsClient.HasRoleAsync(roleKey).ConfigureAwait(false);
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
        public virtual async Task<bool> HasAnyRoleAsync(IEnumerable<string> roleKeys)
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
                        var normalized = new HashSet<string>(
                            tokenRoles.Where(r => !string.IsNullOrWhiteSpace(r)).Select(r => r.Trim()),
                            StringComparer.OrdinalIgnoreCase);
                        var hasAnyRole = roleKeys.Any(key => !string.IsNullOrWhiteSpace(key) && normalized.Contains(key.Trim()));
                        _logger?.LogDebug("Any role check in token: {HasAnyRole}", hasAnyRole);
                        return hasAnyRole;
                    }
                }

                // Fall back to API call
                _logger?.LogDebug("No roles in token, falling back to API for any role check");
                var accountsClient = GetAccountsClient();
                return await accountsClient.HasAnyRoleAsync(roleKeys);
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
        public virtual async Task<bool> HasAllRolesAsync(IEnumerable<string> roleKeys)
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
                        var normalized = new HashSet<string>(
                            tokenRoles.Where(r => !string.IsNullOrWhiteSpace(r)).Select(r => r.Trim()),
                            StringComparer.OrdinalIgnoreCase);
                        var hasAllRoles = roleKeys.All(key => !string.IsNullOrWhiteSpace(key) && normalized.Contains(key.Trim()));
                        _logger?.LogDebug("All roles check in token: {HasAllRoles}", hasAllRoles);
                        return hasAllRoles;
                    }
                }

                // Fall back to API call
                _logger?.LogDebug("No roles in token, falling back to API for all roles check");
                var accountsClient = GetAccountsClient();
                return await accountsClient.HasAllRolesAsync(roleKeys);
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
                    var roles = await accountsClient.GetRolesAsync();
                    var rolesList = roles?.ToList() ?? new List<string>();
                    _logger?.LogDebug("Retrieved {Count} roles from API", rolesList.Count);
                    return rolesList;
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

        // ========== Hard Check Methods Implementation ==========

        /// <summary>
        /// Strictly check if the user has a specific role (hard check).
        /// This method enforces stricter validation than the basic HasRoleAsync.
        /// </summary>
        /// <param name="roleKey">The role key to check</param>
        /// <returns>True if the hard-check passes, false otherwise</returns>
        public virtual async Task<bool> HasRoleHardCheckAsync(string roleKey)
        {
            if (string.IsNullOrWhiteSpace(roleKey))
            {
                _logger?.LogWarning("Role key cannot be null or empty for hard check");
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
                        var hasRole = tokenRoles.Any(r => string.Equals(r?.Trim(), roleKey, StringComparison.OrdinalIgnoreCase));
                        _logger?.LogDebug("Role '{RoleKey}' hard check in token: {HasRole}", roleKey, hasRole);
                        if (hasRole)
                        {
                            return true;
                        }
                    }
                }

                // Fall back to API call for hard check
                _logger?.LogDebug("Role '{RoleKey}' not in token, falling back to API for hard check", roleKey);
                var accountsClient = GetAccountsClient();
                var apiHasRole = await accountsClient.HasRoleAsync(roleKey);
                _logger?.LogDebug("Role '{RoleKey}' hard check from API: {HasRole}", roleKey, apiHasRole);
                return apiHasRole;
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error in role hard check for '{RoleKey}'", roleKey);
                return false;
            }
        }

        /// <summary>
        /// Hard check: true if any key passes HasRoleHardCheckAsync.
        /// </summary>
        /// <param name="roleKeys">The role keys to check</param>
        /// <returns>True if any role passes hard check, false otherwise</returns>
        public virtual async Task<bool> HasAnyRoleHardCheckAsync(IEnumerable<string> roleKeys)
        {
            if (roleKeys == null || !roleKeys.Any())
            {
                _logger?.LogWarning("Role keys cannot be null or empty for hard check");
                return false;
            }

            foreach (string key in roleKeys)
            {
                if (await HasRoleHardCheckAsync(key))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Hard check: true only if all keys pass HasRoleHardCheckAsync.
        /// </summary>
        /// <param name="roleKeys">The role keys to check</param>
        /// <returns>True if all roles pass hard check, false otherwise</returns>
        public virtual async Task<bool> HasAllRolesHardCheckAsync(IEnumerable<string> roleKeys)
        {
            if (roleKeys == null || !roleKeys.Any())
            {
                _logger?.LogWarning("Role keys cannot be null or empty for hard check");
                return false;
            }

            foreach (string key in roleKeys)
            {
                if (!await HasRoleHardCheckAsync(key))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
