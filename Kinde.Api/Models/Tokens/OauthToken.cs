using Newtonsoft.Json;

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
        public DateTime Recieved { get; init; } = DateTime.Now;
        public bool IsExpired { get { return Recieved.AddSeconds(ExpiresIn - 5) < DateTime.Now; } }
        public TimeSpan Duration { get { return Recieved.AddSeconds(ExpiresIn).Subtract(DateTime.Now); } }
    }
}
