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

        public Roles(KindeClient client, IKindeAccountsClient accountsClient, bool forceApi, ILogger logger = null) : base(forceApi, logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _accountsClient = accountsClient; // Can be null when ForceApi is false
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
                if (ShouldUseApi())
                {
                    // Use API call for hard check
                    _logger?.LogDebug("Checking role via API (hard check): {RoleKey}", roleKey);
                    if (_accountsClient == null)
                    {
                        _logger?.LogWarning("ForceApi is enabled but accounts client is not available. Falling back to token-based check.");
                        // For token-based role checking, we'll use the claims approach
                        var client = GetClient();
                        if (client != null)
                        {
                            var claim = client.GetClaim("roles");
                            if (claim != null)
                            {
                                var roles = claim.Value?.ToString();
                                var hasRole = !string.IsNullOrEmpty(roles) && roles.Contains(roleKey);
                                _logger?.LogDebug("Role '{RoleKey}' token check result: {HasRole}", roleKey, hasRole);
                                return hasRole;
                            }
                        }
                        _logger?.LogWarning("No KindeClient available for role check");
                        return false;
                    }
                    var apiHasRole = await _accountsClient.HasRoleAsync(roleKey);
                    _logger?.LogDebug("Role '{RoleKey}' API check result: {HasRole}", roleKey, apiHasRole);
                    return apiHasRole;
                }
                else
                {
                    // Use token-based approach for role checking
                    _logger?.LogDebug("Checking role via token-based approach: {RoleKey}", roleKey);
                    var client = GetClient();
                    if (client != null)
                    {
                        // For token-based role checking, we'll use the claims approach
                        // since KindeClient doesn't have a direct GetRoles method
                        var claim = client.GetClaim("roles");
                        if (claim != null)
                        {
                            var roles = claim.Value?.ToString();
                            var tokenHasRole = !string.IsNullOrEmpty(roles) && roles.Contains(roleKey);
                            _logger?.LogDebug("Role '{RoleKey}' token check result: {HasRole}", roleKey, tokenHasRole);
                            return tokenHasRole;
                        }
                    }

                    _logger?.LogWarning("No KindeClient available for role check");
                    return false;
                }
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
                if (ShouldUseApi())
                {
                    // Use API call for hard check
                    _logger?.LogDebug("Checking any role via API (hard check)");
                    if (_accountsClient == null)
                    {
                        _logger?.LogWarning("ForceApi is enabled but accounts client is not available. Falling back to token-based check.");
                        // Use token-based approach for role checking
                        var client = GetClient();
                        if (client != null)
                        {
                            var claim = client.GetClaim("roles");
                            if (claim != null)
                            {
                                var roles = claim.Value?.ToString();
                                var hasAnyRole = !string.IsNullOrEmpty(roles) && roleKeys.Any(key => roles.Contains(key));
                                _logger?.LogDebug("Any role token check result: {HasAnyRole}", hasAnyRole);
                                return hasAnyRole;
                            }
                        }
                        _logger?.LogWarning("No KindeClient available for any role check");
                        return false;
                    }
                    var apiHasAnyRole = await _accountsClient.HasAnyRoleAsync(roleKeys);
                    _logger?.LogDebug("Any role API check result: {HasAnyRole}", apiHasAnyRole);
                    return apiHasAnyRole;
                }
                else
                {
                    // Use token-based approach for role checking
                    _logger?.LogDebug("Checking any role via token-based approach");
                    var client = GetClient();
                    if (client != null)
                    {
                        var claim = client.GetClaim("roles");
                        if (claim != null)
                        {
                            var roles = claim.Value?.ToString();
                            var tokenHasAnyRole = !string.IsNullOrEmpty(roles) && roleKeys.Any(key => roles.Contains(key));
                            _logger?.LogDebug("Any role token check result: {HasAnyRole}", tokenHasAnyRole);
                            return tokenHasAnyRole;
                        }
                    }

                    _logger?.LogWarning("No KindeClient available for any role check");
                    return false;
                }
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
                if (ShouldUseApi())
                {
                    // Use API call for hard check
                    _logger?.LogDebug("Checking all roles via API (hard check)");
                    if (_accountsClient == null)
                    {
                        _logger?.LogWarning("ForceApi is enabled but accounts client is not available. Falling back to token-based check.");
                        // Use token-based approach for role checking
                        var client = GetClient();
                        if (client != null)
                        {
                            var claim = client.GetClaim("roles");
                            if (claim != null)
                            {
                                var roles = claim.Value?.ToString();
                                var hasAllRoles = !string.IsNullOrEmpty(roles) && roleKeys.All(key => roles.Contains(key));
                                _logger?.LogDebug("All roles token check result: {HasAllRoles}", hasAllRoles);
                                return hasAllRoles;
                            }
                        }
                        _logger?.LogWarning("No KindeClient available for all roles check");
                        return false;
                    }
                    var apiHasAllRoles = await _accountsClient.HasAllRolesAsync(roleKeys);
                    _logger?.LogDebug("All roles API check result: {HasAllRoles}", apiHasAllRoles);
                    return apiHasAllRoles;
                }
                else
                {
                    // Use token-based approach for role checking
                    _logger?.LogDebug("Checking all roles via token-based approach");
                    var client = GetClient();
                    if (client != null)
                    {
                        var claim = client.GetClaim("roles");
                        if (claim != null)
                        {
                            var roles = claim.Value?.ToString();
                            var tokenHasAllRoles = !string.IsNullOrEmpty(roles) && roleKeys.All(key => roles.Contains(key));
                            _logger?.LogDebug("All roles token check result: {HasAllRoles}", tokenHasAllRoles);
                            return tokenHasAllRoles;
                        }
                    }

                    _logger?.LogWarning("No KindeClient available for all roles check");
                    return false;
                }
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
                if (ShouldUseApi())
                {
                    // Use API call for hard check
                    _logger?.LogDebug("Retrieving roles via API (hard check)");
                    if (_accountsClient == null)
                    {
                        _logger?.LogWarning("ForceApi is enabled but accounts client is not available. Falling back to token-based check.");
                        // Use token-based approach for role retrieval
                        var client = GetClient();
                        if (client != null)
                        {
                            var claim = client.GetClaim("roles");
                            if (claim != null)
                            {
                            var roles = claim.Value?.ToString();
                            var tokenRoleList = !string.IsNullOrEmpty(roles) ? roles.Split(',').Select(r => r.Trim()).ToList() : new List<string>();
                            _logger?.LogDebug("Retrieved {Count} roles from KindeClient", tokenRoleList.Count);
                            return tokenRoleList;
                            }
                        }
                        _logger?.LogWarning("No KindeClient available for roles retrieval");
                        return new List<string>();
                    }
                    var response = await _accountsClient.GetRolesAsync();
                    var apiRoles = response?.Roles?.Select(r => r.Key).ToList() ?? new List<string>();
                    _logger?.LogDebug("Retrieved {Count} roles from API", apiRoles.Count);
                    return apiRoles;
                }
                else
                {
                    // Use token-based approach for role retrieval
                    _logger?.LogDebug("Retrieving roles via token-based approach");
                    var client = GetClient();
                    if (client != null)
                    {
                        var claim = client.GetClaim("roles");
                        if (claim != null)
                        {
                            var roles = claim.Value?.ToString();
                            var roleNames = !string.IsNullOrEmpty(roles) ? roles.Split(',').Select(r => r.Trim()).ToList() : new List<string>();
                            _logger?.LogDebug("Retrieved {Count} roles from token", roleNames.Count);
                            return roleNames;
                        }
                    }

                    _logger?.LogWarning("No KindeClient available for roles retrieval");
                    return new List<string>();
                }
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error retrieving roles");
                return new List<string>();
            }
        }
    }
}
