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
    /// GetUserPropertiesResponse
    /// </summary>
    public partial class GetUserPropertiesResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserPropertiesResponse" /> class.
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="metadata">metadata</param>
        [JsonConstructor]
        public GetUserPropertiesResponse(GetUserPropertiesResponseData data, GetUserPropertiesResponseMetadata metadata)
        {
            Data = data;
            Metadata = metadata;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// Gets or Sets Data
        /// </summary>
        [JsonPropertyName("data")]
        public GetUserPropertiesResponseData Data { get; set; }

        /// <summary>
        /// Gets or Sets Metadata
        /// </summary>
        [JsonPropertyName("metadata")]
        public GetUserPropertiesResponseMetadata Metadata { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetUserPropertiesResponse {\n");
            sb.Append("  Data: ").Append(Data).Append("\n");
            sb.Append("  Metadata: ").Append(Metadata).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }

    /// <summary>
    /// A Json converter for type <see cref="GetUserPropertiesResponse" />
    /// </summary>
    public class GetUserPropertiesResponseJsonConverter : JsonConverter<GetUserPropertiesResponse>
    {
        /// <summary>
        /// Deserializes json to <see cref="GetUserPropertiesResponse" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override GetUserPropertiesResponse Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            GetUserPropertiesResponseData? data = default;
            GetUserPropertiesResponseMetadata? metadata = default;

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
                        case "data":
                            if (utf8JsonReader.TokenType != JsonTokenType.Null)
                                data = JsonSerializer.Deserialize<GetUserPropertiesResponseData>(ref utf8JsonReader, jsonSerializerOptions);
                            break;
                        case "metadata":
                            if (utf8JsonReader.TokenType != JsonTokenType.Null)
                                metadata = JsonSerializer.Deserialize<GetUserPropertiesResponseMetadata>(ref utf8JsonReader, jsonSerializerOptions);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (data == null)
                throw new ArgumentNullException(nameof(data), "Property is required for class GetUserPropertiesResponse.");

            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata), "Property is required for class GetUserPropertiesResponse.");

            return new GetUserPropertiesResponse(data, metadata);
        }

        /// <summary>
        /// Serializes a <see cref="GetUserPropertiesResponse" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getUserPropertiesResponse"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, GetUserPropertiesResponse getUserPropertiesResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(ref writer, getUserPropertiesResponse, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="GetUserPropertiesResponse" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getUserPropertiesResponse"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(ref Utf8JsonWriter writer, GetUserPropertiesResponse getUserPropertiesResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WritePropertyName("data");
            JsonSerializer.Serialize(writer, getUserPropertiesResponse.Data, jsonSerializerOptions);
            writer.WritePropertyName("metadata");
            JsonSerializer.Serialize(writer, getUserPropertiesResponse.Metadata, jsonSerializerOptions);
        }
    }
}
