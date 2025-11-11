using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for Webhook that handles the Option<> structure
    /// </summary>
    public class WebhookNewtonsoftConverter : Newtonsoft.Json.JsonConverter<Webhook>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override Webhook ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, Webhook existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            string? endpoint = default(string?);
            if (jsonObject["endpoint"] != null)
            {
                endpoint = jsonObject["endpoint"].ToObject<string?>();
            }
            string? description = default(string?);
            if (jsonObject["description"] != null)
            {
                description = jsonObject["description"].ToObject<string?>();
            }
            List<string> eventTypes = default(List<string>);
            if (jsonObject["event_types"] != null)
            {
                eventTypes = jsonObject["event_types"].ToObject<List<string>>(serializer);
            }
            string? createdOn = default(string?);
            if (jsonObject["created_on"] != null)
            {
                createdOn = jsonObject["created_on"].ToObject<string?>();
            }

            return new Webhook(
                id: id != null ? new Option<string?>(id) : default,                 name: name != null ? new Option<string?>(name) : default,                 endpoint: endpoint != null ? new Option<string?>(endpoint) : default,                 description: description != null ? new Option<string?>(description) : default,                 eventTypes: eventTypes != null ? new Option<List<string>?>(eventTypes) : default,                 createdOn: createdOn != null ? new Option<string?>(createdOn) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, Webhook value, Newtonsoft.Json.JsonSerializer serializer)
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
            if (value.EndpointOption.IsSet && value.Endpoint != null)
            {
                writer.WritePropertyName("endpoint");
                serializer.Serialize(writer, value.Endpoint);
            }
            if (value.DescriptionOption.IsSet && value.Description != null)
            {
                writer.WritePropertyName("description");
                serializer.Serialize(writer, value.Description);
            }
            if (value.EventTypesOption.IsSet)
            {
                writer.WritePropertyName("event_types");
                serializer.Serialize(writer, value.EventTypes);
            }
            if (value.CreatedOnOption.IsSet && value.CreatedOn != null)
            {
                writer.WritePropertyName("created_on");
                serializer.Serialize(writer, value.CreatedOn);
            }

            writer.WriteEndObject();
        }
    }
}