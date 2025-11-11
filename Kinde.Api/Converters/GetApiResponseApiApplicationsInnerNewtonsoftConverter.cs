using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetApiResponseApiApplicationsInner that handles the Option<> structure
    /// </summary>
    public class GetApiResponseApiApplicationsInnerNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetApiResponseApiApplicationsInner>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetApiResponseApiApplicationsInner ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetApiResponseApiApplicationsInner existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            GetApiResponseApiApplicationsInner.TypeEnum? type = default(GetApiResponseApiApplicationsInner.TypeEnum?);
            if (jsonObject["type"] != null)
            {
                var typeStr = jsonObject["type"].ToObject<string>();
                if (!string.IsNullOrEmpty(typeStr))
                {
                    type = GetApiResponseApiApplicationsInner.TypeEnumFromString(typeStr);
                }
            }
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
            bool? isActive = default(bool?);
            if (jsonObject["is_active"] != null)
            {
                isActive = jsonObject["is_active"].ToObject<bool?>(serializer);
            }

            return new GetApiResponseApiApplicationsInner(
                type: type != null ? new Option<GetApiResponseApiApplicationsInner.TypeEnum?>(type) : default,                 id: id != null ? new Option<string?>(id) : default,                 name: name != null ? new Option<string?>(name) : default,                 isActive: isActive != null ? new Option<bool?>(isActive) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetApiResponseApiApplicationsInner value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.TypeOption.IsSet && value.Type != null)
            {
                writer.WritePropertyName("type");
                var typeStr = GetApiResponseApiApplicationsInner.TypeEnumToJsonValue(value.Type.Value);
                writer.WriteValue(typeStr);
            }
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
            if (value.IsActiveOption.IsSet && value.IsActive != null)
            {
                writer.WritePropertyName("is_active");
                serializer.Serialize(writer, value.IsActive);
            }

            writer.WriteEndObject();
        }
    }
}