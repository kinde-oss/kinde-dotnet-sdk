using Microsoft.Kiota.Abstractions;
using Moq;

namespace KiotaTests.Helpers;

public static class KindeApiTestHelpers
{
    public static IRequestAdapter CreateRequestAdapter()
    {
        var mock = new Mock<IRequestAdapter>();
        mock.SetupProperty(adapter => adapter.BaseUrl, "https://api.example.test");
        return mock.Object;
    }

    public static Dictionary<string, object> CreatePathParameters()
    {
        return new Dictionary<string, object>
        {
            { "api_id", "api_id" },
            { "app_id", "app_id" },
            { "application_id", "application_id" },
            { "category_id", "category_id" },
            { "connection_id", "connection_id" },
            { "directory_id", "directory_id" },
            { "event_id", "event_id" },
            { "factor_id", "factor_id" },
            { "feature_flag_key", "feature_flag_key" },
            { "identity_id", "identity_id" },
            { "invite_code", "invite_code" },
            { "key_id", "key_id" },
            { "org_code", "org_code" },
            { "organization_code", "organization_code" },
            { "permission_id", "permission_id" },
            { "property_id", "property_id" },
            { "property_key", "property_key" },
            { "role_id", "role_id" },
            { "scope_id", "scope_id" },
            { "subscriber_id", "subscriber_id" },
            { "type", "type" },
            { "user_id", "user_id" },
            { "variable_id", "variable_id" },
            { "webhook_id", "webhook_id" },
            { "organization_id", "organization_id" },
            { "invite_id", "invite_id" },
            { "session_id", "session_id" },
            { "event_type", "event_type" },
        };
    }
}
