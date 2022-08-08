using Kinde;
using Kinde.Authorization.Enums;
using Kinde.Authorization.Flows;
using Kinde.Authorization.Models.Configuration;
using Kinde.Authorization.Models.Tokens;
using Kinde.Authorization.Models.Utils;

namespace Kinde
{
    public partial class KindeClient
    {
        public static AuthorizationCodeStore<string, string> CodeStore =  new AuthorizationCodeStore<string, string>();
        public AuthotizationStates AuthotizationState { get { return authorizationFlow?.AuthotizationState ?? AuthotizationStates.None; } }
        protected IAuthorizationFlow authorizationFlow { get; set; } 
        public OauthToken Token { get { return authorizationFlow.Token; } }
        public IClientConfiguration ClientConfiguration { get; set; }
        public KindeClient(IClientConfiguration clientConfiguration, HttpClient httpClient) : this(httpClient)
        {          
            ClientConfiguration = clientConfiguration;
            var businessName = GetSubDomain(clientConfiguration.Domain);
            BaseUrl = BaseUrl.Replace("{businessName}", businessName);
            CodeStore = new AuthorizationCodeStore<string, string>();
        }
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder)
        {
            authorizationFlow.AuthorizeRequest(request);
        }
        public async Task Authorize(IAuthorizationConfiguration authorizationConfiguration)
        {
            authorizationFlow = authorizationConfiguration.CreateAuthorizationFlow(ClientConfiguration);
            await authorizationFlow.Authorize(_httpClient);
        }
        public async Task<string> GetRedirectionUrl(string state)
        {
            return await authorizationFlow.UserActionsResolver.GetLoginUrl(state);
        }
        public static void OnCodeRecieved(string code, string state)
        {
            lock(CodeStore)
            {
                CodeStore.Add(state, code);
            }
        }
        public static void OnCodeConsumed(string code, string state)
        {
            lock (CodeStore)
            {
                CodeStore.Remove(state);
            }
        }
        private string GetSubDomain(string _url)
        {
            var url = new Uri(_url);
            if (url.HostNameType == UriHostNameType.Dns)
            {
                string host = url.Host;
                if (host.Split('.').Length > 2)
                {
                    int lastIndex = host.LastIndexOf(".");
                    int index = host.LastIndexOf(".", lastIndex - 1);
                    return host.Substring(0, index);
                }
            }
            return null;

        }

    }
}