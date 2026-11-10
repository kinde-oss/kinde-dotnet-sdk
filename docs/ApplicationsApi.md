# Kinde.Api.Api.ApplicationsApi

All URIs are relative to *https://your_kinde_subdomain.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**AddApplicationAccessRole**](ApplicationsApi.md#addapplicationaccessrole) | **POST** /api/v1/applications/{application_id}/access_roles/{role_id} | Add application access role |
| [**CreateApplication**](ApplicationsApi.md#createapplication) | **POST** /api/v1/applications | Create application |
| [**CreateApplicationNativeApp**](ApplicationsApi.md#createapplicationnativeapp) | **POST** /api/v1/applications/{application_id}/native_apps | Register a native app |
| [**DeleteApplication**](ApplicationsApi.md#deleteapplication) | **DELETE** /api/v1/applications/{application_id} | Delete application |
| [**DeleteApplicationNativeApp**](ApplicationsApi.md#deleteapplicationnativeapp) | **DELETE** /api/v1/applications/{application_id}/native_apps/{native_app_id} | Remove a native app |
| [**EnableConnection**](ApplicationsApi.md#enableconnection) | **POST** /api/v1/applications/{application_id}/connections/{connection_id} | Enable connection |
| [**GetApplication**](ApplicationsApi.md#getapplication) | **GET** /api/v1/applications/{application_id} | Get application |
| [**GetApplicationAccessRoles**](ApplicationsApi.md#getapplicationaccessroles) | **GET** /api/v1/applications/{application_id}/access_roles | Get application access roles |
| [**GetApplicationConnections**](ApplicationsApi.md#getapplicationconnections) | **GET** /api/v1/applications/{application_id}/connections | Get connections |
| [**GetApplicationNativeApps**](ApplicationsApi.md#getapplicationnativeapps) | **GET** /api/v1/applications/{application_id}/native_apps | List native apps |
| [**GetApplicationPropertyValues**](ApplicationsApi.md#getapplicationpropertyvalues) | **GET** /api/v1/applications/{application_id}/properties | Get property values |
| [**GetApplications**](ApplicationsApi.md#getapplications) | **GET** /api/v1/applications | Get applications |
| [**RemoveApplicationAccessRole**](ApplicationsApi.md#removeapplicationaccessrole) | **DELETE** /api/v1/applications/{application_id}/access_roles/{role_id} | Remove application access role |
| [**RemoveConnection**](ApplicationsApi.md#removeconnection) | **DELETE** /api/v1/applications/{application_id}/connections/{connection_id} | Remove connection |
| [**UpdateApplication**](ApplicationsApi.md#updateapplication) | **PATCH** /api/v1/applications/{application_id} | Update Application |
| [**UpdateApplicationNativeSignIn**](ApplicationsApi.md#updateapplicationnativesignin) | **PUT** /api/v1/applications/{application_id}/native_sign_in | Turn native sign-in on or off |
| [**UpdateApplicationTokens**](ApplicationsApi.md#updateapplicationtokens) | **PATCH** /api/v1/applications/{application_id}/tokens | Update application tokens |
| [**UpdateApplicationsProperty**](ApplicationsApi.md#updateapplicationsproperty) | **PUT** /api/v1/applications/{application_id}/properties/{property_key} | Update property |

<a id="addapplicationaccessrole"></a>
# **AddApplicationAccessRole**
> SuccessResponse AddApplicationAccessRole (string applicationId, string roleId)

Add application access role

Add a role to the set configured as allowed to access an application. Adding a role does not enable role-based access control for the application.  <div>   <code>update:applications</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class AddApplicationAccessRoleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ApplicationsApi(config);
            var applicationId = 20bbffaa4c5e492a962273039d4ae18b;  // string | The identifier/client ID for the application.
            var roleId = 0192c16a-bb53-b442-77e5-97d31877ba5b;  // string | The identifier for the role.

            try
            {
                // Add application access role
                SuccessResponse result = apiInstance.AddApplicationAccessRole(applicationId, roleId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.AddApplicationAccessRole: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AddApplicationAccessRoleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add application access role
    ApiResponse<SuccessResponse> response = apiInstance.AddApplicationAccessRoleWithHttpInfo(applicationId, roleId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.AddApplicationAccessRoleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationId** | **string** | The identifier/client ID for the application. |  |
| **roleId** | **string** | The identifier for the role. |  |

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
| **200** | Access role successfully added. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createapplication"></a>
# **CreateApplication**
> CreateApplicationResponse CreateApplication (CreateApplicationRequest createApplicationRequest)

Create application

Create a new client.  <div>   <code>create:applications</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class CreateApplicationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ApplicationsApi(config);
            var createApplicationRequest = new CreateApplicationRequest(); // CreateApplicationRequest | 

            try
            {
                // Create application
                CreateApplicationResponse result = apiInstance.CreateApplication(createApplicationRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.CreateApplication: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateApplicationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create application
    ApiResponse<CreateApplicationResponse> response = apiInstance.CreateApplicationWithHttpInfo(createApplicationRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.CreateApplicationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createApplicationRequest** | [**CreateApplicationRequest**](CreateApplicationRequest.md) |  |  |

### Return type

[**CreateApplicationResponse**](CreateApplicationResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | Application successfully created. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createapplicationnativeapp"></a>
# **CreateApplicationNativeApp**
> CreateApplicationNativeApp201Response CreateApplicationNativeApp (string applicationId, CreateApplicationNativeAppRequest createApplicationNativeAppRequest)

Register a native app

Registers an Android app (package name and SHA-256 signing-certificate fingerprints) or an iOS app (Team ID and bundle ID) for an application, so it can use passkeys for the domain without a browser.  By default an application can register up to 10 native apps, iOS and Android together. Registering more returns `NATIVE_APP_LIMIT_REACHED`.  <div>   <code>create:application_native_apps</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class CreateApplicationNativeAppExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ApplicationsApi(config);
            var applicationId = 3b0b5c6c8fcc464fab397f4969b5f482;  // string | The application's ID / client ID.
            var createApplicationNativeAppRequest = new CreateApplicationNativeAppRequest(); // CreateApplicationNativeAppRequest | 

            try
            {
                // Register a native app
                CreateApplicationNativeApp201Response result = apiInstance.CreateApplicationNativeApp(applicationId, createApplicationNativeAppRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.CreateApplicationNativeApp: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateApplicationNativeAppWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Register a native app
    ApiResponse<CreateApplicationNativeApp201Response> response = apiInstance.CreateApplicationNativeAppWithHttpInfo(applicationId, createApplicationNativeAppRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.CreateApplicationNativeAppWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationId** | **string** | The application&#39;s ID / client ID. |  |
| **createApplicationNativeAppRequest** | [**CreateApplicationNativeAppRequest**](CreateApplicationNativeAppRequest.md) |  |  |

### Return type

[**CreateApplicationNativeApp201Response**](CreateApplicationNativeApp201Response.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | Native app successfully registered. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteapplication"></a>
# **DeleteApplication**
> SuccessResponse DeleteApplication (string applicationId)

Delete application

Delete a client / application.  <div>   <code>delete:applications</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class DeleteApplicationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ApplicationsApi(config);
            var applicationId = 20bbffaa4c5e492a962273039d4ae18b;  // string | The identifier for the application.

            try
            {
                // Delete application
                SuccessResponse result = apiInstance.DeleteApplication(applicationId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.DeleteApplication: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteApplicationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete application
    ApiResponse<SuccessResponse> response = apiInstance.DeleteApplicationWithHttpInfo(applicationId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.DeleteApplicationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationId** | **string** | The identifier for the application. |  |

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
| **200** | Application successfully deleted. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteapplicationnativeapp"></a>
# **DeleteApplicationNativeApp**
> SuccessResponse DeleteApplicationNativeApp (string applicationId, int nativeAppId)

Remove a native app

Removes a registered native app from an application.  <div>   <code>delete:application_native_apps</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class DeleteApplicationNativeAppExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ApplicationsApi(config);
            var applicationId = 3b0b5c6c8fcc464fab397f4969b5f482;  // string | The application's ID / client ID.
            var nativeAppId = 56;  // int | The identifier of the native app.

            try
            {
                // Remove a native app
                SuccessResponse result = apiInstance.DeleteApplicationNativeApp(applicationId, nativeAppId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.DeleteApplicationNativeApp: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteApplicationNativeAppWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Remove a native app
    ApiResponse<SuccessResponse> response = apiInstance.DeleteApplicationNativeAppWithHttpInfo(applicationId, nativeAppId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.DeleteApplicationNativeAppWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationId** | **string** | The application&#39;s ID / client ID. |  |
| **nativeAppId** | **int** | The identifier of the native app. |  |

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
| **200** | Native app successfully removed. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="enableconnection"></a>
# **EnableConnection**
> void EnableConnection (string applicationId, string connectionId)

Enable connection

Enable an auth connection for an application.  <div>   <code>create:application_connections</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class EnableConnectionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ApplicationsApi(config);
            var applicationId = 20bbffaa4c5e492a962273039d4ae18b;  // string | The identifier/client ID for the application.
            var connectionId = conn_0192c16abb53b44277e597d31877ba5b;  // string | The identifier for the connection.

            try
            {
                // Enable connection
                apiInstance.EnableConnection(applicationId, connectionId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.EnableConnection: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the EnableConnectionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Enable connection
    apiInstance.EnableConnectionWithHttpInfo(applicationId, connectionId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.EnableConnectionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationId** | **string** | The identifier/client ID for the application. |  |
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

<a id="getapplication"></a>
# **GetApplication**
> GetApplicationResponse GetApplication (string applicationId)

Get application

Gets an application given the application's ID.  <div>   <code>read:applications</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetApplicationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ApplicationsApi(config);
            var applicationId = 20bbffaa4c5e492a962273039d4ae18b;  // string | The identifier for the application.

            try
            {
                // Get application
                GetApplicationResponse result = apiInstance.GetApplication(applicationId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.GetApplication: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetApplicationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get application
    ApiResponse<GetApplicationResponse> response = apiInstance.GetApplicationWithHttpInfo(applicationId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.GetApplicationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationId** | **string** | The identifier for the application. |  |

### Return type

[**GetApplicationResponse**](GetApplicationResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Application successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getapplicationaccessroles"></a>
# **GetApplicationAccessRoles**
> GetApplicationAccessRolesResponse GetApplicationAccessRoles (string applicationId, int? pageSize = null, string? startingAfter = null, string? endingBefore = null)

Get application access roles

Gets the roles configured as allowed to access an application. These roles are enforced only when role-based access control is enabled for the application.  <div>   <code>read:applications</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetApplicationAccessRolesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ApplicationsApi(config);
            var applicationId = 20bbffaa4c5e492a962273039d4ae18b;  // string | The identifier/client ID for the application.
            var pageSize = 56;  // int? | Number of results per page. Defaults to 10 if parameter not sent. (optional) 
            var startingAfter = "startingAfter_example";  // string? | The ID of the role to start after. (optional) 
            var endingBefore = "endingBefore_example";  // string? | The ID of the role to end before. (optional) 

            try
            {
                // Get application access roles
                GetApplicationAccessRolesResponse result = apiInstance.GetApplicationAccessRoles(applicationId, pageSize, startingAfter, endingBefore);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.GetApplicationAccessRoles: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetApplicationAccessRolesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get application access roles
    ApiResponse<GetApplicationAccessRolesResponse> response = apiInstance.GetApplicationAccessRolesWithHttpInfo(applicationId, pageSize, startingAfter, endingBefore);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.GetApplicationAccessRolesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationId** | **string** | The identifier/client ID for the application. |  |
| **pageSize** | **int?** | Number of results per page. Defaults to 10 if parameter not sent. | [optional]  |
| **startingAfter** | **string?** | The ID of the role to start after. | [optional]  |
| **endingBefore** | **string?** | The ID of the role to end before. | [optional]  |

### Return type

[**GetApplicationAccessRolesResponse**](GetApplicationAccessRolesResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Application access roles successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getapplicationconnections"></a>
# **GetApplicationConnections**
> GetConnectionsResponse GetApplicationConnections (string applicationId)

Get connections

Gets all connections for an application.  <div>   <code>read:application_connections</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetApplicationConnectionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ApplicationsApi(config);
            var applicationId = 20bbffaa4c5e492a962273039d4ae18b;  // string | The identifier/client ID for the application.

            try
            {
                // Get connections
                GetConnectionsResponse result = apiInstance.GetApplicationConnections(applicationId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.GetApplicationConnections: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetApplicationConnectionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get connections
    ApiResponse<GetConnectionsResponse> response = apiInstance.GetApplicationConnectionsWithHttpInfo(applicationId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.GetApplicationConnectionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationId** | **string** | The identifier/client ID for the application. |  |

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
| **200** | Application connections successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getapplicationnativeapps"></a>
# **GetApplicationNativeApps**
> GetApplicationNativeApps200Response GetApplicationNativeApps (string applicationId)

List native apps

Returns the iOS and Android apps registered for an application, and whether native (browserless) passkey sign-in is enabled for it. Registered Android apps are listed in the domain's `/.well-known/assetlinks.json` and iOS apps in its `/.well-known/apple-app-site-association`.  <div>   <code>read:application_native_apps</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetApplicationNativeAppsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ApplicationsApi(config);
            var applicationId = 3b0b5c6c8fcc464fab397f4969b5f482;  // string | The application's ID / client ID.

            try
            {
                // List native apps
                GetApplicationNativeApps200Response result = apiInstance.GetApplicationNativeApps(applicationId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.GetApplicationNativeApps: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetApplicationNativeAppsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List native apps
    ApiResponse<GetApplicationNativeApps200Response> response = apiInstance.GetApplicationNativeAppsWithHttpInfo(applicationId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.GetApplicationNativeAppsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationId** | **string** | The application&#39;s ID / client ID. |  |

### Return type

[**GetApplicationNativeApps200Response**](GetApplicationNativeApps200Response.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Native apps successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getapplicationpropertyvalues"></a>
# **GetApplicationPropertyValues**
> GetPropertyValuesResponse GetApplicationPropertyValues (string applicationId)

Get property values

Gets properties for an application by client ID.  <div>   <code>read:application_properties</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetApplicationPropertyValuesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ApplicationsApi(config);
            var applicationId = 3b0b5c6c8fcc464fab397f4969b5f482;  // string | The application's ID / client ID.

            try
            {
                // Get property values
                GetPropertyValuesResponse result = apiInstance.GetApplicationPropertyValues(applicationId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.GetApplicationPropertyValues: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetApplicationPropertyValuesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get property values
    ApiResponse<GetPropertyValuesResponse> response = apiInstance.GetApplicationPropertyValuesWithHttpInfo(applicationId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.GetApplicationPropertyValuesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationId** | **string** | The application&#39;s ID / client ID. |  |

### Return type

[**GetPropertyValuesResponse**](GetPropertyValuesResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Properties successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getapplications"></a>
# **GetApplications**
> GetApplicationsResponse GetApplications (string? sort = null, int? pageSize = null, string? nextToken = null)

Get applications

Get a list of applications / clients.  <div>   <code>read:applications</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetApplicationsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ApplicationsApi(config);
            var sort = "name_asc";  // string? | Field and order to sort the result by. (optional) 
            var pageSize = 56;  // int? | Number of results per page. Defaults to 10 if parameter not sent. (optional) 
            var nextToken = "nextToken_example";  // string? | A string to get the next page of results if there are more results. (optional) 

            try
            {
                // Get applications
                GetApplicationsResponse result = apiInstance.GetApplications(sort, pageSize, nextToken);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.GetApplications: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetApplicationsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get applications
    ApiResponse<GetApplicationsResponse> response = apiInstance.GetApplicationsWithHttpInfo(sort, pageSize, nextToken);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.GetApplicationsWithHttpInfo: " + e.Message);
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

[**GetApplicationsResponse**](GetApplicationsResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A successful response with a list of applications or an empty list. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="removeapplicationaccessrole"></a>
# **RemoveApplicationAccessRole**
> SuccessResponse RemoveApplicationAccessRole (string applicationId, string roleId)

Remove application access role

Remove a role from the set that is allowed to access an application. Removing a role does not change whether role-based access control is enabled for the application.  <div>   <code>update:applications</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class RemoveApplicationAccessRoleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ApplicationsApi(config);
            var applicationId = 20bbffaa4c5e492a962273039d4ae18b;  // string | The identifier/client ID for the application.
            var roleId = 0192c16a-bb53-b442-77e5-97d31877ba5b;  // string | The identifier for the role.

            try
            {
                // Remove application access role
                SuccessResponse result = apiInstance.RemoveApplicationAccessRole(applicationId, roleId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.RemoveApplicationAccessRole: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RemoveApplicationAccessRoleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Remove application access role
    ApiResponse<SuccessResponse> response = apiInstance.RemoveApplicationAccessRoleWithHttpInfo(applicationId, roleId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.RemoveApplicationAccessRoleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationId** | **string** | The identifier/client ID for the application. |  |
| **roleId** | **string** | The identifier for the role. |  |

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
| **200** | Access role successfully removed. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="removeconnection"></a>
# **RemoveConnection**
> SuccessResponse RemoveConnection (string applicationId, string connectionId)

Remove connection

Turn off an auth connection for an application  <div>   <code>delete:application_connections</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class RemoveConnectionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ApplicationsApi(config);
            var applicationId = 20bbffaa4c5e492a962273039d4ae18b;  // string | The identifier/client ID for the application.
            var connectionId = conn_0192c16abb53b44277e597d31877ba5b;  // string | The identifier for the connection.

            try
            {
                // Remove connection
                SuccessResponse result = apiInstance.RemoveConnection(applicationId, connectionId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.RemoveConnection: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RemoveConnectionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Remove connection
    ApiResponse<SuccessResponse> response = apiInstance.RemoveConnectionWithHttpInfo(applicationId, connectionId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.RemoveConnectionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationId** | **string** | The identifier/client ID for the application. |  |
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

<a id="updateapplication"></a>
# **UpdateApplication**
> void UpdateApplication (string applicationId, UpdateApplicationRequest? updateApplicationRequest = null)

Update Application

Updates a client's settings. For more information, read [Applications in Kinde](https://docs.kinde.com/build/applications/about-applications)  <div>   <code>update:applications</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class UpdateApplicationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ApplicationsApi(config);
            var applicationId = 20bbffaa4c5e492a962273039d4ae18b;  // string | The identifier for the application.
            var updateApplicationRequest = new UpdateApplicationRequest?(); // UpdateApplicationRequest? | Application details. (optional) 

            try
            {
                // Update Application
                apiInstance.UpdateApplication(applicationId, updateApplicationRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.UpdateApplication: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateApplicationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Application
    apiInstance.UpdateApplicationWithHttpInfo(applicationId, updateApplicationRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.UpdateApplicationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationId** | **string** | The identifier for the application. |  |
| **updateApplicationRequest** | [**UpdateApplicationRequest?**](UpdateApplicationRequest?.md) | Application details. | [optional]  |

### Return type

void (empty response body)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Application successfully updated. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateapplicationnativesignin"></a>
# **UpdateApplicationNativeSignIn**
> SuccessResponse UpdateApplicationNativeSignIn (string applicationId, UpdateApplicationNativeSignInRequest updateApplicationNativeSignInRequest)

Turn native sign-in on or off

Enables or disables native (browserless) passkey sign-in through the authorization challenge endpoint, sets which passkeys it accepts, and whether native passkey sign-ins skip MFA. Only available for front-end and mobile (public) applications. Omitted fields are left unchanged.  <div>   <code>update:application_native_apps</code> </div>  A request that sends `is_passkey_mfa_exempt`, whatever its value, also requires the `update:mfa` and `update:organization_mfa` scopes. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class UpdateApplicationNativeSignInExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ApplicationsApi(config);
            var applicationId = 3b0b5c6c8fcc464fab397f4969b5f482;  // string | The application's ID / client ID.
            var updateApplicationNativeSignInRequest = new UpdateApplicationNativeSignInRequest(); // UpdateApplicationNativeSignInRequest | 

            try
            {
                // Turn native sign-in on or off
                SuccessResponse result = apiInstance.UpdateApplicationNativeSignIn(applicationId, updateApplicationNativeSignInRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.UpdateApplicationNativeSignIn: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateApplicationNativeSignInWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Turn native sign-in on or off
    ApiResponse<SuccessResponse> response = apiInstance.UpdateApplicationNativeSignInWithHttpInfo(applicationId, updateApplicationNativeSignInRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.UpdateApplicationNativeSignInWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationId** | **string** | The application&#39;s ID / client ID. |  |
| **updateApplicationNativeSignInRequest** | [**UpdateApplicationNativeSignInRequest**](UpdateApplicationNativeSignInRequest.md) |  |  |

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
| **200** | Native sign-in settings successfully updated. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateapplicationtokens"></a>
# **UpdateApplicationTokens**
> SuccessResponse UpdateApplicationTokens (string applicationId, UpdateApplicationTokensRequest updateApplicationTokensRequest)

Update application tokens

Configure tokens for an application.   <div>     <code>update:application_tokens</code>   </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class UpdateApplicationTokensExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ApplicationsApi(config);
            var applicationId = 20bbffaa4c5e492a962273039d4ae18b;  // string | The identifier/client ID for the application.
            var updateApplicationTokensRequest = new UpdateApplicationTokensRequest(); // UpdateApplicationTokensRequest | Application tokens.

            try
            {
                // Update application tokens
                SuccessResponse result = apiInstance.UpdateApplicationTokens(applicationId, updateApplicationTokensRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.UpdateApplicationTokens: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateApplicationTokensWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update application tokens
    ApiResponse<SuccessResponse> response = apiInstance.UpdateApplicationTokensWithHttpInfo(applicationId, updateApplicationTokensRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.UpdateApplicationTokensWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationId** | **string** | The identifier/client ID for the application. |  |
| **updateApplicationTokensRequest** | [**UpdateApplicationTokensRequest**](UpdateApplicationTokensRequest.md) | Application tokens. |  |

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
| **200** | Application tokens successfully updated. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateapplicationsproperty"></a>
# **UpdateApplicationsProperty**
> SuccessResponse UpdateApplicationsProperty (string applicationId, string propertyKey, UpdateApplicationsPropertyRequest updateApplicationsPropertyRequest)

Update property

Update application property value.  <div>   <code>update:application_properties</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class UpdateApplicationsPropertyExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ApplicationsApi(config);
            var applicationId = 3b0b5c6c8fcc464fab397f4969b5f482;  // string | The application's ID / client ID.
            var propertyKey = kp_some_key;  // string | The property's key.
            var updateApplicationsPropertyRequest = new UpdateApplicationsPropertyRequest(); // UpdateApplicationsPropertyRequest | 

            try
            {
                // Update property
                SuccessResponse result = apiInstance.UpdateApplicationsProperty(applicationId, propertyKey, updateApplicationsPropertyRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.UpdateApplicationsProperty: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateApplicationsPropertyWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update property
    ApiResponse<SuccessResponse> response = apiInstance.UpdateApplicationsPropertyWithHttpInfo(applicationId, propertyKey, updateApplicationsPropertyRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.UpdateApplicationsPropertyWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationId** | **string** | The application&#39;s ID / client ID. |  |
| **propertyKey** | **string** | The property&#39;s key. |  |
| **updateApplicationsPropertyRequest** | [**UpdateApplicationsPropertyRequest**](UpdateApplicationsPropertyRequest.md) |  |  |

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
| **200** | Property successfully updated |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

