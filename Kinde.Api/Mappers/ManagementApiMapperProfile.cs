using AutoMapper;
using Kinde.Api.Model;
using KiotaModels = Kinde.Api.Kiota.Management.Models;

namespace Kinde.Api.Mappers
{
    /// <summary>
    /// AutoMapper profile for mapping between OpenAPI Management API models and Kiota Management API models.
    /// This profile handles bidirectional mapping to support both request and response translation.
    /// </summary>
    public class ManagementApiMapperProfile : Profile
    {
        public ManagementApiMapperProfile()
        {
            // Configure default mapping behavior
            AllowNullCollections = true;
            AllowNullDestinationValues = true;

            // ===== Response Models =====
            
            // Add Organization Users
            CreateMap<KiotaModels.Add_organization_users_response, AddOrganizationUsersResponse>().ReverseMap();
            
            // Add Role Scope
            CreateMap<KiotaModels.Add_role_scope_response, AddRoleScopeResponse>().ReverseMap();
            
            // Applications
            CreateMap<KiotaModels.Applications, Applications>().ReverseMap();
            
            // Authorize App API
            CreateMap<KiotaModels.Authorize_app_api_response, AuthorizeAppApiResponse>().ReverseMap();
            
            // Category
            CreateMap<KiotaModels.Category, Category>().ReverseMap();
            
            // Connected Apps
            CreateMap<KiotaModels.Connected_apps_access_token, ConnectedAppsAccessToken>().ReverseMap();
            CreateMap<KiotaModels.Connected_apps_auth_url, ConnectedAppsAuthUrl>().ReverseMap();
            
            // Connection
            CreateMap<KiotaModels.Connection, Connection>().ReverseMap();
            CreateMap<KiotaModels.Connection_connection, ConnectionConnection>().ReverseMap();
            
            // API Key
            CreateMap<KiotaModels.Create_api_key_response, CreateApiKeyResponse>().ReverseMap();
            CreateMap<KiotaModels.Create_api_key_response_api_key, CreateApiKeyResponseApiKey>().ReverseMap();
            
            // API Scopes
            CreateMap<KiotaModels.Create_api_scopes_response, CreateApiScopesResponse>().ReverseMap();
            CreateMap<KiotaModels.Create_api_scopes_response_scope, CreateApiScopesResponseScope>().ReverseMap();
            
            // APIs
            CreateMap<KiotaModels.Create_apis_response, CreateApisResponse>().ReverseMap();
            CreateMap<KiotaModels.Create_apis_response_api, CreateApisResponseApi>().ReverseMap();
            
            // Application
            CreateMap<KiotaModels.Create_application_response, CreateApplicationResponse>().ReverseMap();
            CreateMap<KiotaModels.Create_application_response_application, CreateApplicationResponseApplication>().ReverseMap();
            
            // Category
            CreateMap<KiotaModels.Create_category_response, CreateCategoryResponse>().ReverseMap();
            CreateMap<KiotaModels.Create_category_response_category, CreateCategoryResponseCategory>().ReverseMap();
            CreateMap<CreateCategoryRequest, Kiota.Management.Api.V1.Property_categories.Property_categoriesPostRequestBody>()
                .ForMember(dest => dest.Context, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<UpdateCategoryRequest, Kiota.Management.Api.V1.Property_categories.Item.WithCategory_PutRequestBody>().ReverseMap();
            
            // Connection
            CreateMap<KiotaModels.Create_connection_response, CreateConnectionResponse>().ReverseMap();
            CreateMap<KiotaModels.Create_connection_response_connection, CreateConnectionResponseConnection>().ReverseMap();
            CreateMap<CreateConnectionRequest, Kiota.Management.Api.V1.Connections.ConnectionsPostRequestBody>().ReverseMap();
            CreateMap<ReplaceConnectionRequest, Kiota.Management.Api.V1.Connections.Item.WithConnection_PutRequestBody>().ReverseMap();
            CreateMap<UpdateConnectionRequest, Kiota.Management.Api.V1.Connections.Item.WithConnection_PatchRequestBody>().ReverseMap();
            
            // Environment Variable
            CreateMap<KiotaModels.Create_environment_variable_response, CreateEnvironmentVariableResponse>().ReverseMap();
            CreateMap<KiotaModels.Create_environment_variable_response_environment_variable, CreateEnvironmentVariableResponseEnvironmentVariable>().ReverseMap();
            CreateMap<CreateEnvironmentVariableRequest, Kiota.Management.Api.V1.Environment_variables.Environment_variablesPostRequestBody>().ReverseMap();
            CreateMap<UpdateEnvironmentVariableRequest, Kiota.Management.Api.V1.Environment_variables.Item.WithVariable_PatchRequestBody>().ReverseMap();
            
            // Identity
            CreateMap<KiotaModels.Create_identity_response, CreateIdentityResponse>().ReverseMap();
            CreateMap<KiotaModels.Create_identity_response_identity, CreateIdentityResponseIdentity>().ReverseMap();
            
            // Meter Usage
            CreateMap<KiotaModels.Create_meter_usage_record_response, CreateMeterUsageRecordResponse>().ReverseMap();
            
            // Organization
            CreateMap<KiotaModels.Create_organization_response, CreateOrganizationResponse>().ReverseMap();
            CreateMap<KiotaModels.Create_organization_response_organization, CreateOrganizationResponseOrganization>().ReverseMap();
            
            // Property
            CreateMap<KiotaModels.Create_property_response, CreatePropertyResponse>().ReverseMap();
            CreateMap<KiotaModels.Create_property_response_property, CreatePropertyResponseProperty>().ReverseMap();
            CreateMap<CreatePropertyRequest, Kiota.Management.Api.V1.Properties.PropertiesPostRequestBody>()
                .ForMember(dest => dest.Context, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<UpdatePropertyRequest, Kiota.Management.Api.V1.Properties.Item.WithProperty_PutRequestBody>().ReverseMap();
            
            // Roles
            CreateMap<KiotaModels.Create_roles_response, CreateRolesResponse>().ReverseMap();
            CreateMap<KiotaModels.Create_roles_response_role, CreateRolesResponseRole>().ReverseMap();
            
            // Subscriber
            CreateMap<KiotaModels.Create_subscriber_success_response, CreateSubscriberSuccessResponse>().ReverseMap();
            CreateMap<KiotaModels.Create_subscriber_success_response_subscriber, CreateSubscriberSuccessResponseSubscriber>().ReverseMap();
            
            // User
            CreateMap<KiotaModels.Create_user_response, CreateUserResponse>().ReverseMap();
            
            // Webhook
            CreateMap<KiotaModels.Create_webhook_response, CreateWebhookResponse>().ReverseMap();
            CreateMap<KiotaModels.Create_webhook_response_webhook, CreateWebhookResponseWebhook>().ReverseMap();
            CreateMap<CreateWebHookRequest, Kiota.Management.Api.V1.Webhooks.WebhooksPostRequestBody>().ReverseMap();
            CreateMap<UpdateWebHookRequest, Kiota.Management.Api.V1.Webhooks.Item.WithWebhook_PatchRequestBody>().ReverseMap();
            
            // Delete responses
            CreateMap<KiotaModels.Delete_api_response, DeleteApiResponse>().ReverseMap();
            CreateMap<KiotaModels.Delete_environment_variable_response, DeleteEnvironmentVariableResponse>().ReverseMap();
            CreateMap<KiotaModels.Delete_role_scope_response, DeleteRoleScopeResponse>().ReverseMap();
            CreateMap<KiotaModels.Delete_webhook_response, DeleteWebhookResponse>().ReverseMap();
            
            // Environment Variable
            CreateMap<KiotaModels.Environment_variable, EnvironmentVariable>().ReverseMap();
            
            // Error
            CreateMap<KiotaModels.Error, Error>().ReverseMap();
            CreateMap<KiotaModels.Error_response, ErrorResponse>().ReverseMap();
            
            // Event Type
            CreateMap<KiotaModels.Event_type, EventType>().ReverseMap();
            
            // Get API Key
            CreateMap<KiotaModels.Get_api_key_response, GetApiKeyResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_api_key_response_api_key, GetApiKeyResponseApiKey>().ReverseMap();
            CreateMap<KiotaModels.Get_api_keys_response, GetApiKeysResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_api_keys_response_api_keys, GetApiKeysResponseApiKeysInner>().ReverseMap();
            
            // Get API
            CreateMap<KiotaModels.Get_api_response, GetApiResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_api_response_api, GetApiResponseApi>().ReverseMap();
            CreateMap<KiotaModels.Get_api_response_api_applications, GetApiResponseApiApplicationsInner>().ReverseMap();
            CreateMap<KiotaModels.Get_api_response_api_scopes, GetApiResponseApiScopesInner>().ReverseMap();
            
            // Get API Scope
            CreateMap<KiotaModels.Get_api_scope_response, GetApiScopeResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_api_scopes_response, GetApiScopesResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_api_scopes_response_scopes, GetApiScopesResponseScopesInner>().ReverseMap();
            
            // Get APIs
            CreateMap<KiotaModels.Get_apis_response, GetApisResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_apis_response_apis, GetApisResponseApisInner>().ReverseMap();
            CreateMap<KiotaModels.Get_apis_response_apis_scopes, GetApisResponseApisInnerScopesInner>().ReverseMap();
            
            // Get Application
            CreateMap<KiotaModels.Get_application_response, GetApplicationResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_application_response_application, GetApplicationResponseApplication>().ReverseMap();
            CreateMap<KiotaModels.Get_applications_response, GetApplicationsResponse>().ReverseMap();
            
            // Get Billing
            CreateMap<KiotaModels.Get_billing_agreements_response, GetBillingAgreementsResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_billing_agreements_response_agreements, GetBillingAgreementsResponseAgreementsInner>().ReverseMap();
            CreateMap<KiotaModels.Get_billing_agreements_response_agreements_entitlements, GetBillingAgreementsResponseAgreementsInnerEntitlementsInner>().ReverseMap();
            CreateMap<KiotaModels.Get_billing_entitlements_response, GetBillingEntitlementsResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_billing_entitlements_response_entitlements, GetBillingEntitlementsResponseEntitlementsInner>().ReverseMap();
            CreateMap<KiotaModels.Get_billing_entitlements_response_plans, GetBillingEntitlementsResponsePlansInner>().ReverseMap();
            
            // Get Business
            CreateMap<KiotaModels.Get_business_response, GetBusinessResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_business_response_business, GetBusinessResponseBusiness>().ReverseMap();
            
            // Get Categories
            CreateMap<KiotaModels.Get_categories_response, GetCategoriesResponse>().ReverseMap();
            
            // Get Connections
            CreateMap<KiotaModels.Get_connections_response, GetConnectionsResponse>().ReverseMap();
            
            // Get Environment
            CreateMap<KiotaModels.Get_environment_feature_flags_response, GetEnvironmentFeatureFlagsResponse>().ReverseMap();
            CreateMap<CreateFeatureFlagRequest, Kiota.Management.Api.V1.Feature_flags.Feature_flagsPostRequestBody>()
                .ForMember(dest => dest.Type, opt => opt.Ignore())
                .ForMember(dest => dest.AllowOverrideLevel, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<KiotaModels.Get_environment_response, GetEnvironmentResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_environment_response_environment, GetEnvironmentResponseEnvironment>().ReverseMap();
            CreateMap<KiotaModels.Get_environment_response_environment_background_color, GetEnvironmentResponseEnvironmentBackgroundColor>().ReverseMap();
            CreateMap<KiotaModels.Get_environment_response_environment_link_color, GetEnvironmentResponseEnvironmentLinkColor>().ReverseMap();
            CreateMap<KiotaModels.Get_environment_variable_response, GetEnvironmentVariableResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_environment_variables_response, GetEnvironmentVariablesResponse>().ReverseMap();
            CreateMap<UpdateEnvironementFeatureFlagOverrideRequest, Kiota.Management.Api.V1.EnvironmentNamespace.Feature_flags.Item.WithFeature_flag_keyPatchRequestBody>().ReverseMap();
            
            // Get Event
            CreateMap<KiotaModels.Get_event_response, GetEventResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_event_response_event, GetEventResponseEvent>().ReverseMap();
            CreateMap<KiotaModels.Get_event_types_response, GetEventTypesResponse>().ReverseMap();
            
            // Get Identities
            CreateMap<KiotaModels.Get_identities_response, GetIdentitiesResponse>().ReverseMap();
            
            // Get Industries
            CreateMap<KiotaModels.Get_industries_response, GetIndustriesResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_industries_response_industries, GetIndustriesResponseIndustriesInner>().ReverseMap();
            
            // Get Organization
            CreateMap<KiotaModels.Get_organization_feature_flags_response, GetOrganizationFeatureFlagsResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_organization_feature_flags_response_feature_flags, GetOrganizationFeatureFlagsResponseFeatureFlagsValue>().ReverseMap();
            CreateMap<KiotaModels.Get_organization_response, GetOrganizationResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_organization_response_billing, GetOrganizationResponseBilling>().ReverseMap();
            CreateMap<KiotaModels.Get_organization_response_billing_agreements, GetOrganizationResponseBillingAgreementsInner>().ReverseMap();
            CreateMap<KiotaModels.Get_organization_users_response, GetOrganizationUsersResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_organizations_response, GetOrganizationsResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_organizations_user_permissions_response, GetOrganizationsUserPermissionsResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_organizations_user_roles_response, GetOrganizationsUserRolesResponse>().ReverseMap();
            
            // Get Permissions
            CreateMap<KiotaModels.Get_permissions_response, GetPermissionsResponse>().ReverseMap();
            CreateMap<CreatePermissionRequest, Kiota.Management.Api.V1.Permissions.PermissionsPostRequestBody>().ReverseMap();
            CreateMap<CreatePermissionRequest, Kiota.Management.Api.V1.Permissions.Item.WithPermission_PatchRequestBody>().ReverseMap();
            
            // Get Properties
            CreateMap<KiotaModels.Get_properties_response, GetPropertiesResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_property_values_response, GetPropertyValuesResponse>().ReverseMap();
            
            // Get Role
            CreateMap<KiotaModels.Get_role_response, GetRoleResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_role_response_role, GetRoleResponseRole>().ReverseMap();
            CreateMap<KiotaModels.Get_roles_response, GetRolesResponse>().ReverseMap();
            
            // Get Subscriber
            CreateMap<KiotaModels.Get_subscriber_response, GetSubscriberResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_subscribers_response, GetSubscribersResponse>().ReverseMap();
            
            // Get Timezones
            CreateMap<KiotaModels.Get_timezones_response, GetTimezonesResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_timezones_response_timezones, GetTimezonesResponseTimezonesInner>().ReverseMap();
            
            // Get User MFA
            CreateMap<KiotaModels.Get_user_mfa_response, GetUserMfaResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_user_mfa_response_mfa, GetUserMfaResponseMfa>().ReverseMap();
            
            // Get User Sessions
            CreateMap<KiotaModels.Get_user_sessions_response, GetUserSessionsResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_user_sessions_response_sessions, GetUserSessionsResponseSessionsInner>().ReverseMap();
            
            // Get Webhooks
            CreateMap<KiotaModels.Get_webhooks_response, GetWebhooksResponse>().ReverseMap();
            
            // Identity
            CreateMap<KiotaModels.Identity, Identity>().ReverseMap();
            CreateMap<UpdateIdentityRequest, Kiota.Management.Api.V1.Identities.Item.WithIdentity_PatchRequestBody>().ReverseMap();
            
            // Logout Redirect URLs
            CreateMap<KiotaModels.Logout_redirect_urls, LogoutRedirectUrls>().ReverseMap();
            CreateMap<ReplaceLogoutRedirectURLsRequest, Kiota.Management.Api.V1.Applications.Item.Auth_logout_urls.Auth_logout_urlsPostRequestBody>().ReverseMap();
            CreateMap<ReplaceLogoutRedirectURLsRequest, Kiota.Management.Api.V1.Applications.Item.Auth_logout_urls.Auth_logout_urlsPutRequestBody>().ReverseMap();
            
            // Not Found
            CreateMap<KiotaModels.Not_found_response, NotFoundResponse>().ReverseMap();
            CreateMap<KiotaModels.Not_found_response_errors, NotFoundResponseErrors>().ReverseMap();
            
            // Organization
            CreateMap<KiotaModels.Organization_item_schema, OrganizationItemSchema>().ReverseMap();
            CreateMap<KiotaModels.Organization_user, OrganizationUser>().ReverseMap();
            CreateMap<KiotaModels.Organization_user_permission, OrganizationUserPermission>().ReverseMap();
            CreateMap<KiotaModels.Organization_user_permission_roles, OrganizationUserPermissionRolesInner>().ReverseMap();
            CreateMap<KiotaModels.Organization_user_role, OrganizationUserRole>().ReverseMap();
            
            // Permissions
            CreateMap<KiotaModels.Permissions, Permissions>().ReverseMap();
            
            // Property
            CreateMap<KiotaModels.Property, Property>().ReverseMap();
            CreateMap<KiotaModels.Property_value, PropertyValue>().ReverseMap();
            
            // Logo
            CreateMap<KiotaModels.Read_env_logo_response, ReadEnvLogoResponse>().ReverseMap();
            CreateMap<KiotaModels.Read_env_logo_response_logos, ReadEnvLogoResponseLogosInner>().ReverseMap();
            CreateMap<KiotaModels.Read_logo_response, ReadLogoResponse>().ReverseMap();
            CreateMap<KiotaModels.Read_logo_response_logos, ReadLogoResponseLogosInner>().ReverseMap();
            
            // Redirect Callback URLs
            CreateMap<KiotaModels.Redirect_callback_urls, RedirectCallbackUrls>().ReverseMap();
            CreateMap<ReplaceRedirectCallbackURLsRequest, Kiota.Management.Api.V1.Applications.Item.Auth_redirect_urls.Auth_redirect_urlsPostRequestBody>().ReverseMap();
            CreateMap<ReplaceRedirectCallbackURLsRequest, Kiota.Management.Api.V1.Applications.Item.Auth_redirect_urls.Auth_redirect_urlsPutRequestBody>().ReverseMap();
            
            // Role
            CreateMap<KiotaModels.Role_permissions_response, RolePermissionsResponse>().ReverseMap();
            CreateMap<KiotaModels.Role_scopes_response, RoleScopesResponse>().ReverseMap();
            CreateMap<KiotaModels.Roles, Roles>().ReverseMap();
            
            // Rotate API Key
            CreateMap<KiotaModels.Rotate_api_key_response, RotateApiKeyResponse>().ReverseMap();
            CreateMap<KiotaModels.Rotate_api_key_response_api_key, RotateApiKeyResponseApiKey>().ReverseMap();
            
            // Scopes
            CreateMap<KiotaModels.Scopes, Scopes>().ReverseMap();
            
            // Search Users
            CreateMap<KiotaModels.Search_users_response, SearchUsersResponse>().ReverseMap();
            CreateMap<KiotaModels.Search_users_response_results, SearchUsersResponseResultsInner>().ReverseMap();
            CreateMap<KiotaModels.Search_users_response_results_api_scopes, SearchUsersResponseResultsInnerApiScopesInner>().ReverseMap();
            
            // Subscriber
            CreateMap<KiotaModels.Subscriber, Subscriber>().ReverseMap();
            CreateMap<KiotaModels.Subscribers_subscriber, SubscribersSubscriber>().ReverseMap();
            
            // Success
            CreateMap<KiotaModels.Success_response, SuccessResponse>().ReverseMap();
            
            // Update responses
            CreateMap<KiotaModels.Update_environment_variable_response, UpdateEnvironmentVariableResponse>().ReverseMap();
            CreateMap<KiotaModels.Update_organization_users_response, UpdateOrganizationUsersResponse>().ReverseMap();
            CreateMap<KiotaModels.Update_role_permissions_response, UpdateRolePermissionsResponse>().ReverseMap();
            CreateMap<KiotaModels.Update_user_response, UpdateUserResponse>().ReverseMap();
            CreateMap<KiotaModels.Update_webhook_response, UpdateWebhookResponse>().ReverseMap();
            CreateMap<KiotaModels.Update_webhook_response_webhook, UpdateWebhookResponseWebhook>().ReverseMap();
            
            // User
            CreateMap<KiotaModels.User, User>().ReverseMap();
            CreateMap<KiotaModels.User_billing, UserBilling>().ReverseMap();
            CreateMap<KiotaModels.User_identities, UserIdentitiesInner>().ReverseMap();
            CreateMap<KiotaModels.User_identity, UserIdentity>().ReverseMap();
            CreateMap<KiotaModels.User_identity_result, UserIdentityResult>().ReverseMap();
            CreateMap<KiotaModels.Users_response, UsersResponse>().ReverseMap();
            CreateMap<KiotaModels.Users_response_users, UsersResponseUsersInner>().ReverseMap();
            CreateMap<KiotaModels.Users_response_users_billing, UsersResponseUsersInnerBilling>().ReverseMap();
            
            // Verify API Key
            CreateMap<KiotaModels.Verify_api_key_response, VerifyApiKeyResponse>().ReverseMap();
            
            // Webhook
            CreateMap<KiotaModels.Webhook, Webhook>().ReverseMap();
            
            // ===== Request Body Models for Roles API =====
            CreateMap<AddRoleScopeRequest, Kiota.Management.Api.V1.Roles.Item.Scopes.ScopesPostRequestBody>().ReverseMap();
            CreateMap<CreateRoleRequest, Kiota.Management.Api.V1.Roles.RolesPostRequestBody>().ReverseMap();
            CreateMap<UpdateRolesRequest, Kiota.Management.Api.V1.Roles.Item.WithRole_PatchRequestBody>().ReverseMap();
            CreateMap<UpdateRolePermissionsRequest, Kiota.Management.Api.V1.Roles.Item.Permissions.PermissionsPatchRequestBody>().ReverseMap();
            CreateMap<UpdateRolePermissionsRequestPermissionsInner, Kiota.Management.Api.V1.Roles.Item.Permissions.PermissionsPatchRequestBody_permissions>().ReverseMap();
            
            // ===== Request Body Models for Applications API =====
            CreateMap<CreateApplicationRequest, Kiota.Management.Api.V1.Applications.ApplicationsPostRequestBody>().ReverseMap();
            CreateMap<UpdateApplicationRequest, Kiota.Management.Api.V1.Applications.Item.Application_PatchRequestBody>().ReverseMap();
            CreateMap<UpdateApplicationTokensRequest, Kiota.Management.Api.V1.Applications.Item.Tokens.TokensPatchRequestBody>().ReverseMap();
            CreateMap<UpdateApplicationsPropertyRequest, Kiota.Management.Api.V1.Applications.Item.Properties.Item.WithProperty_keyPutRequestBody>().ReverseMap();
            
            // ===== Request Body Models for Billing API =====
            CreateMap<CreateBillingAgreementRequest, Kiota.Management.Api.V1.Billing.Agreements.AgreementsPostRequestBody>().ReverseMap();
            CreateMap<CreateMeterUsageRecordRequest, Kiota.Management.Api.V1.Billing.Meter_usage.Meter_usagePostRequestBody>().ReverseMap();
            
            // ===== Request Body Models for Business API =====
            CreateMap<UpdateBusinessRequest, Kiota.Management.Api.V1.Business.BusinessPatchRequestBody>().ReverseMap();
            
            // ===== Request Body Models for MFA API =====
            CreateMap<ReplaceMFARequest, Kiota.Management.Api.V1.Mfa.MfaPutRequestBody>().ReverseMap();
            
            // ===== Request Body Models for Users API =====
            CreateMap<CreateUserRequest, Kiota.Management.Api.V1.User.UserPostRequestBody>().ReverseMap();
            CreateMap<CreateUserRequestProfile, Kiota.Management.Api.V1.User.UserPostRequestBody_profile>().ReverseMap();
            CreateMap<CreateUserRequestIdentitiesInner, Kiota.Management.Api.V1.User.UserPostRequestBody_identities>().ReverseMap();
            CreateMap<UpdateUserRequest, Kiota.Management.Api.V1.User.UserPatchRequestBody>().ReverseMap();
            
            // ===== Request Body Models for Organizations API =====
            CreateMap<CreateOrganizationRequest, Kiota.Management.Api.V1.Organization.OrganizationPostRequestBody>()
                .ForMember(dest => dest.FeatureFlags, opt => opt.Ignore()) // Type mismatch: Dict<string, enum> vs object
                .ReverseMap()
                .ForMember(dest => dest.FeatureFlags, opt => opt.Ignore());
            CreateMap<AddOrganizationUsersRequest, Kiota.Management.Api.V1.Organizations.Item.Users.UsersPostRequestBody>().ReverseMap();
            CreateMap<AddOrganizationUsersRequestUsersInner, Kiota.Management.Api.V1.Organizations.Item.Users.UsersPostRequestBody_users>().ReverseMap();
            
            // Additional User mappings (Users_response_users to OpenAPI User model)
            CreateMap<KiotaModels.Users_response_users, User>().ReverseMap();
        }
    }
}

