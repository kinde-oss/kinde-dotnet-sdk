using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for EventType that handles the Option<> structure
    /// </summary>
    public class EventTypeNewtonsoftConverter : Newtonsoft.Json.JsonConverter<EventType>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override EventType ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, EventType existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            string? code = default(string?);
            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string?>();
            }
            string? name = default(string?);
            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string?>();
            }
            string? origin = default(string?);
            if (jsonObject["origin"] != null)
            {
                origin = jsonObject["origin"].ToObject<string?>();
            }
            Object? schema = default(Object?);
            if (jsonObject["schema"] != null)
            {
                schema = jsonObject["schema"].ToObject<Object?>(serializer);
            }

            return new EventType(
                id: id != null ? new Option<string?>(id) : default,                 code: code != null ? new Option<string?>(code) : default,                 name: name != null ? new Option<string?>(name) : default,                 origin: origin != null ? new Option<string?>(origin) : default,                 schema: schema != null ? new Option<Object?>(schema) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, EventType value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IdOption.IsSet && value.Id != null)
            {
                writer.WritePropertyName("id");
                serializer.Serialize(writer, value.Id);
            }
            if (value.CodeOption.IsSet && value.Code != null)
            {
                writer.WritePropertyName("code");
                serializer.Serialize(writer, value.Code);
            }
            if (value.NameOption.IsSet && value.Name != null)
            {
                writer.WritePropertyName("name");
                serializer.Serialize(writer, value.Name);
            }
            if (value.OriginOption.IsSet && value.Origin != null)
            {
                writer.WritePropertyName("origin");
                serializer.Serialize(writer, value.Origin);
            }
            if (value.SchemaOption.IsSet && value.Schema != null)
            {
                writer.WritePropertyName("schema");
                serializer.Serialize(writer, value.Schema);
            }

            writer.WriteEndObject();
        }
    }
}