using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for AddAPIScopeRequest that handles the Option<> structure
    /// </summary>
    public class AddAPIScopeRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<AddAPIScopeRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override AddAPIScopeRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, AddAPIScopeRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string key = default(string);
            string? description = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["key"] != null)
            {
                key = jsonObject["key"].ToObject<string>();
            }

            if (jsonObject["description"] != null)
            {
                description = jsonObject["description"].ToObject<string>();
            }

            return new AddAPIScopeRequest(
                key: key, description: description != null ? new Option<string?>(description) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, AddAPIScopeRequest value, Newtonsoft.Json.JsonSerializer serializer)
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
