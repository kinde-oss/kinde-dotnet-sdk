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

            var jsonObject = JObject.Load(reader);

            bool? isSecret = default(bool?);
            if (jsonObject["is_secret"] != null)
            {
                isSecret = jsonObject["is_secret"].ToObject<bool?>(serializer);
            }
            string key = default(string);
            if (jsonObject["key"] != null)
            {
                key = jsonObject["key"].ToObject<string>();
            }
            string value = default(string);
            if (jsonObject["value"] != null)
            {
                value = jsonObject["value"].ToObject<string>();
            }

            return new CreateEnvironmentVariableRequest(
                isSecret: isSecret != null ? new Option<bool?>(isSecret) : default,                 key: key,                 value: value            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateEnvironmentVariableRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IsSecretOption.IsSet && value.IsSecret != null)
            {
                writer.WritePropertyName("is_secret");
                serializer.Serialize(writer, value.IsSecret);
            }

            writer.WriteEndObject();
        }
    }
}