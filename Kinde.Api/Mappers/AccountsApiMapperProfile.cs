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

            // Kiota's IBackedModel/IAdditionalDataHolder plumbing members never have a counterpart
            // on the OpenAPI-generated side and would otherwise fail AssertConfigurationIsValid().
            AddGlobalIgnore("AdditionalData");
            AddGlobalIgnore("BackingStore");

            // ===== Error Models =====
            CreateMap<KiotaModels.Error, Error>().ReverseMap();
            CreateMap<KiotaModels.Error_response, ErrorResponse>().ReverseMap();
            CreateMap<KiotaModels.Token_error_response, TokenErrorResponse>().ReverseMap();

            // ===== Entitlement Models =====
            CreateMap<KiotaModels.Get_entitlement_response, GetEntitlementResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_entitlement_response_data, GetEntitlementResponseData>().ReverseMap();
            CreateMap<KiotaModels.Get_entitlement_response_data_entitlement, EntitlementDetail>().ReverseMap();
            // Note: Get_entitlement_response_metadata (singular "entitlement") is an empty Kiota class
            // with no wire fields - the real Kinde API returns no pagination metadata for a single-entitlement
            // lookup. GetEntitlementResponse.Metadata is typed as `object` and never routes through a typed
            // CreateMap, so there is intentionally no CreateMap for Get_entitlement_response_metadata here -
            // mapping it to GetEntitlementsResponseMetadata (the *plural*, paginated response's metadata,
            // which has HasMore/NextPageStartingAfter) was a copy/paste bug that AutoMapper's validation
            // correctly rejects ("No available constructor").

            CreateMap<KiotaModels.Get_entitlements_response, GetEntitlementsResponse>().ReverseMap();
            CreateMap<KiotaModels.Get_entitlements_response_data, GetEntitlementsResponseData>().ReverseMap();
            CreateMap<KiotaModels.Get_entitlements_response_data_entitlements, Entitlement>().ReverseMap();
            // Plan's constructor requires a non-nullable DateTime for subscribedOn, but the Kiota
            // side exposes SubscribedOn as DateTimeOffset? - AutoMapper's constructor-parameter
            // binding won't implicitly unwrap/convert that combination, so it's mapped explicitly.
            CreateMap<KiotaModels.Get_entitlements_response_data_plans, Plan>()
                .ForCtorParam("subscribedOn", opt => opt.MapFrom(src => src.SubscribedOn.HasValue ? src.SubscribedOn.Value.DateTime : default(DateTime)))
                .ReverseMap();
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
            // UserPropertyValue only has internal constructors (it's a hand-authored oneOf
            // wrapper), so it can't be built via reflection/constructor-mapping - convert it
            // explicitly from the Kiota composed-type wrapper's Boolean/Integer/String members.
            CreateMap<KiotaModels.Get_user_properties_response_data_properties.Get_user_properties_response_data_properties_value, UserPropertyValue>()
                .ConvertUsing(src => ConvertUserPropertyValue(src));
            CreateMap<UserPropertyValue, KiotaModels.Get_user_properties_response_data_properties.Get_user_properties_response_data_properties_value>()
                .ConvertUsing(src => ConvertToKiotaPropertyValue(src));
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

        private static UserPropertyValue ConvertUserPropertyValue(KiotaModels.Get_user_properties_response_data_properties.Get_user_properties_response_data_properties_value src)
        {
            if (src is null) return null;
            if (src.String is not null) return new UserPropertyValue(src.String);
            if (src.Boolean.HasValue) return new UserPropertyValue(src.Boolean.Value);
            if (src.Integer.HasValue) return new UserPropertyValue(src.Integer.Value);
            return null;
        }

        private static KiotaModels.Get_user_properties_response_data_properties.Get_user_properties_response_data_properties_value ConvertToKiotaPropertyValue(UserPropertyValue src)
        {
            if (src is null) return null;
            var result = new KiotaModels.Get_user_properties_response_data_properties.Get_user_properties_response_data_properties_value();
            if (src.VarString is not null) result.String = src.VarString;
            else if (src.VarBool.HasValue) result.Boolean = src.VarBool.Value;
            else if (src.VarInt.HasValue) result.Integer = src.VarInt.Value;
            return result;
        }
    }
}

