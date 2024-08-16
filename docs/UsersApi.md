# Kinde.Api.Api.UsersApi

All URIs are relative to *https://app.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateUser**](UsersApi.md#createuser) | **POST** /api/v1/user | Create User |
| [**CreateUserIdentity**](UsersApi.md#createuseridentity) | **POST** /api/v1/users/{user_id}/identities | Create identity |
| [**DeleteUser**](UsersApi.md#deleteuser) | **DELETE** /api/v1/user | Delete User |
| [**GetUserData**](UsersApi.md#getuserdata) | **GET** /api/v1/user | Get User |
| [**GetUserIdentities**](UsersApi.md#getuseridentities) | **GET** /api/v1/users/{user_id}/identities | Get identities |
| [**GetUserPropertyValues**](UsersApi.md#getuserpropertyvalues) | **GET** /api/v1/users/{user_id}/properties | Get property values |
| [**GetUsers**](UsersApi.md#getusers) | **GET** /api/v1/users | List Users |
| [**RefreshUserClaims**](UsersApi.md#refreshuserclaims) | **POST** /api/v1/users/{user_id}/refresh_claims | Refresh User Claims and Invalidate Cache |
| [**SetUserPassword**](UsersApi.md#setuserpassword) | **PUT** /api/v1/users/{user_id}/password | Set User password |
| [**UpdateUser**](UsersApi.md#updateuser) | **PATCH** /api/v1/user | Update User |
| [**UpdateUserFeatureFlagOverride**](UsersApi.md#updateuserfeatureflagoverride) | **PATCH** /api/v1/users/{user_id}/feature_flags/{feature_flag_key} | Update User Feature Flag Override |
| [**UpdateUserProperties**](UsersApi.md#updateuserproperties) | **PATCH** /api/v1/users/{user_id}/properties | Update Property values |
| [**UpdateUserProperty**](UsersApi.md#updateuserproperty) | **PUT** /api/v1/users/{user_id}/properties/{property_key} | Update Property value |

<a id="createuser"></a>
# **CreateUser**
> CreateUserResponse CreateUser (CreateUserRequest? createUserRequest = null)

Create User

Creates a user record and optionally zero or more identities for the user. An example identity could be the email address of the user. 

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
    public class CreateUserExample
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
            var apiInstance = new UsersApi(httpClient, config, httpClientHandler);
            var createUserRequest = new CreateUserRequest?(); // CreateUserRequest? | The details of the user to create. (optional) 

            try
            {
                // Create User
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
    // Create User
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
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | User successfully created. |  -  |
| **400** | Error creating user. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createuseridentity"></a>
# **CreateUserIdentity**
> CreateIdentityResponse CreateUserIdentity (string userId, CreateUserIdentityRequest? createUserIdentityRequest = null)

Create identity

Creates an identity for a user. 

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
    public class CreateUserIdentityExample
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
            var apiInstance = new UsersApi(httpClient, config, httpClientHandler);
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
| **200** | Identity successfully created. |  -  |
| **400** | Error creating identity. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteuser"></a>
# **DeleteUser**
> SuccessResponse DeleteUser (string id, bool? isDeleteProfile = null)

Delete User

Delete a user record. 

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
    public class DeleteUserExample
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
            var apiInstance = new UsersApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | The user's id.
            var isDeleteProfile = true;  // bool? | Delete all data and remove the user's profile from all of Kinde, including the subscriber list (optional) 

            try
            {
                // Delete User
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
    // Delete User
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
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | User successfully deleted. |  -  |
| **400** | Bad request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getuserdata"></a>
# **GetUserData**
> User GetUserData (string id, string? expand = null)

Get User

Retrieve a user record. 

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
    public class GetUserDataExample
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
            var apiInstance = new UsersApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | The user's id.
            var expand = "expand_example";  // string? | Specify additional data to retrieve. Use \"organizations\" and/or \"identities\". (optional) 

            try
            {
                // Get User
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
    // Get User
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
| **expand** | **string?** | Specify additional data to retrieve. Use \&quot;organizations\&quot; and/or \&quot;identities\&quot;. | [optional]  |

### Return type

[**User**](User.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | User successfully updated. |  -  |
| **400** | Bad request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getuseridentities"></a>
# **GetUserIdentities**
> GetIdentitiesResponse GetUserIdentities (string userId)

Get identities

Gets a list of identities for an user by ID. 

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
    public class GetUserIdentitiesExample
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
            var apiInstance = new UsersApi(httpClient, config, httpClientHandler);
            var userId = "userId_example";  // string | The user's ID.

            try
            {
                // Get identities
                GetIdentitiesResponse result = apiInstance.GetUserIdentities(userId);
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
    ApiResponse<GetIdentitiesResponse> response = apiInstance.GetUserIdentitiesWithHttpInfo(userId);
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

Gets properties for an user by ID. 

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
    public class GetUserPropertyValuesExample
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
            var apiInstance = new UsersApi(httpClient, config, httpClientHandler);
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

<a id="getusers"></a>
# **GetUsers**
> UsersResponse GetUsers (int? pageSize = null, string? userId = null, string? nextToken = null, string? email = null, string? username = null, string? expand = null, bool? hasOrganization = null)

List Users

The returned list can be sorted by full name or email address in ascending or descending order. The number of records to return at a time can also be controlled using the `page_size` query string parameter. 

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
    public class GetUsersExample
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
            var apiInstance = new UsersApi(httpClient, config, httpClientHandler);
            var pageSize = 56;  // int? | Number of results per page. Defaults to 10 if parameter not sent. (optional) 
            var userId = "userId_example";  // string? | ID of the user to filter by. (optional) 
            var nextToken = "nextToken_example";  // string? | A string to get the next page of results if there are more results. (optional) 
            var email = "email_example";  // string? | Filter the results by email address. The query string should be comma separated and url encoded. (optional) 
            var username = "username_example";  // string? | Filter the results by username. The query string should be comma separated and url encoded. (optional) 
            var expand = "expand_example";  // string? | Specify additional data to retrieve. Use \"organizations\" and/or \"identities\". (optional) 
            var hasOrganization = true;  // bool? | Filter the results by if the user has at least one organization assigned. (optional) 

            try
            {
                // List Users
                UsersResponse result = apiInstance.GetUsers(pageSize, userId, nextToken, email, username, expand, hasOrganization);
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
    // List Users
    ApiResponse<UsersResponse> response = apiInstance.GetUsersWithHttpInfo(pageSize, userId, nextToken, email, username, expand, hasOrganization);
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
| **userId** | **string?** | ID of the user to filter by. | [optional]  |
| **nextToken** | **string?** | A string to get the next page of results if there are more results. | [optional]  |
| **email** | **string?** | Filter the results by email address. The query string should be comma separated and url encoded. | [optional]  |
| **username** | **string?** | Filter the results by username. The query string should be comma separated and url encoded. | [optional]  |
| **expand** | **string?** | Specify additional data to retrieve. Use \&quot;organizations\&quot; and/or \&quot;identities\&quot;. | [optional]  |
| **hasOrganization** | **bool?** | Filter the results by if the user has at least one organization assigned. | [optional]  |

### Return type

[**UsersResponse**](UsersResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Users successfully retrieved. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="refreshuserclaims"></a>
# **RefreshUserClaims**
> SuccessResponse RefreshUserClaims (string userId)

Refresh User Claims and Invalidate Cache

Refreshes the user's claims and invalidates the current cache. 

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
    public class RefreshUserClaimsExample
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
            var apiInstance = new UsersApi(httpClient, config, httpClientHandler);
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

<a id="setuserpassword"></a>
# **SetUserPassword**
> SuccessResponse SetUserPassword (string userId, SetUserPasswordRequest setUserPasswordRequest)

Set User password

Set user password.

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
    public class SetUserPasswordExample
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
            var apiInstance = new UsersApi(httpClient, config, httpClientHandler);
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

Update User

Update a user record. 

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
    public class UpdateUserExample
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
            var apiInstance = new UsersApi(httpClient, config, httpClientHandler);
            var id = "id_example";  // string | The user's id.
            var updateUserRequest = new UpdateUserRequest(); // UpdateUserRequest | The user to update.

            try
            {
                // Update User
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
    // Update User
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
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | User successfully updated. |  -  |
| **400** | Bad request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateuserfeatureflagoverride"></a>
# **UpdateUserFeatureFlagOverride**
> SuccessResponse UpdateUserFeatureFlagOverride (string userId, string featureFlagKey, string value)

Update User Feature Flag Override

Update user feature flag override.

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
    public class UpdateUserFeatureFlagOverrideExample
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
            var apiInstance = new UsersApi(httpClient, config, httpClientHandler);
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

Update property values.

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
    public class UpdateUserPropertiesExample
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
            var apiInstance = new UsersApi(httpClient, config, httpClientHandler);
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

Update property value.

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
    public class UpdateUserPropertyExample
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
            var apiInstance = new UsersApi(httpClient, config, httpClientHandler);
            var userId = "userId_example";  // string | The identifier for the user
            var propertyKey = "propertyKey_example";  // string | The identifier for the property
            var value = "value_example";  // string | The new property value

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
| **value** | **string** | The new property value |  |

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

