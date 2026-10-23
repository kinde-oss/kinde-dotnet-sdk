using Kinde.Api.Agents;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using McpMetadata = ModelContextProtocol.Authentication.ProtectedResourceMetadata;
using ModelContextProtocol.AspNetCore.Authentication;

namespace Kinde.Mcp
{
    /// <summary>
    /// Wires a Kinde AI agents and MCP application into an MCP server built with the C#
    /// ModelContextProtocol SDK.
    /// </summary>
    /// <remarks>
    /// The MCP SDK owns the protocol side: it serves the protected resource metadata and issues the
    /// 401 challenge an MCP client follows. Kinde owns token verification. This bridges the two, so
    /// neither is reimplemented.
    /// </remarks>
    public static class KindeMcpAuthExtensions
    {
        /// <summary>
        /// Adds MCP authentication backed by Kinde, reading settings from the
        /// <c>KindeAgentAuth</c> configuration section.
        /// </summary>
        public static AuthenticationBuilder AddKindeMcpAuth(
            this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configuration);

            var section = configuration.GetSection(KindeAgentAuthOptions.SectionName);
            if (!section.Exists())
            {
                throw new InvalidOperationException(
                    $"No '{KindeAgentAuthOptions.SectionName}' configuration section was found. Add "
                    + "one with Domain, ClientId, ClientSecret and ResourceUrl from your Kinde AI "
                    + "agents and MCP application.");
            }

            return services.AddKindeMcpAuth(options => section.Bind(options.Kinde));
        }

        /// <summary>
        /// Adds MCP authentication backed by Kinde, configured in code.
        /// </summary>
        public static AuthenticationBuilder AddKindeMcpAuth(
            this IServiceCollection services,
            Action<KindeAgentAuthenticationOptions> configure)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configure);

            // Invoked eagerly here to build the MCP metadata, and again through the options
            // system for the handler. If configuration reloads, the two can diverge until the
            // next restart; the metadata is static per application, so this is acceptable.
            var resolved = new KindeAgentAuthenticationOptions();
            configure(resolved);
            resolved.Kinde.Validate();

            // Defaults are only filled in when the application has not chosen its own. Setting
            // them outright would silently repoint an app that also hosts, say, a cookie-
            // authenticated admin UI, with the outcome depending on call order.
            services.PostConfigure<AuthenticationOptions>(options =>
            {
                // DefaultScheme is also checked: when an application sets only that, challenge and
                // authenticate already fall back to it, so filling in the specific defaults here
                // would still repoint an app that chose its own.
                if (options.DefaultScheme != null)
                {
                    return;
                }

                options.DefaultChallengeScheme ??= McpAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme ??= KindeAgentAuthenticationOptions.DefaultScheme;
            });

            var builder = services.AddAuthentication().AddKindeAgentAuth(configure);

            // The MCP scheme answers the challenge, because an MCP client expects the metadata
            // document and the 401 to come from the MCP server's own well-known path. Kinde's
            // scheme stays responsible for deciding whether a token is valid.
            builder.AddMcp(options =>
            {
                options.ForwardAuthenticate = KindeAgentAuthenticationOptions.DefaultScheme;
                options.ResourceMetadata = BuildResourceMetadata(resolved.Kinde);

                // Challenges normally stay on the MCP scheme, which answers 401 with the
                // resource_metadata pointer an MCP client needs. The exception is Kinde being
                // unreachable: that must stay a 503, or the client treats an outage as an
                // authentication failure and restarts authorization instead of retrying.
                options.ForwardDefaultSelector = context =>
                    context.GetKindeAuthContext()?.FailureKind
                    == KindeAuthFailureKind.AuthorizationServerUnavailable
                        ? KindeAgentAuthenticationOptions.DefaultScheme
                        : null;
            });

            return builder;
        }

        /// <summary>
        /// Translates Kinde's metadata into the MCP SDK's own metadata type.
        /// </summary>
        private static McpMetadata BuildResourceMetadata(KindeAgentAuthOptions options)
        {
            var metadata = new McpMetadata
            {
                Resource = options.ResourceUrl,
                JwksUri = options.JwksUrl
            };

            // Cleared before filling: a collection initializer would append to whatever the SDK
            // already put there, which is how "header" ends up advertised twice.
            metadata.AuthorizationServers.Clear();
            // The scoped issuer, which is what a client runs discovery against. Tokens themselves
            // carry the bare domain in iss.
            metadata.AuthorizationServers.Add(options.Issuer);

            metadata.BearerMethodsSupported.Clear();
            metadata.BearerMethodsSupported.Add("header");

            metadata.ScopesSupported.Clear();
            foreach (var scope in options.EffectiveScopesSupported)
            {
                metadata.ScopesSupported.Add(scope);
            }

            if (!string.IsNullOrWhiteSpace(options.ResourceName))
            {
                metadata.ResourceName = options.ResourceName;
            }

            return metadata;
        }
    }
}
