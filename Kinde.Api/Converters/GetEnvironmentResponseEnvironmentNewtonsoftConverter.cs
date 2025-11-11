using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetEnvironmentResponseEnvironment that handles the Option<> structure
    /// </summary>
    public class GetEnvironmentResponseEnvironmentNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetEnvironmentResponseEnvironment>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetEnvironmentResponseEnvironment ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetEnvironmentResponseEnvironment existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            GetEnvironmentResponseEnvironment.ThemeCodeEnum? themeCode = default(GetEnvironmentResponseEnvironment.ThemeCodeEnum?);
            if (jsonObject["theme_code"] != null)
            {
                var themeCodeStr = jsonObject["theme_code"].ToObject<string>();
                if (!string.IsNullOrEmpty(themeCodeStr))
                {
                    themeCode = GetEnvironmentResponseEnvironment.ThemeCodeEnumFromString(themeCodeStr);
                }
            }
            GetEnvironmentResponseEnvironment.ColorSchemeEnum? colorScheme = default(GetEnvironmentResponseEnvironment.ColorSchemeEnum?);
            if (jsonObject["color_scheme"] != null)
            {
                var colorSchemeStr = jsonObject["color_scheme"].ToObject<string>();
                if (!string.IsNullOrEmpty(colorSchemeStr))
                {
                    colorScheme = GetEnvironmentResponseEnvironment.ColorSchemeEnumFromString(colorSchemeStr);
                }
            }
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
            string? hotjarSiteId = default(string?);
            if (jsonObject["hotjar_site_id"] != null)
            {
                hotjarSiteId = jsonObject["hotjar_site_id"].ToObject<string?>();
            }
            string? googleAnalyticsTag = default(string?);
            if (jsonObject["google_analytics_tag"] != null)
            {
                googleAnalyticsTag = jsonObject["google_analytics_tag"].ToObject<string?>();
            }
            bool? isDefault = default(bool?);
            if (jsonObject["is_default"] != null)
            {
                isDefault = jsonObject["is_default"].ToObject<bool?>(serializer);
            }
            bool? isLive = default(bool?);
            if (jsonObject["is_live"] != null)
            {
                isLive = jsonObject["is_live"].ToObject<bool?>(serializer);
            }
            string? kindeDomain = default(string?);
            if (jsonObject["kinde_domain"] != null)
            {
                kindeDomain = jsonObject["kinde_domain"].ToObject<string?>();
            }
            string? customDomain = default(string?);
            if (jsonObject["custom_domain"] != null)
            {
                customDomain = jsonObject["custom_domain"].ToObject<string?>();
            }
            string? logo = default(string?);
            if (jsonObject["logo"] != null)
            {
                logo = jsonObject["logo"].ToObject<string?>();
            }
            string? logoDark = default(string?);
            if (jsonObject["logo_dark"] != null)
            {
                logoDark = jsonObject["logo_dark"].ToObject<string?>();
            }
            string? faviconSvg = default(string?);
            if (jsonObject["favicon_svg"] != null)
            {
                faviconSvg = jsonObject["favicon_svg"].ToObject<string?>();
            }
            string? faviconFallback = default(string?);
            if (jsonObject["favicon_fallback"] != null)
            {
                faviconFallback = jsonObject["favicon_fallback"].ToObject<string?>();
            }
            GetEnvironmentResponseEnvironmentLinkColor? linkColor = default(GetEnvironmentResponseEnvironmentLinkColor?);
            if (jsonObject["link_color"] != null)
            {
                linkColor = jsonObject["link_color"].ToObject<GetEnvironmentResponseEnvironmentLinkColor?>(serializer);
            }
            GetEnvironmentResponseEnvironmentBackgroundColor? backgroundColor = default(GetEnvironmentResponseEnvironmentBackgroundColor?);
            if (jsonObject["background_color"] != null)
            {
                backgroundColor = jsonObject["background_color"].ToObject<GetEnvironmentResponseEnvironmentBackgroundColor?>(serializer);
            }
            GetEnvironmentResponseEnvironmentLinkColor? buttonColor = default(GetEnvironmentResponseEnvironmentLinkColor?);
            if (jsonObject["button_color"] != null)
            {
                buttonColor = jsonObject["button_color"].ToObject<GetEnvironmentResponseEnvironmentLinkColor?>(serializer);
            }
            GetEnvironmentResponseEnvironmentBackgroundColor? buttonTextColor = default(GetEnvironmentResponseEnvironmentBackgroundColor?);
            if (jsonObject["button_text_color"] != null)
            {
                buttonTextColor = jsonObject["button_text_color"].ToObject<GetEnvironmentResponseEnvironmentBackgroundColor?>(serializer);
            }
            GetEnvironmentResponseEnvironmentLinkColor? linkColorDark = default(GetEnvironmentResponseEnvironmentLinkColor?);
            if (jsonObject["link_color_dark"] != null)
            {
                linkColorDark = jsonObject["link_color_dark"].ToObject<GetEnvironmentResponseEnvironmentLinkColor?>(serializer);
            }
            GetEnvironmentResponseEnvironmentLinkColor? backgroundColorDark = default(GetEnvironmentResponseEnvironmentLinkColor?);
            if (jsonObject["background_color_dark"] != null)
            {
                backgroundColorDark = jsonObject["background_color_dark"].ToObject<GetEnvironmentResponseEnvironmentLinkColor?>(serializer);
            }
            GetEnvironmentResponseEnvironmentLinkColor? buttonTextColorDark = default(GetEnvironmentResponseEnvironmentLinkColor?);
            if (jsonObject["button_text_color_dark"] != null)
            {
                buttonTextColorDark = jsonObject["button_text_color_dark"].ToObject<GetEnvironmentResponseEnvironmentLinkColor?>(serializer);
            }
            GetEnvironmentResponseEnvironmentLinkColor? buttonColorDark = default(GetEnvironmentResponseEnvironmentLinkColor?);
            if (jsonObject["button_color_dark"] != null)
            {
                buttonColorDark = jsonObject["button_color_dark"].ToObject<GetEnvironmentResponseEnvironmentLinkColor?>(serializer);
            }
            int? buttonBorderRadius = default(int?);
            if (jsonObject["button_border_radius"] != null)
            {
                buttonBorderRadius = jsonObject["button_border_radius"].ToObject<int?>(serializer);
            }
            int? cardBorderRadius = default(int?);
            if (jsonObject["card_border_radius"] != null)
            {
                cardBorderRadius = jsonObject["card_border_radius"].ToObject<int?>(serializer);
            }
            int? inputBorderRadius = default(int?);
            if (jsonObject["input_border_radius"] != null)
            {
                inputBorderRadius = jsonObject["input_border_radius"].ToObject<int?>(serializer);
            }
            string? createdOn = default(string?);
            if (jsonObject["created_on"] != null)
            {
                createdOn = jsonObject["created_on"].ToObject<string?>();
            }

            return new GetEnvironmentResponseEnvironment(
                themeCode: themeCode != null ? new Option<GetEnvironmentResponseEnvironment.ThemeCodeEnum?>(themeCode) : default,                 colorScheme: colorScheme != null ? new Option<GetEnvironmentResponseEnvironment.ColorSchemeEnum?>(colorScheme) : default,                 code: code != null ? new Option<string?>(code) : default,                 name: name != null ? new Option<string?>(name) : default,                 hotjarSiteId: hotjarSiteId != null ? new Option<string?>(hotjarSiteId) : default,                 googleAnalyticsTag: googleAnalyticsTag != null ? new Option<string?>(googleAnalyticsTag) : default,                 isDefault: isDefault != null ? new Option<bool?>(isDefault) : default,                 isLive: isLive != null ? new Option<bool?>(isLive) : default,                 kindeDomain: kindeDomain != null ? new Option<string?>(kindeDomain) : default,                 customDomain: customDomain != null ? new Option<string?>(customDomain) : default,                 logo: logo != null ? new Option<string?>(logo) : default,                 logoDark: logoDark != null ? new Option<string?>(logoDark) : default,                 faviconSvg: faviconSvg != null ? new Option<string?>(faviconSvg) : default,                 faviconFallback: faviconFallback != null ? new Option<string?>(faviconFallback) : default,                 linkColor: linkColor != null ? new Option<GetEnvironmentResponseEnvironmentLinkColor?>(linkColor) : default,                 backgroundColor: backgroundColor != null ? new Option<GetEnvironmentResponseEnvironmentBackgroundColor?>(backgroundColor) : default,                 buttonColor: buttonColor != null ? new Option<GetEnvironmentResponseEnvironmentLinkColor?>(buttonColor) : default,                 buttonTextColor: buttonTextColor != null ? new Option<GetEnvironmentResponseEnvironmentBackgroundColor?>(buttonTextColor) : default,                 linkColorDark: linkColorDark != null ? new Option<GetEnvironmentResponseEnvironmentLinkColor?>(linkColorDark) : default,                 backgroundColorDark: backgroundColorDark != null ? new Option<GetEnvironmentResponseEnvironmentLinkColor?>(backgroundColorDark) : default,                 buttonTextColorDark: buttonTextColorDark != null ? new Option<GetEnvironmentResponseEnvironmentLinkColor?>(buttonTextColorDark) : default,                 buttonColorDark: buttonColorDark != null ? new Option<GetEnvironmentResponseEnvironmentLinkColor?>(buttonColorDark) : default,                 buttonBorderRadius: buttonBorderRadius != null ? new Option<int?>(buttonBorderRadius) : default,                 cardBorderRadius: cardBorderRadius != null ? new Option<int?>(cardBorderRadius) : default,                 inputBorderRadius: inputBorderRadius != null ? new Option<int?>(inputBorderRadius) : default,                 createdOn: createdOn != null ? new Option<string?>(createdOn) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetEnvironmentResponseEnvironment value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.ThemeCodeOption.IsSet && value.ThemeCode != null)
            {
                writer.WritePropertyName("theme_code");
                var themeCodeStr = GetEnvironmentResponseEnvironment.ThemeCodeEnumToJsonValue(value.ThemeCode.Value);
                writer.WriteValue(themeCodeStr);
            }
            if (value.ColorSchemeOption.IsSet && value.ColorScheme != null)
            {
                writer.WritePropertyName("color_scheme");
                var colorSchemeStr = GetEnvironmentResponseEnvironment.ColorSchemeEnumToJsonValue(value.ColorScheme.Value);
                writer.WriteValue(colorSchemeStr);
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
            if (value.HotjarSiteIdOption.IsSet && value.HotjarSiteId != null)
            {
                writer.WritePropertyName("hotjar_site_id");
                serializer.Serialize(writer, value.HotjarSiteId);
            }
            if (value.GoogleAnalyticsTagOption.IsSet && value.GoogleAnalyticsTag != null)
            {
                writer.WritePropertyName("google_analytics_tag");
                serializer.Serialize(writer, value.GoogleAnalyticsTag);
            }
            if (value.IsDefaultOption.IsSet && value.IsDefault != null)
            {
                writer.WritePropertyName("is_default");
                serializer.Serialize(writer, value.IsDefault);
            }
            if (value.IsLiveOption.IsSet && value.IsLive != null)
            {
                writer.WritePropertyName("is_live");
                serializer.Serialize(writer, value.IsLive);
            }
            if (value.KindeDomainOption.IsSet && value.KindeDomain != null)
            {
                writer.WritePropertyName("kinde_domain");
                serializer.Serialize(writer, value.KindeDomain);
            }
            if (value.CustomDomainOption.IsSet && value.CustomDomain != null)
            {
                writer.WritePropertyName("custom_domain");
                serializer.Serialize(writer, value.CustomDomain);
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
                serializer.Serialize(writer, value.ButtonBorderRadius);
            }
            if (value.CardBorderRadiusOption.IsSet && value.CardBorderRadius != null)
            {
                writer.WritePropertyName("card_border_radius");
                serializer.Serialize(writer, value.CardBorderRadius);
            }
            if (value.InputBorderRadiusOption.IsSet && value.InputBorderRadius != null)
            {
                writer.WritePropertyName("input_border_radius");
                serializer.Serialize(writer, value.InputBorderRadius);
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