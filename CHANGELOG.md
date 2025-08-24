# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.0.0] - 2024-12-19

### ⚠️ BREAKING CHANGES

- **Target Framework**: Changed from `netstandard2.1` to `net9.0`
  - This is a breaking change that requires consumers to upgrade to .NET 9.0 or later
  - Required due to dependencies on RestSharp 110.2.0 and Microsoft.Extensions.* 9.0.4 which require .NET 6.0+

### 🔧 Technical Changes

- **C# Language Version**: Changed from explicit `10` to `default` to let the SDK select optimal version
- **Package Version**: Bumped from 1.3.1 to 2.0.0 to reflect breaking change

### 🚀 Features

- Enhanced async/sync parity with proper certificate validation callback support
- Improved cookie handling in async operations
- Better error handling and configuration consistency

### 🐛 Bug Fixes

- Fixed async operations not respecting certificate validation callbacks
- Fixed async operations not properly handling cookies
- Corrected file paths in documentation

### 📚 Documentation

- Updated ASYNC_PARITY_FIX_SUMMARY.md with correct file paths
- Added comprehensive changelog for version 2.0.0

## [1.3.1] - Previous Version

### Features
- Initial release with netstandard2.1 support
- Basic authentication and authorization flows
- Feature flag support
- Billing integration

---

## Migration Guide

### Upgrading from 1.3.1 to 2.0.0

1. **Update Target Framework**: Change your project's target framework to .NET 9.0 or later
   ```xml
   <TargetFramework>net9.0</TargetFramework>
   ```

2. **Update Package Reference**: Update the Kinde.SDK package reference
   ```xml
   <PackageReference Include="Kinde.SDK" Version="2.0.0" />
   ```

3. **Verify Dependencies**: Ensure your project supports .NET 9.0
   - RestSharp 110.2.0+ (requires .NET 6.0+)
   - Microsoft.Extensions.* 9.0.4+ (requires .NET 6.0+)

4. **Test Integration**: Verify that your authentication flows and API calls continue to work as expected

### Breaking Changes Summary

- **Minimum .NET Version**: .NET 9.0 required (was .NET Standard 2.1)
- **Package Version**: Major version bump from 1.3.1 to 2.0.0
- **Dependencies**: Updated to newer versions that require .NET 6.0+

### Benefits of Upgrading

- **Better Performance**: .NET 9.0 provides improved performance and security
- **Enhanced Features**: Full async/sync parity with proper certificate validation
- **Future Compatibility**: Access to latest .NET features and security updates
- **Improved Reliability**: Better error handling and configuration consistency
