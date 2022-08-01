using Kinde.Authorization.Enums;
using Kinde.Authorization.Flows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinde.Authorization.Models.Configuration
{
    public abstract class BaseAuthorizationConfiguration : IAuthorizationConfiguration
    {
        public BaseAuthorizationConfiguration(string clientId, string clientSecret, GrantTypes grantType, string scope)
        {
            ClientId = clientId;
            GrantType = grantType;
            Scope = scope;
            ClientSecret = clientSecret; ;
        }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }
        public string Scope { get; set; }
        public GrantTypes GrantType { get; set; }

        public abstract IAuthorizationFlow CreateAuthorizationFlow(IClientConfiguration clientConfiguration);

    }
}
