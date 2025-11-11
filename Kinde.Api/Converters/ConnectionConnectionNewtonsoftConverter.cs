using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for ConnectionConnection that handles the Option<> structure
    /// </summary>
    public class ConnectionConnectionNewtonsoftConverter : Newtonsoft.Json.JsonConverter<ConnectionConnection>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override ConnectionConnection ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, ConnectionConnection existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            string? displayName = default(string?);
            if (jsonObject["display_name"] != null)
            {
                displayName = jsonObject["display_name"].ToObject<string?>();
            }
            string? strategy = default(string?);
            if (jsonObject["strategy"] != null)
            {
                strategy = jsonObject["strategy"].ToObject<string?>();
            }

            return new ConnectionConnection(
                id: id != null ? new Option<string?>(id) : default,                 name: name != null ? new Option<string?>(name) : default,                 displayName: displayName != null ? new Option<string?>(displayName) : default,                 strategy: strategy != null ? new Option<string?>(strategy) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, ConnectionConnection value, Newtonsoft.Json.JsonSerializer serializer)
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
            if (value.DisplayNameOption.IsSet && value.DisplayName != null)
            {
                writer.WritePropertyName("display_name");
                serializer.Serialize(writer, value.DisplayName);
            }
            if (value.StrategyOption.IsSet && value.Strategy != null)
            {
                writer.WritePropertyName("strategy");
                serializer.Serialize(writer, value.Strategy);
            }

            writer.WriteEndObject();
        }
    }
}