using Kinde.Authorization.Models.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinde.Authorization.Models
{
    public class KindeHttpClient : HttpClient
    {
        public KindeHttpClient() : base(new HttpClientHandler() { AllowAutoRedirect = false })
        {
            DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "PostmanRuntime/7.29.2"); 
        }
        private OauthToken token = null!;
        public OauthToken Token
        {
            get
            {
                return token;
            }
            set
            {
                token = value;
                DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token?.AccessToken);
            }
        }
        public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.TryAddWithoutValidation("cache-control", "no-cache");
            request.Headers.TryAddWithoutValidation("User-Agent", "PostmanRuntime/7.29.2");
            if (Token != null) request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + Token.AccessToken);
            
            return base.SendAsync(request, cancellationToken);
        }
        public override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.TryAddWithoutValidation("cache-control", "no-cache");
            request.Headers.TryAddWithoutValidation("User-Agent", "PostmanRuntime/7.29.2");
            if (Token != null) request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + Token.AccessToken);
          
            return base.Send(request, cancellationToken);
        }

    }
}
