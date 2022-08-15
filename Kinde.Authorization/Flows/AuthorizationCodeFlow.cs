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
    public class AuthorizationCodeFlow : BaseAuthorizationFlow<AuthorizationCodeConfiguration>, IAuthorizationFlow, ICodeFlow
    {
        public override IUserActionResolver UserActionsResolver { get; init; } = new AuthorizationCodeUserActionResolver();

        public AuthorizationCodeFlow(IClientConfiguration clientConfiguration, AuthorizationCodeConfiguration configuration) : base(clientConfiguration, configuration)
        {

        }
        public override async Task<AuthotizationStates> Authorize(HttpClient httpClient)
        {
            var parameters = CreateBaseRequestParameters();
            parameters.Add("response_type", "code");
            parameters.Add("grant_type", "authorization_code");
            parameters.Add("state", Configuration.State);
            return await base.SendRequest(httpClient, parameters);

        }
        public override void OnCodeRecieved(HttpClient httpClient,string state, string code)
        {
          
            var parameters = new Dictionary<string, string>();

            parameters.Add("grant_type", "authorization_code");
            parameters.Add("client_id", Configuration.ClientId);
            parameters.Add("client_secret", Configuration.ClientSecret);
            parameters.Add("scope", Configuration.Scope);
            parameters.Add("code", code);
            parameters.Add("redirect_uri", ClientConfiguration.ReplyUrl);
            SendCode(httpClient, parameters);

        }
    }
}
