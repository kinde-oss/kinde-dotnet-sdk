# Kinde.Api.Api.BillingMeterUsageApi

All URIs are relative to *https://your_kinde_subdomain.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateMeterUsageRecord**](BillingMeterUsageApi.md#createmeterusagerecord) | **POST** /api/v1/billing/meter_usage | Create meter usage record |
| [**GetBillingMeterUsage**](BillingMeterUsageApi.md#getbillingmeterusage) | **GET** /api/v1/billing/meter_usage | Get billing meter usage |

<a id="createmeterusagerecord"></a>
# **CreateMeterUsageRecord**
> CreateMeterUsageRecordResponse CreateMeterUsageRecord (CreateMeterUsageRecordRequest createMeterUsageRecordRequest)

Create meter usage record

Create a new meter usage record against a billing agreement and feature.  Metered one-time features can receive usage after the customer has completed their first purchase of that feature.  `meter_type_code` chooses how this record updates the running total. The caller is responsible for sending the correct type and value. `meter_usage_timestamp` is stored on the record (and, for plan-metered entitlements, sent to the billing provider). It does not choose a billing cycle, does not insert the record in timestamp order, and does not change the running-total arithmetic.  Running-total rules:  - `absolute` (default): replaces the running total with   `meter_value`. - `delta`: adds `meter_value` to the last calculated running   total.  Which running total is updated:  - Plan-metered entitlements, and one-time features that expire   at the end of the current billing cycle: the current   billing cycle total. A new cycle starts at 0. - One-time features that never expire: a lifetime total for   that agreement and feature. Every accepted record updates   it, including records whose `meter_usage_timestamp` is   before the first paid purchase. This total does not reset   when the billing cycle rolls.  The response includes the created record `id` and a grant snapshot (`units_granted`, `current_usage`, and `expiration_policy_code` when the grant is a one-time purchase) so the caller can tell whether usage has exceeded the grant without a follow-up GET. `current_usage` is that grant-relative running total after this record. Grant fields are omitted when there is no current grant. `units_granted` is also omitted when the grant is unlimited; an unlimited one-time grant still returns `expiration_policy_code`.  <div>   <code>create:meter_usage</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
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

            var apiInstance = new BillingMeterUsageApi(config);
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

<a id="getbillingmeterusage"></a>
# **GetBillingMeterUsage**
> GetBillingMeterUsageResponse GetBillingMeterUsage (string customerAgreementId, string featureCode, int? pageSize = null, string? startingAfter = null, string? endingBefore = null, bool? allCycles = null)

Get billing meter usage

Returns meter usage records for one billing agreement and feature.  By default only records in the customer's current billing cycle are returned. Pass `all_cycles=true` to page every record for that meter.  The response includes the same grant snapshot as POST (`units_granted`, `current_usage`, and `expiration_policy_code` when the grant is a one-time purchase). `current_usage` is the grant-relative running total after every accepted record — not the page sum. For never-expires one-time features that is the lifetime total (ingest-order absolute/delta). For cycle-end one-time features and plan-metered entitlements that is the current billing cycle total. Grant fields are omitted when there is no current grant. `units_granted` is also omitted when the grant is unlimited; an unlimited one-time grant still returns `expiration_policy_code`. `customer_agreement_id` and `feature_code` are on the response, not repeated on each record.  <div>   <code>read:meter_usage</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetBillingMeterUsageExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BillingMeterUsageApi(config);
            var customerAgreementId = agreement_0195ac80a14c2ca2cec97d026d864de0;  // string | The billing agreement to retrieve meter usage for
            var featureCode = extra_api_calls;  // string | The feature code to retrieve meter usage for
            var pageSize = 500;  // int? | Number of results per page. Defaults to the API page size limit if parameter not sent. Must be between 1 and that limit.  (optional)  (default to 500)
            var startingAfter = "startingAfter_example";  // string? | The ID of the meter usage record to start after. (optional) 
            var endingBefore = "endingBefore_example";  // string? | The ID of the meter usage record to end before. (optional) 
            var allCycles = false;  // bool? | When true, include usage records from every billing cycle. Defaults to false (current cycle only). The grant snapshot stays grant-relative and is not the listed page.  (optional)  (default to false)

            try
            {
                // Get billing meter usage
                GetBillingMeterUsageResponse result = apiInstance.GetBillingMeterUsage(customerAgreementId, featureCode, pageSize, startingAfter, endingBefore, allCycles);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BillingMeterUsageApi.GetBillingMeterUsage: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetBillingMeterUsageWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get billing meter usage
    ApiResponse<GetBillingMeterUsageResponse> response = apiInstance.GetBillingMeterUsageWithHttpInfo(customerAgreementId, featureCode, pageSize, startingAfter, endingBefore, allCycles);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BillingMeterUsageApi.GetBillingMeterUsageWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **customerAgreementId** | **string** | The billing agreement to retrieve meter usage for |  |
| **featureCode** | **string** | The feature code to retrieve meter usage for |  |
| **pageSize** | **int?** | Number of results per page. Defaults to the API page size limit if parameter not sent. Must be between 1 and that limit.  | [optional] [default to 500] |
| **startingAfter** | **string?** | The ID of the meter usage record to start after. | [optional]  |
| **endingBefore** | **string?** | The ID of the meter usage record to end before. | [optional]  |
| **allCycles** | **bool?** | When true, include usage records from every billing cycle. Defaults to false (current cycle only). The grant snapshot stays grant-relative and is not the listed page.  | [optional] [default to false] |

### Return type

[**GetBillingMeterUsageResponse**](GetBillingMeterUsageResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json; charset=utf-8, application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Billing meter usage successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

