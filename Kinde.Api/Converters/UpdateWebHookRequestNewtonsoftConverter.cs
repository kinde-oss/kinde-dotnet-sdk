using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UpdateWebHookRequest that handles the Option<> structure
    /// </summary>
    public class UpdateWebHookRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UpdateWebHookRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UpdateWebHookRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UpdateWebHookRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            List<string> eventTypes = default(List<string>);
            if (jsonObject["event_types"] != null)
            {
                eventTypes = jsonObject["event_types"].ToObject<List<string>>(serializer);
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

            return new UpdateWebHookRequest(
                eventTypes: eventTypes != null ? new Option<List<string>?>(eventTypes) : default,                 name: name != null ? new Option<string?>(name) : default,                 description: description != null ? new Option<string?>(description) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UpdateWebHookRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.EventTypesOption.IsSet)
            {
                writer.WritePropertyName("event_types");
                serializer.Serialize(writer, value.EventTypes);
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

            writer.WriteEndObject();
        }
    }
}