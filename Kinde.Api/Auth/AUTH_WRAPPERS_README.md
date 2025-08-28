# Kinde Auth Wrappers for .NET SDK

## Overview

This document describes the new authentication wrapper classes that have been added to the Kinde .NET SDK, following the same pattern as the Java SDK. These wrappers provide simplified access to claims, permissions, feature flags, roles, and entitlements functionality.

## Architecture

The auth wrappers are implemented as follows:

1. **BaseAuth**: Abstract base class that provides shared methods for accessing sessions and tokens
2. **Claims**: Wrapper for accessing token claims
3. **Permissions**: Wrapper for checking user permissions with hard check fallback
4. **FeatureFlags**: Wrapper for accessing feature flags with hard check fallback
5. **Roles**: Wrapper for checking user roles with hard check fallback
6. **Entitlements**: Wrapper for accessing user entitlements
7. **Auth**: Main client that provides access to all wrapper classes

## Usage

### Basic Usage

```csharp
// Create the main Auth client
var auth = new Auth(kindeClient);

// Access different functionality through the wrapper classes
var claims = auth.Claims();
var permissions = auth.Permissions();
var featureFlags = auth.FeatureFlags();
var roles = auth.Roles();
var entitlements = auth.Entitlements();
```

### Claims

The Claims wrapper provides access to token claims:

```csharp
var claims = auth.Claims();

// Get a specific claim
var userClaim = claims.GetClaim("sub");
Console.WriteLine($"User claim: {userClaim}");

// Get all claims
var allClaims = claims.GetAllClaims();
Console.WriteLine($"All claims: {allClaims.Count}");

// Get typed claims
var email = claims.GetClaim<string>("email");
var userId = claims.GetClaim<int>("user_id");

// Check if claim exists
var hasEmail = claims.HasClaim("email");
```

### Permissions

The Permissions wrapper provides methods for checking user permissions with automatic API fallback:

```csharp
var permissions = auth.Permissions();

// Check if user has a specific permission
var hasPermission = await permissions.HasPermissionAsync("create:todos");

// Check if user has any of multiple permissions
var permissionKeys = new List<string> { "create:todos", "read:todos", "update:todos" };
var hasAnyPermission = await permissions.HasAnyPermissionAsync(permissionKeys);

// Check if user has all permissions
var hasAllPermissions = await permissions.HasAllPermissionsAsync(permissionKeys);

// Get all permissions
var allPermissions = await permissions.GetPermissionsAsync();
```

### Feature Flags

The FeatureFlags wrapper provides access to feature flags with automatic API fallback:

```csharp
var featureFlags = auth.FeatureFlags();

// Check if a feature flag is enabled
var isEnabled = await featureFlags.IsFeatureFlagEnabledAsync("new_ui");

// Get feature flag value
var flagValue = await featureFlags.GetFeatureFlagValueAsync("max_items");

// Get typed feature flag values
var stringFlag = await featureFlags.GetFeatureFlagStringAsync("theme");
var intFlag = await featureFlags.GetFeatureFlagIntegerAsync("max_items");
var boolFlag = await featureFlags.GetFeatureFlagBooleanAsync("beta_features");

// Get all feature flags
var allFlags = await featureFlags.GetFeatureFlagsAsync();
```

### Roles

The Roles wrapper provides methods for checking user roles with automatic API fallback:

```csharp
var roles = auth.Roles();

// Check if user has a specific role
var isAdmin = await roles.HasRoleAsync("admin");

// Check if user has any of multiple roles
var roleKeys = new List<string> { "admin", "moderator", "user" };
var hasAnyRole = await roles.HasAnyRoleAsync(roleKeys);

// Check if user has all roles
var hasAllRoles = await roles.HasAllRolesAsync(roleKeys);

// Get all roles
var allRoles = await roles.GetRolesAsync();
```

### Entitlements

The Entitlements wrapper provides access to user entitlements from the Kinde Accounts API:

```csharp
var entitlements = auth.Entitlements();

// Check if user has a specific entitlement
var hasPremium = await entitlements.HasEntitlementAsync("premium_features");

// Check if user has any of multiple entitlements
var entitlementKeys = new List<string> { "premium_features", "advanced_analytics" };
var hasAnyEntitlement = await entitlements.HasAnyEntitlementAsync(entitlementKeys);

// Check if user has all entitlements
var hasAllEntitlements = await entitlements.HasAllEntitlementsAsync(entitlementKeys);

// Get all entitlements
var allEntitlements = await entitlements.GetAllEntitlementsAsync();

// Get a specific entitlement with full details
var entitlement = await entitlements.GetEntitlementAsync("premium_features");
```

## Complex Access Control

You can combine multiple wrappers for complex access control scenarios:

```csharp
// Check if user can access a premium feature
var canAccessPremium = await permissions.HasPermissionAsync("access:premium") && 
                      await featureFlags.IsFeatureFlagEnabledAsync("premium_features") &&
                      await entitlements.HasEntitlementAsync("premium_features");

// Check if user is an admin or has specific permissions
var isAuthorized = await roles.HasRoleAsync("admin") || 
                  await permissions.HasAnyPermissionAsync(new List<string> { "manage:users", "manage:system" });

// Check multiple requirements
var hasRequirements = await permissions.HasAllPermissionsAsync(new List<string> { "read:users", "write:users" }) &&
                     await roles.HasRoleAsync("admin") &&
                     await featureFlags.IsFeatureFlagEnabledAsync("user-management");
```

## Hard Check Functionality

The wrappers implement "hard check" functionality that automatically falls back to API calls when information is not available in the JWT token:

1. **Token-First Strategy**: Fast in-memory checks when token data is available
2. **Automatic API Fallback**: Falls back to API calls when token data is insufficient
3. **Comprehensive Error Handling**: Graceful handling of all error scenarios
4. **Async Operations**: All API operations are asynchronous

### How Hard Check Works

```csharp
// The wrapper automatically tries token first, then API
var hasPermission = await permissions.HasPermissionAsync("create:users");

// This is equivalent to:
// 1. Check if permission is in token
// 2. If not in token, make API call to /account_api/v1/permissions
// 3. Return the result
```

## Integration with Existing SDK

The auth wrappers are designed to work alongside the existing Kinde .NET SDK functionality. They provide a simplified interface for common authentication and authorization tasks while maintaining compatibility with the existing API.

### Session Management

The wrappers automatically access the current session through the provided KindeClient:

```csharp
// The wrappers automatically get the session from the provided KindeClient
var auth = new Auth(kindeClient);
// No need to manually pass session - it's handled internally
```

### Token Access

The wrappers access tokens through the KindeClient:

```csharp
// Tokens are automatically retrieved from the current KindeClient
var claims = auth.Claims();
var userClaim = claims.GetClaim("sub");
```

## Error Handling

The wrappers include comprehensive error handling:

- **Missing Tokens**: Returns appropriate default values when no token is available
- **Invalid Claims**: Handles missing or malformed claims gracefully
- **API Failures**: Logs errors and returns fallback values when API calls fail
- **Type Conversion**: Safely handles type conversion for feature flag values

## Logging

All wrapper classes include logging for debugging and monitoring:

```csharp
// Logging is automatically configured for each wrapper class
// Debug messages are logged for token access and API calls
// Warning messages are logged for errors and fallbacks
var auth = new Auth(kindeClient, logger);
```

## Migration from Java SDK

If you're migrating from the Java SDK, the .NET wrappers follow the same patterns:

| Java SDK | .NET SDK |
|----------|----------|
| `auth.claims.getClaim("sub")` | `auth.Claims().GetClaim("sub")` |
| `auth.permissions.hasPermission("create:todos")` | `await auth.Permissions().HasPermissionAsync("create:todos")` |
| `auth.featureFlags.isFeatureFlagEnabled("new_ui")` | `await auth.FeatureFlags().IsFeatureFlagEnabledAsync("new_ui")` |
| `auth.roles.hasRole("admin")` | `await auth.Roles().HasRoleAsync("admin")` |
| `auth.entitlements.hasEntitlement("premium")` | `await auth.Entitlements().HasEntitlementAsync("premium")` |

## Examples

See `AuthExample.cs` for comprehensive examples of how to use all the wrapper classes.

## Dependencies

The auth wrappers depend on:
- `Kinde.Api.Client`: Core functionality for authentication and session management
- `Microsoft.Extensions.Logging`: Logging framework
- .NET Standard 2.1+ for async/await support

## Benefits

- **Reliability**: Access control works even when token data is incomplete
- **Performance**: Fast token checks with lazy API loading
- **Simplicity**: Easy-to-use API following existing patterns
- **Flexibility**: Support for complex access control scenarios
- **Consistency**: Same patterns as Java SDK for cross-platform development
