using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreatePropertyRequest that handles the Option<> structure
    /// </summary>
    public class CreatePropertyRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreatePropertyRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreatePropertyRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreatePropertyRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string name = default(string);
            string key = default(string);
            CreatePropertyRequest.TypeEnum type = default(CreatePropertyRequest.TypeEnum);
            CreatePropertyRequest.ContextEnum context = default(CreatePropertyRequest.ContextEnum);
            bool isPrivate = default(bool);
            string categoryId = default(string);
            string? description = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string>();
            }

            if (jsonObject["key"] != null)
            {
                key = jsonObject["key"].ToObject<string>();
            }

            if (jsonObject["type"] != null)
            {
                var typeStr = jsonObject["type"].ToObject<string>();
                if (!string.IsNullOrEmpty(typeStr))
                {
                    type = CreatePropertyRequest.TypeEnumFromString(typeStr);
                }
            }

            if (jsonObject["context"] != null)
            {
                var contextStr = jsonObject["context"].ToObject<string>();
                if (!string.IsNullOrEmpty(contextStr))
                {
                    context = CreatePropertyRequest.ContextEnumFromString(contextStr);
                }
            }

            if (jsonObject["is_private"] != null)
            {
                isPrivate = jsonObject["is_private"].ToObject<bool>(serializer);
            }

            if (jsonObject["category_id"] != null)
            {
                categoryId = jsonObject["category_id"].ToObject<string>();
            }

            if (jsonObject["description"] != null)
            {
                description = jsonObject["description"].ToObject<string>();
            }

            return new CreatePropertyRequest(
                name: name, key: key, type: type, context: context, isPrivate: isPrivate, categoryId: categoryId, description: description != null ? new Option<string?>(description) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreatePropertyRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.DescriptionOption.IsSet && value.Description != null)
            {
                writer.WritePropertyName("description");
                serializer.Serialize(writer, value.Description);
            }

            writer.WriteEndObject();
        }
    }
}
