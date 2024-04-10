# Kinde.Api.Api.ApplicationsApi

All URIs are relative to *https://app.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateApplication**](ApplicationsApi.md#createapplication) | **POST** /api/v1/applications | Create Application |
| [**DeleteApplication**](ApplicationsApi.md#deleteapplication) | **DELETE** /api/v1/applications/{application_id} | Delete Application |
| [**GetApplication**](ApplicationsApi.md#getapplication) | **GET** /api/v1/applications/{application_id} | Get Application |
| [**GetApplications**](ApplicationsApi.md#getapplications) | **GET** /api/v1/applications | List Applications |
| [**UpdateApplication**](ApplicationsApi.md#updateapplication) | **PATCH** /api/v1/applications/{application_id} | Update Application |

<a id="createapplication"></a>
# **CreateApplication**
> CreateApplicationResponse CreateApplication (CreateApplicationRequest? createApplicationRequest = null)

Create Application

Create an application.

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
    public class CreateApplicationExample
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
            var apiInstance = new ApplicationsApi(httpClient, config, httpClientHandler);
            var createApplicationRequest = new CreateApplicationRequest?(); // CreateApplicationRequest? | Application details. (optional) 

            try
            {
                // Create Application
                CreateApplicationResponse result = apiInstance.CreateApplication(createApplicationRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.CreateApplication: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateApplicationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Application
    ApiResponse<CreateApplicationResponse> response = apiInstance.CreateApplicationWithHttpInfo(createApplicationRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.CreateApplicationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createApplicationRequest** | [**CreateApplicationRequest?**](CreateApplicationRequest?.md) | Application details. | [optional]  |

### Return type

[**CreateApplicationResponse**](CreateApplicationResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Application successfully created. |  -  |
| **400** | Error creating user. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteapplication"></a>
# **DeleteApplication**
> SuccessResponse DeleteApplication (string applicationId)

Delete Application

Delete application. 

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
    public class DeleteApplicationExample
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
            var apiInstance = new ApplicationsApi(httpClient, config, httpClientHandler);
            var applicationId = "applicationId_example";  // string | The identifier for the application.

            try
            {
                // Delete Application
                SuccessResponse result = apiInstance.DeleteApplication(applicationId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.DeleteApplication: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteApplicationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Application
    ApiResponse<SuccessResponse> response = apiInstance.DeleteApplicationWithHttpInfo(applicationId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.DeleteApplicationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationId** | **string** | The identifier for the application. |  |

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
| **200** | Application successfully deleted. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getapplication"></a>
# **GetApplication**
> GetApplicationResponse GetApplication (string applicationId)

Get Application

Gets an application given the application's id. 

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
    public class GetApplicationExample
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
            var apiInstance = new ApplicationsApi(httpClient, config, httpClientHandler);
            var applicationId = "applicationId_example";  // string | The identifier for the application.

            try
            {
                // Get Application
                GetApplicationResponse result = apiInstance.GetApplication(applicationId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.GetApplication: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetApplicationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Application
    ApiResponse<GetApplicationResponse> response = apiInstance.GetApplicationWithHttpInfo(applicationId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.GetApplicationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationId** | **string** | The identifier for the application. |  |

### Return type

[**GetApplicationResponse**](GetApplicationResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Application successfully retrieved. |  -  |
| **400** | Bad request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getapplications"></a>
# **GetApplications**
> GetApplicationsResponse GetApplications (string? sort = null, int? pageSize = null, string? nextToken = null)

List Applications

Get a list of applications. 

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
    public class GetApplicationsExample
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
            var apiInstance = new ApplicationsApi(httpClient, config, httpClientHandler);
            var sort = "name_asc";  // string? | Field and order to sort the result by. (optional) 
            var pageSize = 56;  // int? | Number of results per page. Defaults to 10 if parameter not sent. (optional) 
            var nextToken = "nextToken_example";  // string? | A string to get the next page of results if there are more results. (optional) 

            try
            {
                // List Applications
                GetApplicationsResponse result = apiInstance.GetApplications(sort, pageSize, nextToken);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.GetApplications: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetApplicationsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Applications
    ApiResponse<GetApplicationsResponse> response = apiInstance.GetApplicationsWithHttpInfo(sort, pageSize, nextToken);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.GetApplicationsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **sort** | **string?** | Field and order to sort the result by. | [optional]  |
| **pageSize** | **int?** | Number of results per page. Defaults to 10 if parameter not sent. | [optional]  |
| **nextToken** | **string?** | A string to get the next page of results if there are more results. | [optional]  |

### Return type

[**GetApplicationsResponse**](GetApplicationsResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A successful response with a list of applications or an empty list. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateapplication"></a>
# **UpdateApplication**
> void UpdateApplication (string applicationId, UpdateApplicationRequest? updateApplicationRequest = null)

Update Application

Update an application.

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
    public class UpdateApplicationExample
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
            var apiInstance = new ApplicationsApi(httpClient, config, httpClientHandler);
            var applicationId = "applicationId_example";  // string | The identifier for the application.
            var updateApplicationRequest = new UpdateApplicationRequest?(); // UpdateApplicationRequest? | Application details. (optional) 

            try
            {
                // Update Application
                apiInstance.UpdateApplication(applicationId, updateApplicationRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.UpdateApplication: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateApplicationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Application
    apiInstance.UpdateApplicationWithHttpInfo(applicationId, updateApplicationRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.UpdateApplicationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationId** | **string** | The identifier for the application. |  |
| **updateApplicationRequest** | [**UpdateApplicationRequest?**](UpdateApplicationRequest?.md) | Application details. | [optional]  |

### Return type

void (empty response body)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Application successfully updated. |  -  |
| **400** | Bad request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

