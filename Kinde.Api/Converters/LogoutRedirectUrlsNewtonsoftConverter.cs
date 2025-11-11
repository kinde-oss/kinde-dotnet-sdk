using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for LogoutRedirectUrls that handles the Option<> structure
    /// </summary>
    public class LogoutRedirectUrlsNewtonsoftConverter : Newtonsoft.Json.JsonConverter<LogoutRedirectUrls>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override LogoutRedirectUrls ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, LogoutRedirectUrls existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            List<string> logoutUrls = default(List<string>);
            if (jsonObject["logout_urls"] != null)
            {
                logoutUrls = jsonObject["logout_urls"].ToObject<List<string>>(serializer);
            }
            string? code = default(string?);
            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string?>();
            }
            string? message = default(string?);
            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string?>();
            }

            return new LogoutRedirectUrls(
                logoutUrls: logoutUrls != null ? new Option<List<string>?>(logoutUrls) : default,                 code: code != null ? new Option<string?>(code) : default,                 message: message != null ? new Option<string?>(message) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, LogoutRedirectUrls value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.LogoutUrlsOption.IsSet)
            {
                writer.WritePropertyName("logout_urls");
                serializer.Serialize(writer, value.LogoutUrls);
            }
            if (value.CodeOption.IsSet && value.Code != null)
            {
                writer.WritePropertyName("code");
                serializer.Serialize(writer, value.Code);
            }
            if (value.MessageOption.IsSet && value.Message != null)
            {
                writer.WritePropertyName("message");
                serializer.Serialize(writer, value.Message);
            }

            writer.WriteEndObject();
        }
    }
}