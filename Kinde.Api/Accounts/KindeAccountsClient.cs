using Kinde.Accounts.Api;
using Kinde.Accounts.Model;
using Kinde.Api.Client;
using Kinde.Api.Models.Configuration;
using Kinde.Api.Models.Tokens;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace Kinde.Api.Accounts
{
    /// <summary>
    /// Client for interacting with the Kinde Accounts API
    /// </summary>
    public class KindeAccountsClient : IKindeAccountsClient
    {
        private readonly IBillingApi _billingApi;
        private readonly IPermissionsApi _permissionsApi;
        private readonly IRolesApi _rolesApi;
        private readonly IFeatureFlagsApi _featureFlagsApi;
        private readonly ILogger<KindeAccountsClient> _logger;

        /// <summary>
        /// Initializes a new instance of the KindeAccountsClient
        /// </summary>
        /// <param name="billingApi">The billing API client</param>
        /// <param name="permissionsApi">The permissions API client</param>
        /// <param name="rolesApi">The roles API client</param>
        /// <param name="featureFlagsApi">The feature flags API client</param>
        /// <param name="logger">The logger</param>
        public KindeAccountsClient(
            IBillingApi billingApi,
            IPermissionsApi permissionsApi,
            IRolesApi rolesApi,
            IFeatureFlagsApi featureFlagsApi,
            ILogger<KindeAccountsClient> logger)
        {
            _billingApi = billingApi ?? throw new ArgumentNullException(nameof(billingApi));
            _permissionsApi = permissionsApi ?? throw new ArgumentNullException(nameof(permissionsApi));
            _rolesApi = rolesApi ?? throw new ArgumentNullException(nameof(rolesApi));
            _featureFlagsApi = featureFlagsApi ?? throw new ArgumentNullException(nameof(featureFlagsApi));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets all entitlements for the current user's organization.
        /// </summary>
        /// <returns>A task containing the entitlements response</returns>
        public async Task<GetEntitlementsResponse> GetEntitlementsAsync()
        {
            try
            {
                var response = await _billingApi.GetEntitlementsAsync();
                if (response.IsOk && response.Ok() != null)
                {
                    return response.Ok()!;
                }
                throw new InvalidOperationException("Failed to get entitlements");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get entitlements");
                throw new InvalidOperationException("Failed to get entitlements", ex);
            }
        }

        /// <summary>
        /// Gets a specific entitlement by key.
        /// </summary>
        /// <param name="key">The entitlement key to retrieve</param>
        /// <returns>A task containing the entitlement response</returns>
        public async Task<GetEntitlementResponse> GetEntitlementAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Entitlement key cannot be null or empty", nameof(key));

            try
            {
                var response = await _billingApi.GetEntitlementAsync(key);
                if (response.IsOk && response.Ok() != null)
                {
                    return response.Ok()!;
                }
                throw new InvalidOperationException($"Failed to get entitlement: {key}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get entitlement: {Key}", key);
                throw new InvalidOperationException($"Failed to get entitlement: {key}", ex);
            }
        }

        /// <summary>
        /// Gets all permissions for the current user.
        /// </summary>
        /// <returns>A task containing the permissions response</returns>
        public async Task<GetUserPermissionsResponse> GetPermissionsAsync()
        {
            try
            {
                var response = await _permissionsApi.GetUserPermissionsAsync();
                if (response.IsOk && response.Ok() != null)
                {
                    return response.Ok()!;
                }
                throw new InvalidOperationException("Failed to get permissions");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get permissions");
                throw new InvalidOperationException("Failed to get permissions", ex);
            }
        }

        /// <summary>
        /// Gets all roles for the current user.
        /// </summary>
        /// <returns>A task containing the roles response</returns>
        public async Task<GetUserRolesResponse> GetRolesAsync()
        {
            try
            {
                var response = await _rolesApi.GetUserRolesAsync();
                if (response.IsOk && response.Ok() != null)
                {
                    return response.Ok()!;
                }
                throw new InvalidOperationException("Failed to get roles");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get roles");
                throw new InvalidOperationException("Failed to get roles", ex);
            }
        }

        /// <summary>
        /// Gets all feature flags for the current user.
        /// </summary>
        /// <returns>A task containing the feature flags response</returns>
        public async Task<GetFeatureFlagsResponse> GetFeatureFlagsAsync()
        {
            try
            {
                var response = await _featureFlagsApi.GetFeatureFlagsAsync();
                if (response.IsOk && response.Ok() != null)
                {
                    return response.Ok()!;
                }
                throw new InvalidOperationException("Failed to get feature flags");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get feature flags");
                throw new InvalidOperationException("Failed to get feature flags", ex);
            }
        }

        /// <summary>
        /// Gets a specific feature flag value by key.
        /// </summary>
        /// <param name="flagKey">The feature flag key to retrieve</param>
        /// <returns>A task containing the feature flag value</returns>
        public async Task<object> GetFeatureFlagValueAsync(string flagKey)
        {
            if (string.IsNullOrWhiteSpace(flagKey))
                throw new ArgumentException("Feature flag key cannot be null or empty", nameof(flagKey));

            try
            {
                var response = await GetFeatureFlagsAsync();
                var flag = response.Data?.FeatureFlags?.FirstOrDefault(f => f.Key == flagKey);
                return flag?.Value?.ToString() ?? "false";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get feature flag value: {FlagKey}", flagKey);
                throw new InvalidOperationException($"Failed to get feature flag value: {flagKey}", ex);
            }
        }
    }
}