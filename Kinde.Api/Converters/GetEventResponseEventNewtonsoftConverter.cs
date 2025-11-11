using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetEventResponseEvent that handles the Option<> structure
    /// </summary>
    public class GetEventResponseEventNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetEventResponseEvent>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetEventResponseEvent ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetEventResponseEvent existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? type = default(string?);
            if (jsonObject["type"] != null)
            {
                type = jsonObject["type"].ToObject<string?>();
            }
            string? source = default(string?);
            if (jsonObject["source"] != null)
            {
                source = jsonObject["source"].ToObject<string?>();
            }
            string? eventId = default(string?);
            if (jsonObject["event_id"] != null)
            {
                eventId = jsonObject["event_id"].ToObject<string?>();
            }
            int? timestamp = default(int?);
            if (jsonObject["timestamp"] != null)
            {
                timestamp = jsonObject["timestamp"].ToObject<int?>(serializer);
            }
            Object? data = default(Object?);
            if (jsonObject["data"] != null)
            {
                data = jsonObject["data"].ToObject<Object?>(serializer);
            }

            return new GetEventResponseEvent(
                type: type != null ? new Option<string?>(type) : default,                 source: source != null ? new Option<string?>(source) : default,                 eventId: eventId != null ? new Option<string?>(eventId) : default,                 timestamp: timestamp != null ? new Option<int?>(timestamp) : default,                 data: data != null ? new Option<Object?>(data) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetEventResponseEvent value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.TypeOption.IsSet && value.Type != null)
            {
                writer.WritePropertyName("type");
                serializer.Serialize(writer, value.Type);
            }
            if (value.SourceOption.IsSet && value.Source != null)
            {
                writer.WritePropertyName("source");
                serializer.Serialize(writer, value.Source);
            }
            if (value.EventIdOption.IsSet && value.EventId != null)
            {
                writer.WritePropertyName("event_id");
                serializer.Serialize(writer, value.EventId);
            }
            if (value.TimestampOption.IsSet && value.Timestamp != null)
            {
                writer.WritePropertyName("timestamp");
                serializer.Serialize(writer, value.Timestamp);
            }
            if (value.DataOption.IsSet && value.Data != null)
            {
                writer.WritePropertyName("data");
                serializer.Serialize(writer, value.Data);
            }

            writer.WriteEndObject();
        }
    }
}