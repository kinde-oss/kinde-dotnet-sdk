using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetApiKeyResponse that handles the Option<> structure
    /// </summary>
    public class GetApiKeyResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetApiKeyResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetApiKeyResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetApiKeyResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? code = null;
            string? message = null;
            GetApiKeyResponseApiKey? apiKey = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string>();
            }

            if (jsonObject["api_key"] != null)
            {
                apiKey = jsonObject["api_key"].ToObject<GetApiKeyResponseApiKey>(serializer);
            }

            return new GetApiKeyResponse(
                code: code != null ? new Option<string?>(code) : default, message: message != null ? new Option<string?>(message) : default, apiKey: apiKey != null ? new Option<GetApiKeyResponseApiKey?>(apiKey) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetApiKeyResponse value, Newtonsoft.Json.JsonSerializer serializer)
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

            if (value.ApiKeyOption.IsSet && value.ApiKey != null)
            {
                writer.WritePropertyName("api_key");
                serializer.Serialize(writer, value.ApiKey);
            }

            writer.WriteEndObject();
        }
    }
}
