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
    /// Client for accessing feature flags functionality.
    /// This provides simplified access to feature flags with hard check fallback.
    /// </summary>
    public class FeatureFlags : BaseAuth
    {
        private readonly KindeClient _client;
        private readonly IKindeAccountsClient _accountsClient;

        public FeatureFlags(KindeClient client, IKindeAccountsClient accountsClient, ILogger logger = null) : base(logger)
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
        /// Check if a feature flag is enabled.
        /// </summary>
        /// <param name="flagKey">The feature flag key to check</param>
        /// <returns>True if the feature flag is enabled, false otherwise</returns>
        public async Task<bool> IsFeatureFlagEnabledAsync(string flagKey)
        {
            if (string.IsNullOrWhiteSpace(flagKey))
            {
                _logger?.LogWarning("Feature flag key cannot be null or empty");
                return false;
            }

            try
            {
                // Use API call for feature flag checking
                _logger?.LogDebug("Checking feature flag via API: {FlagKey}", flagKey);
                var accountsClient = GetAccountsClient();
                if (accountsClient != null)
                {
                    var response = await accountsClient.GetFeatureFlagsAsync();
                    var flag = response?.FeatureFlags?.FirstOrDefault(f => f.Key == flagKey);
                    var isEnabled = flag?.Value?.ToString()?.ToLower() == "true" || flag?.Value?.ToString() == "1";
                    return isEnabled;
                }

                _logger?.LogWarning("No accounts client available for feature flag check");
                return false;
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error checking feature flag '{FlagKey}'", flagKey);
                return false;
            }
        }

        /// <summary>
        /// Get a feature flag value.
        /// </summary>
        /// <param name="flagKey">The feature flag key</param>
        /// <returns>The feature flag value, or null if not found</returns>
        public async Task<object> GetFeatureFlagValueAsync(string flagKey)
        {
            if (string.IsNullOrWhiteSpace(flagKey))
            {
                _logger?.LogWarning("Feature flag key cannot be null or empty");
                return null;
            }

            try
            {
                // Use API call for feature flag value
                _logger?.LogDebug("Getting feature flag value via API: {FlagKey}", flagKey);
                var accountsClient = GetAccountsClient();
                if (accountsClient != null)
                {
                    return await accountsClient.GetFeatureFlagValueAsync(flagKey);
                }

                _logger?.LogWarning("No accounts client available for feature flag value retrieval");
                return null;
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error getting feature flag value '{FlagKey}'", flagKey);
                return null;
            }
        }

        /// <summary>
        /// Get a feature flag value as a specific type.
        /// </summary>
        /// <typeparam name="T">The type to convert the flag value to</typeparam>
        /// <param name="flagKey">The feature flag key</param>
        /// <returns>The feature flag value as the specified type, or default(T) if not found</returns>
        public async Task<T> GetFeatureFlagValueAsync<T>(string flagKey)
        {
            var value = await GetFeatureFlagValueAsync(flagKey);
            if (value == null)
                return default(T);

            try
            {
                if (value is T typedValue)
                    return typedValue;

                // Bool: accept "true"/"false" and "1"/"0"
                if (typeof(T) == typeof(bool))
                {
                    var s = Convert.ToString(value)?.Trim();
                    var b = bool.TryParse(s, out var parsed) ? parsed : string.Equals(s, "1", StringComparison.Ordinal);
                    return (T)(object)b;
                }
                // Enums: parse names case-insensitively
                if (typeof(T).IsEnum)
                {
                    return (T)Enum.Parse(typeof(T), Convert.ToString(value), ignoreCase: true);
                }
                // Fallback using invariant culture
                return (T)Convert.ChangeType(value, typeof(T), System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error converting feature flag '{FlagKey}' to type {Type}", flagKey, typeof(T).Name);
                return default(T);
            }
        }

        /// <summary>
        /// Get a feature flag value as a string.
        /// </summary>
        /// <param name="flagKey">The feature flag key</param>
        /// <returns>The feature flag value as a string, or null if not found</returns>
        public async Task<string> GetFeatureFlagStringAsync(string flagKey)
        {
            return await GetFeatureFlagValueAsync<string>(flagKey);
        }

        /// <summary>
        /// Get a feature flag value as an integer.
        /// </summary>
        /// <param name="flagKey">The feature flag key</param>
        /// <returns>The feature flag value as an integer, or 0 if not found</returns>
        public async Task<int> GetFeatureFlagIntegerAsync(string flagKey)
        {
            return await GetFeatureFlagValueAsync<int>(flagKey);
        }

        /// <summary>
        /// Get a feature flag value as a boolean.
        /// </summary>
        /// <param name="flagKey">The feature flag key</param>
        /// <returns>The feature flag value as a boolean, or false if not found</returns>
        public async Task<bool> GetFeatureFlagBooleanAsync(string flagKey)
        {
            return await GetFeatureFlagValueAsync<bool>(flagKey);
        }

        /// <summary>
        /// Get all feature flags for the current user.
        /// </summary>
        /// <returns>Dictionary of feature flag key-value pairs, or empty dictionary if none found</returns>
        public async Task<Dictionary<string, object>> GetFeatureFlagsAsync()
        {
            try
            {
                // Use API call for feature flags
                _logger?.LogDebug("Retrieving feature flags via API");
                var accountsClient = GetAccountsClient();
                if (accountsClient != null)
                {
                    var response = await accountsClient.GetFeatureFlagsAsync();
                    var flags = new Dictionary<string, object>();
                    if (response?.FeatureFlags != null)
                    {
                        foreach (var flag in response.FeatureFlags)
                        {
                            flags[flag.Key] = flag.Value;
                        }
                    }
                    _logger?.LogDebug("Retrieved {Count} feature flags from API", flags.Count);
                    return flags;
                }

                _logger?.LogWarning("No accounts client available for feature flags retrieval");
                return new Dictionary<string, object>();
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error retrieving feature flags");
                return new Dictionary<string, object>();
            }
        }
    }
}
