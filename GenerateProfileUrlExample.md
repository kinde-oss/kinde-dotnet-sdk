# Generate Profile URL

This document explains how to use the `GenerateProfileUrl` method to create URLs for the user profile portal.

## Overview

The `GenerateProfileUrl` method allows you to generate a URL that redirects users to their profile portal where they can manage their account, update their profile, and view their entitlements.

## Method Signature

```csharp
public async Task<GetPortalLink> GenerateProfileUrl(GenerateProfileUrlOptions options)
```

## Parameters

The method takes a `GenerateProfileUrlOptions` object with the following properties:

- **`Domain`** (string, required): The domain of the Kinde instance
- **`ReturnUrl`** (string, required): URL to redirect to after completing the profile flow
- **`SubNav`** (PortalPage, optional): Sub-navigation section to display (defaults to Profile)

## PortalPage Enum

The `PortalPage` enum provides the following options:

- **`Profile`**: Profile page (default)
- **`Billing`**: Billing page
- **`Settings`**: Settings page

## Usage Example

```csharp
using Kinde.Api.Client;
using Kinde.Api.Models.Configuration;
using Kinde.Api.Enums;

// Create your application configuration
var appConfig = new ApplicationConfiguration
{
    Domain = "https://your-domain.kinde.com",
    ReplyUrl = "https://your-app.com/callback",
    LogoutUrl = "https://your-app.com/logout"
};

// Create Kinde client
var httpClient = new HttpClient();
var kindeClient = new KindeClient(appConfig, httpClient);

// Authenticate the user first
var authConfig = new AuthorizationCodeConfiguration(
    clientId: "your_client_id",
    scope: "openid profile email",
    clientSecret: "your_client_secret",
    state: "optional_state",
    audience: "https://your-domain.kinde.com"
);

await kindeClient.Authorize(authConfig);

// Generate profile URL
var options = new GenerateProfileUrlOptions
{
    Domain = "https://your-domain.kinde.com",
    ReturnUrl = "https://your-app.com/dashboard",
    SubNav = PortalPage.Profile
};

var result = await kindeClient.GenerateProfileUrl(options);

// Redirect user to the portal
string portalUrl = result.Url;
// Use portalUrl to redirect the user
```

## Error Handling

The method throws the following exceptions:

- **`ArgumentNullException`**: If options is null
- **`ArgumentException`**: If Domain or ReturnUrl is null/empty, or if ReturnUrl is not an absolute URL
- **`ApplicationException`**: If user is not authenticated, access token is not found, or API request fails

## Example with Different Portal Pages

```csharp
// Generate billing portal URL
var billingOptions = new GenerateProfileUrlOptions
{
    Domain = "https://your-domain.kinde.com",
    ReturnUrl = "https://your-app.com/billing",
    SubNav = PortalPage.Billing
};

var billingResult = await kindeClient.GenerateProfileUrl(billingOptions);

// Generate settings portal URL
var settingsOptions = new GenerateProfileUrlOptions
{
    Domain = "https://your-domain.kinde.com",
    ReturnUrl = "https://your-app.com/settings",
    SubNav = PortalPage.Settings
};

var settingsResult = await kindeClient.GenerateProfileUrl(settingsOptions);
```

## Important Notes

1. **Authentication Required**: The user must be authenticated before calling this method
2. **Absolute URLs**: The ReturnUrl must be an absolute URL
3. **Token Validation**: The method automatically validates the access token
4. **URL Validation**: The returned URL is validated to ensure it's a well-formed absolute URL
5. **Error Handling**: Always handle exceptions appropriately in your application

## API Endpoint

The method calls the following API endpoint:
```
GET {domain}/account_api/v1/portal_link?sub_nav={subnav}&return_url={returnUrl}
```

With the Authorization header:
```
Authorization: Bearer {access_token}
``` 