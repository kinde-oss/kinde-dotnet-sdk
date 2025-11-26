#nullable enable

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using OpenAPIClientUtils = Kinde.Accounts.Client.ClientUtils;
using Kinde.Accounts.Model.Entities;

namespace Kinde.Accounts.Model.Entities
{
    /// <summary>
    /// JSON converter for <see cref="FeatureFlagValue" />
    /// </summary>
public class FeatureFlagValueJsonConverter : JsonConverter<FeatureFlagValue>
    {
        /// <summary>
        /// Deserializes json to <see cref="FeatureFlagValue" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override FeatureFlagValue Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            string? varString = default;
            bool? varBool = default;
            int? varInt = default;
            Object? varObject = default;

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

                    Utf8JsonReader utf8JsonReaderVarObject = utf8JsonReader;
                    OpenAPIClientUtils.TryDeserialize<Object?>(ref utf8JsonReaderVarObject, jsonSerializerOptions, out varObject);
                }
            }

            if (varString != null)
                return new FeatureFlagValue(varString);

            if (varBool != null)
                return new FeatureFlagValue(varBool.Value);

            if (varInt != null)
                return new FeatureFlagValue(varInt.Value);

            if (varObject != null)
                return new FeatureFlagValue(varObject);

            throw new JsonException();
        }

        /// <summary>
        /// Serializes a <see cref="FeatureFlagValue" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="featureFlagValue"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, FeatureFlagValue featureFlagValue, JsonSerializerOptions jsonSerializerOptions)
        {
            if (featureFlagValue.VarString != null)
            {
                JsonSerializer.Serialize(writer, featureFlagValue.VarString, jsonSerializerOptions);
                return;
            }

            if (featureFlagValue.VarBool.HasValue)
            {
                JsonSerializer.Serialize(writer, featureFlagValue.VarBool.Value, jsonSerializerOptions);
                return;
            }

            if (featureFlagValue.VarInt.HasValue)
            {
                JsonSerializer.Serialize(writer, featureFlagValue.VarInt.Value, jsonSerializerOptions);
                return;
            }

            if (featureFlagValue.VarObject != null)
            {
                JsonSerializer.Serialize(writer, featureFlagValue.VarObject, jsonSerializerOptions);
                return;
            }

            writer.WriteNullValue();
        }
    }
}
