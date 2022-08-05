using Kinde.Authorization.Enums;
using Kinde.Authorization.Models.Configuration;
using Kinde.Authorization.Models.Tokens;
using Kinde.Authorization.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Kinde.Authorization.Flows
{
    public abstract class BaseAuthorizationFlow<TConfig>:IAuthorizationFlow where TConfig : IAuthorizationConfiguration
    {
        public AuthotizationStates AuthotizationState { get; set; }
        public TConfig Configuration { get; private set; }
        public IClientConfiguration ClientConfiguration { get; private set; }
        protected OauthToken Token { get; set; } = null!;

        public virtual IUserActionResolver UserActionsResolver { get; init; } = new DefaultUserActionResolver();

        public virtual bool RequiresRedirection => true;

        public BaseAuthorizationFlow(IClientConfiguration clientConfiguration, TConfig configuration)
        {
            Configuration = configuration;
            ClientConfiguration = clientConfiguration;
        }
        protected virtual Dictionary<string,string> CreateBaseRequestParameters()
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("client_id", Configuration.ClientId);
            parameters.Add("client_secret", Configuration.ClientSecret);
            parameters.Add("scope", Configuration.Scope);
            return parameters;
        }

        protected virtual string BuildUrl(string baseUrl, Dictionary<string, string> parameters)
        {
            return baseUrl + "?" + string.Join("&", parameters.Select(x => HttpUtility.UrlEncode( x.Key) + "="+ HttpUtility.UrlEncode(x.Value)));
        }
        protected virtual FormUrlEncodedContent BuildContent(Dictionary<string, string> parameters)
        {
            string body = string.Join("&", parameters.Select(x => x.Key + "=" + x.Value));

            return new FormUrlEncodedContent(parameters);
        }
        public async Task AuthorizeRequest(HttpRequestMessage httpRequestMessage)
        {
            httpRequestMessage.Headers.TryAddWithoutValidation("cache-control", "no-cache");
            httpRequestMessage.Headers.TryAddWithoutValidation("User-Agent", "PostmanRuntime/7.29.2");
            if (Token != null)
            {
                httpRequestMessage.Headers.TryAddWithoutValidation("Authorization", "Bearer " + Token.AccessToken);
            }

        }

        public virtual Task<AuthotizationStates> Authorize(HttpClient httpClient)
        {
            throw new NotImplementedException("This method MUST be overriden in derived class");
        }

        public virtual async Task Logout(HttpClient httpClient)
        {
            Token = null;
        }

        public async virtual Task Renew(HttpClient httpClient)
        {
            await Authorize(httpClient);
        }
    }
}
