// See https://aka.ms/new-console-template for more information


using Kinde.Authorization.Flows;
using Kinde.Authorization.Models;

Console.WriteLine("Hello, World!");
var client = new Kinde.KindeClient(new ClientConfiguration("https://testauth.kinde.com", "https://test.akno.one/callback"), new KindeHttpClient());
await client.Authorize(new AuthorizationCodeConfiguration("reg@live","openid", "1QsRoIgEwY5cIuYO16yRecWVundBHSwF5MylLHDkSenOA3FiwqO",Guid.NewGuid().ToString("D")));
await client.CreateOrganizationAsync("dungeon");
//await client.GetUsersAsync(null, 10, null, "next");
