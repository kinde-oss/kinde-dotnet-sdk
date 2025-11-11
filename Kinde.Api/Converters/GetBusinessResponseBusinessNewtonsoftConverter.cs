using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetBusinessResponseBusiness that handles the Option<> structure
    /// </summary>
    public class GetBusinessResponseBusinessNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetBusinessResponseBusiness>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetBusinessResponseBusiness ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetBusinessResponseBusiness existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? code = default(string?);
            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string?>();
            }
            string? name = default(string?);
            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string?>();
            }
            string? phone = default(string?);
            if (jsonObject["phone"] != null)
            {
                phone = jsonObject["phone"].ToObject<string?>();
            }
            string? email = default(string?);
            if (jsonObject["email"] != null)
            {
                email = jsonObject["email"].ToObject<string?>();
            }
            string? industry = default(string?);
            if (jsonObject["industry"] != null)
            {
                industry = jsonObject["industry"].ToObject<string?>();
            }
            string? timezone = default(string?);
            if (jsonObject["timezone"] != null)
            {
                timezone = jsonObject["timezone"].ToObject<string?>();
            }
            string? privacyUrl = default(string?);
            if (jsonObject["privacy_url"] != null)
            {
                privacyUrl = jsonObject["privacy_url"].ToObject<string?>();
            }
            string? termsUrl = default(string?);
            if (jsonObject["terms_url"] != null)
            {
                termsUrl = jsonObject["terms_url"].ToObject<string?>();
            }
            bool? hasClickwrap = default(bool?);
            if (jsonObject["has_clickwrap"] != null)
            {
                hasClickwrap = jsonObject["has_clickwrap"].ToObject<bool?>(serializer);
            }
            bool? hasKindeBranding = default(bool?);
            if (jsonObject["has_kinde_branding"] != null)
            {
                hasKindeBranding = jsonObject["has_kinde_branding"].ToObject<bool?>(serializer);
            }
            string? createdOn = default(string?);
            if (jsonObject["created_on"] != null)
            {
                createdOn = jsonObject["created_on"].ToObject<string?>();
            }

            return new GetBusinessResponseBusiness(
                code: code != null ? new Option<string?>(code) : default,                 name: name != null ? new Option<string?>(name) : default,                 phone: phone != null ? new Option<string?>(phone) : default,                 email: email != null ? new Option<string?>(email) : default,                 industry: industry != null ? new Option<string?>(industry) : default,                 timezone: timezone != null ? new Option<string?>(timezone) : default,                 privacyUrl: privacyUrl != null ? new Option<string?>(privacyUrl) : default,                 termsUrl: termsUrl != null ? new Option<string?>(termsUrl) : default,                 hasClickwrap: hasClickwrap != null ? new Option<bool?>(hasClickwrap) : default,                 hasKindeBranding: hasKindeBranding != null ? new Option<bool?>(hasKindeBranding) : default,                 createdOn: createdOn != null ? new Option<string?>(createdOn) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetBusinessResponseBusiness value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.CodeOption.IsSet && value.Code != null)
            {
                writer.WritePropertyName("code");
                serializer.Serialize(writer, value.Code);
            }
            if (value.NameOption.IsSet && value.Name != null)
            {
                writer.WritePropertyName("name");
                serializer.Serialize(writer, value.Name);
            }
            if (value.PhoneOption.IsSet && value.Phone != null)
            {
                writer.WritePropertyName("phone");
                serializer.Serialize(writer, value.Phone);
            }
            if (value.EmailOption.IsSet && value.Email != null)
            {
                writer.WritePropertyName("email");
                serializer.Serialize(writer, value.Email);
            }
            if (value.IndustryOption.IsSet && value.Industry != null)
            {
                writer.WritePropertyName("industry");
                serializer.Serialize(writer, value.Industry);
            }
            if (value.TimezoneOption.IsSet && value.Timezone != null)
            {
                writer.WritePropertyName("timezone");
                serializer.Serialize(writer, value.Timezone);
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
            if (value.HasClickwrapOption.IsSet && value.HasClickwrap != null)
            {
                writer.WritePropertyName("has_clickwrap");
                serializer.Serialize(writer, value.HasClickwrap);
            }
            if (value.HasKindeBrandingOption.IsSet && value.HasKindeBranding != null)
            {
                writer.WritePropertyName("has_kinde_branding");
                serializer.Serialize(writer, value.HasKindeBranding);
            }
            if (value.CreatedOnOption.IsSet && value.CreatedOn != null)
            {
                writer.WritePropertyName("created_on");
                serializer.Serialize(writer, value.CreatedOn);
            }

            writer.WriteEndObject();
        }
    }
}