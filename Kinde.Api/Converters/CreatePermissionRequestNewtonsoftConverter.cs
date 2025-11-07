using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreatePermissionRequest that handles the Option<> structure
    /// </summary>
    public class CreatePermissionRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreatePermissionRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreatePermissionRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreatePermissionRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? name = null;
            string? description = null;
            string? key = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string>();
            }

            if (jsonObject["description"] != null)
            {
                description = jsonObject["description"].ToObject<string>();
            }

            if (jsonObject["key"] != null)
            {
                key = jsonObject["key"].ToObject<string>();
            }

            return new CreatePermissionRequest(
                name: name != null ? new Option<string?>(name) : default, description: description != null ? new Option<string?>(description) : default, key: key != null ? new Option<string?>(key) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreatePermissionRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.NameOption.IsSet && value.Name != null)
            {
                writer.WritePropertyName("name");
                serializer.Serialize(writer, value.Name);
            }

            if (value.DescriptionOption.IsSet && value.Description != null)
            {
                writer.WritePropertyName("description");
                serializer.Serialize(writer, value.Description);
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
