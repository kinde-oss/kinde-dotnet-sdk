using Kinde.Authorization.Flows;

namespace Kinde.Authorization.Models.Configuration
{
    public class ClientCredentialsConfiguration : BaseAuthorizationConfiguration
    {

        public ClientCredentialsConfiguration(string clientId, string scope, string clientSecret) : base(clientId, clientSecret, "ClientCredentials", scope)
        {

        }

        public override IAuthorizationFlow CreateAuthorizationFlow(IIdentityProviderConfiguration identityProviderConfiguration)
        {
            return new ClientCredentialsFlow(identityProviderConfiguration, this);
        }
    }
}
