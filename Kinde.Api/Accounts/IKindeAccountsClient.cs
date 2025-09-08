using Kinde.Accounts.Model;

namespace Kinde.Api.Accounts
{
    /// <summary>
    /// Interface for the Kinde Accounts API client
    /// </summary>
    public interface IKindeAccountsClient
    {
        /// <summary>
        /// Gets all entitlements for the current user's organization.
        /// </summary>
        /// <returns>A task containing the entitlements response</returns>
        Task<GetEntitlementsResponse> GetEntitlementsAsync();

        /// <summary>
        /// Gets a specific entitlement by key.
        /// </summary>
        /// <param name="key">The entitlement key to retrieve</param>
        /// <returns>A task containing the entitlement response</returns>
        Task<GetEntitlementResponse> GetEntitlementAsync(string key);

        /// <summary>
        /// Gets all permissions for the current user.
        /// </summary>
        /// <returns>A task containing the permissions response</returns>
        Task<GetUserPermissionsResponse> GetPermissionsAsync();

        /// <summary>
        /// Gets all roles for the current user.
        /// </summary>
        /// <returns>A task containing the roles response</returns>
        Task<GetUserRolesResponse> GetRolesAsync();

        /// <summary>
        /// Gets all feature flags for the current user.
        /// </summary>
        /// <returns>A task containing the feature flags response</returns>
        Task<GetFeatureFlagsResponse> GetFeatureFlagsAsync();

        /// <summary>
        /// Gets a specific feature flag value by key.
        /// </summary>
        /// <param name="flagKey">The feature flag key to retrieve</param>
        /// <returns>A task containing the feature flag value</returns>
        Task<object> GetFeatureFlagValueAsync(string flagKey);
    }
}