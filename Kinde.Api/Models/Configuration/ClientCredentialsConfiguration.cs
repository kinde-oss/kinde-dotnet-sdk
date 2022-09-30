using Kinde.Api.Flows;

namespace Kinde.Api.Models.Configuration
{
    public class ClientCredentialsConfiguration : BaseAuthorizationConfiguration
    {
        public ClientCredentialsConfiguration()
        {

        }
        public ClientCredentialsConfiguration(string clientId, string scope, string clientSecret) : base(clientId, clientSecret, "ClientCredentials", scope)
        {

        }

        public override IAuthorizationFlow CreateAuthorizationFlow(IApplicationConfiguration identityProviderConfiguration)
        {
            return new ClientCredentialsFlow(identityProviderConfiguration, this);
        }
    }
}
