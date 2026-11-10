# Kinde.Api.Api.UsersApi

All URIs are relative to *https://your_kinde_subdomain.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateUser**](UsersApi.md#createuser) | **POST** /api/v1/user | Create user |
| [**CreateUserBillingCustomer**](UsersApi.md#createuserbillingcustomer) | **POST** /api/v1/users/{user_id}/billing_customer | Create user billing customer |
| [**CreateUserIdentity**](UsersApi.md#createuseridentity) | **POST** /api/v1/users/{user_id}/identities | Create identity |
| [**DeleteUser**](UsersApi.md#deleteuser) | **DELETE** /api/v1/user | Delete user |
| [**DeleteUserSessions**](UsersApi.md#deleteusersessions) | **DELETE** /api/v1/users/{user_id}/sessions | Delete user sessions |
| [**GetUserData**](UsersApi.md#getuserdata) | **GET** /api/v1/user | Get user |
| [**GetUserIdentities**](UsersApi.md#getuseridentities) | **GET** /api/v1/users/{user_id}/identities | Get identities |
| [**GetUserPropertyValues**](UsersApi.md#getuserpropertyvalues) | **GET** /api/v1/users/{user_id}/properties | Get property values |
| [**GetUserSessions**](UsersApi.md#getusersessions) | **GET** /api/v1/users/{user_id}/sessions | Get user sessions |
| [**GetUsers**](UsersApi.md#getusers) | **GET** /api/v1/users | Get users |
| [**GetUsersMFA**](UsersApi.md#getusersmfa) | **GET** /api/v1/users/{user_id}/mfa | Get user&#39;s MFA configuration |
| [**RefreshUserClaims**](UsersApi.md#refreshuserclaims) | **POST** /api/v1/users/{user_id}/refresh_claims | Refresh User Claims and Invalidate Cache |
| [**ResetUsersMFA**](UsersApi.md#resetusersmfa) | **DELETE** /api/v1/users/{user_id}/mfa/{factor_id} | Reset specific environment MFA for a user |
| [**ResetUsersMFAAll**](UsersApi.md#resetusersmfaall) | **DELETE** /api/v1/users/{user_id}/mfa | Reset all environment MFA for a user |
| [**SetUserPassword**](UsersApi.md#setuserpassword) | **PUT** /api/v1/users/{user_id}/password | Set User password |
| [**UpdateUser**](UsersApi.md#updateuser) | **PATCH** /api/v1/user | Update user |
| [**UpdateUserFeatureFlagOverride**](UsersApi.md#updateuserfeatureflagoverride) | **PATCH** /api/v1/users/{user_id}/feature_flags/{feature_flag_key} | Update User Feature Flag Override |
| [**UpdateUserProperties**](UsersApi.md#updateuserproperties) | **PATCH** /api/v1/users/{user_id}/properties | Update Property values |
| [**UpdateUserProperty**](UsersApi.md#updateuserproperty) | **PUT** /api/v1/users/{user_id}/properties/{property_key} | Update Property value |

<a id="createuser"></a>
# **CreateUser**
> CreateUserResponse CreateUser (CreateUserRequest? createUserRequest = null)

Create user

Creates a user record and optionally zero or more identities for the user. An example identity could be the email address of the user.  Users cannot be created in an organization managed by directory sync (`ORGANIZATION_MANAGED_BY_DIRECTORY_SYNC`); user and role changes for those organizations must be made in the upstream identity provider. If `organization_code` is omitted, this also applies to the default registration organization.  <div>   <code>create:users</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class CreateUserExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new UsersApi(config);
            var createUserRequest = new CreateUserRequest?(); // CreateUserRequest? | The details of the user to create. (optional) 

            try
            {
                // Create user
                CreateUserResponse result = apiInstance.CreateUser(createUserRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UsersApi.CreateUser: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateUserWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create user
    ApiResponse<CreateUserResponse> response = apiInstance.CreateUserWithHttpInfo(createUserRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UsersApi.CreateUserWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createUserRequest** | [**CreateUserRequest?**](CreateUserRequest?.md) | The details of the user to create. | [optional]  |

### Return type

[**CreateUserResponse**](CreateUserResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | User successfully created. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createuserbillingcustomer"></a>
# **CreateUserBillingCustomer**
> CreateUserBillingCustomerResponse CreateUserBillingCustomer (string userId, CreateUserBillingCustomerRequest createUserBillingCustomerRequest)

Create user billing customer

Creates a billing customer for a user in an organization, and assigns a published user billing plan.  This mirrors the admin \"Start billing customer\" action on the user billing page.  At most one billing customer is created per user in an organization. If one already exists, the request fails with `BILLING_CUSTOMER_EXISTS`. Concurrent creates that both pass the existence check before either finishes may both return success with the same customer and agreement ids (onboarding reuses the row created under the advisory lock).  <div>   <code>create:user_billing_customers</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class CreateUserBillingCustomerExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new UsersApi(config);
            var userId = kp_c3143a4b50ad43c88e541d9077681782;  // string | The user's ID.
            var createUserBillingCustomerRequest = new CreateUserBillingCustomerRequest(); // CreateUserBillingCustomerRequest | Billing customer details.

            try
            {
                // Create user billing customer
                CreateUserBillingCustomerResponse result = apiInstance.CreateUserBillingCustomer(userId, createUserBillingCustomerRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UsersApi.CreateUserBillingCustomer: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateUserBillingCustomerWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create user billing customer
    ApiResponse<CreateUserBillingCustomerResponse> response = apiInstance.CreateUserBillingCustomerWithHttpInfo(userId, createUserBillingCustomerRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UsersApi.CreateUserBillingCustomerWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **userId** | **string** | The user&#39;s ID. |  |
| **createUserBillingCustomerRequest** | [**CreateUserBillingCustomerRequest**](CreateUserBillingCustomerRequest.md) | Billing customer details. |  |

### Return type

[**CreateUserBillingCustomerResponse**](CreateUserBillingCustomerResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Billing customer successfully created. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createuseridentity"></a>
# **CreateUserIdentity**
> CreateIdentityResponse CreateUserIdentity (string userId, CreateUserIdentityRequest? createUserIdentityRequest = null)

Create identity

Creates an identity for a user.  Identities cannot be added to a user managed by directory sync (`USER_MANAGED_BY_DIRECTORY_SYNC`); user and role changes must be made in the upstream identity provider.  Adding an `enterprise` identity pre-provisions the user for that connection and replaces all of their other identities. A user with an enterprise identity cannot be given any other identity (`CANNOT_ADD_ENTERPRISE_USER_IDENTITY`).  <div>   <code>create:user_identities</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class CreateUserIdentityExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new UsersApi(config);
            var userId = "userId_example";  // string | The user's ID.
            var createUserIdentityRequest = new CreateUserIdentityRequest?(); // CreateUserIdentityRequest? | The identity details. (optional) 

            try
            {
                // Create identity
                CreateIdentityResponse result = apiInstance.CreateUserIdentity(userId, createUserIdentityRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UsersApi.CreateUserIdentity: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateUserIdentityWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create identity
    ApiResponse<CreateIdentityResponse> response = apiInstance.CreateUserIdentityWithHttpInfo(userId, createUserIdentityRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UsersApi.CreateUserIdentityWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **userId** | **string** | The user&#39;s ID. |  |
| **createUserIdentityRequest** | [**CreateUserIdentityRequest?**](CreateUserIdentityRequest?.md) | The identity details. | [optional]  |

### Return type

[**CreateIdentityResponse**](CreateIdentityResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | Identity successfully created. |  -  |
| **400** | Error creating identity. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteuser"></a>
# **DeleteUser**
> SuccessResponse DeleteUser (string id, bool? isDeleteProfile = null)

Delete user

Delete a user record.  Users managed by directory sync cannot be deleted (`USER_MANAGED_BY_DIRECTORY_SYNC`); user and role changes must be made in the upstream identity provider.  <div>   <code>delete:users</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class DeleteUserExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new UsersApi(config);
            var id = kp_c3143a4b50ad43c88e541d9077681782;  // string | The user's id.
            var isDeleteProfile = true;  // bool? | Delete all data and remove the user's profile from all of Kinde, including the subscriber list (optional) 

            try
            {
                // Delete user
                SuccessResponse result = apiInstance.DeleteUser(id, isDeleteProfile);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UsersApi.DeleteUser: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteUserWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete user
    ApiResponse<SuccessResponse> response = apiInstance.DeleteUserWithHttpInfo(id, isDeleteProfile);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UsersApi.DeleteUserWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | The user&#39;s id. |  |
| **isDeleteProfile** | **bool?** | Delete all data and remove the user&#39;s profile from all of Kinde, including the subscriber list | [optional]  |

### Return type

[**SuccessResponse**](SuccessResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | User successfully deleted. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteusersessions"></a>
# **DeleteUserSessions**
> SuccessResponse DeleteUserSessions (string userId)

Delete user sessions

Invalidate user sessions.  <div>   <code>delete:user_sessions</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class DeleteUserSessionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new UsersApi(config);
            var userId = kp_c3143a4b50ad43c88e541d9077681782;  // string | The identifier for the user

            try
            {
                // Delete user sessions
                SuccessResponse result = apiInstance.DeleteUserSessions(userId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UsersApi.DeleteUserSessions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteUserSessionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete user sessions
    ApiResponse<SuccessResponse> response = apiInstance.DeleteUserSessionsWithHttpInfo(userId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UsersApi.DeleteUserSessionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **userId** | **string** | The identifier for the user |  |

### Return type

[**SuccessResponse**](SuccessResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | User sessions successfully invalidated. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **404** | The specified resource was not found |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getuserdata"></a>
# **GetUserData**
> User GetUserData (string id, string? expand = null)

Get user

Retrieve a user record.  <div>   <code>read:users</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetUserDataExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new UsersApi(config);
            var id = "id_example";  // string | The user's id.
            var expand = "expand_example";  // string? | Additional data to include in the response. One or more of (comma-separated): \"organizations\", \"identities\", \"billing\". (optional) 

            try
            {
                // Get user
                User result = apiInstance.GetUserData(id, expand);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UsersApi.GetUserData: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetUserDataWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get user
    ApiResponse<User> response = apiInstance.GetUserDataWithHttpInfo(id, expand);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UsersApi.GetUserDataWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | The user&#39;s id. |  |
| **expand** | **string?** | Additional data to include in the response. One or more of (comma-separated): \&quot;organizations\&quot;, \&quot;identities\&quot;, \&quot;billing\&quot;. | [optional]  |

### Return type

[**User**](User.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | User successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getuseridentities"></a>
# **GetUserIdentities**
> GetIdentitiesResponse GetUserIdentities (string userId, string? startingAfter = null, string? endingBefore = null)

Get identities

Gets a list of identities for an user by ID.  <div>   <code>read:user_identities</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetUserIdentitiesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new UsersApi(config);
            var userId = "userId_example";  // string | The user's ID.
            var startingAfter = "startingAfter_example";  // string? | The ID of the identity to start after. (optional) 
            var endingBefore = "endingBefore_example";  // string? | The ID of the identity to end before. (optional) 

            try
            {
                // Get identities
                GetIdentitiesResponse result = apiInstance.GetUserIdentities(userId, startingAfter, endingBefore);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UsersApi.GetUserIdentities: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetUserIdentitiesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get identities
    ApiResponse<GetIdentitiesResponse> response = apiInstance.GetUserIdentitiesWithHttpInfo(userId, startingAfter, endingBefore);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UsersApi.GetUserIdentitiesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **userId** | **string** | The user&#39;s ID. |  |
| **startingAfter** | **string?** | The ID of the identity to start after. | [optional]  |
| **endingBefore** | **string?** | The ID of the identity to end before. | [optional]  |

### Return type

[**GetIdentitiesResponse**](GetIdentitiesResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Identities successfully retrieved. |  -  |
| **400** | Bad request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getuserpropertyvalues"></a>
# **GetUserPropertyValues**
> GetPropertyValuesResponse GetUserPropertyValues (string userId)

Get property values

Gets properties for an user by ID.  <div>   <code>read:user_properties</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetUserPropertyValuesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new UsersApi(config);
            var userId = "userId_example";  // string | The user's ID.

            try
            {
                // Get property values
                GetPropertyValuesResponse result = apiInstance.GetUserPropertyValues(userId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UsersApi.GetUserPropertyValues: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetUserPropertyValuesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get property values
    ApiResponse<GetPropertyValuesResponse> response = apiInstance.GetUserPropertyValuesWithHttpInfo(userId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UsersApi.GetUserPropertyValuesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **userId** | **string** | The user&#39;s ID. |  |

### Return type

[**GetPropertyValuesResponse**](GetPropertyValuesResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Properties successfully retrieved. |  -  |
| **400** | Bad request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getusersessions"></a>
# **GetUserSessions**
> GetUserSessionsResponse GetUserSessions (string userId)

Get user sessions

Retrieve the list of active sessions for a specific user.  <div>   <code>read:user_sessions</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetUserSessionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new UsersApi(config);
            var userId = kp_c3143a4b50ad43c88e541d9077681782;  // string | The identifier for the user

            try
            {
                // Get user sessions
                GetUserSessionsResponse result = apiInstance.GetUserSessions(userId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UsersApi.GetUserSessions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetUserSessionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get user sessions
    ApiResponse<GetUserSessionsResponse> response = apiInstance.GetUserSessionsWithHttpInfo(userId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UsersApi.GetUserSessionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **userId** | **string** | The identifier for the user |  |

### Return type

[**GetUserSessionsResponse**](GetUserSessionsResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successfully retrieved user sessions. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **404** | The specified resource was not found |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getusers"></a>
# **GetUsers**
> UsersResponse GetUsers (int? pageSize = null, string? userId = null, string? nextToken = null, string? email = null, string? username = null, string? phone = null, string? expand = null, bool? hasOrganization = null, DateTimeOffset? activeSince = null)

Get users

The returned list can be sorted by full name or email address in ascending or descending order. The number of records to return at a time can also be controlled using the `page_size` query string parameter.  <div>   <code>read:users</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetUsersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new UsersApi(config);
            var pageSize = 56;  // int? | Number of results per page. Defaults to 10 if parameter not sent. (optional) 
            var userId = "userId_example";  // string? | Filter the results by User ID. The query string should be comma separated and url encoded. (optional) 
            var nextToken = "nextToken_example";  // string? | A string to get the next page of results if there are more results. (optional) 
            var email = "email_example";  // string? | Filter the results by email address. The query string should be comma separated and url encoded. (optional) 
            var username = "username_example";  // string? | Filter the results by username. The query string should be comma separated and url encoded. (optional) 
            var phone = "phone_example";  // string? | Filter the results by phone. The query string should be comma separated and url encoded. (optional) 
            var expand = "expand_example";  // string? | Additional data to include in the response. One or more of (comma-separated): \"organizations\", \"identities\", \"billing\". (optional) 
            var hasOrganization = true;  // bool? | Filter the results by if the user has at least one organization assigned. (optional) 
            var activeSince = DateTime.Parse("2013-10-20T19:20:30+01:00");  // DateTimeOffset? | Filter the results to only include users who have been active since this date. Date should be in ISO 8601 format. (optional) 

            try
            {
                // Get users
                UsersResponse result = apiInstance.GetUsers(pageSize, userId, nextToken, email, username, expand, hasOrganization, phone, activeSince);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UsersApi.GetUsers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetUsersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get users
    ApiResponse<UsersResponse> response = apiInstance.GetUsersWithHttpInfo(pageSize, userId, nextToken, email, username, expand, hasOrganization, phone, activeSince);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UsersApi.GetUsersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **pageSize** | **int?** | Number of results per page. Defaults to 10 if parameter not sent. | [optional]  |
| **userId** | **string?** | Filter the results by User ID. The query string should be comma separated and url encoded. | [optional]  |
| **nextToken** | **string?** | A string to get the next page of results if there are more results. | [optional]  |
| **email** | **string?** | Filter the results by email address. The query string should be comma separated and url encoded. | [optional]  |
| **username** | **string?** | Filter the results by username. The query string should be comma separated and url encoded. | [optional]  |
| **expand** | **string?** | Additional data to include in the response. One or more of (comma-separated): \&quot;organizations\&quot;, \&quot;identities\&quot;, \&quot;billing\&quot;. | [optional]  |
| **hasOrganization** | **bool?** | Filter the results by if the user has at least one organization assigned. | [optional]  |
| **phone** | **string?** | Filter the results by phone. The query string should be comma separated and url encoded. | [optional]  |
| **activeSince** | **DateTimeOffset?** | Filter the results to only include users who have been active since this date. Date should be in ISO 8601 format. | [optional]  |

### Return type

[**UsersResponse**](UsersResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Users successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getusersmfa"></a>
# **GetUsersMFA**
> GetUserMfaResponse GetUsersMFA (string userId)

Get user's MFA configuration

Get a user’s MFA configuration.  <div>   <code>read:user_mfa</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetUsersMFAExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new UsersApi(config);
            var userId = kp_c3143a4b50ad43c88e541d9077681782;  // string | The identifier for the user

            try
            {
                // Get user's MFA configuration
                GetUserMfaResponse result = apiInstance.GetUsersMFA(userId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UsersApi.GetUsersMFA: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetUsersMFAWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get user's MFA configuration
    ApiResponse<GetUserMfaResponse> response = apiInstance.GetUsersMFAWithHttpInfo(userId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UsersApi.GetUsersMFAWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **userId** | **string** | The identifier for the user |  |

### Return type

[**GetUserMfaResponse**](GetUserMfaResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successfully retrieve user&#39;s MFA configuration. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **404** | The specified resource was not found |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="refreshuserclaims"></a>
# **RefreshUserClaims**
> SuccessResponse RefreshUserClaims (string userId)

Refresh User Claims and Invalidate Cache

Refreshes the user's claims and invalidates the current cache.  <div>   <code>update:user_refresh_claims</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class RefreshUserClaimsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new UsersApi(config);
            var userId = "userId_example";  // string | The id of the user whose claims needs to be updated.

            try
            {
                // Refresh User Claims and Invalidate Cache
                SuccessResponse result = apiInstance.RefreshUserClaims(userId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UsersApi.RefreshUserClaims: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RefreshUserClaimsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Refresh User Claims and Invalidate Cache
    ApiResponse<SuccessResponse> response = apiInstance.RefreshUserClaimsWithHttpInfo(userId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UsersApi.RefreshUserClaimsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **userId** | **string** | The id of the user whose claims needs to be updated. |  |

### Return type

[**SuccessResponse**](SuccessResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json; charset=utf-8, application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Claims successfully refreshed. |  -  |
| **400** | Bad request. |  -  |
| **403** | Bad request. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="resetusersmfa"></a>
# **ResetUsersMFA**
> SuccessResponse ResetUsersMFA (string userId, string factorId)

Reset specific environment MFA for a user

Reset a specific environment MFA factor for a user.  <div>   <code>delete:user_mfa</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class ResetUsersMFAExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new UsersApi(config);
            var userId = kp_c3143a4b50ad43c88e541d9077681782;  // string | The identifier for the user
            var factorId = mfa_0193278a00ac29b3f6d4e4d462d55c47;  // string | The identifier for the MFA factor

            try
            {
                // Reset specific environment MFA for a user
                SuccessResponse result = apiInstance.ResetUsersMFA(userId, factorId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UsersApi.ResetUsersMFA: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ResetUsersMFAWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Reset specific environment MFA for a user
    ApiResponse<SuccessResponse> response = apiInstance.ResetUsersMFAWithHttpInfo(userId, factorId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UsersApi.ResetUsersMFAWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **userId** | **string** | The identifier for the user |  |
| **factorId** | **string** | The identifier for the MFA factor |  |

### Return type

[**SuccessResponse**](SuccessResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | User&#39;s MFA successfully reset. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **404** | The specified resource was not found |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="resetusersmfaall"></a>
# **ResetUsersMFAAll**
> SuccessResponse ResetUsersMFAAll (string userId)

Reset all environment MFA for a user

Reset all environment MFA factors for a user.  <div>   <code>delete:user_mfa</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class ResetUsersMFAAllExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new UsersApi(config);
            var userId = kp_c3143a4b50ad43c88e541d9077681782;  // string | The identifier for the user

            try
            {
                // Reset all environment MFA for a user
                SuccessResponse result = apiInstance.ResetUsersMFAAll(userId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UsersApi.ResetUsersMFAAll: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ResetUsersMFAAllWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Reset all environment MFA for a user
    ApiResponse<SuccessResponse> response = apiInstance.ResetUsersMFAAllWithHttpInfo(userId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UsersApi.ResetUsersMFAAllWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **userId** | **string** | The identifier for the user |  |

### Return type

[**SuccessResponse**](SuccessResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | User&#39;s MFA successfully reset. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **404** | The specified resource was not found |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="setuserpassword"></a>
# **SetUserPassword**
> SuccessResponse SetUserPassword (string userId, SetUserPasswordRequest setUserPasswordRequest)

Set User password

Set user password.  Passwords cannot be set on a user managed by directory sync (`USER_MANAGED_BY_DIRECTORY_SYNC`); user and role changes must be made in the upstream identity provider.  <div>   <code>update:user_passwords</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class SetUserPasswordExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new UsersApi(config);
            var userId = "userId_example";  // string | The identifier for the user
            var setUserPasswordRequest = new SetUserPasswordRequest(); // SetUserPasswordRequest | Password details.

            try
            {
                // Set User password
                SuccessResponse result = apiInstance.SetUserPassword(userId, setUserPasswordRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UsersApi.SetUserPassword: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the SetUserPasswordWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Set User password
    ApiResponse<SuccessResponse> response = apiInstance.SetUserPasswordWithHttpInfo(userId, setUserPasswordRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UsersApi.SetUserPasswordWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **userId** | **string** | The identifier for the user |  |
| **setUserPasswordRequest** | [**SetUserPasswordRequest**](SetUserPasswordRequest.md) | Password details. |  |

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
| **200** | User successfully created. |  -  |
| **400** | Error creating user. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateuser"></a>
# **UpdateUser**
> UpdateUserResponse UpdateUser (string id, UpdateUserRequest updateUserRequest)

Update user

Update a user record.  When `given_name` or `family_name` is updated and the user is the owner of a family billing customer, the change is also propagated to the corresponding billing customer details.  Users managed by directory sync cannot be updated (`USER_MANAGED_BY_DIRECTORY_SYNC`); user and role changes must be made in the upstream identity provider.  <div>   <code>update:users</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class UpdateUserExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new UsersApi(config);
            var id = "id_example";  // string | The user's id.
            var updateUserRequest = new UpdateUserRequest(); // UpdateUserRequest | The user to update.

            try
            {
                // Update user
                UpdateUserResponse result = apiInstance.UpdateUser(id, updateUserRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UsersApi.UpdateUser: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateUserWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update user
    ApiResponse<UpdateUserResponse> response = apiInstance.UpdateUserWithHttpInfo(id, updateUserRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UsersApi.UpdateUserWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | The user&#39;s id. |  |
| **updateUserRequest** | [**UpdateUserRequest**](UpdateUserRequest.md) | The user to update. |  |

### Return type

[**UpdateUserResponse**](UpdateUserResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | User successfully updated. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateuserfeatureflagoverride"></a>
# **UpdateUserFeatureFlagOverride**
> SuccessResponse UpdateUserFeatureFlagOverride (string userId, string featureFlagKey, string value)

Update User Feature Flag Override

Update user feature flag override.  <div>   <code>update:user_feature_flags</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class UpdateUserFeatureFlagOverrideExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new UsersApi(config);
            var userId = "userId_example";  // string | The identifier for the user
            var featureFlagKey = "featureFlagKey_example";  // string | The identifier for the feature flag
            var value = "value_example";  // string | Override value

            try
            {
                // Update User Feature Flag Override
                SuccessResponse result = apiInstance.UpdateUserFeatureFlagOverride(userId, featureFlagKey, value);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UsersApi.UpdateUserFeatureFlagOverride: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateUserFeatureFlagOverrideWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update User Feature Flag Override
    ApiResponse<SuccessResponse> response = apiInstance.UpdateUserFeatureFlagOverrideWithHttpInfo(userId, featureFlagKey, value);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UsersApi.UpdateUserFeatureFlagOverrideWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **userId** | **string** | The identifier for the user |  |
| **featureFlagKey** | **string** | The identifier for the feature flag |  |
| **value** | **string** | Override value |  |

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
| **200** | Feature flag override successfully updated. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateuserproperties"></a>
# **UpdateUserProperties**
> SuccessResponse UpdateUserProperties (string userId, UpdateOrganizationPropertiesRequest updateOrganizationPropertiesRequest)

Update Property values

Update property values.  <div>   <code>update:user_properties</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class UpdateUserPropertiesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new UsersApi(config);
            var userId = "userId_example";  // string | The identifier for the user
            var updateOrganizationPropertiesRequest = new UpdateOrganizationPropertiesRequest(); // UpdateOrganizationPropertiesRequest | Properties to update.

            try
            {
                // Update Property values
                SuccessResponse result = apiInstance.UpdateUserProperties(userId, updateOrganizationPropertiesRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UsersApi.UpdateUserProperties: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateUserPropertiesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Property values
    ApiResponse<SuccessResponse> response = apiInstance.UpdateUserPropertiesWithHttpInfo(userId, updateOrganizationPropertiesRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UsersApi.UpdateUserPropertiesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **userId** | **string** | The identifier for the user |  |
| **updateOrganizationPropertiesRequest** | [**UpdateOrganizationPropertiesRequest**](UpdateOrganizationPropertiesRequest.md) | Properties to update. |  |

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
| **200** | Properties successfully updated. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateuserproperty"></a>
# **UpdateUserProperty**
> SuccessResponse UpdateUserProperty (string userId, string propertyKey, string value)

Update Property value

Update property value.  <div>   <code>update:user_properties</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class UpdateUserPropertyExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new UsersApi(config);
            var userId = "userId_example";  // string | The identifier for the user
            var propertyKey = "propertyKey_example";  // string | The identifier for the property
            var value = "value_example";  // string | The new property value. Send an empty string to unset the property, so it reads back as not set.

            try
            {
                // Update Property value
                SuccessResponse result = apiInstance.UpdateUserProperty(userId, propertyKey, value);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UsersApi.UpdateUserProperty: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateUserPropertyWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Property value
    ApiResponse<SuccessResponse> response = apiInstance.UpdateUserPropertyWithHttpInfo(userId, propertyKey, value);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UsersApi.UpdateUserPropertyWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **userId** | **string** | The identifier for the user |  |
| **propertyKey** | **string** | The identifier for the property |  |
| **value** | **string** | The new property value. Send an empty string to unset the property, so it reads back as not set. |  |

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
| **200** | Property successfully updated. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

