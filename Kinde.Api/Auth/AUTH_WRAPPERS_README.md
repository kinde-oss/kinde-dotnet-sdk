# Kinde Auth Wrappers for .NET SDK

## Overview

This document describes the new authentication wrapper classes that have been added to the Kinde .NET SDK, following the same pattern as the Java SDK. These wrappers provide simplified access to claims, permissions, feature flags, roles, and entitlements functionality.

The implementation uses a **Factory Pattern** to completely hide the complexity of the underlying Accounts API from users, providing a clean and intuitive interface.

## Architecture

The auth wrappers are implemented as follows:

1. **BaseAuth**: Abstract base class that provides shared methods for accessing sessions and tokens
2. **Claims**: Wrapper for accessing token claims
3. **Permissions**: Wrapper for checking user permissions via Accounts API
4. **FeatureFlags**: Wrapper for accessing feature flags via Accounts API
5. **Roles**: Wrapper for checking user roles via Accounts API
6. **Entitlements**: Wrapper for accessing user entitlements via Accounts API
7. **Auth**: Main client that provides access to all wrapper classes
8. **KindeClient**: Enhanced with an `Auth` property that automatically manages all dependencies

## Usage

### Basic Usage (Recommended)

The simplest way to use the auth wrappers is through the `KindeClient.Auth` property:

```csharp
// Create the KindeClient (this automatically sets up the Auth property)
var kindeClient = new KindeClient(identityProviderConfig, httpClient);

// Access authentication functionality directly - no setup required!
var hasPermission = await kindeClient.Auth.Permissions().HasPermissionAsync("create:users");
var isFeatureEnabled = await kindeClient.Auth.FeatureFlags().IsFeatureFlagEnabledAsync("new-ui");
var userRoles = await kindeClient.Auth.Roles().GetRolesAsync();
var entitlements = await kindeClient.Auth.Entitlements().GetAllEntitlementsAsync();
```

### Advanced Usage (Direct Instantiation)

For advanced scenarios, you can still create the Auth client directly:

```csharp
// Create the main Auth client (requires HttpClient parameter)
var auth = new Auth(kindeClient, httpClient);

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

## Accounts API Integration

The wrappers directly integrate with the Kinde Accounts API to provide real-time access to user permissions, roles, feature flags, and entitlements:

1. **Direct API Access**: All data is retrieved directly from the Accounts API
2. **Real-time Data**: Always up-to-date information from Kinde
3. **Comprehensive Error Handling**: Graceful handling of all error scenarios
4. **Async Operations**: All API operations are asynchronous
5. **Automatic Dependency Management**: The `IKindeAccountsClient` is managed internally

### How API Integration Works

```csharp
// The wrapper makes direct API calls to the Accounts API
var hasPermission = await permissions.HasPermissionAsync("create:users");

// This makes a call to: GET /account_api/v1/permissions
// Returns the most current permission data from Kinde
```

### Internal Architecture

The `KindeClient` automatically creates and manages the `IKindeAccountsClient` internally:

```csharp
// When you create a KindeClient, it automatically:
// 1. Creates the individual API clients (BillingApi, PermissionsApi, RolesApi, FeatureFlagsApi)
// 2. Wires them together into an IKindeAccountsClient
// 3. Creates the Auth wrapper with all dependencies
// 4. Exposes everything through the simple .Auth property

var kindeClient = new KindeClient(config, httpClient);
// All the complexity is hidden - just use kindeClient.Auth!
```

## Integration with Existing SDK

The auth wrappers are designed to work alongside the existing Kinde .NET SDK functionality. They provide a simplified interface for common authentication and authorization tasks while maintaining compatibility with the existing API.

### Session Management

The wrappers automatically access the current session through the provided KindeClient:

```csharp
// The wrappers automatically get the session from the provided KindeClient
var kindeClient = new KindeClient(config, httpClient);
var auth = kindeClient.Auth; // Automatically configured with session access
// No need to manually pass session - it's handled internally
```

### Token Access

The wrappers access tokens through the KindeClient:

```csharp
// Tokens are automatically retrieved from the current KindeClient
var claims = kindeClient.Auth.Claims();
var userClaim = claims.GetClaim("sub");
```

### Dependency Injection

The `KindeClient` automatically manages all dependencies using the Factory Pattern:

```csharp
// All of this complexity is handled automatically:
// - HttpClient management
// - LoggerFactory creation
// - Individual API client instantiation (BillingApi, PermissionsApi, etc.)
// - IKindeAccountsClient creation and wiring
// - Auth wrapper initialization

var kindeClient = new KindeClient(config, httpClient);
// Everything is ready to use immediately!
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
// Debug messages are logged for API calls and data retrieval
// Warning messages are logged for errors and API failures
// Error messages are logged for critical failures

var kindeClient = new KindeClient(config, httpClient);
// Logging is automatically configured - no setup required!
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

Here's a comprehensive example showing how to use all the wrapper classes:

```csharp
// Initialize the KindeClient
var kindeClient = new KindeClient(identityProviderConfig, httpClient);

// Check user permissions
var canCreateUsers = await kindeClient.Auth.Permissions().HasPermissionAsync("create:users");
var canManageSystem = await kindeClient.Auth.Permissions().HasAnyPermissionAsync(
    new List<string> { "manage:users", "manage:system" });

// Check feature flags
var isNewUIEnabled = await kindeClient.Auth.FeatureFlags().IsFeatureFlagEnabledAsync("new-ui");
var maxItems = await kindeClient.Auth.FeatureFlags().GetFeatureFlagValueAsync("max-items");

// Check user roles
var isAdmin = await kindeClient.Auth.Roles().HasRoleAsync("admin");
var allRoles = await kindeClient.Auth.Roles().GetRolesAsync();

// Check entitlements
var hasPremium = await kindeClient.Auth.Entitlements().HasEntitlementAsync("premium-features");
var allEntitlements = await kindeClient.Auth.Entitlements().GetAllEntitlementsAsync();

// Access token claims
var userId = kindeClient.Auth.Claims().GetClaim("sub");
var userEmail = kindeClient.Auth.Claims().GetClaim("email");
```

## Dependencies

The auth wrappers depend on:
- `Kinde.Api.Client`: Core functionality for authentication and session management
- `Microsoft.Extensions.Logging`: Logging framework
- .NET 9.0+ (match the SDK's target framework)

## Benefits

- **Simplicity**: Clean, intuitive API that hides all complexity from users
- **Reliability**: Direct integration with Accounts API ensures always up-to-date data
- **Performance**: Efficient API calls with proper error handling and logging
- **Maintainability**: Factory pattern ensures easy updates and dependency management
- **Flexibility**: Support for complex access control scenarios
- **Consistency**: Same patterns as Java SDK for cross-platform development
- **Zero Configuration**: Everything works out of the box with `kindeClient.Auth`
- **Type Safety**: Full .NET type safety with proper async/await patterns
