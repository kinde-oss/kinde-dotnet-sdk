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
                var token = GetToken();
                if (token == null)
                {
                    _logger?.LogDebug("No token available for claim retrieval");
                    return null;
                }

                // Extract claim from token
                var claims = token.GetAllClaims();
                if (claims != null && claims.ContainsKey(claimName))
                {
                    _logger?.LogDebug("Claim '{ClaimName}' found in token", claimName);
                    return claims[claimName];
                }

                _logger?.LogDebug("Claim '{ClaimName}' not found in token", claimName);
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
                var token = GetToken();
                if (token == null)
                {
                    _logger?.LogDebug("No token available for claims retrieval");
                    return new Dictionary<string, object>();
                }

                var claims = token.GetAllClaims();
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

                // Try to convert the claim to the target type
                return (T)Convert.ChangeType(claim, typeof(T));
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
