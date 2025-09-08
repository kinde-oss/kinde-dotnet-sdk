using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kinde.Api.Auth
{
    /// <summary>
    /// Interface for accounts client functionality
    /// </summary>
    public interface IAccountsClient
    {
        Task<bool> HasRoleAsync(string role);
        Task<bool> HasAnyRoleAsync(IEnumerable<string> roles);
        Task<bool> HasAllRolesAsync(IEnumerable<string> roles);
        Task<IEnumerable<string>> GetRolesAsync();
        Task<bool> HasPermissionAsync(string permission);
        Task<bool> HasAnyPermissionAsync(IEnumerable<string> permissions);
        Task<bool> HasAllPermissionsAsync(IEnumerable<string> permissions);
        Task<IEnumerable<string>> GetPermissionsAsync();
        Task<bool> IsFeatureFlagEnabledAsync(string featureFlag);
        Task<object> GetFeatureFlagValueAsync(string featureFlag);
        Task<IEnumerable<object>> GetFeatureFlagsAsync();
    }
}
