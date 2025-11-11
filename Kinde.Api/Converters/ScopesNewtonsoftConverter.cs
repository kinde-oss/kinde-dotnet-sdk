using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for Scopes that handles the Option<> structure
    /// </summary>
    public class ScopesNewtonsoftConverter : Newtonsoft.Json.JsonConverter<Scopes>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override Scopes ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, Scopes existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            string? apiId = default(string?);
            if (jsonObject["api_id"] != null)
            {
                apiId = jsonObject["api_id"].ToObject<string?>();
            }

            return new Scopes(
                id: id != null ? new Option<string?>(id) : default,                 key: key != null ? new Option<string?>(key) : default,                 description: description != null ? new Option<string?>(description) : default,                 apiId: apiId != null ? new Option<string?>(apiId) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, Scopes value, Newtonsoft.Json.JsonSerializer serializer)
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
            if (value.ApiIdOption.IsSet && value.ApiId != null)
            {
                writer.WritePropertyName("api_id");
                serializer.Serialize(writer, value.ApiId);
            }

            writer.WriteEndObject();
        }
    }
}