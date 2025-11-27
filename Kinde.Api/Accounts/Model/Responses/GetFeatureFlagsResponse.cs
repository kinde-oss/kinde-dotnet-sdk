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
    /// GetFeatureFlagsResponse
    /// </summary>
    public partial class GetFeatureFlagsResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetFeatureFlagsResponse" /> class.
        /// </summary>
        /// <param name="data">data</param>
        [JsonConstructor]
        public GetFeatureFlagsResponse(GetFeatureFlagsResponseData data)
        {
            Data = data;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// Gets or Sets Data
        /// </summary>
        [JsonPropertyName("data")]
        public GetFeatureFlagsResponseData Data { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetFeatureFlagsResponse {\n");
            sb.Append("  Data: ").Append(Data).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }

    /// <summary>
    /// A Json converter for type <see cref="GetFeatureFlagsResponse" />
    /// </summary>
    public class GetFeatureFlagsResponseJsonConverter : JsonConverter<GetFeatureFlagsResponse>
    {
        /// <summary>
        /// Deserializes json to <see cref="GetFeatureFlagsResponse" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override GetFeatureFlagsResponse Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            GetFeatureFlagsResponseData? data = default;

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
                                data = JsonSerializer.Deserialize<GetFeatureFlagsResponseData>(ref utf8JsonReader, jsonSerializerOptions);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (data == null)
                throw new ArgumentNullException(nameof(data), "Property is required for class GetFeatureFlagsResponse.");

            return new GetFeatureFlagsResponse(data);
        }

        /// <summary>
        /// Serializes a <see cref="GetFeatureFlagsResponse" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getFeatureFlagsResponse"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, GetFeatureFlagsResponse getFeatureFlagsResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(ref writer, getFeatureFlagsResponse, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="GetFeatureFlagsResponse" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getFeatureFlagsResponse"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(ref Utf8JsonWriter writer, GetFeatureFlagsResponse getFeatureFlagsResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WritePropertyName("data");
            JsonSerializer.Serialize(writer, getFeatureFlagsResponse.Data, jsonSerializerOptions);
        }
    }
}
