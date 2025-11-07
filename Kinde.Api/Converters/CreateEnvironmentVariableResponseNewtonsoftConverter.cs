using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateEnvironmentVariableResponse that handles the Option<> structure
    /// </summary>
    public class CreateEnvironmentVariableResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateEnvironmentVariableResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateEnvironmentVariableResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateEnvironmentVariableResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? message = null;
            string? code = null;
            CreateEnvironmentVariableResponseEnvironmentVariable? environmentVariable = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string>();
            }

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["environment_variable"] != null)
            {
                environmentVariable = jsonObject["environment_variable"].ToObject<CreateEnvironmentVariableResponseEnvironmentVariable>(serializer);
            }

            return new CreateEnvironmentVariableResponse(
                message: message != null ? new Option<string?>(message) : default, code: code != null ? new Option<string?>(code) : default, environmentVariable: environmentVariable != null ? new Option<CreateEnvironmentVariableResponseEnvironmentVariable?>(environmentVariable) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateEnvironmentVariableResponse value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.MessageOption.IsSet && value.Message != null)
            {
                writer.WritePropertyName("message");
                serializer.Serialize(writer, value.Message);
            }

            if (value.CodeOption.IsSet && value.Code != null)
            {
                writer.WritePropertyName("code");
                serializer.Serialize(writer, value.Code);
            }

            if (value.EnvironmentVariableOption.IsSet && value.EnvironmentVariable != null)
            {
                writer.WritePropertyName("environment_variable");
                serializer.Serialize(writer, value.EnvironmentVariable);
            }

            writer.WriteEndObject();
        }
    }
}
