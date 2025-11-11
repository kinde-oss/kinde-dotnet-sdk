using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetEnvironmentResponse that handles the Option<> structure
    /// </summary>
    public class GetEnvironmentResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetEnvironmentResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetEnvironmentResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetEnvironmentResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            GetEnvironmentResponseEnvironment? varEnvironment = default(GetEnvironmentResponseEnvironment?);
            if (jsonObject["environment"] != null)
            {
                varEnvironment = jsonObject["environment"].ToObject<GetEnvironmentResponseEnvironment?>(serializer);
            }

            return new GetEnvironmentResponse(
                code: code != null ? new Option<string?>(code) : default,                 message: message != null ? new Option<string?>(message) : default,                 varEnvironment: varEnvironment != null ? new Option<GetEnvironmentResponseEnvironment?>(varEnvironment) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetEnvironmentResponse value, Newtonsoft.Json.JsonSerializer serializer)
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
            if (value.VarEnvironmentOption.IsSet && value.VarEnvironment != null)
            {
                writer.WritePropertyName("environment");
                serializer.Serialize(writer, value.VarEnvironment);
            }

            writer.WriteEndObject();
        }
    }
}