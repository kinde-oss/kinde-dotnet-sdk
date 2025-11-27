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
    /// TokenErrorResponse
    /// </summary>
    public partial class TokenErrorResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenErrorResponse" /> class.
        /// </summary>
        /// <param name="error">Error.</param>
        /// <param name="errorDescription">The error description.</param>
        [JsonConstructor]
        public TokenErrorResponse(string error, string errorDescription)
        {
            Error = error;
            ErrorDescription = errorDescription;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// Error.
        /// </summary>
        /// <value>Error.</value>
        [JsonPropertyName("error")]
        public string Error { get; set; }

        /// <summary>
        /// The error description.
        /// </summary>
        /// <value>The error description.</value>
        [JsonPropertyName("error_description")]
        public string ErrorDescription { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class TokenErrorResponse {\n");
            sb.Append("  Error: ").Append(Error).Append("\n");
            sb.Append("  ErrorDescription: ").Append(ErrorDescription).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }

    /// <summary>
    /// A Json converter for type <see cref="TokenErrorResponse" />
    /// </summary>
    public class TokenErrorResponseJsonConverter : JsonConverter<TokenErrorResponse>
    {
        /// <summary>
        /// Deserializes json to <see cref="TokenErrorResponse" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override TokenErrorResponse Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            string? error = default;
            string? errorDescription = default;

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
                        case "error":
                            error = utf8JsonReader.GetString();
                            break;
                        case "error_description":
                            errorDescription = utf8JsonReader.GetString();
                            break;
                        default:
                            break;
                    }
                }
            }

            if (error == null)
                throw new ArgumentNullException(nameof(error), "Property is required for class TokenErrorResponse.");

            if (errorDescription == null)
                throw new ArgumentNullException(nameof(errorDescription), "Property is required for class TokenErrorResponse.");

            return new TokenErrorResponse(error, errorDescription);
        }

        /// <summary>
        /// Serializes a <see cref="TokenErrorResponse" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="tokenErrorResponse"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, TokenErrorResponse tokenErrorResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(ref writer, tokenErrorResponse, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="TokenErrorResponse" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="tokenErrorResponse"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(ref Utf8JsonWriter writer, TokenErrorResponse tokenErrorResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteString("error", tokenErrorResponse.Error);
            writer.WriteString("error_description", tokenErrorResponse.ErrorDescription);
        }
    }
}
