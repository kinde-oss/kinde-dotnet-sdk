# Kinde.Api.Api.OrganizationsApi

All URIs are relative to *https://your_kinde_subdomain.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**AddOrganizationLogo**](OrganizationsApi.md#addorganizationlogo) | **POST** /api/v1/organizations/{org_code}/logos/{type} | Add organization logo |
| [**AddOrganizationUserAPIScope**](OrganizationsApi.md#addorganizationuserapiscope) | **POST** /api/v1/organizations/{org_code}/users/{user_id}/apis/{api_id}/scopes/{scope_id} | Add scope to organization user api |
| [**AddOrganizationUsers**](OrganizationsApi.md#addorganizationusers) | **POST** /api/v1/organizations/{org_code}/users | Add Organization Users |
| [**CreateOrganization**](OrganizationsApi.md#createorganization) | **POST** /api/v1/organization | Create organization |
| [**CreateOrganizationInvite**](OrganizationsApi.md#createorganizationinvite) | **POST** /api/v1/organization/{org_code}/invites | Create organization invite |
| [**CreateOrganizationPermission**](OrganizationsApi.md#createorganizationpermission) | **POST** /api/v1/organizations/{org_code}/permissions | Create organization-managed permission |
| [**CreateOrganizationRole**](OrganizationsApi.md#createorganizationrole) | **POST** /api/v1/organizations/{org_code}/roles | Create organization-managed role |
| [**CreateOrganizationUserPermission**](OrganizationsApi.md#createorganizationuserpermission) | **POST** /api/v1/organizations/{org_code}/users/{user_id}/permissions | Add Organization User Permission |
| [**CreateOrganizationUserRole**](OrganizationsApi.md#createorganizationuserrole) | **POST** /api/v1/organizations/{org_code}/users/{user_id}/roles | Add Organization User Role |
| [**DeleteOrganization**](OrganizationsApi.md#deleteorganization) | **DELETE** /api/v1/organization/{org_code} | Delete Organization |
| [**DeleteOrganizationFeatureFlagOverride**](OrganizationsApi.md#deleteorganizationfeatureflagoverride) | **DELETE** /api/v1/organizations/{org_code}/feature_flags/{feature_flag_key} | Delete Organization Feature Flag Override |
| [**DeleteOrganizationFeatureFlagOverrides**](OrganizationsApi.md#deleteorganizationfeatureflagoverrides) | **DELETE** /api/v1/organizations/{org_code}/feature_flags | Delete Organization Feature Flag Overrides |
| [**DeleteOrganizationHandle**](OrganizationsApi.md#deleteorganizationhandle) | **DELETE** /api/v1/organization/{org_code}/handle | Delete organization handle |
| [**DeleteOrganizationInvite**](OrganizationsApi.md#deleteorganizationinvite) | **DELETE** /api/v1/organization/{org_code}/invites/{invite_code} | Delete organization invite |
| [**DeleteOrganizationLogo**](OrganizationsApi.md#deleteorganizationlogo) | **DELETE** /api/v1/organizations/{org_code}/logos/{type} | Delete organization logo |
| [**DeleteOrganizationPermission**](OrganizationsApi.md#deleteorganizationpermission) | **DELETE** /api/v1/organizations/{org_code}/permissions/{permission_id} | Delete organization-managed permission |
| [**DeleteOrganizationRole**](OrganizationsApi.md#deleteorganizationrole) | **DELETE** /api/v1/organizations/{org_code}/roles/{role_id} | Delete organization-managed role |
| [**DeleteOrganizationUserAPIScope**](OrganizationsApi.md#deleteorganizationuserapiscope) | **DELETE** /api/v1/organizations/{org_code}/users/{user_id}/apis/{api_id}/scopes/{scope_id} | Delete scope from organization user API |
| [**DeleteOrganizationUserPermission**](OrganizationsApi.md#deleteorganizationuserpermission) | **DELETE** /api/v1/organizations/{org_code}/users/{user_id}/permissions/{permission_id} | Delete Organization User Permission |
| [**DeleteOrganizationUserRole**](OrganizationsApi.md#deleteorganizationuserrole) | **DELETE** /api/v1/organizations/{org_code}/users/{user_id}/roles/{role_id} | Delete Organization User Role |
| [**EnableOrgConnection**](OrganizationsApi.md#enableorgconnection) | **POST** /api/v1/organizations/{organization_code}/connections/{connection_id} | Enable connection |
| [**GetOrgUserMFA**](OrganizationsApi.md#getorgusermfa) | **GET** /api/v1/organizations/{org_code}/users/{user_id}/mfa | Get an organization user&#39;s MFA configuration |
| [**GetOrganization**](OrganizationsApi.md#getorganization) | **GET** /api/v1/organization | Get organization |
| [**GetOrganizationConnections**](OrganizationsApi.md#getorganizationconnections) | **GET** /api/v1/organizations/{organization_code}/connections | Get connections |
| [**GetOrganizationFeatureFlags**](OrganizationsApi.md#getorganizationfeatureflags) | **GET** /api/v1/organizations/{org_code}/feature_flags | List Organization Feature Flags |
| [**GetOrganizationInvite**](OrganizationsApi.md#getorganizationinvite) | **GET** /api/v1/organization/{org_code}/invites/{invite_code} | Get organization invite |
| [**GetOrganizationInvites**](OrganizationsApi.md#getorganizationinvites) | **GET** /api/v1/organization/{org_code}/invites | Get organization invites |
| [**GetOrganizationMFA**](OrganizationsApi.md#getorganizationmfa) | **GET** /api/v1/organizations/{org_code}/mfa | Get Organization MFA Configuration |
| [**GetOrganizationPasskey**](OrganizationsApi.md#getorganizationpasskey) | **GET** /api/v1/organizations/{org_code}/passkey | Get organization passkey settings |
| [**GetOrganizationPermission**](OrganizationsApi.md#getorganizationpermission) | **GET** /api/v1/organizations/{org_code}/permissions/{permission_id} | Get organization-managed permission |
| [**GetOrganizationPermissions**](OrganizationsApi.md#getorganizationpermissions) | **GET** /api/v1/organizations/{org_code}/permissions | List organization-managed permissions |
| [**GetOrganizationPropertyValues**](OrganizationsApi.md#getorganizationpropertyvalues) | **GET** /api/v1/organizations/{org_code}/properties | Get Organization Property Values |
| [**GetOrganizationRole**](OrganizationsApi.md#getorganizationrole) | **GET** /api/v1/organizations/{org_code}/roles/{role_id} | Get organization-managed role |
| [**GetOrganizationRoleActiveUsersCount**](OrganizationsApi.md#getorganizationroleactiveuserscount) | **GET** /api/v1/organizations/{org_code}/roles/{role_id}/active_users/count | Get organization role active users count |
| [**GetOrganizationRoleUsers**](OrganizationsApi.md#getorganizationroleusers) | **GET** /api/v1/organizations/{org_code}/roles/{role_id}/users | List organization role users |
| [**GetOrganizationRoleUsersCount**](OrganizationsApi.md#getorganizationroleuserscount) | **GET** /api/v1/organizations/{org_code}/roles/{role_id}/users/count | Count organization role users |
| [**GetOrganizationRoles**](OrganizationsApi.md#getorganizationroles) | **GET** /api/v1/organizations/{org_code}/roles | List organization-managed roles |
| [**GetOrganizationUserPermissions**](OrganizationsApi.md#getorganizationuserpermissions) | **GET** /api/v1/organizations/{org_code}/users/{user_id}/permissions | List Organization User Permissions |
| [**GetOrganizationUserRoles**](OrganizationsApi.md#getorganizationuserroles) | **GET** /api/v1/organizations/{org_code}/users/{user_id}/roles | List Organization User Roles |
| [**GetOrganizationUsers**](OrganizationsApi.md#getorganizationusers) | **GET** /api/v1/organizations/{org_code}/users | Get organization users |
| [**GetOrganizations**](OrganizationsApi.md#getorganizations) | **GET** /api/v1/organizations | Get organizations |
| [**ReadOrganizationLogo**](OrganizationsApi.md#readorganizationlogo) | **GET** /api/v1/organizations/{org_code}/logos | Read organization logo details |
| [**RemoveOrgConnection**](OrganizationsApi.md#removeorgconnection) | **DELETE** /api/v1/organizations/{organization_code}/connections/{connection_id} | Remove connection |
| [**RemoveOrganizationUser**](OrganizationsApi.md#removeorganizationuser) | **DELETE** /api/v1/organizations/{org_code}/users/{user_id} | Remove Organization User |
| [**ReplaceOrganizationMFA**](OrganizationsApi.md#replaceorganizationmfa) | **PUT** /api/v1/organizations/{org_code}/mfa | Replace Organization MFA Configuration |
| [**ResetOrgUserMFA**](OrganizationsApi.md#resetorgusermfa) | **DELETE** /api/v1/organizations/{org_code}/users/{user_id}/mfa/{factor_id} | Reset specific organization MFA for a user |
| [**ResetOrgUserMFAAll**](OrganizationsApi.md#resetorgusermfaall) | **DELETE** /api/v1/organizations/{org_code}/users/{user_id}/mfa | Reset all organization MFA for a user |
| [**UpdateOrganization**](OrganizationsApi.md#updateorganization) | **PATCH** /api/v1/organization/{org_code} | Update Organization |
| [**UpdateOrganizationFeatureFlagOverride**](OrganizationsApi.md#updateorganizationfeatureflagoverride) | **PATCH** /api/v1/organizations/{org_code}/feature_flags/{feature_flag_key} | Update Organization Feature Flag Override |
| [**UpdateOrganizationPasskey**](OrganizationsApi.md#updateorganizationpasskey) | **PUT** /api/v1/organizations/{org_code}/passkey | Update organization passkey settings |
| [**UpdateOrganizationPermission**](OrganizationsApi.md#updateorganizationpermission) | **PATCH** /api/v1/organizations/{org_code}/permissions/{permission_id} | Update organization-managed permission |
| [**UpdateOrganizationProperties**](OrganizationsApi.md#updateorganizationproperties) | **PATCH** /api/v1/organizations/{org_code}/properties | Update Organization Property values |
| [**UpdateOrganizationProperty**](OrganizationsApi.md#updateorganizationproperty) | **PUT** /api/v1/organizations/{org_code}/properties/{property_key} | Update Organization Property value |
| [**UpdateOrganizationRole**](OrganizationsApi.md#updateorganizationrole) | **PATCH** /api/v1/organizations/{org_code}/roles/{role_id} | Update organization-managed role |
| [**UpdateOrganizationSessions**](OrganizationsApi.md#updateorganizationsessions) | **PATCH** /api/v1/organizations/{org_code}/sessions | Update organization session configuration |
| [**UpdateOrganizationUsers**](OrganizationsApi.md#updateorganizationusers) | **PATCH** /api/v1/organizations/{org_code}/users | Update Organization Users |

<a id="addorganizationlogo"></a>
# **AddOrganizationLogo**
> SuccessResponse AddOrganizationLogo (string orgCode, string type, System.IO.Stream logo)

Add organization logo

Add organization logo  <div>   <code>update:organizations</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class AddOrganizationLogoExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = org_1ccfb819462;  // string | The organization's code.
            var type = dark;  // string | The type of logo to add.
            var logo = new System.IO.MemoryStream(System.IO.File.ReadAllBytes("/path/to/file.txt"));  // System.IO.Stream | The logo file to upload.

            try
            {
                // Add organization logo
                SuccessResponse result = apiInstance.AddOrganizationLogo(orgCode, type, logo);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.AddOrganizationLogo: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AddOrganizationLogoWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add organization logo
    ApiResponse<SuccessResponse> response = apiInstance.AddOrganizationLogoWithHttpInfo(orgCode, type, logo);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.AddOrganizationLogoWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **type** | **string** | The type of logo to add. |  |
| **logo** | **System.IO.Stream****System.IO.Stream** | The logo file to upload. |  |

### Return type

[**SuccessResponse**](SuccessResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: multipart/form-data
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Organization logo successfully updated |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="addorganizationuserapiscope"></a>
# **AddOrganizationUserAPIScope**
> void AddOrganizationUserAPIScope (string orgCode, string userId, string apiId, string scopeId)

Add scope to organization user api

Add a scope to an organization user api.  <div>   <code>create:organization_user_api_scopes</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class AddOrganizationUserAPIScopeExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The identifier for the organization.
            var userId = kp_5ce676e5d6a24bc9aac2fba35a46e958;  // string | User ID
            var apiId = 838f208d006a482dbd8cdb79a9889f68;  // string | API ID
            var scopeId = api_scope_019391daf58d87d8a7213419c016ac95;  // string | Scope ID

            try
            {
                // Add scope to organization user api
                apiInstance.AddOrganizationUserAPIScope(orgCode, userId, apiId, scopeId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.AddOrganizationUserAPIScope: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AddOrganizationUserAPIScopeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add scope to organization user api
    apiInstance.AddOrganizationUserAPIScopeWithHttpInfo(orgCode, userId, apiId, scopeId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.AddOrganizationUserAPIScopeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization. |  |
| **userId** | **string** | User ID |  |
| **apiId** | **string** | API ID |  |
| **scopeId** | **string** | Scope ID |  |

### Return type

void (empty response body)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | API scope successfully added to organization user api |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="addorganizationusers"></a>
# **AddOrganizationUsers**
> AddOrganizationUsersResponse AddOrganizationUsers (string orgCode, AddOrganizationUsersRequest? addOrganizationUsersRequest = null)

Add Organization Users

Add existing users to an organization.  Every key in `roles` must be a business-wide role key, or the key of one of this organization's own roles while organization-managed roles are available on the business's plan. Any other key fails the whole request with `INVALID_ROLES` instead of being skipped.  Users cannot be added to organizations that are managed by directory sync (`ORGANIZATION_MANAGED_BY_DIRECTORY_SYNC`); user and role changes for those organizations must be made in the upstream identity provider.  <div>   <code>create:organization_users</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class AddOrganizationUsersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var addOrganizationUsersRequest = new AddOrganizationUsersRequest?(); // AddOrganizationUsersRequest? |  (optional) 

            try
            {
                // Add Organization Users
                AddOrganizationUsersResponse result = apiInstance.AddOrganizationUsers(orgCode, addOrganizationUsersRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.AddOrganizationUsers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AddOrganizationUsersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add Organization Users
    ApiResponse<AddOrganizationUsersResponse> response = apiInstance.AddOrganizationUsersWithHttpInfo(orgCode, addOrganizationUsersRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.AddOrganizationUsersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **addOrganizationUsersRequest** | [**AddOrganizationUsersRequest?**](AddOrganizationUsersRequest?.md) |  | [optional]  |

### Return type

[**AddOrganizationUsersResponse**](AddOrganizationUsersResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Add organization users request successfully processed. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createorganization"></a>
# **CreateOrganization**
> CreateOrganizationResponse CreateOrganization (CreateOrganizationRequest createOrganizationRequest)

Create organization

Create a new organization. To learn more read about [multi tenancy using organizations](https://docs.kinde.com/build/organizations/multi-tenancy-using-organizations/)  <div>   <code>create:organizations</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class CreateOrganizationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var createOrganizationRequest = new CreateOrganizationRequest(); // CreateOrganizationRequest | Organization details.

            try
            {
                // Create organization
                CreateOrganizationResponse result = apiInstance.CreateOrganization(createOrganizationRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.CreateOrganization: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateOrganizationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create organization
    ApiResponse<CreateOrganizationResponse> response = apiInstance.CreateOrganizationWithHttpInfo(createOrganizationRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.CreateOrganizationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createOrganizationRequest** | [**CreateOrganizationRequest**](CreateOrganizationRequest.md) | Organization details. |  |

### Return type

[**CreateOrganizationResponse**](CreateOrganizationResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Organization successfully created. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createorganizationinvite"></a>
# **CreateOrganizationInvite**
> CreateOrganizationInviteResponse CreateOrganizationInvite (string orgCode, CreateOrganizationInviteRequest createOrganizationInviteRequest)

Create organization invite

Create a new invitation for an organization. An invitation email will be sent to the provided email address if `send_email` is set to `true`.  Invitations cannot be created for organizations that are managed by directory sync; user and role changes for those organizations must be made in the upstream identity provider.  Roles that require an explicit assignment permission cannot be granted to an invitee unless the caller (or the user the token represents) holds that permission. On Kinde-hosted plans, roles outside `owner`/`admin` additionally require the `extended_roles` entitlement.  Per-organization rate limits apply: a maximum number of invitations may be created per rolling 24 hour window, and a maximum number of active (non-accepted, non-revoked) invitations may exist at any time. Requests that exceed either limit are rejected.  <div>   <code>create:organization_invites</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class CreateOrganizationInviteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = org_1ccfb819462;  // string | The organization's code.
            var createOrganizationInviteRequest = new CreateOrganizationInviteRequest(); // CreateOrganizationInviteRequest | Invitation details. `email` is capped at 254 characters (RFC 5321). `first_name` and `last_name` are capped at 64 characters each. Inputs over these limits are rejected with `EMAIL_TOO_LONG`, `FIRST_NAME_TOO_LONG`, or `LAST_NAME_TOO_LONG`.

            try
            {
                // Create organization invite
                CreateOrganizationInviteResponse result = apiInstance.CreateOrganizationInvite(orgCode, createOrganizationInviteRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.CreateOrganizationInvite: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateOrganizationInviteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create organization invite
    ApiResponse<CreateOrganizationInviteResponse> response = apiInstance.CreateOrganizationInviteWithHttpInfo(orgCode, createOrganizationInviteRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.CreateOrganizationInviteWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **createOrganizationInviteRequest** | [**CreateOrganizationInviteRequest**](CreateOrganizationInviteRequest.md) | Invitation details. &#x60;email&#x60; is capped at 254 characters (RFC 5321). &#x60;first_name&#x60; and &#x60;last_name&#x60; are capped at 64 characters each. Inputs over these limits are rejected with &#x60;EMAIL_TOO_LONG&#x60;, &#x60;FIRST_NAME_TOO_LONG&#x60;, or &#x60;LAST_NAME_TOO_LONG&#x60;. |  |

### Return type

[**CreateOrganizationInviteResponse**](CreateOrganizationInviteResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | Invitation successfully created. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createorganizationpermission"></a>
# **CreateOrganizationPermission**
> SuccessResponse CreateOrganizationPermission (string orgCode, CreateOrganizationPermissionRequest createOrganizationPermissionRequest)

Create organization-managed permission

Define a permission that belongs to an organization. An organization can have up to 100 permissions of its own.  Organization-managed permissions are permissions an organization defines for itself. Only that organization's own roles can hold them, they appear in tokens through those roles, and they are available on the Scale plan alongside organization-managed roles (they use the same scopes). If the business moves off Scale they stay stored but stop appearing in tokens. They are not available on the Kinde business.  <div>   <code>create:organization_roles</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class CreateOrganizationPermissionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var createOrganizationPermissionRequest = new CreateOrganizationPermissionRequest(); // CreateOrganizationPermissionRequest | Permission details.

            try
            {
                // Create organization-managed permission
                SuccessResponse result = apiInstance.CreateOrganizationPermission(orgCode, createOrganizationPermissionRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.CreateOrganizationPermission: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateOrganizationPermissionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create organization-managed permission
    ApiResponse<SuccessResponse> response = apiInstance.CreateOrganizationPermissionWithHttpInfo(orgCode, createOrganizationPermissionRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.CreateOrganizationPermissionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **createOrganizationPermissionRequest** | [**CreateOrganizationPermissionRequest**](CreateOrganizationPermissionRequest.md) | Permission details. |  |

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
| **201** | Permission successfully created. |  -  |
| **400** | Bad request. |  -  |
| **403** | Invalid credentials, organization-managed permissions are not available on the business&#39;s plan (&#x60;PLAN_LIMIT_REACHED&#x60;), or not available on this business at all (&#x60;ORGANIZATION_PERMISSIONS_UNAVAILABLE&#x60;). |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createorganizationrole"></a>
# **CreateOrganizationRole**
> CreateRolesResponse CreateOrganizationRole (string orgCode, CreateOrganizationRoleRequest createOrganizationRoleRequest)

Create organization-managed role

Create a role that belongs to an organization. An organization can have up to 100 roles of its own.  Organization-managed roles are roles an organization owns. They can only be assigned to members of that organization, they hold only the permissions the business marks as `is_organization_role_assignable`, and they are available on the Scale plan. If the business moves off Scale they stay stored but stop appearing in tokens and permission checks.  <div>   <code>create:organization_roles</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class CreateOrganizationRoleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var createOrganizationRoleRequest = new CreateOrganizationRoleRequest(); // CreateOrganizationRoleRequest | Role details.

            try
            {
                // Create organization-managed role
                CreateRolesResponse result = apiInstance.CreateOrganizationRole(orgCode, createOrganizationRoleRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.CreateOrganizationRole: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateOrganizationRoleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create organization-managed role
    ApiResponse<CreateRolesResponse> response = apiInstance.CreateOrganizationRoleWithHttpInfo(orgCode, createOrganizationRoleRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.CreateOrganizationRoleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **createOrganizationRoleRequest** | [**CreateOrganizationRoleRequest**](CreateOrganizationRoleRequest.md) | Role details. |  |

### Return type

[**CreateRolesResponse**](CreateRolesResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | Role successfully created. |  -  |
| **400** | Bad request. |  -  |
| **403** | Invalid credentials, organization-managed roles are not available on the business&#39;s plan (&#x60;PLAN_LIMIT_REACHED&#x60;), or not on this business (&#x60;ORGANIZATION_ROLES_UNAVAILABLE&#x60;). |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createorganizationuserpermission"></a>
# **CreateOrganizationUserPermission**
> SuccessResponse CreateOrganizationUserPermission (string orgCode, string userId, CreateOrganizationUserPermissionRequest createOrganizationUserPermissionRequest)

Add Organization User Permission

Add permission to an organization user.  <div>   <code>create:organization_user_permissions</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class CreateOrganizationUserPermissionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var userId = "userId_example";  // string | The user's id.
            var createOrganizationUserPermissionRequest = new CreateOrganizationUserPermissionRequest(); // CreateOrganizationUserPermissionRequest | Permission details.

            try
            {
                // Add Organization User Permission
                SuccessResponse result = apiInstance.CreateOrganizationUserPermission(orgCode, userId, createOrganizationUserPermissionRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.CreateOrganizationUserPermission: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateOrganizationUserPermissionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add Organization User Permission
    ApiResponse<SuccessResponse> response = apiInstance.CreateOrganizationUserPermissionWithHttpInfo(orgCode, userId, createOrganizationUserPermissionRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.CreateOrganizationUserPermissionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **userId** | **string** | The user&#39;s id. |  |
| **createOrganizationUserPermissionRequest** | [**CreateOrganizationUserPermissionRequest**](CreateOrganizationUserPermissionRequest.md) | Permission details. |  |

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
| **200** | User permission successfully updated. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createorganizationuserrole"></a>
# **CreateOrganizationUserRole**
> SuccessResponse CreateOrganizationUserRole (string orgCode, string userId, CreateOrganizationUserRoleRequest createOrganizationUserRoleRequest)

Add Organization User Role

Add role to an organization user.  Roles cannot be added to users in organizations managed by directory sync (`ORGANIZATION_MANAGED_BY_DIRECTORY_SYNC`); user and role changes for those organizations must be made in the upstream identity provider.  <div>   <code>create:organization_user_roles</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class CreateOrganizationUserRoleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var userId = "userId_example";  // string | The user's id.
            var createOrganizationUserRoleRequest = new CreateOrganizationUserRoleRequest(); // CreateOrganizationUserRoleRequest | Role details.

            try
            {
                // Add Organization User Role
                SuccessResponse result = apiInstance.CreateOrganizationUserRole(orgCode, userId, createOrganizationUserRoleRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.CreateOrganizationUserRole: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateOrganizationUserRoleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add Organization User Role
    ApiResponse<SuccessResponse> response = apiInstance.CreateOrganizationUserRoleWithHttpInfo(orgCode, userId, createOrganizationUserRoleRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.CreateOrganizationUserRoleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **userId** | **string** | The user&#39;s id. |  |
| **createOrganizationUserRoleRequest** | [**CreateOrganizationUserRoleRequest**](CreateOrganizationUserRoleRequest.md) | Role details. |  |

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
| **200** | Role successfully added. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteorganization"></a>
# **DeleteOrganization**
> SuccessResponse DeleteOrganization (string orgCode)

Delete Organization

Delete an organization.  Deletion is refused when a billing customer linked to the organization has agreements. Billing customers with no agreements are removed with the organization.  <div>   <code>delete:organizations</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class DeleteOrganizationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The identifier for the organization.

            try
            {
                // Delete Organization
                SuccessResponse result = apiInstance.DeleteOrganization(orgCode);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.DeleteOrganization: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteOrganizationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Organization
    ApiResponse<SuccessResponse> response = apiInstance.DeleteOrganizationWithHttpInfo(orgCode);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization. |  |

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
| **200** | Organization successfully deleted. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **404** | The specified resource was not found |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteorganizationfeatureflagoverride"></a>
# **DeleteOrganizationFeatureFlagOverride**
> SuccessResponse DeleteOrganizationFeatureFlagOverride (string orgCode, string featureFlagKey)

Delete Organization Feature Flag Override

Delete organization feature flag override.  <div>   <code>delete:organization_feature_flags</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class DeleteOrganizationFeatureFlagOverrideExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The identifier for the organization.
            var featureFlagKey = "featureFlagKey_example";  // string | The identifier for the feature flag.

            try
            {
                // Delete Organization Feature Flag Override
                SuccessResponse result = apiInstance.DeleteOrganizationFeatureFlagOverride(orgCode, featureFlagKey);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationFeatureFlagOverride: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteOrganizationFeatureFlagOverrideWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Organization Feature Flag Override
    ApiResponse<SuccessResponse> response = apiInstance.DeleteOrganizationFeatureFlagOverrideWithHttpInfo(orgCode, featureFlagKey);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationFeatureFlagOverrideWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization. |  |
| **featureFlagKey** | **string** | The identifier for the feature flag. |  |

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
| **200** | Feature flag override successfully deleted. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteorganizationfeatureflagoverrides"></a>
# **DeleteOrganizationFeatureFlagOverrides**
> SuccessResponse DeleteOrganizationFeatureFlagOverrides (string orgCode)

Delete Organization Feature Flag Overrides

Delete all organization feature flag overrides.  <div>   <code>delete:organization_feature_flags</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class DeleteOrganizationFeatureFlagOverridesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The identifier for the organization.

            try
            {
                // Delete Organization Feature Flag Overrides
                SuccessResponse result = apiInstance.DeleteOrganizationFeatureFlagOverrides(orgCode);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationFeatureFlagOverrides: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteOrganizationFeatureFlagOverridesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Organization Feature Flag Overrides
    ApiResponse<SuccessResponse> response = apiInstance.DeleteOrganizationFeatureFlagOverridesWithHttpInfo(orgCode);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationFeatureFlagOverridesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization. |  |

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
| **200** | Feature flag overrides successfully deleted. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteorganizationhandle"></a>
# **DeleteOrganizationHandle**
> SuccessResponse DeleteOrganizationHandle (string orgCode)

Delete organization handle

Delete organization handle  <div>   <code>delete:organization_handles</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class DeleteOrganizationHandleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The organization's code.

            try
            {
                // Delete organization handle
                SuccessResponse result = apiInstance.DeleteOrganizationHandle(orgCode);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationHandle: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteOrganizationHandleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete organization handle
    ApiResponse<SuccessResponse> response = apiInstance.DeleteOrganizationHandleWithHttpInfo(orgCode);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationHandleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |

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
| **200** | Handle successfully deleted. |  -  |
| **400** | Bad request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteorganizationinvite"></a>
# **DeleteOrganizationInvite**
> SuccessResponse DeleteOrganizationInvite (string orgCode, string inviteCode)

Delete organization invite

Revoke (delete) an invitation. This will mark the invitation as revoked and prevent it from being accepted.  <div>   <code>delete:organization_invites</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class DeleteOrganizationInviteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = org_1ccfb819462;  // string | The organization's code.
            var inviteCode = inv_abc123def456;  // string | The invitation's code.

            try
            {
                // Delete organization invite
                SuccessResponse result = apiInstance.DeleteOrganizationInvite(orgCode, inviteCode);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationInvite: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteOrganizationInviteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete organization invite
    ApiResponse<SuccessResponse> response = apiInstance.DeleteOrganizationInviteWithHttpInfo(orgCode, inviteCode);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationInviteWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **inviteCode** | **string** | The invitation&#39;s code. |  |

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
| **200** | Invitation successfully revoked. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **404** | The specified resource was not found |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteorganizationlogo"></a>
# **DeleteOrganizationLogo**
> SuccessResponse DeleteOrganizationLogo (string orgCode, string type)

Delete organization logo

Delete organization logo  <div>   <code>update:organizations</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class DeleteOrganizationLogoExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = org_1ccfb819462;  // string | The organization's code.
            var type = dark;  // string | The type of logo to delete.

            try
            {
                // Delete organization logo
                SuccessResponse result = apiInstance.DeleteOrganizationLogo(orgCode, type);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationLogo: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteOrganizationLogoWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete organization logo
    ApiResponse<SuccessResponse> response = apiInstance.DeleteOrganizationLogoWithHttpInfo(orgCode, type);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationLogoWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **type** | **string** | The type of logo to delete. |  |

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
| **200** | Organization logo successfully deleted |  -  |
| **204** | No logo found to delete |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteorganizationpermission"></a>
# **DeleteOrganizationPermission**
> SuccessResponse DeleteOrganizationPermission (string orgCode, string permissionId)

Delete organization-managed permission

Delete a permission the organization owns. The organization's roles that hold it lose it. This works on any plan.  <div>   <code>delete:organization_roles</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class DeleteOrganizationPermissionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var permissionId = "permissionId_example";  // string | The permission's ID.

            try
            {
                // Delete organization-managed permission
                SuccessResponse result = apiInstance.DeleteOrganizationPermission(orgCode, permissionId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationPermission: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteOrganizationPermissionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete organization-managed permission
    ApiResponse<SuccessResponse> response = apiInstance.DeleteOrganizationPermissionWithHttpInfo(orgCode, permissionId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationPermissionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **permissionId** | **string** | The permission&#39;s ID. |  |

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
| **200** | Permission successfully deleted. |  -  |
| **400** | Bad request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteorganizationrole"></a>
# **DeleteOrganizationRole**
> SuccessResponse DeleteOrganizationRole (string orgCode, string roleId)

Delete organization-managed role

Delete a role the organization owns. Members who hold it lose it. This works on any plan, so roles made while on Scale can be cleaned up after a downgrade.  <div>   <code>delete:organization_roles</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class DeleteOrganizationRoleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var roleId = "roleId_example";  // string | The role's ID.

            try
            {
                // Delete organization-managed role
                SuccessResponse result = apiInstance.DeleteOrganizationRole(orgCode, roleId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationRole: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteOrganizationRoleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete organization-managed role
    ApiResponse<SuccessResponse> response = apiInstance.DeleteOrganizationRoleWithHttpInfo(orgCode, roleId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationRoleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **roleId** | **string** | The role&#39;s ID. |  |

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
| **200** | Role successfully deleted. |  -  |
| **400** | Bad request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteorganizationuserapiscope"></a>
# **DeleteOrganizationUserAPIScope**
> void DeleteOrganizationUserAPIScope (string orgCode, string userId, string apiId, string scopeId)

Delete scope from organization user API

Delete a scope from an organization user api you previously created.  <div>   <code>delete:organization_user_api_scopes</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class DeleteOrganizationUserAPIScopeExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The identifier for the organization.
            var userId = kp_5ce676e5d6a24bc9aac2fba35a46e958;  // string | User ID
            var apiId = 838f208d006a482dbd8cdb79a9889f68;  // string | API ID
            var scopeId = api_scope_019391daf58d87d8a7213419c016ac95;  // string | Scope ID

            try
            {
                // Delete scope from organization user API
                apiInstance.DeleteOrganizationUserAPIScope(orgCode, userId, apiId, scopeId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationUserAPIScope: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteOrganizationUserAPIScopeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete scope from organization user API
    apiInstance.DeleteOrganizationUserAPIScopeWithHttpInfo(orgCode, userId, apiId, scopeId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationUserAPIScopeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization. |  |
| **userId** | **string** | User ID |  |
| **apiId** | **string** | API ID |  |
| **scopeId** | **string** | Scope ID |  |

### Return type

void (empty response body)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Organization user API scope successfully deleted. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteorganizationuserpermission"></a>
# **DeleteOrganizationUserPermission**
> SuccessResponse DeleteOrganizationUserPermission (string orgCode, string userId, string permissionId)

Delete Organization User Permission

Delete permission for an organization user.  <div>   <code>delete:organization_user_permissions</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class DeleteOrganizationUserPermissionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var userId = "userId_example";  // string | The user's id.
            var permissionId = "permissionId_example";  // string | The permission id.

            try
            {
                // Delete Organization User Permission
                SuccessResponse result = apiInstance.DeleteOrganizationUserPermission(orgCode, userId, permissionId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationUserPermission: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteOrganizationUserPermissionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Organization User Permission
    ApiResponse<SuccessResponse> response = apiInstance.DeleteOrganizationUserPermissionWithHttpInfo(orgCode, userId, permissionId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationUserPermissionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **userId** | **string** | The user&#39;s id. |  |
| **permissionId** | **string** | The permission id. |  |

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
| **200** | User successfully removed. |  -  |
| **400** | Error creating user. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteorganizationuserrole"></a>
# **DeleteOrganizationUserRole**
> SuccessResponse DeleteOrganizationUserRole (string orgCode, string userId, string roleId)

Delete Organization User Role

Delete role for an organization user.  Roles cannot be removed from users in organizations managed by directory sync (`ORGANIZATION_MANAGED_BY_DIRECTORY_SYNC`); user and role changes for those organizations must be made in the upstream identity provider.  <div>   <code>delete:organization_user_roles</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class DeleteOrganizationUserRoleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var userId = "userId_example";  // string | The user's id.
            var roleId = "roleId_example";  // string | The role id.

            try
            {
                // Delete Organization User Role
                SuccessResponse result = apiInstance.DeleteOrganizationUserRole(orgCode, userId, roleId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationUserRole: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteOrganizationUserRoleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Organization User Role
    ApiResponse<SuccessResponse> response = apiInstance.DeleteOrganizationUserRoleWithHttpInfo(orgCode, userId, roleId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.DeleteOrganizationUserRoleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **userId** | **string** | The user&#39;s id. |  |
| **roleId** | **string** | The role id. |  |

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
| **200** | User successfully removed. |  -  |
| **400** | Error creating user. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="enableorgconnection"></a>
# **EnableOrgConnection**
> void EnableOrgConnection (string organizationCode, string connectionId)

Enable connection

Enable an auth connection for an organization.  <div>   <code>create:organization_connections</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class EnableOrgConnectionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var organizationCode = org_7d45b01ef13;  // string | The unique code for the organization.
            var connectionId = conn_0192c16abb53b44277e597d31877ba5b;  // string | The identifier for the connection.

            try
            {
                // Enable connection
                apiInstance.EnableOrgConnection(organizationCode, connectionId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.EnableOrgConnection: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the EnableOrgConnectionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Enable connection
    apiInstance.EnableOrgConnectionWithHttpInfo(organizationCode, connectionId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.EnableOrgConnectionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **organizationCode** | **string** | The unique code for the organization. |  |
| **connectionId** | **string** | The identifier for the connection. |  |

### Return type

void (empty response body)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Connection successfully enabled. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getorgusermfa"></a>
# **GetOrgUserMFA**
> GetUserMfaResponse GetOrgUserMFA (string orgCode, string userId)

Get an organization user's MFA configuration

Get an organization user’s MFA configuration.  <div>   <code>read:organization_user_mfa</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetOrgUserMFAExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = org_1ccfb819462;  // string | The identifier for the organization.
            var userId = kp_c3143a4b50ad43c88e541d9077681782;  // string | The identifier for the user

            try
            {
                // Get an organization user's MFA configuration
                GetUserMfaResponse result = apiInstance.GetOrgUserMFA(orgCode, userId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrgUserMFA: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrgUserMFAWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get an organization user's MFA configuration
    ApiResponse<GetUserMfaResponse> response = apiInstance.GetOrgUserMFAWithHttpInfo(orgCode, userId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrgUserMFAWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization. |  |
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

<a id="getorganization"></a>
# **GetOrganization**
> GetOrganizationResponse GetOrganization (string code, string? expand = null)

Get organization

Retrieve organization details by code.  <div>   <code>read:organizations</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetOrganizationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var code = org_1ccfb819462;  // string | The organization's code.
            var expand = billing;  // string? | Additional data to include in the response. Allowed value: \"billing\". (optional) 

            try
            {
                // Get organization
                GetOrganizationResponse result = apiInstance.GetOrganization(code, expand);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrganization: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrganizationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get organization
    ApiResponse<GetOrganizationResponse> response = apiInstance.GetOrganizationWithHttpInfo(code, expand);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrganizationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **code** | **string** | The organization&#39;s code. |  |
| **expand** | **string?** | Additional data to include in the response. Allowed value: \&quot;billing\&quot;. | [optional]  |

### Return type

[**GetOrganizationResponse**](GetOrganizationResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Organization successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getorganizationconnections"></a>
# **GetOrganizationConnections**
> GetConnectionsResponse GetOrganizationConnections (string organizationCode)

Get connections

Gets all connections for an organization.  <div>   <code>read:organization_connections</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetOrganizationConnectionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var organizationCode = org_7d45b01ef13;  // string | The organization code.

            try
            {
                // Get connections
                GetConnectionsResponse result = apiInstance.GetOrganizationConnections(organizationCode);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrganizationConnections: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrganizationConnectionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get connections
    ApiResponse<GetConnectionsResponse> response = apiInstance.GetOrganizationConnectionsWithHttpInfo(organizationCode);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrganizationConnectionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **organizationCode** | **string** | The organization code. |  |

### Return type

[**GetConnectionsResponse**](GetConnectionsResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Organization connections successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getorganizationfeatureflags"></a>
# **GetOrganizationFeatureFlags**
> GetOrganizationFeatureFlagsResponse GetOrganizationFeatureFlags (string orgCode)

List Organization Feature Flags

Get all organization feature flags.  <div>   <code>read:organization_feature_flags</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetOrganizationFeatureFlagsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The identifier for the organization.

            try
            {
                // List Organization Feature Flags
                GetOrganizationFeatureFlagsResponse result = apiInstance.GetOrganizationFeatureFlags(orgCode);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrganizationFeatureFlags: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrganizationFeatureFlagsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Organization Feature Flags
    ApiResponse<GetOrganizationFeatureFlagsResponse> response = apiInstance.GetOrganizationFeatureFlagsWithHttpInfo(orgCode);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrganizationFeatureFlagsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization. |  |

### Return type

[**GetOrganizationFeatureFlagsResponse**](GetOrganizationFeatureFlagsResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Feature flag overrides successfully returned. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getorganizationinvite"></a>
# **GetOrganizationInvite**
> GetOrganizationInviteResponse GetOrganizationInvite (string orgCode, string inviteCode)

Get organization invite

Get details of a specific invitation by its code.  <div>   <code>read:organization_invites</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetOrganizationInviteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = org_1ccfb819462;  // string | The organization's code.
            var inviteCode = inv_abc123def456;  // string | The invitation's code.

            try
            {
                // Get organization invite
                GetOrganizationInviteResponse result = apiInstance.GetOrganizationInvite(orgCode, inviteCode);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrganizationInvite: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrganizationInviteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get organization invite
    ApiResponse<GetOrganizationInviteResponse> response = apiInstance.GetOrganizationInviteWithHttpInfo(orgCode, inviteCode);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrganizationInviteWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **inviteCode** | **string** | The invitation&#39;s code. |  |

### Return type

[**GetOrganizationInviteResponse**](GetOrganizationInviteResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Invitation successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **404** | The specified resource was not found |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getorganizationinvites"></a>
# **GetOrganizationInvites**
> GetOrganizationInvitesResponse GetOrganizationInvites (string orgCode, string? sort = null, int? pageSize = null, string? nextToken = null, bool? includeRevoked = null, bool? includeAccepted = null)

Get organization invites

Get a list of invitations for an organization. By default, only pending (non-revoked, non-accepted) invitations are returned.  <div>   <code>read:organization_invites</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetOrganizationInvitesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = org_1ccfb819462;  // string | The organization's code.
            var sort = created_on_desc;  // string? | Field and order to sort the result by. (optional) 
            var pageSize = 10;  // int? | Number of results per page. Defaults to 10 if parameter not sent. (optional) 
            var nextToken = "nextToken_example";  // string? | A string to get the next page of results if there are more results. (optional) 
            var includeRevoked = false;  // bool? | Include revoked invitations in the results. (optional)  (default to false)
            var includeAccepted = false;  // bool? | Include accepted invitations in the results. (optional)  (default to false)

            try
            {
                // Get organization invites
                GetOrganizationInvitesResponse result = apiInstance.GetOrganizationInvites(orgCode, sort, pageSize, nextToken, includeRevoked, includeAccepted);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrganizationInvites: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrganizationInvitesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get organization invites
    ApiResponse<GetOrganizationInvitesResponse> response = apiInstance.GetOrganizationInvitesWithHttpInfo(orgCode, sort, pageSize, nextToken, includeRevoked, includeAccepted);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrganizationInvitesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **sort** | **string?** | Field and order to sort the result by. | [optional]  |
| **pageSize** | **int?** | Number of results per page. Defaults to 10 if parameter not sent. | [optional]  |
| **nextToken** | **string?** | A string to get the next page of results if there are more results. | [optional]  |
| **includeRevoked** | **bool?** | Include revoked invitations in the results. | [optional] [default to false] |
| **includeAccepted** | **bool?** | Include accepted invitations in the results. | [optional] [default to false] |

### Return type

[**GetOrganizationInvitesResponse**](GetOrganizationInvitesResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Invitations successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getorganizationmfa"></a>
# **GetOrganizationMFA**
> GetOrganizationMFA200Response GetOrganizationMFA (string orgCode)

Get Organization MFA Configuration

Get the MFA configuration for an organization, including the enterprise connections exempt from organization MFA.  <div>   <code>read:organization_mfa</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetOrganizationMFAExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = org_1ccfb819462;  // string | The identifier for the organization

            try
            {
                // Get Organization MFA Configuration
                GetOrganizationMFA200Response result = apiInstance.GetOrganizationMFA(orgCode);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrganizationMFA: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrganizationMFAWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Organization MFA Configuration
    ApiResponse<GetOrganizationMFA200Response> response = apiInstance.GetOrganizationMFAWithHttpInfo(orgCode);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrganizationMFAWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization |  |

### Return type

[**GetOrganizationMFA200Response**](GetOrganizationMFA200Response.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Organization MFA configuration successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getorganizationpasskey"></a>
# **GetOrganizationPasskey**
> GetOrganizationPasskey200Response GetOrganizationPasskey (string orgCode)

Get organization passkey settings

Retrieve passkey settings for an organization, including whether the organization overrides the environment default.  <div>   <code>read:organization_passkey</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetOrganizationPasskeyExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = org_1ccfb819462;  // string | The organization's code.

            try
            {
                // Get organization passkey settings
                GetOrganizationPasskey200Response result = apiInstance.GetOrganizationPasskey(orgCode);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrganizationPasskey: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrganizationPasskeyWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get organization passkey settings
    ApiResponse<GetOrganizationPasskey200Response> response = apiInstance.GetOrganizationPasskeyWithHttpInfo(orgCode);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrganizationPasskeyWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |

### Return type

[**GetOrganizationPasskey200Response**](GetOrganizationPasskey200Response.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Organization passkey settings successfully retrieved. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getorganizationpermission"></a>
# **GetOrganizationPermission**
> GetOrganizationPermissionResponse GetOrganizationPermission (string orgCode, string permissionId)

Get organization-managed permission

Get a permission the organization owns.  <div>   <code>read:organization_roles</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetOrganizationPermissionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var permissionId = "permissionId_example";  // string | The permission's ID.

            try
            {
                // Get organization-managed permission
                GetOrganizationPermissionResponse result = apiInstance.GetOrganizationPermission(orgCode, permissionId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrganizationPermission: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrganizationPermissionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get organization-managed permission
    ApiResponse<GetOrganizationPermissionResponse> response = apiInstance.GetOrganizationPermissionWithHttpInfo(orgCode, permissionId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrganizationPermissionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **permissionId** | **string** | The permission&#39;s ID. |  |

### Return type

[**GetOrganizationPermissionResponse**](GetOrganizationPermissionResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Permission successfully retrieved. |  -  |
| **400** | Bad request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getorganizationpermissions"></a>
# **GetOrganizationPermissions**
> GetOrganizationPermissionsResponse GetOrganizationPermissions (string orgCode, string? sort = null, int? pageSize = null, string? nextToken = null)

List organization-managed permissions

List the permissions an organization has defined for itself. Business-wide permissions are not included; use the permissions endpoints for those.  Organization-managed permissions are permissions an organization defines for itself. Only that organization's own roles can hold them, they appear in tokens through those roles, and they are available on the Scale plan alongside organization-managed roles (they use the same scopes). If the business moves off Scale they stay stored but stop appearing in tokens. They are not available on the Kinde business.  <div>   <code>read:organization_roles</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetOrganizationPermissionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var sort = "name_asc";  // string? | Field and order to sort the result by. Defaults to key_asc. (optional) 
            var pageSize = 56;  // int? | Number of results per page. Defaults to 10, at most 500. (optional) 
            var nextToken = "nextToken_example";  // string? | A string to get the next page of results if there are more results. (optional) 

            try
            {
                // List organization-managed permissions
                GetOrganizationPermissionsResponse result = apiInstance.GetOrganizationPermissions(orgCode, sort, pageSize, nextToken);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrganizationPermissions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrganizationPermissionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List organization-managed permissions
    ApiResponse<GetOrganizationPermissionsResponse> response = apiInstance.GetOrganizationPermissionsWithHttpInfo(orgCode, sort, pageSize, nextToken);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrganizationPermissionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **sort** | **string?** | Field and order to sort the result by. Defaults to key_asc. | [optional]  |
| **pageSize** | **int?** | Number of results per page. Defaults to 10, at most 500. | [optional]  |
| **nextToken** | **string?** | A string to get the next page of results if there are more results. | [optional]  |

### Return type

[**GetOrganizationPermissionsResponse**](GetOrganizationPermissionsResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Organization permissions successfully retrieved. |  -  |
| **400** | Bad request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getorganizationpropertyvalues"></a>
# **GetOrganizationPropertyValues**
> GetPropertyValuesResponse GetOrganizationPropertyValues (string orgCode)

Get Organization Property Values

Gets properties for an organization by org code.  <div>   <code>read:organization_properties</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetOrganizationPropertyValuesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The organization's code.

            try
            {
                // Get Organization Property Values
                GetPropertyValuesResponse result = apiInstance.GetOrganizationPropertyValues(orgCode);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrganizationPropertyValues: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrganizationPropertyValuesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Organization Property Values
    ApiResponse<GetPropertyValuesResponse> response = apiInstance.GetOrganizationPropertyValuesWithHttpInfo(orgCode);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrganizationPropertyValuesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |

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

<a id="getorganizationrole"></a>
# **GetOrganizationRole**
> GetOrganizationRoleResponse GetOrganizationRole (string orgCode, string roleId)

Get organization-managed role

Get a role the organization owns, with its permissions.  <div>   <code>read:organization_roles</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetOrganizationRoleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var roleId = "roleId_example";  // string | The role's ID.

            try
            {
                // Get organization-managed role
                GetOrganizationRoleResponse result = apiInstance.GetOrganizationRole(orgCode, roleId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrganizationRole: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrganizationRoleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get organization-managed role
    ApiResponse<GetOrganizationRoleResponse> response = apiInstance.GetOrganizationRoleWithHttpInfo(orgCode, roleId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrganizationRoleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **roleId** | **string** | The role&#39;s ID. |  |

### Return type

[**GetOrganizationRoleResponse**](GetOrganizationRoleResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Role successfully retrieved. |  -  |
| **400** | Bad request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getorganizationroleactiveuserscount"></a>
# **GetOrganizationRoleActiveUsersCount**
> GetOrganizationRoleActiveUsersCountResponse GetOrganizationRoleActiveUsersCount (string orgCode, string roleId, DateTimeOffset dateTimeFrom, DateTimeOffset dateTimeTo)

Get organization role active users count

Get the number of active users that hold a given role within a specific organization.  A user is counted as active if they were issued at least one access token during the requested period, regardless of organization context on the token. Only users who currently hold the role in the organization are included. The count is scoped to the current environment.  Both `date_time_from` and `date_time_to` are required, inclusive, and must be ISO 8601 datetimes in UTC. Provide them at second precision (no fractional seconds). If a value includes fractional seconds, it is normalized before any other processing: `date_time_from` is rounded down and `date_time_to` is rounded up to the nearest second. Window validation, the active-user query, and the echoed response bounds all use those normalized values.  The requested window must not exceed 3 days. Because both bounds are inclusive, this means `date_time_to` must be earlier than `date_time_from` plus 3 days. For example, `2026-07-01T00:00:00Z` to `2026-07-03T23:59:59Z` is a full 3 day window covering the whole of 1, 2 and 3 July. A longer window is rejected with `DATE_TIME_RANGE_TOO_LARGE`.  <div>   <code>read:organization_user_roles</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetOrganizationRoleActiveUsersCountExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var roleId = "roleId_example";  // string | The role's public id.
            var dateTimeFrom = 2026-07-01T00:00Z;  // DateTimeOffset | Start of the active period (inclusive), as an ISO 8601 datetime in UTC at second precision. Fractional seconds are accepted but rounded down to the nearest second before validation.
            var dateTimeTo = 2026-07-03T23:59:59Z;  // DateTimeOffset | End of the active period (inclusive), as an ISO 8601 datetime in UTC at second precision. Fractional seconds are accepted but rounded up to the nearest second before validation. The window must not exceed 3 days, so this must be earlier than `date_time_from` plus 3 days.

            try
            {
                // Get organization role active users count
                GetOrganizationRoleActiveUsersCountResponse result = apiInstance.GetOrganizationRoleActiveUsersCount(orgCode, roleId, dateTimeFrom, dateTimeTo);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrganizationRoleActiveUsersCount: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrganizationRoleActiveUsersCountWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get organization role active users count
    ApiResponse<GetOrganizationRoleActiveUsersCountResponse> response = apiInstance.GetOrganizationRoleActiveUsersCountWithHttpInfo(orgCode, roleId, dateTimeFrom, dateTimeTo);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrganizationRoleActiveUsersCountWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **roleId** | **string** | The role&#39;s public id. |  |
| **dateTimeFrom** | **DateTimeOffset** | Start of the active period (inclusive), as an ISO 8601 datetime in UTC at second precision. Fractional seconds are accepted but rounded down to the nearest second before validation. |  |
| **dateTimeTo** | **DateTimeOffset** | End of the active period (inclusive), as an ISO 8601 datetime in UTC at second precision. Fractional seconds are accepted but rounded up to the nearest second before validation. The window must not exceed 3 days, so this must be earlier than &#x60;date_time_from&#x60; plus 3 days. |  |

### Return type

[**GetOrganizationRoleActiveUsersCountResponse**](GetOrganizationRoleActiveUsersCountResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Active users count for the role in the organization. |  -  |
| **400** | Bad request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getorganizationroleusers"></a>
# **GetOrganizationRoleUsers**
> GetOrganizationRoleUsersResponse GetOrganizationRoleUsers (string orgCode, string roleId, int? pageSize = null, string? nextToken = null)

List organization role users

Get users that have a given role within a specific organization.  <div>   <code>read:organization_user_roles</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetOrganizationRoleUsersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var roleId = "roleId_example";  // string | The role's public id.
            var pageSize = 56;  // int? | Number of results per page. Defaults to 10 if parameter not sent. (optional) 
            var nextToken = "nextToken_example";  // string? | A string to get the next page of results if there are more results. (optional) 

            try
            {
                // List organization role users
                GetOrganizationRoleUsersResponse result = apiInstance.GetOrganizationRoleUsers(orgCode, roleId, pageSize, nextToken);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrganizationRoleUsers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrganizationRoleUsersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List organization role users
    ApiResponse<GetOrganizationRoleUsersResponse> response = apiInstance.GetOrganizationRoleUsersWithHttpInfo(orgCode, roleId, pageSize, nextToken);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrganizationRoleUsersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **roleId** | **string** | The role&#39;s public id. |  |
| **pageSize** | **int?** | Number of results per page. Defaults to 10 if parameter not sent. | [optional]  |
| **nextToken** | **string?** | A string to get the next page of results if there are more results. | [optional]  |

### Return type

[**GetOrganizationRoleUsersResponse**](GetOrganizationRoleUsersResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A list of users with the specified role in the organization. |  -  |
| **400** | Bad request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getorganizationroleuserscount"></a>
# **GetOrganizationRoleUsersCount**
> GetOrganizationRoleUsersCountResponse GetOrganizationRoleUsersCount (string orgCode, string roleId)

Count organization role users

Get the number of users that have a given role within a specific organization.  <div>   <code>read:organization_user_roles</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetOrganizationRoleUsersCountExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var roleId = "roleId_example";  // string | The role's public id.

            try
            {
                // Count organization role users
                GetOrganizationRoleUsersCountResponse result = apiInstance.GetOrganizationRoleUsersCount(orgCode, roleId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrganizationRoleUsersCount: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrganizationRoleUsersCountWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Count organization role users
    ApiResponse<GetOrganizationRoleUsersCountResponse> response = apiInstance.GetOrganizationRoleUsersCountWithHttpInfo(orgCode, roleId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrganizationRoleUsersCountWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **roleId** | **string** | The role&#39;s public id. |  |

### Return type

[**GetOrganizationRoleUsersCountResponse**](GetOrganizationRoleUsersCountResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | The number of users with the specified role in the organization. |  -  |
| **400** | Bad request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getorganizationroles"></a>
# **GetOrganizationRoles**
> GetOrganizationRolesResponse GetOrganizationRoles (string orgCode, string? sort = null, int? pageSize = null, string? nextToken = null)

List organization-managed roles

List the roles an organization has created for itself. Business-wide roles are not included; use the roles endpoints for those.  Organization-managed roles are roles an organization owns. They can only be assigned to members of that organization, they hold only the permissions the business marks as `is_organization_role_assignable`, and they are available on the Scale plan. If the business moves off Scale they stay stored but stop appearing in tokens and permission checks.  <div>   <code>read:organization_roles</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetOrganizationRolesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var sort = "name_asc";  // string? | Field and order to sort the result by. (optional) 
            var pageSize = 56;  // int? | Number of results per page. Defaults to 10, at most 500. (optional) 
            var nextToken = "nextToken_example";  // string? | A string to get the next page of results if there are more results. (optional) 

            try
            {
                // List organization-managed roles
                GetOrganizationRolesResponse result = apiInstance.GetOrganizationRoles(orgCode, sort, pageSize, nextToken);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrganizationRoles: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrganizationRolesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List organization-managed roles
    ApiResponse<GetOrganizationRolesResponse> response = apiInstance.GetOrganizationRolesWithHttpInfo(orgCode, sort, pageSize, nextToken);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrganizationRolesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **sort** | **string?** | Field and order to sort the result by. | [optional]  |
| **pageSize** | **int?** | Number of results per page. Defaults to 10, at most 500. | [optional]  |
| **nextToken** | **string?** | A string to get the next page of results if there are more results. | [optional]  |

### Return type

[**GetOrganizationRolesResponse**](GetOrganizationRolesResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Organization roles successfully retrieved. |  -  |
| **400** | Bad request. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getorganizationuserpermissions"></a>
# **GetOrganizationUserPermissions**
> GetOrganizationsUserPermissionsResponse GetOrganizationUserPermissions (string orgCode, string userId, string? expand = null)

List Organization User Permissions

Get permissions for an organization user.  <div>   <code>read:organization_user_permissions</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetOrganizationUserPermissionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var userId = "userId_example";  // string | The user's id.
            var expand = "expand_example";  // string? | Additional data to include in the response. Allowed value: \"roles\". (optional) 

            try
            {
                // List Organization User Permissions
                GetOrganizationsUserPermissionsResponse result = apiInstance.GetOrganizationUserPermissions(orgCode, userId, expand);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrganizationUserPermissions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrganizationUserPermissionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Organization User Permissions
    ApiResponse<GetOrganizationsUserPermissionsResponse> response = apiInstance.GetOrganizationUserPermissionsWithHttpInfo(orgCode, userId, expand);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrganizationUserPermissionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **userId** | **string** | The user&#39;s id. |  |
| **expand** | **string?** | Additional data to include in the response. Allowed value: \&quot;roles\&quot;. | [optional]  |

### Return type

[**GetOrganizationsUserPermissionsResponse**](GetOrganizationsUserPermissionsResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A successful response with a list of user permissions. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getorganizationuserroles"></a>
# **GetOrganizationUserRoles**
> GetOrganizationsUserRolesResponse GetOrganizationUserRoles (string orgCode, string userId)

List Organization User Roles

Get roles for an organization user.  <div>   <code>read:organization_user_roles</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetOrganizationUserRolesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var userId = "userId_example";  // string | The user's id.

            try
            {
                // List Organization User Roles
                GetOrganizationsUserRolesResponse result = apiInstance.GetOrganizationUserRoles(orgCode, userId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrganizationUserRoles: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrganizationUserRolesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Organization User Roles
    ApiResponse<GetOrganizationsUserRolesResponse> response = apiInstance.GetOrganizationUserRolesWithHttpInfo(orgCode, userId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrganizationUserRolesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **userId** | **string** | The user&#39;s id. |  |

### Return type

[**GetOrganizationsUserRolesResponse**](GetOrganizationsUserRolesResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/json; charset=utf-8


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A successful response with a list of user roles. |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getorganizationusers"></a>
# **GetOrganizationUsers**
> GetOrganizationUsersResponse GetOrganizationUsers (string orgCode, string? sort = null, int? pageSize = null, string? nextToken = null, string? permissions = null, string? roles = null)

Get organization users

Get user details for all members of an organization.  <div>   <code>read:organization_users</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetOrganizationUsersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = org_1ccfb819462;  // string | The organization's code.
            var sort = email_asc;  // string? | Field and order to sort the result by. (optional) 
            var pageSize = 10;  // int? | Number of results per page. Defaults to 10 if parameter not sent. (optional) 
            var nextToken = MTo6OmlkX2FzYw==;  // string? | A string to get the next page of results if there are more results. (optional) 
            var permissions = admin;  // string? | Filter by user permissions comma separated (where all match) (optional) 
            var roles = manager;  // string? | Filter by user roles comma separated (where all match) (optional) 

            try
            {
                // Get organization users
                GetOrganizationUsersResponse result = apiInstance.GetOrganizationUsers(orgCode, sort, pageSize, nextToken, permissions, roles);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrganizationUsers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrganizationUsersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get organization users
    ApiResponse<GetOrganizationUsersResponse> response = apiInstance.GetOrganizationUsersWithHttpInfo(orgCode, sort, pageSize, nextToken, permissions, roles);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrganizationUsersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **sort** | **string?** | Field and order to sort the result by. | [optional]  |
| **pageSize** | **int?** | Number of results per page. Defaults to 10 if parameter not sent. | [optional]  |
| **nextToken** | **string?** | A string to get the next page of results if there are more results. | [optional]  |
| **permissions** | **string?** | Filter by user permissions comma separated (where all match) | [optional]  |
| **roles** | **string?** | Filter by user roles comma separated (where all match) | [optional]  |

### Return type

[**GetOrganizationUsersResponse**](GetOrganizationUsersResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A successful response with a list of organization users or an empty list. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getorganizations"></a>
# **GetOrganizations**
> GetOrganizationsResponse GetOrganizations (string? sort = null, int? pageSize = null, string? nextToken = null)

Get organizations

Get a list of organizations.  <div>   <code>read:organizations</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class GetOrganizationsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var sort = "name_asc";  // string? | Field and order to sort the result by. (optional) 
            var pageSize = 56;  // int? | Number of results per page. Defaults to 10 if parameter not sent. (optional) 
            var nextToken = "nextToken_example";  // string? | A string to get the next page of results if there are more results. (optional) 

            try
            {
                // Get organizations
                GetOrganizationsResponse result = apiInstance.GetOrganizations(sort, pageSize, nextToken);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.GetOrganizations: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOrganizationsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get organizations
    ApiResponse<GetOrganizationsResponse> response = apiInstance.GetOrganizationsWithHttpInfo(sort, pageSize, nextToken);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.GetOrganizationsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **sort** | **string?** | Field and order to sort the result by. | [optional]  |
| **pageSize** | **int?** | Number of results per page. Defaults to 10 if parameter not sent. | [optional]  |
| **nextToken** | **string?** | A string to get the next page of results if there are more results. | [optional]  |

### Return type

[**GetOrganizationsResponse**](GetOrganizationsResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Organizations successfully retreived. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="readorganizationlogo"></a>
# **ReadOrganizationLogo**
> ReadLogoResponse ReadOrganizationLogo (string orgCode)

Read organization logo details

Read organization logo details  <div>   <code>read:organizations</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class ReadOrganizationLogoExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = org_1ccfb819462;  // string | The organization's code.

            try
            {
                // Read organization logo details
                ReadLogoResponse result = apiInstance.ReadOrganizationLogo(orgCode);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.ReadOrganizationLogo: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ReadOrganizationLogoWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Read organization logo details
    ApiResponse<ReadLogoResponse> response = apiInstance.ReadOrganizationLogoWithHttpInfo(orgCode);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.ReadOrganizationLogoWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |

### Return type

[**ReadLogoResponse**](ReadLogoResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successfully retrieved organization logo details |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="removeorgconnection"></a>
# **RemoveOrgConnection**
> SuccessResponse RemoveOrgConnection (string organizationCode, string connectionId)

Remove connection

Turn off an auth connection for an organization  <div>   <code>delete:organization_connections</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class RemoveOrgConnectionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var organizationCode = org_7d45b01ef13;  // string | The unique code for the organization.
            var connectionId = conn_0192c16abb53b44277e597d31877ba5b;  // string | The identifier for the connection.

            try
            {
                // Remove connection
                SuccessResponse result = apiInstance.RemoveOrgConnection(organizationCode, connectionId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.RemoveOrgConnection: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RemoveOrgConnectionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Remove connection
    ApiResponse<SuccessResponse> response = apiInstance.RemoveOrgConnectionWithHttpInfo(organizationCode, connectionId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.RemoveOrgConnectionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **organizationCode** | **string** | The unique code for the organization. |  |
| **connectionId** | **string** | The identifier for the connection. |  |

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
| **200** | Connection successfully removed. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="removeorganizationuser"></a>
# **RemoveOrganizationUser**
> SuccessResponse RemoveOrganizationUser (string orgCode, string userId)

Remove Organization User

Remove user from an organization.  Users cannot be removed from organizations that are managed by directory sync (`ORGANIZATION_MANAGED_BY_DIRECTORY_SYNC`); user and role changes for those organizations must be made in the upstream identity provider.  <div>   <code>delete:organization_users</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class RemoveOrganizationUserExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var userId = "userId_example";  // string | The user's id.

            try
            {
                // Remove Organization User
                SuccessResponse result = apiInstance.RemoveOrganizationUser(orgCode, userId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.RemoveOrganizationUser: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RemoveOrganizationUserWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Remove Organization User
    ApiResponse<SuccessResponse> response = apiInstance.RemoveOrganizationUserWithHttpInfo(orgCode, userId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.RemoveOrganizationUserWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **userId** | **string** | The user&#39;s id. |  |

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
| **200** | User successfully removed from organization |  -  |
| **400** | Error removing user |  -  |
| **403** | Invalid credentials. |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="replaceorganizationmfa"></a>
# **ReplaceOrganizationMFA**
> SuccessResponse ReplaceOrganizationMFA (string orgCode, ReplaceOrganizationMFARequest replaceOrganizationMFARequest)

Replace Organization MFA Configuration

Replace Organization MFA Configuration.  <div>   <code>update:organization_mfa</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class ReplaceOrganizationMFAExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The identifier for the organization
            var replaceOrganizationMFARequest = new ReplaceOrganizationMFARequest(); // ReplaceOrganizationMFARequest | MFA details.

            try
            {
                // Replace Organization MFA Configuration
                SuccessResponse result = apiInstance.ReplaceOrganizationMFA(orgCode, replaceOrganizationMFARequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.ReplaceOrganizationMFA: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ReplaceOrganizationMFAWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Replace Organization MFA Configuration
    ApiResponse<SuccessResponse> response = apiInstance.ReplaceOrganizationMFAWithHttpInfo(orgCode, replaceOrganizationMFARequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.ReplaceOrganizationMFAWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization |  |
| **replaceOrganizationMFARequest** | [**ReplaceOrganizationMFARequest**](ReplaceOrganizationMFARequest.md) | MFA details. |  |

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
| **200** | MFA Configuration updated successfully. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="resetorgusermfa"></a>
# **ResetOrgUserMFA**
> SuccessResponse ResetOrgUserMFA (string orgCode, string userId, string factorId)

Reset specific organization MFA for a user

Reset a specific organization MFA factor for a user.  <div>   <code>delete:organization_user_mfa</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class ResetOrgUserMFAExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = org_1ccfb819462;  // string | The identifier for the organization.
            var userId = kp_c3143a4b50ad43c88e541d9077681782;  // string | The identifier for the user
            var factorId = mfa_0193278a00ac29b3f6d4e4d462d55c47;  // string | The identifier for the MFA factor

            try
            {
                // Reset specific organization MFA for a user
                SuccessResponse result = apiInstance.ResetOrgUserMFA(orgCode, userId, factorId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.ResetOrgUserMFA: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ResetOrgUserMFAWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Reset specific organization MFA for a user
    ApiResponse<SuccessResponse> response = apiInstance.ResetOrgUserMFAWithHttpInfo(orgCode, userId, factorId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.ResetOrgUserMFAWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization. |  |
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

<a id="resetorgusermfaall"></a>
# **ResetOrgUserMFAAll**
> SuccessResponse ResetOrgUserMFAAll (string orgCode, string userId)

Reset all organization MFA for a user

Reset all organization MFA factors for a user.  <div>   <code>delete:organization_user_mfa</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class ResetOrgUserMFAAllExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = org_1ccfb819462;  // string | The identifier for the organization.
            var userId = kp_c3143a4b50ad43c88e541d9077681782;  // string | The identifier for the user

            try
            {
                // Reset all organization MFA for a user
                SuccessResponse result = apiInstance.ResetOrgUserMFAAll(orgCode, userId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.ResetOrgUserMFAAll: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ResetOrgUserMFAAllWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Reset all organization MFA for a user
    ApiResponse<SuccessResponse> response = apiInstance.ResetOrgUserMFAAllWithHttpInfo(orgCode, userId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.ResetOrgUserMFAAllWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization. |  |
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

<a id="updateorganization"></a>
# **UpdateOrganization**
> SuccessResponse UpdateOrganization (string orgCode, string? expand = null, UpdateOrganizationRequest? updateOrganizationRequest = null)

Update Organization

Update an organization.  When the organization name is updated and the organization is a billing customer, the change is also propagated to the corresponding billing customer details.  <div>   <code>update:organizations</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class UpdateOrganizationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = org_1ccfb819462;  // string | The identifier for the organization.
            var expand = "billing";  // string? | Additional data to include in the response. Allowed value: \"billing\". (optional) 
            var updateOrganizationRequest = new UpdateOrganizationRequest?(); // UpdateOrganizationRequest? | Organization details. (optional) 

            try
            {
                // Update Organization
                SuccessResponse result = apiInstance.UpdateOrganization(orgCode, expand, updateOrganizationRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.UpdateOrganization: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateOrganizationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Organization
    ApiResponse<SuccessResponse> response = apiInstance.UpdateOrganizationWithHttpInfo(orgCode, expand, updateOrganizationRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization. |  |
| **expand** | **string?** | Additional data to include in the response. Allowed value: \&quot;billing\&quot;. | [optional]  |
| **updateOrganizationRequest** | [**UpdateOrganizationRequest?**](UpdateOrganizationRequest?.md) | Organization details. | [optional]  |

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
| **200** | Organization successfully updated. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateorganizationfeatureflagoverride"></a>
# **UpdateOrganizationFeatureFlagOverride**
> SuccessResponse UpdateOrganizationFeatureFlagOverride (string orgCode, string featureFlagKey, string value)

Update Organization Feature Flag Override

Update organization feature flag override.  <div>   <code>update:organization_feature_flags</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class UpdateOrganizationFeatureFlagOverrideExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The identifier for the organization
            var featureFlagKey = "featureFlagKey_example";  // string | The identifier for the feature flag
            var value = "value_example";  // string | Override value

            try
            {
                // Update Organization Feature Flag Override
                SuccessResponse result = apiInstance.UpdateOrganizationFeatureFlagOverride(orgCode, featureFlagKey, value);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationFeatureFlagOverride: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateOrganizationFeatureFlagOverrideWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Organization Feature Flag Override
    ApiResponse<SuccessResponse> response = apiInstance.UpdateOrganizationFeatureFlagOverrideWithHttpInfo(orgCode, featureFlagKey, value);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationFeatureFlagOverrideWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization |  |
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

<a id="updateorganizationpasskey"></a>
# **UpdateOrganizationPasskey**
> UpdateOrganizationPasskey200Response UpdateOrganizationPasskey (string orgCode, UpdateOrganizationPasskeyRequest updateOrganizationPasskeyRequest)

Update organization passkey settings

Update passkey settings for an organization. Set `is_override_environment_passkey_settings` to `false` to revert to the environment default without providing a policy.  <div>   <code>update:organization_passkey</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class UpdateOrganizationPasskeyExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = org_1ccfb819462;  // string | The organization's code.
            var updateOrganizationPasskeyRequest = new UpdateOrganizationPasskeyRequest(); // UpdateOrganizationPasskeyRequest | Organization passkey settings.

            try
            {
                // Update organization passkey settings
                UpdateOrganizationPasskey200Response result = apiInstance.UpdateOrganizationPasskey(orgCode, updateOrganizationPasskeyRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationPasskey: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateOrganizationPasskeyWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update organization passkey settings
    ApiResponse<UpdateOrganizationPasskey200Response> response = apiInstance.UpdateOrganizationPasskeyWithHttpInfo(orgCode, updateOrganizationPasskeyRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationPasskeyWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **updateOrganizationPasskeyRequest** | [**UpdateOrganizationPasskeyRequest**](UpdateOrganizationPasskeyRequest.md) | Organization passkey settings. |  |

### Return type

[**UpdateOrganizationPasskey200Response**](UpdateOrganizationPasskey200Response.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Organization passkey settings successfully updated. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateorganizationpermission"></a>
# **UpdateOrganizationPermission**
> SuccessResponse UpdateOrganizationPermission (string orgCode, string permissionId, UpdateOrganizationPermissionRequest? updateOrganizationPermissionRequest = null)

Update organization-managed permission

Update a permission the organization owns. Fields you leave out are not changed. The key cannot be changed after creation.  <div>   <code>update:organization_roles</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class UpdateOrganizationPermissionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var permissionId = "permissionId_example";  // string | The permission's ID.
            var updateOrganizationPermissionRequest = new UpdateOrganizationPermissionRequest?(); // UpdateOrganizationPermissionRequest? | Permission details. (optional) 

            try
            {
                // Update organization-managed permission
                SuccessResponse result = apiInstance.UpdateOrganizationPermission(orgCode, permissionId, updateOrganizationPermissionRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationPermission: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateOrganizationPermissionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update organization-managed permission
    ApiResponse<SuccessResponse> response = apiInstance.UpdateOrganizationPermissionWithHttpInfo(orgCode, permissionId, updateOrganizationPermissionRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationPermissionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **permissionId** | **string** | The permission&#39;s ID. |  |
| **updateOrganizationPermissionRequest** | [**UpdateOrganizationPermissionRequest?**](UpdateOrganizationPermissionRequest?.md) | Permission details. | [optional]  |

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
| **200** | Permission successfully updated. |  -  |
| **400** | Bad request. |  -  |
| **403** | Invalid credentials, organization-managed permissions are not available on the business&#39;s plan (&#x60;PLAN_LIMIT_REACHED&#x60;), or not available on this business at all (&#x60;ORGANIZATION_PERMISSIONS_UNAVAILABLE&#x60;). |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateorganizationproperties"></a>
# **UpdateOrganizationProperties**
> SuccessResponse UpdateOrganizationProperties (string orgCode, UpdateOrganizationPropertiesRequest updateOrganizationPropertiesRequest)

Update Organization Property values

Update organization property values.  <div>   <code>update:organization_properties</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class UpdateOrganizationPropertiesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The identifier for the organization
            var updateOrganizationPropertiesRequest = new UpdateOrganizationPropertiesRequest(); // UpdateOrganizationPropertiesRequest | Properties to update.

            try
            {
                // Update Organization Property values
                SuccessResponse result = apiInstance.UpdateOrganizationProperties(orgCode, updateOrganizationPropertiesRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationProperties: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateOrganizationPropertiesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Organization Property values
    ApiResponse<SuccessResponse> response = apiInstance.UpdateOrganizationPropertiesWithHttpInfo(orgCode, updateOrganizationPropertiesRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationPropertiesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization |  |
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

<a id="updateorganizationproperty"></a>
# **UpdateOrganizationProperty**
> SuccessResponse UpdateOrganizationProperty (string orgCode, string propertyKey, string value)

Update Organization Property value

Update organization property value.  <div>   <code>update:organization_properties</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class UpdateOrganizationPropertyExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The identifier for the organization
            var propertyKey = "propertyKey_example";  // string | The identifier for the property
            var value = "value_example";  // string | The new property value. Send an empty string to unset the property, so it reads back as not set.

            try
            {
                // Update Organization Property value
                SuccessResponse result = apiInstance.UpdateOrganizationProperty(orgCode, propertyKey, value);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationProperty: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateOrganizationPropertyWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Organization Property value
    ApiResponse<SuccessResponse> response = apiInstance.UpdateOrganizationPropertyWithHttpInfo(orgCode, propertyKey, value);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationPropertyWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The identifier for the organization |  |
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

<a id="updateorganizationrole"></a>
# **UpdateOrganizationRole**
> SuccessResponse UpdateOrganizationRole (string orgCode, string roleId, UpdateOrganizationRoleRequest? updateOrganizationRoleRequest = null)

Update organization-managed role

Update a role the organization owns. Fields you leave out are not changed. The key cannot be changed after creation.  <div>   <code>update:organization_roles</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class UpdateOrganizationRoleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var roleId = "roleId_example";  // string | The role's ID.
            var updateOrganizationRoleRequest = new UpdateOrganizationRoleRequest?(); // UpdateOrganizationRoleRequest? | Role details. (optional) 

            try
            {
                // Update organization-managed role
                SuccessResponse result = apiInstance.UpdateOrganizationRole(orgCode, roleId, updateOrganizationRoleRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationRole: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateOrganizationRoleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update organization-managed role
    ApiResponse<SuccessResponse> response = apiInstance.UpdateOrganizationRoleWithHttpInfo(orgCode, roleId, updateOrganizationRoleRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationRoleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **roleId** | **string** | The role&#39;s ID. |  |
| **updateOrganizationRoleRequest** | [**UpdateOrganizationRoleRequest?**](UpdateOrganizationRoleRequest?.md) | Role details. | [optional]  |

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
| **200** | Role successfully updated. |  -  |
| **400** | Bad request. |  -  |
| **403** | Invalid credentials, organization-managed roles are not available on the business&#39;s plan (&#x60;PLAN_LIMIT_REACHED&#x60;), or not on this business (&#x60;ORGANIZATION_ROLES_UNAVAILABLE&#x60;). |  -  |
| **429** | Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateorganizationsessions"></a>
# **UpdateOrganizationSessions**
> SuccessResponse UpdateOrganizationSessions (string orgCode, UpdateOrganizationSessionsRequest updateOrganizationSessionsRequest)

Update organization session configuration

Update the organization's session configuration.  <div>   <code>update:organizations</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class UpdateOrganizationSessionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = org_1ccfb819462;  // string | The organization's code.
            var updateOrganizationSessionsRequest = new UpdateOrganizationSessionsRequest(); // UpdateOrganizationSessionsRequest | Organization session configuration.

            try
            {
                // Update organization session configuration
                SuccessResponse result = apiInstance.UpdateOrganizationSessions(orgCode, updateOrganizationSessionsRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationSessions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateOrganizationSessionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update organization session configuration
    ApiResponse<SuccessResponse> response = apiInstance.UpdateOrganizationSessionsWithHttpInfo(orgCode, updateOrganizationSessionsRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationSessionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **updateOrganizationSessionsRequest** | [**UpdateOrganizationSessionsRequest**](UpdateOrganizationSessionsRequest.md) | Organization session configuration. |  |

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
| **200** | Organization sessions successfully updated |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateorganizationusers"></a>
# **UpdateOrganizationUsers**
> UpdateOrganizationUsersResponse UpdateOrganizationUsers (string orgCode, UpdateOrganizationUsersRequest? updateOrganizationUsersRequest = null)

Update Organization Users

Update users that belong to an organization.  `roles` replaces the user's roles. Every key in it must be a business-wide role key, or the key of one of this organization's own roles while organization-managed roles are available on the business's plan. Any other key fails the whole request with `INVALID_ROLES` instead of being skipped. Below that plan the user's organization-managed role assignments are kept but not issued in tokens; they cannot be named in `roles` until the plan is upgraded, and replacing `roles` does not remove them.  Users in organizations managed by directory sync cannot be updated (`ORGANIZATION_MANAGED_BY_DIRECTORY_SYNC`); user and role changes for those organizations must be made in the upstream identity provider.  <div>   <code>update:organization_users</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class UpdateOrganizationUsersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new OrganizationsApi(config);
            var orgCode = "orgCode_example";  // string | The organization's code.
            var updateOrganizationUsersRequest = new UpdateOrganizationUsersRequest?(); // UpdateOrganizationUsersRequest? |  (optional) 

            try
            {
                // Update Organization Users
                UpdateOrganizationUsersResponse result = apiInstance.UpdateOrganizationUsers(orgCode, updateOrganizationUsersRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationUsers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateOrganizationUsersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Organization Users
    ApiResponse<UpdateOrganizationUsersResponse> response = apiInstance.UpdateOrganizationUsersWithHttpInfo(orgCode, updateOrganizationUsersRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OrganizationsApi.UpdateOrganizationUsersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orgCode** | **string** | The organization&#39;s code. |  |
| **updateOrganizationUsersRequest** | [**UpdateOrganizationUsersRequest?**](UpdateOrganizationUsersRequest?.md) |  | [optional]  |

### Return type

[**UpdateOrganizationUsersResponse**](UpdateOrganizationUsersResponse.md)

### Authorization

[kindeBearerAuth](../README.md#kindeBearerAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Users successfully removed. |  -  |
| **400** | Invalid request. |  -  |
| **403** | Unauthorized - invalid credentials. |  -  |
| **429** | Too many requests. Request was throttled. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

