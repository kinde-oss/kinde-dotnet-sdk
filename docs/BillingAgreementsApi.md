# Kinde.Api.Api.BillingAgreementsApi

All URIs are relative to *https://your_kinde_subdomain.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateBillingAgreement**](BillingAgreementsApi.md#createbillingagreement) | **POST** /api/v1/billing/agreements | Create billing agreement |
| [**GetBillingAgreements**](BillingAgreementsApi.md#getbillingagreements) | **GET** /api/v1/billing/agreements | Get billing agreements |

<a id="createbillingagreement"></a>
# **CreateBillingAgreement**
> SuccessResponse CreateBillingAgreement (CreateBillingAgreementRequest createBillingAgreementRequest)

Create billing agreement

Creates a new billing agreement based on the plan code passed, and cancels the customer's existing agreements  <div>   <code>create:billing_agreements</code> </div> 

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
    public class CreateBillingAgreementExample
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
            var apiInstance = new BillingAgreementsApi(httpClient, config, httpClientHandler);
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

<a id="getbillingagreements"></a>
# **GetBillingAgreements**
> GetBillingAgreementsResponse GetBillingAgreements (string customerId, int? pageSize = null, string startingAfter = null, string endingBefore = null, string featureCode = null)

Get billing agreements

Returns all the agreements a billing customer currently has access to  <div>   <code>read:billing_agreements</code> </div> 

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
    public class GetBillingAgreementsExample
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
            var apiInstance = new BillingAgreementsApi(httpClient, config, httpClientHandler);
            var customerId = customer_0195ac80a14c2ca2cec97d026d864de0;  // string | The ID of the billing customer to retrieve agreements for
            var pageSize = 56;  // int? | Number of results per page. Defaults to 10 if parameter not sent. (optional) 
            var startingAfter = "startingAfter_example";  // string | The ID of the billing agreement to start after. (optional) 
            var endingBefore = "endingBefore_example";  // string | The ID of the billing agreement to end before. (optional) 
            var featureCode = "featureCode_example";  // string | The feature code to filter by agreements only containing that feature (optional) 

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
| **startingAfter** | **string** | The ID of the billing agreement to start after. | [optional]  |
| **endingBefore** | **string** | The ID of the billing agreement to end before. | [optional]  |
| **featureCode** | **string** | The feature code to filter by agreements only containing that feature | [optional]  |

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

