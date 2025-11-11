using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetPropertiesResponse that handles the Option<> structure
    /// </summary>
    public class GetPropertiesResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetPropertiesResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetPropertiesResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetPropertiesResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            List<Property> properties = default(List<Property>);
            if (jsonObject["properties"] != null)
            {
                properties = jsonObject["properties"].ToObject<List<Property>>(serializer);
            }
            bool? hasMore = default(bool?);
            if (jsonObject["has_more"] != null)
            {
                hasMore = jsonObject["has_more"].ToObject<bool?>(serializer);
            }

            return new GetPropertiesResponse(
                code: code != null ? new Option<string?>(code) : default,                 message: message != null ? new Option<string?>(message) : default,                 properties: properties != null ? new Option<List<Property>?>(properties) : default,                 hasMore: hasMore != null ? new Option<bool?>(hasMore) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetPropertiesResponse value, Newtonsoft.Json.JsonSerializer serializer)
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
            if (value.PropertiesOption.IsSet)
            {
                writer.WritePropertyName("properties");
                serializer.Serialize(writer, value.Properties);
            }
            if (value.HasMoreOption.IsSet && value.HasMore != null)
            {
                writer.WritePropertyName("has_more");
                serializer.Serialize(writer, value.HasMore);
            }

            writer.WriteEndObject();
        }
    }
}