#nullable enable

using System.Net;
using System.Text;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Kiota.Api;

namespace KiotaTests;

public sealed class MockHttpMessageHandler : HttpMessageHandler
{
    private readonly HttpStatusCode _statusCode;
    private readonly string _responseBody;
    public HttpRequestMessage? LastRequest { get; private set; }

    public MockHttpMessageHandler(HttpStatusCode statusCode, string responseBody)
    {
        _statusCode = statusCode;
        _responseBody = responseBody;
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        LastRequest = request;
        return Task.FromResult(new HttpResponseMessage(_statusCode)
        {
            Content = new StringContent(_responseBody, Encoding.UTF8, "application/json")
        });
    }
}

public static class ApiClientFactory
{
    public static (ApiClient client, MockHttpMessageHandler handler) Create(
        HttpStatusCode statusCode, string responseBody)
    {
        var handler    = new MockHttpMessageHandler(statusCode, responseBody);
        var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://test.kinde.com") };
        var adapter    = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider(), httpClient: httpClient);
        adapter.BaseUrl = "https://test.kinde.com";
        return (new ApiClient(adapter), handler);
    }
}

public static class MockData
{
    public const string SuccessResponse = "{\"code\":\"OK\",\"message\":\"Success\"}";
    public const string Error400        = "{\"errors\":[{\"code\":\"BAD_REQUEST\",\"message\":\"Invalid request\"}]}";
    public const string Error403        = "{\"errors\":[{\"code\":\"UNAUTHORIZED\",\"message\":\"Unauthorized\"}]}";
    public const string Error429        = "{\"errors\":[{\"code\":\"TOO_MANY_REQUESTS\",\"message\":\"Throttled\"}]}";

    public const string GetApisResponse =
        "{\"code\":\"OK\",\"apis\":[{\"id\":\"7ccd126599aa422a771abcb341596881\",\"name\":\"My Test API\"," +
        "\"audience\":\"https://api.example.com\",\"is_management_api\":false}],\"next_token\":null}";

    public const string GetApiResponse =
        "{\"code\":\"OK\",\"api\":{\"id\":\"7ccd126599aa422a771abcb341596881\",\"name\":\"My Test API\"," +
        "\"audience\":\"https://api.example.com\",\"is_management_api\":false}}";

    public const string CreateApiResponse =
        "{\"code\":\"OK\",\"api\":{\"id\":\"new_api_id_001\"}}";

    public const string DeleteApiResponse =
        "{\"code\":\"OK\",\"message\":\"API deleted successfully\"}";

    public const string GetApiScopesResponse =
      "{\"code\":\"OK\",\"scopes\":[{\"id\":\"scope_001\",\"key\":\"read:data\",\"description\":\"Read access\"}]}";

    public const string GetApplicationsResponse =
        "{\"code\":\"OK\",\"applications\":[{\"id\":\"3b0b5c6c8fcc464fab397f4969b5f482\"," +
        "\"name\":\"My React app\",\"type\":\"spa\",\"client_id\":\"3b0b5c6c8fcc464fab397f4969b5f482\"}],\"next_token\":null}";

    public const string GetApplicationResponse =
        "{\"code\":\"OK\",\"application\":{\"id\":\"3b0b5c6c8fcc464fab397f4969b5f482\",\"name\":\"My React app\"," +
        "\"type\":\"spa\",\"client_id\":\"3b0b5c6c8fcc464fab397f4969b5f482\"," +
        "\"homepage_uri\":\"https://yourapp.com\",\"login_uri\":\"https://yourapp.com/login\"}}";

    public const string CreateApplicationResponse =
        "{\"code\":\"OK\",\"application\":{\"id\":\"3b0b5c6c8fcc464fab397f4969b5f482\"," +
        "\"client_id\":\"3b0b5c6c8fcc464fab397f4969b5f482\"," +
        "\"client_secret\":\"sUJSHI3ZQEVTJkx6hOxdOSHaLsZkCBRFLzTNOI791rX8mDjgt7LC\"}}";

    public const string GetRedirectUrlsResponse =
        "{\"redirect_urls\":[{\"id\":\"url_001\",\"url\":\"https://myapp.com/callback\"}]}";

    public const string GetConnectionsResponse =
    "{\"code\":\"OK\",\"connections\":[{\"code\":\"OK\"," +
    "\"connection\":{\"id\":\"conn_001\",\"name\":\"Google\"," +
    "\"display_name\":\"Sign in with Google\",\"strategy\":\"oauth2:google\"}}]}";

    public const string GetBillingEntitlementsResponse =
        "{\"entitlements\":[{\"id\":\"entitlement_001\",\"feature_code\":\"CcdkvEXpbg6UY\"," +
        "\"feature_name\":\"Pro Gym\",\"fixed_charge\":35}]," +
        "\"plans\":[{\"code\":\"pro_plan\",\"subscribed_on\":\"2024-11-18T13:32:03+11:00\"}]}";

    public const string GetBillingAgreementsResponse =
        "{\"agreements\":[{\"id\":\"agreement_001\",\"plan_code\":\"pro_plan\"," +
        "\"billing_group_id\":\"sbg_001\",\"expires_on\":\"2024-11-18T13:32:03+11:00\",\"entitlements\":[]}]}";

    public const string GetBusinessResponse =
        "{\"code\":\"OK\",\"business\":{\"code\":\"biz_001\",\"name\":\"Acme Corp\"," +
        "\"email\":\"admin@acme.com\",\"industry_key\":\"technology\",\"timezone_id\":\"Australia/Sydney\"}}";

    public const string GetConnectedAppAuthUrlResponse =
        "{\"url\":\"https://auth.provider.com/oauth?state=xyz\",\"session_id\":\"sess_abc\"}";

    public const string GetConnectedAppTokenResponse =
        "{\"access_token\":\"eyJhbGci...\",\"access_token_expiry\":\"2024-12-01T00:00:00Z\"}";

    public const string GetEnvironmentResponse =
        "{\"code\":\"OK\",\"environment\":{\"code\":\"env_default\",\"name\":\"Default\",\"handle\":\"default\"}}";

    public const string GetEnvironmentVariablesResponse =
        "{\"code\":\"OK\",\"environment_variables\":[{\"id\":\"var_001\",\"key\":\"APP_URL\"," +
        "\"value\":\"https://myapp.com\",\"is_secret\":false}]}";

    public const string GetFeatureFlagsResponse =
        "{\"code\":\"OK\",\"feature_flags\":[{\"id\":\"flag_001\",\"key\":\"new_dashboard\"," +
        "\"name\":\"New Dashboard\",\"type\":\"bool\",\"default_value\":\"false\"}]}";

    public const string GetEnvLogosResponse =
        "{\"logos\":[{\"type\":\"light\",\"file_name\":\"kinde_light.jpeg\"}]}";

    public const string GetEventResponse =
    "{\"code\":\"OK\",\"event\":{\"event_id\":\"evt_001\",\"type\":\"user.created\",\"source\":\"kinde\",\"data\":{}}}";

    public const string GetEventTypesResponse =
        "{\"code\":\"OK\",\"event_types\":[{\"id\":\"et_001\",\"code\":\"user.created\",\"name\":\"User Created\"}]}";

    public const string GetIndustriesResponse =
        "{\"industries\":[{\"id\":\"ind_001\",\"name\":\"Technology\",\"key\":\"technology\"}]}";

    public const string GetOrganizationResponse =
    "{\"code\":\"org_1767f11ce62\",\"name\":\"Acme Org\",\"handle\":\"acme\",\"is_default\":false}";

    public const string GetOrganizationsResponse =
        "{\"code\":\"OK\",\"organizations\":[{\"code\":\"org_1767f11ce62\",\"name\":\"Acme Org\",\"handle\":\"acme\"}]," +
        "\"next_token\":null}";

    public const string GetOrgUsersResponse =
        "{\"code\":\"OK\",\"organization_users\":[{\"id\":\"kp_user_001\",\"email\":\"alice@example.com\"," +
        "\"full_name\":\"Alice Smith\",\"roles\":[]}],\"next_token\":null}";

    public const string GetOrgLogosResponse =
        "{\"logos\":[{\"type\":\"light\",\"file_name\":\"logo.png\",\"path\":\"/logo?p_org_code=org_001\"}]}";

    public const string GetPermissionsResponse =
        "{\"code\":\"OK\",\"permissions\":[{\"id\":\"perm_001\",\"key\":\"read:reports\"," +
        "\"name\":\"Read Reports\",\"description\":\"Can read reports\"}],\"next_token\":null}";

    public const string GetPropertiesResponse =
        "{\"code\":\"OK\",\"properties\":[{\"id\":\"prop_001\",\"key\":\"favorite_color\"," +
        "\"name\":\"Favorite Color\",\"type\":\"string\",\"context\":\"usr\"}],\"next_token\":null}";

    public const string GetPropertyCategoriesResponse =
        "{\"code\":\"OK\",\"categories\":[{\"id\":\"cat_001\",\"name\":\"Profile\",\"context\":\"usr\"}]}";

    public const string GetRolesResponse =
        "{\"code\":\"OK\",\"roles\":[{\"id\":\"role_001\",\"key\":\"admin\",\"name\":\"Admin\"," +
        "\"description\":\"Full admin access\",\"is_default_role\":false}],\"next_token\":null}";

    public const string GetRoleResponse =
        "{\"code\":\"OK\",\"role\":{\"id\":\"role_001\",\"key\":\"admin\",\"name\":\"Admin\"}}";

    public const string GetRolePermissionsResponse =
        "{\"code\":\"OK\",\"permissions\":[{\"id\":\"perm_001\",\"key\":\"read:reports\",\"name\":\"Read Reports\"}]}";

    public const string GetRoleScopesResponse =
    "{\"code\":\"OK\",\"scopes\":[{\"id\":\"scope_001\",\"key\":\"read:data\"}]}";

    public const string GetSubscribersResponse =
        "{\"code\":\"OK\",\"subscribers\":[{\"id\":\"sub_001\",\"email\":\"subscriber@example.com\"," +
        "\"first_name\":\"Sub\",\"last_name\":\"Scriber\"}],\"next_token\":null}";

    public const string GetSubscriberResponse =
    "{\"code\":\"OK\",\"subscribers\":[{\"id\":\"sub_001\",\"preferred_email\":\"subscriber@example.com\",\"first_name\":\"Sub\",\"last_name\":\"Scriber\"}]}";

    public const string GetTimezonesResponse =
        "{\"timezones\":[{\"id\":\"tz_001\",\"key\":\"Australia/Sydney\",\"display_name\":\"Australia/Sydney\"}]}";

    public const string GetUsersResponse =
        "{\"code\":\"OK\",\"users\":[{\"id\":\"kp_user_001\",\"email\":\"alice@example.com\"," +
        "\"first_name\":\"Alice\",\"last_name\":\"Smith\",\"is_suspended\":false}],\"next_token\":null}";

    public const string GetUserResponse =
    "{\"id\":\"kp_user_001\",\"preferred_email\":\"alice@example.com\",\"first_name\":\"Alice\",\"last_name\":\"Smith\",\"is_suspended\":false}";

    public const string CreateUserResponse =
    "{\"id\":\"kp_user_002\",\"preferred_email\":\"bob@example.com\",\"first_name\":\"Bob\",\"last_name\":\"Jones\",\"is_suspended\":false}";

    public const string GetUserMfaResponse =
        "{\"code\":\"OK\",\"mfa_factors\":[{\"id\":\"factor_001\",\"type\":\"totp\"}]}";

    public const string GetUserSessionsResponse =
    "{\"sessions\":[{\"session_id\":\"sess_001\",\"started_on\":\"2024-01-01T00:00:00Z\"}]}";

    public const string GetIdentitiesResponse =
     "{\"identities\":[{\"id\":\"ident_001\",\"type\":\"email\",\"email\":\"alice@example.com\"}]}";

    public const string GetWebhooksResponse =
        "{\"code\":\"OK\",\"webhooks\":[{\"id\":\"wh_001\",\"endpoint\":\"https://myapp.com/webhook\"," +
        "\"event_types\":[\"user.created\"]}]}";

    public const string CreateWebhookResponse =
        "{\"code\":\"OK\",\"webhook\":{\"id\":\"wh_new\",\"endpoint\":\"https://myapp.com/hook\"}}";

    public const string SearchUsersResponse =
        "{\"results\":[{\"id\":\"kp_user_001\",\"email\":\"alice@example.com\",\"name\":\"Alice Smith\"}]}";
}