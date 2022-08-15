using Kinde.Authorization.Enums;
using Kinde.Authorization.Models.Configuration;
using Kinde.Authorization.Models.Tokens;
using Kinde.Authorization.Models.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Kinde.Authorization.Flows
{
    public class ClientCredentialsFlow : BaseAuthorizationFlow<ClientCredentialsConfiguration>, IAuthorizationFlow
    {
        public override bool RequiresRedirection => false; 
        public override IUserActionResolver UserActionsResolver => new DefaultUserActionResolver();

        public ClientCredentialsFlow(IClientConfiguration clientConfiguration, ClientCredentialsConfiguration configuration):base(clientConfiguration, configuration)
        {
          
        }
        public override async Task<AuthotizationStates> Authorize(HttpClient httpClient)
        {
            var parameters = CreateBaseRequestParameters();
            parameters.Add("grant_type", "client_credentials");
            return await SendRequest(httpClient, parameters);
        }

        public override void OnCodeRecieved(HttpClient httpClient, string key, string value)
        {
            throw new NotImplementedException("Code is not applicable for this flow");
        }
        public override Task<object> GetUserProfile(HttpClient httpClient)
        {
            throw new NotImplementedException("User profile is not applicable for this flow");
        }
    }
}
