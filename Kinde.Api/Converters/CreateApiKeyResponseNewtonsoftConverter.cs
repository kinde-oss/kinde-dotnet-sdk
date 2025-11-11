using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateApiKeyResponse that handles the Option<> structure
    /// </summary>
    public class CreateApiKeyResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateApiKeyResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateApiKeyResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateApiKeyResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? message = default(string?);
            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string?>();
            }
            string? code = default(string?);
            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string?>();
            }
            CreateApiKeyResponseApiKey? apiKey = default(CreateApiKeyResponseApiKey?);
            if (jsonObject["api_key"] != null)
            {
                apiKey = jsonObject["api_key"].ToObject<CreateApiKeyResponseApiKey?>(serializer);
            }

            return new CreateApiKeyResponse(
                message: message != null ? new Option<string?>(message) : default,                 code: code != null ? new Option<string?>(code) : default,                 apiKey: apiKey != null ? new Option<CreateApiKeyResponseApiKey?>(apiKey) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateApiKeyResponse value, Newtonsoft.Json.JsonSerializer serializer)
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
            if (value.ApiKeyOption.IsSet && value.ApiKey != null)
            {
                writer.WritePropertyName("api_key");
                serializer.Serialize(writer, value.ApiKey);
            }

            writer.WriteEndObject();
        }
    }
}