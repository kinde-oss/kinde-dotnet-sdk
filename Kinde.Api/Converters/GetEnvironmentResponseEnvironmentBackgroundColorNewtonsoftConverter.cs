using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetEnvironmentResponseEnvironmentBackgroundColor that handles the Option<> structure
    /// </summary>
    public class GetEnvironmentResponseEnvironmentBackgroundColorNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetEnvironmentResponseEnvironmentBackgroundColor>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetEnvironmentResponseEnvironmentBackgroundColor ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetEnvironmentResponseEnvironmentBackgroundColor existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? raw = default(string?);
            if (jsonObject["raw"] != null)
            {
                raw = jsonObject["raw"].ToObject<string?>();
            }
            string? hex = default(string?);
            if (jsonObject["hex"] != null)
            {
                hex = jsonObject["hex"].ToObject<string?>();
            }
            string? hsl = default(string?);
            if (jsonObject["hsl"] != null)
            {
                hsl = jsonObject["hsl"].ToObject<string?>();
            }

            return new GetEnvironmentResponseEnvironmentBackgroundColor(
                raw: raw != null ? new Option<string?>(raw) : default,                 hex: hex != null ? new Option<string?>(hex) : default,                 hsl: hsl != null ? new Option<string?>(hsl) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetEnvironmentResponseEnvironmentBackgroundColor value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.RawOption.IsSet && value.Raw != null)
            {
                writer.WritePropertyName("raw");
                serializer.Serialize(writer, value.Raw);
            }
            if (value.HexOption.IsSet && value.Hex != null)
            {
                writer.WritePropertyName("hex");
                serializer.Serialize(writer, value.Hex);
            }
            if (value.HslOption.IsSet && value.Hsl != null)
            {
                writer.WritePropertyName("hsl");
                serializer.Serialize(writer, value.Hsl);
            }

            writer.WriteEndObject();
        }
    }
}