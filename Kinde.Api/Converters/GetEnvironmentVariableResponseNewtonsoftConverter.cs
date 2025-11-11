using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetEnvironmentVariableResponse that handles the Option<> structure
    /// </summary>
    public class GetEnvironmentVariableResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetEnvironmentVariableResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetEnvironmentVariableResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetEnvironmentVariableResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? code = default(string?);
            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string?>();
            }
            string? message = default(string?);
            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string?>();
            }
            EnvironmentVariable? environmentVariable = default(EnvironmentVariable?);
            if (jsonObject["environment_variable"] != null)
            {
                environmentVariable = jsonObject["environment_variable"].ToObject<EnvironmentVariable?>(serializer);
            }

            return new GetEnvironmentVariableResponse(
                code: code != null ? new Option<string?>(code) : default,                 message: message != null ? new Option<string?>(message) : default,                 environmentVariable: environmentVariable != null ? new Option<EnvironmentVariable?>(environmentVariable) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetEnvironmentVariableResponse value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.CodeOption.IsSet && value.Code != null)
            {
                writer.WritePropertyName("code");
                serializer.Serialize(writer, value.Code);
            }
            if (value.MessageOption.IsSet && value.Message != null)
            {
                writer.WritePropertyName("message");
                serializer.Serialize(writer, value.Message);
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