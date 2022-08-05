using Kinde.Authorization.Enums;
using Kinde.Authorization.Hashing;
using Kinde.Authorization.Models.Configuration;
using Kinde.WebExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Kinde.DemoMVC.Authorization
{
    public class KindeAuthorize : Attribute, IAuthorizationFilter
    {

        public static Dictionary<string, IAuthorizationConfiguration> Mocks = new Dictionary<string, IAuthorizationConfiguration>()
        {
            {"ClientCredentials", new ClientCredentialsConfiguration("reg@live", "openid","1QsRoIgEwY5cIuYO16yRecWVundBHSwF5MylLHDkSenOA3FiwqO") },
            {"AuthorizationCode", new AuthorizationCodeConfiguration("reg@live", "openid","1QsRoIgEwY5cIuYO16yRecWVundBHSwF5MylLHDkSenOA3FiwqO", Guid.NewGuid().ToString()) },
            { "PKCE", new PKCEConfiguration<SHA256CodeVerifier>("reg@live", "openid","1QsRoIgEwY5cIuYO16yRecWVundBHSwF5MylLHDkSenOA3FiwqO", Guid.NewGuid().ToString()) },
        };
        public string GrantType;
        public KindeAuthorize(string grantType)
        {
            GrantType = grantType;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("KindeSettings");
            
            
            var domain = config.GetValue<string>("Domain");
            var callbackUrl = config.GetValue<string>("CallbackUrl");
            var client = KindeClientFactory.Instance.GetOrCreate(context.HttpContext.Session.Id, new ClientConfiguration(domain, callbackUrl));
            if(client.AuthotizationState == AuthotizationStates.NonAuthorized || client.AuthotizationState == AuthotizationStates.None)
            {
                context.HttpContext.Session.SetString("KindeCorrelationId", Guid.NewGuid().ToString());
                client.Authorize(Mocks[GrantType]).Wait();
                if (client.AuthotizationState == AuthotizationStates.UserActionsNeeded)
                {
                    var url = ExecyteSync(client.GetRedirectionUrl(((AuthorizationCodeConfiguration)Mocks[GrantType]).State));
                  
                    context.Result = new RedirectResult(url);
                    return;
                }
            }
            if (client.AuthotizationState == AuthotizationStates.UserActionsNeeded)
            {
                if (callbackUrl.Contains(context.HttpContext.Request.Host.Value)&& callbackUrl.Contains(context.HttpContext.Request.Path.Value))
                {
                    var code = context.HttpContext.Request.Query["code"];
                    var state = context.HttpContext.Request.Query["state"];
                    KindeClient.OnCodeRecieved(code, state);
                }
            }
            if (client.AuthotizationState == AuthotizationStates.Authorized)
            {
                //context.HttpContext.User = new System.Security.Claims.ClaimsPrincipal( new ClaimsI)
                //var user = ExecyteSync(client.GetUserAsync());
            }
            

        }
        protected TResult ExecyteSync<TResult>(Task<TResult> task)
        {
            task.Wait();
            if (task.IsFaulted)
            {
                throw task.Exception;
            }
            return task.Result;
        } 
    }
}
