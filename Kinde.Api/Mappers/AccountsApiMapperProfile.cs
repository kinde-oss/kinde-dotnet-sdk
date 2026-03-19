using AutoMapper;
using Kinde.Accounts.Model.Entities;
using Kinde.Accounts.Model.Responses;
using KiotaModels = Kinde.Api.Kiota.Accounts.Models;

namespace Kinde.Api.Mappers
{
    /// <summary>
    /// AutoMapper profile for mapping between OpenAPI Accounts API models and Kiota Accounts API models.
    /// This profile handles bidirectional mapping to support both request and response translation.
    /// </summary>
    public class AccountsApiMapperProfile : Profile
    {
        public AccountsApiMapperProfile()
        {
            // Configure default mapping behavior
            AllowNullCollections = true;
            AllowNullDestinationValues = true;

            // ===== Error Models =====
            CreateMap<KiotaModels.Error, Error>().ReverseMap();
            CreateMap<KiotaModels.Error_response, ErrorResponse>().ReverseMap();
            CreateMap<KiotaModels.Token_error_response, TokenErrorResponse>().ReverseMap();

            // ===== Entitlement Models =====
            CreateMap<KiotaModels.Get_entitlement_response, GetEntitlementResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_entitlement_response_data, GetEntitlementResponseData>().ReverseMap();
            CreateMap<KiotaModels.Get_entitlement_response_data_entitlement, EntitlementDetail>().ReverseMap();
            CreateMap<KiotaModels.Get_entitlement_response_metadata, GetEntitlementsResponseMetadata>().ReverseMap();

            CreateMap<KiotaModels.Get_entitlements_response, GetEntitlementsResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_entitlements_response_data, GetEntitlementsResponseData>().ReverseMap();
            CreateMap<KiotaModels.Get_entitlements_response_data_entitlements, Entitlement>().ReverseMap();
            CreateMap<KiotaModels.Get_entitlements_response_data_plans, Plan>().ReverseMap();
            CreateMap<KiotaModels.Get_entitlements_response_metadata, GetEntitlementsResponseMetadata>().ReverseMap();

            // ===== Feature Flags Models =====
            CreateMap<KiotaModels.Get_feature_flags_response, GetFeatureFlagsResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_feature_flags_response_data, GetFeatureFlagsResponseData>().ReverseMap();
            CreateMap<KiotaModels.Get_feature_flags_response_data_feature_flags, FeatureFlag>().ReverseMap();

            // ===== User Permissions Models =====
            CreateMap<KiotaModels.Get_user_permissions_response, GetUserPermissionsResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_user_permissions_response_data, GetUserPermissionsResponseData>().ReverseMap();
            CreateMap<KiotaModels.Get_user_permissions_response_data_permissions, Permission>().ReverseMap();
            CreateMap<KiotaModels.Get_user_permissions_response_metadata, GetUserPermissionsResponseMetadata>().ReverseMap();

            // ===== User Properties Models =====
            CreateMap<KiotaModels.Get_user_properties_response, GetUserPropertiesResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_user_properties_response_data, GetUserPropertiesResponseData>().ReverseMap();
            CreateMap<KiotaModels.Get_user_properties_response_data_properties, UserProperty>().ReverseMap();
            CreateMap<KiotaModels.Get_user_properties_response_metadata, GetUserPropertiesResponseMetadata>().ReverseMap();

            // ===== User Roles Models =====
            CreateMap<KiotaModels.Get_user_roles_response, GetUserRolesResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_user_roles_response_data, GetUserRolesResponseData>().ReverseMap();
            CreateMap<KiotaModels.Get_user_roles_response_data_roles, Role>().ReverseMap();
            CreateMap<KiotaModels.Get_user_roles_response_metadata, GetUserRolesResponseMetadata>().ReverseMap();

            // ===== Portal Link Models =====
            CreateMap<KiotaModels.Portal_link, PortalLink>().ReverseMap();

            // ===== Token Introspect Models =====
            CreateMap<KiotaModels.Token_introspect, TokenIntrospect>().ReverseMap();

            // ===== User Profile Models =====
            CreateMap<KiotaModels.User_profile_v2, UserProfileV2>().ReverseMap();
        }
    }
}

