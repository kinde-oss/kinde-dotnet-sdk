using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreatePropertyResponse that handles the Option<> structure
    /// </summary>
    public class CreatePropertyResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreatePropertyResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreatePropertyResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreatePropertyResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? message = null;
            string? code = null;
            CreatePropertyResponseProperty? property = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string>();
            }

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["property"] != null)
            {
                property = jsonObject["property"].ToObject<CreatePropertyResponseProperty>(serializer);
            }

            return new CreatePropertyResponse(
                message: message != null ? new Option<string?>(message) : default, code: code != null ? new Option<string?>(code) : default, property: property != null ? new Option<CreatePropertyResponseProperty?>(property) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreatePropertyResponse value, Newtonsoft.Json.JsonSerializer serializer)
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

            if (value.PropertyOption.IsSet && value.Property != null)
            {
                writer.WritePropertyName("property");
                serializer.Serialize(writer, value.Property);
            }

            writer.WriteEndObject();
        }
    }
}
