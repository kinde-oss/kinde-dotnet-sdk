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
    /// Client for accessing claims functionality.
    /// This provides simplified access to token claims.
    /// </summary>
    public class Claims : BaseAuth
    {
        private readonly KindeClient _client;

        public Claims(KindeClient client = null, bool forceApi = false, ILogger logger = null) : base(forceApi, logger)
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
        /// Get a specific claim from the token.
        /// </summary>
        /// <param name="claimName">The name of the claim to retrieve</param>
        /// <returns>The claim value, or null if not found</returns>
        public object GetClaim(string claimName)
        {
            if (string.IsNullOrWhiteSpace(claimName))
            {
                _logger?.LogWarning("Claim name cannot be null or empty");
                return null;
            }

            try
            {
                if (ShouldUseApi())
                {
                    // For API mode, we would need to implement API-based claim retrieval
                    // Since claims are typically token-based, we'll fall back to token parsing
                    _logger?.LogDebug("ForceApi is enabled, but claims are typically token-based. Using token parsing for claim: {ClaimName}", claimName);
                }

                var client = GetClient();
                if (client == null)
                {
                    _logger?.LogDebug("No KindeClient available for claim retrieval");
                    return null;
                }

                // Use KindeClient's built-in GetClaim method
                var kindeClaim = client.GetClaim(claimName);
                if (kindeClaim != null)
                {
                    _logger?.LogDebug("Claim '{ClaimName}' found via KindeClient", claimName);
                    return kindeClaim.Value;
                }

                _logger?.LogDebug("Claim '{ClaimName}' not found via KindeClient", claimName);
                return null;
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error retrieving claim '{ClaimName}'", claimName);
                return null;
            }
        }

        /// <summary>
        /// Get all claims from the token.
        /// </summary>
        /// <returns>Dictionary of all claims, or empty dictionary if no token</returns>
        public Dictionary<string, object> GetAllClaims()
        {
            try
            {
                var client = GetClient();
                if (client == null)
                {
                    _logger?.LogDebug("No KindeClient available for claims retrieval");
                    return new Dictionary<string, object>();
                }

                // Get the token from the client and parse it directly
                var token = GetToken();
                if (token?.AccessToken == null)
                {
                    _logger?.LogDebug("No access token available for claims retrieval");
                    return new Dictionary<string, object>();
                }

                // Parse JWT token to extract all claims
                var claims = ParseJwtClaims(token.AccessToken);
                _logger?.LogDebug("Retrieved {Count} claims from token", claims?.Count ?? 0);
                return claims ?? new Dictionary<string, object>();
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error retrieving all claims");
                return new Dictionary<string, object>();
            }
        }

        /// <summary>
        /// Parse JWT token to extract all claims.
        /// </summary>
        /// <param name="accessToken">The JWT access token</param>
        /// <returns>Dictionary of all claims, or null if parsing fails</returns>
        private Dictionary<string, object> ParseJwtClaims(string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
                return null;

            try
            {
                var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(accessToken);
                
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

        /// <summary>
        /// Get a claim as a specific type.
        /// </summary>
        /// <typeparam name="T">The type to convert the claim to</typeparam>
        /// <param name="claimName">The name of the claim to retrieve</param>
        /// <returns>The claim value as the specified type, or default(T) if not found</returns>
        public T GetClaim<T>(string claimName)
        {
            var claim = GetClaim(claimName);
            if (claim == null)
                return default(T);

            try
            {
                if (claim is T typedClaim)
                    return typedClaim;

                var s = claim as string ?? claim?.ToString();
                if (s is null) return default;

                var t = typeof(T);
                if (t == typeof(string)) return (T)(object)s;
                if (t == typeof(bool) && bool.TryParse(s, out var b)) return (T)(object)b;
                if (t == typeof(int) && int.TryParse(s, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out var i)) return (T)(object)i;
                if (t == typeof(long) && long.TryParse(s, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out var l)) return (T)(object)l;
                if (t == typeof(double) && double.TryParse(s, System.Globalization.NumberStyles.Float | System.Globalization.NumberStyles.AllowThousands, System.Globalization.CultureInfo.InvariantCulture, out var d)) return (T)(object)d;
                if (t == typeof(decimal) && decimal.TryParse(s, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out var m)) return (T)(object)m;
                if (t == typeof(Guid) && Guid.TryParse(s, out var g)) return (T)(object)g;
                if (t == typeof(DateTimeOffset))
                {
                    if (long.TryParse(s, out var seconds)) return (T)(object)DateTimeOffset.FromUnixTimeSeconds(seconds);
                    if (DateTimeOffset.TryParse(s, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeUniversal | System.Globalization.DateTimeStyles.AdjustToUniversal, out var dto)) return (T)(object)dto;
                }
                if (t.IsEnum)
                {
                    try { return (T)Enum.Parse(t, s, ignoreCase: true); } catch { }
                }
                return (T)Convert.ChangeType(s, t, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error converting claim '{ClaimName}' to type {Type}", claimName, typeof(T).Name);
                return default(T);
            }
        }

        /// <summary>
        /// Check if a claim exists in the token.
        /// </summary>
        /// <param name="claimName">The name of the claim to check</param>
        /// <returns>True if the claim exists, false otherwise</returns>
        public bool HasClaim(string claimName)
        {
            return GetClaim(claimName) != null;
        }

    }
}
