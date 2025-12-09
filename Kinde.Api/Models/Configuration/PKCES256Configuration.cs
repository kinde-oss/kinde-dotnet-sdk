using Kinde.Api.Flows;
using Kinde.Api.Hashing;

namespace Kinde.Api.Models.Configuration
{
    public class PKCES256Configuration : PKCEConfiguration<SHA256CodeVerifier>
    {
        public PKCES256Configuration()
        {
            CodeVerifier = new SHA256CodeVerifier();
        }

        public PKCES256Configuration(string clientId, string clientSecret, string redirectUri, string scope, string? state) : base(clientId, clientSecret, redirectUri, scope, state)
        {
        }

        public override IAuthorizationFlow CreateAuthorizationFlow(IApplicationConfiguration identityProviderConfiguration)
        {
            if (!IsStateValid(State))
            {
                while (State.Length < 43)
                {
                    State += Guid.NewGuid().ToString("N");
                }
            }

            return new PKCESFlow(identityProviderConfiguration, this);
        }
    }
}
