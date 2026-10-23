using Kinde.Api.Agents;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Json;
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

            var schemes = endpoints.ServiceProvider.GetService<KindeAgentAuthSchemes>()
                          ?? throw new InvalidOperationException(
                              "Kinde agent authentication has not been registered. Call "
                              + "AddAuthentication().AddKindeAgentAuth(...) first.");

            var registry = endpoints.ServiceProvider.GetRequiredService<KindeAgentAuthRegistry>();

            // One route per distinct path. Mapping only one scheme's document would leave every
            // other scheme's challenge pointing at a 404, decided by registration order.
            var builders = new List<IEndpointConventionBuilder>();

            // Case-insensitive, because ASP.NET Core routing matches paths that way. Treating
            // "/mcp" and "/MCP" as distinct would map two equally ranked endpoints, and a request
            // matching both fails as an ambiguous match rather than returning metadata.
            var mapped = new Dictionary<string, (string Scheme, string Document)>(
                StringComparer.OrdinalIgnoreCase);

            foreach (var scheme in schemes.Names)
            {
                var options = registry.Get(scheme).Options;
                var path = options.ProtectedResourceMetadataPath;
                var document = JsonSerializer.Serialize(
                    ProtectedResourceMetadata.FromOptions(options));

                if (mapped.TryGetValue(path, out var existing))
                {
                    // Same path, same document: one route serves both schemes.
                    if (string.Equals(existing.Document, document, StringComparison.Ordinal))
                    {
                        continue;
                    }

                    // Same path, different document: whichever scheme lost would send clients to
                    // the other's metadata, so refuse rather than pick one silently.
                    throw new InvalidOperationException(
                        $"Schemes '{existing.Scheme}' and '{scheme}' both serve their protected "
                        + $"resource metadata at '{path}' but describe different resources. Give "
                        + $"them Resource URLs with different paths, or map each one explicitly "
                        + $"with {nameof(MapKindeProtectedResourceMetadata)}(pattern).");
                }

                mapped[path] = (scheme, document);
                builders.Add(endpoints.MapKindeProtectedResourceMetadata(path, scheme));
            }

            if (builders.Count == 0)
            {
                // Mapping nothing would leave every challenge pointing at a 404 with no clue why.
                throw new InvalidOperationException(
                    "No Kinde authentication scheme has been registered, so there is no protected "
                    + "resource metadata to serve. Call AddAuthentication().AddKindeAgentAuth(...) "
                    + "before mapping the endpoint.");
            }

            return new CompositeEndpointConventionBuilder(builders);
        }

        /// <summary>
        /// Maps the protected resource metadata at an explicit path, for a host that serves the
        /// resource under a different route than its public Resource URL implies.
        /// </summary>
        public static IEndpointConventionBuilder MapKindeProtectedResourceMetadata(
            this IEndpointRouteBuilder endpoints, string pattern) =>
            endpoints.MapKindeProtectedResourceMetadata(pattern, authenticationScheme: null);

        private static IEndpointConventionBuilder MapKindeProtectedResourceMetadata(
            this IEndpointRouteBuilder endpoints, string pattern, string? authenticationScheme)
        {
            ArgumentNullException.ThrowIfNull(endpoints);
            ArgumentException.ThrowIfNullOrWhiteSpace(pattern);

            return endpoints.MapGet(pattern, (HttpContext context) =>
                {
                    var agentAuth = authenticationScheme == null
                        ? context.RequestServices.GetRequiredService<KindeAgentAuth>()
                        : context.RequestServices.GetRequiredService<KindeAgentAuthRegistry>()
                            .Get(authenticationScheme);

                    // Refetched on every unrecognised 401; only changes on reconfiguration.
                    context.Response.Headers.CacheControl = "public, max-age=3600";
                    return Results.Json(agentAuth.Metadata);
                })
                .AllowAnonymous()
                .WithName($"KindeProtectedResourceMetadata:{authenticationScheme ?? pattern}");
        }

        /// <summary>Applies conventions to every route mapped for the registered schemes.</summary>
        private sealed class CompositeEndpointConventionBuilder : IEndpointConventionBuilder
        {
            private readonly IReadOnlyList<IEndpointConventionBuilder> _builders;

            public CompositeEndpointConventionBuilder(IReadOnlyList<IEndpointConventionBuilder> builders)
            {
                _builders = builders;
            }

            public void Add(Action<EndpointBuilder> convention)
            {
                foreach (var builder in _builders)
                {
                    builder.Add(convention);
                }
            }
        }
    }
}
