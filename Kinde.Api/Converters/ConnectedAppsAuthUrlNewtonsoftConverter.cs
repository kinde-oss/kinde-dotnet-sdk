using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for ConnectedAppsAuthUrl that handles the Option<> structure
    /// </summary>
    public class ConnectedAppsAuthUrlNewtonsoftConverter : Newtonsoft.Json.JsonConverter<ConnectedAppsAuthUrl>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override ConnectedAppsAuthUrl ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, ConnectedAppsAuthUrl existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? url = default(string?);
            if (jsonObject["url"] != null)
            {
                url = jsonObject["url"].ToObject<string?>();
            }
            string? sessionId = default(string?);
            if (jsonObject["session_id"] != null)
            {
                sessionId = jsonObject["session_id"].ToObject<string?>();
            }

            return new ConnectedAppsAuthUrl(
                url: url != null ? new Option<string?>(url) : default,                 sessionId: sessionId != null ? new Option<string?>(sessionId) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, ConnectedAppsAuthUrl value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.UrlOption.IsSet && value.Url != null)
            {
                writer.WritePropertyName("url");
                serializer.Serialize(writer, value.Url);
            }
            if (value.SessionIdOption.IsSet && value.SessionId != null)
            {
                writer.WritePropertyName("session_id");
                serializer.Serialize(writer, value.SessionId);
            }

            writer.WriteEndObject();
        }
    }
}