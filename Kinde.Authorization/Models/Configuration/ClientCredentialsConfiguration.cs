using Kinde.Authorization.Enums;
using Kinde.Authorization.Flows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinde.Authorization.Models.Configuration
{
    public class ClientCredentialsConfiguration : BaseAuthorizationConfiguration
    {

        public ClientCredentialsConfiguration(string clientId, string scope, string clientSecret) : base(clientId, clientSecret, GrantTypes.ClientCredentials, scope)
        {

        }

        public override IAuthorizationFlow CreateAuthorizationFlow(IClientConfiguration clientConfiguration)
        {
            return new ClientCredentialsFlow(clientConfiguration, this);
        }
    }
}
