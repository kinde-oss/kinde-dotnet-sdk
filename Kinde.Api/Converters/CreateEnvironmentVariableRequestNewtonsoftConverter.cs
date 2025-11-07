using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateEnvironmentVariableRequest that handles the Option<> structure
    /// </summary>
    public class CreateEnvironmentVariableRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateEnvironmentVariableRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateEnvironmentVariableRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateEnvironmentVariableRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string key = default(string);
            string value = default(string);
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

            return new CreateEnvironmentVariableRequest(
                key: key, value: value, isSecret: isSecret != null ? new Option<bool?>(isSecret) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateEnvironmentVariableRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IsSecretOption.IsSet && value.IsSecret != null)
            {
                writer.WritePropertyName("is_secret");
                writer.WriteValue(value.IsSecret.Value);
            }

            writer.WriteEndObject();
        }
    }
}
