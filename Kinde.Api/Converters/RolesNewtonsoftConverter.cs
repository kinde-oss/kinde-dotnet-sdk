using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for Roles that handles the Option<> structure
    /// </summary>
    public class RolesNewtonsoftConverter : Newtonsoft.Json.JsonConverter<Roles>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override Roles ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, Roles existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            string? description = default(string?);
            if (jsonObject["description"] != null)
            {
                description = jsonObject["description"].ToObject<string?>();
            }
            bool? isDefaultRole = default(bool?);
            if (jsonObject["is_default_role"] != null)
            {
                isDefaultRole = jsonObject["is_default_role"].ToObject<bool?>(serializer);
            }

            return new Roles(
                id: id != null ? new Option<string?>(id) : default,                 key: key != null ? new Option<string?>(key) : default,                 name: name != null ? new Option<string?>(name) : default,                 description: description != null ? new Option<string?>(description) : default,                 isDefaultRole: isDefaultRole != null ? new Option<bool?>(isDefaultRole) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, Roles value, Newtonsoft.Json.JsonSerializer serializer)
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
            if (value.DescriptionOption.IsSet && value.Description != null)
            {
                writer.WritePropertyName("description");
                serializer.Serialize(writer, value.Description);
            }
            if (value.IsDefaultRoleOption.IsSet && value.IsDefaultRole != null)
            {
                writer.WritePropertyName("is_default_role");
                serializer.Serialize(writer, value.IsDefaultRole);
            }

            writer.WriteEndObject();
        }
    }
}