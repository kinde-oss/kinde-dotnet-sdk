using Microsoft.Extensions.Hosting;

namespace Kinde.Api.Agents
{
    /// <summary>
    /// Resolves every registered scheme's configuration while the host starts, so a mistake stops
    /// the application at boot rather than failing the first request that presents a token.
    /// </summary>
    internal sealed class KindeAgentAuthStartupValidator : IHostedService
    {
        private readonly KindeAgentAuthSchemes _schemes;
        private readonly KindeAgentAuthRegistry _registry;

        public KindeAgentAuthStartupValidator(
            KindeAgentAuthSchemes schemes, KindeAgentAuthRegistry registry)
        {
            _schemes = schemes;
            _registry = registry;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Every scheme, not just the first: a second misconfigured scheme would otherwise
            // fail lazily on its first request, which is exactly what this class exists to avoid.
            foreach (var scheme in _schemes.Names)
            {
                _registry.Get(scheme);
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
