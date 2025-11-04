# Management API Integration Test Coverage Audit

**Date:** 2025-01-31  
**SDK Version:** kinde-dotnet-sdk-review (OpenAPI Generator v6)

## Executive Summary

- **Total Management API Classes:** 25
- **Total Async Methods:** ~154
- **Currently Tested:** 1 class (OrganizationsApi), 2 methods
- **Coverage:** ~1.3% of methods, 4% of classes

## Current Test Coverage

### OrganizationsApi (Partially Tested)
**Location:** `Kinde.Api.Test/Integration/Api/OrganizationsApiIntegrationTests.cs`

**Tested Methods:**
1. ✅ `GetOrganizationUsersAsync` (3 test variations)
   - Basic deserialization test
   - Query parameters test
   - OpenAPI spec example test
2. ✅ `CreateOrganizationAsync` (1 test)
   - Serialization/deserialization test

**Methods in OrganizationsApi (Not Tested):**
- `AddOrganizationLogoAsync`
- `AddOrganizationUserAPIScopeAsync`
- `AddOrganizationUsersAsync`
- `CreateOrganizationUserPermissionAsync`
- `CreateOrganizationUserRoleAsync`
- `DeleteOrganizationAsync`
- `DeleteOrganizationFeatureFlagOverrideAsync`
- `DeleteOrganizationFeatureFlagOverridesAsync`
- `DeleteOrganizationHandleAsync`
- `DeleteOrganizationLogoAsync`
- `DeleteOrganizationUserAPIScopeAsync`
- `DeleteOrganizationUserPermissionAsync`
- `DeleteOrganizationUserRoleAsync`
- `EnableOrgConnectionAsync`
- `GetOrgUserMFAAsync`
- `GetOrganizationAsync`
- `GetOrganizationConnectionsAsync`
- `GetOrganizationFeatureFlagsAsync`
- `GetOrganizationPropertyValuesAsync`
- `GetOrganizationUserPermissionsAsync`
- `GetOrganizationUserRolesAsync`
- `GetOrganizationsAsync`
- `ReadOrganizationLogoAsync`
- `RemoveOrgConnectionAsync`
- `RemoveOrganizationUserAsync`
- `ReplaceOrganizationMFAAsync`
- `ResetOrgUserMFAAsync`
- `ResetOrgUserMFAAllAsync`
- `UpdateOrganizationAsync`
- `UpdateOrganizationFeatureFlagOverrideAsync`
- `UpdateOrganizationPropertiesAsync`
- `UpdateOrganizationPropertyAsync`
- `UpdateOrganizationSessionsAsync`
- `UpdateOrganizationUsersAsync`

**Total: 2 tested / 34 methods = 5.9% coverage for OrganizationsApi**

## Untested API Classes

### 1. APIsApi
- `AddAPIsAsync`
- `AddAPIScopeAsync`
- `AddAPIApplicationScopeAsync`
- `DeleteAPIAsync`
- `DeleteAPIScopeAsync`
- `DeleteAPIAppliationScopeAsync`
- `GetAPIAsync`
- `GetAPIsAsync`
- `GetAPIScopeAsync`
- `GetAPIScopesAsync`
- `UpdateAPIApplicationsAsync`
- `UpdateAPIScopeAsync`

### 2. ApplicationsApi
- `CreateApplicationAsync`
- `DeleteApplicationAsync`
- `EnableConnectionAsync`
- `GetApplicationAsync`
- `GetApplicationConnectionsAsync`
- `GetApplicationPropertyValuesAsync`
- `GetApplicationsAsync`
- `RemoveConnectionAsync`
- `UpdateApplicationAsync`
- `UpdateApplicationsPropertyAsync`
- `UpdateApplicationTokensAsync`

### 3. BillingAgreementsApi
- `CreateBillingAgreementAsync`
- `GetBillingAgreementsAsync`

### 4. BillingEntitlementsApi
- `GetBillingEntitlementsAsync`

### 5. BillingMeterUsageApi
- `CreateMeterUsageRecordAsync`

### 6. BusinessApi
- `GetBusinessAsync`
- `UpdateBusinessAsync`

### 7. CallbacksApi
- `AddLogoutRedirectURLsAsync`
- `AddRedirectCallbackURLsAsync`
- `DeleteCallbackURLsAsync`
- `DeleteLogoutURLsAsync`
- `GetCallbackURLsAsync`
- `GetLogoutURLsAsync`
- `ReplaceLogoutRedirectURLsAsync`
- `ReplaceRedirectCallbackURLsAsync`

### 8. ConnectedAppsApi
- `GetConnectedAppAuthUrlAsync`
- `GetConnectedAppTokenAsync`
- `RevokeConnectedAppTokenAsync`

### 9. ConnectionsApi
- `CreateConnectionAsync`
- `DeleteConnectionAsync`
- `GetConnectionAsync`
- `GetConnectionsAsync`
- `ReplaceConnectionAsync`
- `UpdateConnectionAsync`

### 10. EnvironmentsApi
- `AddLogoAsync`
- `DeleteEnvironementFeatureFlagOverrideAsync`
- `DeleteEnvironementFeatureFlagOverridesAsync`
- `DeleteLogoAsync`
- `GetEnvironementFeatureFlagsAsync`
- `GetEnvironmentAsync`
- `ReadLogoAsync`
- `UpdateEnvironementFeatureFlagOverrideAsync`

### 11. EnvironmentVariablesApi
- `CreateEnvironmentVariableAsync`
- `DeleteEnvironmentVariableAsync`
- `GetEnvironmentVariableAsync`
- `GetEnvironmentVariablesAsync`
- `UpdateEnvironmentVariableAsync`

### 12. FeatureFlagsApi
- `CreateFeatureFlagAsync`
- `DeleteFeatureFlagAsync`
- `UpdateFeatureFlagAsync`

### 13. IdentitiesApi
- `DeleteIdentityAsync`
- `GetIdentityAsync`
- `UpdateIdentityAsync`

### 14. IndustriesApi
- `GetIndustriesAsync`

### 15. MFAApi
- `ReplaceMFAAsync`

### 16. PermissionsApi
- Methods not enumerated in detail - check API file

### 17. PropertiesApi
- Methods not enumerated in detail - check API file

### 18. PropertyCategoriesApi
- Methods not enumerated in detail - check API file

### 19. RolesApi
- Methods not enumerated in detail - check API file

### 20. SearchApi
- Methods not enumerated in detail - check API file

### 21. SubscribersApi
- Methods not enumerated in detail - check API file

### 22. TimezonesApi
- Methods not enumerated in detail - check API file

### 23. UsersApi
- Methods not enumerated in detail - check API file

### 24. WebhooksApi
- Methods not enumerated in detail - check API file

## Recommendations

### High Priority
1. **Complete OrganizationsApi coverage** - Test all 34 methods to establish a pattern
2. **UsersApi** - Critical for user management operations
3. **ApplicationsApi** - Essential for application management
4. **ConnectionsApi** - Important for SSO/authentication connections
5. **FeatureFlagsApi** - Feature flag management

### Medium Priority
6. **EnvironmentsApi** - Environment configuration
7. **PermissionsApi** - Permission management
8. **RolesApi** - Role management
9. **PropertiesApi** - Property management
10. **APIsApi** - API management

### Lower Priority
11. Remaining API classes (Billing, Callbacks, ConnectedApps, etc.)

## Test Structure Recommendation

For each API class, create tests that verify:

1. **Serialization** - Request objects are correctly serialized to JSON
2. **Deserialization** - Response JSON is correctly deserialized to response objects
3. **Path Parameters** - Path parameters are correctly included in URLs
4. **Query Parameters** - Query parameters are correctly included in URLs
5. **Request Bodies** - Complex request bodies serialize correctly
6. **Response Bodies** - Complex response bodies deserialize correctly
7. **Error Handling** - Non-200 status codes are handled correctly
8. **Edge Cases** - Null values, empty arrays, optional parameters

## Example Test Template

```csharp
[Fact]
public async Task MethodName_DeserializesCorrectly()
{
    // Arrange
    var expectedResponse = new { /* ... */ };
    MockServer.SetMockResponse("/api/v1/endpoint", "GET", expectedResponse);
    var api = CreateApi();

    // Act
    var result = await api.MethodNameAsync(...);

    // Assert
    Assert.NotNull(result);
    // Verify deserialized properties
}
```

## Notes

- All tests should use the `OpenApiMockServer` infrastructure
- Tests should validate serialization/deserialization, not business logic
- Focus on testing SDK code generation correctness
- Use snake_case in mock responses to match API format
- Test with real-world data structures when possible

