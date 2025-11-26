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
    /// GetUserPermissionsResponse
    /// </summary>
    public partial class GetUserPermissionsResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserPermissionsResponse" /> class.
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="metadata">metadata</param>
        [JsonConstructor]
        public GetUserPermissionsResponse(GetUserPermissionsResponseData data, GetUserPermissionsResponseMetadata metadata)
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
        public GetUserPermissionsResponseData Data { get; set; }

        /// <summary>
        /// Gets or Sets Metadata
        /// </summary>
        [JsonPropertyName("metadata")]
        public GetUserPermissionsResponseMetadata Metadata { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetUserPermissionsResponse {\n");
            sb.Append("  Data: ").Append(Data).Append("\n");
            sb.Append("  Metadata: ").Append(Metadata).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }

    /// <summary>
    /// A Json converter for type <see cref="GetUserPermissionsResponse" />
    /// </summary>
    public class GetUserPermissionsResponseJsonConverter : JsonConverter<GetUserPermissionsResponse>
    {
        /// <summary>
        /// Deserializes json to <see cref="GetUserPermissionsResponse" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override GetUserPermissionsResponse Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            GetUserPermissionsResponseData? data = default;
            GetUserPermissionsResponseMetadata? metadata = default;

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
                                data = JsonSerializer.Deserialize<GetUserPermissionsResponseData>(ref utf8JsonReader, jsonSerializerOptions);
                            break;
                        case "metadata":
                            if (utf8JsonReader.TokenType != JsonTokenType.Null)
                                metadata = JsonSerializer.Deserialize<GetUserPermissionsResponseMetadata>(ref utf8JsonReader, jsonSerializerOptions);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (data == null)
                throw new ArgumentNullException(nameof(data), "Property is required for class GetUserPermissionsResponse.");

            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata), "Property is required for class GetUserPermissionsResponse.");

            return new GetUserPermissionsResponse(data, metadata);
        }

        /// <summary>
        /// Serializes a <see cref="GetUserPermissionsResponse" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getUserPermissionsResponse"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, GetUserPermissionsResponse getUserPermissionsResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(ref writer, getUserPermissionsResponse, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="GetUserPermissionsResponse" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getUserPermissionsResponse"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(ref Utf8JsonWriter writer, GetUserPermissionsResponse getUserPermissionsResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WritePropertyName("data");
            JsonSerializer.Serialize(writer, getUserPermissionsResponse.Data, jsonSerializerOptions);
            writer.WritePropertyName("metadata");
            JsonSerializer.Serialize(writer, getUserPermissionsResponse.Metadata, jsonSerializerOptions);
        }
    }
}
