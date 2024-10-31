# Kinde.Api.Api.EnvironmentVariablesApi

All URIs are relative to *https://your_kinde_subdomain.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateEnvironmentVariable**](EnvironmentVariablesApi.md#createenvironmentvariable) | **POST** /api/v1/environment_variables | Create environment variable |
| [**DeleteEnvironmentVariable**](EnvironmentVariablesApi.md#deleteenvironmentvariable) | **DELETE** /api/v1/environment_variables/{variable_id} | Delete environment variable |
| [**GetEnvironmentVariable**](EnvironmentVariablesApi.md#getenvironmentvariable) | **GET** /api/v1/environment_variables/{variable_id} | Get environment variable |
| [**GetEnvironmentVariables**](EnvironmentVariablesApi.md#getenvironmentvariables) | **GET** /api/v1/environment_variables | Get environment variables |
| [**UpdateEnvironmentVariable**](EnvironmentVariablesApi.md#updateenvironmentvariable) | **PATCH** /api/v1/environment_variables/{variable_id} | Update environment variable |

<a id="createenvironmentvariable"></a>
# **CreateEnvironmentVariable**
> CreateEnvironmentVariableResponse CreateEnvironmentVariable (CreateEnvironmentVariableRequest createEnvironmentVariableRequest)

Create environment variable

Create a new environment variable. This feature is in beta and admin UI is not yet available.  <div>   <code>create:environment_variables</code> </div> 

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
    public class CreateEnvironmentVariableExample
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
            var apiInstance = new EnvironmentVariablesApi(httpClient, config, httpClientHandler);
            var createEnvironmentVariableRequest = new CreateEnvironmentVariableRequest(); // CreateEnvironmentVariableRequest | The environment variable details.

            try
            {
                // Create environment variable
                CreateEnvironmentVariableResponse result = apiInstance.CreateEnvironmentVariable(createEnvironmentVariableRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EnvironmentVariablesApi.CreateEnvironmentVariable: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateEnvironmentVariableWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create environment variable
    ApiResponse<CreateEnvironmentVariableResponse> response = apiInstance.CreateEnvironmentVariableWithHttpInfo(createEnvironmentVariableRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling EnvironmentVariablesApi.CreateEnvironmentVariableWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createEnvironmentVariableRequest** | [**CreateEnvironmentVariableRequest**](CreateEnvironmentVariableRequest.md) | The environment variable details. |  |

### Return type

[**CreateEnvironmentVariableResponse**](CreateEnvironmentVariableResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | Environment variable successfully created. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteenvironmentvariable"></a>
# **DeleteEnvironmentVariable**
> DeleteEnvironmentVariableResponse DeleteEnvironmentVariable (string variableId)

Delete environment variable

Delete an environment variable you previously created. This feature is in beta and admin UI is not yet available.  <div>   <code>delete:environment_variables</code> </div> 

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
    public class DeleteEnvironmentVariableExample
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
            var apiInstance = new EnvironmentVariablesApi(httpClient, config, httpClientHandler);
            var variableId = env_var_0192b1941f125645fa15bf28a662a0b3;  // string | The environment variable's ID.

            try
            {
                // Delete environment variable
                DeleteEnvironmentVariableResponse result = apiInstance.DeleteEnvironmentVariable(variableId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EnvironmentVariablesApi.DeleteEnvironmentVariable: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteEnvironmentVariableWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete environment variable
    ApiResponse<DeleteEnvironmentVariableResponse> response = apiInstance.DeleteEnvironmentVariableWithHttpInfo(variableId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling EnvironmentVariablesApi.DeleteEnvironmentVariableWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **variableId** | **string** | The environment variable&#39;s ID. |  |

### Return type

[**DeleteEnvironmentVariableResponse**](DeleteEnvironmentVariableResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Environment variable successfully deleted. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getenvironmentvariable"></a>
# **GetEnvironmentVariable**
> GetEnvironmentVariableResponse GetEnvironmentVariable (string variableId)

Get environment variable

Retrieve environment variable details by ID. This feature is in beta and admin UI is not yet available.  <div>   <code>read:environment_variables</code> </div> 

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
    public class GetEnvironmentVariableExample
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
            var apiInstance = new EnvironmentVariablesApi(httpClient, config, httpClientHandler);
            var variableId = env_var_0192b1941f125645fa15bf28a662a0b3;  // string | The environment variable's ID.

            try
            {
                // Get environment variable
                GetEnvironmentVariableResponse result = apiInstance.GetEnvironmentVariable(variableId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EnvironmentVariablesApi.GetEnvironmentVariable: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetEnvironmentVariableWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get environment variable
    ApiResponse<GetEnvironmentVariableResponse> response = apiInstance.GetEnvironmentVariableWithHttpInfo(variableId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling EnvironmentVariablesApi.GetEnvironmentVariableWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **variableId** | **string** | The environment variable&#39;s ID. |  |

### Return type

[**GetEnvironmentVariableResponse**](GetEnvironmentVariableResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Environment variable successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getenvironmentvariables"></a>
# **GetEnvironmentVariables**
> GetEnvironmentVariablesResponse GetEnvironmentVariables ()

Get environment variables

Get environment variables. This feature is in beta and admin UI is not yet available.  <div>   <code>read:environment_variables</code> </div> 

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
    public class GetEnvironmentVariablesExample
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
            var apiInstance = new EnvironmentVariablesApi(httpClient, config, httpClientHandler);

            try
            {
                // Get environment variables
                GetEnvironmentVariablesResponse result = apiInstance.GetEnvironmentVariables();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EnvironmentVariablesApi.GetEnvironmentVariables: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetEnvironmentVariablesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get environment variables
    ApiResponse<GetEnvironmentVariablesResponse> response = apiInstance.GetEnvironmentVariablesWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling EnvironmentVariablesApi.GetEnvironmentVariablesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**GetEnvironmentVariablesResponse**](GetEnvironmentVariablesResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A successful response with a list of environment variables or an empty list. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateenvironmentvariable"></a>
# **UpdateEnvironmentVariable**
> UpdateEnvironmentVariableResponse UpdateEnvironmentVariable (string variableId, UpdateEnvironmentVariableRequest updateEnvironmentVariableRequest)

Update environment variable

Update an environment variable you previously created. This feature is in beta and admin UI is not yet available.  <div>   <code>update:environment_variables</code> </div> 

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
    public class UpdateEnvironmentVariableExample
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
            var apiInstance = new EnvironmentVariablesApi(httpClient, config, httpClientHandler);
            var variableId = env_var_0192b1941f125645fa15bf28a662a0b3;  // string | The environment variable's ID.
            var updateEnvironmentVariableRequest = new UpdateEnvironmentVariableRequest(); // UpdateEnvironmentVariableRequest | The new details for the environment variable

            try
            {
                // Update environment variable
                UpdateEnvironmentVariableResponse result = apiInstance.UpdateEnvironmentVariable(variableId, updateEnvironmentVariableRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EnvironmentVariablesApi.UpdateEnvironmentVariable: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateEnvironmentVariableWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update environment variable
    ApiResponse<UpdateEnvironmentVariableResponse> response = apiInstance.UpdateEnvironmentVariableWithHttpInfo(variableId, updateEnvironmentVariableRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling EnvironmentVariablesApi.UpdateEnvironmentVariableWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **variableId** | **string** | The environment variable&#39;s ID. |  |
| **updateEnvironmentVariableRequest** | [**UpdateEnvironmentVariableRequest**](UpdateEnvironmentVariableRequest.md) | The new details for the environment variable |  |

### Return type

[**UpdateEnvironmentVariableResponse**](UpdateEnvironmentVariableResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Environment variable successfully updated. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

