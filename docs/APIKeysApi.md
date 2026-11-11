# Kinde.Api.Api.APIKeysApi

All URIs are relative to *https://your_kinde_subdomain.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateApiKey**](APIKeysApi.md#createapikey) | **POST** /api/v1/api_keys | Create API key |
| [**DeleteApiKey**](APIKeysApi.md#deleteapikey) | **DELETE** /api/v1/api_keys/{key_id} | Delete API key |
| [**GetApiKey**](APIKeysApi.md#getapikey) | **GET** /api/v1/api_keys/{key_id} | Get API key |
| [**GetApiKeys**](APIKeysApi.md#getapikeys) | **GET** /api/v1/api_keys | Get API keys |
| [**RotateApiKey**](APIKeysApi.md#rotateapikey) | **PUT** /api/v1/api_keys/{key_id} | Rotate API key |
| [**VerifyApiKey**](APIKeysApi.md#verifyapikey) | **POST** /api/v1/api_keys/verify | Verify API key |

<a id="createapikey"></a>
# **CreateApiKey**
> CreateApiKeyResponse CreateApiKey (CreateApiKeyRequest createApiKeyRequest)

Create API key

Create a new API key.  <div>   <code>create:api_keys</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class CreateApiKeyExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new APIKeysApi(config);
            var createApiKeyRequest = new CreateApiKeyRequest(); // CreateApiKeyRequest | API key details.

            try
            {
                // Create API key
                CreateApiKeyResponse result = apiInstance.CreateApiKey(createApiKeyRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling APIKeysApi.CreateApiKey: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateApiKeyWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create API key
    ApiResponse<CreateApiKeyResponse> response = apiInstance.CreateApiKeyWithHttpInfo(createApiKeyRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling APIKeysApi.CreateApiKeyWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createApiKeyRequest** | [**CreateApiKeyRequest**](CreateApiKeyRequest.md) | API key details. |  |

### Return type

[**CreateApiKeyResponse**](CreateApiKeyResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | API key successfully created. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteapikey"></a>
# **DeleteApiKey**
> SuccessResponse DeleteApiKey (string keyId)

Delete API key

Delete an API key.  <div>   <code>delete:api_keys</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class DeleteApiKeyExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new APIKeysApi(config);
            var keyId = "keyId_example";  // string | The ID of the API key.

            try
            {
                // Delete API key
                SuccessResponse result = apiInstance.DeleteApiKey(keyId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling APIKeysApi.DeleteApiKey: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteApiKeyWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete API key
    ApiResponse<SuccessResponse> response = apiInstance.DeleteApiKeyWithHttpInfo(keyId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling APIKeysApi.DeleteApiKeyWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **keyId** | **string** | The ID of the API key. |  |

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
| **200** | API key successfully deleted. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **404** | The specified resource was not found |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getapikey"></a>
# **GetApiKey**
> GetApiKeyResponse GetApiKey (string keyId)

Get API key

Retrieve API key details by ID.  <div>   <code>read:api_keys</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetApiKeyExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new APIKeysApi(config);
            var keyId = "keyId_example";  // string | The ID of the API key.

            try
            {
                // Get API key
                GetApiKeyResponse result = apiInstance.GetApiKey(keyId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling APIKeysApi.GetApiKey: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetApiKeyWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get API key
    ApiResponse<GetApiKeyResponse> response = apiInstance.GetApiKeyWithHttpInfo(keyId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling APIKeysApi.GetApiKeyWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **keyId** | **string** | The ID of the API key. |  |

### Return type

[**GetApiKeyResponse**](GetApiKeyResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | API key successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **404** | The specified resource was not found |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getapikeys"></a>
# **GetApiKeys**
> GetApiKeysResponse GetApiKeys (int? pageSize = null, string? startingAfter = null, string? keyType = null, string? status = null, string? userId = null, string? orgCode = null)

Get API keys

Returns a list of API keys.  <div>   <code>read:api_keys</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetApiKeysExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new APIKeysApi(config);
            var pageSize = 56;  // int? | Number of results per page. Defaults to 50 if parameter not sent. (optional) 
            var startingAfter = "startingAfter_example";  // string? | The ID of the API key to start after. (optional) 
            var keyType = "organization";  // string? | Filter by API key type (organization or user). (optional) 
            var status = "active";  // string? | Filter by API key status (active, inactive, revoked). (optional) 
            var userId = "userId_example";  // string? | Filter by user ID to get API keys associated with a specific user. (optional) 
            var orgCode = "orgCode_example";  // string? | Filter by organization code to get API keys associated with a specific organization. (optional) 

            try
            {
                // Get API keys
                GetApiKeysResponse result = apiInstance.GetApiKeys(pageSize, startingAfter, keyType, status, userId, orgCode);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling APIKeysApi.GetApiKeys: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetApiKeysWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get API keys
    ApiResponse<GetApiKeysResponse> response = apiInstance.GetApiKeysWithHttpInfo(pageSize, startingAfter, keyType, status, userId, orgCode);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling APIKeysApi.GetApiKeysWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **pageSize** | **int?** | Number of results per page. Defaults to 50 if parameter not sent. | [optional]  |
| **startingAfter** | **string?** | The ID of the API key to start after. | [optional]  |
| **keyType** | **string?** | Filter by API key type (organization or user). | [optional]  |
| **status** | **string?** | Filter by API key status (active, inactive, revoked). | [optional]  |
| **userId** | **string?** | Filter by user ID to get API keys associated with a specific user. | [optional]  |
| **orgCode** | **string?** | Filter by organization code to get API keys associated with a specific organization. | [optional]  |

### Return type

[**GetApiKeysResponse**](GetApiKeysResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | API keys successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="rotateapikey"></a>
# **RotateApiKey**
> RotateApiKeyResponse RotateApiKey (string keyId)

Rotate API key

Rotate an API key to generate a new key while maintaining the same permissions and associations.  <div>   <code>update:api_keys</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class RotateApiKeyExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new APIKeysApi(config);
            var keyId = "keyId_example";  // string | The ID of the API key to rotate.

            try
            {
                // Rotate API key
                RotateApiKeyResponse result = apiInstance.RotateApiKey(keyId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling APIKeysApi.RotateApiKey: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RotateApiKeyWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Rotate API key
    ApiResponse<RotateApiKeyResponse> response = apiInstance.RotateApiKeyWithHttpInfo(keyId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling APIKeysApi.RotateApiKeyWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **keyId** | **string** | The ID of the API key to rotate. |  |

### Return type

[**RotateApiKeyResponse**](RotateApiKeyResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | API key successfully rotated. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **404** | The specified resource was not found |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="verifyapikey"></a>
# **VerifyApiKey**
> VerifyApiKeyResponse VerifyApiKey (VerifyApiKeyRequest verifyApiKeyRequest)

Verify API key

Verify an API key (public endpoint, no authentication required). 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class VerifyApiKeyExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new APIKeysApi(config);
            var verifyApiKeyRequest = new VerifyApiKeyRequest(); // VerifyApiKeyRequest | API key verification details.

            try
            {
                // Verify API key
                VerifyApiKeyResponse result = apiInstance.VerifyApiKey(verifyApiKeyRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling APIKeysApi.VerifyApiKey: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the VerifyApiKeyWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Verify API key
    ApiResponse<VerifyApiKeyResponse> response = apiInstance.VerifyApiKeyWithHttpInfo(verifyApiKeyRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling APIKeysApi.VerifyApiKeyWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **verifyApiKeyRequest** | [**VerifyApiKeyRequest**](VerifyApiKeyRequest.md) | API key verification details. |  |

### Return type

[**VerifyApiKeyResponse**](VerifyApiKeyResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | API key verification result. |  -  |
| **400** | Invalid request. |  -  |
| **401** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

