### Overview

The Kinde .NET SDK allows developers to quickly and securely integrate a new or an existing .NET application to the Kinde platform. The Kinde SDK is available on the Nuget package repository at https://www.nuget.org/packages/Kinde.SDK

It contains 3 pre-built OAuth2 grants: 
- Client credentials
- Authorization code 
- Authorization code with PKCE

### Build

Visual Studio automatically recreates the API access client using the [Kinde Management API specs](https://kinde.com/api/kinde-mgmt-api-specs.yaml) on build.


### Getting Started

#### Kinde configuration

Before working with the SDK, it is necessary to [create an api](https://kinde.com/docs/developer-tools/register-an-api/) and [create an application](https://kinde.com/docs/developer-tools/add-a-m2m-application-for-api-access/) using the Kinde admin panel.

#### Configuration

##### Please don't use the constructor without the <code>IIdentityProviderConfiguration</code> parameter, otherwise the constructor will throw exceptions.

Environment settings are located in <code>IIdentityProviderConfiguration</code>. These are common settings for any user in the application's scope. The Identity provider configuration contains the following parameters:
- Domain: The Kinde domain you have registered with
- ReplyUrl: When using Authorisation code or PKCE grants, the callback URL as setup in Kinde. This value can be null for client credentials grant.
- LogoutUrl: Url for redirection after logout.

Additional requirements (based on flow):
- ClientID: Required for all grants
- ClientSecret: Required for the Authorisation Code and the client credentials grant
- Scope: Required for all grants
- State: Optional. If it's set to null, it will be autogenerated. <b> Note, that this parameter should not be a constant. It should be random for each call.</b>
- Code verifier generic parameter: Required. Use inbuilt SHA256CodeVerifier, or create another one if needed. 

The SDK supports configuring from values defined in the `appsetings.json` file. Also, you can write your own implementation of ```IAuthorizationConfigurationProvider``` and ```IIdentityProviderConfigurationProvider```.

Configuration example:
```json
  "ApplicationConfiguration": {
    "Domain": "https://testauth.kinde.com",
    "ReplyUrl": "https://localhost:7165/home/callback",
    "LogoutUrl":  "https://localhost:7165/home"
  },
  "DefaultAuthorizationConfiguration": {
    "ConfigurationType": "Kinde.Api.Models.Configuration.PKCES256Configuration",
    "Configuration": {
      "State": null,
      "ClientId": "12354359asf123rasfaf",
      "Scope": "openid offline",
      "GrantType": "code id_token token",
      "ClientSecret": "<my secret>"
    }
  },
```

You should register your configuration providers using .NET DI:
```csharp
builder.Services.AddTransient<IAuthorizationConfigurationProvider, DefaultAuthorizationConfigurationProvider>();
builder.Services.AddTransient<IApplicationConfigurationProvider, DefaultApplicationConfigurationProvider>();
```

The available grant types are:
1. Kinde.Api.Models.Configuration.PKCES256Configuration
2. Kinde.Api.Models.Configuration.AuthorizationCodeConfiguration
3. Kinde.Api.Models.Configuration.ClientCredentialsConfiguration

Besides configuration, all code approaches are quite similar. The main difference is whether the chosen grant requires redirection/callback.

For the client credentials grant, the ```Authorize()``` call is enough for authorization.

For the other 2 grants (PKCE and Authorization Code), you should handle redirection to Kinde (as IdP) and handle callback in your application.

### Integration in your application

The main class used for integration is the ```KindeClientFactory``` class. It provides thread safe instances of a Kinde client. It is not mandatory to use it, you can create your own solution for your web app to control access.

#### Authentication

#### 1. Login with no redirection, using the Client Credentials grant
In trusted environments, such as a backend app, the client credentials grant can be used.

Example: <br>
```csharp
var client = new KindeClient(
    new IdentityProviderConfiguration("https://testauth.kinde.com", "https://test.domain.com/callback", "https://test.domain.com/logout"), 
    new KindeHttpClient());
await client.Authorize(new ClientCredentialsConfiguration("clientId_here", "openid offline any_other_scope", "client secret here"));

// Api call
var oauthApi = new OAuthApi(client);
var user = await oauthApi.GetUserProfileV2Async();
```

After the authorization is complete, you can use the client to call any management API methods.

#### 2. Login with redirection, using PKCE256 flow or Authorization code

For front-end applications, or applications running in an untrusted environment, such as web applications, the Kinde client should be associated to user session. To keep them in sync, use the <code>KindeClientFactory</code> class. It has a thread safe dictionary to save client instances. It is highly recommended to use SessionId or something similar as a key for the instance.

Example:<br>
```csharp
    public async Task<IActionResult> Login()
    {
        // We need some artificial id to correlate user session to client instance
        // NOTE: Session.Id will be always random, we need to add something to session to make it persistent. 
        var correlationId = HttpContext.Session?.GetString("KindeCorrelationId");
        if (string.IsNullOrEmpty(correlationId))
        {
            correlationId = Guid.NewGuid().ToString();
            HttpContext.Session?.SetString("KindeCorrelationId", correlationId);
        }

        // Get client's instance...
        var client = KindeClientFactory.Instance.GetOrCreate(correlationId, _appConfigurationProvider.Get());

        // ...and authorize it
        await client.Authorize(_authConfigurationProvider.Get());

        // if auth flow is not ClientCredentials flow, we need to redirect user to another page
        if (client.AuthorizationState == Api.Enums.AuthorizationStates.UserActionsNeeded)
        {
            // redirect user to login page
            return Redirect(await client.GetRedirectionUrl(correlationId));
        }

        return RedirectToAction("Index");
    }
```

This code won't authenticate the user completely. We should wait for data on callback endpoint and execute this: <br>
```csharp
    public IActionResult Callback(string code, string state)
    {
        KindeClient.OnCodeReceived(code, state);
        string correlationId = HttpContext.Session?.GetString("KindeCorrelationId");
        var client = KindeClientFactory.Instance.Get(correlationId); //already authorized instance
        // Api call
        //   ...
        var myOrganization  = client.CreateOrganizationAsync("My new best organization");
        return RedirectToAction("Index");
    }

```
#### Register user
User registration is same as authorization. With one small difference:
```csharp
    public async Task<IActionResult> SignUp()
    {
        string correlationId = HttpContext.Session?.GetString("KindeCorrelationId");
        if (string.IsNullOrEmpty(correlationId))
        {
            correlationId = Guid.NewGuid().ToString();
            HttpContext.Session.SetString("KindeCorrelationId", correlationId);
        }

        var client = KindeClientFactory.Instance.GetOrCreate(correlationId, _appConfigurationProvider.Get());
        await client.Register(_authConfigurationProvider.Get()) //<--- Register, if needed
        if (client.AuthorizationState == Api.Enums.AuthorizationStates.UserActionsNeeded)
        {
            return Redirect(await client.GetRedirectionUrl(correlationId));
        }

        return RedirectToAction("Index");
    }
```

#### Logout

Logout has two steps: local cache cleanup and token revocation on Kinde side. Call the client.Logout() method. This method returns the redirect url setup for logout. The user should be redirected to it.

Logout example:
```csharp
    public async Task<IActionResult> Logout()
    {
        string correlationId = HttpContext.Session?.GetString("KindeCorrelationId");
        
        var client = KindeClientFactory.Instance.GetOrCreate(correlationId, _appConfigurationProvider.Get());
        var url = await client.Logout();
        
        return Redirect(url);
    }
```

#### Access token

There is a method ```GetToken``` to return the access token with auto-refresh mechanism. 

```csharp
    var client = KindeClientFactory.Instance.GetOrCreate(correlationId, _appConfigurationProvider.Get());
    var accessToken = client.GetToken(); //returns raw access token
```

#### Token renewal
Token will renew automatically in the background. If you want to do it manually, you can call the Renew method.
Example:
```csharp
    public async Task<IActionResult> Renew()
    {
        string correlationId = HttpContext.Session?.GetString("KindeCorrelationId");

        var client = KindeClientFactory.Instance.GetOrCreate(correlationId, _appConfigurationProvider.Get());
        await client.Renew();

        return RedirectToAction("Index");
    }
```
#### Getting user information

There is a method ```GetUserDetails``` to get user profile. 

```csharp
    var client = KindeClientFactory.Instance.GetOrCreate(correlationId, _appConfigurationProvider.Get());
    var claim = client.GetUserDetails(); //returns user profile
```

#### Getting token details

Note, that some of claims and properties will be unavailable if scope 'profile' wasn't used while authorizing. In this case null will be returned.
```csharp
    var client = KindeClientFactory.Instance.GetOrCreate(correlationId, _appConfigurationProvider.Get());
    var claim = client.GetClaim("sub", "id_token"); //get claim
    var organisations = client.GetOrganisations(); ; //get available organisations
    var organisation = client.GetOrganisation();  //get single organisation
    var permissions = client.GetPermissions(); //get all permissions
    var permission = client.GetPermission("something"); //get permission
```

#### Feature Flags - Helper methods
Feature flags are found in the feature_flags claim of the access token.

```csharp
    var client = KindeClientFactory.Instance.GetOrCreate(correlationId, _appConfigurationProvider.Get());
    var featureFlag = client.GetFlag("feature_flag_code");
    var stringFlag = client.GetStringFlag("theme");
    var booleanFlag = client.GetBooleanFlag("is_dark_mode");
    var intFlag = client.GetIntegerFlag("competitions_limit");
```

## Additional Usage

### Calling APIs


```csharp
    // Don't forget to add "using Kinde;", all data objects models located in this namespace 
    var client = KindeClientFactory.Instance.GetOrCreate(correlationId, _appConfigurationProvider.Get());
    var userApi = new UsersApi(client);
    var users = await userApi.GetUsersAsync(sort: "name_asc", pageSize: 20, userId: null, nextToken: "next", cancellationToken: CancellationToken.None);
    foreach (User user in users.Users)
    {
        Console.WriteLine($"{user.FirstName} {user.LastName} is awesome!");
    }
```
The Full API Documentation can be found [here](https://kinde.com/api/docs/#kinde-management-api).

More usage examples can be found in Kinde.DemoMvc project.
