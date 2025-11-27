#nullable enable

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kinde.Accounts.Model.Entities;

namespace Kinde.Accounts.Model.Entities
{
    /// <summary>
    /// JSON converter for <see cref="Entitlement" />
    /// </summary>
public class EntitlementJsonConverter : JsonConverter<Entitlement>
    {
        /// <summary>
        /// Deserializes json to <see cref="Entitlement" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override Entitlement Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            string? featureKey = default;
            string? featureName = default;
            string? id = default;
            string? priceName = default;
            int? entitlementLimitMax = default;
            int? entitlementLimitMin = default;
            int? fixedCharge = default;
            int? unitAmount = default;

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
                        case "feature_key":
                            featureKey = utf8JsonReader.GetString();
                            break;
                        case "feature_name":
                            featureName = utf8JsonReader.GetString();
                            break;
                        case "id":
                            id = utf8JsonReader.GetString();
                            break;
                        case "price_name":
                            priceName = utf8JsonReader.GetString();
                            break;
                        case "entitlement_limit_max":
                            if (utf8JsonReader.TokenType != JsonTokenType.Null)
                                entitlementLimitMax = utf8JsonReader.GetInt32();
                            break;
                        case "entitlement_limit_min":
                            if (utf8JsonReader.TokenType != JsonTokenType.Null)
                                entitlementLimitMin = utf8JsonReader.GetInt32();
                            break;
                        case "fixed_charge":
                            if (utf8JsonReader.TokenType != JsonTokenType.Null)
                                fixedCharge = utf8JsonReader.GetInt32();
                            break;
                        case "unit_amount":
                            if (utf8JsonReader.TokenType != JsonTokenType.Null)
                                unitAmount = utf8JsonReader.GetInt32();
                            break;
                        default:
                            break;
                    }
                }
            }

            if (featureKey == null)
                throw new ArgumentNullException(nameof(featureKey), "Property is required for class Entitlement.");

            if (featureName == null)
                throw new ArgumentNullException(nameof(featureName), "Property is required for class Entitlement.");

            if (id == null)
                throw new ArgumentNullException(nameof(id), "Property is required for class Entitlement.");

            if (priceName == null)
                throw new ArgumentNullException(nameof(priceName), "Property is required for class Entitlement.");

            return new Entitlement(featureKey, featureName, id, priceName, entitlementLimitMax, entitlementLimitMin, fixedCharge, unitAmount);
        }

        /// <summary>
        /// Serializes a <see cref="Entitlement" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="entitlement"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, Entitlement entitlement, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();
            WriteProperties(ref writer, entitlement, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="Entitlement" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="entitlement"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(ref Utf8JsonWriter writer, Entitlement entitlement, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteString("feature_key", entitlement.FeatureKey);
            writer.WriteString("feature_name", entitlement.FeatureName);
            writer.WriteString("id", entitlement.Id);
            writer.WriteString("price_name", entitlement.PriceName);
            if (entitlement.EntitlementLimitMax.HasValue)
                writer.WriteNumber("entitlement_limit_max", entitlement.EntitlementLimitMax.Value);
            if (entitlement.EntitlementLimitMin.HasValue)
                writer.WriteNumber("entitlement_limit_min", entitlement.EntitlementLimitMin.Value);
            if (entitlement.FixedCharge.HasValue)
                writer.WriteNumber("fixed_charge", entitlement.FixedCharge.Value);
            if (entitlement.UnitAmount.HasValue)
                writer.WriteNumber("unit_amount", entitlement.UnitAmount.Value);
        }
    }
}
