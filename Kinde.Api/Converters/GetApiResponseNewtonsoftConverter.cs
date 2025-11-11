using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetApiResponse that handles the Option<> structure
    /// </summary>
    public class GetApiResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetApiResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetApiResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetApiResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            GetApiResponseApi? api = default(GetApiResponseApi?);
            if (jsonObject["api"] != null)
            {
                api = jsonObject["api"].ToObject<GetApiResponseApi?>(serializer);
            }

            return new GetApiResponse(
                code: code != null ? new Option<string?>(code) : default,                 message: message != null ? new Option<string?>(message) : default,                 api: api != null ? new Option<GetApiResponseApi?>(api) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetApiResponse value, Newtonsoft.Json.JsonSerializer serializer)
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
            if (value.ApiOption.IsSet && value.Api != null)
            {
                writer.WritePropertyName("api");
                serializer.Serialize(writer, value.Api);
            }

            writer.WriteEndObject();
        }
    }
}