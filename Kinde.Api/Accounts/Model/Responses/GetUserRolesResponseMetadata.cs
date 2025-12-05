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
    /// GetUserRolesResponseMetadata
    /// </summary>
    public partial class GetUserRolesResponseMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserRolesResponseMetadata" /> class.
        /// </summary>
        /// <param name="hasMore">Whether more records exist.</param>
        /// <param name="nextPageStartingAfter">The ID of the last record on the current page.</param>
        [JsonConstructor]
        public GetUserRolesResponseMetadata(bool hasMore, string nextPageStartingAfter)
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
        /// <example>role_0195ac80a14e8d71f42b98e75d3c61ad</example>
        [JsonPropertyName("next_page_starting_after")]
        public string NextPageStartingAfter { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetUserRolesResponseMetadata {\n");
            sb.Append("  HasMore: ").Append(HasMore).Append("\n");
            sb.Append("  NextPageStartingAfter: ").Append(NextPageStartingAfter).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }

    /// <summary>
    /// A Json converter for type <see cref="GetUserRolesResponseMetadata" />
    /// </summary>
    public class GetUserRolesResponseMetadataJsonConverter : JsonConverter<GetUserRolesResponseMetadata>
    {
        /// <summary>
        /// Deserializes json to <see cref="GetUserRolesResponseMetadata" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override GetUserRolesResponseMetadata Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
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
                throw new ArgumentNullException(nameof(hasMore), "Property is required for class GetUserRolesResponseMetadata.");

            if (nextPageStartingAfter == null)
                throw new ArgumentNullException(nameof(nextPageStartingAfter), "Property is required for class GetUserRolesResponseMetadata.");

            return new GetUserRolesResponseMetadata(hasMore.Value, nextPageStartingAfter);
        }

        /// <summary>
        /// Serializes a <see cref="GetUserRolesResponseMetadata" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getUserRolesResponseMetadata"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, GetUserRolesResponseMetadata getUserRolesResponseMetadata, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(ref writer, getUserRolesResponseMetadata, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="GetUserRolesResponseMetadata" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getUserRolesResponseMetadata"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(ref Utf8JsonWriter writer, GetUserRolesResponseMetadata getUserRolesResponseMetadata, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteBoolean("has_more", getUserRolesResponseMetadata.HasMore);
            writer.WriteString("next_page_starting_after", getUserRolesResponseMetadata.NextPageStartingAfter);
        }
    }
}
