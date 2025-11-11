using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for EnvironmentVariable that handles the Option<> structure
    /// </summary>
    public class EnvironmentVariableNewtonsoftConverter : Newtonsoft.Json.JsonConverter<EnvironmentVariable>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override EnvironmentVariable ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, EnvironmentVariable existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            string? value = default(string?);
            if (jsonObject["value"] != null)
            {
                value = jsonObject["value"].ToObject<string?>();
            }
            bool? isSecret = default(bool?);
            if (jsonObject["is_secret"] != null)
            {
                isSecret = jsonObject["is_secret"].ToObject<bool?>(serializer);
            }
            string? createdOn = default(string?);
            if (jsonObject["created_on"] != null)
            {
                createdOn = jsonObject["created_on"].ToObject<string?>();
            }

            return new EnvironmentVariable(
                id: id != null ? new Option<string?>(id) : default,                 key: key != null ? new Option<string?>(key) : default,                 value: value != null ? new Option<string?>(value) : default,                 isSecret: isSecret != null ? new Option<bool?>(isSecret) : default,                 createdOn: createdOn != null ? new Option<string?>(createdOn) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, EnvironmentVariable value, Newtonsoft.Json.JsonSerializer serializer)
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
            if (value.ValueOption.IsSet && value.Value != null)
            {
                writer.WritePropertyName("value");
                serializer.Serialize(writer, value.Value);
            }
            if (value.IsSecretOption.IsSet && value.IsSecret != null)
            {
                writer.WritePropertyName("is_secret");
                serializer.Serialize(writer, value.IsSecret);
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