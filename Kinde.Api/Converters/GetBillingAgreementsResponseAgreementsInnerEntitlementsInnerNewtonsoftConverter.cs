using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetBillingAgreementsResponseAgreementsInnerEntitlementsInner that handles the Option<> structure
    /// </summary>
    public class GetBillingAgreementsResponseAgreementsInnerEntitlementsInnerNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetBillingAgreementsResponseAgreementsInnerEntitlementsInner>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetBillingAgreementsResponseAgreementsInnerEntitlementsInner ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetBillingAgreementsResponseAgreementsInnerEntitlementsInner existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? featureCode = default(string?);
            if (jsonObject["feature_code"] != null)
            {
                featureCode = jsonObject["feature_code"].ToObject<string?>();
            }
            string? entitlementId = default(string?);
            if (jsonObject["entitlement_id"] != null)
            {
                entitlementId = jsonObject["entitlement_id"].ToObject<string?>();
            }

            return new GetBillingAgreementsResponseAgreementsInnerEntitlementsInner(
                featureCode: featureCode != null ? new Option<string?>(featureCode) : default,                 entitlementId: entitlementId != null ? new Option<string?>(entitlementId) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetBillingAgreementsResponseAgreementsInnerEntitlementsInner value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.FeatureCodeOption.IsSet && value.FeatureCode != null)
            {
                writer.WritePropertyName("feature_code");
                serializer.Serialize(writer, value.FeatureCode);
            }
            if (value.EntitlementIdOption.IsSet && value.EntitlementId != null)
            {
                writer.WritePropertyName("entitlement_id");
                serializer.Serialize(writer, value.EntitlementId);
            }

            writer.WriteEndObject();
        }
    }
}