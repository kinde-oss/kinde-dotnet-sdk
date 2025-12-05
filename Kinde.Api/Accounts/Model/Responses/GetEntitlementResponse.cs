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
    /// GetEntitlementResponse
    /// </summary>
    public partial class GetEntitlementResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetEntitlementResponse" /> class.
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="metadata">metadata</param>
        [JsonConstructor]
        public GetEntitlementResponse(GetEntitlementResponseData data, Object metadata)
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
        public GetEntitlementResponseData Data { get; set; }

        /// <summary>
        /// Gets or Sets Metadata
        /// </summary>
        [JsonPropertyName("metadata")]
        public Object Metadata { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetEntitlementResponse {\n");
            sb.Append("  Data: ").Append(Data).Append("\n");
            sb.Append("  Metadata: ").Append(Metadata).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }

    /// <summary>
    /// A Json converter for type <see cref="GetEntitlementResponse" />
    /// </summary>
    public class GetEntitlementResponseJsonConverter : JsonConverter<GetEntitlementResponse>
    {
        /// <summary>
        /// Deserializes json to <see cref="GetEntitlementResponse" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override GetEntitlementResponse Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            GetEntitlementResponseData? data = default;
            Object? metadata = default;

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
                                data = JsonSerializer.Deserialize<GetEntitlementResponseData>(ref utf8JsonReader, jsonSerializerOptions);
                            break;
                        case "metadata":
                            if (utf8JsonReader.TokenType != JsonTokenType.Null)
                                metadata = JsonSerializer.Deserialize<Object>(ref utf8JsonReader, jsonSerializerOptions);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (data == null)
                throw new ArgumentNullException(nameof(data), "Property is required for class GetEntitlementResponse.");

            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata), "Property is required for class GetEntitlementResponse.");

            return new GetEntitlementResponse(data, metadata);
        }

        /// <summary>
        /// Serializes a <see cref="GetEntitlementResponse" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getEntitlementResponse"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, GetEntitlementResponse getEntitlementResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(ref writer, getEntitlementResponse, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="GetEntitlementResponse" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getEntitlementResponse"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(ref Utf8JsonWriter writer, GetEntitlementResponse getEntitlementResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WritePropertyName("data");
            JsonSerializer.Serialize(writer, getEntitlementResponse.Data, jsonSerializerOptions);
            writer.WritePropertyName("metadata");
            JsonSerializer.Serialize(writer, getEntitlementResponse.Metadata, jsonSerializerOptions);
        }
    }
}
