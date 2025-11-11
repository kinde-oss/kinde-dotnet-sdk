using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kinde.Api.Test.Integration
{
    /// <summary>
    /// Helper class for M2M (Machine-to-Machine) authentication with Kinde Management API
    /// </summary>
    public static class M2MAuthenticationHelper
    {
        /// <summary>
        /// Gets an access token using client credentials flow
        /// </summary>
        /// <param name="domain">Kinde business domain (e.g., https://your-business.kinde.com)</param>
        /// <param name="clientId">M2M application client ID</param>
        /// <param name="clientSecret">M2M application client secret</param>
        /// <param name="audience">API audience (typically https://your-business.kinde.com/api)</param>
        /// <param name="scope">Optional scope string</param>
        /// <returns>Access token or null if authentication failed</returns>
        public static async Task<string?> GetAccessTokenAsync(
            string domain,
            string clientId,
            string clientSecret,
            string audience,
            string? scope = null)
        {
            if (string.IsNullOrWhiteSpace(domain))
                throw new ArgumentException("Domain cannot be null or empty", nameof(domain));
            if (string.IsNullOrWhiteSpace(clientId))
                throw new ArgumentException("ClientId cannot be null or empty", nameof(clientId));
            if (string.IsNullOrWhiteSpace(clientSecret))
                throw new ArgumentException("ClientSecret cannot be null or empty", nameof(clientSecret));
            if (string.IsNullOrWhiteSpace(audience))
                throw new ArgumentException("Audience cannot be null or empty", nameof(audience));

            try
            {
                using var httpClient = new HttpClient();
                var tokenUrl = $"{domain.TrimEnd('/')}/oauth2/token";

                var requestParams = new List<KeyValuePair<string, string>>
                {
                    new("grant_type", "client_credentials"),
                    new("client_id", clientId),
                    new("client_secret", clientSecret),
                    new("audience", audience)
                };

                if (!string.IsNullOrWhiteSpace(scope))
                {
                    requestParams.Add(new KeyValuePair<string, string>("scope", scope));
                }

                var requestContent = new FormUrlEncodedContent(requestParams);
                var response = await httpClient.PostAsync(tokenUrl, requestContent);
                
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var tokenData = JsonConvert.DeserializeObject<JObject>(jsonResponse);

                return tokenData?["access_token"]?.ToString();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Failed to obtain access token: {ex.Message}", ex);
            }
        }
    }
}

