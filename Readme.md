# Kinde .NET SDK

The Kinde SDK for .NET.

You can also use the .NET starter kit [here](https://github.com/kinde-starter-kits/dotnet-starter-kit).

[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](https://makeapullrequest.com) [![Kinde Docs](https://img.shields.io/badge/Kinde-Docs-eee?style=flat-square)](https://kinde.com/docs/developer-tools) [![Kinde Community](https://img.shields.io/badge/Kinde-Community-eee?style=flat-square)](https://thekindecommunity.slack.com)

## Documentation

For details on integrating this SDK into your project, head over to the [Kinde docs](https://kinde.com/docs/) and see the [.NET SDK](https://kinde.com/docs/developer-tools/dotnet-sdk/) doc 👍🏼.

## Entitlements and Hard Check

The SDK includes entitlements and hard check functionality that provides automatic fallback from token-based checks to API calls when permissions, roles, or feature flags are not available in the JWT token.

### Basic Usage

```csharp
// Token claims (direct on token)
var permissions = token.GetPermissions();
var hasPermission = token.HasPermission("read:users");

// Hard check with API fallback
var checker = new KindeTokenChecker(token, accountsClient);
var hasPermission = await checker.HasPermissionAsync("read:users");

// Entitlements
var accountsClient = new KindeAccountsClient(kindeClient);
var entitlements = await accountsClient.GetEntitlementsAsync();
```

### Key Features

- **Token-First Strategy**: Fast in-memory checks when token data is available
- **Automatic API Fallback**: Falls back to API calls when token data is insufficient
- **Comprehensive Error Handling**: Graceful handling of all error scenarios
- **Async Operations**: All API operations are asynchronous
- **Backward Compatible**: Works with existing code

For more details, see the [Entitlements Documentation](ENTITLEMENTS_README.md).

## Publishing

The core team handles publishing.

## Contributing

Please refer to Kinde’s [contributing guidelines](https://github.com/kinde-oss/.github/blob/489e2ca9c3307c2b2e098a885e22f2239116394a/CONTRIBUTING.md).

## License

By contributing to Kinde, you agree that your contributions will be licensed under its MIT License.
