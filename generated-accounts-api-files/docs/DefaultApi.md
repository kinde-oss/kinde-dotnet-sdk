# Kinde.Accounts.Api.DefaultApi

All URIs are relative to *https://your-domain.kinde.com/account_api/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**GetCurrentOrganization**](DefaultApi.md#getcurrentorganization) | **GET** /current_organization | Get current organization |
| [**GetEntitlement**](DefaultApi.md#getentitlement) | **GET** /entitlement/{key} | Get specific entitlement |
| [**GetEntitlements**](DefaultApi.md#getentitlements) | **GET** /entitlements | Get all entitlements |
| [**GetFeatureFlag**](DefaultApi.md#getfeatureflag) | **GET** /feature_flags/{key} | Get specific feature flag |
| [**GetFeatureFlags**](DefaultApi.md#getfeatureflags) | **GET** /feature_flags | Get all feature flags |
| [**GetPermission**](DefaultApi.md#getpermission) | **GET** /permission/{key} | Get specific permission |
| [**GetPermissions**](DefaultApi.md#getpermissions) | **GET** /permissions | Get all permissions |
| [**GetRoles**](DefaultApi.md#getroles) | **GET** /roles | Get all roles |
| [**GetUserOrganizations**](DefaultApi.md#getuserorganizations) | **GET** /user_organizations | Get user organizations |
| [**GetUserProfile**](DefaultApi.md#getuserprofile) | **GET** /user_profile | Get user profile |

<a id="getcurrentorganization"></a>
# **GetCurrentOrganization**
> CurrentOrganizationResponse GetCurrentOrganization ()

Get current organization

Retrieve the current organization information

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Accounts.Api;
using Kinde.Accounts.Client;
using Kinde.Accounts.Model;

namespace Example
{
    public class GetCurrentOrganizationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your-domain.kinde.com/account_api/v1";
            // Configure Bearer token for authorization: BearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DefaultApi(config);

            try
            {
                // Get current organization
                CurrentOrganizationResponse result = apiInstance.GetCurrentOrganization();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DefaultApi.GetCurrentOrganization: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetCurrentOrganizationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get current organization
    ApiResponse<CurrentOrganizationResponse> response = apiInstance.GetCurrentOrganizationWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DefaultApi.GetCurrentOrganizationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**CurrentOrganizationResponse**](CurrentOrganizationResponse.md)

### Authorization

[BearerAuth](../README.md#BearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successful response |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **500** | Internal server error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getentitlement"></a>
# **GetEntitlement**
> EntitlementResponse GetEntitlement (string key)

Get specific entitlement

Retrieve a specific entitlement by key

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Accounts.Api;
using Kinde.Accounts.Client;
using Kinde.Accounts.Model;

namespace Example
{
    public class GetEntitlementExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your-domain.kinde.com/account_api/v1";
            // Configure Bearer token for authorization: BearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DefaultApi(config);
            var key = "key_example";  // string | The entitlement key

            try
            {
                // Get specific entitlement
                EntitlementResponse result = apiInstance.GetEntitlement(key);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DefaultApi.GetEntitlement: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetEntitlementWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get specific entitlement
    ApiResponse<EntitlementResponse> response = apiInstance.GetEntitlementWithHttpInfo(key);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DefaultApi.GetEntitlementWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **key** | **string** | The entitlement key |  |

### Return type

[**EntitlementResponse**](EntitlementResponse.md)

### Authorization

[BearerAuth](../README.md#BearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successful response |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Entitlement not found |  -  |
| **500** | Internal server error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getentitlements"></a>
# **GetEntitlements**
> EntitlementsResponse GetEntitlements (string? startingAfter = null, int? limit = null)

Get all entitlements

Retrieve all entitlements for the current user's organization

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Accounts.Api;
using Kinde.Accounts.Client;
using Kinde.Accounts.Model;

namespace Example
{
    public class GetEntitlementsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your-domain.kinde.com/account_api/v1";
            // Configure Bearer token for authorization: BearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DefaultApi(config);
            var startingAfter = "startingAfter_example";  // string? | Cursor for fetching the next page (use the value returned by metadata.next_page_starting_after) (optional) 
            var limit = 100;  // int? | Maximum number of items to return (1-1000, server may cap this) (optional)  (default to 100)

            try
            {
                // Get all entitlements
                EntitlementsResponse result = apiInstance.GetEntitlements(startingAfter, limit);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DefaultApi.GetEntitlements: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetEntitlementsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get all entitlements
    ApiResponse<EntitlementsResponse> response = apiInstance.GetEntitlementsWithHttpInfo(startingAfter, limit);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DefaultApi.GetEntitlementsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **startingAfter** | **string?** | Cursor for fetching the next page (use the value returned by metadata.next_page_starting_after) | [optional]  |
| **limit** | **int?** | Maximum number of items to return (1-1000, server may cap this) | [optional] [default to 100] |

### Return type

[**EntitlementsResponse**](EntitlementsResponse.md)

### Authorization

[BearerAuth](../README.md#BearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successful response |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **500** | Internal server error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getfeatureflag"></a>
# **GetFeatureFlag**
> FeatureFlagResponse GetFeatureFlag (string key)

Get specific feature flag

Retrieve a specific feature flag by key

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Accounts.Api;
using Kinde.Accounts.Client;
using Kinde.Accounts.Model;

namespace Example
{
    public class GetFeatureFlagExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your-domain.kinde.com/account_api/v1";
            // Configure Bearer token for authorization: BearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DefaultApi(config);
            var key = "key_example";  // string | The feature flag key

            try
            {
                // Get specific feature flag
                FeatureFlagResponse result = apiInstance.GetFeatureFlag(key);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DefaultApi.GetFeatureFlag: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetFeatureFlagWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get specific feature flag
    ApiResponse<FeatureFlagResponse> response = apiInstance.GetFeatureFlagWithHttpInfo(key);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DefaultApi.GetFeatureFlagWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **key** | **string** | The feature flag key |  |

### Return type

[**FeatureFlagResponse**](FeatureFlagResponse.md)

### Authorization

[BearerAuth](../README.md#BearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successful response |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Feature flag not found |  -  |
| **500** | Internal server error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getfeatureflags"></a>
# **GetFeatureFlags**
> FeatureFlagsResponse GetFeatureFlags (string? startingAfter = null, int? limit = null)

Get all feature flags

Retrieve all feature flags for the current user

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Accounts.Api;
using Kinde.Accounts.Client;
using Kinde.Accounts.Model;

namespace Example
{
    public class GetFeatureFlagsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your-domain.kinde.com/account_api/v1";
            // Configure Bearer token for authorization: BearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DefaultApi(config);
            var startingAfter = "startingAfter_example";  // string? | Cursor for fetching the next page (use the value returned by metadata.next_page_starting_after) (optional) 
            var limit = 56;  // int? | Maximum number of items to return (server may cap this) (optional) 

            try
            {
                // Get all feature flags
                FeatureFlagsResponse result = apiInstance.GetFeatureFlags(startingAfter, limit);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DefaultApi.GetFeatureFlags: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetFeatureFlagsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get all feature flags
    ApiResponse<FeatureFlagsResponse> response = apiInstance.GetFeatureFlagsWithHttpInfo(startingAfter, limit);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DefaultApi.GetFeatureFlagsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **startingAfter** | **string?** | Cursor for fetching the next page (use the value returned by metadata.next_page_starting_after) | [optional]  |
| **limit** | **int?** | Maximum number of items to return (server may cap this) | [optional]  |

### Return type

[**FeatureFlagsResponse**](FeatureFlagsResponse.md)

### Authorization

[BearerAuth](../README.md#BearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successful response |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **500** | Internal server error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getpermission"></a>
# **GetPermission**
> PermissionResponse GetPermission (string key)

Get specific permission

Retrieve a specific permission by key

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Accounts.Api;
using Kinde.Accounts.Client;
using Kinde.Accounts.Model;

namespace Example
{
    public class GetPermissionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your-domain.kinde.com/account_api/v1";
            // Configure Bearer token for authorization: BearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DefaultApi(config);
            var key = "key_example";  // string | The permission key

            try
            {
                // Get specific permission
                PermissionResponse result = apiInstance.GetPermission(key);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DefaultApi.GetPermission: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetPermissionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get specific permission
    ApiResponse<PermissionResponse> response = apiInstance.GetPermissionWithHttpInfo(key);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DefaultApi.GetPermissionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **key** | **string** | The permission key |  |

### Return type

[**PermissionResponse**](PermissionResponse.md)

### Authorization

[BearerAuth](../README.md#BearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successful response |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Permission not found |  -  |
| **500** | Internal server error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getpermissions"></a>
# **GetPermissions**
> PermissionsResponse GetPermissions (string? startingAfter = null, int? limit = null)

Get all permissions

Retrieve all permissions for the current user

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Accounts.Api;
using Kinde.Accounts.Client;
using Kinde.Accounts.Model;

namespace Example
{
    public class GetPermissionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your-domain.kinde.com/account_api/v1";
            // Configure Bearer token for authorization: BearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DefaultApi(config);
            var startingAfter = "startingAfter_example";  // string? | Cursor for fetching the next page (use the value returned by metadata.next_page_starting_after) (optional) 
            var limit = 100;  // int? | Maximum number of items to return (1-1000, server may cap this) (optional)  (default to 100)

            try
            {
                // Get all permissions
                PermissionsResponse result = apiInstance.GetPermissions(startingAfter, limit);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DefaultApi.GetPermissions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetPermissionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get all permissions
    ApiResponse<PermissionsResponse> response = apiInstance.GetPermissionsWithHttpInfo(startingAfter, limit);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DefaultApi.GetPermissionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **startingAfter** | **string?** | Cursor for fetching the next page (use the value returned by metadata.next_page_starting_after) | [optional]  |
| **limit** | **int?** | Maximum number of items to return (1-1000, server may cap this) | [optional] [default to 100] |

### Return type

[**PermissionsResponse**](PermissionsResponse.md)

### Authorization

[BearerAuth](../README.md#BearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successful response |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **500** | Internal server error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getroles"></a>
# **GetRoles**
> RolesResponse GetRoles (string? startingAfter = null, int? limit = null)

Get all roles

Retrieve all roles for the current user

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Accounts.Api;
using Kinde.Accounts.Client;
using Kinde.Accounts.Model;

namespace Example
{
    public class GetRolesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your-domain.kinde.com/account_api/v1";
            // Configure Bearer token for authorization: BearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DefaultApi(config);
            var startingAfter = "startingAfter_example";  // string? | Cursor for fetching the next page (use the value returned by metadata.next_page_starting_after) (optional) 
            var limit = 100;  // int? | Maximum number of items to return (1-1000, server may cap this) (optional)  (default to 100)

            try
            {
                // Get all roles
                RolesResponse result = apiInstance.GetRoles(startingAfter, limit);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DefaultApi.GetRoles: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetRolesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get all roles
    ApiResponse<RolesResponse> response = apiInstance.GetRolesWithHttpInfo(startingAfter, limit);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DefaultApi.GetRolesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **startingAfter** | **string?** | Cursor for fetching the next page (use the value returned by metadata.next_page_starting_after) | [optional]  |
| **limit** | **int?** | Maximum number of items to return (1-1000, server may cap this) | [optional] [default to 100] |

### Return type

[**RolesResponse**](RolesResponse.md)

### Authorization

[BearerAuth](../README.md#BearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successful response |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **500** | Internal server error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getuserorganizations"></a>
# **GetUserOrganizations**
> UserOrganizationsResponse GetUserOrganizations (string? startingAfter = null, int? limit = null)

Get user organizations

Retrieve all organizations for the current user

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Accounts.Api;
using Kinde.Accounts.Client;
using Kinde.Accounts.Model;

namespace Example
{
    public class GetUserOrganizationsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your-domain.kinde.com/account_api/v1";
            // Configure Bearer token for authorization: BearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DefaultApi(config);
            var startingAfter = "startingAfter_example";  // string? | Cursor for fetching the next page (use the value returned by metadata.next_page_starting_after) (optional) 
            var limit = 56;  // int? | Maximum number of items to return (server may cap this) (optional) 

            try
            {
                // Get user organizations
                UserOrganizationsResponse result = apiInstance.GetUserOrganizations(startingAfter, limit);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DefaultApi.GetUserOrganizations: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetUserOrganizationsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get user organizations
    ApiResponse<UserOrganizationsResponse> response = apiInstance.GetUserOrganizationsWithHttpInfo(startingAfter, limit);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DefaultApi.GetUserOrganizationsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **startingAfter** | **string?** | Cursor for fetching the next page (use the value returned by metadata.next_page_starting_after) | [optional]  |
| **limit** | **int?** | Maximum number of items to return (server may cap this) | [optional]  |

### Return type

[**UserOrganizationsResponse**](UserOrganizationsResponse.md)

### Authorization

[BearerAuth](../README.md#BearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successful response |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **500** | Internal server error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getuserprofile"></a>
# **GetUserProfile**
> UserProfileResponse GetUserProfile ()

Get user profile

Retrieve the current user's profile information

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Accounts.Api;
using Kinde.Accounts.Client;
using Kinde.Accounts.Model;

namespace Example
{
    public class GetUserProfileExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your-domain.kinde.com/account_api/v1";
            // Configure Bearer token for authorization: BearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DefaultApi(config);

            try
            {
                // Get user profile
                UserProfileResponse result = apiInstance.GetUserProfile();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DefaultApi.GetUserProfile: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetUserProfileWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get user profile
    ApiResponse<UserProfileResponse> response = apiInstance.GetUserProfileWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DefaultApi.GetUserProfileWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**UserProfileResponse**](UserProfileResponse.md)

### Authorization

[BearerAuth](../README.md#BearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successful response |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **500** | Internal server error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

