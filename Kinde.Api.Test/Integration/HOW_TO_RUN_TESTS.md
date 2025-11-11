# How to Run Integration Tests

This guide explains how to run integration tests in both **Mock Mode** (for CI/CD) and **Real API Mode** (for local development).

## Quick Reference

### Mock Mode (CI/CD - No Credentials Required)
```bash
USE_MOCK_MODE=true dotnet test Kinde.Api.Test/Kinde.Api.Test.csproj --filter "FullyQualifiedName~Integration"
```

### Real API Mode (Local Development - Requires Credentials)
```bash
dotnet test Kinde.Api.Test/Kinde.Api.Test.csproj --filter "FullyQualifiedName~Integration"
```

---

## Mock Mode (CI/CD Testing)

Mock mode uses predefined mock HTTP responses. **No Kinde credentials required.** Perfect for:
- GitHub Actions
- CI/CD pipelines
- Quick local testing without API access
- Testing converter logic without network calls

### Option 1: Environment Variable (Recommended for CI/CD)

```bash
# Linux/macOS
export USE_MOCK_MODE=true
dotnet test Kinde.Api.Test/Kinde.Api.Test.csproj --filter "FullyQualifiedName~Integration"

# Windows PowerShell
$env:USE_MOCK_MODE="true"
dotnet test Kinde.Api.Test/Kinde.Api.Test.csproj --filter "FullyQualifiedName~Integration"

# Windows CMD
set USE_MOCK_MODE=true
dotnet test Kinde.Api.Test/Kinde.Api.Test.csproj --filter "FullyQualifiedName~Integration"

# Inline (all platforms)
USE_MOCK_MODE=true dotnet test Kinde.Api.Test/Kinde.Api.Test.csproj --filter "FullyQualifiedName~Integration"
```

### Option 2: Configuration File

Create or update `Kinde.Api.Test/appsettings.json`:

```json
{
  "KindeManagementApi": {
    "UseMockMode": true
  }
}
```

Then run:
```bash
dotnet test Kinde.Api.Test/Kinde.Api.Test.csproj --filter "FullyQualifiedName~Integration"
```

### GitHub Actions Example

```yaml
- name: Run Integration Tests (Mock Mode)
  env:
    USE_MOCK_MODE: "true"
  run: |
    dotnet test Kinde.Api.Test/Kinde.Api.Test.csproj --filter "FullyQualifiedName~Integration"
```

---

## Real API Mode (Local Development)

Real API mode makes actual HTTP requests to Kinde servers. **Requires M2M credentials.**

### Step 1: Configure Credentials

Choose one method:

#### Method A: Environment Variables

```bash
# Linux/macOS
export KINDE_DOMAIN="https://your-business.kinde.com"
export KINDE_CLIENT_ID="your_m2m_client_id"
export KINDE_CLIENT_SECRET="your_m2m_client_secret"
export KINDE_AUDIENCE="https://your-business.kinde.com/api"
export KINDE_SCOPE="read:users read:organizations read:applications read:roles read:permissions read:properties"

# Windows PowerShell
$env:KINDE_DOMAIN="https://your-business.kinde.com"
$env:KINDE_CLIENT_ID="your_m2m_client_id"
$env:KINDE_CLIENT_SECRET="your_m2m_client_secret"
$env:KINDE_AUDIENCE="https://your-business.kinde.com/api"
$env:KINDE_SCOPE="read:users read:organizations read:applications read:roles read:permissions read:properties"
```

#### Method B: Configuration File

Create `Kinde.Api.Test/appsettings.json`:

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

**Note:** Make sure `UseMockMode` is `false` or not set.

### Step 2: Run Tests

```bash
dotnet test Kinde.Api.Test/Kinde.Api.Test.csproj --filter "FullyQualifiedName~Integration"
```

---

## Test Output

Both modes provide enhanced output showing:
- Response type information
- Key properties from API responses
- Full JSON response (truncated if > 2000 chars)
- Serialization round-trip test results
- Error details with stack traces

### Example Output (Mock Mode)

```
═══════════════════════════════════════════════════════════════
Test: GetBusiness
═══════════════════════════════════════════════════════════════
Response Type: GetBusinessResponse

Key Properties:
  code: OK
  message: Success
  business: {...}

Full Response JSON:
{
  "code": "OK",
  "message": "Success",
  "business": {
    "code": "bus_test123",
    "name": "Test Business",
    ...
  }
}
═══════════════════════════════════════════════════════════════
✓ GetBusiness: Success
```

---

## Verifying Mode

The test output will indicate which mode is active:

### Mock Mode
```
✓ Using MOCK mode for integration tests (CI/CD mode)
```

### Real API Mode
```
✓ M2M authentication successful for integration tests (REAL API mode)
```

---

## Troubleshooting

### Tests Skipped / Not Configured

If you see:
```
WARNING: M2M credentials not configured. Integration tests will be skipped.
```

**Solutions:**
1. For mock mode: Set `USE_MOCK_MODE=true` or `UseMockMode: true` in config
2. For real mode: Configure credentials (see "Real API Mode" above)

### Authentication Failed (Real Mode)

If you see:
```
WARNING: Failed to obtain access token. Integration tests will be skipped.
```

**Check:**
- Credentials are correct
- Domain URL is correct (include `https://`)
- M2M application is properly configured in Kinde
- Scopes are correct for your M2M application

### Mock Mode Not Working

If mock mode isn't activating:

1. **Check environment variable:**
   ```bash
   echo $USE_MOCK_MODE  # Should output "true"
   ```

2. **Check config file:**
   ```bash
   cat Kinde.Api.Test/appsettings.json | grep UseMockMode
   ```

3. **Ensure it's not overridden:**
   - Real credentials will take precedence if configured
   - Remove real credentials if you want mock mode

---

## Running Specific Tests

### All Integration Tests
```bash
dotnet test --filter "FullyQualifiedName~Integration"
```

### Only Generated Tests
```bash
dotnet test --filter "FullyQualifiedName~GeneratedConverterIntegrationTests"
```

### Only Manual Tests
```bash
dotnet test --filter "FullyQualifiedName~ConverterIntegrationTests"
```

### Specific Test Method
```bash
dotnet test --filter "FullyQualifiedName~TestGetBusiness_Converter"
```

---

## Summary

| Mode | Command | Credentials Required | Use Case |
|------|---------|---------------------|----------|
| **Mock** | `USE_MOCK_MODE=true dotnet test ...` | ❌ No | CI/CD, quick testing |
| **Real** | `dotnet test ...` | ✅ Yes | Local development, full validation |

