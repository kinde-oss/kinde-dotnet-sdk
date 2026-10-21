using Kinde.Api.Agents;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Kinde.Api.Agents
{
    /// <summary>
    /// Serves the RFC 9728 protected resource metadata document.
    /// </summary>
    public static class KindeProtectedResourceMetadataExtensions
    {
        /// <summary>
        /// Maps the protected resource metadata at the path derived from the configured Resource URL,
        /// which for a Resource URL of <c>https://host/mcp</c> is
        /// <c>/.well-known/oauth-protected-resource/mcp</c>.
        /// </summary>
        /// <remarks>
        /// This endpoint must be reachable without a token. An MCP client fetches it precisely
        /// because it has not authenticated yet, so requiring authentication here deadlocks
        /// discovery.
        /// </remarks>
        public static IEndpointConventionBuilder MapKindeProtectedResourceMetadata(
            this IEndpointRouteBuilder endpoints)
        {
            ArgumentNullException.ThrowIfNull(endpoints);

            var agentAuth = endpoints.ServiceProvider.GetService<KindeAgentAuth>()
                            ?? throw new InvalidOperationException(
                                "Kinde agent authentication has not been registered. Call "
                                + "AddAuthentication().AddKindeAgentAuth(...) first.");

            return endpoints.MapKindeProtectedResourceMetadata(
                agentAuth.Options.ProtectedResourceMetadataPath);
        }

        /// <summary>
        /// Maps the protected resource metadata at an explicit path, for a host that serves the
        /// resource under a different route than its public Resource URL implies.
        /// </summary>
        public static IEndpointConventionBuilder MapKindeProtectedResourceMetadata(
            this IEndpointRouteBuilder endpoints, string pattern)
        {
            ArgumentNullException.ThrowIfNull(endpoints);
            ArgumentException.ThrowIfNullOrWhiteSpace(pattern);

            return endpoints.MapGet(pattern, (HttpContext context, KindeAgentAuth agentAuth) =>
                {
                    // Refetched on every unrecognised 401; only changes on reconfiguration.
                    context.Response.Headers.CacheControl = "public, max-age=3600";
                    return Results.Json(agentAuth.Metadata);
                })
                .AllowAnonymous()
                .WithName("KindeProtectedResourceMetadata");
        }
    }
}
