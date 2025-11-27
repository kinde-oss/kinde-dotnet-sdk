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

namespace Kinde.Accounts.Model.Responses
{
    /// <summary>
    /// GetEntitlementsResponseMetadata
    /// </summary>
    public partial class GetEntitlementsResponseMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetEntitlementsResponseMetadata" /> class.
        /// </summary>
        /// <param name="hasMore">Whether more records exist.</param>
        /// <param name="nextPageStartingAfter">The ID of the last record on the current page.</param>
        [JsonConstructor]
        public GetEntitlementsResponseMetadata(bool hasMore, string nextPageStartingAfter)
        {
            HasMore = hasMore;
            NextPageStartingAfter = nextPageStartingAfter;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// Whether more records exist.
        /// </summary>
        /// <value>Whether more records exist.</value>
        /// <example>false</example>
        [JsonPropertyName("has_more")]
        public bool HasMore { get; set; }

        /// <summary>
        /// The ID of the last record on the current page.
        /// </summary>
        /// <value>The ID of the last record on the current page.</value>
        /// <example>entitlement_0195ac80a14e8d71f42b98e75d3c61ad</example>
        [JsonPropertyName("next_page_starting_after")]
        public string NextPageStartingAfter { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetEntitlementsResponseMetadata {\n");
            sb.Append("  HasMore: ").Append(HasMore).Append("\n");
            sb.Append("  NextPageStartingAfter: ").Append(NextPageStartingAfter).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }

    /// <summary>
    /// A Json converter for type <see cref="GetEntitlementsResponseMetadata" />
    /// </summary>
    public class GetEntitlementsResponseMetadataJsonConverter : JsonConverter<GetEntitlementsResponseMetadata>
    {
        /// <summary>
        /// Deserializes json to <see cref="GetEntitlementsResponseMetadata" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override GetEntitlementsResponseMetadata Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            bool? hasMore = default;
            string? nextPageStartingAfter = default;

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
                        case "has_more":
                            if (utf8JsonReader.TokenType != JsonTokenType.Null)
                                hasMore = utf8JsonReader.GetBoolean();
                            break;
                        case "next_page_starting_after":
                            nextPageStartingAfter = utf8JsonReader.GetString();
                            break;
                        default:
                            break;
                    }
                }
            }

            if (hasMore == null)
                throw new ArgumentNullException(nameof(hasMore), "Property is required for class GetEntitlementsResponseMetadata.");

            if (nextPageStartingAfter == null)
                throw new ArgumentNullException(nameof(nextPageStartingAfter), "Property is required for class GetEntitlementsResponseMetadata.");

            return new GetEntitlementsResponseMetadata(hasMore.Value, nextPageStartingAfter);
        }

        /// <summary>
        /// Serializes a <see cref="GetEntitlementsResponseMetadata" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getEntitlementsResponseMetadata"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, GetEntitlementsResponseMetadata getEntitlementsResponseMetadata, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(ref writer, getEntitlementsResponseMetadata, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="GetEntitlementsResponseMetadata" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getEntitlementsResponseMetadata"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(ref Utf8JsonWriter writer, GetEntitlementsResponseMetadata getEntitlementsResponseMetadata, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteBoolean("has_more", getEntitlementsResponseMetadata.HasMore);
            writer.WriteString("next_page_starting_after", getEntitlementsResponseMetadata.NextPageStartingAfter);
        }
    }
}
