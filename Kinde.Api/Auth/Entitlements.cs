using Kinde.Api.Client;
using Kinde.Api.Models.Tokens;
using Kinde.Accounts.Model;
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

        public Entitlements(KindeClient client = null, ILogger logger = null) : base(logger)
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
        /// Get all entitlements for the current user's organization.
        /// </summary>
        /// <returns>List of entitlement data maps</returns>
        public async Task<List<Dictionary<string, object>>> GetAllEntitlementsAsync()
        {
            try
            {
                var accountsClient = GetAccountsClient();

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
        /// Strictly check if the user has a specific entitlement (hard check).
        /// This method enforces stricter validation than the basic HasEntitlementAsync.
        /// 
        /// Hard check requirements:
        /// - Entitlement exists and has a valid limitMax > 0
        /// - Note: Full type/value validation and limit enforcement are not available
        ///   with the current API model which lacks Type, Value, and EntitlementLimitUsed
        /// </summary>
        /// <param name="entitlementKey">The entitlement key to check</param>
        /// <returns>True if the hard-check passes, false otherwise</returns>
        public async Task<bool> HasEntitlementHardCheckAsync(string entitlementKey)
        {
            var entitlement = await GetEntitlementAsync(entitlementKey);
            if (entitlement == null)
            {
                return false;
            }

            // With the current API model, we can only validate that the entitlement exists
            // and has a valid limitMax > 0. Full type/value validation and limit enforcement
            // require additional properties (Type, Value, EntitlementLimitUsed) that are not
            // available in the current Kinde.Accounts.Model.Entitlement class.
            
            // Check if entitlement has a valid limitMax > 0
            if (entitlement.TryGetValue("limitMax", out var maxObj))
            {
                if (TryToLong(maxObj, out var max))
                {
                    return max > 0;
                }
            }

            // If no limitMax or invalid limitMax, consider entitlement as not available
            return false;
        }

        /// <summary>
        /// Hard check: true if any key passes HasEntitlementHardCheckAsync.
        /// </summary>
        public async Task<bool> HasAnyEntitlementHardCheckAsync(IEnumerable<string> entitlementKeys)
        {
            if (entitlementKeys == null || !entitlementKeys.Any())
            {
                return false;
            }

            foreach (var key in entitlementKeys)
            {
                if (await HasEntitlementHardCheckAsync(key))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Hard check: true only if all keys pass HasEntitlementHardCheckAsync.
        /// </summary>
        public async Task<bool> HasAllEntitlementsHardCheckAsync(IEnumerable<string> entitlementKeys)
        {
            if (entitlementKeys == null || !entitlementKeys.Any())
            {
                return false;
            }

            foreach (var key in entitlementKeys)
            {
                if (!await HasEntitlementHardCheckAsync(key))
                {
                    return false;
                }
            }
            return true;
        }

        internal static bool TryToLong(object obj, out long value)
        {
            try
            {
                if (obj is long l)
                {
                    value = l; return true;
                }
                if (obj is int i)
                {
                    value = i; return true;
                }
                if (obj is string s && long.TryParse(s, out var p))
                {
                    value = p; return true;
                }
            }
            catch (FormatException)
            {
                // Expected for invalid string formats, no logging needed
            }
            catch (OverflowException)
            {
                // Expected for values outside long range, no logging needed
            }
            catch (Exception)
            {
                // Unexpected exception, but this is a utility method that should not fail
                // In a production environment, you might want to log this
            }

            value = 0; return false;
        }

        /// <summary>
        /// Convert EntitlementResponse to a Dictionary for consistent API.
        /// </summary>
        /// <param name="entitlement">The entitlement response to convert</param>
        /// <returns>Dictionary representation of the entitlement</returns>
        private Dictionary<string, object> ConvertEntitlementToMap(Kinde.Accounts.Model.Entitlement entitlement)
        {
            var result = new Dictionary<string, object>();

            if (entitlement != null)
            {
                if (entitlement.FeatureKey != null) result["key"] = entitlement.FeatureKey;
                if (entitlement.FeatureName != null) result["name"] = entitlement.FeatureName;
                if (entitlement.Id != null) result["id"] = entitlement.Id;
                if (entitlement.PriceName != null) result["priceName"] = entitlement.PriceName;
                result["fixedCharge"] = entitlement.FixedCharge;
                result["unitAmount"] = entitlement.UnitAmount;
                result["limitMax"] = entitlement.EntitlementLimitMax;
                result["limitMin"] = entitlement.EntitlementLimitMin;
                // Note: EntitlementLimitUsed is not available in the current Entitlement model
                // This means the hard check limit enforcement will not work until the API provides this data
                
                // Note: Type and Value are not available in the current Entitlement model
                // This means proper boolean type checking and value validation cannot be performed
                // The hard check will only validate limitMax > 0 for now
            }

            return result;
        }
    }
}
