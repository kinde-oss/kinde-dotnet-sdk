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
    /// ErrorResponse
    /// </summary>
    public partial class ErrorResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResponse" /> class.
        /// </summary>
        /// <param name="errors">errors</param>
        [JsonConstructor]
        public ErrorResponse(List<Error> errors)
        {
            Errors = errors;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// Gets or Sets Errors
        /// </summary>
        [JsonPropertyName("errors")]
        public List<Error> Errors { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class ErrorResponse {\n");
            sb.Append("  Errors: ").Append(Errors).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }

    /// <summary>
    /// A Json converter for type <see cref="ErrorResponse" />
    /// </summary>
    public class ErrorResponseJsonConverter : JsonConverter<ErrorResponse>
    {
        /// <summary>
        /// Deserializes json to <see cref="ErrorResponse" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override ErrorResponse Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            List<Error>? errors = default;

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
                        case "errors":
                            if (utf8JsonReader.TokenType != JsonTokenType.Null)
                                errors = JsonSerializer.Deserialize<List<Error>>(ref utf8JsonReader, jsonSerializerOptions);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (errors == null)
                throw new ArgumentNullException(nameof(errors), "Property is required for class ErrorResponse.");

            return new ErrorResponse(errors);
        }

        /// <summary>
        /// Serializes a <see cref="ErrorResponse" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="errorResponse"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, ErrorResponse errorResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(ref writer, errorResponse, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="ErrorResponse" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="errorResponse"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(ref Utf8JsonWriter writer, ErrorResponse errorResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WritePropertyName("errors");
            JsonSerializer.Serialize(writer, errorResponse.Errors, jsonSerializerOptions);
        }
    }
}
