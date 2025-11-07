using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UpdateBusinessRequest that handles the Option<> structure
    /// </summary>
    public class UpdateBusinessRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UpdateBusinessRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UpdateBusinessRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UpdateBusinessRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? businessName = null;
            string? email = null;
            string? industryKey = null;
            bool? isClickWrap = null;
            bool? isShowKindeBranding = null;
            string? kindePerkCode = null;
            string? phone = null;
            string? privacyUrl = null;
            string? termsUrl = null;
            string? timezoneKey = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["business_name"] != null)
            {
                businessName = jsonObject["business_name"].ToObject<string>();
            }

            if (jsonObject["email"] != null)
            {
                email = jsonObject["email"].ToObject<string>();
            }

            if (jsonObject["industry_key"] != null)
            {
                industryKey = jsonObject["industry_key"].ToObject<string>();
            }

            if (jsonObject["is_click_wrap"] != null)
            {
                isClickWrap = jsonObject["is_click_wrap"].ToObject<bool?>();
            }

            if (jsonObject["is_show_kinde_branding"] != null)
            {
                isShowKindeBranding = jsonObject["is_show_kinde_branding"].ToObject<bool?>();
            }

            if (jsonObject["kinde_perk_code"] != null)
            {
                kindePerkCode = jsonObject["kinde_perk_code"].ToObject<string>();
            }

            if (jsonObject["phone"] != null)
            {
                phone = jsonObject["phone"].ToObject<string>();
            }

            if (jsonObject["privacy_url"] != null)
            {
                privacyUrl = jsonObject["privacy_url"].ToObject<string>();
            }

            if (jsonObject["terms_url"] != null)
            {
                termsUrl = jsonObject["terms_url"].ToObject<string>();
            }

            if (jsonObject["timezone_key"] != null)
            {
                timezoneKey = jsonObject["timezone_key"].ToObject<string>();
            }

            return new UpdateBusinessRequest(
                businessName: businessName != null ? new Option<string?>(businessName) : default, email: email != null ? new Option<string?>(email) : default, industryKey: industryKey != null ? new Option<string?>(industryKey) : default, isClickWrap: isClickWrap != null ? new Option<bool?>(isClickWrap) : default, isShowKindeBranding: isShowKindeBranding != null ? new Option<bool?>(isShowKindeBranding) : default, kindePerkCode: kindePerkCode != null ? new Option<string?>(kindePerkCode) : default, phone: phone != null ? new Option<string?>(phone) : default, privacyUrl: privacyUrl != null ? new Option<string?>(privacyUrl) : default, termsUrl: termsUrl != null ? new Option<string?>(termsUrl) : default, timezoneKey: timezoneKey != null ? new Option<string?>(timezoneKey) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UpdateBusinessRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.BusinessNameOption.IsSet && value.BusinessName != null)
            {
                writer.WritePropertyName("business_name");
                serializer.Serialize(writer, value.BusinessName);
            }

            if (value.EmailOption.IsSet && value.Email != null)
            {
                writer.WritePropertyName("email");
                serializer.Serialize(writer, value.Email);
            }

            if (value.IndustryKeyOption.IsSet && value.IndustryKey != null)
            {
                writer.WritePropertyName("industry_key");
                serializer.Serialize(writer, value.IndustryKey);
            }

            if (value.IsClickWrapOption.IsSet && value.IsClickWrap != null)
            {
                writer.WritePropertyName("is_click_wrap");
                writer.WriteValue(value.IsClickWrap.Value);
            }

            if (value.IsShowKindeBrandingOption.IsSet && value.IsShowKindeBranding != null)
            {
                writer.WritePropertyName("is_show_kinde_branding");
                writer.WriteValue(value.IsShowKindeBranding.Value);
            }

            if (value.KindePerkCodeOption.IsSet && value.KindePerkCode != null)
            {
                writer.WritePropertyName("kinde_perk_code");
                serializer.Serialize(writer, value.KindePerkCode);
            }

            if (value.PhoneOption.IsSet && value.Phone != null)
            {
                writer.WritePropertyName("phone");
                serializer.Serialize(writer, value.Phone);
            }

            if (value.PrivacyUrlOption.IsSet && value.PrivacyUrl != null)
            {
                writer.WritePropertyName("privacy_url");
                serializer.Serialize(writer, value.PrivacyUrl);
            }

            if (value.TermsUrlOption.IsSet && value.TermsUrl != null)
            {
                writer.WritePropertyName("terms_url");
                serializer.Serialize(writer, value.TermsUrl);
            }

            if (value.TimezoneKeyOption.IsSet && value.TimezoneKey != null)
            {
                writer.WritePropertyName("timezone_key");
                serializer.Serialize(writer, value.TimezoneKey);
            }

            writer.WriteEndObject();
        }
    }
}
