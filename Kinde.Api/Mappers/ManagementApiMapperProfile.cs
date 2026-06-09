using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            AllowNullCollections = true;
            AllowNullDestinationValues = true;


            CreateMap<KiotaModels.Add_organization_users_response, AddOrganizationUsersResponse>().ReverseMap();

            CreateMap<KiotaModels.Add_role_scope_response, AddRoleScopeResponse>().ReverseMap();

            CreateMap<KiotaModels.Applications, Applications>().ReverseMap();

            CreateMap<KiotaModels.Authorize_app_api_response, AuthorizeAppApiResponse>().ReverseMap();

            CreateMap<KiotaModels.Category, Category>().ReverseMap();

            CreateMap<KiotaModels.Connected_apps_access_token, ConnectedAppsAccessToken>().ReverseMap();
            CreateMap<KiotaModels.Connected_apps_auth_url, ConnectedAppsAuthUrl>().ReverseMap();

            CreateMap<KiotaModels.Connection, Connection>().ReverseMap();
            CreateMap<KiotaModels.Connection_connection, ConnectionConnection>().ReverseMap();

            CreateMap<KiotaModels.Create_api_key_response, CreateApiKeyResponse>().ReverseMap();
            CreateMap<KiotaModels.Create_api_key_response_api_key, CreateApiKeyResponseApiKey>().ReverseMap();

            CreateMap<KiotaModels.Create_api_scopes_response, CreateApiScopesResponse>().ReverseMap();
            CreateMap<KiotaModels.Create_api_scopes_response_scope, CreateApiScopesResponseScope>().ReverseMap();

            CreateMap<KiotaModels.Create_apis_response, CreateApisResponse>().ReverseMap();
            CreateMap<KiotaModels.Create_apis_response_api, CreateApisResponseApi>().ReverseMap();

            CreateMap<KiotaModels.Create_application_response, CreateApplicationResponse>().ReverseMap();
            CreateMap<KiotaModels.Create_application_response_application, CreateApplicationResponseApplication>().ReverseMap();

            CreateMap<KiotaModels.Create_category_response, CreateCategoryResponse>().ReverseMap();
            CreateMap<KiotaModels.Create_category_response_category, CreateCategoryResponseCategory>().ReverseMap();
            CreateMap<CreateCategoryRequest, Kiota.Management.Api.V1.Property_categories.Property_categoriesPostRequestBody>()
                .ForMember(dest => dest.Context, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<UpdateCategoryRequest, Kiota.Management.Api.V1.Property_categories.Item.WithCategory_PutRequestBody>().ReverseMap();

            CreateMap<CreateConnectionRequestOptionsOneOf2.NameIdFormatEnum, Kiota.Management.Api.V1.Connections.ConnectionsPostRequestBody_optionsMember3_name_id_format>()
                .ConvertUsing(s => BridgeEnumByMember<Kiota.Management.Api.V1.Connections.ConnectionsPostRequestBody_optionsMember3_name_id_format>(s));
            CreateMap<CreateConnectionRequestOptionsOneOf2.ProtocolBindingEnum, Kiota.Management.Api.V1.Connections.ConnectionsPostRequestBody_optionsMember3_protocol_binding>()
                .ConvertUsing(s => BridgeEnumByMember<Kiota.Management.Api.V1.Connections.ConnectionsPostRequestBody_optionsMember3_protocol_binding>(s));
            CreateMap<CreateConnectionRequestOptionsOneOf2.SignRequestAlgorithmEnum, Kiota.Management.Api.V1.Connections.ConnectionsPostRequestBody_optionsMember3_sign_request_algorithm>()
                .ConvertUsing(s => BridgeEnumByMember<Kiota.Management.Api.V1.Connections.ConnectionsPostRequestBody_optionsMember3_sign_request_algorithm>(s));
            CreateMap<ReplaceConnectionRequestOptionsOneOf1.NameIdFormatEnum, Kiota.Management.Api.V1.Connections.Item.WithConnection_PutRequestBody_optionsMember3_name_id_format>()
                .ConvertUsing(s => BridgeEnumByMember<Kiota.Management.Api.V1.Connections.Item.WithConnection_PutRequestBody_optionsMember3_name_id_format>(s));
            CreateMap<ReplaceConnectionRequestOptionsOneOf1.ProtocolBindingEnum, Kiota.Management.Api.V1.Connections.Item.WithConnection_PutRequestBody_optionsMember3_protocol_binding>()
                .ConvertUsing(s => BridgeEnumByMember<Kiota.Management.Api.V1.Connections.Item.WithConnection_PutRequestBody_optionsMember3_protocol_binding>(s));
            CreateMap<ReplaceConnectionRequestOptionsOneOf1.SignRequestAlgorithmEnum, Kiota.Management.Api.V1.Connections.Item.WithConnection_PutRequestBody_optionsMember3_sign_request_algorithm>()
                .ConvertUsing(s => BridgeEnumByMember<Kiota.Management.Api.V1.Connections.Item.WithConnection_PutRequestBody_optionsMember3_sign_request_algorithm>(s));
            CreateMap<UpdateConnectionRequestOptionsOneOf1.NameIdFormatEnum, Kiota.Management.Api.V1.Connections.Item.WithConnection_PatchRequestBody_optionsMember3_name_id_format>()
                .ConvertUsing(s => BridgeEnumByMember<Kiota.Management.Api.V1.Connections.Item.WithConnection_PatchRequestBody_optionsMember3_name_id_format>(s));
            CreateMap<UpdateConnectionRequestOptionsOneOf1.ProtocolBindingEnum, Kiota.Management.Api.V1.Connections.Item.WithConnection_PatchRequestBody_optionsMember3_protocol_binding>()
                .ConvertUsing(s => BridgeEnumByMember<Kiota.Management.Api.V1.Connections.Item.WithConnection_PatchRequestBody_optionsMember3_protocol_binding>(s));
            CreateMap<UpdateConnectionRequestOptionsOneOf1.SignRequestAlgorithmEnum, Kiota.Management.Api.V1.Connections.Item.WithConnection_PatchRequestBody_optionsMember3_sign_request_algorithm>()
                .ConvertUsing(s => BridgeEnumByMember<Kiota.Management.Api.V1.Connections.Item.WithConnection_PatchRequestBody_optionsMember3_sign_request_algorithm>(s));

            CreateMap<KiotaModels.Create_connection_response, CreateConnectionResponse>().ReverseMap();
            CreateMap<KiotaModels.Create_connection_response_connection, CreateConnectionResponseConnection>().ReverseMap();

            CreateMap<CreateConnectionRequestOptionsOneOf, Kiota.Management.Api.V1.Connections.ConnectionsPostRequestBody_optionsMember1>().ReverseMap();
            CreateMap<CreateConnectionRequestOptionsOneOf1, Kiota.Management.Api.V1.Connections.ConnectionsPostRequestBody_optionsMember2>().ReverseMap();
            CreateMap<CreateConnectionRequestOptionsOneOf2, Kiota.Management.Api.V1.Connections.ConnectionsPostRequestBody_optionsMember3>().ReverseMap();

            CreateMap<CreateConnectionRequestOptions, Kiota.Management.Api.V1.Connections.ConnectionsPostRequestBody.ConnectionsPostRequestBody_options>()
                .ConvertUsing((src, _, ctx) =>
                {
                    if (src?.ActualInstance is null) return null;

                    var dst = new Kiota.Management.Api.V1.Connections.ConnectionsPostRequestBody.ConnectionsPostRequestBody_options();
                    switch (src.ActualInstance)
                    {
                        case CreateConnectionRequestOptionsOneOf s: dst.ConnectionsPostRequestBodyOptionsMember1 = ctx.Mapper.Map<Kiota.Management.Api.V1.Connections.ConnectionsPostRequestBody_optionsMember1>(s); break;
                        case CreateConnectionRequestOptionsOneOf1 s: dst.ConnectionsPostRequestBodyOptionsMember2 = ctx.Mapper.Map<Kiota.Management.Api.V1.Connections.ConnectionsPostRequestBody_optionsMember2>(s); break;
                        case CreateConnectionRequestOptionsOneOf2 s: dst.ConnectionsPostRequestBodyOptionsMember3 = ctx.Mapper.Map<Kiota.Management.Api.V1.Connections.ConnectionsPostRequestBody_optionsMember3>(s); break;
                        default:
                            throw new ArgumentException(
                                $"Unsupported CreateConnectionRequestOptions variant: {src.ActualInstance.GetType().FullName}. " +
                                "Expected CreateConnectionRequestOptionsOneOf, OneOf1, or OneOf2.",
                                nameof(src));
                    }
                    return dst;
                });

            CreateMap<CreateConnectionRequest, Kiota.Management.Api.V1.Connections.ConnectionsPostRequestBody>();

                .ReverseMap()
            CreateMap<CreateConnectionRequestOptionsOneOf, Kiota.Management.Api.V1.Connections.Item.WithConnection_PutRequestBody_optionsMember1>().ReverseMap();
            CreateMap<ReplaceConnectionRequestOptionsOneOf, Kiota.Management.Api.V1.Connections.Item.WithConnection_PutRequestBody_optionsMember2>().ReverseMap();
            CreateMap<ReplaceConnectionRequestOptionsOneOf1, Kiota.Management.Api.V1.Connections.Item.WithConnection_PutRequestBody_optionsMember3>().ReverseMap();

            CreateMap<ReplaceConnectionRequestOptions, Kiota.Management.Api.V1.Connections.Item.WithConnection_PutRequestBody.WithConnection_PutRequestBody_options>()
                .ConvertUsing((src, _, ctx) =>
                {
                    if (src?.ActualInstance is null) return null;

                    var dst = new Kiota.Management.Api.V1.Connections.Item.WithConnection_PutRequestBody.WithConnection_PutRequestBody_options();
                    switch (src.ActualInstance)
                    {
                        case CreateConnectionRequestOptionsOneOf s: dst.WithConnectionPutRequestBodyOptionsMember1 = ctx.Mapper.Map<Kiota.Management.Api.V1.Connections.Item.WithConnection_PutRequestBody_optionsMember1>(s); break;
                        case ReplaceConnectionRequestOptionsOneOf s: dst.WithConnectionPutRequestBodyOptionsMember2 = ctx.Mapper.Map<Kiota.Management.Api.V1.Connections.Item.WithConnection_PutRequestBody_optionsMember2>(s); break;
                        case ReplaceConnectionRequestOptionsOneOf1 s: dst.WithConnectionPutRequestBodyOptionsMember3 = ctx.Mapper.Map<Kiota.Management.Api.V1.Connections.Item.WithConnection_PutRequestBody_optionsMember3>(s); break;
                        default:
                            throw new ArgumentException(
                                $"Unsupported ReplaceConnectionRequestOptions variant: {src.ActualInstance.GetType().FullName}. " +
                                "Expected CreateConnectionRequestOptionsOneOf, ReplaceConnectionRequestOptionsOneOf, or ReplaceConnectionRequestOptionsOneOf1.",
                                nameof(src));
                    }
                    return dst;
                });

            CreateMap<ReplaceConnectionRequest, Kiota.Management.Api.V1.Connections.Item.WithConnection_PutRequestBody>();

            CreateMap<CreateConnectionRequestOptionsOneOf, Kiota.Management.Api.V1.Connections.Item.WithConnection_PatchRequestBody_optionsMember1>().ReverseMap();
            CreateMap<UpdateConnectionRequestOptionsOneOf, Kiota.Management.Api.V1.Connections.Item.WithConnection_PatchRequestBody_optionsMember2>().ReverseMap();
            CreateMap<UpdateConnectionRequestOptionsOneOf1, Kiota.Management.Api.V1.Connections.Item.WithConnection_PatchRequestBody_optionsMember3>().ReverseMap();

            CreateMap<UpdateConnectionRequestOptions, Kiota.Management.Api.V1.Connections.Item.WithConnection_PatchRequestBody.WithConnection_PatchRequestBody_options>()
                .ConvertUsing((src, _, ctx) =>
                {
                    if (src?.ActualInstance is null) return null;

                    var dst = new Kiota.Management.Api.V1.Connections.Item.WithConnection_PatchRequestBody.WithConnection_PatchRequestBody_options();
                    switch (src.ActualInstance)
                    {
                        case CreateConnectionRequestOptionsOneOf s: dst.WithConnectionPatchRequestBodyOptionsMember1 = ctx.Mapper.Map<Kiota.Management.Api.V1.Connections.Item.WithConnection_PatchRequestBody_optionsMember1>(s); break;
                        case UpdateConnectionRequestOptionsOneOf s: dst.WithConnectionPatchRequestBodyOptionsMember2 = ctx.Mapper.Map<Kiota.Management.Api.V1.Connections.Item.WithConnection_PatchRequestBody_optionsMember2>(s); break;
                        case UpdateConnectionRequestOptionsOneOf1 s: dst.WithConnectionPatchRequestBodyOptionsMember3 = ctx.Mapper.Map<Kiota.Management.Api.V1.Connections.Item.WithConnection_PatchRequestBody_optionsMember3>(s); break;
                        default:
                            throw new ArgumentException(
                                $"Unsupported UpdateConnectionRequestOptions variant: {src.ActualInstance.GetType().FullName}. " +
                                "Expected CreateConnectionRequestOptionsOneOf, UpdateConnectionRequestOptionsOneOf, or UpdateConnectionRequestOptionsOneOf1.",
                                nameof(src));
                    }
                    return dst;
                });

            CreateMap<UpdateConnectionRequest, Kiota.Management.Api.V1.Connections.Item.WithConnection_PatchRequestBody>();

            CreateMap<KiotaModels.Create_environment_variable_response, CreateEnvironmentVariableResponse>().ReverseMap();
            CreateMap<KiotaModels.Create_environment_variable_response_environment_variable, CreateEnvironmentVariableResponseEnvironmentVariable>().ReverseMap();
            CreateMap<CreateEnvironmentVariableRequest, Kiota.Management.Api.V1.Environment_variables.Environment_variablesPostRequestBody>().ReverseMap();
            CreateMap<UpdateEnvironmentVariableRequest, Kiota.Management.Api.V1.Environment_variables.Item.WithVariable_PatchRequestBody>().ReverseMap();

            CreateMap<KiotaModels.Create_identity_response, CreateIdentityResponse>().ReverseMap();
            CreateMap<KiotaModels.Create_identity_response_identity, CreateIdentityResponseIdentity>().ReverseMap();

            CreateMap<KiotaModels.Create_meter_usage_record_response, CreateMeterUsageRecordResponse>().ReverseMap();

            CreateMap<KiotaModels.Create_organization_response, CreateOrganizationResponse>().ReverseMap();
            CreateMap<KiotaModels.Create_organization_response_organization, CreateOrganizationResponseOrganization>().ReverseMap();

            CreateMap<KiotaModels.Create_property_response, CreatePropertyResponse>().ReverseMap();
            CreateMap<KiotaModels.Create_property_response_property, CreatePropertyResponseProperty>().ReverseMap();
            CreateMap<CreatePropertyRequest, Kiota.Management.Api.V1.Properties.PropertiesPostRequestBody>()
                .ForMember(dest => dest.Context, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<UpdatePropertyRequest, Kiota.Management.Api.V1.Properties.Item.WithProperty_PutRequestBody>().ReverseMap();

            CreateMap<KiotaModels.Create_roles_response, CreateRolesResponse>().ReverseMap();
            CreateMap<KiotaModels.Create_roles_response_role, CreateRolesResponseRole>().ReverseMap();

            CreateMap<KiotaModels.Create_subscriber_success_response, CreateSubscriberSuccessResponse>().ReverseMap();
            CreateMap<KiotaModels.Create_subscriber_success_response_subscriber, CreateSubscriberSuccessResponseSubscriber>().ReverseMap();

            CreateMap<KiotaModels.Create_user_response, CreateUserResponse>().ReverseMap();

            CreateMap<KiotaModels.Create_webhook_response, CreateWebhookResponse>().ReverseMap();
            CreateMap<KiotaModels.Create_webhook_response_webhook, CreateWebhookResponseWebhook>().ReverseMap();
            CreateMap<CreateWebHookRequest, Kiota.Management.Api.V1.Webhooks.WebhooksPostRequestBody>().ReverseMap();
            CreateMap<UpdateWebHookRequest, Kiota.Management.Api.V1.Webhooks.Item.WithWebhook_PatchRequestBody>().ReverseMap();

            CreateMap<KiotaModels.Delete_api_response, DeleteApiResponse>().ReverseMap();
            CreateMap<KiotaModels.Delete_environment_variable_response, DeleteEnvironmentVariableResponse>().ReverseMap();
            CreateMap<KiotaModels.Delete_role_scope_response, DeleteRoleScopeResponse>().ReverseMap();
            CreateMap<KiotaModels.Delete_webhook_response, DeleteWebhookResponse>().ReverseMap();

            CreateMap<KiotaModels.Environment_variable, EnvironmentVariable>().ReverseMap();

            CreateMap<KiotaModels.Error, Error>().ReverseMap();
            CreateMap<KiotaModels.Error_response, ErrorResponse>().ReverseMap();

            CreateMap<KiotaModels.Event_type, EventType>().ReverseMap();

            CreateMap<KiotaModels.Get_api_key_response, GetApiKeyResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_api_key_response_api_key, GetApiKeyResponseApiKey>().ReverseMap();
            CreateMap<KiotaModels.Get_api_keys_response, GetApiKeysResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_api_keys_response_api_keys, GetApiKeysResponseApiKeysInner>().ReverseMap();

            CreateMap<KiotaModels.Get_api_response, GetApiResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_api_response_api, GetApiResponseApi>().ReverseMap();
            CreateMap<KiotaModels.Get_api_response_api_applications, GetApiResponseApiApplicationsInner>().ReverseMap();
            CreateMap<KiotaModels.Get_api_response_api_scopes, GetApiResponseApiScopesInner>().ReverseMap();

            CreateMap<KiotaModels.Get_api_scope_response, GetApiScopeResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_api_scopes_response, GetApiScopesResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_api_scopes_response_scopes, GetApiScopesResponseScopesInner>().ReverseMap();

            CreateMap<KiotaModels.Get_apis_response, GetApisResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_apis_response_apis, GetApisResponseApisInner>().ReverseMap();
            CreateMap<KiotaModels.Get_apis_response_apis_scopes, GetApisResponseApisInnerScopesInner>().ReverseMap();

            CreateMap<KiotaModels.Get_application_response, GetApplicationResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_application_response_application, GetApplicationResponseApplication>().ReverseMap();
            CreateMap<KiotaModels.Get_applications_response, GetApplicationsResponse>().ReverseMap();

            CreateMap<KiotaModels.Get_billing_agreements_response, GetBillingAgreementsResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_billing_agreements_response_agreements, GetBillingAgreementsResponseAgreementsInner>().ReverseMap();
            CreateMap<KiotaModels.Get_billing_agreements_response_agreements_entitlements, GetBillingAgreementsResponseAgreementsInnerEntitlementsInner>().ReverseMap();
            CreateMap<KiotaModels.Get_billing_entitlements_response, GetBillingEntitlementsResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_billing_entitlements_response_entitlements, GetBillingEntitlementsResponseEntitlementsInner>().ReverseMap();
            CreateMap<KiotaModels.Get_billing_entitlements_response_plans, GetBillingEntitlementsResponsePlansInner>().ReverseMap();

            CreateMap<KiotaModels.Get_business_response, GetBusinessResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_business_response_business, GetBusinessResponseBusiness>().ReverseMap();

            CreateMap<KiotaModels.Get_categories_response, GetCategoriesResponse>().ReverseMap();

            CreateMap<KiotaModels.Get_connections_response, GetConnectionsResponse>().ReverseMap();

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

            CreateMap<KiotaModels.Get_event_response, GetEventResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_event_response_event, GetEventResponseEvent>().ReverseMap();
            CreateMap<KiotaModels.Get_event_types_response, GetEventTypesResponse>().ReverseMap();

            CreateMap<KiotaModels.Get_identities_response, GetIdentitiesResponse>().ReverseMap();

            CreateMap<KiotaModels.Get_industries_response, GetIndustriesResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_industries_response_industries, GetIndustriesResponseIndustriesInner>().ReverseMap();

            CreateMap<KiotaModels.Get_organization_feature_flags_response, GetOrganizationFeatureFlagsResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_organization_feature_flags_response_feature_flags, GetOrganizationFeatureFlagsResponseFeatureFlagsValue>().ReverseMap();
            CreateMap<KiotaModels.Get_organization_response, GetOrganizationResponse>()
                .AfterMap((src, dst) =>
                {
                    if (ReadAdditionalBool(src.AdditionalData, "is_suspended") is { } sus) dst.IsSuspended = sus;
                    if (ReadAdditionalString(src.AdditionalData, "suspended_on") is { } so) dst.SuspendedOn = so;
                })
                .ReverseMap()
                .AfterMap((src, dst) =>
                {
                    dst.AdditionalData ??= new Dictionary<string, object>();
                    dst.AdditionalData["is_suspended"] = src.IsSuspended;
                    if (src.SuspendedOn is { Length: > 0 } so) dst.AdditionalData["suspended_on"] = so;
                });
            CreateMap<KiotaModels.Get_organization_response_billing, GetOrganizationResponseBilling>().ReverseMap();
            CreateMap<KiotaModels.Get_organization_response_billing_agreements, GetOrganizationResponseBillingAgreementsInner>().ReverseMap();
            CreateMap<KiotaModels.Get_organization_users_response, GetOrganizationUsersResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_organizations_response, GetOrganizationsResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_organizations_user_permissions_response, GetOrganizationsUserPermissionsResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_organizations_user_roles_response, GetOrganizationsUserRolesResponse>().ReverseMap();

            CreateMap<KiotaModels.Get_permissions_response, GetPermissionsResponse>().ReverseMap();
            CreateMap<CreatePermissionRequest, Kiota.Management.Api.V1.Permissions.PermissionsPostRequestBody>().ReverseMap();
            CreateMap<CreatePermissionRequest, Kiota.Management.Api.V1.Permissions.Item.WithPermission_PatchRequestBody>().ReverseMap();

            CreateMap<KiotaModels.Get_properties_response, GetPropertiesResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_property_values_response, GetPropertyValuesResponse>().ReverseMap();

            CreateMap<KiotaModels.Get_role_response, GetRoleResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_role_response_role, GetRoleResponseRole>().ReverseMap();
            CreateMap<KiotaModels.Get_roles_response, GetRolesResponse>().ReverseMap();

            CreateMap<KiotaModels.Get_subscriber_response, GetSubscriberResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_subscribers_response, GetSubscribersResponse>().ReverseMap();

            CreateMap<KiotaModels.Get_timezones_response, GetTimezonesResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_timezones_response_timezones, GetTimezonesResponseTimezonesInner>().ReverseMap();

            CreateMap<KiotaModels.Get_user_mfa_response, GetUserMfaResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_user_mfa_response_mfa, GetUserMfaResponseMfa>().ReverseMap();

            CreateMap<KiotaModels.Get_user_sessions_response, GetUserSessionsResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_user_sessions_response_sessions, GetUserSessionsResponseSessionsInner>().ReverseMap();

            CreateMap<KiotaModels.Get_webhooks_response, GetWebhooksResponse>().ReverseMap();

            CreateMap<KiotaModels.Identity, Identity>().ReverseMap();
            CreateMap<UpdateIdentityRequest, Kiota.Management.Api.V1.Identities.Item.WithIdentity_PatchRequestBody>().ReverseMap();

            CreateMap<KiotaModels.Logout_redirect_urls, LogoutRedirectUrls>().ReverseMap();
            CreateMap<ReplaceLogoutRedirectURLsRequest, Kiota.Management.Api.V1.Applications.Item.Auth_logout_urls.Auth_logout_urlsPostRequestBody>().ReverseMap();
            CreateMap<ReplaceLogoutRedirectURLsRequest, Kiota.Management.Api.V1.Applications.Item.Auth_logout_urls.Auth_logout_urlsPutRequestBody>().ReverseMap();

            CreateMap<KiotaModels.Not_found_response, NotFoundResponse>().ReverseMap();
            CreateMap<KiotaModels.Not_found_response_errors, NotFoundResponseErrors>().ReverseMap();

            CreateMap<KiotaModels.Organization_item_schema, OrganizationItemSchema>().ReverseMap();
            CreateMap<KiotaModels.Organization_user, OrganizationUser>().ReverseMap();
            CreateMap<KiotaModels.Organization_user_permission, OrganizationUserPermission>().ReverseMap();
            CreateMap<KiotaModels.Organization_user_permission_roles, OrganizationUserPermissionRolesInner>().ReverseMap();
            CreateMap<KiotaModels.Organization_user_role, OrganizationUserRole>().ReverseMap();

            CreateMap<KiotaModels.Permissions, Permissions>().ReverseMap();

            CreateMap<KiotaModels.Property, Property>().ReverseMap();
            CreateMap<KiotaModels.Property_value, PropertyValue>().ReverseMap();

            CreateMap<KiotaModels.Read_env_logo_response, ReadEnvLogoResponse>().ReverseMap();
            CreateMap<KiotaModels.Read_env_logo_response_logos, ReadEnvLogoResponseLogosInner>().ReverseMap();
            CreateMap<KiotaModels.Read_logo_response, ReadLogoResponse>().ReverseMap();
            CreateMap<KiotaModels.Read_logo_response_logos, ReadLogoResponseLogosInner>().ReverseMap();

            CreateMap<KiotaModels.Redirect_callback_urls, RedirectCallbackUrls>().ReverseMap();
            CreateMap<ReplaceRedirectCallbackURLsRequest, Kiota.Management.Api.V1.Applications.Item.Auth_redirect_urls.Auth_redirect_urlsPostRequestBody>().ReverseMap();
            CreateMap<ReplaceRedirectCallbackURLsRequest, Kiota.Management.Api.V1.Applications.Item.Auth_redirect_urls.Auth_redirect_urlsPutRequestBody>().ReverseMap();

            CreateMap<KiotaModels.Role_permissions_response, RolePermissionsResponse>().ReverseMap();
            CreateMap<KiotaModels.Role_scopes_response, RoleScopesResponse>().ReverseMap();
            CreateMap<KiotaModels.Roles, Roles>().ReverseMap();

            CreateMap<KiotaModels.Rotate_api_key_response, RotateApiKeyResponse>().ReverseMap();
            CreateMap<KiotaModels.Rotate_api_key_response_api_key, RotateApiKeyResponseApiKey>().ReverseMap();

            CreateMap<KiotaModels.Scopes, Scopes>().ReverseMap();

            CreateMap<KiotaModels.Search_users_response, SearchUsersResponse>().ReverseMap();
            CreateMap<KiotaModels.Search_users_response_results, SearchUsersResponseResultsInner>().ReverseMap();
            CreateMap<KiotaModels.Search_users_response_results_api_scopes, SearchUsersResponseResultsInnerApiScopesInner>().ReverseMap();

            CreateMap<KiotaModels.Subscriber, Subscriber>().ReverseMap();
            CreateMap<KiotaModels.Subscribers_subscriber, SubscribersSubscriber>().ReverseMap();

            CreateMap<KiotaModels.Success_response, SuccessResponse>().ReverseMap();

            CreateMap<KiotaModels.Update_environment_variable_response, UpdateEnvironmentVariableResponse>().ReverseMap();
            CreateMap<KiotaModels.Update_organization_users_response, UpdateOrganizationUsersResponse>().ReverseMap();
            CreateMap<KiotaModels.Update_role_permissions_response, UpdateRolePermissionsResponse>().ReverseMap();
            CreateMap<KiotaModels.Update_user_response, UpdateUserResponse>().ReverseMap();
            CreateMap<KiotaModels.Update_webhook_response, UpdateWebhookResponse>().ReverseMap();
            CreateMap<KiotaModels.Update_webhook_response_webhook, UpdateWebhookResponseWebhook>().ReverseMap();

            CreateMap<KiotaModels.User, User>().ReverseMap();
            CreateMap<KiotaModels.User_billing, UserBilling>().ReverseMap();
            CreateMap<KiotaModels.User_identities, UserIdentitiesInner>().ReverseMap();
            CreateMap<KiotaModels.User_identity, UserIdentity>().ReverseMap();
            CreateMap<KiotaModels.User_identity_result, UserIdentityResult>().ReverseMap();
            CreateMap<KiotaModels.Users_response, UsersResponse>().ReverseMap();
            CreateMap<KiotaModels.Users_response_users, UsersResponseUsersInner>().ReverseMap();
            CreateMap<KiotaModels.Users_response_users_billing, UsersResponseUsersInnerBilling>().ReverseMap();

            CreateMap<KiotaModels.Verify_api_key_response, VerifyApiKeyResponse>().ReverseMap();

            CreateMap<KiotaModels.Webhook, Webhook>().ReverseMap();

            CreateMap<AddRoleScopeRequest, Kiota.Management.Api.V1.Roles.Item.Scopes.ScopesPostRequestBody>().ReverseMap();
            CreateMap<CreateRoleRequest, Kiota.Management.Api.V1.Roles.RolesPostRequestBody>().ReverseMap();
            CreateMap<UpdateRolesRequest, Kiota.Management.Api.V1.Roles.Item.WithRole_PatchRequestBody>().ReverseMap();
            CreateMap<UpdateRolePermissionsRequest, Kiota.Management.Api.V1.Roles.Item.Permissions.PermissionsPatchRequestBody>().ReverseMap();
            CreateMap<UpdateRolePermissionsRequestPermissionsInner, Kiota.Management.Api.V1.Roles.Item.Permissions.PermissionsPatchRequestBody_permissions>().ReverseMap();

            CreateMap<CreateApplicationRequest, Kiota.Management.Api.V1.Applications.ApplicationsPostRequestBody>().ReverseMap();
            CreateMap<UpdateApplicationRequest, Kiota.Management.Api.V1.Applications.Item.Application_PatchRequestBody>().ReverseMap();
            CreateMap<UpdateApplicationTokensRequest, Kiota.Management.Api.V1.Applications.Item.Tokens.TokensPatchRequestBody>().ReverseMap();
            CreateMap<UpdateApplicationsPropertyRequest, Kiota.Management.Api.V1.Applications.Item.Properties.Item.WithProperty_keyPutRequestBody>().ReverseMap();

            CreateMap<CreateBillingAgreementRequest, Kiota.Management.Api.V1.Billing.Agreements.AgreementsPostRequestBody>().ReverseMap();
            CreateMap<CreateMeterUsageRecordRequest, Kiota.Management.Api.V1.Billing.Meter_usage.Meter_usagePostRequestBody>().ReverseMap();

            CreateMap<UpdateBusinessRequest, Kiota.Management.Api.V1.Business.BusinessPatchRequestBody>().ReverseMap();

            CreateMap<ReplaceMFARequest, Kiota.Management.Api.V1.Mfa.MfaPutRequestBody>()
                .AfterMap((src, dst) =>
                {
                    dst.AdditionalData ??= new Dictionary<string, object>();
                    dst.AdditionalData["is_recovery_codes_enabled"] = src.IsRecoveryCodesEnabled;
                })
                .ReverseMap()
                .AfterMap((src, dst) =>
                {
                    if (ReadAdditionalBool(src.AdditionalData, "is_recovery_codes_enabled") is { } v) dst.IsRecoveryCodesEnabled = v;
                });

            CreateMap<CreateUserRequest, Kiota.Management.Api.V1.User.UserPostRequestBody>().ReverseMap();
            CreateMap<CreateUserRequestProfile, Kiota.Management.Api.V1.User.UserPostRequestBody_profile>().ReverseMap();
            CreateMap<CreateUserRequestIdentitiesInner, Kiota.Management.Api.V1.User.UserPostRequestBody_identities>().ReverseMap();
            CreateMap<UpdateUserRequest, Kiota.Management.Api.V1.User.UserPatchRequestBody>().ReverseMap();

            CreateMap<CreateOrganizationRequest, Kiota.Management.Api.V1.Organization.OrganizationPostRequestBody>()
                .ForMember(dest => dest.FeatureFlags, opt => opt.Ignore())
                .AfterMap((src, dst) =>
                {
                    dst.AdditionalData ??= new Dictionary<string, object>();
                    dst.AdditionalData["is_auto_membership_enabled"] = src.IsAutoMembershipEnabled;
                })
                .ReverseMap()
                .ForMember(dest => dest.FeatureFlags, opt => opt.Ignore())
                .AfterMap((src, dst) =>
                {
                    if (ReadAdditionalBool(src.AdditionalData, "is_auto_membership_enabled") is { } v) dst.IsAutoMembershipEnabled = v;
                });
            CreateMap<AddOrganizationUsersRequest, Kiota.Management.Api.V1.Organizations.Item.Users.UsersPostRequestBody>().ReverseMap();
            CreateMap<AddOrganizationUsersRequestUsersInner, Kiota.Management.Api.V1.Organizations.Item.Users.UsersPostRequestBody_users>().ReverseMap();

            CreateMap<KiotaModels.Users_response_users, User>().ReverseMap();

            CreateMap<CreateApiKeyRequest, Kiota.Management.Api.V1.Api_keys.Api_keysPostRequestBody>().ReverseMap();
            CreateMap<VerifyApiKeyRequest, Kiota.Management.Api.V1.Api_keys.Verify.VerifyPostRequestBody>().ReverseMap();

            CreateMap<AddAPIsRequest, Kiota.Management.Api.V1.Apis.ApisPostRequestBody>().ReverseMap();
            CreateMap<UpdateAPIApplicationsRequest, Kiota.Management.Api.V1.Apis.Item.Applications.ApplicationsPatchRequestBody>().ReverseMap();
            CreateMap<UpdateAPIScopeRequest, Kiota.Management.Api.V1.Apis.Item.Scopes.Item.WithScope_PatchRequestBody>().ReverseMap();

            CreateMap<UpdateOrganizationRequest, Kiota.Management.Api.V1.Organization.Item.WithOrg_codePatchRequestBody>().ReverseMap();

            CreateMap<UpdateOrganizationPropertiesRequest, Kiota.Management.Api.V1.Organizations.Item.Properties.PropertiesPatchRequestBody>().ReverseMap();
            CreateMap<UpdateOrganizationSessionsRequest, Kiota.Management.Api.V1.Organizations.Item.Sessions.SessionsPatchRequestBody>().ReverseMap();
            CreateMap<UpdateOrganizationUsersRequest, Kiota.Management.Api.V1.Organizations.Item.Users.UsersPatchRequestBody>().ReverseMap();

            CreateMap<CreateUserIdentityRequest, Kiota.Management.Api.V1.Users.Item.Identities.IdentitiesPostRequestBody>().ReverseMap();
            CreateMap<SetUserPasswordRequest, Kiota.Management.Api.V1.Users.Item.Password.PasswordPutRequestBody>().ReverseMap();
            CreateMap<UpdateOrganizationPropertiesRequest, Kiota.Management.Api.V1.Users.Item.Properties.PropertiesPatchRequestBody>().ReverseMap();
        }

        private static bool? ReadAdditionalBool(IDictionary<string, object> data, string key)
        {
            if (data is null || !data.TryGetValue(key, out var v) || v is null) return null;
            if (v is bool b) return b;
            if (v is string s && bool.TryParse(s, out var parsed)) return parsed;
            var prop = v.GetType().GetProperty("Value");
            if (prop is not null)
            {
                var inner = prop.GetValue(v);
                if (inner is bool ib) return ib;
                if (inner is string istr && bool.TryParse(istr, out var ip)) return ip;
            }
            return bool.TryParse(v.ToString(), out var fallback) ? fallback : (bool?)null;
        }

        private static string ReadAdditionalString(IDictionary<string, object> data, string key)
        {
            if (data is null || !data.TryGetValue(key, out var v) || v is null) return null;
            if (v is string s) return s;
            var prop = v.GetType().GetProperty("Value");
            var inner = prop?.GetValue(v);
            return inner?.ToString() ?? v.ToString();
        }

        private static TDst BridgeEnumByMember<TDst>(Enum src) where TDst : struct, Enum
        {
            if (src is null) return default;
            var wire = GetEnumMemberValue(src);
            return TryParseEnumMember<TDst>(wire, out var v) ? v : default;
        }

        private static string GetEnumMemberValue(Enum value)
        {
            var member = value.GetType().GetMember(value.ToString()).FirstOrDefault();
            var attr = member?.GetCustomAttribute<System.Runtime.Serialization.EnumMemberAttribute>();
            return attr?.Value ?? value.ToString();
        }

        private static bool TryParseEnumMember<TEnum>(string value, out TEnum result) where TEnum : struct, Enum
        {
            foreach (var name in Enum.GetNames(typeof(TEnum)))
            {
                var member = typeof(TEnum).GetMember(name).FirstOrDefault();
                var attr = member?.GetCustomAttribute<System.Runtime.Serialization.EnumMemberAttribute>();
                if (attr?.Value == value)
                {
                    result = (TEnum)Enum.Parse(typeof(TEnum), name);
                    return true;
                }
            }
            result = default;
            return false;
        }
    }
}

