# Kinde.Api.Api.DirectoriesApi

All URIs are relative to *https://your_kinde_subdomain.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateDirectory**](DirectoriesApi.md#createdirectory) | **POST** /api/v1/directories | Create SCIM directory |
| [**DeleteDirectory**](DirectoriesApi.md#deletedirectory) | **DELETE** /api/v1/directories/{directory_id} | Delete SCIM directory |
| [**GetDirectories**](DirectoriesApi.md#getdirectories) | **GET** /api/v1/directories | Get SCIM directories |
| [**GetDirectory**](DirectoriesApi.md#getdirectory) | **GET** /api/v1/directories/{directory_id} | Get SCIM directory |
| [**RotateDirectorySecret**](DirectoriesApi.md#rotatedirectorysecret) | **POST** /api/v1/directories/{directory_id}/rotate_secret | Rotate SCIM directory secret token |
| [**UpdateDirectory**](DirectoriesApi.md#updatedirectory) | **PATCH** /api/v1/directories/{directory_id} | Update SCIM directory |

<a id="createdirectory"></a>
# **CreateDirectory**
> CreateDirectoryResponse CreateDirectory (CreateDirectoryRequest createDirectoryRequest)

Create SCIM directory

Create a new SCIM directory for user and group synchronization.  <div>   <code>create:scim_directories</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class CreateDirectoryExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DirectoriesApi(config);
            var createDirectoryRequest = new CreateDirectoryRequest(); // CreateDirectoryRequest | 

            try
            {
                // Create SCIM directory
                CreateDirectoryResponse result = apiInstance.CreateDirectory(createDirectoryRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DirectoriesApi.CreateDirectory: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateDirectoryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create SCIM directory
    ApiResponse<CreateDirectoryResponse> response = apiInstance.CreateDirectoryWithHttpInfo(createDirectoryRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DirectoriesApi.CreateDirectoryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createDirectoryRequest** | [**CreateDirectoryRequest**](CreateDirectoryRequest.md) |  |  |

### Return type

[**CreateDirectoryResponse**](CreateDirectoryResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | SCIM directory successfully created. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials, or the business plan does not allow another SCIM directory.  |  -  |
| **409** | Conflict - Directory already exists. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deletedirectory"></a>
# **DeleteDirectory**
> DeleteDirectoryResponse DeleteDirectory (string directoryId)

Delete SCIM directory

Delete a SCIM directory and all associated data.  <div>   <code>delete:scim_directories</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class DeleteDirectoryExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DirectoriesApi(config);
            var directoryId = directory_0192b1941f125645fa15bf28a662a0b3;  // string | The directory's ID.

            try
            {
                // Delete SCIM directory
                DeleteDirectoryResponse result = apiInstance.DeleteDirectory(directoryId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DirectoriesApi.DeleteDirectory: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteDirectoryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete SCIM directory
    ApiResponse<DeleteDirectoryResponse> response = apiInstance.DeleteDirectoryWithHttpInfo(directoryId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DirectoriesApi.DeleteDirectoryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **directoryId** | **string** | The directory&#39;s ID. |  |

### Return type

[**DeleteDirectoryResponse**](DeleteDirectoryResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | SCIM directory successfully deleted. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **404** | Directory not found. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getdirectories"></a>
# **GetDirectories**
> GetDirectoriesResponse GetDirectories (int? pageSize = null, string? startingAfter = null, string? organizationCode = null)

Get SCIM directories

Returns a list of SCIM directories for your organization.  <div>   <code>read:scim_directories</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetDirectoriesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DirectoriesApi(config);
            var pageSize = 56;  // int? | Number of results per page. Defaults to 50 if parameter not sent. (optional) 
            var startingAfter = "startingAfter_example";  // string? | The ID of the directory to start after. (optional) 
            var organizationCode = "organizationCode_example";  // string? | Filter by organization code to get directories for a specific organization. (optional) 

            try
            {
                // Get SCIM directories
                GetDirectoriesResponse result = apiInstance.GetDirectories(pageSize, startingAfter, organizationCode);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DirectoriesApi.GetDirectories: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetDirectoriesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get SCIM directories
    ApiResponse<GetDirectoriesResponse> response = apiInstance.GetDirectoriesWithHttpInfo(pageSize, startingAfter, organizationCode);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DirectoriesApi.GetDirectoriesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **pageSize** | **int?** | Number of results per page. Defaults to 50 if parameter not sent. | [optional]  |
| **startingAfter** | **string?** | The ID of the directory to start after. | [optional]  |
| **organizationCode** | **string?** | Filter by organization code to get directories for a specific organization. | [optional]  |

### Return type

[**GetDirectoriesResponse**](GetDirectoriesResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | SCIM directories successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getdirectory"></a>
# **GetDirectory**
> GetDirectoryResponse GetDirectory (string directoryId)

Get SCIM directory

Retrieve SCIM directory details by ID.  <div>   <code>read:scim_directories</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetDirectoryExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DirectoriesApi(config);
            var directoryId = directory_0192b1941f125645fa15bf28a662a0b3;  // string | The directory's ID.

            try
            {
                // Get SCIM directory
                GetDirectoryResponse result = apiInstance.GetDirectory(directoryId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DirectoriesApi.GetDirectory: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetDirectoryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get SCIM directory
    ApiResponse<GetDirectoryResponse> response = apiInstance.GetDirectoryWithHttpInfo(directoryId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DirectoriesApi.GetDirectoryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **directoryId** | **string** | The directory&#39;s ID. |  |

### Return type

[**GetDirectoryResponse**](GetDirectoryResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | SCIM directory successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **404** | Directory not found. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="rotatedirectorysecret"></a>
# **RotateDirectorySecret**
> RotateDirectorySecretResponse RotateDirectorySecret (string directoryId)

Rotate SCIM directory secret token

Rotate the SCIM directory bearer token. The previous token stops working immediately.  <div>   <code>update:scim_directories</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class RotateDirectorySecretExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DirectoriesApi(config);
            var directoryId = directory_0192b1941f125645fa15bf28a662a0b3;  // string | The directory's ID.

            try
            {
                // Rotate SCIM directory secret token
                RotateDirectorySecretResponse result = apiInstance.RotateDirectorySecret(directoryId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DirectoriesApi.RotateDirectorySecret: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RotateDirectorySecretWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Rotate SCIM directory secret token
    ApiResponse<RotateDirectorySecretResponse> response = apiInstance.RotateDirectorySecretWithHttpInfo(directoryId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DirectoriesApi.RotateDirectorySecretWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **directoryId** | **string** | The directory&#39;s ID. |  |

### Return type

[**RotateDirectorySecretResponse**](RotateDirectorySecretResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | SCIM directory secret token successfully rotated. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **404** | Directory not found. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updatedirectory"></a>
# **UpdateDirectory**
> UpdateDirectoryResponse UpdateDirectory (string directoryId, UpdateDirectoryRequest updateDirectoryRequest)

Update SCIM directory

Update SCIM directory configuration.  <div>   <code>update:scim_directories</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class UpdateDirectoryExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DirectoriesApi(config);
            var directoryId = directory_0192b1941f125645fa15bf28a662a0b3;  // string | The directory's ID.
            var updateDirectoryRequest = new UpdateDirectoryRequest(); // UpdateDirectoryRequest | 

            try
            {
                // Update SCIM directory
                UpdateDirectoryResponse result = apiInstance.UpdateDirectory(directoryId, updateDirectoryRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DirectoriesApi.UpdateDirectory: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateDirectoryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update SCIM directory
    ApiResponse<UpdateDirectoryResponse> response = apiInstance.UpdateDirectoryWithHttpInfo(directoryId, updateDirectoryRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DirectoriesApi.UpdateDirectoryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **directoryId** | **string** | The directory&#39;s ID. |  |
| **updateDirectoryRequest** | [**UpdateDirectoryRequest**](UpdateDirectoryRequest.md) |  |  |

### Return type

[**UpdateDirectoryResponse**](UpdateDirectoryResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | SCIM directory successfully updated. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **404** | Directory not found. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

