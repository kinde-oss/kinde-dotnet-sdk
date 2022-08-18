using Kinde.Authorization.Enums;
using Kinde.Authorization.Hashing;
using Kinde.Authorization.Models.Configuration;
using Kinde.WebExtensions;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace Kinde.DemoMVC.Authorization
{
    public class KindeAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuthorizationConfigurationProvider _configurationProvider;
        private  IAuthorizationConfiguration _configuration;
        public KindeAuthorizationMiddleware(RequestDelegate next, IAuthorizationConfigurationProvider configurationProvider)
        {
            _next = next;
            _configurationProvider = configurationProvider;
        }
        public async Task Invoke(HttpContext context)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("KindeSettings");
            var domain = config.GetValue<string>("Domain");
            var callbackUrl = config.GetValue<string>("CallbackUrl");
            string correlationId = context.Session?.GetString("KindeCorrelationId");
            if (string.IsNullOrEmpty(correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
                context.Session.SetString("KindeCorrelationId", correlationId);
            }
            _configuration = _configurationProvider.Get();
            var client = KindeClientFactory.Instance.GetOrCreate(correlationId, new IdentityProviderConfiguration(domain, callbackUrl));
            if (client.AuthotizationState == AuthotizationStates.Authorized)
            {
                //congrats, all is good, chcck if identity exists and return
                //var id = await client.GetUserProfile();
                if(context.Session.GetString("KindeProfile") == null)
                {
                    context.Session.SetString("KindeProfile", JsonConvert.SerializeObject(new object(), Formatting.Indented));
                }
               
                await _next(context);
                return;
            }
            if (client.AuthotizationState == AuthotizationStates.None)
            {
                //means we didn't even started auth flow
                await client.Authorize(_configuration);
                if (client.AuthotizationState == AuthotizationStates.UserActionsNeeded)
                {
                    context.Response.Redirect(await client.GetRedirectionUrl(correlationId));
                    return;
                }
                else if(client.AuthotizationState == AuthotizationStates.Authorized)
                {
                    context.Session.SetString("KindeProfile", JsonConvert.SerializeObject(new object(), Formatting.Indented));
                    await _next(context);
                    return;
                }

            }
            if (client.AuthotizationState == AuthotizationStates.UserActionsNeeded)
            {
                if (callbackUrl.Contains(context.Request.Host.Value) && callbackUrl.Contains(context.Request.Path.Value))
                {
                    var code = context.Request.Query["code"];
                    var state = context.Request.Query["state"];
                    KindeClient.OnCodeRecieved(code, state);
                    if(client.AuthotizationState == AuthotizationStates.Authorized)
                    {
                        var id = await client.GetUserProfile();
                        context.Session.SetString("KindeProfile", JsonConvert.SerializeObject(id, Formatting.Indented));
                        await _next(context);
                    }
                    else
                    {
                        throw new ApplicationException("User was not authenticated");
                        
                    }

                }
                else
                {
                    throw new ApplicationException($"Wrong request: expected call to {callbackUrl}, requested {context.Request.Host.Value}{context.Request.Path.Value} instead");
                }

            }
        }
    }
}
