# Kinde.Api.Api.OrganizationsApi

All URIs are relative to *https://app.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**AddOrganizationUsers**](OrganizationsApi.md#addorganizationusers) | **POST** /api/v1/organizations/{org_code}/users | Add Organization Users |
| [**CreateOrganization**](OrganizationsApi.md#createorganization) | **POST** /api/v1/organization | Create Organization |
| [**CreateOrganizationUserRole**](OrganizationsApi.md#createorganizationuserrole) | **POST** /api/v1/organizations/{org_code}/users/{user_id}/roles | Add Organization User Role |
| [**DeleteOrganizationFeatureFlagOverride**](OrganizationsApi.md#deleteorganizationfeatureflagoverride) | **DELETE** /api/v1/organizations/{org_code}/feature_flags/{feature_flag_key} | Delete Organization Feature Flag Override |
| [**DeleteOrganizationFeatureFlagOverrides**](OrganizationsApi.md#deleteorganizationfeatureflagoverrides) | **DELETE** /api/v1/organizations/{org_code}/feature_flags | Delete Organization Feature Flag Overrides |
| [**DeleteOrganizationUserRole**](OrganizationsApi.md#deleteorganizationuserrole) | **DELETE** /api/v1/organizations/{org_code}/users/{user_id}/roles/{role_id} | Delete Organization User Role |
| [**GetOrganization**](OrganizationsApi.md#getorganization) | **GET** /api/v1/organization | Get Organization |
| [**GetOrganizationFeatureFlags**](OrganizationsApi.md#getorganizationfeatureflags) | **GET** /api/v1/organizations/{org_code}/feature_flags | List Organization Feature Flags |
| [**GetOrganizationUserRoles**](OrganizationsApi.md#getorganizationuserroles) | **GET** /api/v1/organizations/{org_code}/users/{user_id}/roles | List Organization User Roles |
| [**GetOrganizationUsers**](OrganizationsApi.md#getorganizationusers) | **GET** /api/v1/organizations/{org_code}/users | List Organization Users |
| [**GetOrganizations**](OrganizationsApi.md#getorganizations) | **GET** /api/v1/organizations | List Organizations |
| [**RemoveOrganizationUser**](OrganizationsApi.md#removeorganizationuser) | **DELETE** /api/v1/organizations/{org_code}/users/{user_id} | Remove Organization User |
| [**UpdateOrganization**](OrganizationsApi.md#updateorganization) | **PATCH** /api/v1/organization/{org_code} | Update Organization |
| [**UpdateOrganizationFeatureFlagOverride**](OrganizationsApi.md#updateorganizationfeatureflagoverride) | **PATCH** /api/v1/organizations/{org_code}/feature_flags/{feature_flag_key} | Update Organization Feature Flag Override |
| [**UpdateOrganizationUsers**](OrganizationsApi.md#updateorganizationusers) | **PATCH** /api/v1/organizations/{org_code}/users | Update Organization Users |

<a name="addorganizationusers"></a>
# **AddOrganizationUsers**
> AddOrganizationUsersResponse AddOrganizationUsers (string orgCode, AddOrganizationUsersRequest? addOrganizationUsersRequest = null)

Add Organization Users

Add existing users to an organization.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class AddOrganizationUsersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://app.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var addOrganizationUsersRequest = new AddOrganizationUsersRequest?(); // AddOrganizationUsersRequest? |  (optional) 

            try
            {
                // Add Organization Users
                AddOrganizationUsersResponse result = apiInstance.AddOrganizationUsers(orgCode, addOrganizationUsersRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.AddOrganizationUsers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AddOrganizationUsersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add Organization Users
    ApiResponse<AddOrganizationUsersResponse> response = apiInstance.AddOrganizationUsersWithHttpInfo(orgCode, addOrganizationUsersRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.AddOrganizationUsersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **addOrganizationUsersRequest** | [**AddOrganizationUsersRequest?**](AddOrganizationUsersRequest?.md) |  | [optional]  |

### Return type

[**AddOrganizationUsersResponse**](AddOrganizationUsersResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Users successfully added. |  -  |
| **204** | No users added. |  -  |
| **400** | Bad request. |  -  |
| **403** | Invalid credentials. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="createorganization"></a>
# **CreateOrganization**
> CreateOrganizationResponse CreateOrganization (CreateOrganizationRequest? createOrganizationRequest = null)

Create Organization

Create an organization.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class CreateOrganizationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://app.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var createOrganizationRequest = new CreateOrganizationRequest?(); // CreateOrganizationRequest? | Organization details. (optional) 

            try
            {
                // Create Organization
                CreateOrganizationResponse result = apiInstance.CreateOrganization(createOrganizationRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.CreateOrganization: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateOrganizationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Organization
    ApiResponse<CreateOrganizationResponse> response = apiInstance.CreateOrganizationWithHttpInfo(createOrganizationRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.CreateOrganizationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createOrganizationRequest** | [**CreateOrganizationRequest?**](CreateOrganizationRequest?.md) | Organization details. | [optional]  |

### Return type

[**CreateOrganizationResponse**](CreateOrganizationResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Organization successfully created. |  -  |
| **403** | Invalid credentials. |  -  |
| **400** | Error creating user. |  -  |
| **500** | Could not create organization. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="createorganizationuserrole"></a>
# **CreateOrganizationUserRole**
> SuccessResponse CreateOrganizationUserRole (string orgCode, string userId, CreateOrganizationUserRoleRequest createOrganizationUserRoleRequest)

Add Organization User Role

Add role to an organization user.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class CreateOrganizationUserRoleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://app.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var userId = "userId_example";  // string | The user's id.
            var createOrganizationUserRoleRequest = new CreateOrganizationUserRoleRequest(); // CreateOrganizationUserRoleRequest | Role details.

            try
            {
                // Add Organization User Role
                SuccessResponse result = apiInstance.CreateOrganizationUserRole(orgCode, userId, createOrganizationUserRoleRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.CreateOrganizationUserRole: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateOrganizationUserRoleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add Organization User Role
    ApiResponse<SuccessResponse> response = apiInstance.CreateOrganizationUserRoleWithHttpInfo(orgCode, userId, createOrganizationUserRoleRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.CreateOrganizationUserRoleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **userId** | **string** | The user&#39;s id. |  |
| **createOrganizationUserRoleRequest** | [**CreateOrganizationUserRoleRequest**](CreateOrganizationUserRoleRequest.md) | Role details. |  |

### Return type

[**SuccessResponse**](SuccessResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | User successfully removed from |  -  |
| **403** | Invalid credentials. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="deleteorganizationfeatureflagoverride"></a>
# **DeleteOrganizationFeatureFlagOverride**
> SuccessResponse DeleteOrganizationFeatureFlagOverride (string orgCode, string featureFlagKey)

Delete Organization Feature Flag Override

Delete organization feature flag override.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class DeleteOrganizationFeatureFlagOverrideExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://app.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = "orgCode_example";  // string | The identifier for the organization.
            var featureFlagKey = "featureFlagKey_example";  // string | The identifier for the feature flag.

            try
            {
                // Delete Organization Feature Flag Override
                SuccessResponse result = apiInstance.DeleteOrganizationFeatureFlagOverride(orgCode, featureFlagKey);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationFeatureFlagOverride: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteOrganizationFeatureFlagOverrideWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Organization Feature Flag Override
    ApiResponse<SuccessResponse> response = apiInstance.DeleteOrganizationFeatureFlagOverrideWithHttpInfo(orgCode, featureFlagKey);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationFeatureFlagOverrideWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization. |  |
| **featureFlagKey** | **string** | The identifier for the feature flag. |  |

### Return type

[**SuccessResponse**](SuccessResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Feature flag override successfully deleted. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="deleteorganizationfeatureflagoverrides"></a>
# **DeleteOrganizationFeatureFlagOverrides**
> SuccessResponse DeleteOrganizationFeatureFlagOverrides (string orgCode)

Delete Organization Feature Flag Overrides

Delete all organization feature flag overrides.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class DeleteOrganizationFeatureFlagOverridesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://app.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = "orgCode_example";  // string | The identifier for the organization.

            try
            {
                // Delete Organization Feature Flag Overrides
                SuccessResponse result = apiInstance.DeleteOrganizationFeatureFlagOverrides(orgCode);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationFeatureFlagOverrides: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteOrganizationFeatureFlagOverridesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Organization Feature Flag Overrides
    ApiResponse<SuccessResponse> response = apiInstance.DeleteOrganizationFeatureFlagOverridesWithHttpInfo(orgCode);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationFeatureFlagOverridesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization. |  |

### Return type

[**SuccessResponse**](SuccessResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Feature flag overrides successfully deleted. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="deleteorganizationuserrole"></a>
# **DeleteOrganizationUserRole**
> SuccessResponse DeleteOrganizationUserRole (string orgCode, string userId, string roleId)

Delete Organization User Role

Delete role for an organization user.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class DeleteOrganizationUserRoleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://app.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var userId = "userId_example";  // string | The user's id.
            var roleId = "roleId_example";  // string | The role id.

            try
            {
                // Delete Organization User Role
                SuccessResponse result = apiInstance.DeleteOrganizationUserRole(orgCode, userId, roleId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationUserRole: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteOrganizationUserRoleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Organization User Role
    ApiResponse<SuccessResponse> response = apiInstance.DeleteOrganizationUserRoleWithHttpInfo(orgCode, userId, roleId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationUserRoleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **userId** | **string** | The user&#39;s id. |  |
| **roleId** | **string** | The role id. |  |

### Return type

[**SuccessResponse**](SuccessResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | User successfully removed. |  -  |
| **400** | Error creating user. |  -  |
| **403** | Invalid credentials. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getorganization"></a>
# **GetOrganization**
> Organization GetOrganization (string? code = null)

Get Organization

Gets an organization given the organization's code. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetOrganizationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://app.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var code = "code_example";  // string? | The organization's code. (optional) 

            try
            {
                // Get Organization
                Organization result = apiInstance.GetOrganization(code);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrganization: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrganizationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Organization
    ApiResponse<Organization> response = apiInstance.GetOrganizationWithHttpInfo(code);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrganizationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **code** | **string?** | The organization&#39;s code. | [optional]  |

### Return type

[**Organization**](Organization.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Organization successfully retrieved. |  -  |
| **400** | Bad request. |  -  |
| **403** | Invalid credentials. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getorganizationfeatureflags"></a>
# **GetOrganizationFeatureFlags**
> GetOrganizationFeatureFlagsResponse GetOrganizationFeatureFlags (string orgCode)

List Organization Feature Flags

Get all organization feature flags.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetOrganizationFeatureFlagsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://app.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = "orgCode_example";  // string | The identifier for the organization.

            try
            {
                // List Organization Feature Flags
                GetOrganizationFeatureFlagsResponse result = apiInstance.GetOrganizationFeatureFlags(orgCode);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrganizationFeatureFlags: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrganizationFeatureFlagsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Organization Feature Flags
    ApiResponse<GetOrganizationFeatureFlagsResponse> response = apiInstance.GetOrganizationFeatureFlagsWithHttpInfo(orgCode);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrganizationFeatureFlagsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization. |  |

### Return type

[**GetOrganizationFeatureFlagsResponse**](GetOrganizationFeatureFlagsResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Feature flag overrides successfully returned. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getorganizationuserroles"></a>
# **GetOrganizationUserRoles**
> GetOrganizationsUserRolesResponse GetOrganizationUserRoles (string orgCode, string userId)

List Organization User Roles

Get roles for an organization user.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetOrganizationUserRolesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://app.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var userId = "userId_example";  // string | The user's id.

            try
            {
                // List Organization User Roles
                GetOrganizationsUserRolesResponse result = apiInstance.GetOrganizationUserRoles(orgCode, userId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrganizationUserRoles: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrganizationUserRolesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Organization User Roles
    ApiResponse<GetOrganizationsUserRolesResponse> response = apiInstance.GetOrganizationUserRolesWithHttpInfo(orgCode, userId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrganizationUserRolesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **userId** | **string** | The user&#39;s id. |  |

### Return type

[**GetOrganizationsUserRolesResponse**](GetOrganizationsUserRolesResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A successful response with a list of user roles. |  -  |
| **403** | Invalid credentials. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getorganizationusers"></a>
# **GetOrganizationUsers**
> GetOrganizationUsersResponse GetOrganizationUsers (string orgCode, string? sort = null, int? pageSize = null, string? nextToken = null, string? permissions = null)

List Organization Users

Get users in an organization.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetOrganizationUsersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://app.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var sort = "name_asc";  // string? | Field and order to sort the result by. (optional) 
            var pageSize = 56;  // int? | Number of results per page. Defaults to 10 if parameter not sent. (optional) 
            var nextToken = "nextToken_example";  // string? | A string to get the next page of results if there are more results. (optional) 
            var permissions = "permissions_example";  // string? | Filter by user permissions (optional) 

            try
            {
                // List Organization Users
                GetOrganizationUsersResponse result = apiInstance.GetOrganizationUsers(orgCode, sort, pageSize, nextToken, permissions);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrganizationUsers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrganizationUsersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Organization Users
    ApiResponse<GetOrganizationUsersResponse> response = apiInstance.GetOrganizationUsersWithHttpInfo(orgCode, sort, pageSize, nextToken, permissions);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrganizationUsersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **sort** | **string?** | Field and order to sort the result by. | [optional]  |
| **pageSize** | **int?** | Number of results per page. Defaults to 10 if parameter not sent. | [optional]  |
| **nextToken** | **string?** | A string to get the next page of results if there are more results. | [optional]  |
| **permissions** | **string?** | Filter by user permissions | [optional]  |

### Return type

[**GetOrganizationUsersResponse**](GetOrganizationUsersResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A successful response with a list of organization users or an empty list. |  -  |
| **400** | Error creating user |  -  |
| **403** | Invalid credentials. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getorganizations"></a>
# **GetOrganizations**
> GetOrganizationsResponse GetOrganizations (string? sort = null, int? pageSize = null, string? nextToken = null)

List Organizations

Get a list of organizations. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetOrganizationsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://app.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var sort = "name_asc";  // string? | Field and order to sort the result by. (optional) 
            var pageSize = 56;  // int? | Number of results per page. Defaults to 10 if parameter not sent. (optional) 
            var nextToken = "nextToken_example";  // string? | A string to get the next page of results if there are more results. (optional) 

            try
            {
                // List Organizations
                GetOrganizationsResponse result = apiInstance.GetOrganizations(sort, pageSize, nextToken);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrganizations: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrganizationsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Organizations
    ApiResponse<GetOrganizationsResponse> response = apiInstance.GetOrganizationsWithHttpInfo(sort, pageSize, nextToken);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrganizationsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **sort** | **string?** | Field and order to sort the result by. | [optional]  |
| **pageSize** | **int?** | Number of results per page. Defaults to 10 if parameter not sent. | [optional]  |
| **nextToken** | **string?** | A string to get the next page of results if there are more results. | [optional]  |

### Return type

[**GetOrganizationsResponse**](GetOrganizationsResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A successful response with a list of organizations or an empty list. |  -  |
| **403** | Invalid credentials. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="removeorganizationuser"></a>
# **RemoveOrganizationUser**
> SuccessResponse RemoveOrganizationUser (string orgCode, string userId)

Remove Organization User

Remove user from an organization.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class RemoveOrganizationUserExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://app.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var userId = "userId_example";  // string | The user's id.

            try
            {
                // Remove Organization User
                SuccessResponse result = apiInstance.RemoveOrganizationUser(orgCode, userId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.RemoveOrganizationUser: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RemoveOrganizationUserWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Remove Organization User
    ApiResponse<SuccessResponse> response = apiInstance.RemoveOrganizationUserWithHttpInfo(orgCode, userId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.RemoveOrganizationUserWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **userId** | **string** | The user&#39;s id. |  |

### Return type

[**SuccessResponse**](SuccessResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | User successfully removed from organization |  -  |
| **400** | Error removing user |  -  |
| **403** | Invalid credentials. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updateorganization"></a>
# **UpdateOrganization**
> void UpdateOrganization (string orgCode, UpdateOrganizationRequest? updateOrganizationRequest = null)

Update Organization

Update an organization.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class UpdateOrganizationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://app.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = "orgCode_example";  // string | The identifier for the organization.
            var updateOrganizationRequest = new UpdateOrganizationRequest?(); // UpdateOrganizationRequest? | Organization details. (optional) 

            try
            {
                // Update Organization
                apiInstance.UpdateOrganization(orgCode, updateOrganizationRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.UpdateOrganization: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateOrganizationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Organization
    apiInstance.UpdateOrganizationWithHttpInfo(orgCode, updateOrganizationRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization. |  |
| **updateOrganizationRequest** | [**UpdateOrganizationRequest?**](UpdateOrganizationRequest?.md) | Organization details. | [optional]  |

### Return type

void (empty response body)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Organization successfully updated. |  -  |
| **403** | Invalid credentials. |  -  |
| **400** | Error updating organization. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updateorganizationfeatureflagoverride"></a>
# **UpdateOrganizationFeatureFlagOverride**
> SuccessResponse UpdateOrganizationFeatureFlagOverride (string orgCode, string featureFlagKey, string value)

Update Organization Feature Flag Override

Update organization feature flag override.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class UpdateOrganizationFeatureFlagOverrideExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://app.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = "orgCode_example";  // string | The identifier for the organization
            var featureFlagKey = "featureFlagKey_example";  // string | The identifier for the feature flag
            var value = "value_example";  // string | Override value

            try
            {
                // Update Organization Feature Flag Override
                SuccessResponse result = apiInstance.UpdateOrganizationFeatureFlagOverride(orgCode, featureFlagKey, value);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationFeatureFlagOverride: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateOrganizationFeatureFlagOverrideWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Organization Feature Flag Override
    ApiResponse<SuccessResponse> response = apiInstance.UpdateOrganizationFeatureFlagOverrideWithHttpInfo(orgCode, featureFlagKey, value);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationFeatureFlagOverrideWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization |  |
| **featureFlagKey** | **string** | The identifier for the feature flag |  |
| **value** | **string** | Override value |  |

### Return type

[**SuccessResponse**](SuccessResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Feature flag override successfully updated. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updateorganizationusers"></a>
# **UpdateOrganizationUsers**
> UpdateOrganizationUsersResponse UpdateOrganizationUsers (string orgCode, UpdateOrganizationUsersRequest? updateOrganizationUsersRequest = null)

Update Organization Users

Update users that belong to an organization.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class UpdateOrganizationUsersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://app.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var updateOrganizationUsersRequest = new UpdateOrganizationUsersRequest?(); // UpdateOrganizationUsersRequest? |  (optional) 

            try
            {
                // Update Organization Users
                UpdateOrganizationUsersResponse result = apiInstance.UpdateOrganizationUsers(orgCode, updateOrganizationUsersRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationUsers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateOrganizationUsersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Organization Users
    ApiResponse<UpdateOrganizationUsersResponse> response = apiInstance.UpdateOrganizationUsersWithHttpInfo(orgCode, updateOrganizationUsersRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationUsersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **updateOrganizationUsersRequest** | [**UpdateOrganizationUsersRequest?**](UpdateOrganizationUsersRequest?.md) |  | [optional]  |

### Return type

[**UpdateOrganizationUsersResponse**](UpdateOrganizationUsersResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Users successfully removed. |  -  |
| **400** | Error creating user. |  -  |
| **403** | Invalid credentials. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

