using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateCategoryResponse that handles the Option<> structure
    /// </summary>
    public class CreateCategoryResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateCategoryResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateCategoryResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateCategoryResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? message = null;
            string? code = null;
            CreateCategoryResponseCategory? category = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string>();
            }

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["category"] != null)
            {
                category = jsonObject["category"].ToObject<CreateCategoryResponseCategory>(serializer);
            }

            return new CreateCategoryResponse(
                message: message != null ? new Option<string?>(message) : default, code: code != null ? new Option<string?>(code) : default, category: category != null ? new Option<CreateCategoryResponseCategory?>(category) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateCategoryResponse value, Newtonsoft.Json.JsonSerializer serializer)
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

            if (value.CategoryOption.IsSet && value.Category != null)
            {
                writer.WritePropertyName("category");
                serializer.Serialize(writer, value.Category);
            }

            writer.WriteEndObject();
        }
    }
}
