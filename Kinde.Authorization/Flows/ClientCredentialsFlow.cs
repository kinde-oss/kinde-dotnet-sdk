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
    public class ClientCredentialsFlow : BaseAuthorizationFlow, IAuthorizationFlow
    {
        private ClientCredentialsConfiguration internalConfiguration { get { return (ClientCredentialsConfiguration)Configuration; } }

        public IUserActionResolver UserActionsResolver => new DefaultUserActionResolver();

        public ClientCredentialsFlow(IClientConfiguration clientConfiguration, IAuthorizationConfiguration configuration):base(clientConfiguration, configuration)
        {
          
        }
        public async Task<AuthotizationStates> Authorize(HttpClient httpClient)
        {
            var parameters = new Dictionary<string, string>();

            parameters.Add("grant_type", "client_credentials");
            parameters.Add("client_id", internalConfiguration.ClientId);
            parameters.Add("client_secret", internalConfiguration.ClientSecret);
            parameters.Add("scope", internalConfiguration.Scope);
        
            var response = await httpClient.PostAsync(ClientConfiguration.Domain + "/oauth2/token", BuildContent(parameters));// BuildContent(parameters) );

            var tokenString = await response.Content.ReadAsStringAsync();
            Token = JsonConvert.DeserializeObject<OauthToken>(tokenString);
            return AuthotizationStates.Authorized;
        }
     
        public async Task Renew(HttpClient httpClient)
        {
            await Authorize(httpClient);
        }
        public async Task Logout(HttpClient httpClient)
        {
            Token = null;
        }
    }
}
