using Kinde.Authorization.Hashing;
using Kinde.Authorization.Models.Configuration;
using Kinde.WebExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kinde.Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        [HttpGet("callback")]
        public async Task<IActionResult> Callback(string code, string state)
        {
            KindeClient.OnCodeRecieved(code, state);
            var client = KindeClientFactory.Instance.GetOrCreate("1QsRoIgEwY5cIuYO16yRecWVundBHSwF5MylLHDkSenOA3FiwqO", new ClientConfiguration("https://testauth.kinde.com", "https://test.domain.com/api/authorize/callback"));
            return Ok(client.AuthotizationState.ToString());
        }
        [HttpGet("client-credentials/test")]
        public async Task<IActionResult> ClientCredentialsTest()
        {
            var factory = Kinde.WebExtensions.KindeClientFactory.Instance;
            var client = factory.GetOrCreate("1QsRoIgEwY5cIuYO16yRecWVundBHSwF5MylLHDkSenOA3FiwqO", new ClientConfiguration("https://testauth.kinde.com", "https://test.domain.com/api/authorize/callback"));
            await client.Authorize(new ClientCredentialsConfiguration("reg@live", "openid", "1QsRoIgEwY5cIuYO16yRecWVundBHSwF5MylLHDkSenOA3FiwqO"));
            return Ok(client.AuthotizationState.ToString());
        }
     
        [HttpGet("client-credentials")]
        public async Task<IActionResult> ClientCredentials(string clientId, string clientSecret, string domain, string replyUrl, string scope)
        {
            var factory = Kinde.WebExtensions.KindeClientFactory.Instance;
            var client = factory.GetOrCreate(clientSecret, new ClientConfiguration(domain, replyUrl));
            await client.Authorize(new ClientCredentialsConfiguration(clientId, scope, clientSecret));
            return Ok(client.AuthotizationState.ToString());
        }

        [HttpGet("authorization-code/test")]
        public async Task<IActionResult> AuthorizationCodeTest()
        {
            var state = Guid.NewGuid().ToString("N");
            var factory = Kinde.WebExtensions.KindeClientFactory.Instance;
            var client = factory.GetOrCreate("1QsRoIgEwY5cIuYO16yRecWVundBHSwF5MylLHDkSenOA3FiwqO", new ClientConfiguration("https://testauth.kinde.com", "https://test.domain.com/api/authorize/callback"));
            await client.Authorize(new AuthorizationCodeConfiguration("reg@live", "openid", "1QsRoIgEwY5cIuYO16yRecWVundBHSwF5MylLHDkSenOA3FiwqO", state));
           
            return Ok(await  client.GetRedirectionUrl(state));
        }

        [HttpGet("PKCE")]
        public async Task<IActionResult> PKCE(string clientId, string clientSecret, string domain, string replyUrl, string scope,string state)
        {
            var factory = Kinde.WebExtensions.KindeClientFactory.Instance;
            var client = factory.GetOrCreate(clientSecret, new ClientConfiguration(domain, replyUrl));
            await client.Authorize(new PKCEConfiguration<SHA256CodeVerifier>(clientId, scope, clientSecret, state));
            return Ok(client.AuthotizationState.ToString());
        }

        [HttpGet("PKCE/test")]
        public async Task<IActionResult> PKCE()
        {
            var state = Guid.NewGuid().ToString("N");
            var factory = Kinde.WebExtensions.KindeClientFactory.Instance;
            var client = factory.GetOrCreate("1QsRoIgEwY5cIuYO16yRecWVundBHSwF5MylLHDkSenOA3FiwqO", new ClientConfiguration("https://testauth.kinde.com", "https://test.domain.com/api/authorize/callback"));
            await client.Authorize(new PKCEConfiguration<SHA256CodeVerifier>("reg@live", "openid", "1QsRoIgEwY5cIuYO16yRecWVundBHSwF5MylLHDkSenOA3FiwqO", state));

            return Ok(await client.GetRedirectionUrl(state));
        }
    }
}
