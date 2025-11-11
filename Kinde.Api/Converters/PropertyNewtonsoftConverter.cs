using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for Property that handles the Option<> structure
    /// </summary>
    public class PropertyNewtonsoftConverter : Newtonsoft.Json.JsonConverter<Property>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override Property ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, Property existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            string? key = default(string?);
            if (jsonObject["key"] != null)
            {
                key = jsonObject["key"].ToObject<string?>();
            }
            string? name = default(string?);
            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string?>();
            }
            bool? isPrivate = default(bool?);
            if (jsonObject["is_private"] != null)
            {
                isPrivate = jsonObject["is_private"].ToObject<bool?>(serializer);
            }
            string? description = default(string?);
            if (jsonObject["description"] != null)
            {
                description = jsonObject["description"].ToObject<string?>();
            }
            bool? isKindeProperty = default(bool?);
            if (jsonObject["is_kinde_property"] != null)
            {
                isKindeProperty = jsonObject["is_kinde_property"].ToObject<bool?>(serializer);
            }

            return new Property(
                id: id != null ? new Option<string?>(id) : default,                 key: key != null ? new Option<string?>(key) : default,                 name: name != null ? new Option<string?>(name) : default,                 isPrivate: isPrivate != null ? new Option<bool?>(isPrivate) : default,                 description: description != null ? new Option<string?>(description) : default,                 isKindeProperty: isKindeProperty != null ? new Option<bool?>(isKindeProperty) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, Property value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IdOption.IsSet && value.Id != null)
            {
                writer.WritePropertyName("id");
                serializer.Serialize(writer, value.Id);
            }
            if (value.KeyOption.IsSet && value.Key != null)
            {
                writer.WritePropertyName("key");
                serializer.Serialize(writer, value.Key);
            }
            if (value.NameOption.IsSet && value.Name != null)
            {
                writer.WritePropertyName("name");
                serializer.Serialize(writer, value.Name);
            }
            if (value.IsPrivateOption.IsSet && value.IsPrivate != null)
            {
                writer.WritePropertyName("is_private");
                serializer.Serialize(writer, value.IsPrivate);
            }
            if (value.DescriptionOption.IsSet && value.Description != null)
            {
                writer.WritePropertyName("description");
                serializer.Serialize(writer, value.Description);
            }
            if (value.IsKindePropertyOption.IsSet && value.IsKindeProperty != null)
            {
                writer.WritePropertyName("is_kinde_property");
                serializer.Serialize(writer, value.IsKindeProperty);
            }

            writer.WriteEndObject();
        }
    }
}