using Kinde.Api.Flows;

namespace Kinde.Api.Models.Configuration
{
    public class AuthorizationCodeConfiguration : BaseAuthorizationConfiguration, IRedirectAuthorizationConfiguration
    {
        public string State { get; set; }
        public AuthorizationCodeConfiguration(string clientId, string scope, string clientSecret, string? state, string grantType) : base(clientId, clientSecret, grantType, scope)
        {
            if (state == null)
            {
                State = Guid.NewGuid().ToString("N");
            }
            else
            {
                State = state;
            }
        }

        public AuthorizationCodeConfiguration(string clientId, string scope, string clientSecret, string? state) : base(clientId, clientSecret, "AuthorizationCode", scope)
        {
            if (state == null)
            {
                State = Guid.NewGuid().ToString("N");
            }
            else
            {
                State = state;
            }

        }

        public override IAuthorizationFlow CreateAuthorizationFlow(IIdentityProviderConfiguration identityProviderConfiguration)
        {
            return new AuthorizationCodeFlow(identityProviderConfiguration, this);
        }
    }
}
