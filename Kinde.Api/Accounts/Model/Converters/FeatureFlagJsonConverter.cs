#nullable enable

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kinde.Accounts.Model.Entities;

namespace Kinde.Accounts.Model.Entities
{
    /// <summary>
    /// JSON converter for <see cref="FeatureFlag" />
    /// </summary>
public class FeatureFlagJsonConverter : JsonConverter<FeatureFlag>
    {
        /// <summary>
        /// Deserializes json to <see cref="FeatureFlag" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override FeatureFlag Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            string? id = default;
            string? key = default;
            string? name = default;
            string? type = default;
            FeatureFlagValue? value = default;

            while (utf8JsonReader.Read())
            {
                if (startingTokenType == JsonTokenType.StartObject && utf8JsonReader.TokenType == JsonTokenType.EndObject && currentDepth == utf8JsonReader.CurrentDepth)
                    break;

                if (startingTokenType == JsonTokenType.StartArray && utf8JsonReader.TokenType == JsonTokenType.EndArray && currentDepth == utf8JsonReader.CurrentDepth)
                    break;

                if (utf8JsonReader.TokenType == JsonTokenType.PropertyName && currentDepth == utf8JsonReader.CurrentDepth - 1)
                {
                    string? localVarJsonPropertyName = utf8JsonReader.GetString();
                    utf8JsonReader.Read();

                    switch (localVarJsonPropertyName)
                    {
                        case "id":
                            id = utf8JsonReader.GetString();
                            break;
                        case "key":
                            key = utf8JsonReader.GetString();
                            break;
                        case "name":
                            name = utf8JsonReader.GetString();
                            break;
                        case "type":
                            type = utf8JsonReader.GetString();
                            break;
                        case "value":
                            if (utf8JsonReader.TokenType != JsonTokenType.Null)
                                value = JsonSerializer.Deserialize<FeatureFlagValue>(ref utf8JsonReader, jsonSerializerOptions);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (id == null)
                throw new ArgumentNullException(nameof(id), "Property is required for class FeatureFlag.");

            if (key == null)
                throw new ArgumentNullException(nameof(key), "Property is required for class FeatureFlag.");

            if (name == null)
                throw new ArgumentNullException(nameof(name), "Property is required for class FeatureFlag.");

            if (type == null)
                throw new ArgumentNullException(nameof(type), "Property is required for class FeatureFlag.");

            if (value == null)
                throw new ArgumentNullException(nameof(value), "Property is required for class FeatureFlag.");

            return new FeatureFlag(id, key, name, type, value);
        }

        /// <summary>
        /// Serializes a <see cref="FeatureFlag" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="featureFlag"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, FeatureFlag featureFlag, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();
            WriteProperties(ref writer, featureFlag, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="FeatureFlag" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="featureFlag"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(ref Utf8JsonWriter writer, FeatureFlag featureFlag, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteString("id", featureFlag.Id);
            writer.WriteString("key", featureFlag.Key);
            writer.WriteString("name", featureFlag.Name);
            writer.WriteString("type", featureFlag.Type);
            writer.WritePropertyName("value");
            JsonSerializer.Serialize(writer, featureFlag.Value, jsonSerializerOptions);
        }
    }
}
