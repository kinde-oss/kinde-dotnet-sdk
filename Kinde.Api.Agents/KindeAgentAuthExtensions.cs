using Kinde.Api.Agents;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Kinde.Api.Agents
{
    /// <summary>
    /// Registers Kinde agent authentication.
    /// </summary>
    public static class KindeAgentAuthExtensions
    {
        internal const string HttpClientName = "Kinde.AgentAuth";

        /// <summary>
        /// Adds the Kinde agent authentication scheme, reading its settings from the
        /// <c>KindeAgentAuth</c> configuration section.
        /// </summary>
        public static AuthenticationBuilder AddKindeAgentAuth(
            this AuthenticationBuilder builder, IConfiguration configuration) =>
            builder.AddKindeAgentAuth(
                KindeAgentAuthenticationOptions.DefaultScheme, configuration);

        /// <summary>
        /// Adds the Kinde agent authentication scheme under a specific scheme name, reading its
        /// settings from the <c>KindeAgentAuth</c> configuration section.
        /// </summary>
        public static AuthenticationBuilder AddKindeAgentAuth(
            this AuthenticationBuilder builder,
            string authenticationScheme,
            IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration);

            var section = configuration.GetSection(KindeAgentAuthOptions.SectionName);
            if (!section.Exists())
            {
                throw new InvalidOperationException(
                    $"No '{KindeAgentAuthOptions.SectionName}' configuration section was found. Add "
                    + "one with Domain, ClientId, ClientSecret and ResourceUrl from your Kinde AI "
                    + "agents and MCP application, or call AddKindeAgentAuth(scheme, options => ...).");
            }

            return builder.AddKindeAgentAuth(
                authenticationScheme, options => section.Bind(options.Kinde));
        }

        /// <summary>
        /// Adds the Kinde agent authentication scheme, configured in code.
        /// </summary>
        public static AuthenticationBuilder AddKindeAgentAuth(
            this AuthenticationBuilder builder, Action<KindeAgentAuthenticationOptions> configure) =>
            builder.AddKindeAgentAuth(KindeAgentAuthenticationOptions.DefaultScheme, configure);

        /// <summary>
        /// Adds the Kinde agent authentication scheme under a specific scheme name, configured in code.
        /// </summary>
        public static AuthenticationBuilder AddKindeAgentAuth(
            this AuthenticationBuilder builder,
            string authenticationScheme,
            Action<KindeAgentAuthenticationOptions> configure)
        {
            ArgumentNullException.ThrowIfNull(builder);
            ArgumentNullException.ThrowIfNull(configure);

            builder.Services.AddKindeAgentAuthCore(authenticationScheme, configure);

            return builder.AddScheme<KindeAgentAuthenticationOptions, KindeAgentAuthenticationHandler>(
                authenticationScheme, configure);
        }

        private static IServiceCollection AddKindeAgentAuthCore(
            this IServiceCollection services,
            string authenticationScheme,
            Action<KindeAgentAuthenticationOptions> configure)
        {
            // The handler holds its HttpClient for the life of the process, so the factory never
            // gets to rotate the handler. Recycling pooled connections keeps DNS changes at
            // Kinde's edge from stranding the resource server on dead addresses.
            services.AddHttpClient(HttpClientName)
                .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
                {
                    PooledConnectionLifetime = TimeSpan.FromMinutes(5)
                });

            // Options are named per scheme by AddScheme, which calls Configure(scheme, configure)
            // itself. Registering it here as well would run the callback twice on every build,
            // so a callback with side effects would apply twice.
            services.TryAddSingleton<KindeAgentAuthRegistry>();

            // Recorded so the metadata endpoint can serve every scheme's document rather than
            // only the one that happened to be registered first.
            var schemes = services.GetOrAddKindeSchemes();
            schemes.Add(authenticationScheme);

            // The unkeyed registration serves callers that resolve KindeAgentAuth directly, such as
            // MapKindeProtectedResourceMetadata and the startup validator. TryAdd means the first
            // scheme registered wins, so an application that only registers a custom scheme still
            // resolves rather than looking up options for a default scheme nobody configured.
            services.TryAddSingleton(provider =>
                provider.GetRequiredService<KindeAgentAuthRegistry>().Get(authenticationScheme));

            services.AddSingleton<Microsoft.AspNetCore.Authorization.IAuthorizationHandler,
                KindeRequirementHandler>();

            services.AddHostedService<KindeAgentAuthStartupValidator>();

            return services;
        }

        /// <summary>
        /// The shared scheme list, created on the first registration so later calls append to it.
        /// </summary>
        private static KindeAgentAuthSchemes GetOrAddKindeSchemes(this IServiceCollection services)
        {
            var existing = services.FirstOrDefault(
                d => d.ServiceType == typeof(KindeAgentAuthSchemes))?.ImplementationInstance;

            if (existing is KindeAgentAuthSchemes schemes)
            {
                return schemes;
            }

            var created = new KindeAgentAuthSchemes();
            services.AddSingleton(created);
            return created;
        }

        /// <summary>
        /// The verification result for the current request, or null when no token was verified.
        /// </summary>
        /// <remarks>
        /// Use this when you need the raw claim set, the audiences or the expiry; for a simple
        /// scope or permission check prefer <c>[Authorize]</c> with a Kinde policy.
        /// </remarks>
        public static KindeAuthContext? GetKindeAuthContext(this HttpContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            return context.Items.TryGetValue(
                KindeAgentAuthenticationHandler.AuthContextItemKey, out var value)
                ? value as KindeAuthContext
                : null;
        }
    }
}
