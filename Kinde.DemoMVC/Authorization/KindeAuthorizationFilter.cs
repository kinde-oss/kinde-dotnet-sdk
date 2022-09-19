using Kinde.Api.Enums;
using Kinde.Api.Models.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Kinde.DemoMVC.Authorization
{
    public class KindeAuthorizationFilter: IAsyncAuthorizationFilter
    {
     
        private readonly IAuthorizationConfigurationProvider _authConfigurationProvider;
        private readonly IApplicationConfigurationProvider _appConfigurationProvider;
        private IAuthorizationConfiguration _configuration;
        public KindeAuthorizationFilter(IAuthorizationConfigurationProvider authConfigurationProvider, IApplicationConfigurationProvider appConfigurationProvider)
        {

            _authConfigurationProvider = authConfigurationProvider;
            _appConfigurationProvider = appConfigurationProvider;
        }


        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            bool hasAllowAnonymous = context.ActionDescriptor.EndpointMetadata
                                 .Any(em => em.GetType() == typeof(AllowAnonymousAttribute)); 
            if (hasAllowAnonymous) return;

            _configuration = _authConfigurationProvider.Get();
            var appConfiguration = _appConfigurationProvider.Get();
            var domain = appConfiguration.Domain;
            var callbackUrl = appConfiguration.ReplyUrl;
            string correlationId = context.HttpContext.Session?.GetString("KindeCorrelationId");
            if (string.IsNullOrEmpty(correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
                context.HttpContext.Session.SetString("KindeCorrelationId", correlationId);
            }
        
            var client = KindeClientFactory.Instance.GetOrCreate(correlationId, appConfiguration);
            if (client.AuthotizationState == AuthotizationStates.Authorized)
            {
                //congrats, all is good, check if identity exists and return
                //var id = await client.GetUserProfile();
                if (context.HttpContext.Session.GetString("KindeProfile") == null)
                {
                    context.HttpContext.Session.SetString("KindeProfile", JsonConvert.SerializeObject(new object(), Formatting.Indented));
                }

              
                return;
            }
            if (client.AuthotizationState == AuthotizationStates.None)
            {
                //means we didn't even started auth flow
                await client.Authorize(_configuration);
                if (client.AuthotizationState == AuthotizationStates.UserActionsNeeded)
                {
                    
                    context.Result = new RedirectResult(await client.GetRedirectionUrl(correlationId));
                    return;
                }
                else if (client.AuthotizationState == AuthotizationStates.Authorized)
                {
                    context.HttpContext.Session.SetString("KindeProfile", JsonConvert.SerializeObject(new object(), Formatting.Indented));
                   
                    return;
                }

            }
            if (client.AuthotizationState == AuthotizationStates.UserActionsNeeded)
            {
                if (callbackUrl.Contains(context.HttpContext.Request.Host.Value) && callbackUrl.Contains(context.HttpContext.Request.Path.Value))
                {
                    var code = context.HttpContext.Request.Query["code"];
                    var state = context.HttpContext.Request.Query["state"];
                    KindeClient.OnCodeRecieved(code, state);
                    if (client.AuthotizationState == AuthotizationStates.Authorized)
                    {
                        var id = await client.GetUserProfile();
                        context.HttpContext.Session.SetString("KindeProfile", JsonConvert.SerializeObject(id, Formatting.Indented));
                       
                    }
                    else
                    {
                        throw new ApplicationException("User was not authenticated");

                    }

                }
                else
                {
                    throw new ApplicationException($"Wrong request: expected call to {callbackUrl}, requested {context.HttpContext.Request.Host.Value}{context.HttpContext.Request.Path.Value} instead");
                }

            }
        }
    }
}
