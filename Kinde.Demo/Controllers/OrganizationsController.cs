using Kinde.Authorization.Models;
using Kinde.Authorization.Models.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kinde.Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        public OrganizationsController()
        {

        }
        [HttpGet("test")]
        public async Task<IActionResult> CreateTest()
        {
            var factory = Kinde.WebExtensions.KindeClientFactory.Instance;
            var client = factory.GetOrCreate("1QsRoIgEwY5cIuYO16yRecWVundBHSwF5MylLHDkSenOA3FiwqO", new ClientConfiguration("https://testauth.kinde.com", "https://test.akno.one/callback"));
            await client.Authorize(new ClientCredentialsConfiguration("reg@live", "openid", "1QsRoIgEwY5cIuYO16yRecWVundBHSwF5MylLHDkSenOA3FiwqO"));
            return Ok(await client.CreateOrganizationAsync("dungeon"));
        }
        [HttpGet]
        public async Task<IActionResult> Create(string name)
        {
            var factory = Kinde.WebExtensions.KindeClientFactory.Instance;
            var client = factory.GetOrCreate("1QsRoIgEwY5cIuYO16yRecWVundBHSwF5MylLHDkSenOA3FiwqO", new ClientConfiguration("https://testauth.kinde.com", "https://test.akno.one/callback"));
            await client.Authorize(new ClientCredentialsConfiguration("reg@live", "openid", "1QsRoIgEwY5cIuYO16yRecWVundBHSwF5MylLHDkSenOA3FiwqO"));
            return Ok(await client.CreateOrganizationAsync(name));
        }

    }
}
