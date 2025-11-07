using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetPropertyValuesResponse that handles the Option<> structure
    /// </summary>
    public class GetPropertyValuesResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetPropertyValuesResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetPropertyValuesResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetPropertyValuesResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? code = null;
            string? message = null;
            List<PropertyValue> properties = null;
            string? nextToken = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string>();
            }

            if (jsonObject["properties"] != null)
            {
                properties = jsonObject["properties"].ToObject<List<PropertyValue>>(serializer);
            }

            if (jsonObject["next_token"] != null)
            {
                nextToken = jsonObject["next_token"].ToObject<string>();
            }

            return new GetPropertyValuesResponse(
                code: code != null ? new Option<string?>(code) : default, message: message != null ? new Option<string?>(message) : default, properties: properties != null ? new Option<List<PropertyValue>?>(properties) : default, nextToken: nextToken != null ? new Option<string?>(nextToken) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetPropertyValuesResponse value, Newtonsoft.Json.JsonSerializer serializer)
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

            if (value.PropertiesOption.IsSet && value.Properties != null)
            {
                writer.WritePropertyName("properties");
                serializer.Serialize(writer, value.Properties);
            }

            if (value.NextTokenOption.IsSet && value.NextToken != null)
            {
                writer.WritePropertyName("next_token");
                serializer.Serialize(writer, value.NextToken);
            }

            writer.WriteEndObject();
        }
    }
}
