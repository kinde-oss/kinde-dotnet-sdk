using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateWebhookResponseWebhook that handles the Option<> structure
    /// </summary>
    public class CreateWebhookResponseWebhookNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateWebhookResponseWebhook>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateWebhookResponseWebhook ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateWebhookResponseWebhook existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            string? endpoint = default(string?);
            if (jsonObject["endpoint"] != null)
            {
                endpoint = jsonObject["endpoint"].ToObject<string?>();
            }

            return new CreateWebhookResponseWebhook(
                id: id != null ? new Option<string?>(id) : default,                 endpoint: endpoint != null ? new Option<string?>(endpoint) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateWebhookResponseWebhook value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IdOption.IsSet && value.Id != null)
            {
                writer.WritePropertyName("id");
                serializer.Serialize(writer, value.Id);
            }
            if (value.EndpointOption.IsSet && value.Endpoint != null)
            {
                writer.WritePropertyName("endpoint");
                serializer.Serialize(writer, value.Endpoint);
            }

            writer.WriteEndObject();
        }
    }
}