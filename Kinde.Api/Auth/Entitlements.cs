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
    /// Client for accessing entitlements functionality.
    /// This provides simplified access to user entitlements from the Kinde Accounts API.
    /// </summary>
    public class Entitlements : BaseAuth
    {
        private readonly KindeClient _client;
        private readonly IKindeAccountsClient _accountsClient;

        public Entitlements(KindeClient client, IKindeAccountsClient accountsClient, ILogger logger = null) : base(logger)
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
        /// Get all entitlements for the current user's organization.
        /// </summary>
        /// <returns>List of entitlement data maps</returns>
        public async Task<List<Dictionary<string, object>>> GetAllEntitlementsAsync()
        {
            try
            {
                var accountsClient = GetAccountsClient();
                if (accountsClient == null)
                {
                    _logger?.LogDebug("No accounts client available for entitlements retrieval");
                    return new List<Dictionary<string, object>>();
                }

                var response = await accountsClient.GetEntitlementsAsync();
                var entitlements = new List<Dictionary<string, object>>();

                    if (response.Data?.Entitlements != null)
                    {
                        foreach (var entitlement in response.Data.Entitlements)
                        {
                            entitlements.Add(ConvertEntitlementToMap(entitlement));
                        }
                    }

                _logger?.LogDebug("Retrieved {Count} entitlements", entitlements.Count);
                return entitlements;
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error retrieving entitlements");
                return new List<Dictionary<string, object>>();
            }
        }

        /// <summary>
        /// Get a specific entitlement by key.
        /// </summary>
        /// <param name="key">The entitlement key to retrieve</param>
        /// <returns>Entitlement data map, or null if not found</returns>
        public async Task<Dictionary<string, object>?> GetEntitlementAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                _logger?.LogWarning("Entitlement key cannot be null or empty");
                return null;
            }

            try
            {
                var accountsClient = GetAccountsClient();
                if (accountsClient == null)
                {
                    _logger?.LogDebug("No accounts client available for entitlement retrieval");
                    return null;
                }

                var response = await accountsClient.GetEntitlementAsync(key);
                if (response.Data?.Entitlement != null)
                {
                    var entitlement = ConvertEntitlementToMap(response.Data.Entitlement);
                    _logger?.LogDebug("Retrieved entitlement '{Key}'", key);
                    return entitlement;
                }

                _logger?.LogDebug("Entitlement '{Key}' not found", key);
                return null;
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error retrieving entitlement '{Key}'", key);
                return null;
            }
        }

        /// <summary>
        /// Check if the user has a specific entitlement.
        /// </summary>
        /// <param name="entitlementKey">The entitlement key to check</param>
        /// <returns>True if the user has the entitlement, false otherwise</returns>
        public async Task<bool> HasEntitlementAsync(string entitlementKey)
        {
            var entitlement = await GetEntitlementAsync(entitlementKey);
            if (entitlement == null)
            {
                return false;
            }

            // Determine active/enabled state from 'status' or fallback to 'value' and 'type'
            if (entitlement.TryGetValue("status", out var status))
            {
                if (status is bool boolStatus)
                {
                    return boolStatus;
                }
                var statusString = status?.ToString()?.Trim();
                return statusString?.Equals("active", StringComparison.OrdinalIgnoreCase) == true ||
                       statusString?.Equals("enabled", StringComparison.OrdinalIgnoreCase) == true ||
                       statusString?.Equals("true", StringComparison.OrdinalIgnoreCase) == true ||
                       statusString?.Equals("1") == true ||
                       statusString?.Equals("yes", StringComparison.OrdinalIgnoreCase) == true ||
                       statusString?.Equals("on", StringComparison.OrdinalIgnoreCase) == true;
            }

            if (entitlement.TryGetValue("value", out var value) && entitlement.TryGetValue("type", out var type))
            {
                var valueString = value?.ToString()?.Trim();
                var typeString = type?.ToString()?.Trim();
                
                if (typeString?.Equals("boolean", StringComparison.OrdinalIgnoreCase) == true)
                {
                    return valueString?.Equals("true", StringComparison.OrdinalIgnoreCase) == true ||
                           valueString?.Equals("1") == true ||
                           valueString?.Equals("yes", StringComparison.OrdinalIgnoreCase) == true ||
                           valueString?.Equals("on", StringComparison.OrdinalIgnoreCase) == true;
                }
                
                // Heuristic: treat active-like string values as enabled when type is not boolean
                return valueString?.Equals("active", StringComparison.OrdinalIgnoreCase) == true ||
                       valueString?.Equals("enabled", StringComparison.OrdinalIgnoreCase) == true;
            }

            return false;
        }

        /// <summary>
        /// Check if the user has any of the specified entitlements.
        /// </summary>
        /// <param name="entitlementKeys">The entitlement keys to check</param>
        /// <returns>True if the user has any of the entitlements, false otherwise</returns>
        public async Task<bool> HasAnyEntitlementAsync(IEnumerable<string> entitlementKeys)
        {
            if (entitlementKeys == null || !entitlementKeys.Any())
            {
                return false;
            }

            foreach (var key in entitlementKeys)
            {
                if (await HasEntitlementAsync(key))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check if the user has all of the specified entitlements.
        /// </summary>
        /// <param name="entitlementKeys">The entitlement keys to check</param>
        /// <returns>True if the user has all of the entitlements, false otherwise</returns>
        public async Task<bool> HasAllEntitlementsAsync(IEnumerable<string> entitlementKeys)
        {
            if (entitlementKeys == null || !entitlementKeys.Any())
            {
                return false;
            }

            foreach (var key in entitlementKeys)
            {
                if (!await HasEntitlementAsync(key))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Convert EntitlementResponse to a Dictionary for consistent API.
        /// </summary>
        /// <param name="entitlement">The entitlement response to convert</param>
        /// <returns>Dictionary representation of the entitlement</returns>
        private Dictionary<string, object> ConvertEntitlementToMap(dynamic entitlement)
        {
            var result = new Dictionary<string, object>();

            if (entitlement != null)
            {
                // Extract properties from the entitlement object
                if (entitlement.FeatureKey != null)
                    result["key"] = entitlement.FeatureKey;
                if (entitlement.FeatureName != null)
                    result["name"] = entitlement.FeatureName;
                if (entitlement.Description != null)
                    result["description"] = entitlement.Description;
                if (entitlement.Type != null)
                    result["type"] = entitlement.Type;
                if (entitlement.Value != null)
                    result["value"] = entitlement.Value;
                if (entitlement.OrgCode != null)
                    result["orgCode"] = entitlement.OrgCode;
                if (entitlement.Plans != null)
                    result["plans"] = entitlement.Plans;
                if (entitlement.EntitlementLimitMax != null)
                    result["limitMax"] = entitlement.EntitlementLimitMax;
                if (entitlement.EntitlementLimitUsed != null)
                    result["limitUsed"] = entitlement.EntitlementLimitUsed;

                // Derive a boolean status for easy checks when type is boolean
                if (entitlement.Type != null && entitlement.Type.ToString().Equals("boolean", StringComparison.OrdinalIgnoreCase))
                {
                    var value = entitlement.Value?.ToString();
                    var enabled = value != null && (
                        value.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                        value.Equals("1") ||
                        value.Equals("yes", StringComparison.OrdinalIgnoreCase) ||
                        value.Equals("on", StringComparison.OrdinalIgnoreCase)
                    );
                    result["status"] = enabled;
                }
            }

            return result;
        }
    }
}
