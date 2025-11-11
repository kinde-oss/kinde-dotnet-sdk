using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for Category that handles the Option<> structure
    /// </summary>
    public class CategoryNewtonsoftConverter : Newtonsoft.Json.JsonConverter<Category>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override Category ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, Category existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? id = default(string?);
            if (jsonObject["id"] != null)
            {
                id = jsonObject["id"].ToObject<string?>();
            }
            string? name = default(string?);
            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string?>();
            }

            return new Category(
                id: id != null ? new Option<string?>(id) : default,                 name: name != null ? new Option<string?>(name) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, Category value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IdOption.IsSet && value.Id != null)
            {
                writer.WritePropertyName("id");
                serializer.Serialize(writer, value.Id);
            }
            if (value.NameOption.IsSet && value.Name != null)
            {
                writer.WritePropertyName("name");
                serializer.Serialize(writer, value.Name);
            }

            writer.WriteEndObject();
        }
    }
}