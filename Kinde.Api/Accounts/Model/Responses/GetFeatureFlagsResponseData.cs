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
    /// GetFeatureFlagsResponseData
    /// </summary>
    public partial class GetFeatureFlagsResponseData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetFeatureFlagsResponseData" /> class.
        /// </summary>
        /// <param name="featureFlags">A list of feature flags</param>
        [JsonConstructor]
        public GetFeatureFlagsResponseData(List<FeatureFlag> featureFlags)
        {
            FeatureFlags = featureFlags;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// A list of feature flags
        /// </summary>
        /// <value>A list of feature flags</value>
        [JsonPropertyName("feature_flags")]
        public List<FeatureFlag> FeatureFlags { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetFeatureFlagsResponseData {\n");
            sb.Append("  FeatureFlags: ").Append(FeatureFlags).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }

    /// <summary>
    /// A Json converter for type <see cref="GetFeatureFlagsResponseData" />
    /// </summary>
    public class GetFeatureFlagsResponseDataJsonConverter : JsonConverter<GetFeatureFlagsResponseData>
    {
        /// <summary>
        /// Deserializes json to <see cref="GetFeatureFlagsResponseData" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override GetFeatureFlagsResponseData Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            List<FeatureFlag>? featureFlags = default;

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
                        case "feature_flags":
                            if (utf8JsonReader.TokenType != JsonTokenType.Null)
                                featureFlags = JsonSerializer.Deserialize<List<FeatureFlag>>(ref utf8JsonReader, jsonSerializerOptions);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (featureFlags == null)
                throw new ArgumentNullException(nameof(featureFlags), "Property is required for class GetFeatureFlagsResponseData.");

            return new GetFeatureFlagsResponseData(featureFlags);
        }

        /// <summary>
        /// Serializes a <see cref="GetFeatureFlagsResponseData" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getFeatureFlagsResponseData"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, GetFeatureFlagsResponseData getFeatureFlagsResponseData, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(ref writer, getFeatureFlagsResponseData, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="GetFeatureFlagsResponseData" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getFeatureFlagsResponseData"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(ref Utf8JsonWriter writer, GetFeatureFlagsResponseData getFeatureFlagsResponseData, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WritePropertyName("feature_flags");
            JsonSerializer.Serialize(writer, getFeatureFlagsResponseData.FeatureFlags, jsonSerializerOptions);
        }
    }
}
