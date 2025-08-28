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
    /// Client for accessing feature flags functionality.
    /// This provides simplified access to feature flags with hard check fallback.
    /// </summary>
    public class FeatureFlags : BaseAuth
    {
        private readonly KindeClient _client;

        public FeatureFlags(KindeClient client = null, ILogger logger = null) : base(logger)
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
                // First, try to get feature flags from token
                var token = GetToken();
                if (token != null)
                {
                    var tokenFlags = token.GetFeatureFlags();
                    if (tokenFlags != null && tokenFlags.Any())
                    {
                        // Check if flag is in token
                        var isEnabled = token.IsFeatureFlagEnabled(flagKey);
                        _logger?.LogDebug("Feature flag '{FlagKey}' found in token: {IsEnabled}", flagKey, isEnabled);
                        return isEnabled;
                    }
                }

                // Fall back to API call
                _logger?.LogDebug("No feature flags in token, falling back to API for flag: {FlagKey}", flagKey);
                var accountsClient = GetAccountsClient();
                if (accountsClient != null)
                {
                    return await accountsClient.IsFeatureFlagEnabledAsync(flagKey);
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
                // First, try to get feature flags from token
                var token = GetToken();
                if (token != null)
                {
                    var tokenFlags = token.GetFeatureFlags();
                    if (tokenFlags != null && tokenFlags.Any())
                    {
                        // Get flag value from token
                        var value = token.GetFeatureFlag(flagKey);
                        _logger?.LogDebug("Feature flag '{FlagKey}' value found in token: {Value}", flagKey, value);
                        return value;
                    }
                }

                // Fall back to API call
                _logger?.LogDebug("No feature flags in token, falling back to API for flag: {FlagKey}", flagKey);
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

                // Try to convert the value to the target type
                return (T)Convert.ChangeType(value, typeof(T));
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
                // First, try to get feature flags from token
                var token = GetToken();
                if (token != null)
                {
                    var tokenFlags = token.GetFeatureFlags();
                    if (tokenFlags != null && tokenFlags.Any())
                    {
                        var flags = new Dictionary<string, object>();
                        foreach (var flag in tokenFlags)
                        {
                            flags[flag.Key] = flag.Value;
                        }
                        _logger?.LogDebug("Retrieved {Count} feature flags from token", flags.Count);
                        return flags;
                    }
                }

                // Fall back to API call
                _logger?.LogDebug("No feature flags in token, falling back to API");
                var accountsClient = GetAccountsClient();
                if (accountsClient != null)
                {
                    var response = await accountsClient.GetFeatureFlagsAsync();
                    var flags = new Dictionary<string, object>();
                    if (response.Data != null)
                    {
                        foreach (var flag in response.Data)
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
