#nullable enable

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kinde.Accounts.Model.Entities;

namespace Kinde.Accounts.Model.Entities
{
    /// <summary>
    /// JSON converter for <see cref="UserProperty" />
    /// </summary>
public class UserPropertyJsonConverter : JsonConverter<UserProperty>
    {
        /// <summary>
        /// Deserializes json to <see cref="UserProperty" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override UserProperty Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            string? id = default;
            string? key = default;
            string? name = default;
            UserPropertyValue? value = default;

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
                        case "value":
                            if (utf8JsonReader.TokenType != JsonTokenType.Null)
                                value = JsonSerializer.Deserialize<UserPropertyValue>(ref utf8JsonReader, jsonSerializerOptions);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (id == null)
                throw new ArgumentNullException(nameof(id), "Property is required for class UserProperty.");

            if (key == null)
                throw new ArgumentNullException(nameof(key), "Property is required for class UserProperty.");

            if (name == null)
                throw new ArgumentNullException(nameof(name), "Property is required for class UserProperty.");

            if (value == null)
                throw new ArgumentNullException(nameof(value), "Property is required for class UserProperty.");

            return new UserProperty(id, key, name, value);
        }

        /// <summary>
        /// Serializes a <see cref="UserProperty" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="userProperty"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, UserProperty userProperty, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();
            WriteProperties(ref writer, userProperty, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="UserProperty" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="userProperty"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(ref Utf8JsonWriter writer, UserProperty userProperty, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteString("id", userProperty.Id);
            writer.WriteString("key", userProperty.Key);
            writer.WriteString("name", userProperty.Name);
            writer.WritePropertyName("value");
            JsonSerializer.Serialize(writer, userProperty.Value, jsonSerializerOptions);
        }
    }
}
