using Kinde.Accounts.Model;
using Kinde.Api.Models.Tokens;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kinde.Api.Accounts
{
    /// <summary>
    /// Interface for accessing Kinde Accounts API functionality.
    /// This interface provides methods to query the current user's permissions, roles, entitlements, and feature flags.
    /// </summary>
    public interface IKindeAccountsClient
    {
        /// <summary>
        /// Gets all entitlements for the current user's organization.
        /// </summary>
        /// <returns>A task containing the entitlements response</returns>
        Task<EntitlementsResponse> GetEntitlementsAsync();

        /// <summary>
        /// Gets all permissions for the current user.
        /// </summary>
        /// <returns>A task containing the permissions response</returns>
        Task<PermissionsResponse> GetPermissionsAsync();

        /// <summary>
        /// Gets a specific entitlement by key.
        /// </summary>
        /// <param name="key">The entitlement key to retrieve</param>
        /// <returns>A task containing the entitlement response</returns>
        Task<EntitlementResponse> GetEntitlementAsync(string key);

        /// <summary>
        /// Checks if the user has a specific permission.
        /// </summary>
        /// <param name="permissionKey">The permission key to check</param>
        /// <returns>A task containing true if the user has the permission, false otherwise</returns>
        Task<bool> HasPermissionAsync(string permissionKey);

        /// <summary>
        /// Checks if the user has any of the specified permissions.
        /// </summary>
        /// <param name="permissionKeys">The permission keys to check</param>
        /// <returns>A task containing true if the user has any of the permissions, false otherwise</returns>
        Task<bool> HasAnyPermissionAsync(IEnumerable<string> permissionKeys);

        /// <summary>
        /// Checks if the user has all of the specified permissions.
        /// </summary>
        /// <param name="permissionKeys">The permission keys to check</param>
        /// <returns>A task containing true if the user has all of the permissions, false otherwise</returns>
        Task<bool> HasAllPermissionsAsync(IEnumerable<string> permissionKeys);

        /// <summary>
        /// Checks if the user has a specific role.
        /// </summary>
        /// <param name="roleKey">The role key to check</param>
        /// <returns>A task containing true if the user has the role, false otherwise</returns>
        Task<bool> HasRoleAsync(string roleKey);

        /// <summary>
        /// Checks if the user has any of the specified roles.
        /// </summary>
        /// <param name="roleKeys">The role keys to check</param>
        /// <returns>A task containing true if the user has any of the roles, false otherwise</returns>
        Task<bool> HasAnyRoleAsync(IEnumerable<string> roleKeys);

        /// <summary>
        /// Checks if the user has all of the specified roles.
        /// </summary>
        /// <param name="roleKeys">The role keys to check</param>
        /// <returns>A task containing true if the user has all of the roles, false otherwise</returns>
        Task<bool> HasAllRolesAsync(IEnumerable<string> roleKeys);

        /// <summary>
        /// Gets all roles for the current user.
        /// </summary>
        /// <returns>A task containing the roles response</returns>
        Task<RolesResponse> GetRolesAsync();

        /// <summary>
        /// Checks if a feature flag is enabled for the current user.
        /// </summary>
        /// <param name="flagKey">The feature flag key to check</param>
        /// <returns>A task containing true if the feature flag is enabled, false otherwise</returns>
        Task<bool> IsFeatureFlagEnabledAsync(string flagKey);

        /// <summary>
        /// Gets the value of a feature flag for the current user.
        /// </summary>
        /// <param name="flagKey">The feature flag key to retrieve</param>
        /// <returns>A task containing the feature flag value</returns>
        Task<object> GetFeatureFlagValueAsync(string flagKey);

        /// <summary>
        /// Gets all feature flags for the current user.
        /// </summary>
        /// <returns>A task containing the feature flags response</returns>
        Task<FeatureFlagsResponse> GetFeatureFlagsAsync();
    }
}
