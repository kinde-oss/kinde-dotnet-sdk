using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetCategoriesResponse that handles the Option<> structure
    /// </summary>
    public class GetCategoriesResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetCategoriesResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetCategoriesResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetCategoriesResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? code = null;
            string? message = null;
            List<Category> categories = null;
            bool? hasMore = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string>();
            }

            if (jsonObject["categories"] != null)
            {
                categories = jsonObject["categories"].ToObject<List<Category>>(serializer);
            }

            if (jsonObject["has_more"] != null)
            {
                hasMore = jsonObject["has_more"].ToObject<bool?>();
            }

            return new GetCategoriesResponse(
                code: code != null ? new Option<string?>(code) : default, message: message != null ? new Option<string?>(message) : default, categories: categories != null ? new Option<List<Category>?>(categories) : default, hasMore: hasMore != null ? new Option<bool?>(hasMore) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetCategoriesResponse value, Newtonsoft.Json.JsonSerializer serializer)
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

            if (value.CategoriesOption.IsSet && value.Categories != null)
            {
                writer.WritePropertyName("categories");
                serializer.Serialize(writer, value.Categories);
            }

            if (value.HasMoreOption.IsSet && value.HasMore != null)
            {
                writer.WritePropertyName("has_more");
                writer.WriteValue(value.HasMore.Value);
            }

            writer.WriteEndObject();
        }
    }
}
