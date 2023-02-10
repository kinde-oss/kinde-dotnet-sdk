using Kinde.Api.Flows;
using Kinde.Api.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinde.Api.Models.Configuration
{
    public class PKCES256Configuration : PKCEConfiguration<SHA256CodeVerifier>
    {
        public PKCES256Configuration()
        {
            CodeVerifier = new SHA256CodeVerifier();
        }
        public PKCES256Configuration(string clientId, string scope, string clientSecret, string? state) : base(clientId, scope, clientSecret, state)
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
