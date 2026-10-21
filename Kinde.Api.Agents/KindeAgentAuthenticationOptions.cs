using Kinde.Api.Agents;
using Microsoft.AspNetCore.Authentication;

namespace Kinde.Api.Agents
{
    /// <summary>
    /// Options for the Kinde agent authentication scheme.
    /// </summary>
    public class KindeAgentAuthenticationOptions : AuthenticationSchemeOptions
    {
        /// <summary>The default scheme name.</summary>
        public const string DefaultScheme = "KindeAgent";

        /// <summary>
        /// The agent auth settings. Populated from the <c>KindeAgentAuth</c> configuration section
        /// by <see cref="KindeAgentAuthExtensions.AddKindeAgentAuth(AuthenticationBuilder, Microsoft.Extensions.Configuration.IConfiguration)"/>,
        /// or set directly in code.
        /// </summary>
        public KindeAgentAuthOptions Kinde { get; set; } = new KindeAgentAuthOptions();

        /// <summary>
        /// When true, a request whose token is valid but which fails authorization gets the full
        /// diagnostic in the <c>error_description</c> of the challenge. Off by default, because
        /// that text reaches the caller.
        /// </summary>
        public bool IncludeErrorDetails { get; set; }
    }
}
