using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateWebHookRequest that handles the Option<> structure
    /// </summary>
    public class CreateWebHookRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateWebHookRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateWebHookRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateWebHookRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string endpoint = default(string);
            List<string> eventTypes = default(List<string>);
            string name = default(string);
            string? description = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["endpoint"] != null)
            {
                endpoint = jsonObject["endpoint"].ToObject<string>();
            }

            if (jsonObject["event_types"] != null)
            {
                eventTypes = jsonObject["event_types"].ToObject<List<string>>(serializer);
            }

            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string>();
            }

            if (jsonObject["description"] != null)
            {
                description = jsonObject["description"].ToObject<string>();
            }

            return new CreateWebHookRequest(
                endpoint: endpoint, eventTypes: eventTypes, name: name, description: description != null ? new Option<string?>(description) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateWebHookRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.DescriptionOption.IsSet && value.Description != null)
            {
                writer.WritePropertyName("description");
                serializer.Serialize(writer, value.Description);
            }

            writer.WriteEndObject();
        }
    }
}
