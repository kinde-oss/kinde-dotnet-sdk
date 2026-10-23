#nullable enable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Kinde.Api.Agents.Client
{
    /// <summary>
    /// The authorization server metadata an agent discovers before it can do anything else.
    /// </summary>
    public class KindeAgentDiscoveryDocument
    {
        [JsonPropertyName("issuer")]
        public string Issuer { get; set; } = string.Empty;

        [JsonPropertyName("authorization_endpoint")]
        public string AuthorizationEndpoint { get; set; } = string.Empty;

        [JsonPropertyName("token_endpoint")]
        public string TokenEndpoint { get; set; } = string.Empty;

        [JsonPropertyName("registration_endpoint")]
        public string? RegistrationEndpoint { get; set; }

        [JsonPropertyName("introspection_endpoint")]
        public string? IntrospectionEndpoint { get; set; }

        [JsonPropertyName("revocation_endpoint")]
        public string? RevocationEndpoint { get; set; }

        [JsonPropertyName("jwks_uri")]
        public string? JwksUri { get; set; }

        [JsonPropertyName("scopes_supported")]
        public IList<string> ScopesSupported { get; set; } = new List<string>();

        [JsonPropertyName("code_challenge_methods_supported")]
        public IList<string> CodeChallengeMethodsSupported { get; set; } = new List<string>();

        /// <summary>Whether a client id may be the URL of a metadata document (CIMD).</summary>
        [JsonPropertyName("client_id_metadata_document_supported")]
        public bool ClientIdMetadataDocumentSupported { get; set; }

        /// <summary>Whether the server honours RFC 8707 <c>resource</c> parameters.</summary>
        [JsonPropertyName("resource_indicators_supported")]
        public bool ResourceIndicatorsSupported { get; set; }
    }

    /// <summary>A dynamic client registration (RFC 7591), as returned by Kinde.</summary>
    public class KindeAgentRegistration
    {
        [JsonPropertyName("client_id")]
        public string ClientId { get; set; } = string.Empty;

        [JsonPropertyName("client_secret")]
        public string? ClientSecret { get; set; }

        [JsonPropertyName("client_name")]
        public string? ClientName { get; set; }

        [JsonPropertyName("redirect_uris")]
        public IList<string> RedirectUris { get; set; } = new List<string>();

        /// <summary>Unix seconds at which the secret expires; 0 means it does not.</summary>
        [JsonPropertyName("client_secret_expires_at")]
        public long ClientSecretExpiresAt { get; set; }
    }

    /// <summary>A token response from Kinde's agent token endpoint.</summary>
    public class KindeAgentToken
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; } = "Bearer";

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>Only present when the <c>offline</c> scope was granted.</summary>
        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; set; }

        [JsonPropertyName("scope")]
        public string? Scope { get; set; }

        /// <summary>When this token was obtained. Set by the SDK, not by Kinde.</summary>
        [JsonPropertyName("obtained_at")]
        public DateTimeOffset ObtainedAt { get; set; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// Whether the token is at or near expiry. The default minute of leeway stops a request
        /// being sent with a token that expires while it is in flight.
        /// </summary>
        public bool IsExpired(TimeSpan? leeway = null) =>
            DateTimeOffset.UtcNow >= ObtainedAt.AddSeconds(ExpiresIn) - (leeway ?? TimeSpan.FromMinutes(1));
    }

    /// <summary>An authorization request in progress, between the redirect and the callback.</summary>
    public class KindeAgentAuthorizationRequest
    {
        /// <summary>The URL to send the user to.</summary>
        public string AuthorizationUrl { get; set; } = string.Empty;

        /// <summary>The CSRF state, which must come back unchanged.</summary>
        public string State { get; set; } = string.Empty;

        /// <summary>The PKCE verifier, needed to redeem the code. Never leaves the client.</summary>
        public string CodeVerifier { get; set; } = string.Empty;

        /// <summary>The redirect URI used, which the token request must repeat.</summary>
        public string RedirectUri { get; set; } = string.Empty;
    }
}
