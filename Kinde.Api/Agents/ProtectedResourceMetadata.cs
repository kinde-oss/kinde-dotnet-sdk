#nullable enable
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Kinde.Api.Agents
{
    /// <summary>
    /// The RFC 9728 protected resource metadata document. An MCP client fetches this after a 401 to
    /// learn which authorization server to use, so the property names must match the RFC exactly.
    /// </summary>
    public class ProtectedResourceMetadata
    {
        /// <summary>The canonical Resource URL, which is also the audience agents must request.</summary>
        [JsonPropertyName("resource")]
        public string Resource { get; set; } = string.Empty;

        /// <summary>Issuers that can issue tokens for this resource.</summary>
        [JsonPropertyName("authorization_servers")]
        public IList<string> AuthorizationServers { get; set; } = new List<string>();

        /// <summary>The JWKS endpoint, for clients that verify tokens themselves.</summary>
        [JsonPropertyName("jwks_uri")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? JwksUri { get; set; }

        /// <summary>Scopes this resource understands.</summary>
        [JsonPropertyName("scopes_supported")]
        public IList<string> ScopesSupported { get; set; } = new List<string>();

        /// <summary>How a token must be presented. Only the header form is supported.</summary>
        [JsonPropertyName("bearer_methods_supported")]
        public IList<string> BearerMethodsSupported { get; set; } = new List<string> { "header" };

        /// <summary>Optional display name.</summary>
        [JsonPropertyName("resource_name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ResourceName { get; set; }

        /// <summary>Builds the document from the configured options.</summary>
        public static ProtectedResourceMetadata FromOptions(KindeAgentAuthOptions options)
        {
            return new ProtectedResourceMetadata
            {
                Resource = options.ResourceUrl,
                // The scoped issuer, not the bare domain: this is what the client runs discovery
                // against, whereas tokens carry the bare domain in iss.
                AuthorizationServers = new List<string> { options.Issuer },
                JwksUri = options.JwksUrl,
                ScopesSupported = new List<string>(options.EffectiveScopesSupported),
                ResourceName = options.ResourceName
            };
        }
    }
}
