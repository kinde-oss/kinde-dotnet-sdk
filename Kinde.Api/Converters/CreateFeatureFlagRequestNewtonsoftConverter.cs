using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateFeatureFlagRequest that handles the Option<> structure
    /// </summary>
    public class CreateFeatureFlagRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateFeatureFlagRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateFeatureFlagRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateFeatureFlagRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string name = default(string);
            string key = default(string);
            CreateFeatureFlagRequest.TypeEnum type = default(CreateFeatureFlagRequest.TypeEnum);
            string defaultValue = default(string);
            CreateFeatureFlagRequest.AllowOverrideLevelEnum? allowOverrideLevel = null;
            string? description = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string>();
            }

            if (jsonObject["key"] != null)
            {
                key = jsonObject["key"].ToObject<string>();
            }

            if (jsonObject["type"] != null)
            {
                var typeStr = jsonObject["type"].ToObject<string>();
                if (!string.IsNullOrEmpty(typeStr))
                {
                    type = CreateFeatureFlagRequest.TypeEnumFromString(typeStr);
                }
            }

            if (jsonObject["default_value"] != null)
            {
                defaultValue = jsonObject["default_value"].ToObject<string>();
            }

            if (jsonObject["allow_override_level"] != null)
            {
                var allowOverrideLevelStr = jsonObject["allow_override_level"].ToObject<string>();
                if (!string.IsNullOrEmpty(allowOverrideLevelStr))
                {
                    allowOverrideLevel = CreateFeatureFlagRequest.AllowOverrideLevelEnumFromString(allowOverrideLevelStr);
                }
            }

            if (jsonObject["description"] != null)
            {
                description = jsonObject["description"].ToObject<string>();
            }

            return new CreateFeatureFlagRequest(
                name: name, key: key, type: type, defaultValue: defaultValue, allowOverrideLevel: allowOverrideLevel != null ? new Option<CreateFeatureFlagRequest.AllowOverrideLevelEnum?>(allowOverrideLevel) : default, description: description != null ? new Option<string?>(description) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateFeatureFlagRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.AllowOverrideLevelOption.IsSet && value.AllowOverrideLevel != null)
            {
                writer.WritePropertyName("allow_override_level");
                var allowoverridelevelStr = CreateFeatureFlagRequest.AllowOverrideLevelEnumToJsonValue(value.AllowOverrideLevel.Value);
                writer.WriteValue(allowoverridelevelStr);
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
