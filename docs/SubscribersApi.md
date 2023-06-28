# Kinde.Api.Api.SubscribersApi

All URIs are relative to *https://app.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateSubscriber**](SubscribersApi.md#createsubscriber) | **POST** /api/v1/subscribers | Create Subscriber |
| [**GetSubscriber**](SubscribersApi.md#getsubscriber) | **GET** /api/v1/subscribers/{subscriber_id} | Get Subscriber |
| [**GetSubscribers**](SubscribersApi.md#getsubscribers) | **GET** /api/v1/subscribers | List Subscribers |

<a name="createsubscriber"></a>
# **CreateSubscriber**
> CreateSubscriberSuccessResponse CreateSubscriber (string firstName, string lastName, string email)

Create Subscriber

Create subscriber.

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
    public class CreateSubscriberExample
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
            var apiInstance = new SubscribersApi(httpClient, config, httpClientHandler);
            var firstName = "firstName_example";  // string | Subscriber's first name.
            var lastName = "lastName_example";  // string | Subscriber's last name.
            var email = "email_example";  // string | The email address of the subscriber.

            try
            {
                // Create Subscriber
                CreateSubscriberSuccessResponse result = apiInstance.CreateSubscriber(firstName, lastName, email);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SubscribersApi.CreateSubscriber: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateSubscriberWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Subscriber
    ApiResponse<CreateSubscriberSuccessResponse> response = apiInstance.CreateSubscriberWithHttpInfo(firstName, lastName, email);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SubscribersApi.CreateSubscriberWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **firstName** | **string** | Subscriber&#39;s first name. |  |
| **lastName** | **string** | Subscriber&#39;s last name. |  |
| **email** | **string** | The email address of the subscriber. |  |

### Return type

[**CreateSubscriberSuccessResponse**](CreateSubscriberSuccessResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | Subscriber successfully created |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getsubscriber"></a>
# **GetSubscriber**
> GetSubscriberResponse GetSubscriber (string subscriberId)

Get Subscriber

Retrieve a subscriber record. 

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
    public class GetSubscriberExample
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
            var apiInstance = new SubscribersApi(httpClient, config, httpClientHandler);
            var subscriberId = "subscriberId_example";  // string | The subscriber's id.

            try
            {
                // Get Subscriber
                GetSubscriberResponse result = apiInstance.GetSubscriber(subscriberId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SubscribersApi.GetSubscriber: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetSubscriberWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Subscriber
    ApiResponse<GetSubscriberResponse> response = apiInstance.GetSubscriberWithHttpInfo(subscriberId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SubscribersApi.GetSubscriberWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **subscriberId** | **string** | The subscriber&#39;s id. |  |

### Return type

[**GetSubscriberResponse**](GetSubscriberResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Subscriber successfully retrieved. |  -  |
| **400** | Bad request. |  -  |
| **403** | Invalid credentials. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getsubscribers"></a>
# **GetSubscribers**
> GetSubscribersResponse GetSubscribers (string? sort = null, int? pageSize = null, string? nextToken = null)

List Subscribers

The returned list can be sorted by full name or email address in ascending or descending order. The number of records to return at a time can also be controlled using the `page_size` query string parameter. 

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
    public class GetSubscribersExample
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
            var apiInstance = new SubscribersApi(httpClient, config, httpClientHandler);
            var sort = "name_asc";  // string? | Field and order to sort the result by. (optional) 
            var pageSize = 56;  // int? | Number of results per page. Defaults to 10 if parameter not sent. (optional) 
            var nextToken = "nextToken_example";  // string? | A string to get the next page of results if there are more results. (optional) 

            try
            {
                // List Subscribers
                GetSubscribersResponse result = apiInstance.GetSubscribers(sort, pageSize, nextToken);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SubscribersApi.GetSubscribers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetSubscribersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Subscribers
    ApiResponse<GetSubscribersResponse> response = apiInstance.GetSubscribersWithHttpInfo(sort, pageSize, nextToken);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SubscribersApi.GetSubscribersWithHttpInfo: " + e.Message);
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

[**GetSubscribersResponse**](GetSubscribersResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Subscriber successfully retrieved. |  -  |
| **403** | Bad request. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

