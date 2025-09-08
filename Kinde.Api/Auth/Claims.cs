using Kinde.Api.Client;
using Kinde.Api.Models.Tokens;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

        public Claims(KindeClient client = null, ILogger logger = null) : base(logger)
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
                var claims = ReadClaimsFromCurrentToken();
                var claim = claims.FirstOrDefault(c => string.Equals(c.Type, claimName, StringComparison.Ordinal));
                return claim?.Value;
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
                var claims = ReadClaimsFromCurrentToken();
                return claims.ToDictionary(c => c.Type, c => (object)c.Value);
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error retrieving all claims");
                return new Dictionary<string, object>();
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

        /// <summary>
        /// Reads claims from the current JWT token.
        /// </summary>
        /// <returns>Enumerable of claims from the token, or empty if no valid token</returns>
        private IEnumerable<Claim> ReadClaimsFromCurrentToken()
        {
            var token = GetToken();
            if (token == null)
            {
                _logger?.LogDebug("No token available for claims");
                return Enumerable.Empty<Claim>();
            }

            var tokenString = token.IdToken ?? token.AccessToken;
            if (string.IsNullOrWhiteSpace(tokenString))
            {
                _logger?.LogDebug("Token string is empty");
                return Enumerable.Empty<Claim>();
            }

            try
            {
                var handler = new JwtSecurityTokenHandler();
                
                // Validate that we can read the token before attempting to parse it
                if (!handler.CanReadToken(tokenString))
                {
                    _logger?.LogDebug("Token cannot be read as JWT");
                    return Enumerable.Empty<Claim>();
                }

                var jwt = handler.ReadJwtToken(tokenString);
                return jwt.Claims ?? Enumerable.Empty<Claim>();
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Error parsing JWT token for claims");
                return Enumerable.Empty<Claim>();
            }
        }
    }
}
