using System.Collections.Concurrent;
using Kinde.Api.Agents;
using Microsoft.Extensions.Options;

namespace Kinde.Api.Agents
{
    /// <summary>
    /// Holds one <see cref="KindeAgentAuth"/> per authentication scheme.
    /// </summary>
    /// <remarks>
    /// An application may register more than one scheme, each protecting a different resource.
    /// A single shared instance would give every scheme whichever resource was registered last,
    /// so one scheme would reject its own valid tokens while accepting another resource's.
    /// </remarks>
    public class KindeAgentAuthRegistry
    {
        private readonly IOptionsMonitor<KindeAgentAuthenticationOptions> _options;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ConcurrentDictionary<string, KindeAgentAuth> _instances = new();

        public KindeAgentAuthRegistry(
            IOptionsMonitor<KindeAgentAuthenticationOptions> options,
            IHttpClientFactory httpClientFactory)
        {
            _options = options;
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// The instance for <paramref name="authenticationScheme"/>, built and validated on first use.
        /// </summary>
        public KindeAgentAuth Get(string authenticationScheme)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(authenticationScheme);

            return _instances.GetOrAdd(authenticationScheme, scheme =>
            {
                var options = _options.Get(scheme);

                // Both constructors validate, so a misconfigured scheme fails at startup rather
                // than once per request.
                if (options.Verifier != null)
                {
                    return new KindeAgentAuth(options.Kinde, options.Verifier);
                }

                var httpClient = _httpClientFactory.CreateClient(
                    KindeAgentAuthExtensions.HttpClientName);

                return new KindeAgentAuth(options.Kinde, httpClient);
            });
        }
    }
}
