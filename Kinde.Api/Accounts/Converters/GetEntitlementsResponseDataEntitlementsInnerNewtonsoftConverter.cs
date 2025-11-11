using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Accounts.Model;
using Kinde.Accounts.Client;

namespace Kinde.Api.Accounts.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetEntitlementsResponseDataEntitlementsInner that handles the Option<> structure
    /// </summary>
    public class GetEntitlementsResponseDataEntitlementsInnerNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetEntitlementsResponseDataEntitlementsInner>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetEntitlementsResponseDataEntitlementsInner ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetEntitlementsResponseDataEntitlementsInner existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? id = default(string?);
            if (jsonObject["id"] != null)
            {
                id = jsonObject["id"].ToObject<string?>();
            }
            int? fixedCharge = default(int?);
            if (jsonObject["fixed_charge"] != null)
            {
                fixedCharge = jsonObject["fixed_charge"].ToObject<int?>(serializer);
            }
            string? priceName = default(string?);
            if (jsonObject["price_name"] != null)
            {
                priceName = jsonObject["price_name"].ToObject<string?>();
            }
            int? unitAmount = default(int?);
            if (jsonObject["unit_amount"] != null)
            {
                unitAmount = jsonObject["unit_amount"].ToObject<int?>(serializer);
            }
            string? featureKey = default(string?);
            if (jsonObject["feature_key"] != null)
            {
                featureKey = jsonObject["feature_key"].ToObject<string?>();
            }
            string? featureName = default(string?);
            if (jsonObject["feature_name"] != null)
            {
                featureName = jsonObject["feature_name"].ToObject<string?>();
            }
            int? entitlementLimitMax = default(int?);
            if (jsonObject["entitlement_limit_max"] != null)
            {
                entitlementLimitMax = jsonObject["entitlement_limit_max"].ToObject<int?>(serializer);
            }
            int? entitlementLimitMin = default(int?);
            if (jsonObject["entitlement_limit_min"] != null)
            {
                entitlementLimitMin = jsonObject["entitlement_limit_min"].ToObject<int?>(serializer);
            }

            return new GetEntitlementsResponseDataEntitlementsInner(
                id: id != null ? new Option<string?>(id) : default,                 fixedCharge: fixedCharge != null ? new Option<int?>(fixedCharge) : default,                 priceName: priceName != null ? new Option<string?>(priceName) : default,                 unitAmount: unitAmount != null ? new Option<int?>(unitAmount) : default,                 featureKey: featureKey != null ? new Option<string?>(featureKey) : default,                 featureName: featureName != null ? new Option<string?>(featureName) : default,                 entitlementLimitMax: entitlementLimitMax != null ? new Option<int?>(entitlementLimitMax) : default,                 entitlementLimitMin: entitlementLimitMin != null ? new Option<int?>(entitlementLimitMin) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetEntitlementsResponseDataEntitlementsInner value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IdOption.IsSet && value.Id != null)
            {
                writer.WritePropertyName("id");
                serializer.Serialize(writer, value.Id);
            }
            if (value.FixedChargeOption.IsSet && value.FixedCharge != null)
            {
                writer.WritePropertyName("fixed_charge");
                serializer.Serialize(writer, value.FixedCharge);
            }
            if (value.PriceNameOption.IsSet && value.PriceName != null)
            {
                writer.WritePropertyName("price_name");
                serializer.Serialize(writer, value.PriceName);
            }
            if (value.UnitAmountOption.IsSet && value.UnitAmount != null)
            {
                writer.WritePropertyName("unit_amount");
                serializer.Serialize(writer, value.UnitAmount);
            }
            if (value.FeatureKeyOption.IsSet && value.FeatureKey != null)
            {
                writer.WritePropertyName("feature_key");
                serializer.Serialize(writer, value.FeatureKey);
            }
            if (value.FeatureNameOption.IsSet && value.FeatureName != null)
            {
                writer.WritePropertyName("feature_name");
                serializer.Serialize(writer, value.FeatureName);
            }
            if (value.EntitlementLimitMaxOption.IsSet && value.EntitlementLimitMax != null)
            {
                writer.WritePropertyName("entitlement_limit_max");
                serializer.Serialize(writer, value.EntitlementLimitMax);
            }
            if (value.EntitlementLimitMinOption.IsSet && value.EntitlementLimitMin != null)
            {
                writer.WritePropertyName("entitlement_limit_min");
                serializer.Serialize(writer, value.EntitlementLimitMin);
            }

            writer.WriteEndObject();
        }
    }
}