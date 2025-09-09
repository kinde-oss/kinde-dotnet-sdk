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

        public FeatureFlags(KindeClient client, IKindeAccountsClient accountsClient, bool forceApi, ILogger logger = null) : base(forceApi, logger)
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
                if (ShouldUseApi())
                {
                    // Use API call for hard check
                    _logger?.LogDebug("Checking feature flag via API (hard check): {FlagKey}", flagKey);
                    if (_accountsClient == null)
                    {
                        _logger?.LogWarning("ForceApi is enabled but accounts client is not available. Falling back to token-based check.");
                        // Use KindeClient's built-in feature flag checking (token-based)
                        var client = GetClient();
                        if (client != null)
                        {
                            var flag = client.GetFlag(flagKey);
                            var tokenIsEnabled = flag?.Value?.ToString()?.ToLower() == "true" || flag?.Value?.ToString() == "1";
                            _logger?.LogDebug("Feature flag '{FlagKey}' token check result: {IsEnabled}", flagKey, tokenIsEnabled);
                            return tokenIsEnabled;
                        }
                        _logger?.LogWarning("No KindeClient available for feature flag check");
                        return false;
                    }
                    var isEnabled = await _accountsClient.IsFeatureFlagEnabledAsync(flagKey);
                    _logger?.LogDebug("Feature flag '{FlagKey}' API check result: {IsEnabled}", flagKey, isEnabled);
                    return isEnabled;
                }
                else
                {
                    // Use KindeClient's built-in feature flag checking (token-based)
                    _logger?.LogDebug("Checking feature flag via KindeClient (token-based): {FlagKey}", flagKey);
                    var client = GetClient();
                    if (client != null)
                    {
                        var flag = client.GetFlag(flagKey);
                        var isEnabled = flag?.Value?.ToString()?.ToLower() == "true" || flag?.Value?.ToString() == "1";
                        _logger?.LogDebug("Feature flag '{FlagKey}' token check result: {IsEnabled}", flagKey, isEnabled);
                        return isEnabled;
                    }

                    _logger?.LogWarning("No KindeClient available for feature flag check");
                    return false;
                }
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
                if (ShouldUseApi())
                {
                    // Use API call for hard check
                    _logger?.LogDebug("Getting feature flag value via API (hard check): {FlagKey}", flagKey);
                    if (_accountsClient == null)
                    {
                        _logger?.LogWarning("ForceApi is enabled but accounts client is not available. Falling back to token-based check.");
                        // Use KindeClient's built-in feature flag value retrieval (token-based)
                        var client = GetClient();
                        if (client != null)
                        {
                            var flag = client.GetFlag(flagKey);
                            var tokenValue = flag?.Value;
                            _logger?.LogDebug("Feature flag '{FlagKey}' token value: {Value}", flagKey, tokenValue);
                            return tokenValue;
                        }
                        _logger?.LogWarning("No KindeClient available for feature flag value retrieval");
                        return null;
                    }
                    var value = await _accountsClient.GetFeatureFlagValueAsync(flagKey);
                    _logger?.LogDebug("Feature flag '{FlagKey}' API value: {Value}", flagKey, value);
                    return value;
                }
                else
                {
                    // Use KindeClient's built-in feature flag value retrieval (token-based)
                    _logger?.LogDebug("Getting feature flag value via KindeClient (token-based): {FlagKey}", flagKey);
                    var client = GetClient();
                    if (client != null)
                    {
                        var flag = client.GetFlag(flagKey);
                        var value = flag?.Value;
                        _logger?.LogDebug("Feature flag '{FlagKey}' token value: {Value}", flagKey, value);
                        return value;
                    }

                    _logger?.LogWarning("No KindeClient available for feature flag value retrieval");
                    return null;
                }
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
                if (ShouldUseApi())
                {
                    // Use API call for hard check
                    _logger?.LogDebug("Retrieving feature flags via API (hard check)");
                    if (_accountsClient == null)
                    {
                        _logger?.LogWarning("ForceApi is enabled but accounts client is not available. Falling back to token-based check.");
                        // Use token-based approach for feature flags
                        var client = GetClient();
                        if (client != null)
                        {
                            var claim = client.GetClaim("feature_flags");
                            if (claim != null)
                            {
                                var tokenFlags = new Dictionary<string, object>();
                                if (claim.Value is Dictionary<string, object> flagDict)
                                {
                                    tokenFlags = flagDict;
                                }
                                else if (claim.Value is string flagString && !string.IsNullOrEmpty(flagString))
                                {
                                    // Try to parse as JSON if it's a string
                                    try
                                    {
                                        var parsed = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(flagString);
                                        if (parsed != null)
                                            tokenFlags = parsed;
                                    }
                                    catch
                                    {
                                        // If parsing fails, treat as a single flag
                                        tokenFlags[flagString] = true;
                                    }
                                }
                                _logger?.LogDebug("Retrieved {Count} feature flags from KindeClient", tokenFlags.Count);
                                return tokenFlags;
                            }
                        }
                        _logger?.LogWarning("No KindeClient available for feature flags retrieval");
                        return new Dictionary<string, object>();
                    }
                    var response = await _accountsClient.GetFeatureFlagsAsync();
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
                else
                {
                    // Use token-based approach for feature flags
                    _logger?.LogDebug("Retrieving feature flags via token-based approach");
                    var client = GetClient();
                    if (client != null)
                    {
                        // For token-based feature flags, we'll use the claims approach
                        // since KindeClient doesn't have a direct GetFlags method
                        var claim = client.GetClaim("feature_flags");
                        if (claim != null)
                        {
                            var flagsDict = new Dictionary<string, object>();
                            var flags = claim.Value?.ToString();
                            if (!string.IsNullOrEmpty(flags))
                            {
                                // Parse the feature flags from the token claim
                                // This is a simplified approach - in practice, you might need more sophisticated parsing
                                flagsDict["feature_flags"] = flags;
                            }
                            _logger?.LogDebug("Retrieved {Count} feature flags from token", flagsDict.Count);
                            return flagsDict;
                        }
                    }

                    _logger?.LogWarning("No KindeClient available for feature flags retrieval");
                    return new Dictionary<string, object>();
                }
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error retrieving feature flags");
                return new Dictionary<string, object>();
            }
        }
    }
}
