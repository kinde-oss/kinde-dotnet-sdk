# Kinde .NET SDK - Entitlements and Hard Check

## Overview

This implementation adds entitlements and hard check functionality to the Kinde .NET SDK, following the same patterns as the Java SDK. The hard check system provides automatic fallback from token-based checks to API calls when permissions, roles, or feature flags are not available in the JWT token.

## What Was Implemented

### 1. Enhanced OauthToken Class
Added claims extraction methods directly to the existing `OauthToken` class:

```csharp
// Get permissions from token
var permissions = token.GetPermissions();

// Get roles from token  
var roles = token.GetRoles();

// Get feature flags from token
var flags = token.GetFeatureFlags();

// Check specific permissions/roles/flags
var hasPermission = token.HasPermission("read:users");
var hasRole = token.HasRole("admin");
var isEnabled = token.IsFeatureFlagEnabled("beta_features");
```

### 2. KindeTokenChecker Class
A utility class that implements hard check functionality with automatic API fallback:

```csharp
var checker = new KindeTokenChecker(token, accountsClient);

// Check with automatic fallback
var hasPermission = await checker.HasPermissionAsync("read:users");
var hasRole = await checker.HasRoleAsync("admin");
var isEnabled = await checker.IsFeatureFlagEnabledAsync("beta_features");
```

### 3. KindeAccountsClient Class
A client for accessing the Accounts API endpoints:

```csharp
var accountsClient = new KindeAccountsClient(kindeClient);

// Get entitlements
var entitlements = await accountsClient.GetEntitlementsAsync();
var entitlement = await accountsClient.GetEntitlementAsync("premium-features");

// Get user information
var profile = await accountsClient.GetUserProfileAsync();
var organizations = await accountsClient.GetUserOrganizationsAsync();
```

## Usage Examples

### Basic Token Claims
```csharp
// Get claims directly from token
var permissions = token.GetPermissions();
var roles = token.GetRoles();
var flags = token.GetFeatureFlags();

// Check specific values
if (token.HasPermission("read:users")) {
    // User can read users
}

if (token.HasRole("admin")) {
    // User is admin
}

if (token.IsFeatureFlagEnabled("new-ui")) {
    // Show new UI
}
```

### Hard Check with API Fallback
```csharp
// Create token checker
var checker = new KindeTokenChecker(token, accountsClient);

// Check permissions with automatic fallback
var canReadUsers = await checker.HasPermissionAsync("read:users");
var canWriteUsers = await checker.HasPermissionAsync("write:users");

// Check roles with automatic fallback
var isAdmin = await checker.HasRoleAsync("admin");
var isModerator = await checker.HasRoleAsync("moderator");

// Check feature flags with automatic fallback
var betaEnabled = await checker.IsFeatureFlagEnabledAsync("beta-features");
```

### Complex Access Control
```csharp
// Check multiple permissions
var permissions = new List<string> { "read:users", "write:users" };
var hasAnyPermission = await checker.HasAnyPermissionAsync(permissions);
var hasAllPermissions = await checker.HasAllPermissionsAsync(permissions);

// Check multiple roles
var roles = new List<string> { "admin", "moderator" };
var hasAnyRole = await checker.HasAnyRoleAsync(roles);
var hasAllRoles = await checker.HasAllRolesAsync(roles);

// Complex access control
var canManageUsers = await checker.HasAllAsync(
    permissions: new List<string> { "read:users", "write:users" },
    roles: new List<string> { "admin" },
    featureFlags: new List<string> { "user-management" }
);
```

### Entitlements
```csharp
// Get all entitlements
var entitlements = await accountsClient.GetEntitlementsAsync();
foreach (var entitlement in entitlements.Data.Entitlements)
{
    Console.WriteLine($"{entitlement.FeatureName}: {entitlement.FeatureKey}");
}

// Get specific entitlement
var premiumFeatures = await accountsClient.GetEntitlementAsync("premium-features");
Console.WriteLine($"Premium features limit: {premiumFeatures.Data.Entitlement.EntitlementLimitMax}");
```

## API Endpoints Supported

- **Entitlements**: `GET /account_api/v1/entitlements`, `GET /account_api/v1/entitlement/{key}`
- **Permissions**: `GET /account_api/v1/permissions`, `GET /account_api/v1/permission/{key}`
- **Roles**: `GET /account_api/v1/roles`
- **Feature Flags**: `GET /account_api/v1/feature_flags`, `GET /account_api/v1/feature_flags/{key}`
- **User Information**: `GET /account_api/v1/user_profile`, `GET /account_api/v1/user_organizations`, `GET /account_api/v1/current_organization`

## Key Features

- **Token-First Strategy**: Fast in-memory checks when token data is available
- **Automatic API Fallback**: Falls back to API calls when token data is insufficient
- **Comprehensive Error Handling**: Graceful handling of all error scenarios
- **Async Operations**: All API operations are asynchronous
- **Backward Compatible**: Works with existing code

## Files Added

1. **`api/kinde-accounts-api.yaml`** - OpenAPI specification for Accounts API
2. **`generate-accounts-api.sh`** - Script to generate API client code
3. **`Kinde.Api/Accounts/`** - Generated API client code
4. **`Kinde.Api/Accounts/KindeAccountsClient.cs`** - Clean wrapper around generated client
5. **`Kinde.Api/Models/Tokens/KindeTokenChecker.cs`** - Hard check utility class
6. **Enhanced `OauthToken.cs`** - Added claims extraction methods

## Benefits

- **Reliability**: Access control works even when token data is incomplete
- **Performance**: Fast token checks with lazy API loading
- **Simplicity**: Easy-to-use API following existing patterns
- **Flexibility**: Support for complex access control scenarios
