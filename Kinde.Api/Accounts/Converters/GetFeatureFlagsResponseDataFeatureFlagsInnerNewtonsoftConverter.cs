using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Accounts.Model;
using Kinde.Accounts.Client;

namespace Kinde.Api.Accounts.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetFeatureFlagsResponseDataFeatureFlagsInner that handles the Option<> structure
    /// </summary>
    public class GetFeatureFlagsResponseDataFeatureFlagsInnerNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetFeatureFlagsResponseDataFeatureFlagsInner>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetFeatureFlagsResponseDataFeatureFlagsInner ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetFeatureFlagsResponseDataFeatureFlagsInner existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            string? key = default(string?);
            if (jsonObject["key"] != null)
            {
                key = jsonObject["key"].ToObject<string?>();
            }
            string? type = default(string?);
            if (jsonObject["type"] != null)
            {
                type = jsonObject["type"].ToObject<string?>();
            }
            GetFeatureFlagsResponseDataFeatureFlagsInnerValue? value = default(GetFeatureFlagsResponseDataFeatureFlagsInnerValue?);
            if (jsonObject["value"] != null)
            {
                value = jsonObject["value"].ToObject<GetFeatureFlagsResponseDataFeatureFlagsInnerValue?>(serializer);
            }

            return new GetFeatureFlagsResponseDataFeatureFlagsInner(
                id: id != null ? new Option<string?>(id) : default,                 name: name != null ? new Option<string?>(name) : default,                 key: key != null ? new Option<string?>(key) : default,                 type: type != null ? new Option<string?>(type) : default,                 value: value != null ? new Option<GetFeatureFlagsResponseDataFeatureFlagsInnerValue?>(value) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetFeatureFlagsResponseDataFeatureFlagsInner value, Newtonsoft.Json.JsonSerializer serializer)
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
            if (value.KeyOption.IsSet && value.Key != null)
            {
                writer.WritePropertyName("key");
                serializer.Serialize(writer, value.Key);
            }
            if (value.TypeOption.IsSet && value.Type != null)
            {
                writer.WritePropertyName("type");
                serializer.Serialize(writer, value.Type);
            }
            if (value.ValueOption.IsSet && value.Value != null)
            {
                writer.WritePropertyName("value");
                serializer.Serialize(writer, value.Value);
            }

            writer.WriteEndObject();
        }
    }
}