using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UpdateConnectionRequestOptionsOneOf that handles the Option<> structure
    /// </summary>
    public class UpdateConnectionRequestOptionsOneOfNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UpdateConnectionRequestOptionsOneOf>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UpdateConnectionRequestOptionsOneOf ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UpdateConnectionRequestOptionsOneOf existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? clientId = default(string?);
            if (jsonObject["client_id"] != null)
            {
                clientId = jsonObject["client_id"].ToObject<string?>();
            }
            string? clientSecret = default(string?);
            if (jsonObject["client_secret"] != null)
            {
                clientSecret = jsonObject["client_secret"].ToObject<string?>();
            }
            List<string> homeRealmDomains = default(List<string>);
            if (jsonObject["home_realm_domains"] != null)
            {
                homeRealmDomains = jsonObject["home_realm_domains"].ToObject<List<string>>(serializer);
            }
            string? entraIdDomain = default(string?);
            if (jsonObject["entra_id_domain"] != null)
            {
                entraIdDomain = jsonObject["entra_id_domain"].ToObject<string?>();
            }
            bool? isUseCommonEndpoint = default(bool?);
            if (jsonObject["is_use_common_endpoint"] != null)
            {
                isUseCommonEndpoint = jsonObject["is_use_common_endpoint"].ToObject<bool?>(serializer);
            }
            bool? isSyncUserProfileOnLogin = default(bool?);
            if (jsonObject["is_sync_user_profile_on_login"] != null)
            {
                isSyncUserProfileOnLogin = jsonObject["is_sync_user_profile_on_login"].ToObject<bool?>(serializer);
            }
            bool? isRetrieveProviderUserGroups = default(bool?);
            if (jsonObject["is_retrieve_provider_user_groups"] != null)
            {
                isRetrieveProviderUserGroups = jsonObject["is_retrieve_provider_user_groups"].ToObject<bool?>(serializer);
            }
            bool? isExtendedAttributesRequired = default(bool?);
            if (jsonObject["is_extended_attributes_required"] != null)
            {
                isExtendedAttributesRequired = jsonObject["is_extended_attributes_required"].ToObject<bool?>(serializer);
            }
            bool? isCreateMissingUser = default(bool?);
            if (jsonObject["is_create_missing_user"] != null)
            {
                isCreateMissingUser = jsonObject["is_create_missing_user"].ToObject<bool?>(serializer);
            }
            bool? isForceShowSsoButton = default(bool?);
            if (jsonObject["is_force_show_sso_button"] != null)
            {
                isForceShowSsoButton = jsonObject["is_force_show_sso_button"].ToObject<bool?>(serializer);
            }
            Dictionary<string, Object> upstreamParams = default(Dictionary<string, Object>);
            if (jsonObject["upstream_params"] != null)
            {
                upstreamParams = jsonObject["upstream_params"].ToObject<Dictionary<string, Object>>(serializer);
            }

            return new UpdateConnectionRequestOptionsOneOf(
                clientId: clientId != null ? new Option<string?>(clientId) : default,                 clientSecret: clientSecret != null ? new Option<string?>(clientSecret) : default,                 homeRealmDomains: homeRealmDomains != null ? new Option<List<string>?>(homeRealmDomains) : default,                 entraIdDomain: entraIdDomain != null ? new Option<string?>(entraIdDomain) : default,                 isUseCommonEndpoint: isUseCommonEndpoint != null ? new Option<bool?>(isUseCommonEndpoint) : default,                 isSyncUserProfileOnLogin: isSyncUserProfileOnLogin != null ? new Option<bool?>(isSyncUserProfileOnLogin) : default,                 isRetrieveProviderUserGroups: isRetrieveProviderUserGroups != null ? new Option<bool?>(isRetrieveProviderUserGroups) : default,                 isExtendedAttributesRequired: isExtendedAttributesRequired != null ? new Option<bool?>(isExtendedAttributesRequired) : default,                 isCreateMissingUser: isCreateMissingUser != null ? new Option<bool?>(isCreateMissingUser) : default,                 isForceShowSsoButton: isForceShowSsoButton != null ? new Option<bool?>(isForceShowSsoButton) : default,                 upstreamParams: upstreamParams != null ? new Option<Dictionary<string, Object>>(upstreamParams) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UpdateConnectionRequestOptionsOneOf value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.ClientIdOption.IsSet && value.ClientId != null)
            {
                writer.WritePropertyName("client_id");
                serializer.Serialize(writer, value.ClientId);
            }
            if (value.ClientSecretOption.IsSet && value.ClientSecret != null)
            {
                writer.WritePropertyName("client_secret");
                serializer.Serialize(writer, value.ClientSecret);
            }
            if (value.HomeRealmDomainsOption.IsSet)
            {
                writer.WritePropertyName("home_realm_domains");
                serializer.Serialize(writer, value.HomeRealmDomains);
            }
            if (value.EntraIdDomainOption.IsSet && value.EntraIdDomain != null)
            {
                writer.WritePropertyName("entra_id_domain");
                serializer.Serialize(writer, value.EntraIdDomain);
            }
            if (value.IsUseCommonEndpointOption.IsSet && value.IsUseCommonEndpoint != null)
            {
                writer.WritePropertyName("is_use_common_endpoint");
                serializer.Serialize(writer, value.IsUseCommonEndpoint);
            }
            if (value.IsSyncUserProfileOnLoginOption.IsSet && value.IsSyncUserProfileOnLogin != null)
            {
                writer.WritePropertyName("is_sync_user_profile_on_login");
                serializer.Serialize(writer, value.IsSyncUserProfileOnLogin);
            }
            if (value.IsRetrieveProviderUserGroupsOption.IsSet && value.IsRetrieveProviderUserGroups != null)
            {
                writer.WritePropertyName("is_retrieve_provider_user_groups");
                serializer.Serialize(writer, value.IsRetrieveProviderUserGroups);
            }
            if (value.IsExtendedAttributesRequiredOption.IsSet && value.IsExtendedAttributesRequired != null)
            {
                writer.WritePropertyName("is_extended_attributes_required");
                serializer.Serialize(writer, value.IsExtendedAttributesRequired);
            }
            if (value.IsCreateMissingUserOption.IsSet && value.IsCreateMissingUser != null)
            {
                writer.WritePropertyName("is_create_missing_user");
                serializer.Serialize(writer, value.IsCreateMissingUser);
            }
            if (value.IsForceShowSsoButtonOption.IsSet && value.IsForceShowSsoButton != null)
            {
                writer.WritePropertyName("is_force_show_sso_button");
                serializer.Serialize(writer, value.IsForceShowSsoButton);
            }
            if (value.UpstreamParamsOption.IsSet)
            {
                writer.WritePropertyName("upstream_params");
                serializer.Serialize(writer, value.UpstreamParams);
            }

            writer.WriteEndObject();
        }
    }
}