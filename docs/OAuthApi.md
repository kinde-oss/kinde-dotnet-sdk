# Kinde.Api.Api.OAuthApi

All URIs are relative to *https://your_kinde_subdomain.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**GetUserProfileV2**](OAuthApi.md#getuserprofilev2) | **GET** /oauth2/v2/user_profile | Get user profile |
| [**TokenIntrospection**](OAuthApi.md#tokenintrospection) | **POST** /oauth2/introspect | Introspect |
| [**TokenRevocation**](OAuthApi.md#tokenrevocation) | **POST** /oauth2/revoke | Revoke token |

<a id="getuserprofilev2"></a>
# **GetUserProfileV2**
> UserProfileV2 GetUserProfileV2 ()

Get user profile

This endpoint returns a user's ID, names, profile picture URL and email of the currently logged in user. 

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
    public class GetUserProfileV2Example
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
            var apiInstance = new OAuthApi(httpClient, config, httpClientHandler);

            try
            {
                // Get user profile
                UserProfileV2 result = apiInstance.GetUserProfileV2();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OAuthApi.GetUserProfileV2: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetUserProfileV2WithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get user profile
    ApiResponse<UserProfileV2> response = apiInstance.GetUserProfileV2WithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OAuthApi.GetUserProfileV2WithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**UserProfileV2**](UserProfileV2.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Details of logged in user. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="tokenintrospection"></a>
# **TokenIntrospection**
> TokenIntrospect TokenIntrospection (string token, string? tokenTypeHint = null)

Introspect

Retrieve information about the provided token.

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
    public class TokenIntrospectionExample
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
            var apiInstance = new OAuthApi(httpClient, config, httpClientHandler);
            var token = "token_example";  // string | The token to be introspected.
            var tokenTypeHint = "access_token";  // string? | A hint about the token type being queried in the request. (optional) 

            try
            {
                // Introspect
                TokenIntrospect result = apiInstance.TokenIntrospection(token, tokenTypeHint);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OAuthApi.TokenIntrospection: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the TokenIntrospectionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Introspect
    ApiResponse<TokenIntrospect> response = apiInstance.TokenIntrospectionWithHttpInfo(token, tokenTypeHint);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OAuthApi.TokenIntrospectionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **token** | **string** | The token to be introspected. |  |
| **tokenTypeHint** | **string?** | A hint about the token type being queried in the request. | [optional]  |

### Return type

[**TokenIntrospect**](TokenIntrospect.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/x-www-form-urlencoded
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Details of the token. |  -  |
| **401** | Bad request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="tokenrevocation"></a>
# **TokenRevocation**
> void TokenRevocation (string clientId, string token, string? clientSecret = null, string? tokenTypeHint = null)

Revoke token

Use this endpoint to invalidate an access or refresh token. The token will no longer be valid for use.

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
    public class TokenRevocationExample
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
            var apiInstance = new OAuthApi(httpClient, config, httpClientHandler);
            var clientId = "clientId_example";  // string | The `client_id` of your application.
            var token = "token_example";  // string | The token to be revoked.
            var clientSecret = "clientSecret_example";  // string? | The `client_secret` of your application. Required for backend apps only. (optional) 
            var tokenTypeHint = "access_token";  // string? | The type of token to be revoked. (optional) 

            try
            {
                // Revoke token
                apiInstance.TokenRevocation(clientId, token, clientSecret, tokenTypeHint);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OAuthApi.TokenRevocation: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the TokenRevocationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Revoke token
    apiInstance.TokenRevocationWithHttpInfo(clientId, token, clientSecret, tokenTypeHint);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OAuthApi.TokenRevocationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clientId** | **string** | The &#x60;client_id&#x60; of your application. |  |
| **token** | **string** | The token to be revoked. |  |
| **clientSecret** | **string?** | The &#x60;client_secret&#x60; of your application. Required for backend apps only. | [optional]  |
| **tokenTypeHint** | **string?** | The type of token to be revoked. | [optional]  |

### Return type

void (empty response body)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/x-www-form-urlencoded
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Token successfully revoked. |  -  |
| **400** | Invalid request. |  -  |
| **401** | Bad request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

