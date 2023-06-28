# Kinde.Api.Api.TimezonesApi

All URIs are relative to *https://app.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**GetTimezones**](TimezonesApi.md#gettimezones) | **GET** /api/v1/timezones | List timezones and timezone IDs. |

<a name="gettimezones"></a>
# **GetTimezones**
> SuccessResponse GetTimezones (string? timezoneKey = null, string? name = null)

List timezones and timezone IDs.

Get a list of timezones and associated timezone keys.

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
    public class GetTimezonesExample
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
            var apiInstance = new TimezonesApi(httpClient, config, httpClientHandler);
            var timezoneKey = "timezoneKey_example";  // string? | Timezone Key. (optional) 
            var name = "name_example";  // string? | Timezone. (optional) 

            try
            {
                // List timezones and timezone IDs.
                SuccessResponse result = apiInstance.GetTimezones(timezoneKey, name);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling TimezonesApi.GetTimezones: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetTimezonesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List timezones and timezone IDs.
    ApiResponse<SuccessResponse> response = apiInstance.GetTimezonesWithHttpInfo(timezoneKey, name);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling TimezonesApi.GetTimezonesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **timezoneKey** | **string?** | Timezone Key. | [optional]  |
| **name** | **string?** | Timezone. | [optional]  |

### Return type

[**SuccessResponse**](SuccessResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | A successful response with a list of timezones and timezone keys. |  -  |
| **403** | Invalid credentials. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

