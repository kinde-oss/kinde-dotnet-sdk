using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UpdatePropertyRequest that handles the Option<> structure
    /// </summary>
    public class UpdatePropertyRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UpdatePropertyRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UpdatePropertyRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UpdatePropertyRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? description = default(string?);
            if (jsonObject["description"] != null)
            {
                description = jsonObject["description"].ToObject<string?>();
            }
            string name = default(string);
            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string>();
            }
            bool isPrivate = default(bool);
            if (jsonObject["is_private"] != null)
            {
                isPrivate = jsonObject["is_private"].ToObject<bool>(serializer);
            }
            string categoryId = default(string);
            if (jsonObject["category_id"] != null)
            {
                categoryId = jsonObject["category_id"].ToObject<string>();
            }

            return new UpdatePropertyRequest(
                description: description != null ? new Option<string?>(description) : default,                 name: name,                 isPrivate: isPrivate,                 categoryId: categoryId            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UpdatePropertyRequest value, Newtonsoft.Json.JsonSerializer serializer)
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