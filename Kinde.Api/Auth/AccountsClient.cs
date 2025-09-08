using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kinde.Api.Auth
{
    /// <summary>
    /// Placeholder implementation of accounts client
    /// </summary>
    public class AccountsClient : IAccountsClient
    {
        private readonly ILogger _logger;

        public AccountsClient(ILogger logger = null)
        {
            _logger = logger;
        }

        public Task<bool> HasRoleAsync(string role)
        {
            _logger?.LogWarning("HasRoleAsync not implemented - accounts API integration required");
            throw new NotImplementedException("HasRoleAsync requires accounts API integration");
        }

        public Task<bool> HasAnyRoleAsync(IEnumerable<string> roles)
        {
            _logger?.LogWarning("HasAnyRoleAsync not implemented - accounts API integration required");
            throw new NotImplementedException("HasAnyRoleAsync requires accounts API integration");
        }

        public Task<bool> HasAllRolesAsync(IEnumerable<string> roles)
        {
            _logger?.LogWarning("HasAllRolesAsync not implemented - accounts API integration required");
            throw new NotImplementedException("HasAllRolesAsync requires accounts API integration");
        }

        public Task<IEnumerable<string>> GetRolesAsync()
        {
            _logger?.LogWarning("GetRolesAsync not implemented - accounts API integration required");
            throw new NotImplementedException("GetRolesAsync requires accounts API integration");
        }

        public Task<bool> HasPermissionAsync(string permission)
        {
            _logger?.LogWarning("HasPermissionAsync not implemented - accounts API integration required");
            throw new NotImplementedException("HasPermissionAsync requires accounts API integration");
        }

        public Task<bool> HasAnyPermissionAsync(IEnumerable<string> permissions)
        {
            _logger?.LogWarning("HasAnyPermissionAsync not implemented - accounts API integration required");
            throw new NotImplementedException("HasAnyPermissionAsync requires accounts API integration");
        }

        public Task<bool> HasAllPermissionsAsync(IEnumerable<string> permissions)
        {
            _logger?.LogWarning("HasAllPermissionsAsync not implemented - accounts API integration required");
            throw new NotImplementedException("HasAllPermissionsAsync requires accounts API integration");
        }

        public Task<IEnumerable<string>> GetPermissionsAsync()
        {
            _logger?.LogWarning("GetPermissionsAsync not implemented - accounts API integration required");
            throw new NotImplementedException("GetPermissionsAsync requires accounts API integration");
        }

        public Task<bool> IsFeatureFlagEnabledAsync(string featureFlag)
        {
            _logger?.LogWarning("IsFeatureFlagEnabledAsync not implemented - accounts API integration required");
            throw new NotImplementedException("IsFeatureFlagEnabledAsync requires accounts API integration");
        }

        public Task<object> GetFeatureFlagValueAsync(string featureFlag)
        {
            _logger?.LogWarning("GetFeatureFlagValueAsync not implemented - accounts API integration required");
            throw new NotImplementedException("GetFeatureFlagValueAsync requires accounts API integration");
        }

        public Task<IEnumerable<object>> GetFeatureFlagsAsync()
        {
            _logger?.LogWarning("GetFeatureFlagsAsync not implemented - accounts API integration required");
            throw new NotImplementedException("GetFeatureFlagsAsync requires accounts API integration");
        }
    }
}
