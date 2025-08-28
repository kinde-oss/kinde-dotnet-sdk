# Kinde .NET SDK

The Kinde SDK for .NET.

You can also use the .NET starter kit [here](https://github.com/kinde-starter-kits/dotnet-starter-kit).

[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](https://makeapullrequest.com) [![Kinde Docs](https://img.shields.io/badge/Kinde-Docs-eee?style=flat-square)](https://kinde.com/docs/developer-tools) [![Kinde Community](https://img.shields.io/badge/Kinde-Community-eee?style=flat-square)](https://thekindecommunity.slack.com)

## Documentation

For details on integrating this SDK into your project, head over to the [Kinde docs](https://kinde.com/docs/) and see the [.NET SDK](https://kinde.com/docs/developer-tools/dotnet-sdk/) doc 👍🏼.

## Auth Wrapper Classes

The SDK includes comprehensive auth wrapper classes that provide simplified access to claims, permissions, feature flags, roles, and entitlements with automatic hard check fallback functionality.

### Basic Usage

```csharp
// Create the main Auth client
var auth = new Auth(kindeClient);

// Access different functionality through wrapper classes
var claims = auth.Claims();
var permissions = auth.Permissions();
var featureFlags = auth.FeatureFlags();
var roles = auth.Roles();
var entitlements = auth.Entitlements();

// Use the simplified API
var hasPermission = await permissions.HasPermissionAsync("read:users");
var hasRole = await roles.HasRoleAsync("admin");
var isEnabled = await featureFlags.IsFeatureFlagEnabledAsync("beta_features");
var hasPremium = await entitlements.HasEntitlementAsync("premium_features");
```

### Key Features

- **Simplified API**: Clean, intuitive interface following Java SDK patterns
- **Hard Check Functionality**: Automatic fallback from token to API calls
- **Comprehensive Error Handling**: Graceful handling of all error scenarios
- **Async Operations**: All API operations are asynchronous
- **Type Safety**: Strongly typed methods with generic support
- **Logging Support**: Built-in logging for debugging and monitoring
- **Backward Compatible**: Works with existing SDK functionality

For more details, see the [Auth Wrappers Documentation](Kinde.Api/Auth/AUTH_WRAPPERS_README.md).

## Publishing

The core team handles publishing.

## Contributing

Please refer to Kinde’s [contributing guidelines](https://github.com/kinde-oss/.github/blob/489e2ca9c3307c2b2e098a885e22f2239116394a/CONTRIBUTING.md).

## License

By contributing to Kinde, you agree that your contributions will be licensed under its MIT License.
