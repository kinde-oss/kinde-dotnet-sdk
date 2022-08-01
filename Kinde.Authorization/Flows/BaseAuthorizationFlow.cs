using Kinde.Authorization.Models.Configuration;
using Kinde.Authorization.Models.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Kinde.Authorization.Flows
{
    public abstract class BaseAuthorizationFlow
    {
        public IAuthorizationConfiguration Configuration { get; private set; }
        public IClientConfiguration ClientConfiguration { get; private set; }
        protected OauthToken Token { get; set; }
        public BaseAuthorizationFlow(IClientConfiguration clientConfiguration, IAuthorizationConfiguration configuration)
        {
            Configuration = configuration;
            ClientConfiguration = clientConfiguration;
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
       
    }
}
