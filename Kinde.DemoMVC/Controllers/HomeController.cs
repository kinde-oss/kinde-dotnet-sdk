using Kinde.Authorization.Enums;
using Kinde.Authorization.Models.Configuration;
using Kinde.DemoMVC.Authorization;
using Kinde.DemoMVC.Models;
using Kinde;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Kinde.DemoMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
       
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("KindeCorrelationId") != null)
            {
                var model = KindeClientFactory.Instance.Get(HttpContext.Session.GetString("KindeCorrelationId")).Token;

                return View("Index", model);
            }
            return View("Index");
        }
     
        public IActionResult Callback()
        {
            return RedirectToAction("Index");
        }
        public IActionResult Login()
        {
            HttpContext.Session.Remove("SkipAuth");
            return RedirectToAction("Privacy");
        }
        public IActionResult SignInPKCE()
        {

           
            HttpContext.Session.SetString("Flow", "PKCE");
            return RedirectToAction("Login");
        }
        
        public IActionResult SigninClientCredentials()
        {
            HttpContext.Session.SetString("Flow", "ClientCredentials");
            return RedirectToAction("Login");
        }
       
        public IActionResult SignInCode()
        {
            HttpContext.Session.SetString("Flow", "Code");
            return RedirectToAction("Login");
        }
        public IActionResult Privacy()
        {
            if (HttpContext.Session.GetString("KindeCorrelationId") != null)
            {
                var model = KindeClientFactory.Instance.Get(HttpContext.Session.GetString("KindeCorrelationId")).Token;
              
                return View("Index",model);
            }
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}