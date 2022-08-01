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

namespace Kinde.Authorization.Flows
{
    public class AuthorizationCodeFlow : BaseAuthorizationFlow, IAuthorizationFlow, ICodeFlow
    {
        private AuthorizationCodeConfiguration internalConfiguration { get { return (AuthorizationCodeConfiguration)Configuration; } }

        public IUserActionResolver UserActionsResolver => new AuthorizationCodeUserActionResolver();

        public AuthorizationCodeFlow(IClientConfiguration clientConfiguration, IAuthorizationConfiguration configuration) : base(clientConfiguration, configuration)
        {

        }
        public async Task<AuthotizationStates> Authorize(HttpClient httpClient)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("response_type", "code");
            parameters.Add("grant_type", "authorization_code");
            parameters.Add("client_id", internalConfiguration.ClientId);
            parameters.Add("redirect_uri", ClientConfiguration.ReplyUrl);
            parameters.Add("scope", internalConfiguration.Scope);
            parameters.Add("state", internalConfiguration.State);
            parameters.Add("client_secret", internalConfiguration.ClientSecret);
            var resposne =  await httpClient.PostAsync(ClientConfiguration.Domain + "/oauth2/auth", BuildContent(parameters));
            await UserActionsResolver.SetLoginUrl(ClientConfiguration.Domain+ resposne.Headers.Location.ToString(), internalConfiguration.State);
            KindeClient.CodeStore.ItemAdded += CodeStore_ItemAdded;   
            return AuthotizationStates.UserActionsNeeded;
        }

        private async void CodeStore_ItemAdded(object? sender, Models.Utils.ItemAddedEventArgs<string, string> e)
        {
            if (e.Key != internalConfiguration.State) return;
            await OnCodeRecieved(e.Key, e.Value);
        }

        public Task Logout(HttpClient httpClient)
        {
            throw new NotImplementedException();
        }

        public Task Renew(HttpClient httpClient)
        {
            throw new NotImplementedException();
        }

        public async Task OnCodeRecieved( string state, string code)
        {
            var httpClient = new Kinde.Authorization.Models.KindeHttpClient();
            var parameters = new Dictionary<string, string>();

            parameters.Add("grant_type", "code");
            parameters.Add("client_id", internalConfiguration.ClientId);
            parameters.Add("client_secret", internalConfiguration.ClientSecret);
            parameters.Add("scope", internalConfiguration.Scope);
            parameters.Add("code_challenge", code);
            parameters.Add("code_challenge_method", "plain");
            var response = await httpClient.PostAsync(ClientConfiguration.Domain + "/oauth2/token", BuildContent(parameters));
            var tokenString = await response.Content.ReadAsStringAsync();
            Token = JsonConvert.DeserializeObject<OauthToken>(tokenString);

        }
    }
}
