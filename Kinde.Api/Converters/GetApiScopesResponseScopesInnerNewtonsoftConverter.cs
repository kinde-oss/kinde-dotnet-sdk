using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetApiScopesResponseScopesInner that handles the Option<> structure
    /// </summary>
    public class GetApiScopesResponseScopesInnerNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetApiScopesResponseScopesInner>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetApiScopesResponseScopesInner ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetApiScopesResponseScopesInner existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            string? description = default(string?);
            if (jsonObject["description"] != null)
            {
                description = jsonObject["description"].ToObject<string?>();
            }

            return new GetApiScopesResponseScopesInner(
                id: id != null ? new Option<string?>(id) : default,                 key: key != null ? new Option<string?>(key) : default,                 description: description != null ? new Option<string?>(description) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetApiScopesResponseScopesInner value, Newtonsoft.Json.JsonSerializer serializer)
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
            if (value.DescriptionOption.IsSet && value.Description != null)
            {
                writer.WritePropertyName("description");
                serializer.Serialize(writer, value.Description);
            }

            writer.WriteEndObject();
        }
    }
}