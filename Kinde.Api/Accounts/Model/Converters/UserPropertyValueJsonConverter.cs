#nullable enable

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using OpenAPIClientUtils = Kinde.Accounts.Client.ClientUtils;
using Kinde.Accounts.Model.Entities;

namespace Kinde.Accounts.Model.Entities
{
    /// <summary>
    /// JSON converter for <see cref="UserPropertyValue" />
    /// </summary>
public class UserPropertyValueJsonConverter : JsonConverter<UserPropertyValue>
    {
        /// <summary>
        /// Deserializes json to <see cref="UserPropertyValue" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override UserPropertyValue Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            string? varString = default;
            bool? varBool = default;
            int? varInt = default;

            Utf8JsonReader utf8JsonReaderOneOf = utf8JsonReader;
            while (utf8JsonReaderOneOf.Read())
            {
                if (startingTokenType == JsonTokenType.StartObject && utf8JsonReaderOneOf.TokenType == JsonTokenType.EndObject && currentDepth == utf8JsonReaderOneOf.CurrentDepth)
                    break;

                if (startingTokenType == JsonTokenType.StartArray && utf8JsonReaderOneOf.TokenType == JsonTokenType.EndArray && currentDepth == utf8JsonReaderOneOf.CurrentDepth)
                    break;

                if (utf8JsonReaderOneOf.TokenType == JsonTokenType.PropertyName && currentDepth == utf8JsonReaderOneOf.CurrentDepth - 1)
                {
                    Utf8JsonReader utf8JsonReaderVarString = utf8JsonReader;
                    OpenAPIClientUtils.TryDeserialize<string?>(ref utf8JsonReaderVarString, jsonSerializerOptions, out varString);

                    Utf8JsonReader utf8JsonReaderVarBool = utf8JsonReader;
                    OpenAPIClientUtils.TryDeserialize<bool?>(ref utf8JsonReaderVarBool, jsonSerializerOptions, out varBool);

                    Utf8JsonReader utf8JsonReaderVarInt = utf8JsonReader;
                    OpenAPIClientUtils.TryDeserialize<int?>(ref utf8JsonReaderVarInt, jsonSerializerOptions, out varInt);
                }
            }

            if (varString != null)
                return new UserPropertyValue(varString);

            if (varBool != null)
                return new UserPropertyValue(varBool.Value);

            if (varInt != null)
                return new UserPropertyValue(varInt.Value);

            throw new JsonException();
        }

        /// <summary>
        /// Serializes a <see cref="UserPropertyValue" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="userPropertyValue"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, UserPropertyValue userPropertyValue, JsonSerializerOptions jsonSerializerOptions)
        {
            if (userPropertyValue.VarString != null)
            {
                JsonSerializer.Serialize(writer, userPropertyValue.VarString, jsonSerializerOptions);
                return;
            }

            if (userPropertyValue.VarBool.HasValue)
            {
                JsonSerializer.Serialize(writer, userPropertyValue.VarBool.Value, jsonSerializerOptions);
                return;
            }

            if (userPropertyValue.VarInt.HasValue)
            {
                JsonSerializer.Serialize(writer, userPropertyValue.VarInt.Value, jsonSerializerOptions);
                return;
            }

            writer.WriteNullValue();
        }
    }
}
