#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kinde.Accounts.Model.Entities;

namespace Kinde.Accounts.Model.Responses
{
    /// <summary>
    /// GetUserPropertiesResponseData
    /// </summary>
    public partial class GetUserPropertiesResponseData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserPropertiesResponseData" /> class.
        /// </summary>
        /// <param name="properties">A list of properties</param>
        [JsonConstructor]
        public GetUserPropertiesResponseData(List<UserProperty> properties)
        {
            Properties = properties;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// A list of properties
        /// </summary>
        /// <value>A list of properties</value>
        [JsonPropertyName("properties")]
        public List<UserProperty> Properties { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetUserPropertiesResponseData {\n");
            sb.Append("  Properties: ").Append(Properties).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }

    /// <summary>
    /// A Json converter for type <see cref="GetUserPropertiesResponseData" />
    /// </summary>
    public class GetUserPropertiesResponseDataJsonConverter : JsonConverter<GetUserPropertiesResponseData>
    {
        /// <summary>
        /// Deserializes json to <see cref="GetUserPropertiesResponseData" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override GetUserPropertiesResponseData Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            List<UserProperty>? properties = default;

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
                        case "properties":
                            if (utf8JsonReader.TokenType != JsonTokenType.Null)
                                properties = JsonSerializer.Deserialize<List<UserProperty>>(ref utf8JsonReader, jsonSerializerOptions);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (properties == null)
                throw new ArgumentNullException(nameof(properties), "Property is required for class GetUserPropertiesResponseData.");

            return new GetUserPropertiesResponseData(properties);
        }

        /// <summary>
        /// Serializes a <see cref="GetUserPropertiesResponseData" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getUserPropertiesResponseData"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, GetUserPropertiesResponseData getUserPropertiesResponseData, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(ref writer, getUserPropertiesResponseData, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="GetUserPropertiesResponseData" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getUserPropertiesResponseData"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(ref Utf8JsonWriter writer, GetUserPropertiesResponseData getUserPropertiesResponseData, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WritePropertyName("properties");
            JsonSerializer.Serialize(writer, getUserPropertiesResponseData.Properties, jsonSerializerOptions);
        }
    }
}
