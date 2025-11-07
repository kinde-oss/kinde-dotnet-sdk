using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UpdateOrganizationSessionsRequest that handles the Option<> structure
    /// </summary>
    public class UpdateOrganizationSessionsRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UpdateOrganizationSessionsRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UpdateOrganizationSessionsRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UpdateOrganizationSessionsRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            UpdateOrganizationSessionsRequest.SsoSessionPersistenceModeEnum? ssoSessionPersistenceMode = null;
            bool? isUseOrgSsoSessionPolicy = null;
            bool? isUseOrgAuthenticatedSessionLifetime = null;
            int? authenticatedSessionLifetime = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["sso_session_persistence_mode"] != null)
            {
                var ssoSessionPersistenceModeStr = jsonObject["sso_session_persistence_mode"].ToObject<string>();
                if (!string.IsNullOrEmpty(ssoSessionPersistenceModeStr))
                {
                    ssoSessionPersistenceMode = UpdateOrganizationSessionsRequest.SsoSessionPersistenceModeEnumFromString(ssoSessionPersistenceModeStr);
                }
            }

            if (jsonObject["is_use_org_sso_session_policy"] != null)
            {
                isUseOrgSsoSessionPolicy = jsonObject["is_use_org_sso_session_policy"].ToObject<bool?>();
            }

            if (jsonObject["is_use_org_authenticated_session_lifetime"] != null)
            {
                isUseOrgAuthenticatedSessionLifetime = jsonObject["is_use_org_authenticated_session_lifetime"].ToObject<bool?>();
            }

            if (jsonObject["authenticated_session_lifetime"] != null)
            {
                authenticatedSessionLifetime = jsonObject["authenticated_session_lifetime"].ToObject<int?>();
            }

            return new UpdateOrganizationSessionsRequest(
                ssoSessionPersistenceMode: ssoSessionPersistenceMode != null ? new Option<UpdateOrganizationSessionsRequest.SsoSessionPersistenceModeEnum?>(ssoSessionPersistenceMode) : default, isUseOrgSsoSessionPolicy: isUseOrgSsoSessionPolicy != null ? new Option<bool?>(isUseOrgSsoSessionPolicy) : default, isUseOrgAuthenticatedSessionLifetime: isUseOrgAuthenticatedSessionLifetime != null ? new Option<bool?>(isUseOrgAuthenticatedSessionLifetime) : default, authenticatedSessionLifetime: authenticatedSessionLifetime != null ? new Option<int?>(authenticatedSessionLifetime) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UpdateOrganizationSessionsRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.SsoSessionPersistenceModeOption.IsSet && value.SsoSessionPersistenceMode != null)
            {
                writer.WritePropertyName("sso_session_persistence_mode");
                var ssosessionpersistencemodeStr = UpdateOrganizationSessionsRequest.SsoSessionPersistenceModeEnumToJsonValue(value.SsoSessionPersistenceMode.Value);
                writer.WriteValue(ssosessionpersistencemodeStr);
            }

            if (value.IsUseOrgSsoSessionPolicyOption.IsSet && value.IsUseOrgSsoSessionPolicy != null)
            {
                writer.WritePropertyName("is_use_org_sso_session_policy");
                writer.WriteValue(value.IsUseOrgSsoSessionPolicy.Value);
            }

            if (value.IsUseOrgAuthenticatedSessionLifetimeOption.IsSet && value.IsUseOrgAuthenticatedSessionLifetime != null)
            {
                writer.WritePropertyName("is_use_org_authenticated_session_lifetime");
                writer.WriteValue(value.IsUseOrgAuthenticatedSessionLifetime.Value);
            }

            if (value.AuthenticatedSessionLifetimeOption.IsSet && value.AuthenticatedSessionLifetime != null)
            {
                writer.WritePropertyName("authenticated_session_lifetime");
                writer.WriteValue(value.AuthenticatedSessionLifetime.Value);
            }

            writer.WriteEndObject();
        }
    }
}
