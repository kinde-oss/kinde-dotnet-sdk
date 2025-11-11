using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UpdateOrganizationRequest that handles the Option<> structure
    /// </summary>
    public class UpdateOrganizationRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UpdateOrganizationRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UpdateOrganizationRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UpdateOrganizationRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            UpdateOrganizationRequest.ThemeCodeEnum? themeCode = default(UpdateOrganizationRequest.ThemeCodeEnum?);
            if (jsonObject["theme_code"] != null)
            {
                var themeCodeStr = jsonObject["theme_code"].ToObject<string>();
                if (!string.IsNullOrEmpty(themeCodeStr))
                {
                    themeCode = UpdateOrganizationRequest.ThemeCodeEnumFromString(themeCodeStr);
                }
            }
            string? name = default(string?);
            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string?>();
            }
            string? externalId = default(string?);
            if (jsonObject["external_id"] != null)
            {
                externalId = jsonObject["external_id"].ToObject<string?>();
            }
            string? backgroundColor = default(string?);
            if (jsonObject["background_color"] != null)
            {
                backgroundColor = jsonObject["background_color"].ToObject<string?>();
            }
            string? buttonColor = default(string?);
            if (jsonObject["button_color"] != null)
            {
                buttonColor = jsonObject["button_color"].ToObject<string?>();
            }
            string? buttonTextColor = default(string?);
            if (jsonObject["button_text_color"] != null)
            {
                buttonTextColor = jsonObject["button_text_color"].ToObject<string?>();
            }
            string? linkColor = default(string?);
            if (jsonObject["link_color"] != null)
            {
                linkColor = jsonObject["link_color"].ToObject<string?>();
            }
            string? backgroundColorDark = default(string?);
            if (jsonObject["background_color_dark"] != null)
            {
                backgroundColorDark = jsonObject["background_color_dark"].ToObject<string?>();
            }
            string? buttonColorDark = default(string?);
            if (jsonObject["button_color_dark"] != null)
            {
                buttonColorDark = jsonObject["button_color_dark"].ToObject<string?>();
            }
            string? buttonTextColorDark = default(string?);
            if (jsonObject["button_text_color_dark"] != null)
            {
                buttonTextColorDark = jsonObject["button_text_color_dark"].ToObject<string?>();
            }
            string? linkColorDark = default(string?);
            if (jsonObject["link_color_dark"] != null)
            {
                linkColorDark = jsonObject["link_color_dark"].ToObject<string?>();
            }
            string? handle = default(string?);
            if (jsonObject["handle"] != null)
            {
                handle = jsonObject["handle"].ToObject<string?>();
            }
            bool? isAutoJoinDomainList = default(bool?);
            if (jsonObject["is_auto_join_domain_list"] != null)
            {
                isAutoJoinDomainList = jsonObject["is_auto_join_domain_list"].ToObject<bool?>(serializer);
            }
            List<string> allowedDomains = default(List<string>);
            if (jsonObject["allowed_domains"] != null)
            {
                allowedDomains = jsonObject["allowed_domains"].ToObject<List<string>>(serializer);
            }
            bool? isEnableAdvancedOrgs = default(bool?);
            if (jsonObject["is_enable_advanced_orgs"] != null)
            {
                isEnableAdvancedOrgs = jsonObject["is_enable_advanced_orgs"].ToObject<bool?>(serializer);
            }
            bool? isEnforceMfa = default(bool?);
            if (jsonObject["is_enforce_mfa"] != null)
            {
                isEnforceMfa = jsonObject["is_enforce_mfa"].ToObject<bool?>(serializer);
            }
            string? senderName = default(string?);
            if (jsonObject["sender_name"] != null)
            {
                senderName = jsonObject["sender_name"].ToObject<string?>();
            }
            string? senderEmail = default(string?);
            if (jsonObject["sender_email"] != null)
            {
                senderEmail = jsonObject["sender_email"].ToObject<string?>();
            }

            return new UpdateOrganizationRequest(
                themeCode: themeCode != null ? new Option<UpdateOrganizationRequest.ThemeCodeEnum?>(themeCode) : default,                 name: name != null ? new Option<string?>(name) : default,                 externalId: externalId != null ? new Option<string?>(externalId) : default,                 backgroundColor: backgroundColor != null ? new Option<string?>(backgroundColor) : default,                 buttonColor: buttonColor != null ? new Option<string?>(buttonColor) : default,                 buttonTextColor: buttonTextColor != null ? new Option<string?>(buttonTextColor) : default,                 linkColor: linkColor != null ? new Option<string?>(linkColor) : default,                 backgroundColorDark: backgroundColorDark != null ? new Option<string?>(backgroundColorDark) : default,                 buttonColorDark: buttonColorDark != null ? new Option<string?>(buttonColorDark) : default,                 buttonTextColorDark: buttonTextColorDark != null ? new Option<string?>(buttonTextColorDark) : default,                 linkColorDark: linkColorDark != null ? new Option<string?>(linkColorDark) : default,                 handle: handle != null ? new Option<string?>(handle) : default,                 isAutoJoinDomainList: isAutoJoinDomainList != null ? new Option<bool?>(isAutoJoinDomainList) : default,                 allowedDomains: allowedDomains != null ? new Option<List<string>?>(allowedDomains) : default,                 isEnableAdvancedOrgs: isEnableAdvancedOrgs != null ? new Option<bool?>(isEnableAdvancedOrgs) : default,                 isEnforceMfa: isEnforceMfa != null ? new Option<bool?>(isEnforceMfa) : default,                 senderName: senderName != null ? new Option<string?>(senderName) : default,                 senderEmail: senderEmail != null ? new Option<string?>(senderEmail) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UpdateOrganizationRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.ThemeCodeOption.IsSet && value.ThemeCode != null)
            {
                writer.WritePropertyName("theme_code");
                var themeCodeStr = UpdateOrganizationRequest.ThemeCodeEnumToJsonValue(value.ThemeCode.Value);
                writer.WriteValue(themeCodeStr);
            }
            if (value.NameOption.IsSet && value.Name != null)
            {
                writer.WritePropertyName("name");
                serializer.Serialize(writer, value.Name);
            }
            if (value.ExternalIdOption.IsSet && value.ExternalId != null)
            {
                writer.WritePropertyName("external_id");
                serializer.Serialize(writer, value.ExternalId);
            }
            if (value.BackgroundColorOption.IsSet && value.BackgroundColor != null)
            {
                writer.WritePropertyName("background_color");
                serializer.Serialize(writer, value.BackgroundColor);
            }
            if (value.ButtonColorOption.IsSet && value.ButtonColor != null)
            {
                writer.WritePropertyName("button_color");
                serializer.Serialize(writer, value.ButtonColor);
            }
            if (value.ButtonTextColorOption.IsSet && value.ButtonTextColor != null)
            {
                writer.WritePropertyName("button_text_color");
                serializer.Serialize(writer, value.ButtonTextColor);
            }
            if (value.LinkColorOption.IsSet && value.LinkColor != null)
            {
                writer.WritePropertyName("link_color");
                serializer.Serialize(writer, value.LinkColor);
            }
            if (value.BackgroundColorDarkOption.IsSet && value.BackgroundColorDark != null)
            {
                writer.WritePropertyName("background_color_dark");
                serializer.Serialize(writer, value.BackgroundColorDark);
            }
            if (value.ButtonColorDarkOption.IsSet && value.ButtonColorDark != null)
            {
                writer.WritePropertyName("button_color_dark");
                serializer.Serialize(writer, value.ButtonColorDark);
            }
            if (value.ButtonTextColorDarkOption.IsSet && value.ButtonTextColorDark != null)
            {
                writer.WritePropertyName("button_text_color_dark");
                serializer.Serialize(writer, value.ButtonTextColorDark);
            }
            if (value.LinkColorDarkOption.IsSet && value.LinkColorDark != null)
            {
                writer.WritePropertyName("link_color_dark");
                serializer.Serialize(writer, value.LinkColorDark);
            }
            if (value.HandleOption.IsSet && value.Handle != null)
            {
                writer.WritePropertyName("handle");
                serializer.Serialize(writer, value.Handle);
            }
            if (value.IsAutoJoinDomainListOption.IsSet && value.IsAutoJoinDomainList != null)
            {
                writer.WritePropertyName("is_auto_join_domain_list");
                serializer.Serialize(writer, value.IsAutoJoinDomainList);
            }
            if (value.AllowedDomainsOption.IsSet)
            {
                writer.WritePropertyName("allowed_domains");
                serializer.Serialize(writer, value.AllowedDomains);
            }
            if (value.IsEnableAdvancedOrgsOption.IsSet && value.IsEnableAdvancedOrgs != null)
            {
                writer.WritePropertyName("is_enable_advanced_orgs");
                serializer.Serialize(writer, value.IsEnableAdvancedOrgs);
            }
            if (value.IsEnforceMfaOption.IsSet && value.IsEnforceMfa != null)
            {
                writer.WritePropertyName("is_enforce_mfa");
                serializer.Serialize(writer, value.IsEnforceMfa);
            }
            if (value.SenderNameOption.IsSet && value.SenderName != null)
            {
                writer.WritePropertyName("sender_name");
                serializer.Serialize(writer, value.SenderName);
            }
            if (value.SenderEmailOption.IsSet && value.SenderEmail != null)
            {
                writer.WritePropertyName("sender_email");
                serializer.Serialize(writer, value.SenderEmail);
            }

            writer.WriteEndObject();
        }
    }
}