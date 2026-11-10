# Kinde.Api.Api.BillingOneTimePurchasesApi

All URIs are relative to *https://your_kinde_subdomain.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**GetBillingOneTimePurchases**](BillingOneTimePurchasesApi.md#getbillingonetimepurchases) | **GET** /api/v1/billing/one_time_purchases | Get billing one-time purchases |

<a id="getbillingonetimepurchases"></a>
# **GetBillingOneTimePurchases**
> GetBillingOneTimePurchasesResponse GetBillingOneTimePurchases (string customerId, int? pageSize = null, string? startingAfter = null, string? endingBefore = null, string? featureCode = null, bool? featureTotals = null)

Get billing one-time purchases

Returns the current one-time purchases a billing customer has. A purchase is current when it is paid, belongs to an active agreement, and has not expired (`expires_on` is null or in the future). Pending, failed, void, and expired purchases are omitted.  Changing plan (`POST /api/v1/billing/agreements`) or cancelling the agreement (`DELETE /api/v1/billing/agreements/{agreement_id}`) leaves those purchases on the previous agreement. They are no longer current, so they do not appear in this list.  Each item is one purchase row. Repeat buys of the same feature are separate items. Pass `feature_totals=true` for per-feature aggregates across all current purchases matching the filters, not just the current page. Metered totals include `current_billing_cycle_usage` (this billing period) and `usage_since_first_purchased` (the ingest-order lifetime total for that agreement and feature).  <div>   <code>read:billing_one_time_purchases</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetBillingOneTimePurchasesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BillingOneTimePurchasesApi(config);
            var customerId = customer_0195ac80a14c2ca2cec97d026d864de0;  // string | The ID of the billing customer to retrieve one-time purchases for
            var pageSize = 56;  // int? | Number of results per page. Defaults to 10 if parameter not sent. (optional) 
            var startingAfter = "startingAfter_example";  // string? | The ID of the one-time purchase to start after. (optional) 
            var endingBefore = "endingBefore_example";  // string? | The ID of the one-time purchase to end before. (optional) 
            var featureCode = "featureCode_example";  // string? | The feature code to filter by purchases of that feature only (optional) 
            var featureTotals = false;  // bool? | When true, include feature_totals aggregated from every current purchase that matches the filters. Defaults to false. The property is omitted when false.  (optional)  (default to false)

            try
            {
                // Get billing one-time purchases
                GetBillingOneTimePurchasesResponse result = apiInstance.GetBillingOneTimePurchases(customerId, pageSize, startingAfter, endingBefore, featureCode, featureTotals);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BillingOneTimePurchasesApi.GetBillingOneTimePurchases: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetBillingOneTimePurchasesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get billing one-time purchases
    ApiResponse<GetBillingOneTimePurchasesResponse> response = apiInstance.GetBillingOneTimePurchasesWithHttpInfo(customerId, pageSize, startingAfter, endingBefore, featureCode, featureTotals);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BillingOneTimePurchasesApi.GetBillingOneTimePurchasesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **customerId** | **string** | The ID of the billing customer to retrieve one-time purchases for |  |
| **pageSize** | **int?** | Number of results per page. Defaults to 10 if parameter not sent. | [optional]  |
| **startingAfter** | **string?** | The ID of the one-time purchase to start after. | [optional]  |
| **endingBefore** | **string?** | The ID of the one-time purchase to end before. | [optional]  |
| **featureCode** | **string?** | The feature code to filter by purchases of that feature only | [optional]  |
| **featureTotals** | **bool?** | When true, include feature_totals aggregated from every current purchase that matches the filters. Defaults to false. The property is omitted when false.  | [optional] [default to false] |

### Return type

[**GetBillingOneTimePurchasesResponse**](GetBillingOneTimePurchasesResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json; charset=utf-8, application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Billing one-time purchases successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

