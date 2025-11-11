using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateOrganizationRequest that handles the Option<> structure
    /// </summary>
    public class CreateOrganizationRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateOrganizationRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateOrganizationRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateOrganizationRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            Dictionary<string, CreateOrganizationRequest.InnerEnum> featureFlags = default(Dictionary<string, CreateOrganizationRequest.InnerEnum>);
            if (jsonObject["feature_flags"] != null)
            {
                featureFlags = jsonObject["feature_flags"].ToObject<Dictionary<string, CreateOrganizationRequest.InnerEnum>>(serializer);
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
            string? themeCode = default(string?);
            if (jsonObject["theme_code"] != null)
            {
                themeCode = jsonObject["theme_code"].ToObject<string?>();
            }
            string? handle = default(string?);
            if (jsonObject["handle"] != null)
            {
                handle = jsonObject["handle"].ToObject<string?>();
            }
            bool? isAllowRegistrations = default(bool?);
            if (jsonObject["is_allow_registrations"] != null)
            {
                isAllowRegistrations = jsonObject["is_allow_registrations"].ToObject<bool?>(serializer);
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
            bool? isCreateBillingCustomer = default(bool?);
            if (jsonObject["is_create_billing_customer"] != null)
            {
                isCreateBillingCustomer = jsonObject["is_create_billing_customer"].ToObject<bool?>(serializer);
            }
            string? billingEmail = default(string?);
            if (jsonObject["billing_email"] != null)
            {
                billingEmail = jsonObject["billing_email"].ToObject<string?>();
            }
            string? billingPlanCode = default(string?);
            if (jsonObject["billing_plan_code"] != null)
            {
                billingPlanCode = jsonObject["billing_plan_code"].ToObject<string?>();
            }
            string name = default(string);
            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string>();
            }

            return new CreateOrganizationRequest(
                featureFlags: featureFlags != null ? new Option<Dictionary<string, CreateOrganizationRequest.InnerEnum>>(featureFlags) : default,                 externalId: externalId != null ? new Option<string?>(externalId) : default,                 backgroundColor: backgroundColor != null ? new Option<string?>(backgroundColor) : default,                 buttonColor: buttonColor != null ? new Option<string?>(buttonColor) : default,                 buttonTextColor: buttonTextColor != null ? new Option<string?>(buttonTextColor) : default,                 linkColor: linkColor != null ? new Option<string?>(linkColor) : default,                 backgroundColorDark: backgroundColorDark != null ? new Option<string?>(backgroundColorDark) : default,                 buttonColorDark: buttonColorDark != null ? new Option<string?>(buttonColorDark) : default,                 buttonTextColorDark: buttonTextColorDark != null ? new Option<string?>(buttonTextColorDark) : default,                 linkColorDark: linkColorDark != null ? new Option<string?>(linkColorDark) : default,                 themeCode: themeCode != null ? new Option<string?>(themeCode) : default,                 handle: handle != null ? new Option<string?>(handle) : default,                 isAllowRegistrations: isAllowRegistrations != null ? new Option<bool?>(isAllowRegistrations) : default,                 senderName: senderName != null ? new Option<string?>(senderName) : default,                 senderEmail: senderEmail != null ? new Option<string?>(senderEmail) : default,                 isCreateBillingCustomer: isCreateBillingCustomer != null ? new Option<bool?>(isCreateBillingCustomer) : default,                 billingEmail: billingEmail != null ? new Option<string?>(billingEmail) : default,                 billingPlanCode: billingPlanCode != null ? new Option<string?>(billingPlanCode) : default,                 name: name            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateOrganizationRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.FeatureFlagsOption.IsSet)
            {
                writer.WritePropertyName("feature_flags");
                serializer.Serialize(writer, value.FeatureFlags);
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
            if (value.ThemeCodeOption.IsSet && value.ThemeCode != null)
            {
                writer.WritePropertyName("theme_code");
                serializer.Serialize(writer, value.ThemeCode);
            }
            if (value.HandleOption.IsSet && value.Handle != null)
            {
                writer.WritePropertyName("handle");
                serializer.Serialize(writer, value.Handle);
            }
            if (value.IsAllowRegistrationsOption.IsSet && value.IsAllowRegistrations != null)
            {
                writer.WritePropertyName("is_allow_registrations");
                serializer.Serialize(writer, value.IsAllowRegistrations);
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
            if (value.IsCreateBillingCustomerOption.IsSet && value.IsCreateBillingCustomer != null)
            {
                writer.WritePropertyName("is_create_billing_customer");
                serializer.Serialize(writer, value.IsCreateBillingCustomer);
            }
            if (value.BillingEmailOption.IsSet && value.BillingEmail != null)
            {
                writer.WritePropertyName("billing_email");
                serializer.Serialize(writer, value.BillingEmail);
            }
            if (value.BillingPlanCodeOption.IsSet && value.BillingPlanCode != null)
            {
                writer.WritePropertyName("billing_plan_code");
                serializer.Serialize(writer, value.BillingPlanCode);
            }

            writer.WriteEndObject();
        }
    }
}