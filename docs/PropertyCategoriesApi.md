# Kinde.Api.Api.PropertyCategoriesApi

All URIs are relative to *https://app.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateCategory**](PropertyCategoriesApi.md#createcategory) | **POST** /api/v1/property_categories | Create Category |
| [**GetCategories**](PropertyCategoriesApi.md#getcategories) | **GET** /api/v1/property_categories | List categories |
| [**UpdateCategory**](PropertyCategoriesApi.md#updatecategory) | **PUT** /api/v1/property_categories/{category_id} | Update Category |

<a id="createcategory"></a>
# **CreateCategory**
> CreateCategoryResponse CreateCategory (CreateCategoryRequest createCategoryRequest)

Create Category

Create category.

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
    public class CreateCategoryExample
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
            var apiInstance = new PropertyCategoriesApi(httpClient, config, httpClientHandler);
            var createCategoryRequest = new CreateCategoryRequest(); // CreateCategoryRequest | Category details.

            try
            {
                // Create Category
                CreateCategoryResponse result = apiInstance.CreateCategory(createCategoryRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PropertyCategoriesApi.CreateCategory: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateCategoryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Category
    ApiResponse<CreateCategoryResponse> response = apiInstance.CreateCategoryWithHttpInfo(createCategoryRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling PropertyCategoriesApi.CreateCategoryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createCategoryRequest** | [**CreateCategoryRequest**](CreateCategoryRequest.md) | Category details. |  |

### Return type

[**CreateCategoryResponse**](CreateCategoryResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | Category successfully created |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getcategories"></a>
# **GetCategories**
> GetCategoriesResponse GetCategories (int? pageSize = null, string? startingAfter = null, string? endingBefore = null, string? context = null)

List categories

Returns a list of categories. 

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
    public class GetCategoriesExample
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
            var apiInstance = new PropertyCategoriesApi(httpClient, config, httpClientHandler);
            var pageSize = 56;  // int? | Number of results per page. Defaults to 10 if parameter not sent. (optional) 
            var startingAfter = "startingAfter_example";  // string? | The ID of the category to start after. (optional) 
            var endingBefore = "endingBefore_example";  // string? | The ID of the category to end before. (optional) 
            var context = "usr";  // string? | Filter the results by User or Organization context (optional) 

            try
            {
                // List categories
                GetCategoriesResponse result = apiInstance.GetCategories(pageSize, startingAfter, endingBefore, context);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PropertyCategoriesApi.GetCategories: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetCategoriesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List categories
    ApiResponse<GetCategoriesResponse> response = apiInstance.GetCategoriesWithHttpInfo(pageSize, startingAfter, endingBefore, context);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling PropertyCategoriesApi.GetCategoriesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **pageSize** | **int?** | Number of results per page. Defaults to 10 if parameter not sent. | [optional]  |
| **startingAfter** | **string?** | The ID of the category to start after. | [optional]  |
| **endingBefore** | **string?** | The ID of the category to end before. | [optional]  |
| **context** | **string?** | Filter the results by User or Organization context | [optional]  |

### Return type

[**GetCategoriesResponse**](GetCategoriesResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json; charset=utf-8, application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Categories successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updatecategory"></a>
# **UpdateCategory**
> SuccessResponse UpdateCategory (string categoryId, UpdateCategoryRequest updateCategoryRequest)

Update Category

Update category.

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
    public class UpdateCategoryExample
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
            var apiInstance = new PropertyCategoriesApi(httpClient, config, httpClientHandler);
            var categoryId = "categoryId_example";  // string | The unique identifier for the category.
            var updateCategoryRequest = new UpdateCategoryRequest(); // UpdateCategoryRequest | The fields of the category to update.

            try
            {
                // Update Category
                SuccessResponse result = apiInstance.UpdateCategory(categoryId, updateCategoryRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PropertyCategoriesApi.UpdateCategory: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateCategoryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Category
    ApiResponse<SuccessResponse> response = apiInstance.UpdateCategoryWithHttpInfo(categoryId, updateCategoryRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling PropertyCategoriesApi.UpdateCategoryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **categoryId** | **string** | The unique identifier for the category. |  |
| **updateCategoryRequest** | [**UpdateCategoryRequest**](UpdateCategoryRequest.md) | The fields of the category to update. |  |

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
| **200** | category successfully updated. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

