# Billing Parameters for Registration

This document explains how to use the new billing parameters `plan_interest` and `pricing_table_key` when registering users with Kinde.

## Overview

The Kinde .NET SDK now supports billing parameters that can be passed during user registration. These parameters are only included in the authorization URL when using the `Register` method (not the `Authorize` method).

## Available Parameters

- **`plan_interest`**: Specifies the plan the user is interested in
- **`pricing_table_key`**: Specifies the pricing table key for billing

## Usage Example

```csharp
using Kinde.Api.Client;
using Kinde.Api.Models.Configuration;

// Create your application configuration
var appConfig = new ApplicationConfiguration
{
    Domain = "https://your-domain.kinde.com",
    ReplyUrl = "https://your-app.com/callback",
    LogoutUrl = "https://your-app.com/logout"
};

// Create authorization configuration with billing parameters
var authConfig = new AuthorizationCodeConfiguration(
    clientId: "your_client_id",
    scope: "openid profile email",
    clientSecret: "your_client_secret",
    state: "optional_state",
    audience: "https://your-domain.kinde.com"
)
{
    // Set billing parameters for registration
    PlanInterest = "pro_plan",
    PricingTableKey = "pricing_table_123"
};

// Create Kinde client
var httpClient = new HttpClient();
var kindeClient = new KindeClient(appConfig, httpClient);

// Register user with billing parameters
await kindeClient.Register(authConfig);

// Get the registration URL (this will include the billing parameters)
string registrationUrl = await kindeClient.GetRedirectionUrl(authConfig.State);
```

## Generated URL

When using the `Register` method with billing parameters, the generated authorization URL will include:

```
https://your-domain.kinde.com/oauth2/auth?
  client_id=your_client_id&
  scope=openid%20profile%20email&
  response_type=code&
  grant_type=authorization_code&
  state=optional_state&
  start_page=registration&
  plan_interest=pro_plan&
  pricing_table_key=pricing_table_123&
  redirect_uri=https%3A%2F%2Fyour-app.com%2Fcallback
```

## Important Notes

1. **Registration Only**: These parameters are only included when using the `Register` method, not the `Authorize` method
2. **Optional Parameters**: Both parameters are optional and will only be included in the URL if they have non-empty values
3. **URL Encoding**: The parameters are automatically URL-encoded when added to the authorization URL
4. **Backward Compatibility**: Existing code will continue to work without any changes

## Supported Authorization Flows

The billing parameters are supported in all authorization flows that support registration:

- Authorization Code Flow (`AuthorizationCodeConfiguration`)
- PKCE Flow (`PKCES256Configuration`)

Note: Client Credentials Flow does not support registration, so billing parameters are not applicable. 