using Kinde.Authorization.Enums;
using Kinde.Authorization.Flows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinde.Authorization.Models.Configuration
{
    public class AuthorizationCodeConfiguration : BaseAuthorizationConfiguration, IRedirectAuthorizationConfiguration
    {
        public string State { get; set; }
        public AuthorizationCodeConfiguration(string clientId, string scope, string clientSecret, string? state,string grantType) : base(clientId, clientSecret,grantType, scope)
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
            if(state == null)
            {
                State = Guid.NewGuid().ToString("N");
            }
            else
            {
                State = state;
            }
            
        }

        public override IAuthorizationFlow CreateAuthorizationFlow(IClientConfiguration clientConfiguration)
        {
            return new AuthorizationCodeFlow(clientConfiguration, this);
        }
    }
}
