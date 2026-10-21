using Kinde.Api.Agents;
using Microsoft.Extensions.Hosting;

namespace Kinde.Api.Agents
{
    /// <summary>
    /// Forces the agent auth configuration to be resolved, and therefore validated, while the host
    /// is starting.
    /// </summary>
    /// <remarks>
    /// Without this the <see cref="KindeAgentAuth"/> singleton is built lazily on the first request
    /// that presents a token, so a missing <c>ResourceUrl</c> or client secret surfaces as a 500 on
    /// that request instead of stopping the application at boot.
    /// </remarks>
    internal sealed class KindeAgentAuthStartupValidator : IHostedService
    {
        private readonly KindeAgentAuth _agentAuth;

        // Resolving the parameter is what runs the validation; the instance is otherwise unused.
        public KindeAgentAuthStartupValidator(KindeAgentAuth agentAuth)
        {
            _agentAuth = agentAuth;
        }

        public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
