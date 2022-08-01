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

        }
        public OauthToken Token { get; set; }
        public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.TryAddWithoutValidation("cache-control", "no-cache");
            request.Headers.TryAddWithoutValidation("User-Agent", "PostmanRuntime/7.29.2");
            return base.SendAsync(request, cancellationToken);
        }
        

    }
}
