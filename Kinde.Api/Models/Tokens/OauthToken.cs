using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Kinde.Api.Models.Tokens
{
    public class OauthToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("id_token")]
        public string IdToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("received")]
        public DateTime Received { get; init; } = DateTime.Now;

        [JsonProperty("isExpired")]
        public bool IsExpired { get { return Received.AddSeconds(ExpiresIn - 5) < DateTime.Now; } }

        [JsonProperty("duration")]
        public TimeSpan Duration { get { return Received.AddSeconds(ExpiresIn).Subtract(DateTime.Now); } }

        #region Token Claims Methods

        /// <summary>
        /// Gets all permissions from the token.
        /// </summary>
        /// <returns>A list of permission strings, or null if not found</returns>
        public List<string> GetPermissions()
        {
            if (AccessToken == null)
                return null;

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(AccessToken);
                
                var permissionsClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "permissions");
                if (permissionsClaim == null)
                    return null;

                return JsonConvert.DeserializeObject<List<string>>(permissionsClaim.Value);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets all roles from the token.
        /// </summary>
        /// <returns>A list of role strings, or null if not found</returns>
        public List<string> GetRoles()
        {
            if (AccessToken == null)
                return null;

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(AccessToken);
                
                var rolesClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "roles");
                if (rolesClaim == null)
                    return null;

                return JsonConvert.DeserializeObject<List<string>>(rolesClaim.Value);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets all feature flags from the token.
        /// </summary>
        /// <returns>A dictionary of feature flag key-value pairs, or null if not found</returns>
        public Dictionary<string, object> GetFeatureFlags()
        {
            if (AccessToken == null)
                return null;

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(AccessToken);
                
                var flagsClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "feature_flags");
                if (flagsClaim == null)
                    return null;

                return JsonConvert.DeserializeObject<Dictionary<string, object>>(flagsClaim.Value);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a specific feature flag value from the token.
        /// </summary>
        /// <param name="flagKey">The feature flag key</param>
        /// <returns>The feature flag value, or null if not found</returns>
        public object GetFeatureFlag(string flagKey)
        {
            if (string.IsNullOrWhiteSpace(flagKey))
                return null;

            var flags = GetFeatureFlags();
            return flags?.TryGetValue(flagKey, out var value) == true ? value : null;
        }

        /// <summary>
        /// Checks if a specific permission exists in the token.
        /// </summary>
        /// <param name="permissionKey">The permission key to check</param>
        /// <returns>True if the permission exists, false otherwise</returns>
        public bool HasPermission(string permissionKey)
        {
            if (string.IsNullOrWhiteSpace(permissionKey))
                return false;

            var permissions = GetPermissions();
            return permissions?.Contains(permissionKey) ?? false;
        }

        /// <summary>
        /// Checks if a specific role exists in the token.
        /// </summary>
        /// <param name="roleKey">The role key to check</param>
        /// <returns>True if the role exists, false otherwise</returns>
        public bool HasRole(string roleKey)
        {
            if (string.IsNullOrWhiteSpace(roleKey))
                return false;

            var roles = GetRoles();
            return roles?.Contains(roleKey) ?? false;
        }

        /// <summary>
        /// Checks if a feature flag is enabled in the token.
        /// </summary>
        /// <param name="flagKey">The feature flag key to check</param>
        /// <returns>True if the feature flag is enabled, false otherwise</returns>
        public bool IsFeatureFlagEnabled(string flagKey)
        {
            if (string.IsNullOrWhiteSpace(flagKey))
                return false;

            var value = GetFeatureFlag(flagKey);
            return value is bool boolValue && boolValue;
        }

        /// <summary>
        /// Gets all claims from the token as a dictionary.
        /// </summary>
        /// <returns>A dictionary of all claims, or null if token is invalid</returns>
        public Dictionary<string, object> GetAllClaims()
        {
            if (AccessToken == null)
                return null;

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(AccessToken);
                
                var claims = new Dictionary<string, object>();
                foreach (var claim in jwtToken.Claims)
                {
                    claims[claim.Type] = claim.Value;
                }
                
                return claims;
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}
