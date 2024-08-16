# Kinde.Api.Api.ConnectedAppsApi

All URIs are relative to *https://app.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**GetConnectedAppAuthUrl**](ConnectedAppsApi.md#getconnectedappauthurl) | **GET** /api/v1/connected_apps/auth_url | Get Connected App URL |
| [**GetConnectedAppToken**](ConnectedAppsApi.md#getconnectedapptoken) | **GET** /api/v1/connected_apps/token | Get Connected App Token |
| [**RevokeConnectedAppToken**](ConnectedAppsApi.md#revokeconnectedapptoken) | **POST** /api/v1/connected_apps/revoke | Revoke Connected App Token |

<a id="getconnectedappauthurl"></a>
# **GetConnectedAppAuthUrl**
> ConnectedAppsAuthUrl GetConnectedAppAuthUrl (string keyCodeRef, string? userId = null, string? orgCode = null, string? overrideCallbackUrl = null)

Get Connected App URL

Get a URL that authenticates and authorizes a user to a third-party connected app.

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
    public class GetConnectedAppAuthUrlExample
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
            var apiInstance = new ConnectedAppsApi(httpClient, config, httpClientHandler);
            var keyCodeRef = "keyCodeRef_example";  // string | The unique key code reference of the connected app to authenticate against.
            var userId = "userId_example";  // string? | The id of the user that needs to authenticate to the third-party connected app. (optional) 
            var orgCode = "orgCode_example";  // string? | The code of the Kinde organization that needs to authenticate to the third-party connected app. (optional) 
            var overrideCallbackUrl = "overrideCallbackUrl_example";  // string? | A URL that overrides the default callback URL setup in your connected app configuration (optional) 

            try
            {
                // Get Connected App URL
                ConnectedAppsAuthUrl result = apiInstance.GetConnectedAppAuthUrl(keyCodeRef, userId, orgCode, overrideCallbackUrl);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ConnectedAppsApi.GetConnectedAppAuthUrl: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetConnectedAppAuthUrlWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Connected App URL
    ApiResponse<ConnectedAppsAuthUrl> response = apiInstance.GetConnectedAppAuthUrlWithHttpInfo(keyCodeRef, userId, orgCode, overrideCallbackUrl);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ConnectedAppsApi.GetConnectedAppAuthUrlWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **keyCodeRef** | **string** | The unique key code reference of the connected app to authenticate against. |  |
| **userId** | **string?** | The id of the user that needs to authenticate to the third-party connected app. | [optional]  |
| **orgCode** | **string?** | The code of the Kinde organization that needs to authenticate to the third-party connected app. | [optional]  |
| **overrideCallbackUrl** | **string?** | A URL that overrides the default callback URL setup in your connected app configuration | [optional]  |

### Return type

[**ConnectedAppsAuthUrl**](ConnectedAppsAuthUrl.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A URL that can be used to authenticate and a session id to identify this authentication session. |  -  |
| **400** | Error retrieving connected app auth url. |  -  |
| **403** | Invalid credentials. |  -  |
| **404** | Error retrieving connected app auth url. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getconnectedapptoken"></a>
# **GetConnectedAppToken**
> ConnectedAppsAccessToken GetConnectedAppToken (string sessionId)

Get Connected App Token

Get an access token that can be used to call the third-party provider linked to the connected app.

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
    public class GetConnectedAppTokenExample
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
            var apiInstance = new ConnectedAppsApi(httpClient, config, httpClientHandler);
            var sessionId = "sessionId_example";  // string | The unique sesssion id representing the login session of a user.

            try
            {
                // Get Connected App Token
                ConnectedAppsAccessToken result = apiInstance.GetConnectedAppToken(sessionId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ConnectedAppsApi.GetConnectedAppToken: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetConnectedAppTokenWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Connected App Token
    ApiResponse<ConnectedAppsAccessToken> response = apiInstance.GetConnectedAppTokenWithHttpInfo(sessionId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ConnectedAppsApi.GetConnectedAppTokenWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **sessionId** | **string** | The unique sesssion id representing the login session of a user. |  |

### Return type

[**ConnectedAppsAccessToken**](ConnectedAppsAccessToken.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | An access token that can be used to query a third-party provider, as well as the token&#39;s expiry time. |  -  |
| **400** | The session id provided points to an invalid session. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="revokeconnectedapptoken"></a>
# **RevokeConnectedAppToken**
> SuccessResponse RevokeConnectedAppToken (string sessionId)

Revoke Connected App Token

Revoke the tokens linked to the connected app session.

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
    public class RevokeConnectedAppTokenExample
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
            var apiInstance = new ConnectedAppsApi(httpClient, config, httpClientHandler);
            var sessionId = "sessionId_example";  // string | The unique sesssion id representing the login session of a user.

            try
            {
                // Revoke Connected App Token
                SuccessResponse result = apiInstance.RevokeConnectedAppToken(sessionId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ConnectedAppsApi.RevokeConnectedAppToken: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RevokeConnectedAppTokenWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Revoke Connected App Token
    ApiResponse<SuccessResponse> response = apiInstance.RevokeConnectedAppTokenWithHttpInfo(sessionId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ConnectedAppsApi.RevokeConnectedAppTokenWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **sessionId** | **string** | The unique sesssion id representing the login session of a user. |  |

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
| **200** | An access token that can be used to query a third-party provider, as well as the token&#39;s expiry time. |  -  |
| **400** | Bad request. |  -  |
| **403** | Invalid credentials. |  -  |
| **405** | Invalid HTTP method used. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

