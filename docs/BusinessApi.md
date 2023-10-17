# Kinde.Api.Api.BusinessApi

All URIs are relative to *https://app.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**GetBusiness**](BusinessApi.md#getbusiness) | **GET** /api/v1/business | List business details |
| [**UpdateBusiness**](BusinessApi.md#updatebusiness) | **PATCH** /api/v1/business | Update business details |

<a id="getbusiness"></a>
# **GetBusiness**
> SuccessResponse GetBusiness (string code, string name, string email, string? phone = null, string? industry = null, string? timezone = null, string? privacyUrl = null, string? termsUrl = null)

List business details

Get your business details.

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
    public class GetBusinessExample
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
            var apiInstance = new BusinessApi(httpClient, config, httpClientHandler);
            var code = "code_example";  // string | Business code.
            var name = "name_example";  // string | Business name.
            var email = "email_example";  // string | Email associated with business.
            var phone = "phone_example";  // string? | Phone number associated with business. (optional) 
            var industry = "industry_example";  // string? | The industry your business is in. (optional) 
            var timezone = "timezone_example";  // string? | The timezone your business is in. (optional) 
            var privacyUrl = "privacyUrl_example";  // string? | Your Privacy policy URL. (optional) 
            var termsUrl = "termsUrl_example";  // string? | Your Terms and Conditions URL. (optional) 

            try
            {
                // List business details
                SuccessResponse result = apiInstance.GetBusiness(code, name, email, phone, industry, timezone, privacyUrl, termsUrl);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BusinessApi.GetBusiness: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetBusinessWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List business details
    ApiResponse<SuccessResponse> response = apiInstance.GetBusinessWithHttpInfo(code, name, email, phone, industry, timezone, privacyUrl, termsUrl);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BusinessApi.GetBusinessWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **code** | **string** | Business code. |  |
| **name** | **string** | Business name. |  |
| **email** | **string** | Email associated with business. |  |
| **phone** | **string?** | Phone number associated with business. | [optional]  |
| **industry** | **string?** | The industry your business is in. | [optional]  |
| **timezone** | **string?** | The timezone your business is in. | [optional]  |
| **privacyUrl** | **string?** | Your Privacy policy URL. | [optional]  |
| **termsUrl** | **string?** | Your Terms and Conditions URL. | [optional]  |

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
| **201** | A successful response with your business details. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updatebusiness"></a>
# **UpdateBusiness**
> SuccessResponse UpdateBusiness (string businessName, string primaryEmail, string? primaryPhone = null, string? industryKey = null, string? timezoneId = null, string? privacyUrl = null, string? termsUrl = null, string? isShowKindeBranding = null, bool? isClickWrap = null, string? partnerCode = null)

Update business details

Update business details.

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
    public class UpdateBusinessExample
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
            var apiInstance = new BusinessApi(httpClient, config, httpClientHandler);
            var businessName = "businessName_example";  // string | Business name.
            var primaryEmail = "primaryEmail_example";  // string | Email associated with business.
            var primaryPhone = "primaryPhone_example";  // string? | Phone number associated with business. (optional) 
            var industryKey = "industryKey_example";  // string? | The key of the industry your business is in. (optional) 
            var timezoneId = "timezoneId_example";  // string? | The ID of the timezone your business is in. (optional) 
            var privacyUrl = "privacyUrl_example";  // string? | Your Privacy policy URL. (optional) 
            var termsUrl = "termsUrl_example";  // string? | Your Terms and Conditions URL. (optional) 
            var isShowKindeBranding = "isShowKindeBranding_example";  // string? | Display \"Powered by Kinde\" on your sign up, sign in, and subscription pages. (optional) 
            var isClickWrap = true;  // bool? | Show a policy acceptance checkbox on sign up. (optional) 
            var partnerCode = "partnerCode_example";  // string? | Your Kinde Perk code. (optional) 

            try
            {
                // Update business details
                SuccessResponse result = apiInstance.UpdateBusiness(businessName, primaryEmail, primaryPhone, industryKey, timezoneId, privacyUrl, termsUrl, isShowKindeBranding, isClickWrap, partnerCode);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BusinessApi.UpdateBusiness: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateBusinessWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update business details
    ApiResponse<SuccessResponse> response = apiInstance.UpdateBusinessWithHttpInfo(businessName, primaryEmail, primaryPhone, industryKey, timezoneId, privacyUrl, termsUrl, isShowKindeBranding, isClickWrap, partnerCode);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BusinessApi.UpdateBusinessWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **businessName** | **string** | Business name. |  |
| **primaryEmail** | **string** | Email associated with business. |  |
| **primaryPhone** | **string?** | Phone number associated with business. | [optional]  |
| **industryKey** | **string?** | The key of the industry your business is in. | [optional]  |
| **timezoneId** | **string?** | The ID of the timezone your business is in. | [optional]  |
| **privacyUrl** | **string?** | Your Privacy policy URL. | [optional]  |
| **termsUrl** | **string?** | Your Terms and Conditions URL. | [optional]  |
| **isShowKindeBranding** | **string?** | Display \&quot;Powered by Kinde\&quot; on your sign up, sign in, and subscription pages. | [optional]  |
| **isClickWrap** | **bool?** | Show a policy acceptance checkbox on sign up. | [optional]  |
| **partnerCode** | **string?** | Your Kinde Perk code. | [optional]  |

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
| **201** | Business successfully updated. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

