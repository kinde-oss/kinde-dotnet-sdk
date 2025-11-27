#nullable enable

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kinde.Accounts.Model.Entities;

namespace Kinde.Accounts.Model.Entities
{
    /// <summary>
    /// JSON converter for <see cref="PortalLink" />
    /// </summary>
public class PortalLinkJsonConverter : JsonConverter<PortalLink>
    {
        /// <summary>
        /// Deserializes json to <see cref="PortalLink" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override PortalLink Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            string? url = default;

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
                        case "url":
                            url = utf8JsonReader.GetString();
                            break;
                        default:
                            break;
                    }
                }
            }

            if (url == null)
                throw new ArgumentNullException(nameof(url), "Property is required for class PortalLink.");

            return new PortalLink(url);
        }

        /// <summary>
        /// Serializes a <see cref="PortalLink" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="portalLink"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, PortalLink portalLink, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();
            WriteProperties(ref writer, portalLink, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="PortalLink" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="portalLink"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(ref Utf8JsonWriter writer, PortalLink portalLink, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteString("url", portalLink.Url);
        }
    }
}
