using Kinde.Api.Flows;

namespace Kinde.Api.Models.Configuration
{
    public class ClientCredentialsConfiguration : BaseAuthorizationConfiguration
    {
        public ClientCredentialsConfiguration()
        {
        }

        public ClientCredentialsConfiguration(string clientId, string scope, string clientSecret, string audience) : base(clientId, clientSecret, "ClientCredentials", scope, audience)
        {
        }

        public override IAuthorizationFlow CreateAuthorizationFlow(IApplicationConfiguration identityProviderConfiguration)
        {
            return new ClientCredentialsFlow(identityProviderConfiguration, this);
        }
    }
}
