# OpenAPI Integration Testing

This directory contains integration tests for the Kinde Management API SDK. The tests use an OpenAPI-based mock server to validate serialization and deserialization of API requests and responses.

## Architecture

### OpenApiMockServer
The `OpenApiMockServer` class reads the OpenAPI specification (`openapi.yaml`) and dynamically creates an in-memory HTTP server that:
- Responds to all Management API endpoints defined in the OpenAPI spec
- Generates mock responses based on schema examples
- Allows custom mock responses to be set for specific test scenarios
- Validates that serialization/deserialization works correctly

### IntegrationTestBase
Base class that provides:
- Shared mock server instance (via `MockServerFixture`)
- Configuration for API clients pointing to the mock server
- Common utilities for test setup

## Usage

### Running Integration Tests

```bash
dotnet test Kinde.Api.Test/Kinde.Api.Test.csproj --filter "Category=Integration"
```

Or run specific test classes:
```bash
dotnet test Kinde.Api.Test/Kinde.Api.Test.csproj --filter "FullyQualifiedName~OrganizationsApiIntegrationTests"
```

### Writing New Integration Tests

1. Create a test class that inherits from `IntegrationTestBase`:

```csharp
public class MyApiIntegrationTests : IntegrationTestBase
{
    public MyApiIntegrationTests(MockServerFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task MyApiMethod_DeserializesCorrectly()
    {
        // Arrange - Set up mock response
        var expectedResponse = new { /* your response structure */ };
        MockServer.SetMockResponse(
            "/api/v1/your-endpoint",
            "GET",
            expectedResponse
        );

        // Act - Call the API
        var configuration = CreateApiConfiguration();
        var api = new YourApi(HttpClient, configuration);
        var result = await api.YourMethodAsync();

        // Assert - Verify deserialization worked
        Assert.NotNull(result);
        // Add more assertions...
    }
}
```

### Setting Custom Mock Responses

The mock server automatically generates responses from the OpenAPI spec examples. For custom responses:

```csharp
MockServer.SetMockResponse(
    "/api/v1/organizations/{org_code}/users",  // Path template from OpenAPI spec
    "GET",                                      // HTTP method
    new {                                       // Response object (will be serialized)
        code = "OK",
        message = "Success",
        organization_users = new[] { /* ... */ }
    },
    HttpStatusCode.OK                           // Optional status code
);
```

## Long-Term Maintenance

### Updating When OpenAPI Spec Changes

1. Update `api/openapi.yaml` in the repository root
2. Run tests - they should automatically pick up new endpoints
3. If new endpoints need testing, add test methods following the pattern above

### Adding Support for New Response Types

The mock server automatically handles:
- Schema examples from OpenAPI spec
- Schema references (`$ref`)
- Basic schema types (object, array, string, integer, etc.)

For complex cases, you can always set custom mock responses using `SetMockResponse()`.

## Benefits

1. **Tests Real Serialization**: Uses the actual SDK code to serialize/deserialize
2. **Validates Against Spec**: Responses match the OpenAPI specification
3. **No External Dependencies**: All tests run in-memory
4. **Fast**: No network calls, everything is local
5. **Maintainable**: Automatically picks up changes to the OpenAPI spec

## Troubleshooting

### Tests Failing with Deserialization Errors

If you see deserialization errors, check:
1. The mock response structure matches the expected C# model
2. Property names match (snake_case vs camelCase)
3. Nullable fields are handled correctly
4. Custom converters are registered if needed

### Mock Server Not Finding OpenAPI Spec

The server looks for `api/openapi.yaml` in:
1. Test output directory (`api/openapi.yaml`)
2. Relative path: `../../../../api/openapi.yaml`

Ensure the spec file is copied to the test output directory (configured in `.csproj`).

