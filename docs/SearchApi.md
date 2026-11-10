# Kinde.Api.Api.SearchApi

All URIs are relative to *https://your_kinde_subdomain.kinde.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**SearchUsers**](SearchApi.md#searchusers) | **GET** /api/v1/search/users | Search users |

<a id="searchusers"></a>
# **SearchUsers**
> SearchUsersResponse SearchUsers (int? pageSize = null, string? query = null, string? apiScopes = null, Dictionary<string, List<string>>? properties = null, string? startingAfter = null, string? endingBefore = null, string? expand = null)

Search users

Search for users based on the provided query string. Set query to '*' to filter by other parameters only. The number of records to return at a time can be controlled using the `page_size` query string parameter.  <div>   <code>read:users</code> </div> 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Model;

namespace Example
{
    public class SearchUsersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://your_kinde_subdomain.kinde.com";
            // Configure Bearer token for authorization: kindeBearerAuth
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new SearchApi(config);
            var pageSize = 56;  // int? | Number of results per page. Defaults to 10 if parameter not sent. (optional) 
            var query = "query_example";  // string? | Search the users by email or name. Use '*' to search all. (optional) 
            var apiScopes = "apiScopes_example";  // string? | Search the users by api scopes. (optional) 
            var properties = new Dictionary<string, List<string>>?(); // Dictionary<string, List<string>>? |  (optional) 
            var startingAfter = "startingAfter_example";  // string? | The ID of the user to start after. (optional) 
            var endingBefore = "endingBefore_example";  // string? | The ID of the user to end before. (optional) 
            var expand = "expand_example";  // string? | Additional data to include in the response. One or more of (comma-separated): \"organizations\", \"identities\", \"properties\". (optional) 

            try
            {
                // Search users
                SearchUsersResponse result = apiInstance.SearchUsers(pageSize, query, properties, startingAfter, endingBefore, expand, apiScopes);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SearchApi.SearchUsers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the SearchUsersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Search users
    ApiResponse<SearchUsersResponse> response = apiInstance.SearchUsersWithHttpInfo(pageSize, query, properties, startingAfter, endingBefore, expand, apiScopes);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SearchApi.SearchUsersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **pageSize** | **int?** | Number of results per page. Defaults to 10 if parameter not sent. | [optional]  |
| **query** | **string?** | Search the users by email or name. Use &#39;*&#39; to search all. | [optional]  |
| **properties** | [**Dictionary&lt;string, List&lt;string&gt;&gt;?**](List&lt;string&gt;.md) |  | [optional]  |
| **startingAfter** | **string?** | The ID of the user to start after. | [optional]  |
| **endingBefore** | **string?** | The ID of the user to end before. | [optional]  |
| **expand** | **string?** | Additional data to include in the response. One or more of (comma-separated): \&quot;organizations\&quot;, \&quot;identities\&quot;, \&quot;properties\&quot;. | [optional]  |
| **apiScopes** | **string?** | Search the users by api scopes. | [optional]  |

### Return type

[**SearchUsersResponse**](SearchUsersResponse.md)

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

