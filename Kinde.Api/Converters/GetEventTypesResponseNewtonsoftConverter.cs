using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetEventTypesResponse that handles the Option<> structure
    /// </summary>
    public class GetEventTypesResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetEventTypesResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetEventTypesResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetEventTypesResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? code = null;
            string? message = null;
            List<EventType> eventTypes = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string>();
            }

            if (jsonObject["event_types"] != null)
            {
                eventTypes = jsonObject["event_types"].ToObject<List<EventType>>(serializer);
            }

            return new GetEventTypesResponse(
                code: code != null ? new Option<string?>(code) : default, message: message != null ? new Option<string?>(message) : default, eventTypes: eventTypes != null ? new Option<List<EventType>?>(eventTypes) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetEventTypesResponse value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.CodeOption.IsSet && value.Code != null)
            {
                writer.WritePropertyName("code");
                serializer.Serialize(writer, value.Code);
            }

            if (value.MessageOption.IsSet && value.Message != null)
            {
                writer.WritePropertyName("message");
                serializer.Serialize(writer, value.Message);
            }

            if (value.EventTypesOption.IsSet && value.EventTypes != null)
            {
                writer.WritePropertyName("event_types");
                serializer.Serialize(writer, value.EventTypes);
            }

            writer.WriteEndObject();
        }
    }
}
