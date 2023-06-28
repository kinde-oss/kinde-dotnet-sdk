# Kinde.Api.Api.CallbacksApi

All URIs are relative to *https://app.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**AddRedirectCallbackURLs**](CallbacksApi.md#addredirectcallbackurls) | **POST** /api/v1/applications/{app_id}/auth_redirect_urls | Add Redirect Callback URLs |
| [**GetCallbackURLs**](CallbacksApi.md#getcallbackurls) | **GET** /api/v1/applications/{app_id}/auth_redirect_urls | List Callback URLs |
| [**ReplaceRedirectCallbackURLs**](CallbacksApi.md#replaceredirectcallbackurls) | **PUT** /api/v1/applications/{app_id}/auth_redirect_urls | Replace Redirect Callback URLs |

<a name="addredirectcallbackurls"></a>
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

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getcallbackurls"></a>
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
 - **Accept**: application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Callback URLs successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="replaceredirectcallbackurls"></a>
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

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

