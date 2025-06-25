# Kinde.Api.Api.OrganizationsApi

All URIs are relative to *https://your_kinde_subdomain.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**AddOrganizationLogo**](OrganizationsApi.md#addorganizationlogo) | **POST** /api/v1/organizations/{org_code}/logos/{type} | Add organization logo |
| [**AddOrganizationUserAPIScope**](OrganizationsApi.md#addorganizationuserapiscope) | **POST** /api/v1/organizations/{org_code}/users/{user_id}/apis/{api_id}/scopes/{scope_id} | Add scope to organization user api |
| [**AddOrganizationUsers**](OrganizationsApi.md#addorganizationusers) | **POST** /api/v1/organizations/{org_code}/users | Add Organization Users |
| [**CreateOrganization**](OrganizationsApi.md#createorganization) | **POST** /api/v1/organization | Create organization |
| [**CreateOrganizationUserPermission**](OrganizationsApi.md#createorganizationuserpermission) | **POST** /api/v1/organizations/{org_code}/users/{user_id}/permissions | Add Organization User Permission |
| [**CreateOrganizationUserRole**](OrganizationsApi.md#createorganizationuserrole) | **POST** /api/v1/organizations/{org_code}/users/{user_id}/roles | Add Organization User Role |
| [**DeleteOrganization**](OrganizationsApi.md#deleteorganization) | **DELETE** /api/v1/organization/{org_code} | Delete Organization |
| [**DeleteOrganizationFeatureFlagOverride**](OrganizationsApi.md#deleteorganizationfeatureflagoverride) | **DELETE** /api/v1/organizations/{org_code}/feature_flags/{feature_flag_key} | Delete Organization Feature Flag Override |
| [**DeleteOrganizationFeatureFlagOverrides**](OrganizationsApi.md#deleteorganizationfeatureflagoverrides) | **DELETE** /api/v1/organizations/{org_code}/feature_flags | Delete Organization Feature Flag Overrides |
| [**DeleteOrganizationHandle**](OrganizationsApi.md#deleteorganizationhandle) | **DELETE** /api/v1/organization/{org_code}/handle | Delete organization handle |
| [**DeleteOrganizationLogo**](OrganizationsApi.md#deleteorganizationlogo) | **DELETE** /api/v1/organizations/{org_code}/logos/{type} | Delete organization logo |
| [**DeleteOrganizationUserAPIScope**](OrganizationsApi.md#deleteorganizationuserapiscope) | **DELETE** /api/v1/organizations/{org_code}/users/{user_id}/apis/{api_id}/scopes/{scope_id} | Delete scope from organization user API |
| [**DeleteOrganizationUserPermission**](OrganizationsApi.md#deleteorganizationuserpermission) | **DELETE** /api/v1/organizations/{org_code}/users/{user_id}/permissions/{permission_id} | Delete Organization User Permission |
| [**DeleteOrganizationUserRole**](OrganizationsApi.md#deleteorganizationuserrole) | **DELETE** /api/v1/organizations/{org_code}/users/{user_id}/roles/{role_id} | Delete Organization User Role |
| [**EnableOrgConnection**](OrganizationsApi.md#enableorgconnection) | **POST** /api/v1/organizations/{organization_code}/connections/{connection_id} | Enable connection |
| [**GetOrgUserMFA**](OrganizationsApi.md#getorgusermfa) | **GET** /api/v1/organizations/{org_code}/users/{user_id}/mfa | Get an organization user&#39;s MFA configuration |
| [**GetOrganization**](OrganizationsApi.md#getorganization) | **GET** /api/v1/organization | Get organization |
| [**GetOrganizationConnections**](OrganizationsApi.md#getorganizationconnections) | **GET** /api/v1/organizations/{organization_code}/connections | Get connections |
| [**GetOrganizationFeatureFlags**](OrganizationsApi.md#getorganizationfeatureflags) | **GET** /api/v1/organizations/{org_code}/feature_flags | List Organization Feature Flags |
| [**GetOrganizationPropertyValues**](OrganizationsApi.md#getorganizationpropertyvalues) | **GET** /api/v1/organizations/{org_code}/properties | Get Organization Property Values |
| [**GetOrganizationUserPermissions**](OrganizationsApi.md#getorganizationuserpermissions) | **GET** /api/v1/organizations/{org_code}/users/{user_id}/permissions | List Organization User Permissions |
| [**GetOrganizationUserRoles**](OrganizationsApi.md#getorganizationuserroles) | **GET** /api/v1/organizations/{org_code}/users/{user_id}/roles | List Organization User Roles |
| [**GetOrganizationUsers**](OrganizationsApi.md#getorganizationusers) | **GET** /api/v1/organizations/{org_code}/users | Get organization users |
| [**GetOrganizations**](OrganizationsApi.md#getorganizations) | **GET** /api/v1/organizations | Get organizations |
| [**ReadOrganizationLogo**](OrganizationsApi.md#readorganizationlogo) | **GET** /api/v1/organizations/{org_code}/logos | Read organization logo details |
| [**RemoveOrgConnection**](OrganizationsApi.md#removeorgconnection) | **DELETE** /api/v1/organizations/{organization_code}/connections/{connection_id} | Remove connection |
| [**RemoveOrganizationUser**](OrganizationsApi.md#removeorganizationuser) | **DELETE** /api/v1/organizations/{org_code}/users/{user_id} | Remove Organization User |
| [**ReplaceOrganizationMFA**](OrganizationsApi.md#replaceorganizationmfa) | **PUT** /api/v1/organizations/{org_code}/mfa | Replace Organization MFA Configuration |
| [**ResetOrgUserMFA**](OrganizationsApi.md#resetorgusermfa) | **DELETE** /api/v1/organizations/{org_code}/users/{user_id}/mfa/{factor_id} | Reset specific organization MFA for a user |
| [**ResetOrgUserMFAAll**](OrganizationsApi.md#resetorgusermfaall) | **DELETE** /api/v1/organizations/{org_code}/users/{user_id}/mfa | Reset all organization MFA for a user |
| [**UpdateOrganization**](OrganizationsApi.md#updateorganization) | **PATCH** /api/v1/organization/{org_code} | Update Organization |
| [**UpdateOrganizationFeatureFlagOverride**](OrganizationsApi.md#updateorganizationfeatureflagoverride) | **PATCH** /api/v1/organizations/{org_code}/feature_flags/{feature_flag_key} | Update Organization Feature Flag Override |
| [**UpdateOrganizationProperties**](OrganizationsApi.md#updateorganizationproperties) | **PATCH** /api/v1/organizations/{org_code}/properties | Update Organization Property values |
| [**UpdateOrganizationProperty**](OrganizationsApi.md#updateorganizationproperty) | **PUT** /api/v1/organizations/{org_code}/properties/{property_key} | Update Organization Property value |
| [**UpdateOrganizationSessions**](OrganizationsApi.md#updateorganizationsessions) | **PATCH** /api/v1/organizations/{org_code}/sessions | Update organization session configuration |
| [**UpdateOrganizationUsers**](OrganizationsApi.md#updateorganizationusers) | **PATCH** /api/v1/organizations/{org_code}/users | Update Organization Users |

<a id="addorganizationlogo"></a>
# **AddOrganizationLogo**
> SuccessResponse AddOrganizationLogo (string orgCode, string type, FileParameter logo)

Add organization logo

Add organization logo  <div>   <code>update:organizations</code> </div> 

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
    public class AddOrganizationLogoExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = org_1ccfb819462;  // string | The organization's code.
            var type = dark;  // string | The type of logo to add.
            var logo = new System.IO.MemoryStream(System.IO.File.ReadAllBytes("/path/to/file.txt"));  // FileParameter | The logo file to upload.

            try
            {
                // Add organization logo
                SuccessResponse result = apiInstance.AddOrganizationLogo(orgCode, type, logo);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.AddOrganizationLogo: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AddOrganizationLogoWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add organization logo
    ApiResponse<SuccessResponse> response = apiInstance.AddOrganizationLogoWithHttpInfo(orgCode, type, logo);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.AddOrganizationLogoWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **type** | **string** | The type of logo to add. |  |
| **logo** | **FileParameter****FileParameter** | The logo file to upload. |  |

### Return type

[**SuccessResponse**](SuccessResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: multipart/form-data
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Organization logo successfully updated |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="addorganizationuserapiscope"></a>
# **AddOrganizationUserAPIScope**
> void AddOrganizationUserAPIScope (string orgCode, string userId, string apiId, string scopeId)

Add scope to organization user api

Add a scope to an organization user api.  <div>   <code>create:organization_user_api_scopes</code> </div> 

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
    public class AddOrganizationUserAPIScopeExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = "orgCode_example";  // string | The identifier for the organization.
            var userId = kp_5ce676e5d6a24bc9aac2fba35a46e958;  // string | User ID
            var apiId = 838f208d006a482dbd8cdb79a9889f68;  // string | API ID
            var scopeId = api_scope_019391daf58d87d8a7213419c016ac95;  // string | Scope ID

            try
            {
                // Add scope to organization user api
                apiInstance.AddOrganizationUserAPIScope(orgCode, userId, apiId, scopeId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.AddOrganizationUserAPIScope: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AddOrganizationUserAPIScopeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add scope to organization user api
    apiInstance.AddOrganizationUserAPIScopeWithHttpInfo(orgCode, userId, apiId, scopeId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.AddOrganizationUserAPIScopeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization. |  |
| **userId** | **string** | User ID |  |
| **apiId** | **string** | API ID |  |
| **scopeId** | **string** | Scope ID |  |

### Return type

void (empty response body)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | API scope successfully added to organization user api |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="addorganizationusers"></a>
# **AddOrganizationUsers**
> AddOrganizationUsersResponse AddOrganizationUsers (string orgCode, AddOrganizationUsersRequest addOrganizationUsersRequest = null)

Add Organization Users

Add existing users to an organization.  <div>   <code>create:organization_users</code> </div> 

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
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var addOrganizationUsersRequest = new AddOrganizationUsersRequest(); // AddOrganizationUsersRequest |  (optional) 

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
| **addOrganizationUsersRequest** | [**AddOrganizationUsersRequest**](AddOrganizationUsersRequest.md) |  | [optional]  |

### Return type

[**AddOrganizationUsersResponse**](AddOrganizationUsersResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Users successfully added. |  -  |
| **204** | No users added. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createorganization"></a>
# **CreateOrganization**
> CreateOrganizationResponse CreateOrganization (CreateOrganizationRequest createOrganizationRequest)

Create organization

Create a new organization. To learn more read about [multi tenancy using organizations](https://docs.kinde.com/build/organizations/multi-tenancy-using-organizations/)  <div>   <code>create:organizations</code> </div> 

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
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var createOrganizationRequest = new CreateOrganizationRequest(); // CreateOrganizationRequest | Organization details.

            try
            {
                // Create organization
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
    // Create organization
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
| **createOrganizationRequest** | [**CreateOrganizationRequest**](CreateOrganizationRequest.md) | Organization details. |  |

### Return type

[**CreateOrganizationResponse**](CreateOrganizationResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Organization successfully created. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createorganizationuserpermission"></a>
# **CreateOrganizationUserPermission**
> SuccessResponse CreateOrganizationUserPermission (string orgCode, string userId, CreateOrganizationUserPermissionRequest createOrganizationUserPermissionRequest)

Add Organization User Permission

Add permission to an organization user.  <div>   <code>create:organization_user_permissions</code> </div> 

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
    public class CreateOrganizationUserPermissionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var userId = "userId_example";  // string | The user's id.
            var createOrganizationUserPermissionRequest = new CreateOrganizationUserPermissionRequest(); // CreateOrganizationUserPermissionRequest | Permission details.

            try
            {
                // Add Organization User Permission
                SuccessResponse result = apiInstance.CreateOrganizationUserPermission(orgCode, userId, createOrganizationUserPermissionRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.CreateOrganizationUserPermission: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateOrganizationUserPermissionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add Organization User Permission
    ApiResponse<SuccessResponse> response = apiInstance.CreateOrganizationUserPermissionWithHttpInfo(orgCode, userId, createOrganizationUserPermissionRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.CreateOrganizationUserPermissionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **userId** | **string** | The user&#39;s id. |  |
| **createOrganizationUserPermissionRequest** | [**CreateOrganizationUserPermissionRequest**](CreateOrganizationUserPermissionRequest.md) | Permission details. |  |

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
| **200** | User permission successfully updated. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createorganizationuserrole"></a>
# **CreateOrganizationUserRole**
> SuccessResponse CreateOrganizationUserRole (string orgCode, string userId, CreateOrganizationUserRoleRequest createOrganizationUserRoleRequest)

Add Organization User Role

Add role to an organization user.  <div>   <code>create:organization_user_roles</code> </div> 

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
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
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
| **200** | Role successfully added. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteorganization"></a>
# **DeleteOrganization**
> SuccessResponse DeleteOrganization (string orgCode)

Delete Organization

Delete an organization.  <div>   <code>delete:organizations</code> </div> 

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
    public class DeleteOrganizationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = "orgCode_example";  // string | The identifier for the organization.

            try
            {
                // Delete Organization
                SuccessResponse result = apiInstance.DeleteOrganization(orgCode);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.DeleteOrganization: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteOrganizationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Organization
    ApiResponse<SuccessResponse> response = apiInstance.DeleteOrganizationWithHttpInfo(orgCode);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationWithHttpInfo: " + e.Message);
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
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Organization successfully deleted. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **404** | The specified resource was not found |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteorganizationfeatureflagoverride"></a>
# **DeleteOrganizationFeatureFlagOverride**
> SuccessResponse DeleteOrganizationFeatureFlagOverride (string orgCode, string featureFlagKey)

Delete Organization Feature Flag Override

Delete organization feature flag override.  <div>   <code>delete:organization_feature_flags</code> </div> 

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
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
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
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteorganizationfeatureflagoverrides"></a>
# **DeleteOrganizationFeatureFlagOverrides**
> SuccessResponse DeleteOrganizationFeatureFlagOverrides (string orgCode)

Delete Organization Feature Flag Overrides

Delete all organization feature flag overrides.  <div>   <code>delete:organization_feature_flags</code> </div> 

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
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
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
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteorganizationhandle"></a>
# **DeleteOrganizationHandle**
> SuccessResponse DeleteOrganizationHandle (string orgCode)

Delete organization handle

Delete organization handle  <div>   <code>delete:organization_handles</code> </div> 

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
    public class DeleteOrganizationHandleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = "orgCode_example";  // string | The organization's code.

            try
            {
                // Delete organization handle
                SuccessResponse result = apiInstance.DeleteOrganizationHandle(orgCode);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationHandle: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteOrganizationHandleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete organization handle
    ApiResponse<SuccessResponse> response = apiInstance.DeleteOrganizationHandleWithHttpInfo(orgCode);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationHandleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |

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
| **200** | Handle successfully deleted. |  -  |
| **400** | Bad request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteorganizationlogo"></a>
# **DeleteOrganizationLogo**
> SuccessResponse DeleteOrganizationLogo (string orgCode, string type)

Delete organization logo

Delete organization logo  <div>   <code>update:organizations</code> </div> 

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
    public class DeleteOrganizationLogoExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = org_1ccfb819462;  // string | The organization's code.
            var type = dark;  // string | The type of logo to delete.

            try
            {
                // Delete organization logo
                SuccessResponse result = apiInstance.DeleteOrganizationLogo(orgCode, type);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationLogo: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteOrganizationLogoWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete organization logo
    ApiResponse<SuccessResponse> response = apiInstance.DeleteOrganizationLogoWithHttpInfo(orgCode, type);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationLogoWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **type** | **string** | The type of logo to delete. |  |

### Return type

[**SuccessResponse**](SuccessResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Organization logo successfully deleted |  -  |
| **204** | No logo found to delete |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteorganizationuserapiscope"></a>
# **DeleteOrganizationUserAPIScope**
> void DeleteOrganizationUserAPIScope (string orgCode, string userId, string apiId, string scopeId)

Delete scope from organization user API

Delete a scope from an organization user api you previously created.  <div>   <code>delete:organization_user_api_scopes</code> </div> 

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
    public class DeleteOrganizationUserAPIScopeExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = "orgCode_example";  // string | The identifier for the organization.
            var userId = kp_5ce676e5d6a24bc9aac2fba35a46e958;  // string | User ID
            var apiId = 838f208d006a482dbd8cdb79a9889f68;  // string | API ID
            var scopeId = api_scope_019391daf58d87d8a7213419c016ac95;  // string | Scope ID

            try
            {
                // Delete scope from organization user API
                apiInstance.DeleteOrganizationUserAPIScope(orgCode, userId, apiId, scopeId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationUserAPIScope: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteOrganizationUserAPIScopeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete scope from organization user API
    apiInstance.DeleteOrganizationUserAPIScopeWithHttpInfo(orgCode, userId, apiId, scopeId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationUserAPIScopeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization. |  |
| **userId** | **string** | User ID |  |
| **apiId** | **string** | API ID |  |
| **scopeId** | **string** | Scope ID |  |

### Return type

void (empty response body)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Organization user API scope successfully deleted. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteorganizationuserpermission"></a>
# **DeleteOrganizationUserPermission**
> SuccessResponse DeleteOrganizationUserPermission (string orgCode, string userId, string permissionId)

Delete Organization User Permission

Delete permission for an organization user.  <div>   <code>delete:organization_user_permissions</code> </div> 

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
    public class DeleteOrganizationUserPermissionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var userId = "userId_example";  // string | The user's id.
            var permissionId = "permissionId_example";  // string | The permission id.

            try
            {
                // Delete Organization User Permission
                SuccessResponse result = apiInstance.DeleteOrganizationUserPermission(orgCode, userId, permissionId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationUserPermission: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteOrganizationUserPermissionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Organization User Permission
    ApiResponse<SuccessResponse> response = apiInstance.DeleteOrganizationUserPermissionWithHttpInfo(orgCode, userId, permissionId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationUserPermissionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **userId** | **string** | The user&#39;s id. |  |
| **permissionId** | **string** | The permission id. |  |

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
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteorganizationuserrole"></a>
# **DeleteOrganizationUserRole**
> SuccessResponse DeleteOrganizationUserRole (string orgCode, string userId, string roleId)

Delete Organization User Role

Delete role for an organization user.  <div>   <code>delete:organization_user_roles</code> </div> 

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
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
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
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="enableorgconnection"></a>
# **EnableOrgConnection**
> void EnableOrgConnection (string organizationCode, string connectionId)

Enable connection

Enable an auth connection for an organization.  <div>   <code>create:organization_connections</code> </div> 

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
    public class EnableOrgConnectionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var organizationCode = org_7d45b01ef13;  // string | The unique code for the organization.
            var connectionId = conn_0192c16abb53b44277e597d31877ba5b;  // string | The identifier for the connection.

            try
            {
                // Enable connection
                apiInstance.EnableOrgConnection(organizationCode, connectionId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.EnableOrgConnection: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the EnableOrgConnectionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Enable connection
    apiInstance.EnableOrgConnectionWithHttpInfo(organizationCode, connectionId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.EnableOrgConnectionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **organizationCode** | **string** | The unique code for the organization. |  |
| **connectionId** | **string** | The identifier for the connection. |  |

### Return type

void (empty response body)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Connection successfully enabled. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getorgusermfa"></a>
# **GetOrgUserMFA**
> GetUserMfaResponse GetOrgUserMFA (string orgCode, string userId)

Get an organization user's MFA configuration

Get an organization user’s MFA configuration.  <div>   <code>read:organization_user_mfa</code> </div> 

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
    public class GetOrgUserMFAExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = org_1ccfb819462;  // string | The identifier for the organization.
            var userId = kp_c3143a4b50ad43c88e541d9077681782;  // string | The identifier for the user

            try
            {
                // Get an organization user's MFA configuration
                GetUserMfaResponse result = apiInstance.GetOrgUserMFA(orgCode, userId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrgUserMFA: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrgUserMFAWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get an organization user's MFA configuration
    ApiResponse<GetUserMfaResponse> response = apiInstance.GetOrgUserMFAWithHttpInfo(orgCode, userId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrgUserMFAWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization. |  |
| **userId** | **string** | The identifier for the user |  |

### Return type

[**GetUserMfaResponse**](GetUserMfaResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successfully retrieve user&#39;s MFA configuration. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **404** | The specified resource was not found |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getorganization"></a>
# **GetOrganization**
> GetOrganizationResponse GetOrganization (string code = null)

Get organization

Retrieve organization details by code.  <div>   <code>read:organizations</code> </div> 

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
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var code = org_1ccfb819462;  // string | The organization's code. (optional) 

            try
            {
                // Get organization
                GetOrganizationResponse result = apiInstance.GetOrganization(code);
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
    // Get organization
    ApiResponse<GetOrganizationResponse> response = apiInstance.GetOrganizationWithHttpInfo(code);
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
| **code** | **string** | The organization&#39;s code. | [optional]  |

### Return type

[**GetOrganizationResponse**](GetOrganizationResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Organization successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getorganizationconnections"></a>
# **GetOrganizationConnections**
> GetConnectionsResponse GetOrganizationConnections (string organizationCode)

Get connections

Gets all connections for an organization.  <div>   <code>read:organization_connections</code> </div> 

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
    public class GetOrganizationConnectionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var organizationCode = org_7d45b01ef13;  // string | The organization code.

            try
            {
                // Get connections
                GetConnectionsResponse result = apiInstance.GetOrganizationConnections(organizationCode);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrganizationConnections: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrganizationConnectionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get connections
    ApiResponse<GetConnectionsResponse> response = apiInstance.GetOrganizationConnectionsWithHttpInfo(organizationCode);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrganizationConnectionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **organizationCode** | **string** | The organization code. |  |

### Return type

[**GetConnectionsResponse**](GetConnectionsResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Organization connections successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getorganizationfeatureflags"></a>
# **GetOrganizationFeatureFlags**
> GetOrganizationFeatureFlagsResponse GetOrganizationFeatureFlags (string orgCode)

List Organization Feature Flags

Get all organization feature flags.  <div>   <code>read:organization_feature_flags</code> </div> 

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
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
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
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getorganizationpropertyvalues"></a>
# **GetOrganizationPropertyValues**
> GetPropertyValuesResponse GetOrganizationPropertyValues (string orgCode)

Get Organization Property Values

Gets properties for an organization by org code.  <div>   <code>read:organization_properties</code> </div> 

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
    public class GetOrganizationPropertyValuesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = "orgCode_example";  // string | The organization's code.

            try
            {
                // Get Organization Property Values
                GetPropertyValuesResponse result = apiInstance.GetOrganizationPropertyValues(orgCode);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrganizationPropertyValues: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrganizationPropertyValuesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Organization Property Values
    ApiResponse<GetPropertyValuesResponse> response = apiInstance.GetOrganizationPropertyValuesWithHttpInfo(orgCode);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrganizationPropertyValuesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |

### Return type

[**GetPropertyValuesResponse**](GetPropertyValuesResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Properties successfully retrieved. |  -  |
| **400** | Bad request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getorganizationuserpermissions"></a>
# **GetOrganizationUserPermissions**
> GetOrganizationsUserPermissionsResponse GetOrganizationUserPermissions (string orgCode, string userId, string expand = null)

List Organization User Permissions

Get permissions for an organization user.  <div>   <code>read:organization_user_permissions</code> </div> 

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
    public class GetOrganizationUserPermissionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var userId = "userId_example";  // string | The user's id.
            var expand = "expand_example";  // string | Specify additional data to retrieve. Use \"roles\". (optional) 

            try
            {
                // List Organization User Permissions
                GetOrganizationsUserPermissionsResponse result = apiInstance.GetOrganizationUserPermissions(orgCode, userId, expand);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrganizationUserPermissions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrganizationUserPermissionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Organization User Permissions
    ApiResponse<GetOrganizationsUserPermissionsResponse> response = apiInstance.GetOrganizationUserPermissionsWithHttpInfo(orgCode, userId, expand);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrganizationUserPermissionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **userId** | **string** | The user&#39;s id. |  |
| **expand** | **string** | Specify additional data to retrieve. Use \&quot;roles\&quot;. | [optional]  |

### Return type

[**GetOrganizationsUserPermissionsResponse**](GetOrganizationsUserPermissionsResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A successful response with a list of user permissions. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getorganizationuserroles"></a>
# **GetOrganizationUserRoles**
> GetOrganizationsUserRolesResponse GetOrganizationUserRoles (string orgCode, string userId)

List Organization User Roles

Get roles for an organization user.  <div>   <code>read:organization_user_roles</code> </div> 

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
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
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
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getorganizationusers"></a>
# **GetOrganizationUsers**
> GetOrganizationUsersResponse GetOrganizationUsers (string orgCode, string sort = null, int? pageSize = null, string nextToken = null, string permissions = null, string roles = null)

Get organization users

Get user details for all members of an organization.  <div>   <code>read:organization_users</code> </div> 

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
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = org_1ccfb819462;  // string | The organization's code.
            var sort = email_asc;  // string | Field and order to sort the result by. (optional) 
            var pageSize = 10;  // int? | Number of results per page. Defaults to 10 if parameter not sent. (optional) 
            var nextToken = MTo6OmlkX2FzYw==;  // string | A string to get the next page of results if there are more results. (optional) 
            var permissions = admin;  // string | Filter by user permissions comma separated (where all match) (optional) 
            var roles = manager;  // string | Filter by user roles comma separated (where all match) (optional) 

            try
            {
                // Get organization users
                GetOrganizationUsersResponse result = apiInstance.GetOrganizationUsers(orgCode, sort, pageSize, nextToken, permissions, roles);
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
    // Get organization users
    ApiResponse<GetOrganizationUsersResponse> response = apiInstance.GetOrganizationUsersWithHttpInfo(orgCode, sort, pageSize, nextToken, permissions, roles);
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
| **sort** | **string** | Field and order to sort the result by. | [optional]  |
| **pageSize** | **int?** | Number of results per page. Defaults to 10 if parameter not sent. | [optional]  |
| **nextToken** | **string** | A string to get the next page of results if there are more results. | [optional]  |
| **permissions** | **string** | Filter by user permissions comma separated (where all match) | [optional]  |
| **roles** | **string** | Filter by user roles comma separated (where all match) | [optional]  |

### Return type

[**GetOrganizationUsersResponse**](GetOrganizationUsersResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A successful response with a list of organization users or an empty list. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getorganizations"></a>
# **GetOrganizations**
> GetOrganizationsResponse GetOrganizations (string sort = null, int? pageSize = null, string nextToken = null)

Get organizations

Get a list of organizations.  <div>   <code>read:organizations</code> </div> 

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
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var sort = "name_asc";  // string | Field and order to sort the result by. (optional) 
            var pageSize = 56;  // int? | Number of results per page. Defaults to 10 if parameter not sent. (optional) 
            var nextToken = "nextToken_example";  // string | A string to get the next page of results if there are more results. (optional) 

            try
            {
                // Get organizations
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
    // Get organizations
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
| **sort** | **string** | Field and order to sort the result by. | [optional]  |
| **pageSize** | **int?** | Number of results per page. Defaults to 10 if parameter not sent. | [optional]  |
| **nextToken** | **string** | A string to get the next page of results if there are more results. | [optional]  |

### Return type

[**GetOrganizationsResponse**](GetOrganizationsResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Organizations successfully retreived. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="readorganizationlogo"></a>
# **ReadOrganizationLogo**
> ReadLogoResponse ReadOrganizationLogo (string orgCode)

Read organization logo details

Read organization logo details  <div>   <code>read:organizations</code> </div> 

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
    public class ReadOrganizationLogoExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = org_1ccfb819462;  // string | The organization's code.

            try
            {
                // Read organization logo details
                ReadLogoResponse result = apiInstance.ReadOrganizationLogo(orgCode);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.ReadOrganizationLogo: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ReadOrganizationLogoWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Read organization logo details
    ApiResponse<ReadLogoResponse> response = apiInstance.ReadOrganizationLogoWithHttpInfo(orgCode);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.ReadOrganizationLogoWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |

### Return type

[**ReadLogoResponse**](ReadLogoResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successfully retrieved organization logo details |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="removeorgconnection"></a>
# **RemoveOrgConnection**
> SuccessResponse RemoveOrgConnection (string organizationCode, string connectionId)

Remove connection

Turn off an auth connection for an organization  <div>   <code>delete:organization_connections</code> </div> 

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
    public class RemoveOrgConnectionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var organizationCode = org_7d45b01ef13;  // string | The unique code for the organization.
            var connectionId = conn_0192c16abb53b44277e597d31877ba5b;  // string | The identifier for the connection.

            try
            {
                // Remove connection
                SuccessResponse result = apiInstance.RemoveOrgConnection(organizationCode, connectionId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.RemoveOrgConnection: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RemoveOrgConnectionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Remove connection
    ApiResponse<SuccessResponse> response = apiInstance.RemoveOrgConnectionWithHttpInfo(organizationCode, connectionId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.RemoveOrgConnectionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **organizationCode** | **string** | The unique code for the organization. |  |
| **connectionId** | **string** | The identifier for the connection. |  |

### Return type

[**SuccessResponse**](SuccessResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Connection successfully removed. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="removeorganizationuser"></a>
# **RemoveOrganizationUser**
> SuccessResponse RemoveOrganizationUser (string orgCode, string userId)

Remove Organization User

Remove user from an organization.  <div>   <code>delete:organization_users</code> </div> 

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
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
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
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="replaceorganizationmfa"></a>
# **ReplaceOrganizationMFA**
> SuccessResponse ReplaceOrganizationMFA (string orgCode, ReplaceOrganizationMFARequest replaceOrganizationMFARequest)

Replace Organization MFA Configuration

Replace Organization MFA Configuration.  <div>   <code>update:organization_mfa</code> </div> 

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
    public class ReplaceOrganizationMFAExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = "orgCode_example";  // string | The identifier for the organization
            var replaceOrganizationMFARequest = new ReplaceOrganizationMFARequest(); // ReplaceOrganizationMFARequest | MFA details.

            try
            {
                // Replace Organization MFA Configuration
                SuccessResponse result = apiInstance.ReplaceOrganizationMFA(orgCode, replaceOrganizationMFARequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.ReplaceOrganizationMFA: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ReplaceOrganizationMFAWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Replace Organization MFA Configuration
    ApiResponse<SuccessResponse> response = apiInstance.ReplaceOrganizationMFAWithHttpInfo(orgCode, replaceOrganizationMFARequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.ReplaceOrganizationMFAWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization |  |
| **replaceOrganizationMFARequest** | [**ReplaceOrganizationMFARequest**](ReplaceOrganizationMFARequest.md) | MFA details. |  |

### Return type

[**SuccessResponse**](SuccessResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | MFA Configuration updated successfully. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="resetorgusermfa"></a>
# **ResetOrgUserMFA**
> SuccessResponse ResetOrgUserMFA (string orgCode, string userId, string factorId)

Reset specific organization MFA for a user

Reset a specific organization MFA factor for a user.  <div>   <code>delete:organization_user_mfa</code> </div> 

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
    public class ResetOrgUserMFAExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = org_1ccfb819462;  // string | The identifier for the organization.
            var userId = kp_c3143a4b50ad43c88e541d9077681782;  // string | The identifier for the user
            var factorId = mfa_0193278a00ac29b3f6d4e4d462d55c47;  // string | The identifier for the MFA factor

            try
            {
                // Reset specific organization MFA for a user
                SuccessResponse result = apiInstance.ResetOrgUserMFA(orgCode, userId, factorId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.ResetOrgUserMFA: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ResetOrgUserMFAWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Reset specific organization MFA for a user
    ApiResponse<SuccessResponse> response = apiInstance.ResetOrgUserMFAWithHttpInfo(orgCode, userId, factorId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.ResetOrgUserMFAWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization. |  |
| **userId** | **string** | The identifier for the user |  |
| **factorId** | **string** | The identifier for the MFA factor |  |

### Return type

[**SuccessResponse**](SuccessResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | User&#39;s MFA successfully reset. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **404** | The specified resource was not found |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="resetorgusermfaall"></a>
# **ResetOrgUserMFAAll**
> SuccessResponse ResetOrgUserMFAAll (string orgCode, string userId)

Reset all organization MFA for a user

Reset all organization MFA factors for a user.  <div>   <code>delete:organization_user_mfa</code> </div> 

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
    public class ResetOrgUserMFAAllExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = org_1ccfb819462;  // string | The identifier for the organization.
            var userId = kp_c3143a4b50ad43c88e541d9077681782;  // string | The identifier for the user

            try
            {
                // Reset all organization MFA for a user
                SuccessResponse result = apiInstance.ResetOrgUserMFAAll(orgCode, userId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.ResetOrgUserMFAAll: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ResetOrgUserMFAAllWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Reset all organization MFA for a user
    ApiResponse<SuccessResponse> response = apiInstance.ResetOrgUserMFAAllWithHttpInfo(orgCode, userId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.ResetOrgUserMFAAllWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization. |  |
| **userId** | **string** | The identifier for the user |  |

### Return type

[**SuccessResponse**](SuccessResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | User&#39;s MFA successfully reset. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **404** | The specified resource was not found |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateorganization"></a>
# **UpdateOrganization**
> SuccessResponse UpdateOrganization (string orgCode, string expand = null, UpdateOrganizationRequest updateOrganizationRequest = null)

Update Organization

Update an organization.  <div>   <code>update:organizations</code> </div> 

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
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = org_1ccfb819462;  // string | The identifier for the organization.
            var expand = "billing";  // string | Specify additional data to retrieve. Use \"billing\". (optional) 
            var updateOrganizationRequest = new UpdateOrganizationRequest(); // UpdateOrganizationRequest | Organization details. (optional) 

            try
            {
                // Update Organization
                SuccessResponse result = apiInstance.UpdateOrganization(orgCode, expand, updateOrganizationRequest);
                Debug.WriteLine(result);
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
    ApiResponse<SuccessResponse> response = apiInstance.UpdateOrganizationWithHttpInfo(orgCode, expand, updateOrganizationRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
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
| **expand** | **string** | Specify additional data to retrieve. Use \&quot;billing\&quot;. | [optional]  |
| **updateOrganizationRequest** | [**UpdateOrganizationRequest**](UpdateOrganizationRequest.md) | Organization details. | [optional]  |

### Return type

[**SuccessResponse**](SuccessResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Organization successfully updated. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateorganizationfeatureflagoverride"></a>
# **UpdateOrganizationFeatureFlagOverride**
> SuccessResponse UpdateOrganizationFeatureFlagOverride (string orgCode, string featureFlagKey, string value)

Update Organization Feature Flag Override

Update organization feature flag override.  <div>   <code>update:organization_feature_flags</code> </div> 

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
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
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
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateorganizationproperties"></a>
# **UpdateOrganizationProperties**
> SuccessResponse UpdateOrganizationProperties (string orgCode, UpdateOrganizationPropertiesRequest updateOrganizationPropertiesRequest)

Update Organization Property values

Update organization property values.  <div>   <code>update:organization_properties</code> </div> 

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
    public class UpdateOrganizationPropertiesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = "orgCode_example";  // string | The identifier for the organization
            var updateOrganizationPropertiesRequest = new UpdateOrganizationPropertiesRequest(); // UpdateOrganizationPropertiesRequest | Properties to update.

            try
            {
                // Update Organization Property values
                SuccessResponse result = apiInstance.UpdateOrganizationProperties(orgCode, updateOrganizationPropertiesRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationProperties: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateOrganizationPropertiesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Organization Property values
    ApiResponse<SuccessResponse> response = apiInstance.UpdateOrganizationPropertiesWithHttpInfo(orgCode, updateOrganizationPropertiesRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationPropertiesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization |  |
| **updateOrganizationPropertiesRequest** | [**UpdateOrganizationPropertiesRequest**](UpdateOrganizationPropertiesRequest.md) | Properties to update. |  |

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
| **200** | Properties successfully updated. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateorganizationproperty"></a>
# **UpdateOrganizationProperty**
> SuccessResponse UpdateOrganizationProperty (string orgCode, string propertyKey, string value)

Update Organization Property value

Update organization property value.  <div>   <code>update:organization_properties</code> </div> 

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
    public class UpdateOrganizationPropertyExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = "orgCode_example";  // string | The identifier for the organization
            var propertyKey = "propertyKey_example";  // string | The identifier for the property
            var value = "value_example";  // string | The new property value

            try
            {
                // Update Organization Property value
                SuccessResponse result = apiInstance.UpdateOrganizationProperty(orgCode, propertyKey, value);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationProperty: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateOrganizationPropertyWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Organization Property value
    ApiResponse<SuccessResponse> response = apiInstance.UpdateOrganizationPropertyWithHttpInfo(orgCode, propertyKey, value);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationPropertyWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization |  |
| **propertyKey** | **string** | The identifier for the property |  |
| **value** | **string** | The new property value |  |

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
| **200** | Property successfully updated. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateorganizationsessions"></a>
# **UpdateOrganizationSessions**
> SuccessResponse UpdateOrganizationSessions (string orgCode, UpdateOrganizationSessionsRequest updateOrganizationSessionsRequest)

Update organization session configuration

Update the organization's session configuration.  <div>   <code>update:organizations</code> </div> 

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
    public class UpdateOrganizationSessionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = org_1ccfb819462;  // string | The organization's code.
            var updateOrganizationSessionsRequest = new UpdateOrganizationSessionsRequest(); // UpdateOrganizationSessionsRequest | Organization session configuration.

            try
            {
                // Update organization session configuration
                SuccessResponse result = apiInstance.UpdateOrganizationSessions(orgCode, updateOrganizationSessionsRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationSessions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateOrganizationSessionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update organization session configuration
    ApiResponse<SuccessResponse> response = apiInstance.UpdateOrganizationSessionsWithHttpInfo(orgCode, updateOrganizationSessionsRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationSessionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **updateOrganizationSessionsRequest** | [**UpdateOrganizationSessionsRequest**](UpdateOrganizationSessionsRequest.md) | Organization session configuration. |  |

### Return type

[**SuccessResponse**](SuccessResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Organization sessions successfully updated |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateorganizationusers"></a>
# **UpdateOrganizationUsers**
> UpdateOrganizationUsersResponse UpdateOrganizationUsers (string orgCode, UpdateOrganizationUsersRequest updateOrganizationUsersRequest = null)

Update Organization Users

Update users that belong to an organization.  <div>   <code>update:organization_users</code> </div> 

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
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new OrganizationsApi(httpClient, config, httpClientHandler);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var updateOrganizationUsersRequest = new UpdateOrganizationUsersRequest(); // UpdateOrganizationUsersRequest |  (optional) 

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
| **updateOrganizationUsersRequest** | [**UpdateOrganizationUsersRequest**](UpdateOrganizationUsersRequest.md) |  | [optional]  |

### Return type

[**UpdateOrganizationUsersResponse**](UpdateOrganizationUsersResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Users successfully removed. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

