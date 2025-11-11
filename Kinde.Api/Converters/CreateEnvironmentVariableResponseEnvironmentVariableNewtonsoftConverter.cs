using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateEnvironmentVariableResponseEnvironmentVariable that handles the Option<> structure
    /// </summary>
    public class CreateEnvironmentVariableResponseEnvironmentVariableNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateEnvironmentVariableResponseEnvironmentVariable>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateEnvironmentVariableResponseEnvironmentVariable ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateEnvironmentVariableResponseEnvironmentVariable existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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

            return new CreateEnvironmentVariableResponseEnvironmentVariable(
                id: id != null ? new Option<string?>(id) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateEnvironmentVariableResponseEnvironmentVariable value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IdOption.IsSet && value.Id != null)
            {
                writer.WritePropertyName("id");
                serializer.Serialize(writer, value.Id);
            }

            writer.WriteEndObject();
        }
    }
}