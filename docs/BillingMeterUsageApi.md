# Kinde.Api.Api.BillingMeterUsageApi

All URIs are relative to *https://your_kinde_subdomain.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateMeterUsageRecord**](BillingMeterUsageApi.md#createmeterusagerecord) | **POST** /api/v1/billing/meter_usage | Create meter usage record |

<a id="createmeterusagerecord"></a>
# **CreateMeterUsageRecord**
> CreateMeterUsageRecordResponse CreateMeterUsageRecord (CreateMeterUsageRecordRequest createMeterUsageRecordRequest)

Create meter usage record

Create a new meter usage record  <div>   <code>create:meter_usage</code> </div> 

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
    public class CreateMeterUsageRecordExample
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
            var apiInstance = new BillingMeterUsageApi(httpClient, config, httpClientHandler);
            var createMeterUsageRecordRequest = new CreateMeterUsageRecordRequest(); // CreateMeterUsageRecordRequest | Meter usage record

            try
            {
                // Create meter usage record
                CreateMeterUsageRecordResponse result = apiInstance.CreateMeterUsageRecord(createMeterUsageRecordRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BillingMeterUsageApi.CreateMeterUsageRecord: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateMeterUsageRecordWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create meter usage record
    ApiResponse<CreateMeterUsageRecordResponse> response = apiInstance.CreateMeterUsageRecordWithHttpInfo(createMeterUsageRecordRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BillingMeterUsageApi.CreateMeterUsageRecordWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createMeterUsageRecordRequest** | [**CreateMeterUsageRecordRequest**](CreateMeterUsageRecordRequest.md) | Meter usage record |  |

### Return type

[**CreateMeterUsageRecordResponse**](CreateMeterUsageRecordResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Meter usage record successfully created. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

