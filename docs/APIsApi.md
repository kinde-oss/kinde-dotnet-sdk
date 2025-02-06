# Kinde.Api.Api.APIsApi

All URIs are relative to *https://your_kinde_subdomain.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**AddAPIApplicationScope**](APIsApi.md#addapiapplicationscope) | **POST** /api/v1/apis/{api_id}/applications/{application_id}/scopes/{scope_id} | Add scope to API application |
| [**AddAPIScope**](APIsApi.md#addapiscope) | **POST** /api/v1/apis/{api_id}/scopes | Create API scope |
| [**AddAPIs**](APIsApi.md#addapis) | **POST** /api/v1/apis | Create API |
| [**DeleteAPI**](APIsApi.md#deleteapi) | **DELETE** /api/v1/apis/{api_id} | Delete API |
| [**DeleteAPIAppliationScope**](APIsApi.md#deleteapiappliationscope) | **DELETE** /api/v1/apis/{api_id}/applications/{application_id}/scopes/{scope_id} | Delete API application scope |
| [**DeleteAPIScope**](APIsApi.md#deleteapiscope) | **DELETE** /api/v1/apis/{api_id}/scopes/{scope_id} | Delete API scope |
| [**GetAPI**](APIsApi.md#getapi) | **GET** /api/v1/apis/{api_id} | Get API |
| [**GetAPIScope**](APIsApi.md#getapiscope) | **GET** /api/v1/apis/{api_id}/scopes/{scope_id} | Get API scope |
| [**GetAPIScopes**](APIsApi.md#getapiscopes) | **GET** /api/v1/apis/{api_id}/scopes | Get API scopes |
| [**GetAPIs**](APIsApi.md#getapis) | **GET** /api/v1/apis | Get APIs |
| [**UpdateAPIApplications**](APIsApi.md#updateapiapplications) | **PATCH** /api/v1/apis/{api_id}/applications | Authorize API applications |
| [**UpdateAPIScope**](APIsApi.md#updateapiscope) | **PATCH** /api/v1/apis/{api_id}/scopes/{scope_id} | Update API scope |

<a id="addapiapplicationscope"></a>
# **AddAPIApplicationScope**
> void AddAPIApplicationScope (string apiId, string applicationId, string scopeId)

Add scope to API application

Add a scope to an API application.  <div>   <code>create:api_application_scopes</code> </div> 

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
    public class AddAPIApplicationScopeExample
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
            var apiInstance = new APIsApi(httpClient, config, httpClientHandler);
            var apiId = 838f208d006a482dbd8cdb79a9889f68;  // string | API ID
            var applicationId = 7643b487c97545aab79257fd13a1085a;  // string | Application ID
            var scopeId = api_scope_019391daf58d87d8a7213419c016ac95;  // string | Scope ID

            try
            {
                // Add scope to API application
                apiInstance.AddAPIApplicationScope(apiId, applicationId, scopeId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling APIsApi.AddAPIApplicationScope: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AddAPIApplicationScopeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add scope to API application
    apiInstance.AddAPIApplicationScopeWithHttpInfo(apiId, applicationId, scopeId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling APIsApi.AddAPIApplicationScopeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **apiId** | **string** | API ID |  |
| **applicationId** | **string** | Application ID |  |
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
| **200** | API scope successfully added to API application |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="addapiscope"></a>
# **AddAPIScope**
> CreateApiScopesResponse AddAPIScope (string apiId, AddAPIScopeRequest addAPIScopeRequest)

Create API scope

Create a new API scope.  <div>   <code>create:api_scopes</code> </div> 

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
    public class AddAPIScopeExample
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
            var apiInstance = new APIsApi(httpClient, config, httpClientHandler);
            var apiId = 838f208d006a482dbd8cdb79a9889f68;  // string | API ID
            var addAPIScopeRequest = new AddAPIScopeRequest(); // AddAPIScopeRequest | 

            try
            {
                // Create API scope
                CreateApiScopesResponse result = apiInstance.AddAPIScope(apiId, addAPIScopeRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling APIsApi.AddAPIScope: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AddAPIScopeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create API scope
    ApiResponse<CreateApiScopesResponse> response = apiInstance.AddAPIScopeWithHttpInfo(apiId, addAPIScopeRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling APIsApi.AddAPIScopeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **apiId** | **string** | API ID |  |
| **addAPIScopeRequest** | [**AddAPIScopeRequest**](AddAPIScopeRequest.md) |  |  |

### Return type

[**CreateApiScopesResponse**](CreateApiScopesResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | API scopes successfully created |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="addapis"></a>
# **AddAPIs**
> CreateApisResponse AddAPIs (AddAPIsRequest addAPIsRequest)

Create API

Register a new API. For more information read [Register and manage APIs](https://docs.kinde.com/developer-tools/your-apis/register-manage-apis/).  <div>   <code>create:apis</code> </div> 

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
    public class AddAPIsExample
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
            var apiInstance = new APIsApi(httpClient, config, httpClientHandler);
            var addAPIsRequest = new AddAPIsRequest(); // AddAPIsRequest | 

            try
            {
                // Create API
                CreateApisResponse result = apiInstance.AddAPIs(addAPIsRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling APIsApi.AddAPIs: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AddAPIsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create API
    ApiResponse<CreateApisResponse> response = apiInstance.AddAPIsWithHttpInfo(addAPIsRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling APIsApi.AddAPIsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **addAPIsRequest** | [**AddAPIsRequest**](AddAPIsRequest.md) |  |  |

### Return type

[**CreateApisResponse**](CreateApisResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | APIs successfully updated |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteapi"></a>
# **DeleteAPI**
> DeleteApiResponse DeleteAPI (string apiId)

Delete API

Delete an API you previously created.  <div>   <code>delete:apis</code> </div> 

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
    public class DeleteAPIExample
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
            var apiInstance = new APIsApi(httpClient, config, httpClientHandler);
            var apiId = 7ccd126599aa422a771abcb341596881;  // string | The API's ID.

            try
            {
                // Delete API
                DeleteApiResponse result = apiInstance.DeleteAPI(apiId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling APIsApi.DeleteAPI: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteAPIWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete API
    ApiResponse<DeleteApiResponse> response = apiInstance.DeleteAPIWithHttpInfo(apiId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling APIsApi.DeleteAPIWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **apiId** | **string** | The API&#39;s ID. |  |

### Return type

[**DeleteApiResponse**](DeleteApiResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | API successfully deleted. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteapiappliationscope"></a>
# **DeleteAPIAppliationScope**
> void DeleteAPIAppliationScope (string apiId, string applicationId, string scopeId)

Delete API application scope

Delete an API application scope you previously created.  <div>   <code>delete:apis_application_scopes</code> </div> 

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
    public class DeleteAPIAppliationScopeExample
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
            var apiInstance = new APIsApi(httpClient, config, httpClientHandler);
            var apiId = 838f208d006a482dbd8cdb79a9889f68;  // string | API ID
            var applicationId = 7643b487c97545aab79257fd13a1085a;  // string | Application ID
            var scopeId = api_scope_019391daf58d87d8a7213419c016ac95;  // string | Scope ID

            try
            {
                // Delete API application scope
                apiInstance.DeleteAPIAppliationScope(apiId, applicationId, scopeId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling APIsApi.DeleteAPIAppliationScope: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteAPIAppliationScopeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete API application scope
    apiInstance.DeleteAPIAppliationScopeWithHttpInfo(apiId, applicationId, scopeId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling APIsApi.DeleteAPIAppliationScopeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **apiId** | **string** | API ID |  |
| **applicationId** | **string** | Application ID |  |
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
| **200** | API scope successfully deleted. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteapiscope"></a>
# **DeleteAPIScope**
> void DeleteAPIScope (string apiId, string scopeId)

Delete API scope

Delete an API scope you previously created.  <div>   <code>delete:apis_scopes</code> </div> 

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
    public class DeleteAPIScopeExample
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
            var apiInstance = new APIsApi(httpClient, config, httpClientHandler);
            var apiId = 838f208d006a482dbd8cdb79a9889f68;  // string | API ID
            var scopeId = api_scope_019391daf58d87d8a7213419c016ac95;  // string | Scope ID

            try
            {
                // Delete API scope
                apiInstance.DeleteAPIScope(apiId, scopeId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling APIsApi.DeleteAPIScope: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteAPIScopeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete API scope
    apiInstance.DeleteAPIScopeWithHttpInfo(apiId, scopeId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling APIsApi.DeleteAPIScopeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
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
| **200** | API scope successfully deleted. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getapi"></a>
# **GetAPI**
> GetApiResponse GetAPI (string apiId)

Get API

Retrieve API details by ID.  <div>   <code>read:apis</code> </div> 

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
    public class GetAPIExample
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
            var apiInstance = new APIsApi(httpClient, config, httpClientHandler);
            var apiId = 7ccd126599aa422a771abcb341596881;  // string | The API's ID.

            try
            {
                // Get API
                GetApiResponse result = apiInstance.GetAPI(apiId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling APIsApi.GetAPI: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetAPIWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get API
    ApiResponse<GetApiResponse> response = apiInstance.GetAPIWithHttpInfo(apiId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling APIsApi.GetAPIWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **apiId** | **string** | The API&#39;s ID. |  |

### Return type

[**GetApiResponse**](GetApiResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | API successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getapiscope"></a>
# **GetAPIScope**
> GetApiScopeResponse GetAPIScope (string apiId, string scopeId)

Get API scope

Retrieve API scope by API ID.  <div>   <code>read:api_scopes</code> </div> 

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
    public class GetAPIScopeExample
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
            var apiInstance = new APIsApi(httpClient, config, httpClientHandler);
            var apiId = 838f208d006a482dbd8cdb79a9889f68;  // string | API ID
            var scopeId = api_scope_019391daf58d87d8a7213419c016ac95;  // string | Scope ID

            try
            {
                // Get API scope
                GetApiScopeResponse result = apiInstance.GetAPIScope(apiId, scopeId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling APIsApi.GetAPIScope: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetAPIScopeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get API scope
    ApiResponse<GetApiScopeResponse> response = apiInstance.GetAPIScopeWithHttpInfo(apiId, scopeId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling APIsApi.GetAPIScopeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **apiId** | **string** | API ID |  |
| **scopeId** | **string** | Scope ID |  |

### Return type

[**GetApiScopeResponse**](GetApiScopeResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | API scope successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getapiscopes"></a>
# **GetAPIScopes**
> GetApiScopesResponse GetAPIScopes (string apiId)

Get API scopes

Retrieve API scopes by API ID.  <div>   <code>read:api_scopes</code> </div> 

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
    public class GetAPIScopesExample
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
            var apiInstance = new APIsApi(httpClient, config, httpClientHandler);
            var apiId = 838f208d006a482dbd8cdb79a9889f68;  // string | API ID

            try
            {
                // Get API scopes
                GetApiScopesResponse result = apiInstance.GetAPIScopes(apiId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling APIsApi.GetAPIScopes: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetAPIScopesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get API scopes
    ApiResponse<GetApiScopesResponse> response = apiInstance.GetAPIScopesWithHttpInfo(apiId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling APIsApi.GetAPIScopesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **apiId** | **string** | API ID |  |

### Return type

[**GetApiScopesResponse**](GetApiScopesResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | API scopes successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getapis"></a>
# **GetAPIs**
> GetApisResponse GetAPIs (string? expand = null)

Get APIs

Returns a list of your APIs. The APIs are returned sorted by name.  <div>   <code>read:apis</code> </div> 

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
    public class GetAPIsExample
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
            var apiInstance = new APIsApi(httpClient, config, httpClientHandler);
            var expand = "scopes";  // string? | Specify additional data to retrieve. Use \"scopes\". (optional) 

            try
            {
                // Get APIs
                GetApisResponse result = apiInstance.GetAPIs(expand);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling APIsApi.GetAPIs: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetAPIsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get APIs
    ApiResponse<GetApisResponse> response = apiInstance.GetAPIsWithHttpInfo(expand);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling APIsApi.GetAPIsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **expand** | **string?** | Specify additional data to retrieve. Use \&quot;scopes\&quot;. | [optional]  |

### Return type

[**GetApisResponse**](GetApisResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A list of APIs. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateapiapplications"></a>
# **UpdateAPIApplications**
> AuthorizeAppApiResponse UpdateAPIApplications (string apiId, UpdateAPIApplicationsRequest updateAPIApplicationsRequest)

Authorize API applications

Authorize applications to be allowed to request access tokens for an API  <div>   <code>update:apis</code> </div> 

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
    public class UpdateAPIApplicationsExample
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
            var apiInstance = new APIsApi(httpClient, config, httpClientHandler);
            var apiId = 7ccd126599aa422a771abcb341596881;  // string | The API's ID.
            var updateAPIApplicationsRequest = new UpdateAPIApplicationsRequest(); // UpdateAPIApplicationsRequest | The applications you want to authorize.

            try
            {
                // Authorize API applications
                AuthorizeAppApiResponse result = apiInstance.UpdateAPIApplications(apiId, updateAPIApplicationsRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling APIsApi.UpdateAPIApplications: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateAPIApplicationsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Authorize API applications
    ApiResponse<AuthorizeAppApiResponse> response = apiInstance.UpdateAPIApplicationsWithHttpInfo(apiId, updateAPIApplicationsRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling APIsApi.UpdateAPIApplicationsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **apiId** | **string** | The API&#39;s ID. |  |
| **updateAPIApplicationsRequest** | [**UpdateAPIApplicationsRequest**](UpdateAPIApplicationsRequest.md) | The applications you want to authorize. |  |

### Return type

[**AuthorizeAppApiResponse**](AuthorizeAppApiResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Authorized applications updated. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateapiscope"></a>
# **UpdateAPIScope**
> void UpdateAPIScope (string apiId, string scopeId, UpdateAPIScopeRequest updateAPIScopeRequest)

Update API scope

Update an API scope.  <div>   <code>update:api_scopes</code> </div> 

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
    public class UpdateAPIScopeExample
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
            var apiInstance = new APIsApi(httpClient, config, httpClientHandler);
            var apiId = 838f208d006a482dbd8cdb79a9889f68;  // string | API ID
            var scopeId = api_scope_019391daf58d87d8a7213419c016ac95;  // string | Scope ID
            var updateAPIScopeRequest = new UpdateAPIScopeRequest(); // UpdateAPIScopeRequest | 

            try
            {
                // Update API scope
                apiInstance.UpdateAPIScope(apiId, scopeId, updateAPIScopeRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling APIsApi.UpdateAPIScope: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateAPIScopeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update API scope
    apiInstance.UpdateAPIScopeWithHttpInfo(apiId, scopeId, updateAPIScopeRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling APIsApi.UpdateAPIScopeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **apiId** | **string** | API ID |  |
| **scopeId** | **string** | Scope ID |  |
| **updateAPIScopeRequest** | [**UpdateAPIScopeRequest**](UpdateAPIScopeRequest.md) |  |  |

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
| **200** | API scope successfully updated |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

