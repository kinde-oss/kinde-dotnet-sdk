using Kinde.Authorization.Enums;
using Kinde.Authorization.Hashing;
using Kinde.Authorization.Models.Configuration;
using Kinde.WebExtensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Kinde.DemoMVC.Authorization
{
    public class KindeAuthorize : Attribute, IAuthorizationFilter
    {

        public static Dictionary<GrantTypes, IAuthorizationConfiguration> Mocks = new Dictionary<GrantTypes, IAuthorizationConfiguration>()
        {
            {GrantTypes.ClientCredentials, new ClientCredentialsConfiguration("reg@live", "openid","1QsRoIgEwY5cIuYO16yRecWVundBHSwF5MylLHDkSenOA3FiwqO") },
            {GrantTypes.Code, new AuthorizationCodeConfiguration("reg@live", "openid","1QsRoIgEwY5cIuYO16yRecWVundBHSwF5MylLHDkSenOA3FiwqO", Guid.NewGuid().ToString()) },
            {GrantTypes.PKCE, new PKCEConfiguration<SHA256CodeVerifier>("reg@live", "openid","1QsRoIgEwY5cIuYO16yRecWVundBHSwF5MylLHDkSenOA3FiwqO", Guid.NewGuid().ToString()) },
        };
        public GrantTypes GrantType;
        public KindeAuthorize(GrantTypes type)
        {
            GrantType = type;
        }
        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            var config = new ConfigurationManager();
            var domain = config.GetValue<string>("Domain");
            var callbackUrl = config.GetValue<string>("CallbackUrl");
            var client = KindeClientFactory.Instance.GetOrCreate(context.HttpContext.Session.Id, new ClientConfiguration(domain, callbackUrl));
            await client.Authorize(Mocks[GrantType]);
            if (client.AuthotizationState == AuthotizationStates.Authorized)
            {
                //bla bla bla
            }
            if (client.AuthotizationState == AuthotizationStates.UserActionsNeeded)
            {
                context.HttpContext.Response.Clear();
                context.HttpContext.Response.Redirect(await client.GetRedirectionUrl(((AuthorizationCodeConfiguration)Mocks[GrantType]).State));

            }
        }
    }
}
