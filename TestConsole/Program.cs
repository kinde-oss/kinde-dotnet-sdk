// See https://aka.ms/new-console-template for more information


using Kinde.Api.Models;
using Kinde.Api.Models.Configuration;

Console.WriteLine("Hello, World!");
var client = new Kinde.KindeClient(new IdentityProviderConfiguration("https://testauth.kinde.com", "https://test.akno.one/callback"), new KindeHttpClient());
await client.Authorize(new ClientCredentialsConfiguration("reg@live", "openid", "1QsRoIgEwY5cIuYO16yRecWVundBHSwF5MylLHDkSenOA3FiwqO"));
var c = await client.GetUserAsync();
//await client.GetUsersAsync(null, 10, null, "next");
