using Kinde.Authorization.Enums;
using Kinde.Authorization.Flows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinde.Authorization.Models.Configuration
{
    public class AuthorizationCodeConfiguration : BaseAuthorizationConfiguration
    {
        public string State { get; set; }

        public AuthorizationCodeConfiguration(string clientId, string scope, string clientSecret, string state) : base(clientId, clientSecret, GrantTypes.Code, scope)
        {
            State = state;
        }

        public override IAuthorizationFlow CreateAuthorizationFlow(IClientConfiguration clientConfiguration)
        {
            return new AuthorizationCodeFlow(clientConfiguration, this);
        }
    }
}
