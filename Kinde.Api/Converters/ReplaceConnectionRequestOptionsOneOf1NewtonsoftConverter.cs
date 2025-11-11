using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for ReplaceConnectionRequestOptionsOneOf1 that handles the Option<> structure
    /// </summary>
    public class ReplaceConnectionRequestOptionsOneOf1NewtonsoftConverter : Newtonsoft.Json.JsonConverter<ReplaceConnectionRequestOptionsOneOf1>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override ReplaceConnectionRequestOptionsOneOf1 ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, ReplaceConnectionRequestOptionsOneOf1 existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            List<string> homeRealmDomains = default(List<string>);
            if (jsonObject["home_realm_domains"] != null)
            {
                homeRealmDomains = jsonObject["home_realm_domains"].ToObject<List<string>>(serializer);
            }
            string? samlEntityId = default(string?);
            if (jsonObject["saml_entity_id"] != null)
            {
                samlEntityId = jsonObject["saml_entity_id"].ToObject<string?>();
            }
            string? samlAcsUrl = default(string?);
            if (jsonObject["saml_acs_url"] != null)
            {
                samlAcsUrl = jsonObject["saml_acs_url"].ToObject<string?>();
            }
            string? samlIdpMetadataUrl = default(string?);
            if (jsonObject["saml_idp_metadata_url"] != null)
            {
                samlIdpMetadataUrl = jsonObject["saml_idp_metadata_url"].ToObject<string?>();
            }
            string? samlEmailKeyAttr = default(string?);
            if (jsonObject["saml_email_key_attr"] != null)
            {
                samlEmailKeyAttr = jsonObject["saml_email_key_attr"].ToObject<string?>();
            }
            string? samlFirstNameKeyAttr = default(string?);
            if (jsonObject["saml_first_name_key_attr"] != null)
            {
                samlFirstNameKeyAttr = jsonObject["saml_first_name_key_attr"].ToObject<string?>();
            }
            string? samlLastNameKeyAttr = default(string?);
            if (jsonObject["saml_last_name_key_attr"] != null)
            {
                samlLastNameKeyAttr = jsonObject["saml_last_name_key_attr"].ToObject<string?>();
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
            string? samlSigningCertificate = default(string?);
            if (jsonObject["saml_signing_certificate"] != null)
            {
                samlSigningCertificate = jsonObject["saml_signing_certificate"].ToObject<string?>();
            }
            string? samlSigningPrivateKey = default(string?);
            if (jsonObject["saml_signing_private_key"] != null)
            {
                samlSigningPrivateKey = jsonObject["saml_signing_private_key"].ToObject<string?>();
            }

            return new ReplaceConnectionRequestOptionsOneOf1(
                homeRealmDomains: homeRealmDomains != null ? new Option<List<string>?>(homeRealmDomains) : default,                 samlEntityId: samlEntityId != null ? new Option<string?>(samlEntityId) : default,                 samlAcsUrl: samlAcsUrl != null ? new Option<string?>(samlAcsUrl) : default,                 samlIdpMetadataUrl: samlIdpMetadataUrl != null ? new Option<string?>(samlIdpMetadataUrl) : default,                 samlEmailKeyAttr: samlEmailKeyAttr != null ? new Option<string?>(samlEmailKeyAttr) : default,                 samlFirstNameKeyAttr: samlFirstNameKeyAttr != null ? new Option<string?>(samlFirstNameKeyAttr) : default,                 samlLastNameKeyAttr: samlLastNameKeyAttr != null ? new Option<string?>(samlLastNameKeyAttr) : default,                 isCreateMissingUser: isCreateMissingUser != null ? new Option<bool?>(isCreateMissingUser) : default,                 isForceShowSsoButton: isForceShowSsoButton != null ? new Option<bool?>(isForceShowSsoButton) : default,                 upstreamParams: upstreamParams != null ? new Option<Dictionary<string, Object>>(upstreamParams) : default,                 samlSigningCertificate: samlSigningCertificate != null ? new Option<string?>(samlSigningCertificate) : default,                 samlSigningPrivateKey: samlSigningPrivateKey != null ? new Option<string?>(samlSigningPrivateKey) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, ReplaceConnectionRequestOptionsOneOf1 value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.HomeRealmDomainsOption.IsSet)
            {
                writer.WritePropertyName("home_realm_domains");
                serializer.Serialize(writer, value.HomeRealmDomains);
            }
            if (value.SamlEntityIdOption.IsSet && value.SamlEntityId != null)
            {
                writer.WritePropertyName("saml_entity_id");
                serializer.Serialize(writer, value.SamlEntityId);
            }
            if (value.SamlAcsUrlOption.IsSet && value.SamlAcsUrl != null)
            {
                writer.WritePropertyName("saml_acs_url");
                serializer.Serialize(writer, value.SamlAcsUrl);
            }
            if (value.SamlIdpMetadataUrlOption.IsSet && value.SamlIdpMetadataUrl != null)
            {
                writer.WritePropertyName("saml_idp_metadata_url");
                serializer.Serialize(writer, value.SamlIdpMetadataUrl);
            }
            if (value.SamlEmailKeyAttrOption.IsSet && value.SamlEmailKeyAttr != null)
            {
                writer.WritePropertyName("saml_email_key_attr");
                serializer.Serialize(writer, value.SamlEmailKeyAttr);
            }
            if (value.SamlFirstNameKeyAttrOption.IsSet && value.SamlFirstNameKeyAttr != null)
            {
                writer.WritePropertyName("saml_first_name_key_attr");
                serializer.Serialize(writer, value.SamlFirstNameKeyAttr);
            }
            if (value.SamlLastNameKeyAttrOption.IsSet && value.SamlLastNameKeyAttr != null)
            {
                writer.WritePropertyName("saml_last_name_key_attr");
                serializer.Serialize(writer, value.SamlLastNameKeyAttr);
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
            if (value.SamlSigningCertificateOption.IsSet && value.SamlSigningCertificate != null)
            {
                writer.WritePropertyName("saml_signing_certificate");
                serializer.Serialize(writer, value.SamlSigningCertificate);
            }
            if (value.SamlSigningPrivateKeyOption.IsSet && value.SamlSigningPrivateKey != null)
            {
                writer.WritePropertyName("saml_signing_private_key");
                serializer.Serialize(writer, value.SamlSigningPrivateKey);
            }

            writer.WriteEndObject();
        }
    }
}