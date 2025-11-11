# Impact of Auto-Generation on Integration Tests

## Overview

The integration test infrastructure supports both **manually written tests** and **auto-generated tests**. This document explains how the auto-generation process affects the test infrastructure.

## Test Files

### Manually Maintained (NOT affected by regeneration)

These files are **never overwritten** by the generation process:

- ✅ `BaseIntegrationTest.cs` - Base class with mock mode support
- ✅ `MockHttpHandler.cs` - Mock HTTP handler for CI/CD
- ✅ `TestOutputHelper.cs` - Enhanced output formatting
- ✅ `ConverterIntegrationTests.cs` - Manually written integration tests
- ✅ `M2MAuthenticationHelper.cs` - M2M authentication helper
- ✅ `IntegrationTestFixture.cs` - Test fixture (part of BaseIntegrationTest.cs)

### Auto-Generated (OVERWRITTEN on regeneration)

This file is **completely regenerated** each time you run the test generator:

- ⚠️ `GeneratedConverterIntegrationTests.cs` - Auto-generated from OpenAPI spec

## What Happens When Tests Are Regenerated?

### ✅ Safe - Won't Break

1. **Base Class Compatibility**: The generated tests inherit from `BaseIntegrationTest`, which we've enhanced with:
   - Mock mode support (`UseMockMode`, `MockHttpClient`)
   - Enhanced configuration handling
   - Both real and mock mode support

2. **Template Updates**: The test generator template (`tools/test-generator/templates/integration_test.cs.j2`) has been updated to:
   - Use mock HTTP client when in mock mode
   - Use `TestOutputHelper` for enhanced output
   - Follow the same patterns as manually written tests

### ⚠️ What Gets Regenerated

When you run `generate-all-apis.sh` or manually run the test generator:

1. **`GeneratedConverterIntegrationTests.cs`** is completely rewritten
2. All test methods are regenerated based on the current OpenAPI spec
3. New endpoints get new test methods automatically
4. Removed endpoints have their test methods removed

### ✅ What Stays the Same

1. **Manually written tests** in `ConverterIntegrationTests.cs` are never touched
2. **Test infrastructure** (BaseIntegrationTest, MockHttpHandler, etc.) remains unchanged
3. **Configuration** (appsettings.json) is not modified

## Regeneration Process

### Current State (After Updates)

The test generator template now generates tests that:

```csharp
// ✅ Uses mock mode when available
var api = UseMockMode && MockHttpClient != null
    ? new BusinessApi(MockHttpClient, ApiConfiguration)
    : new BusinessApi(ApiConfiguration);

// ✅ Uses enhanced output
TestOutputHelper.WriteResponseDetails(_output, "GetBusiness", result);

// ✅ Uses enhanced serialization test output
TestSerializationRoundTrip(result, "GetBusiness");
```

### Before Template Updates

The old template generated:

```csharp
// ❌ Didn't use mock mode
var api = new BusinessApi(ApiConfiguration);

// ❌ Basic output only
_output.WriteLine($"✓ {testName}: Converter test passed");
```

## Best Practices

### 1. Don't Edit Generated Tests

**Never manually edit** `GeneratedConverterIntegrationTests.cs`. Your changes will be lost on regeneration.

### 2. Add Custom Tests to Manual File

If you need custom test logic, add it to `ConverterIntegrationTests.cs` instead.

### 3. Update Template for Changes

If you want to change how **all** generated tests work, update:
- `tools/test-generator/templates/integration_test.cs.j2`

### 4. Regenerate After API Changes

After updating the OpenAPI spec or regenerating API clients:
```bash
./generate-all-apis.sh
```

This will:
- Regenerate converters
- Regenerate API clients
- **Regenerate integration tests** (with latest template)

## Verification

After regeneration, verify:

1. ✅ Tests compile: `dotnet build Kinde.Api.Test/Kinde.Api.Test.csproj`
2. ✅ Mock mode works: `USE_MOCK_MODE=true dotnet test --filter "FullyQualifiedName~Generated"`
3. ✅ Real mode works: `dotnet test --filter "FullyQualifiedName~Generated"`
4. ✅ Enhanced output appears in test results

## Summary

| Component | Regenerated? | Impact |
|-----------|--------------|--------|
| `BaseIntegrationTest.cs` | ❌ No | Safe - manually maintained |
| `MockHttpHandler.cs` | ❌ No | Safe - manually maintained |
| `TestOutputHelper.cs` | ❌ No | Safe - manually maintained |
| `ConverterIntegrationTests.cs` | ❌ No | Safe - manually maintained |
| `GeneratedConverterIntegrationTests.cs` | ✅ Yes | Regenerated with latest template |
| Test Generator Template | ❌ No | Update manually when needed |

**Bottom Line**: The auto-generation process is now **fully compatible** with the enhanced test infrastructure. Regenerating tests will use mock mode and enhanced output automatically.

