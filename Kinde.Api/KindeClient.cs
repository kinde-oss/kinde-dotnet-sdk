using Kinde.Api.Enums;
using Kinde.Api.Flows;
using Kinde.Api.Models.Configuration;
using Kinde.Api.Models.Tokens;
using Kinde.Api.Models.Utils;

namespace Kinde
{
    public partial class KindeClient
    {
        public static AuthorizationCodeStore<string, string> CodeStore = new AuthorizationCodeStore<string, string>();
        public AuthotizationStates AuthotizationState { get { return authorizationFlow?.AuthotizationState ?? AuthotizationStates.None; } }
        protected IAuthorizationFlow authorizationFlow { get; set; }
        public OauthToken Token { get { return authorizationFlow.Token; } }
        public IApplicationConfiguration IdentityProviderConfiguration { get; set; }
        public KindeClient(IApplicationConfiguration identityProviderConfiguration, HttpClient httpClient) : this(httpClient)
        {
            IdentityProviderConfiguration = identityProviderConfiguration;
            var businessName = GetSubDomain(IdentityProviderConfiguration.Domain);
            BaseUrl = BaseUrl.Replace("{businessName}", businessName);
            CodeStore = new AuthorizationCodeStore<string, string>();
        }
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder)
        {
            authorizationFlow.AuthorizeRequest(request);
        }
        public async Task Authorize(IAuthorizationConfiguration authorizationConfiguration, bool register = false)
        {
            if (IdentityProviderConfiguration == null)
            {
                throw new ArgumentNullException("Identity provider configuration missing");
            }
            if (authorizationConfiguration == null)
            {
                throw new ArgumentNullException("Authorization flow configuration missing");
            }
            authorizationFlow = authorizationConfiguration.CreateAuthorizationFlow(IdentityProviderConfiguration);

            var state = await authorizationFlow.Authorize(_httpClient, register);
            if (state == AuthotizationStates.NonAuthorized)
            {
                throw new ApplicationException("Authorization failed");
            }
        }
        public async Task<string> GetRedirectionUrl(string state)
        {
            return await authorizationFlow.UserActionsResolver.GetLoginUrl(state);
        }
        public static void OnCodeRecieved(string code, string state)
        {
            CodeStore.Add(state, code);
            OnCodeConsumed(code, state);
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

        public async Task<object?> GetUserProfile()
        {
            if (authorizationFlow.RequiresRedirection)
            {
                return await authorizationFlow.GetUserProfile(_httpClient);
            }
            else
            {
                return null;
            }
           
        }

        public async Task<string> Logout()
        {
             await authorizationFlow.Logout(_httpClient);
            return IdentityProviderConfiguration.Domain +   "/logout?redirect=" + IdentityProviderConfiguration.LogoutUrl;
        }
        public async Task Renew()
        {
            await authorizationFlow.Renew(_httpClient);
        }
    }
}