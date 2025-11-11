using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for RotateApiKeyResponseApiKey that handles the Option<> structure
    /// </summary>
    public class RotateApiKeyResponseApiKeyNewtonsoftConverter : Newtonsoft.Json.JsonConverter<RotateApiKeyResponseApiKey>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override RotateApiKeyResponseApiKey ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, RotateApiKeyResponseApiKey existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? id = default(string?);
            if (jsonObject["id"] != null)
            {
                id = jsonObject["id"].ToObject<string?>();
            }
            string? key = default(string?);
            if (jsonObject["key"] != null)
            {
                key = jsonObject["key"].ToObject<string?>();
            }

            return new RotateApiKeyResponseApiKey(
                id: id != null ? new Option<string?>(id) : default,                 key: key != null ? new Option<string?>(key) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, RotateApiKeyResponseApiKey value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IdOption.IsSet && value.Id != null)
            {
                writer.WritePropertyName("id");
                serializer.Serialize(writer, value.Id);
            }
            if (value.KeyOption.IsSet && value.Key != null)
            {
                writer.WritePropertyName("key");
                serializer.Serialize(writer, value.Key);
            }

            writer.WriteEndObject();
        }
    }
}