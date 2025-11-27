#nullable enable

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kinde.Accounts.Model.Entities;

namespace Kinde.Accounts.Model.Entities
{
    /// <summary>
    /// JSON converter for <see cref="TokenIntrospect" />
    /// </summary>
public class TokenIntrospectJsonConverter : JsonConverter<TokenIntrospect>
    {
        /// <summary>
        /// Deserializes json to <see cref="TokenIntrospect" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override TokenIntrospect Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            bool? active = default;
            List<string>? aud = default;
            string? clientId = default;
            int? exp = default;
            int? iat = default;

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
                        case "active":
                            if (utf8JsonReader.TokenType != JsonTokenType.Null)
                                active = utf8JsonReader.GetBoolean();
                            break;
                        case "aud":
                            if (utf8JsonReader.TokenType != JsonTokenType.Null)
                                aud = JsonSerializer.Deserialize<List<string>>(ref utf8JsonReader, jsonSerializerOptions);
                            break;
                        case "client_id":
                            clientId = utf8JsonReader.GetString();
                            break;
                        case "exp":
                            if (utf8JsonReader.TokenType != JsonTokenType.Null)
                                exp = utf8JsonReader.GetInt32();
                            break;
                        case "iat":
                            if (utf8JsonReader.TokenType != JsonTokenType.Null)
                                iat = utf8JsonReader.GetInt32();
                            break;
                        default:
                            break;
                    }
                }
            }

            return new TokenIntrospect(active, aud, clientId, exp, iat);
        }

        /// <summary>
        /// Serializes a <see cref="TokenIntrospect" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="tokenIntrospect"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, TokenIntrospect tokenIntrospect, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();
            WriteProperties(ref writer, tokenIntrospect, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="TokenIntrospect" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="tokenIntrospect"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(ref Utf8JsonWriter writer, TokenIntrospect tokenIntrospect, JsonSerializerOptions jsonSerializerOptions)
        {
            if (tokenIntrospect.Active.HasValue)
                writer.WriteBoolean("active", tokenIntrospect.Active.Value);
            if (tokenIntrospect.Aud != null)
            {
                writer.WritePropertyName("aud");
                JsonSerializer.Serialize(writer, tokenIntrospect.Aud, jsonSerializerOptions);
            }
            if (tokenIntrospect.ClientId != null)
                writer.WriteString("client_id", tokenIntrospect.ClientId);
            if (tokenIntrospect.Exp.HasValue)
                writer.WriteNumber("exp", tokenIntrospect.Exp.Value);
            if (tokenIntrospect.Iat.HasValue)
                writer.WriteNumber("iat", tokenIntrospect.Iat.Value);
        }
    }
}
