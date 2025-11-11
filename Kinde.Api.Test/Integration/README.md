# Integration Tests

This directory contains integration tests for the Kinde .NET SDK. The tests support two modes:

## Test Modes

### 1. Real API Mode (Local Development)

Tests run against actual Kinde API servers. Requires M2M credentials.

**Configuration:**

Set environment variables or configure `appsettings.json`:

```json
{
  "KindeManagementApi": {
    "UseMockMode": false,
    "Domain": "https://your-business.kinde.com",
    "ClientId": "your_m2m_client_id_here",
    "ClientSecret": "your_m2m_client_secret_here",
    "Audience": "https://your-business.kinde.com/api",
    "Scope": "read:users read:organizations read:applications read:roles read:permissions read:properties"
  }
}
```

Or set environment variables:
- `KINDE_DOMAIN`
- `KINDE_CLIENT_ID`
- `KINDE_CLIENT_SECRET`
- `KINDE_AUDIENCE`
- `KINDE_SCOPE` (optional)

**Running:**

```bash
dotnet test Kinde.Api.Test/Kinde.Api.Test.csproj --filter "FullyQualifiedName~Integration"
```

### 2. Mock Mode (CI/CD)

Tests use mock HTTP responses. No credentials required. Perfect for GitHub Actions and CI/CD pipelines.

**Configuration:**

Set environment variable:
```bash
export USE_MOCK_MODE=true
```

Or in `appsettings.json`:
```json
{
  "KindeManagementApi": {
    "UseMockMode": true
  }
}
```

**Running:**

```bash
USE_MOCK_MODE=true dotnet test Kinde.Api.Test/Kinde.Api.Test.csproj --filter "FullyQualifiedName~Integration"
```

## Test Output

Tests provide detailed output including:
- Response type information
- Key properties from API responses
- Full JSON response (truncated if > 2000 chars)
- Serialization round-trip test results
- Error details with stack traces

## Files

- `BaseIntegrationTest.cs` - Base class supporting both test modes
- `MockHttpHandler.cs` - Mock HTTP handler for CI/CD testing
- `TestOutputHelper.cs` - Enhanced test output formatting
- `ConverterIntegrationTests.cs` - Manual integration tests
- `GeneratedConverterIntegrationTests.cs` - Auto-generated integration tests
- `M2MAuthenticationHelper.cs` - M2M authentication helper

## GitHub Actions Example

```yaml
- name: Run Integration Tests (Mock Mode)
  env:
    USE_MOCK_MODE: "true"
  run: |
    dotnet test Kinde.Api.Test/Kinde.Api.Test.csproj --filter "FullyQualifiedName~Integration"
```

