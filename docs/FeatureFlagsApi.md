# Kinde.Api.Api.FeatureFlagsApi

All URIs are relative to *https://your_kinde_subdomain.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateFeatureFlag**](FeatureFlagsApi.md#createfeatureflag) | **POST** /api/v1/feature_flags | Create Feature Flag |
| [**DeleteFeatureFlag**](FeatureFlagsApi.md#deletefeatureflag) | **DELETE** /api/v1/feature_flags/{feature_flag_key} | Delete Feature Flag |
| [**UpdateFeatureFlag**](FeatureFlagsApi.md#updatefeatureflag) | **PUT** /api/v1/feature_flags/{feature_flag_key} | Replace Feature Flag |

<a id="createfeatureflag"></a>
# **CreateFeatureFlag**
> SuccessResponse CreateFeatureFlag (CreateFeatureFlagRequest createFeatureFlagRequest)

Create Feature Flag

Create feature flag.  <div>   <code>create:feature_flags</code> </div> 

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
    public class CreateFeatureFlagExample
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
            var apiInstance = new FeatureFlagsApi(httpClient, config, httpClientHandler);
            var createFeatureFlagRequest = new CreateFeatureFlagRequest(); // CreateFeatureFlagRequest | Flag details.

            try
            {
                // Create Feature Flag
                SuccessResponse result = apiInstance.CreateFeatureFlag(createFeatureFlagRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FeatureFlagsApi.CreateFeatureFlag: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateFeatureFlagWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Feature Flag
    ApiResponse<SuccessResponse> response = apiInstance.CreateFeatureFlagWithHttpInfo(createFeatureFlagRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FeatureFlagsApi.CreateFeatureFlagWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createFeatureFlagRequest** | [**CreateFeatureFlagRequest**](CreateFeatureFlagRequest.md) | Flag details. |  |

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
| **201** | Feature flag successfully created |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deletefeatureflag"></a>
# **DeleteFeatureFlag**
> SuccessResponse DeleteFeatureFlag (string featureFlagKey)

Delete Feature Flag

Delete feature flag  <div>   <code>delete:feature_flags</code> </div> 

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
    public class DeleteFeatureFlagExample
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
            var apiInstance = new FeatureFlagsApi(httpClient, config, httpClientHandler);
            var featureFlagKey = "featureFlagKey_example";  // string | The identifier for the feature flag.

            try
            {
                // Delete Feature Flag
                SuccessResponse result = apiInstance.DeleteFeatureFlag(featureFlagKey);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FeatureFlagsApi.DeleteFeatureFlag: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteFeatureFlagWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Feature Flag
    ApiResponse<SuccessResponse> response = apiInstance.DeleteFeatureFlagWithHttpInfo(featureFlagKey);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FeatureFlagsApi.DeleteFeatureFlagWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
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
| **200** | Feature flag successfully updated. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updatefeatureflag"></a>
# **UpdateFeatureFlag**
> SuccessResponse UpdateFeatureFlag (string featureFlagKey, string name, string description, string type, string allowOverrideLevel, string defaultValue)

Replace Feature Flag

Update feature flag.  <div>   <code>update:feature_flags</code> </div> 

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
    public class UpdateFeatureFlagExample
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
            var apiInstance = new FeatureFlagsApi(httpClient, config, httpClientHandler);
            var featureFlagKey = "featureFlagKey_example";  // string | The key identifier for the feature flag.
            var name = "name_example";  // string | The name of the flag.
            var description = "description_example";  // string | Description of the flag purpose.
            var type = "str";  // string | The variable type
            var allowOverrideLevel = "env";  // string | Allow the flag to be overridden at a different level.
            var defaultValue = "defaultValue_example";  // string | Default value for the flag used by environments and organizations.

            try
            {
                // Replace Feature Flag
                SuccessResponse result = apiInstance.UpdateFeatureFlag(featureFlagKey, name, description, type, allowOverrideLevel, defaultValue);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FeatureFlagsApi.UpdateFeatureFlag: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateFeatureFlagWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Replace Feature Flag
    ApiResponse<SuccessResponse> response = apiInstance.UpdateFeatureFlagWithHttpInfo(featureFlagKey, name, description, type, allowOverrideLevel, defaultValue);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FeatureFlagsApi.UpdateFeatureFlagWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **featureFlagKey** | **string** | The key identifier for the feature flag. |  |
| **name** | **string** | The name of the flag. |  |
| **description** | **string** | Description of the flag purpose. |  |
| **type** | **string** | The variable type |  |
| **allowOverrideLevel** | **string** | Allow the flag to be overridden at a different level. |  |
| **defaultValue** | **string** | Default value for the flag used by environments and organizations. |  |

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
| **200** | Feature flag successfully updated. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

