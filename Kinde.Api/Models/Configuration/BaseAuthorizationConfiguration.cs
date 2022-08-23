using Kinde.Api.Flows;

namespace Kinde.Api.Models.Configuration
{
    public abstract class BaseAuthorizationConfiguration : IAuthorizationConfiguration
    {

        public BaseAuthorizationConfiguration(string clientId, string clientSecret, string grantType, string scope)
        {
            ClientId = clientId;
            GrantType = grantType;
            Scope = scope;
            ClientSecret = clientSecret; ;
        }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }
        public string Scope { get; set; }
        public string GrantType { get; set; }

        public abstract IAuthorizationFlow CreateAuthorizationFlow(IIdentityProviderConfiguration clientConfiguration);

    }
}
