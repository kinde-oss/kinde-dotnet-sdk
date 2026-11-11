# Kinde.Api.Api.BillingAgreementsApi

All URIs are relative to *https://your_kinde_subdomain.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateBillingAgreement**](BillingAgreementsApi.md#createbillingagreement) | **POST** /api/v1/billing/agreements | Create billing agreement |
| [**DeleteBillingAgreement**](BillingAgreementsApi.md#deletebillingagreement) | **DELETE** /api/v1/billing/agreements/{agreement_id} | Cancel billing agreement |
| [**GetBillingAgreements**](BillingAgreementsApi.md#getbillingagreements) | **GET** /api/v1/billing/agreements | Get billing agreements |
| [**RevertBillingAgreementCancellation**](BillingAgreementsApi.md#revertbillingagreementcancellation) | **DELETE** /api/v1/billing/agreements/{agreement_id}/cancellation | Revert a scheduled billing agreement cancellation |

<a id="createbillingagreement"></a>
# **CreateBillingAgreement**
> SuccessResponse CreateBillingAgreement (CreateBillingAgreementRequest createBillingAgreementRequest)

Create billing agreement

Creates a new billing agreement based on the plan code passed, and cancels the customer's existing agreements. Existing agreements are cancelled even if they belong to a different billing group. The new plan must be the same customer type as the customer (organization or user).  Changing plan starts a new subscription. One-time purchases stay on the previous agreement and are no longer current. Buying the same item on the new plan is a new purchase. This does not refund what was already paid.  `plan_version` selects a published version of `plan_code`. When it is omitted, the latest version is assigned. Posting the customer's current plan_code does not change their version. `plan_version` is required to move them onto a different published version.  <div>   <code>create:billing_agreements</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class CreateBillingAgreementExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BillingAgreementsApi(config);
            var createBillingAgreementRequest = new CreateBillingAgreementRequest(); // CreateBillingAgreementRequest | New agreement request values

            try
            {
                // Create billing agreement
                SuccessResponse result = apiInstance.CreateBillingAgreement(createBillingAgreementRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BillingAgreementsApi.CreateBillingAgreement: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateBillingAgreementWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create billing agreement
    ApiResponse<SuccessResponse> response = apiInstance.CreateBillingAgreementWithHttpInfo(createBillingAgreementRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BillingAgreementsApi.CreateBillingAgreementWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createBillingAgreementRequest** | [**CreateBillingAgreementRequest**](CreateBillingAgreementRequest.md) | New agreement request values |  |

### Return type

[**SuccessResponse**](SuccessResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Billing agreement successfully changed |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deletebillingagreement"></a>
# **DeleteBillingAgreement**
> DeleteBillingAgreement200Response DeleteBillingAgreement (string agreementId, string? cancelAt = null)

Cancel billing agreement

Cancels a billing agreement. The same action as Cancel plan in the self-serve portal and on the admin billing page.  When the cancellation takes effect is decided by `cancel_at`, or, when it is omitted, by the cancellation timing of the plan version the customer agreed to:  - `immediately` expires the agreement in Kinde (sets `expires_on`)   and cancels the linked billing-provider subscription when one   exists. The response code is `AGREEMENT_DELETED`. - `min_period_end` and `max_period_end` schedule the cancellation   for the end of the earliest or the latest billing period of the   subscription's charges. The agreement stays active, and the   customer keeps their entitlements, until `cancels_on`, which the   response returns with code `AGREEMENT_CANCELLATION_SCHEDULED`.   The scheduled cancellation can be taken back with   `DELETE /api/v1/billing/agreements/{agreement_id}/cancellation`   until then. Without a provider subscription the end of the   current billing cycle is used; with no known period end, or   one that has already passed or is less than 15 minutes away,   the agreement is cancelled immediately.  A scheduled cancellation is idempotent: a further request for the same agreement without `cancel_at`, or with a deferred value, answers `AGREEMENT_CANCELLATION_SCHEDULED` with the existing `cancels_on` before any other check and runs no workflow. `cancel_at=immediately` on a scheduled agreement ends it now.  One-time purchases stay on this agreement. Once it expires they are no longer current and do not appear on GET one-time purchases; for a scheduled cancellation that happens on `cancels_on`. This does not refund what was already paid.  Proration on cancellation follows the environment billing settings for both immediate and scheduled cancellations. Invoice-now applies to immediate cancellations only: a scheduled cancellation always receives the final period invoice, usage included, at the effective date. Neither can be overridden on this request.  If a live `user:plan_cancellation_request` workflow is attached, it runs first. A deny response returns HTTP 400 with `PLAN_CANCELLATION_BLOCKED_BY_WORKFLOW` and the agreement is not cancelled.  <div>   <code>delete:billing_agreements</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class DeleteBillingAgreementExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BillingAgreementsApi(config);
            var agreementId = agreement_0195ac80a14c2ca2cec97d026d864de0;  // string | The ID of the billing agreement to cancel.
            var cancelAt = min_period_end;  // string? | When the cancellation takes effect. Defaults to the cancellation timing of the plan version the customer agreed to.  (optional) 

            try
            {
                // Cancel billing agreement
                DeleteBillingAgreement200Response result = apiInstance.DeleteBillingAgreement(agreementId, cancelAt);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BillingAgreementsApi.DeleteBillingAgreement: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteBillingAgreementWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Cancel billing agreement
    ApiResponse<DeleteBillingAgreement200Response> response = apiInstance.DeleteBillingAgreementWithHttpInfo(agreementId, cancelAt);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BillingAgreementsApi.DeleteBillingAgreementWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **agreementId** | **string** | The ID of the billing agreement to cancel. |  |
| **cancelAt** | **string?** | When the cancellation takes effect. Defaults to the cancellation timing of the plan version the customer agreed to.  | [optional]  |

### Return type

[**DeleteBillingAgreement200Response**](DeleteBillingAgreement200Response.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Billing agreement successfully cancelled, or its cancellation scheduled. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getbillingagreements"></a>
# **GetBillingAgreements**
> GetBillingAgreementsResponse GetBillingAgreements (string customerId, int? pageSize = null, string? startingAfter = null, string? endingBefore = null, string? featureCode = null)

Get billing agreements

Returns all the agreements a billing customer currently has access to  <div>   <code>read:billing_agreements</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetBillingAgreementsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BillingAgreementsApi(config);
            var customerId = customer_0195ac80a14c2ca2cec97d026d864de0;  // string | The ID of the billing customer to retrieve agreements for
            var pageSize = 56;  // int? | Number of results per page. Defaults to 10 if parameter not sent. (optional) 
            var startingAfter = "startingAfter_example";  // string? | The ID of the billing agreement to start after. (optional) 
            var endingBefore = "endingBefore_example";  // string? | The ID of the billing agreement to end before. (optional) 
            var featureCode = "featureCode_example";  // string? | The feature code to filter by agreements only containing that feature (optional) 

            try
            {
                // Get billing agreements
                GetBillingAgreementsResponse result = apiInstance.GetBillingAgreements(customerId, pageSize, startingAfter, endingBefore, featureCode);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BillingAgreementsApi.GetBillingAgreements: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetBillingAgreementsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get billing agreements
    ApiResponse<GetBillingAgreementsResponse> response = apiInstance.GetBillingAgreementsWithHttpInfo(customerId, pageSize, startingAfter, endingBefore, featureCode);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BillingAgreementsApi.GetBillingAgreementsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **customerId** | **string** | The ID of the billing customer to retrieve agreements for |  |
| **pageSize** | **int?** | Number of results per page. Defaults to 10 if parameter not sent. | [optional]  |
| **startingAfter** | **string?** | The ID of the billing agreement to start after. | [optional]  |
| **endingBefore** | **string?** | The ID of the billing agreement to end before. | [optional]  |
| **featureCode** | **string?** | The feature code to filter by agreements only containing that feature | [optional]  |

### Return type

[**GetBillingAgreementsResponse**](GetBillingAgreementsResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json; charset=utf-8, application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Billing agreements successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="revertbillingagreementcancellation"></a>
# **RevertBillingAgreementCancellation**
> SuccessResponse RevertBillingAgreementCancellation (string agreementId)

Revert a scheduled billing agreement cancellation

Takes back a scheduled cancellation of a billing agreement. The agreement stays active and the linked billing-provider subscription is no longer set to cancel. The same action as Keep plan in the self-serve portal and on the admin billing page.  Only a cancellation that has not taken effect yet can be reverted. An agreement that has already expired returns HTTP 400 with `AGREEMENT_ALREADY_CANCELLED`; one with no scheduled cancellation returns `AGREEMENT_NOT_SCHEDULED_FOR_CANCELLATION`.  <div>   <code>update:billing_agreements</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class RevertBillingAgreementCancellationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BillingAgreementsApi(config);
            var agreementId = agreement_0195ac80a14c2ca2cec97d026d864de0;  // string | The ID of the billing agreement whose scheduled cancellation to revert.

            try
            {
                // Revert a scheduled billing agreement cancellation
                SuccessResponse result = apiInstance.RevertBillingAgreementCancellation(agreementId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BillingAgreementsApi.RevertBillingAgreementCancellation: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RevertBillingAgreementCancellationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Revert a scheduled billing agreement cancellation
    ApiResponse<SuccessResponse> response = apiInstance.RevertBillingAgreementCancellationWithHttpInfo(agreementId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BillingAgreementsApi.RevertBillingAgreementCancellationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **agreementId** | **string** | The ID of the billing agreement whose scheduled cancellation to revert. |  |

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
| **200** | Scheduled cancellation successfully reverted. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

