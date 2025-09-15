# Changelog

All notable changes to the Kinde .NET SDK will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.0.1] - 2025-09-11

### 🎯 Primary Feature: .NET Standard Support

#### Added
- **.NET Standard 2.0 Support**: Full compatibility with .NET Standard 2.0
- **.NET Standard 2.1 Support**: Full compatibility with .NET Standard 2.1
- **Cross-Platform Compatibility**: Enhanced support for older .NET Framework and .NET Core versions
- **Backward Compatibility**: Maintains compatibility with existing .NET 8.0 applications
- **Legacy Framework Support**: Now works with .NET Framework 4.6.1+ and .NET Core 2.0+

#### Supported Frameworks
- .NET 8.0
- .NET Standard 2.1
- .NET Standard 2.0
- .NET Framework 4.6.1+
- .NET Core 2.0+

#### Changed
- **Multi-Target Framework**: SDK now supports multiple target frameworks simultaneously
- **Code Generation Improvements**: Enhanced generated code with better documentation and XML comments
- **JSON Serialization**: Improved JSON converter implementations with better parameter documentation
- **Model Classes**: Enhanced model classes with additional XML documentation and examples

#### Technical Details
- Updated `Configuration.cs` version constants from 1.3.1 to 2.0.1
- Updated `Kinde.Api.csproj` version from 2.0.0 to 2.0.1
- Enhanced generated API models with improved XML documentation
- Added better parameter documentation to JSON converters
- Improved code generation templates for better developer experience
- **Multi-targeting**: SDK now targets .NET 8.0, .NET Standard 2.1, and .NET Standard 2.0

### Files Modified
- `Kinde.Api/Client/Configuration.cs` - Version constants updated
- `Kinde.Api/Kinde.Api.csproj` - Package version updated
- All generated model classes - Enhanced documentation and examples
- All JSON converter classes - Improved parameter documentation

---

## [2.0.0] - 2025-09-09

### 🚀 Major Update

#### Added
- **.NET 8.0 Support**: Full support for .NET 8.0 with modern language features
- **OpenAPI Generator v7.15.0**: Updated to latest OpenAPI Generator for improved code generation
- **Enhanced Performance**: Improved performance with modern .NET 8.0 optimizations
- **Modern .NET Features**: Support for latest C# language features and .NET APIs

#### Changed
- **Breaking Change**: Requires .NET 8.0 or later (minimum supported framework)
- **Package Dependencies**: Updated all NuGet package references to latest compatible versions
- **Code Generation**: Improved generated code quality and consistency
- **SDK Architecture**: Enhanced SDK architecture for better maintainability

#### Technical Improvements
- Updated target framework from .NET 6.0 to .NET 8.0
- Enhanced authentication modules and authorization flow
- Improved client utilities and error handling
- Better feature flag functionality
- Enhanced JWT claims handling
- Improved base authentication functionality

#### Dependencies Updated
- Microsoft.Extensions.* packages updated to v9.0.4
- System.IdentityModel.Tokens.Jwt updated to v8.8.0
- Polly updated to v8.5.2
- Newtonsoft.Json updated to v13.0.3
- JsonSubTypes updated to v2.0.1

---

## [1.3.1] - Previous Version

### Features
- .NET 6.0 support
- Basic authentication and authorization
- Feature flags support
- User management
- Organization management

---

## Migration Guide

### From 1.3.1 to 2.0.0
- **Breaking Change**: Update your project to target .NET 8.0 or later
- Update package references to use the new version
- Review any custom authentication implementations for compatibility

### From 2.0.0 to 2.0.1
- **No Breaking Changes**: This is a patch release with enhanced compatibility
- **New Framework Support**: Now supports .NET Standard 2.0 and 2.1 in addition to .NET 8.0
- Simply update your package reference to 2.0.1
- No code changes required
- **Legacy Framework Support**: If you're using older .NET Framework or .NET Core versions, you can now use this SDK

---

## Support

For questions and support:
- [Kinde Documentation](https://kinde.com/docs/)
- [GitHub Issues](https://github.com/kinde-oss/kinde-dotnet-sdk/issues)
- [Kinde Community](https://thekindecommunity.slack.com)
