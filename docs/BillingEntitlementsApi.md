# Kinde.Api.Api.BillingEntitlementsApi

All URIs are relative to *https://your_kinde_subdomain.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**GetBillingEntitlements**](BillingEntitlementsApi.md#getbillingentitlements) | **GET** /api/v1/billing/entitlements | Get billing entitlements |

<a id="getbillingentitlements"></a>
# **GetBillingEntitlements**
> GetBillingEntitlementsResponse GetBillingEntitlements (string customerId, int? pageSize = null, string startingAfter = null, string endingBefore = null, string maxValue = null, string expand = null)

Get billing entitlements

Returns all the entitlements a billing customer currently has access to  <div>   <code>read:billing_entitlements</code> </div> 

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
    public class GetBillingEntitlementsExample
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
            var apiInstance = new BillingEntitlementsApi(httpClient, config, httpClientHandler);
            var customerId = "customerId_example";  // string | The ID of the billing customer to retrieve entitlements for
            var pageSize = 56;  // int? | Number of results per page. Defaults to 10 if parameter not sent. (optional) 
            var startingAfter = "startingAfter_example";  // string | The ID of the billing entitlement to start after. (optional) 
            var endingBefore = "endingBefore_example";  // string | The ID of the billing entitlement to end before. (optional) 
            var maxValue = "maxValue_example";  // string | When the maximum limit of an entitlement is null, this value is returned as the maximum limit (optional) 
            var expand = "plans";  // string | Specify additional plan data to retrieve. Use \"plans\". (optional) 

            try
            {
                // Get billing entitlements
                GetBillingEntitlementsResponse result = apiInstance.GetBillingEntitlements(customerId, pageSize, startingAfter, endingBefore, maxValue, expand);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BillingEntitlementsApi.GetBillingEntitlements: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetBillingEntitlementsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get billing entitlements
    ApiResponse<GetBillingEntitlementsResponse> response = apiInstance.GetBillingEntitlementsWithHttpInfo(customerId, pageSize, startingAfter, endingBefore, maxValue, expand);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BillingEntitlementsApi.GetBillingEntitlementsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **customerId** | **string** | The ID of the billing customer to retrieve entitlements for |  |
| **pageSize** | **int?** | Number of results per page. Defaults to 10 if parameter not sent. | [optional]  |
| **startingAfter** | **string** | The ID of the billing entitlement to start after. | [optional]  |
| **endingBefore** | **string** | The ID of the billing entitlement to end before. | [optional]  |
| **maxValue** | **string** | When the maximum limit of an entitlement is null, this value is returned as the maximum limit | [optional]  |
| **expand** | **string** | Specify additional plan data to retrieve. Use \&quot;plans\&quot;. | [optional]  |

### Return type

[**GetBillingEntitlementsResponse**](GetBillingEntitlementsResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json; charset=utf-8, application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Billing entitlements successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

