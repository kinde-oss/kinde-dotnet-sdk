using Kinde.Api.Models.Configuration;
using Kinde.DemoMVC.Authorization;
using Kinde.DemoMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Kinde.DemoMVC.Controllers
{
    //[TypeFilter(typeof(KindeAuthorizationFilter))]
    public class HomeController : Controller
    {
        private readonly IAuthorizationConfigurationProvider _authConfigurationProvider;
        private readonly IApplicationConfigurationProvider _appConfigurationProvider;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IAuthorizationConfigurationProvider authConfigurationProvider, IApplicationConfigurationProvider appConfigurationProvider)
        {
            _logger = logger;

            _authConfigurationProvider = authConfigurationProvider;
            _appConfigurationProvider = appConfigurationProvider;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("KindeCorrelationId") != null)
            {
                var client = KindeClientFactory.Instance.Get(HttpContext.Session.GetString("KindeCorrelationId"));
                if(client.AuthotizationState == Kinde.Api.Enums.AuthotizationStates.Authorized)
                {
                    ViewBag.Authorized = true;
                    var model = await client.GetUserProfile();
                    
                    return View("Index", model);
                }
              
            }
            return View("Index");
        }

        public IActionResult Callback(string code, string state)
        {
            Kinde.KindeClient.OnCodeRecieved(code, state);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Login()
        {
            string correlationId = HttpContext.Session?.GetString("KindeCorrelationId");
            if (string.IsNullOrEmpty(correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
                HttpContext.Session.SetString("KindeCorrelationId", correlationId);
            }
            var client = KindeClientFactory.Instance.GetOrCreate(correlationId, _appConfigurationProvider.Get());
            await client.Authorize(_authConfigurationProvider.Get());
            if (client.AuthotizationState == Api.Enums.AuthotizationStates.UserActionsNeeded)
            {
                return Redirect(await client.GetRedirectionUrl(correlationId));
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Renew()
        {
            string correlationId = HttpContext.Session?.GetString("KindeCorrelationId");

            var client = KindeClientFactory.Instance.GetOrCreate(correlationId, _appConfigurationProvider.Get());
            await client.Renew();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Logout()
        {
            string correlationId = HttpContext.Session?.GetString("KindeCorrelationId");
          
            var client = KindeClientFactory.Instance.GetOrCreate(correlationId, _appConfigurationProvider.Get());
             var url = await client.Logout();
            
            return Redirect(url);
        }
        public async Task<IActionResult> SignUp()
        {
            string correlationId = HttpContext.Session?.GetString("KindeCorrelationId");
            if (string.IsNullOrEmpty(correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
                HttpContext.Session.SetString("KindeCorrelationId", correlationId);
            }
            var client = KindeClientFactory.Instance.GetOrCreate(correlationId, _appConfigurationProvider.Get());
            await client.Authorize(_authConfigurationProvider.Get(), true);
            if (client.AuthotizationState == Api.Enums.AuthotizationStates.UserActionsNeeded)
            {
                return Redirect(await client.GetRedirectionUrl(correlationId));
            }
            return RedirectToAction("Index");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}