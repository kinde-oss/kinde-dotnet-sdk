#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kinde.Api.Agents.Client
{
    /// <summary>
    /// Settings for an agent acting as an OAuth client against a Kinde AI agents and MCP
    /// application.
    /// </summary>
    public class KindeAgentClientOptions
    {
        /// <summary>Configuration section these options bind from.</summary>
        public const string SectionName = "KindeAgentClient";

        /// <summary>Kinde domain, for example https://example.kinde.com.</summary>
        public string Domain { get; set; } = string.Empty;

        /// <summary>
        /// The AI agents and MCP application's id, which scopes the endpoints. This identifies the
        /// application the agent authorizes against, not the agent itself.
        /// </summary>
        public string AgentApplicationId { get; set; } = string.Empty;

        /// <summary>
        /// This client's own id, when it has one. Leave empty to register dynamically. For a CIMD
        /// client this is the URL of its metadata document.
        /// </summary>
        public string? ClientId { get; set; }

        /// <summary>This client's secret, when it is confidential. PKCE covers public clients.</summary>
        public string? ClientSecret { get; set; }

        /// <summary>Where Kinde sends the user back. Must be registered or dynamically declared.</summary>
        public string RedirectUri { get; set; } = string.Empty;

        /// <summary>
        /// The Resource URL this agent wants a token for (RFC 8707). Without it the token's
        /// audience will not match the resource server and every call is rejected.
        /// </summary>
        public string? ResourceUrl { get; set; }

        /// <summary>Scopes to request. "offline" is always included.</summary>
        public IList<string> Scopes { get; set; } = new List<string>();

        /// <summary>Name sent at dynamic registration, shown to the user on the consent screen.</summary>
        public string ClientName { get; set; } = "Kinde .NET agent";

        /// <summary>Domain with any trailing slash removed.</summary>
        public string NormalizedDomain => (Domain ?? string.Empty).TrimEnd('/');

        /// <summary>RFC 8414 discovery, which inserts the path rather than appending it.</summary>
        public string AuthorizationServerMetadataUrl =>
            $"{NormalizedDomain}/.well-known/oauth-authorization-server/oauth2/agents/{AgentApplicationId}";

        /// <summary>
        /// The scopes actually requested. "offline" is always first: without it Kinde issues no
        /// refresh token, and the agent must ask the user to consent again within the hour.
        /// </summary>
        public IReadOnlyList<string> EffectiveScopes
        {
            get
            {
                var scopes = (Scopes ?? new List<string>())
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Distinct(StringComparer.Ordinal)
                    .ToList();

                if (!scopes.Contains(KindeAgentAuthOptions.OfflineScope, StringComparer.Ordinal))
                {
                    scopes.Insert(0, KindeAgentAuthOptions.OfflineScope);
                }

                return scopes;
            }
        }

        /// <summary>Fails fast with a message naming each missing or malformed setting.</summary>
        public void Validate()
        {
            var problems = new List<string>();

            if (string.IsNullOrWhiteSpace(Domain))
            {
                problems.Add($"{nameof(Domain)} is required (for example https://example.kinde.com).");
            }
            else if (!Uri.TryCreate(NormalizedDomain, UriKind.Absolute, out var domain)
                     || domain.Scheme != Uri.UriSchemeHttps)
            {
                problems.Add($"{nameof(Domain)} must be an absolute https URL, but was '{Domain}'.");
            }

            if (string.IsNullOrWhiteSpace(AgentApplicationId))
            {
                problems.Add($"{nameof(AgentApplicationId)} is required. Use the client ID of the "
                             + "Kinde AI agents and MCP application.");
            }

            if (string.IsNullOrWhiteSpace(RedirectUri))
            {
                problems.Add($"{nameof(RedirectUri)} is required.");
            }
            else if (!Uri.TryCreate(RedirectUri, UriKind.Absolute, out _))
            {
                problems.Add($"{nameof(RedirectUri)} must be an absolute URL, but was '{RedirectUri}'.");
            }

            if (!string.IsNullOrWhiteSpace(ResourceUrl)
                && !Uri.TryCreate(ResourceUrl, UriKind.Absolute, out _))
            {
                problems.Add($"{nameof(ResourceUrl)} must be an absolute URL, but was '{ResourceUrl}'.");
            }

            if (problems.Count > 0)
            {
                throw new ArgumentException(
                    $"Kinde agent client is not configured correctly (section '{SectionName}'):"
                    + Environment.NewLine + " - " + string.Join(Environment.NewLine + " - ", problems));
            }
        }
    }
}
