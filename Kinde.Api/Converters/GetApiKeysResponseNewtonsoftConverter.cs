using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetApiKeysResponse that handles the Option<> structure
    /// </summary>
    public class GetApiKeysResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetApiKeysResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetApiKeysResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetApiKeysResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? code = null;
            string? message = null;
            bool? hasMore = null;
            List<GetApiKeysResponseApiKeysInner> apiKeys = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string>();
            }

            if (jsonObject["has_more"] != null)
            {
                hasMore = jsonObject["has_more"].ToObject<bool?>();
            }

            if (jsonObject["api_keys"] != null)
            {
                apiKeys = jsonObject["api_keys"].ToObject<List<GetApiKeysResponseApiKeysInner>>(serializer);
            }

            return new GetApiKeysResponse(
                code: code != null ? new Option<string?>(code) : default, message: message != null ? new Option<string?>(message) : default, hasMore: hasMore != null ? new Option<bool?>(hasMore) : default, apiKeys: apiKeys != null ? new Option<List<GetApiKeysResponseApiKeysInner>?>(apiKeys) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetApiKeysResponse value, Newtonsoft.Json.JsonSerializer serializer)
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

            if (value.HasMoreOption.IsSet && value.HasMore != null)
            {
                writer.WritePropertyName("has_more");
                writer.WriteValue(value.HasMore.Value);
            }

            if (value.ApiKeysOption.IsSet && value.ApiKeys != null)
            {
                writer.WritePropertyName("api_keys");
                serializer.Serialize(writer, value.ApiKeys);
            }

            writer.WriteEndObject();
        }
    }
}
