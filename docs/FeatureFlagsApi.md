# Kinde.Api.Api.FeatureFlagsApi

All URIs are relative to *https://app.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateFeatureFlag**](FeatureFlagsApi.md#createfeatureflag) | **POST** /api/v1/feature_flags | Create a new feature flag |
| [**DeleteFeatureFlag**](FeatureFlagsApi.md#deletefeatureflag) | **DELETE** /api/v1/feature_flags/{feature_flag_key} | Delete a feature flag |
| [**UpdateFeatureFlag**](FeatureFlagsApi.md#updatefeatureflag) | **PUT** /api/v1/feature_flags/{feature_flag_key} | Update a feature flag |

<a name="createfeatureflag"></a>
# **CreateFeatureFlag**
> SuccessResponse CreateFeatureFlag (string name, string description, string key, string type, string allowOverrideLevel, string defaultValue)

Create a new feature flag

Create feature flag.

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
            config.BasePath = "https://app.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new FeatureFlagsApi(httpClient, config, httpClientHandler);
            var name = "name_example";  // string | The name of the flag.
            var description = "description_example";  // string | Description of the flag purpose.
            var key = "key_example";  // string | The flag identifier to use in code.
            var type = "str";  // string | The variable type.
            var allowOverrideLevel = "env";  // string | Allow the flag to be overridden at a different level.
            var defaultValue = "defaultValue_example";  // string | Default value for the flag used by environments and organizations.

            try
            {
                // Create a new feature flag
                SuccessResponse result = apiInstance.CreateFeatureFlag(name, description, key, type, allowOverrideLevel, defaultValue);
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
    // Create a new feature flag
    ApiResponse<SuccessResponse> response = apiInstance.CreateFeatureFlagWithHttpInfo(name, description, key, type, allowOverrideLevel, defaultValue);
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
| **name** | **string** | The name of the flag. |  |
| **description** | **string** | Description of the flag purpose. |  |
| **key** | **string** | The flag identifier to use in code. |  |
| **type** | **string** | The variable type. |  |
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
| **201** | Feature flag successfully created |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="deletefeatureflag"></a>
# **DeleteFeatureFlag**
> SuccessResponse DeleteFeatureFlag (string featureFlagKey)

Delete a feature flag

Delete feature flag

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
            config.BasePath = "https://app.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new FeatureFlagsApi(httpClient, config, httpClientHandler);
            var featureFlagKey = "featureFlagKey_example";  // string | The identifier for the feature flag.

            try
            {
                // Delete a feature flag
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
    // Delete a feature flag
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

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updatefeatureflag"></a>
# **UpdateFeatureFlag**
> SuccessResponse UpdateFeatureFlag (string featureFlagKey, string name, string description, string key, string type, string allowOverrideLevel, string defaultValue)

Update a feature flag

Update feature flag.

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
            config.BasePath = "https://app.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new FeatureFlagsApi(httpClient, config, httpClientHandler);
            var featureFlagKey = "featureFlagKey_example";  // string | The identifier for the feature flag.
            var name = "name_example";  // string | The name of the flag.
            var description = "description_example";  // string | Description of the flag purpose.
            var key = "key_example";  // string | The flag identifier to use in code.
            var type = "str";  // string | The variable type
            var allowOverrideLevel = "env";  // string | Allow the flag to be overridden at a different level.
            var defaultValue = "defaultValue_example";  // string | Default value for the flag used by environments and organizations.

            try
            {
                // Update a feature flag
                SuccessResponse result = apiInstance.UpdateFeatureFlag(featureFlagKey, name, description, key, type, allowOverrideLevel, defaultValue);
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
    // Update a feature flag
    ApiResponse<SuccessResponse> response = apiInstance.UpdateFeatureFlagWithHttpInfo(featureFlagKey, name, description, key, type, allowOverrideLevel, defaultValue);
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
| **featureFlagKey** | **string** | The identifier for the feature flag. |  |
| **name** | **string** | The name of the flag. |  |
| **description** | **string** | Description of the flag purpose. |  |
| **key** | **string** | The flag identifier to use in code. |  |
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

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

