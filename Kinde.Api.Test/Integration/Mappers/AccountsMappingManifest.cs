// Generated from the AutoMapper profiles this SDK no longer uses, so the test suite can
// assert that every mapping the profiles declared is still carried by the mapper that
// replaced them. Do not hand-edit: regenerate if the mapping surface changes.
using System;
using System.Collections.Generic;
using Kinde.Accounts.Model.Entities;
using Kinde.Accounts.Model.Responses;

namespace Kinde.Api.Test.Integration.Mappers
{
    internal static class AccountsMappingManifest
    {
        internal static IEnumerable<(Type Source, Type Destination)> Pairs => new[]
        {
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Error), typeof(Error)),
            (typeof(Error), typeof(global::Kinde.Api.Kiota.Accounts.Models.Error)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Error_response), typeof(ErrorResponse)),
            (typeof(ErrorResponse), typeof(global::Kinde.Api.Kiota.Accounts.Models.Error_response)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Token_error_response), typeof(TokenErrorResponse)),
            (typeof(TokenErrorResponse), typeof(global::Kinde.Api.Kiota.Accounts.Models.Token_error_response)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_entitlement_response), typeof(GetEntitlementResponse)),
            (typeof(GetEntitlementResponse), typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_entitlement_response)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_entitlement_response_data), typeof(GetEntitlementResponseData)),
            (typeof(GetEntitlementResponseData), typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_entitlement_response_data)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_entitlement_response_data_entitlement), typeof(EntitlementDetail)),
            (typeof(EntitlementDetail), typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_entitlement_response_data_entitlement)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_entitlements_response), typeof(GetEntitlementsResponse)),
            (typeof(GetEntitlementsResponse), typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_entitlements_response)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_entitlements_response_data), typeof(GetEntitlementsResponseData)),
            (typeof(GetEntitlementsResponseData), typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_entitlements_response_data)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_entitlements_response_data_entitlements), typeof(Entitlement)),
            (typeof(Entitlement), typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_entitlements_response_data_entitlements)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_entitlements_response_data_plans), typeof(Plan)),
            (typeof(Plan), typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_entitlements_response_data_plans)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_entitlements_response_metadata), typeof(GetEntitlementsResponseMetadata)),
            (typeof(GetEntitlementsResponseMetadata), typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_entitlements_response_metadata)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_feature_flags_response), typeof(GetFeatureFlagsResponse)),
            (typeof(GetFeatureFlagsResponse), typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_feature_flags_response)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_feature_flags_response_data), typeof(GetFeatureFlagsResponseData)),
            (typeof(GetFeatureFlagsResponseData), typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_feature_flags_response_data)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_feature_flags_response_data_feature_flags), typeof(FeatureFlag)),
            (typeof(FeatureFlag), typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_feature_flags_response_data_feature_flags)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_user_permissions_response), typeof(GetUserPermissionsResponse)),
            (typeof(GetUserPermissionsResponse), typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_user_permissions_response)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_user_permissions_response_data), typeof(GetUserPermissionsResponseData)),
            (typeof(GetUserPermissionsResponseData), typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_user_permissions_response_data)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_user_permissions_response_data_permissions), typeof(Permission)),
            (typeof(Permission), typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_user_permissions_response_data_permissions)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_user_permissions_response_metadata), typeof(GetUserPermissionsResponseMetadata)),
            (typeof(GetUserPermissionsResponseMetadata), typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_user_permissions_response_metadata)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_user_properties_response), typeof(GetUserPropertiesResponse)),
            (typeof(GetUserPropertiesResponse), typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_user_properties_response)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_user_properties_response_data), typeof(GetUserPropertiesResponseData)),
            (typeof(GetUserPropertiesResponseData), typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_user_properties_response_data)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_user_properties_response_data_properties.Get_user_properties_response_data_properties_value), typeof(UserPropertyValue)),
            (typeof(UserPropertyValue), typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_user_properties_response_data_properties.Get_user_properties_response_data_properties_value)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_user_properties_response_data_properties), typeof(UserProperty)),
            (typeof(UserProperty), typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_user_properties_response_data_properties)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_user_properties_response_metadata), typeof(GetUserPropertiesResponseMetadata)),
            (typeof(GetUserPropertiesResponseMetadata), typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_user_properties_response_metadata)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_user_roles_response), typeof(GetUserRolesResponse)),
            (typeof(GetUserRolesResponse), typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_user_roles_response)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_user_roles_response_data), typeof(GetUserRolesResponseData)),
            (typeof(GetUserRolesResponseData), typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_user_roles_response_data)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_user_roles_response_data_roles), typeof(Role)),
            (typeof(Role), typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_user_roles_response_data_roles)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_user_roles_response_metadata), typeof(GetUserRolesResponseMetadata)),
            (typeof(GetUserRolesResponseMetadata), typeof(global::Kinde.Api.Kiota.Accounts.Models.Get_user_roles_response_metadata)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Portal_link), typeof(PortalLink)),
            (typeof(PortalLink), typeof(global::Kinde.Api.Kiota.Accounts.Models.Portal_link)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.Token_introspect), typeof(TokenIntrospect)),
            (typeof(TokenIntrospect), typeof(global::Kinde.Api.Kiota.Accounts.Models.Token_introspect)),
            (typeof(global::Kinde.Api.Kiota.Accounts.Models.User_profile_v2), typeof(UserProfileV2)),
            (typeof(UserProfileV2), typeof(global::Kinde.Api.Kiota.Accounts.Models.User_profile_v2)),
        };
    }
}
