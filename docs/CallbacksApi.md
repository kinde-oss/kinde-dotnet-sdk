# Kinde.Api.Api.CallbacksApi

All URIs are relative to *https://app.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**AddLogoutRedirectURLs**](CallbacksApi.md#addlogoutredirecturls) | **POST** /api/v1/applications/{app_id}/auth_logout_urls | Add Logout Redirect URLs |
| [**AddRedirectCallbackURLs**](CallbacksApi.md#addredirectcallbackurls) | **POST** /api/v1/applications/{app_id}/auth_redirect_urls | Add Redirect Callback URLs |
| [**DeleteCallbackURLs**](CallbacksApi.md#deletecallbackurls) | **DELETE** /api/v1/applications/{app_id}/auth_redirect_urls | Delete Callback URLs |
| [**DeleteLogoutURLs**](CallbacksApi.md#deletelogouturls) | **DELETE** /api/v1/applications/{app_id}/auth_logout_urls | Delete Logout URLs |
| [**GetCallbackURLs**](CallbacksApi.md#getcallbackurls) | **GET** /api/v1/applications/{app_id}/auth_redirect_urls | List Callback URLs |
| [**GetLogoutURLs**](CallbacksApi.md#getlogouturls) | **GET** /api/v1/applications/{app_id}/auth_logout_urls | List Logout URLs |
| [**ReplaceLogoutRedirectURLs**](CallbacksApi.md#replacelogoutredirecturls) | **PUT** /api/v1/applications/{app_id}/auth_logout_urls | Replace Logout Redirect URLs |
| [**ReplaceRedirectCallbackURLs**](CallbacksApi.md#replaceredirectcallbackurls) | **PUT** /api/v1/applications/{app_id}/auth_redirect_urls | Replace Redirect Callback URLs |

<a id="addlogoutredirecturls"></a>
# **AddLogoutRedirectURLs**
> SuccessResponse AddLogoutRedirectURLs (string appId, ReplaceLogoutRedirectURLsRequest replaceLogoutRedirectURLsRequest)

Add Logout Redirect URLs

Add additional logout redirect URLs. 

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
    public class AddLogoutRedirectURLsExample
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
            var apiInstance = new CallbacksApi(httpClient, config, httpClientHandler);
            var appId = "appId_example";  // string | The identifier for the application.
            var replaceLogoutRedirectURLsRequest = new ReplaceLogoutRedirectURLsRequest(); // ReplaceLogoutRedirectURLsRequest | Callback details.

            try
            {
                // Add Logout Redirect URLs
                SuccessResponse result = apiInstance.AddLogoutRedirectURLs(appId, replaceLogoutRedirectURLsRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CallbacksApi.AddLogoutRedirectURLs: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AddLogoutRedirectURLsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add Logout Redirect URLs
    ApiResponse<SuccessResponse> response = apiInstance.AddLogoutRedirectURLsWithHttpInfo(appId, replaceLogoutRedirectURLsRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CallbacksApi.AddLogoutRedirectURLsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **appId** | **string** | The identifier for the application. |  |
| **replaceLogoutRedirectURLsRequest** | [**ReplaceLogoutRedirectURLsRequest**](ReplaceLogoutRedirectURLsRequest.md) | Callback details. |  |

### Return type

[**SuccessResponse**](SuccessResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Logouts successfully updated |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="addredirectcallbackurls"></a>
# **AddRedirectCallbackURLs**
> SuccessResponse AddRedirectCallbackURLs (string appId, ReplaceRedirectCallbackURLsRequest replaceRedirectCallbackURLsRequest)

Add Redirect Callback URLs

Add additional redirect callback URLs. 

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
    public class AddRedirectCallbackURLsExample
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
            var apiInstance = new CallbacksApi(httpClient, config, httpClientHandler);
            var appId = "appId_example";  // string | The identifier for the application.
            var replaceRedirectCallbackURLsRequest = new ReplaceRedirectCallbackURLsRequest(); // ReplaceRedirectCallbackURLsRequest | Callback details.

            try
            {
                // Add Redirect Callback URLs
                SuccessResponse result = apiInstance.AddRedirectCallbackURLs(appId, replaceRedirectCallbackURLsRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CallbacksApi.AddRedirectCallbackURLs: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AddRedirectCallbackURLsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add Redirect Callback URLs
    ApiResponse<SuccessResponse> response = apiInstance.AddRedirectCallbackURLsWithHttpInfo(appId, replaceRedirectCallbackURLsRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CallbacksApi.AddRedirectCallbackURLsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **appId** | **string** | The identifier for the application. |  |
| **replaceRedirectCallbackURLsRequest** | [**ReplaceRedirectCallbackURLsRequest**](ReplaceRedirectCallbackURLsRequest.md) | Callback details. |  |

### Return type

[**SuccessResponse**](SuccessResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Callbacks successfully updated |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deletecallbackurls"></a>
# **DeleteCallbackURLs**
> SuccessResponse DeleteCallbackURLs (string appId, string urls)

Delete Callback URLs

Delete callback URLs. 

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
    public class DeleteCallbackURLsExample
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
            var apiInstance = new CallbacksApi(httpClient, config, httpClientHandler);
            var appId = "appId_example";  // string | The identifier for the application.
            var urls = "urls_example";  // string | Urls to delete, comma separated and url encoded.

            try
            {
                // Delete Callback URLs
                SuccessResponse result = apiInstance.DeleteCallbackURLs(appId, urls);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CallbacksApi.DeleteCallbackURLs: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteCallbackURLsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Callback URLs
    ApiResponse<SuccessResponse> response = apiInstance.DeleteCallbackURLsWithHttpInfo(appId, urls);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CallbacksApi.DeleteCallbackURLsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **appId** | **string** | The identifier for the application. |  |
| **urls** | **string** | Urls to delete, comma separated and url encoded. |  |

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
| **200** | Callback URLs successfully deleted. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deletelogouturls"></a>
# **DeleteLogoutURLs**
> SuccessResponse DeleteLogoutURLs (string appId, string urls)

Delete Logout URLs

Delete logout URLs. 

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
    public class DeleteLogoutURLsExample
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
            var apiInstance = new CallbacksApi(httpClient, config, httpClientHandler);
            var appId = "appId_example";  // string | The identifier for the application.
            var urls = "urls_example";  // string | Urls to delete, comma separated and url encoded.

            try
            {
                // Delete Logout URLs
                SuccessResponse result = apiInstance.DeleteLogoutURLs(appId, urls);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CallbacksApi.DeleteLogoutURLs: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteLogoutURLsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Logout URLs
    ApiResponse<SuccessResponse> response = apiInstance.DeleteLogoutURLsWithHttpInfo(appId, urls);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CallbacksApi.DeleteLogoutURLsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **appId** | **string** | The identifier for the application. |  |
| **urls** | **string** | Urls to delete, comma separated and url encoded. |  |

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
| **200** | Logout URLs successfully deleted. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getcallbackurls"></a>
# **GetCallbackURLs**
> RedirectCallbackUrls GetCallbackURLs (string appId)

List Callback URLs

Returns an application's redirect callback URLs. 

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
    public class GetCallbackURLsExample
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
            var apiInstance = new CallbacksApi(httpClient, config, httpClientHandler);
            var appId = "appId_example";  // string | The identifier for the application.

            try
            {
                // List Callback URLs
                RedirectCallbackUrls result = apiInstance.GetCallbackURLs(appId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CallbacksApi.GetCallbackURLs: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetCallbackURLsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Callback URLs
    ApiResponse<RedirectCallbackUrls> response = apiInstance.GetCallbackURLsWithHttpInfo(appId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CallbacksApi.GetCallbackURLsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **appId** | **string** | The identifier for the application. |  |

### Return type

[**RedirectCallbackUrls**](RedirectCallbackUrls.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Callback URLs successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getlogouturls"></a>
# **GetLogoutURLs**
> LogoutRedirectUrls GetLogoutURLs (string appId)

List Logout URLs

Returns an application's logout redirect URLs. 

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
    public class GetLogoutURLsExample
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
            var apiInstance = new CallbacksApi(httpClient, config, httpClientHandler);
            var appId = "appId_example";  // string | The identifier for the application.

            try
            {
                // List Logout URLs
                LogoutRedirectUrls result = apiInstance.GetLogoutURLs(appId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CallbacksApi.GetLogoutURLs: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetLogoutURLsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Logout URLs
    ApiResponse<LogoutRedirectUrls> response = apiInstance.GetLogoutURLsWithHttpInfo(appId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CallbacksApi.GetLogoutURLsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **appId** | **string** | The identifier for the application. |  |

### Return type

[**LogoutRedirectUrls**](LogoutRedirectUrls.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Logout URLs successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="replacelogoutredirecturls"></a>
# **ReplaceLogoutRedirectURLs**
> SuccessResponse ReplaceLogoutRedirectURLs (string appId, ReplaceLogoutRedirectURLsRequest replaceLogoutRedirectURLsRequest)

Replace Logout Redirect URLs

Replace all logout redirect URLs. 

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
    public class ReplaceLogoutRedirectURLsExample
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
            var apiInstance = new CallbacksApi(httpClient, config, httpClientHandler);
            var appId = "appId_example";  // string | The identifier for the application.
            var replaceLogoutRedirectURLsRequest = new ReplaceLogoutRedirectURLsRequest(); // ReplaceLogoutRedirectURLsRequest | Callback details.

            try
            {
                // Replace Logout Redirect URLs
                SuccessResponse result = apiInstance.ReplaceLogoutRedirectURLs(appId, replaceLogoutRedirectURLsRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CallbacksApi.ReplaceLogoutRedirectURLs: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ReplaceLogoutRedirectURLsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Replace Logout Redirect URLs
    ApiResponse<SuccessResponse> response = apiInstance.ReplaceLogoutRedirectURLsWithHttpInfo(appId, replaceLogoutRedirectURLsRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CallbacksApi.ReplaceLogoutRedirectURLsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **appId** | **string** | The identifier for the application. |  |
| **replaceLogoutRedirectURLsRequest** | [**ReplaceLogoutRedirectURLsRequest**](ReplaceLogoutRedirectURLsRequest.md) | Callback details. |  |

### Return type

[**SuccessResponse**](SuccessResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Logout URLs successfully updated |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="replaceredirectcallbackurls"></a>
# **ReplaceRedirectCallbackURLs**
> SuccessResponse ReplaceRedirectCallbackURLs (string appId, ReplaceRedirectCallbackURLsRequest replaceRedirectCallbackURLsRequest)

Replace Redirect Callback URLs

Replace all redirect callback URLs. 

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
    public class ReplaceRedirectCallbackURLsExample
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
            var apiInstance = new CallbacksApi(httpClient, config, httpClientHandler);
            var appId = "appId_example";  // string | The identifier for the application.
            var replaceRedirectCallbackURLsRequest = new ReplaceRedirectCallbackURLsRequest(); // ReplaceRedirectCallbackURLsRequest | Callback details.

            try
            {
                // Replace Redirect Callback URLs
                SuccessResponse result = apiInstance.ReplaceRedirectCallbackURLs(appId, replaceRedirectCallbackURLsRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CallbacksApi.ReplaceRedirectCallbackURLs: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ReplaceRedirectCallbackURLsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Replace Redirect Callback URLs
    ApiResponse<SuccessResponse> response = apiInstance.ReplaceRedirectCallbackURLsWithHttpInfo(appId, replaceRedirectCallbackURLsRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CallbacksApi.ReplaceRedirectCallbackURLsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **appId** | **string** | The identifier for the application. |  |
| **replaceRedirectCallbackURLsRequest** | [**ReplaceRedirectCallbackURLsRequest**](ReplaceRedirectCallbackURLsRequest.md) | Callback details. |  |

### Return type

[**SuccessResponse**](SuccessResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Callbacks successfully updated |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

