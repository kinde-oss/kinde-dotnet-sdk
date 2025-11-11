using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetTimezonesResponseTimezonesInner that handles the Option<> structure
    /// </summary>
    public class GetTimezonesResponseTimezonesInnerNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetTimezonesResponseTimezonesInner>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetTimezonesResponseTimezonesInner ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetTimezonesResponseTimezonesInner existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? key = default(string?);
            if (jsonObject["key"] != null)
            {
                key = jsonObject["key"].ToObject<string?>();
            }
            string? name = default(string?);
            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string?>();
            }

            return new GetTimezonesResponseTimezonesInner(
                key: key != null ? new Option<string?>(key) : default,                 name: name != null ? new Option<string?>(name) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetTimezonesResponseTimezonesInner value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.KeyOption.IsSet && value.Key != null)
            {
                writer.WritePropertyName("key");
                serializer.Serialize(writer, value.Key);
            }
            if (value.NameOption.IsSet && value.Name != null)
            {
                writer.WritePropertyName("name");
                serializer.Serialize(writer, value.Name);
            }

            writer.WriteEndObject();
        }
    }
}