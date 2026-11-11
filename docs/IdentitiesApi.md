# Kinde.Api.Api.IdentitiesApi

All URIs are relative to *https://your_kinde_subdomain.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**DeleteIdentity**](IdentitiesApi.md#deleteidentity) | **DELETE** /api/v1/identities/{identity_id} | Delete identity |
| [**GetIdentities**](IdentitiesApi.md#getidentities) | **GET** /api/v1/identities | Get identities |
| [**GetIdentity**](IdentitiesApi.md#getidentity) | **GET** /api/v1/identities/{identity_id} | Get identity |
| [**UpdateIdentity**](IdentitiesApi.md#updateidentity) | **PATCH** /api/v1/identities/{identity_id} | Update identity |

<a id="deleteidentity"></a>
# **DeleteIdentity**
> SuccessResponse DeleteIdentity (string identityId)

Delete identity

Delete identity by ID.  Identities cannot be deleted from a user managed by directory sync (`USER_MANAGED_BY_DIRECTORY_SYNC`); user and role changes must be made in the upstream identity provider.  <div>   <code>delete:identities</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class DeleteIdentityExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new IdentitiesApi(config);
            var identityId = "identityId_example";  // string | The unique identifier for the identity.

            try
            {
                // Delete identity
                SuccessResponse result = apiInstance.DeleteIdentity(identityId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling IdentitiesApi.DeleteIdentity: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteIdentityWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete identity
    ApiResponse<SuccessResponse> response = apiInstance.DeleteIdentityWithHttpInfo(identityId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling IdentitiesApi.DeleteIdentityWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **identityId** | **string** | The unique identifier for the identity. |  |

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
| **200** | Identity successfully deleted. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getidentities"></a>
# **GetIdentities**
> IdentitiesResponse GetIdentities (string? email = null, string? phone = null, string? username = null, string? connectionId = null)

Get identities

Find identities by their exact value and return the user each one belongs to.  Use this to resolve a user from any of their identities, including email addresses that are not their primary one. Exactly one of `email`, `phone` or `username` must be supplied, and the value is matched in full - this endpoint does not do partial or fuzzy matching.  Email, phone and username identities are unique across the environment and are always searched. Social and enterprise identities are keyed on the ID the provider issued rather than on the email address, so they are only searched when `connection_id` names the connection to look in.  <div>   <code>read:identities</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetIdentitiesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new IdentitiesApi(config);
            var email = "email_example";  // string? | The email address to find. Matched in full, and case-insensitively. (optional) 
            var phone = "phone_example";  // string? | The phone number to find, in E.164 format. Matched in full. (optional) 
            var username = "username_example";  // string? | The username to find. Matched in full, and case-insensitively. (optional) 
            var connectionId = "connectionId_example";  // string? | Also search the social or enterprise identities on this connection for the given email. Can only be combined with `email`.  (optional) 

            try
            {
                // Get identities
                IdentitiesResponse result = apiInstance.GetIdentities(email, phone, username, connectionId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling IdentitiesApi.GetIdentities: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetIdentitiesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get identities
    ApiResponse<IdentitiesResponse> response = apiInstance.GetIdentitiesWithHttpInfo(email, phone, username, connectionId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling IdentitiesApi.GetIdentitiesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **email** | **string?** | The email address to find. Matched in full, and case-insensitively. | [optional]  |
| **phone** | **string?** | The phone number to find, in E.164 format. Matched in full. | [optional]  |
| **username** | **string?** | The username to find. Matched in full, and case-insensitively. | [optional]  |
| **connectionId** | **string?** | Also search the social or enterprise identities on this connection for the given email. Can only be combined with &#x60;email&#x60;.  | [optional]  |

### Return type

[**IdentitiesResponse**](IdentitiesResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Identities successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getidentity"></a>
# **GetIdentity**
> Identity GetIdentity (string identityId)

Get identity

Returns an identity by ID  <div>   <code>read:identities</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetIdentityExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new IdentitiesApi(config);
            var identityId = "identityId_example";  // string | The unique identifier for the identity.

            try
            {
                // Get identity
                Identity result = apiInstance.GetIdentity(identityId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling IdentitiesApi.GetIdentity: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetIdentityWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get identity
    ApiResponse<Identity> response = apiInstance.GetIdentityWithHttpInfo(identityId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling IdentitiesApi.GetIdentityWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **identityId** | **string** | The unique identifier for the identity. |  |

### Return type

[**Identity**](Identity.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json; charset=utf-8, application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Identity successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateidentity"></a>
# **UpdateIdentity**
> SuccessResponse UpdateIdentity (string identityId, UpdateIdentityRequest updateIdentityRequest)

Update identity

Update identity by ID.  Identities cannot be updated on a user managed by directory sync (`USER_MANAGED_BY_DIRECTORY_SYNC`); user and role changes must be made in the upstream identity provider.  <div>   <code>update:identities</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class UpdateIdentityExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new IdentitiesApi(config);
            var identityId = "identityId_example";  // string | The unique identifier for the identity.
            var updateIdentityRequest = new UpdateIdentityRequest(); // UpdateIdentityRequest | The fields of the identity to update.

            try
            {
                // Update identity
                SuccessResponse result = apiInstance.UpdateIdentity(identityId, updateIdentityRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling IdentitiesApi.UpdateIdentity: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateIdentityWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update identity
    ApiResponse<SuccessResponse> response = apiInstance.UpdateIdentityWithHttpInfo(identityId, updateIdentityRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling IdentitiesApi.UpdateIdentityWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **identityId** | **string** | The unique identifier for the identity. |  |
| **updateIdentityRequest** | [**UpdateIdentityRequest**](UpdateIdentityRequest.md) | The fields of the identity to update. |  |

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
| **200** | Identity successfully updated. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

