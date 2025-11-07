using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetOrganizationResponse that handles the Option<> structure
    /// </summary>
    public class GetOrganizationResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetOrganizationResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetOrganizationResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetOrganizationResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            GetOrganizationResponse.ThemeCodeEnum? themeCode = null;
            GetOrganizationResponse.ColorSchemeEnum? colorScheme = null;
            string? code = null;
            string? name = null;
            string? handle = null;
            bool? isDefault = null;
            string? externalId = null;
            bool? isAutoMembershipEnabled = null;
            string? logo = null;
            string? logoDark = null;
            string? faviconSvg = null;
            string? faviconFallback = null;
            GetEnvironmentResponseEnvironmentLinkColor? linkColor = null;
            GetEnvironmentResponseEnvironmentBackgroundColor? backgroundColor = null;
            GetEnvironmentResponseEnvironmentLinkColor? buttonColor = null;
            GetEnvironmentResponseEnvironmentBackgroundColor? buttonTextColor = null;
            GetEnvironmentResponseEnvironmentLinkColor? linkColorDark = null;
            GetEnvironmentResponseEnvironmentLinkColor? backgroundColorDark = null;
            GetEnvironmentResponseEnvironmentLinkColor? buttonTextColorDark = null;
            GetEnvironmentResponseEnvironmentLinkColor? buttonColorDark = null;
            int? buttonBorderRadius = null;
            int? cardBorderRadius = null;
            int? inputBorderRadius = null;
            string? createdOn = null;
            string? senderName = null;
            string? senderEmail = null;
            GetOrganizationResponseBilling? billing = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["theme_code"] != null)
            {
                var themeCodeStr = jsonObject["theme_code"].ToObject<string>();
                if (!string.IsNullOrEmpty(themeCodeStr))
                {
                    themeCode = GetOrganizationResponse.ThemeCodeEnumFromString(themeCodeStr);
                }
            }

            if (jsonObject["color_scheme"] != null)
            {
                var colorSchemeStr = jsonObject["color_scheme"].ToObject<string>();
                if (!string.IsNullOrEmpty(colorSchemeStr))
                {
                    colorScheme = GetOrganizationResponse.ColorSchemeEnumFromString(colorSchemeStr);
                }
            }

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string>();
            }

            if (jsonObject["handle"] != null)
            {
                handle = jsonObject["handle"].ToObject<string>();
            }

            if (jsonObject["is_default"] != null)
            {
                isDefault = jsonObject["is_default"].ToObject<bool?>();
            }

            if (jsonObject["external_id"] != null)
            {
                externalId = jsonObject["external_id"].ToObject<string>();
            }

            if (jsonObject["is_auto_membership_enabled"] != null)
            {
                isAutoMembershipEnabled = jsonObject["is_auto_membership_enabled"].ToObject<bool?>();
            }

            if (jsonObject["logo"] != null)
            {
                logo = jsonObject["logo"].ToObject<string>();
            }

            if (jsonObject["logo_dark"] != null)
            {
                logoDark = jsonObject["logo_dark"].ToObject<string>();
            }

            if (jsonObject["favicon_svg"] != null)
            {
                faviconSvg = jsonObject["favicon_svg"].ToObject<string>();
            }

            if (jsonObject["favicon_fallback"] != null)
            {
                faviconFallback = jsonObject["favicon_fallback"].ToObject<string>();
            }

            if (jsonObject["link_color"] != null)
            {
                linkColor = jsonObject["link_color"].ToObject<GetEnvironmentResponseEnvironmentLinkColor>(serializer);
            }

            if (jsonObject["background_color"] != null)
            {
                backgroundColor = jsonObject["background_color"].ToObject<GetEnvironmentResponseEnvironmentBackgroundColor>(serializer);
            }

            if (jsonObject["button_color"] != null)
            {
                buttonColor = jsonObject["button_color"].ToObject<GetEnvironmentResponseEnvironmentLinkColor>(serializer);
            }

            if (jsonObject["button_text_color"] != null)
            {
                buttonTextColor = jsonObject["button_text_color"].ToObject<GetEnvironmentResponseEnvironmentBackgroundColor>(serializer);
            }

            if (jsonObject["link_color_dark"] != null)
            {
                linkColorDark = jsonObject["link_color_dark"].ToObject<GetEnvironmentResponseEnvironmentLinkColor>(serializer);
            }

            if (jsonObject["background_color_dark"] != null)
            {
                backgroundColorDark = jsonObject["background_color_dark"].ToObject<GetEnvironmentResponseEnvironmentLinkColor>(serializer);
            }

            if (jsonObject["button_text_color_dark"] != null)
            {
                buttonTextColorDark = jsonObject["button_text_color_dark"].ToObject<GetEnvironmentResponseEnvironmentLinkColor>(serializer);
            }

            if (jsonObject["button_color_dark"] != null)
            {
                buttonColorDark = jsonObject["button_color_dark"].ToObject<GetEnvironmentResponseEnvironmentLinkColor>(serializer);
            }

            if (jsonObject["button_border_radius"] != null)
            {
                buttonBorderRadius = jsonObject["button_border_radius"].ToObject<int?>();
            }

            if (jsonObject["card_border_radius"] != null)
            {
                cardBorderRadius = jsonObject["card_border_radius"].ToObject<int?>();
            }

            if (jsonObject["input_border_radius"] != null)
            {
                inputBorderRadius = jsonObject["input_border_radius"].ToObject<int?>();
            }

            if (jsonObject["created_on"] != null)
            {
                createdOn = jsonObject["created_on"].ToObject<string>();
            }

            if (jsonObject["sender_name"] != null)
            {
                senderName = jsonObject["sender_name"].ToObject<string>();
            }

            if (jsonObject["sender_email"] != null)
            {
                senderEmail = jsonObject["sender_email"].ToObject<string>();
            }

            if (jsonObject["billing"] != null)
            {
                billing = jsonObject["billing"].ToObject<GetOrganizationResponseBilling>(serializer);
            }

            return new GetOrganizationResponse(
                themeCode: themeCode != null ? new Option<GetOrganizationResponse.ThemeCodeEnum?>(themeCode) : default, colorScheme: colorScheme != null ? new Option<GetOrganizationResponse.ColorSchemeEnum?>(colorScheme) : default, code: code != null ? new Option<string?>(code) : default, name: name != null ? new Option<string?>(name) : default, handle: handle != null ? new Option<string?>(handle) : default, isDefault: isDefault != null ? new Option<bool?>(isDefault) : default, externalId: externalId != null ? new Option<string?>(externalId) : default, isAutoMembershipEnabled: isAutoMembershipEnabled != null ? new Option<bool?>(isAutoMembershipEnabled) : default, logo: logo != null ? new Option<string?>(logo) : default, logoDark: logoDark != null ? new Option<string?>(logoDark) : default, faviconSvg: faviconSvg != null ? new Option<string?>(faviconSvg) : default, faviconFallback: faviconFallback != null ? new Option<string?>(faviconFallback) : default, linkColor: linkColor != null ? new Option<GetEnvironmentResponseEnvironmentLinkColor?>(linkColor) : default, backgroundColor: backgroundColor != null ? new Option<GetEnvironmentResponseEnvironmentBackgroundColor?>(backgroundColor) : default, buttonColor: buttonColor != null ? new Option<GetEnvironmentResponseEnvironmentLinkColor?>(buttonColor) : default, buttonTextColor: buttonTextColor != null ? new Option<GetEnvironmentResponseEnvironmentBackgroundColor?>(buttonTextColor) : default, linkColorDark: linkColorDark != null ? new Option<GetEnvironmentResponseEnvironmentLinkColor?>(linkColorDark) : default, backgroundColorDark: backgroundColorDark != null ? new Option<GetEnvironmentResponseEnvironmentLinkColor?>(backgroundColorDark) : default, buttonTextColorDark: buttonTextColorDark != null ? new Option<GetEnvironmentResponseEnvironmentLinkColor?>(buttonTextColorDark) : default, buttonColorDark: buttonColorDark != null ? new Option<GetEnvironmentResponseEnvironmentLinkColor?>(buttonColorDark) : default, buttonBorderRadius: buttonBorderRadius != null ? new Option<int?>(buttonBorderRadius) : default, cardBorderRadius: cardBorderRadius != null ? new Option<int?>(cardBorderRadius) : default, inputBorderRadius: inputBorderRadius != null ? new Option<int?>(inputBorderRadius) : default, createdOn: createdOn != null ? new Option<string?>(createdOn) : default, senderName: senderName != null ? new Option<string?>(senderName) : default, senderEmail: senderEmail != null ? new Option<string?>(senderEmail) : default, billing: billing != null ? new Option<GetOrganizationResponseBilling?>(billing) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetOrganizationResponse value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.ThemeCodeOption.IsSet && value.ThemeCode != null)
            {
                writer.WritePropertyName("theme_code");
                var themecodeStr = GetOrganizationResponse.ThemeCodeEnumToJsonValue(value.ThemeCode.Value);
                writer.WriteValue(themecodeStr);
            }

            if (value.ColorSchemeOption.IsSet && value.ColorScheme != null)
            {
                writer.WritePropertyName("color_scheme");
                var colorschemeStr = GetOrganizationResponse.ColorSchemeEnumToJsonValue(value.ColorScheme.Value);
                writer.WriteValue(colorschemeStr);
            }

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

            if (value.HandleOption.IsSet && value.Handle != null)
            {
                writer.WritePropertyName("handle");
                serializer.Serialize(writer, value.Handle);
            }

            if (value.IsDefaultOption.IsSet && value.IsDefault != null)
            {
                writer.WritePropertyName("is_default");
                writer.WriteValue(value.IsDefault.Value);
            }

            if (value.ExternalIdOption.IsSet && value.ExternalId != null)
            {
                writer.WritePropertyName("external_id");
                serializer.Serialize(writer, value.ExternalId);
            }

            if (value.IsAutoMembershipEnabledOption.IsSet && value.IsAutoMembershipEnabled != null)
            {
                writer.WritePropertyName("is_auto_membership_enabled");
                writer.WriteValue(value.IsAutoMembershipEnabled.Value);
            }

            if (value.LogoOption.IsSet && value.Logo != null)
            {
                writer.WritePropertyName("logo");
                serializer.Serialize(writer, value.Logo);
            }

            if (value.LogoDarkOption.IsSet && value.LogoDark != null)
            {
                writer.WritePropertyName("logo_dark");
                serializer.Serialize(writer, value.LogoDark);
            }

            if (value.FaviconSvgOption.IsSet && value.FaviconSvg != null)
            {
                writer.WritePropertyName("favicon_svg");
                serializer.Serialize(writer, value.FaviconSvg);
            }

            if (value.FaviconFallbackOption.IsSet && value.FaviconFallback != null)
            {
                writer.WritePropertyName("favicon_fallback");
                serializer.Serialize(writer, value.FaviconFallback);
            }

            if (value.LinkColorOption.IsSet && value.LinkColor != null)
            {
                writer.WritePropertyName("link_color");
                serializer.Serialize(writer, value.LinkColor);
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

            if (value.LinkColorDarkOption.IsSet && value.LinkColorDark != null)
            {
                writer.WritePropertyName("link_color_dark");
                serializer.Serialize(writer, value.LinkColorDark);
            }

            if (value.BackgroundColorDarkOption.IsSet && value.BackgroundColorDark != null)
            {
                writer.WritePropertyName("background_color_dark");
                serializer.Serialize(writer, value.BackgroundColorDark);
            }

            if (value.ButtonTextColorDarkOption.IsSet && value.ButtonTextColorDark != null)
            {
                writer.WritePropertyName("button_text_color_dark");
                serializer.Serialize(writer, value.ButtonTextColorDark);
            }

            if (value.ButtonColorDarkOption.IsSet && value.ButtonColorDark != null)
            {
                writer.WritePropertyName("button_color_dark");
                serializer.Serialize(writer, value.ButtonColorDark);
            }

            if (value.ButtonBorderRadiusOption.IsSet && value.ButtonBorderRadius != null)
            {
                writer.WritePropertyName("button_border_radius");
                writer.WriteValue(value.ButtonBorderRadius.Value);
            }

            if (value.CardBorderRadiusOption.IsSet && value.CardBorderRadius != null)
            {
                writer.WritePropertyName("card_border_radius");
                writer.WriteValue(value.CardBorderRadius.Value);
            }

            if (value.InputBorderRadiusOption.IsSet && value.InputBorderRadius != null)
            {
                writer.WritePropertyName("input_border_radius");
                writer.WriteValue(value.InputBorderRadius.Value);
            }

            if (value.CreatedOnOption.IsSet && value.CreatedOn != null)
            {
                writer.WritePropertyName("created_on");
                serializer.Serialize(writer, value.CreatedOn);
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

            if (value.BillingOption.IsSet && value.Billing != null)
            {
                writer.WritePropertyName("billing");
                serializer.Serialize(writer, value.Billing);
            }

            writer.WriteEndObject();
        }
    }
}
