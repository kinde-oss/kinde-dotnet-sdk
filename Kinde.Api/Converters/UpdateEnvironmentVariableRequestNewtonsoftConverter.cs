using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UpdateEnvironmentVariableRequest that handles the Option<> structure
    /// </summary>
    public class UpdateEnvironmentVariableRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UpdateEnvironmentVariableRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UpdateEnvironmentVariableRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UpdateEnvironmentVariableRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? key = null;
            string? value = null;
            bool? isSecret = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["key"] != null)
            {
                key = jsonObject["key"].ToObject<string>();
            }

            if (jsonObject["value"] != null)
            {
                value = jsonObject["value"].ToObject<string>();
            }

            if (jsonObject["is_secret"] != null)
            {
                isSecret = jsonObject["is_secret"].ToObject<bool?>();
            }

            return new UpdateEnvironmentVariableRequest(
                key: key != null ? new Option<string?>(key) : default, value: value != null ? new Option<string?>(value) : default, isSecret: isSecret != null ? new Option<bool?>(isSecret) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UpdateEnvironmentVariableRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

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
                writer.WriteValue(value.IsSecret.Value);
            }

            writer.WriteEndObject();
        }
    }
}
