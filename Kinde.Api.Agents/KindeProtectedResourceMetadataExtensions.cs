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

            var builders = new List<IEndpointConventionBuilder>();

            // Grouped by path, case-insensitively, because ASP.NET Core matches routes that way:
            // "/mcp" and "/MCP" are one route, and mapping both would make every request to it an
            // ambiguous match rather than a metadata response.
            var groups = schemes.Names
                .Select(scheme => new
                {
                    Scheme = scheme,
                    Options = registry.Get(scheme).Options
                })
                .GroupBy(entry => entry.Options.ProtectedResourceMetadataPath,
                    StringComparer.OrdinalIgnoreCase);

            foreach (var group in groups)
            {
                var path = group.Key;
                var entries = group.ToList();

                // Identical documents are not a conflict: one route serves every scheme.
                var distinct = entries
                    .GroupBy(e => JsonSerializer.Serialize(
                        ProtectedResourceMetadata.FromOptions(e.Options)), StringComparer.Ordinal)
                    .ToList();

                if (distinct.Count == 1)
                {
                    builders.Add(endpoints.MapKindeProtectedResourceMetadata(
                        path, entries[0].Scheme));
                    continue;
                }

                // Different documents at the same path. Each scheme's challenge advertises that
                // path on its own host, so the host is what tells them apart: map the same path
                // once per scheme, constrained to its resource's authority. Serving them at
                // invented paths instead would put the documents at URLs no challenge names.
                var byHost = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                // One route per distinct document, not per scheme: several schemes can describe
                // the same resource, and treating each as a separate claim on the host would
                // reject a configuration that is perfectly valid.
                foreach (var document in distinct)
                {
                    var representative = document.First();
                    var host = AuthorityOf(representative.Options.ResourceUrl);

                    if (host == null)
                    {
                        throw new InvalidOperationException(
                            $"Scheme '{representative.Scheme}' has a Resource URL without a host, "
                            + $"so its metadata at '{path}' cannot be told apart from another "
                            + $"scheme's.");
                    }

                    if (byHost.TryGetValue(host, out var clashing))
                    {
                        // Same host and same path but different documents: nothing distinguishes
                        // the two requests, so refuse rather than answer one of them wrongly.
                        throw new InvalidOperationException(
                            $"Schemes '{clashing}' and '{representative.Scheme}' both advertise "
                            + $"their protected resource metadata at '{host}{path}' but describe "
                            + $"different resources. Give them Resource URLs that differ by host "
                            + $"or path.");
                    }

                    byHost[host] = representative.Scheme;
                    builders.Add(endpoints
                        .MapKindeProtectedResourceMetadata(path, representative.Scheme)
                        .RequireHost(host));
                }
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

        /// <summary>
        /// Maps a named scheme's protected resource metadata at an explicit path.
        /// </summary>
        /// <remarks>
        /// Needed when an application runs more than one Kinde scheme: the overload without a
        /// scheme serves whichever was registered first, so it cannot describe the others.
        /// </remarks>
        public static IEndpointConventionBuilder MapKindeProtectedResourceMetadataForScheme(
            this IEndpointRouteBuilder endpoints, string pattern, string authenticationScheme)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(authenticationScheme);

            return endpoints.MapKindeProtectedResourceMetadata(pattern, authenticationScheme);
        }

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

        /// <summary>
        /// The host (with port, when not the scheme's default) a Resource URL is served on, which
        /// is what <c>RequireHost</c> matches against the request's Host header.
        /// </summary>
        private static string? AuthorityOf(string resourceUrl) =>
            Uri.TryCreate(resourceUrl, UriKind.Absolute, out var uri)
            && !string.IsNullOrEmpty(uri.Host)
                ? uri.Authority
                : null;

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
