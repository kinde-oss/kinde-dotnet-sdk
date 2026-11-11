# Kinde.Api.Api.EnvironmentsApi

All URIs are relative to *https://your_kinde_subdomain.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**AddLogo**](EnvironmentsApi.md#addlogo) | **PUT** /api/v1/environment/logos/{type} | Add logo |
| [**DeleteEnvironementFeatureFlagOverride**](EnvironmentsApi.md#deleteenvironementfeatureflagoverride) | **DELETE** /api/v1/environment/feature_flags/{feature_flag_key} | Delete Environment Feature Flag Override |
| [**DeleteEnvironementFeatureFlagOverrides**](EnvironmentsApi.md#deleteenvironementfeatureflagoverrides) | **DELETE** /api/v1/environment/feature_flags | Delete Environment Feature Flag Overrides |
| [**DeleteLogo**](EnvironmentsApi.md#deletelogo) | **DELETE** /api/v1/environment/logos/{type} | Delete logo |
| [**GetEnvironementFeatureFlags**](EnvironmentsApi.md#getenvironementfeatureflags) | **GET** /api/v1/environment/feature_flags | List Environment Feature Flags |
| [**GetEnvironment**](EnvironmentsApi.md#getenvironment) | **GET** /api/v1/environment | Get environment |
| [**GetPasskey**](EnvironmentsApi.md#getpasskey) | **GET** /api/v1/passkey | Get environment passkey settings |
| [**ReadLogo**](EnvironmentsApi.md#readlogo) | **GET** /api/v1/environment/logos | Read logo details |
| [**UpdateEnvironementFeatureFlagOverride**](EnvironmentsApi.md#updateenvironementfeatureflagoverride) | **PATCH** /api/v1/environment/feature_flags/{feature_flag_key} | Update Environment Feature Flag Override |
| [**UpdatePasskey**](EnvironmentsApi.md#updatepasskey) | **PUT** /api/v1/passkey | Update environment passkey settings |

<a id="addlogo"></a>
# **AddLogo**
> SuccessResponse AddLogo (string type, System.IO.Stream logo)

Add logo

Add environment logo  <div>   <code>update:environments</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class AddLogoExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new EnvironmentsApi(config);
            var type = dark;  // string | The type of logo to add.
            var logo = new System.IO.MemoryStream(System.IO.File.ReadAllBytes("/path/to/file.txt"));  // System.IO.Stream | The logo file to upload.

            try
            {
                // Add logo
                SuccessResponse result = apiInstance.AddLogo(type, logo);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EnvironmentsApi.AddLogo: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AddLogoWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add logo
    ApiResponse<SuccessResponse> response = apiInstance.AddLogoWithHttpInfo(type, logo);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling EnvironmentsApi.AddLogoWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **type** | **string** | The type of logo to add. |  |
| **logo** | **System.IO.Stream****System.IO.Stream** | The logo file to upload. |  |

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
| **200** | Logo successfully updated |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteenvironementfeatureflagoverride"></a>
# **DeleteEnvironementFeatureFlagOverride**
> SuccessResponse DeleteEnvironementFeatureFlagOverride (string featureFlagKey)

Delete Environment Feature Flag Override

Delete environment feature flag override.  <div>   <code>delete:environment_feature_flags</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class DeleteEnvironementFeatureFlagOverrideExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new EnvironmentsApi(config);
            var featureFlagKey = "featureFlagKey_example";  // string | The identifier for the feature flag.

            try
            {
                // Delete Environment Feature Flag Override
                SuccessResponse result = apiInstance.DeleteEnvironementFeatureFlagOverride(featureFlagKey);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EnvironmentsApi.DeleteEnvironementFeatureFlagOverride: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteEnvironementFeatureFlagOverrideWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Environment Feature Flag Override
    ApiResponse<SuccessResponse> response = apiInstance.DeleteEnvironementFeatureFlagOverrideWithHttpInfo(featureFlagKey);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling EnvironmentsApi.DeleteEnvironementFeatureFlagOverrideWithHttpInfo: " + e.Message);
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
| **200** | Feature flag deleted successfully. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteenvironementfeatureflagoverrides"></a>
# **DeleteEnvironementFeatureFlagOverrides**
> SuccessResponse DeleteEnvironementFeatureFlagOverrides ()

Delete Environment Feature Flag Overrides

Delete all environment feature flag overrides.  <div>   <code>delete:environment_feature_flags</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class DeleteEnvironementFeatureFlagOverridesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new EnvironmentsApi(config);

            try
            {
                // Delete Environment Feature Flag Overrides
                SuccessResponse result = apiInstance.DeleteEnvironementFeatureFlagOverrides();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EnvironmentsApi.DeleteEnvironementFeatureFlagOverrides: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteEnvironementFeatureFlagOverridesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Environment Feature Flag Overrides
    ApiResponse<SuccessResponse> response = apiInstance.DeleteEnvironementFeatureFlagOverridesWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling EnvironmentsApi.DeleteEnvironementFeatureFlagOverridesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
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
| **200** | Feature flag overrides deleted successfully. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deletelogo"></a>
# **DeleteLogo**
> SuccessResponse DeleteLogo (string type)

Delete logo

Delete environment logo  <div>   <code>update:environments</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class DeleteLogoExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new EnvironmentsApi(config);
            var type = dark;  // string | The type of logo to delete.

            try
            {
                // Delete logo
                SuccessResponse result = apiInstance.DeleteLogo(type);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EnvironmentsApi.DeleteLogo: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteLogoWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete logo
    ApiResponse<SuccessResponse> response = apiInstance.DeleteLogoWithHttpInfo(type);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling EnvironmentsApi.DeleteLogoWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
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
| **200** | Logo successfully deleted |  -  |
| **204** | No logo found to delete |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getenvironementfeatureflags"></a>
# **GetEnvironementFeatureFlags**
> GetEnvironmentFeatureFlagsResponse GetEnvironementFeatureFlags ()

List Environment Feature Flags

Get environment feature flags.  <div>   <code>read:environment_feature_flags</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetEnvironementFeatureFlagsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new EnvironmentsApi(config);

            try
            {
                // List Environment Feature Flags
                GetEnvironmentFeatureFlagsResponse result = apiInstance.GetEnvironementFeatureFlags();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EnvironmentsApi.GetEnvironementFeatureFlags: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetEnvironementFeatureFlagsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Environment Feature Flags
    ApiResponse<GetEnvironmentFeatureFlagsResponse> response = apiInstance.GetEnvironementFeatureFlagsWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling EnvironmentsApi.GetEnvironementFeatureFlagsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**GetEnvironmentFeatureFlagsResponse**](GetEnvironmentFeatureFlagsResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Feature flags retrieved successfully. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getenvironment"></a>
# **GetEnvironment**
> GetEnvironmentResponse GetEnvironment ()

Get environment

Gets the current environment.  <div>   <code>read:environments</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetEnvironmentExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new EnvironmentsApi(config);

            try
            {
                // Get environment
                GetEnvironmentResponse result = apiInstance.GetEnvironment();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EnvironmentsApi.GetEnvironment: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetEnvironmentWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get environment
    ApiResponse<GetEnvironmentResponse> response = apiInstance.GetEnvironmentWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling EnvironmentsApi.GetEnvironmentWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**GetEnvironmentResponse**](GetEnvironmentResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Environment successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getpasskey"></a>
# **GetPasskey**
> GetPasskey200Response GetPasskey ()

Get environment passkey settings

Retrieve passkey policy for the current environment.  <div>   <code>read:passkey</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetPasskeyExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new EnvironmentsApi(config);

            try
            {
                // Get environment passkey settings
                GetPasskey200Response result = apiInstance.GetPasskey();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EnvironmentsApi.GetPasskey: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetPasskeyWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get environment passkey settings
    ApiResponse<GetPasskey200Response> response = apiInstance.GetPasskeyWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling EnvironmentsApi.GetPasskeyWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**GetPasskey200Response**](GetPasskey200Response.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Passkey settings successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="readlogo"></a>
# **ReadLogo**
> ReadEnvLogoResponse ReadLogo ()

Read logo details

Read environment logo details  <div>   <code>read:environments</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class ReadLogoExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new EnvironmentsApi(config);

            try
            {
                // Read logo details
                ReadEnvLogoResponse result = apiInstance.ReadLogo();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EnvironmentsApi.ReadLogo: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ReadLogoWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Read logo details
    ApiResponse<ReadEnvLogoResponse> response = apiInstance.ReadLogoWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling EnvironmentsApi.ReadLogoWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**ReadEnvLogoResponse**](ReadEnvLogoResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Success |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateenvironementfeatureflagoverride"></a>
# **UpdateEnvironementFeatureFlagOverride**
> SuccessResponse UpdateEnvironementFeatureFlagOverride (string featureFlagKey, UpdateEnvironementFeatureFlagOverrideRequest updateEnvironementFeatureFlagOverrideRequest)

Update Environment Feature Flag Override

Update environment feature flag override.  <div>   <code>update:environment_feature_flags</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class UpdateEnvironementFeatureFlagOverrideExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new EnvironmentsApi(config);
            var featureFlagKey = "featureFlagKey_example";  // string | The identifier for the feature flag.
            var updateEnvironementFeatureFlagOverrideRequest = new UpdateEnvironementFeatureFlagOverrideRequest(); // UpdateEnvironementFeatureFlagOverrideRequest | Flag details.

            try
            {
                // Update Environment Feature Flag Override
                SuccessResponse result = apiInstance.UpdateEnvironementFeatureFlagOverride(featureFlagKey, updateEnvironementFeatureFlagOverrideRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EnvironmentsApi.UpdateEnvironementFeatureFlagOverride: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateEnvironementFeatureFlagOverrideWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Environment Feature Flag Override
    ApiResponse<SuccessResponse> response = apiInstance.UpdateEnvironementFeatureFlagOverrideWithHttpInfo(featureFlagKey, updateEnvironementFeatureFlagOverrideRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling EnvironmentsApi.UpdateEnvironementFeatureFlagOverrideWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **featureFlagKey** | **string** | The identifier for the feature flag. |  |
| **updateEnvironementFeatureFlagOverrideRequest** | [**UpdateEnvironementFeatureFlagOverrideRequest**](UpdateEnvironementFeatureFlagOverrideRequest.md) | Flag details. |  |

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
| **200** | Feature flag override successful |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updatepasskey"></a>
# **UpdatePasskey**
> UpdatePasskey200Response UpdatePasskey (UpdatePasskeyRequest updatePasskeyRequest)

Update environment passkey settings

Set the passkey policy for the current environment. Policies other than `off` require the `passkeys` entitlement.  <div>   <code>update:passkey</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class UpdatePasskeyExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new EnvironmentsApi(config);
            var updatePasskeyRequest = new UpdatePasskeyRequest(); // UpdatePasskeyRequest | Environment passkey settings.

            try
            {
                // Update environment passkey settings
                UpdatePasskey200Response result = apiInstance.UpdatePasskey(updatePasskeyRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EnvironmentsApi.UpdatePasskey: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdatePasskeyWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update environment passkey settings
    ApiResponse<UpdatePasskey200Response> response = apiInstance.UpdatePasskeyWithHttpInfo(updatePasskeyRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling EnvironmentsApi.UpdatePasskeyWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **updatePasskeyRequest** | [**UpdatePasskeyRequest**](UpdatePasskeyRequest.md) | Environment passkey settings. |  |

### Return type

[**UpdatePasskey200Response**](UpdatePasskey200Response.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Passkey settings successfully updated. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

