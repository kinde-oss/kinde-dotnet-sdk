using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetBillingAgreementsResponseAgreementsInner that handles the Option<> structure
    /// </summary>
    public class GetBillingAgreementsResponseAgreementsInnerNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetBillingAgreementsResponseAgreementsInner>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetBillingAgreementsResponseAgreementsInner ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetBillingAgreementsResponseAgreementsInner existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            string? planCode = default(string?);
            if (jsonObject["plan_code"] != null)
            {
                planCode = jsonObject["plan_code"].ToObject<string?>();
            }
            DateTimeOffset? expiresOn = default(DateTimeOffset?);
            if (jsonObject["expires_on"] != null)
            {
                expiresOn = jsonObject["expires_on"].ToObject<DateTimeOffset?>(serializer);
            }
            string? billingGroupId = default(string?);
            if (jsonObject["billing_group_id"] != null)
            {
                billingGroupId = jsonObject["billing_group_id"].ToObject<string?>();
            }
            List<GetBillingAgreementsResponseAgreementsInnerEntitlementsInner> entitlements = default(List<GetBillingAgreementsResponseAgreementsInnerEntitlementsInner>);
            if (jsonObject["entitlements"] != null)
            {
                entitlements = jsonObject["entitlements"].ToObject<List<GetBillingAgreementsResponseAgreementsInnerEntitlementsInner>>(serializer);
            }

            return new GetBillingAgreementsResponseAgreementsInner(
                id: id != null ? new Option<string?>(id) : default,                 planCode: planCode != null ? new Option<string?>(planCode) : default,                 expiresOn: expiresOn != null ? new Option<DateTimeOffset?>(expiresOn) : default,                 billingGroupId: billingGroupId != null ? new Option<string?>(billingGroupId) : default,                 entitlements: entitlements != null ? new Option<List<GetBillingAgreementsResponseAgreementsInnerEntitlementsInner>?>(entitlements) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetBillingAgreementsResponseAgreementsInner value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IdOption.IsSet && value.Id != null)
            {
                writer.WritePropertyName("id");
                serializer.Serialize(writer, value.Id);
            }
            if (value.PlanCodeOption.IsSet && value.PlanCode != null)
            {
                writer.WritePropertyName("plan_code");
                serializer.Serialize(writer, value.PlanCode);
            }
            if (value.ExpiresOnOption.IsSet && value.ExpiresOn != null)
            {
                writer.WritePropertyName("expires_on");
                serializer.Serialize(writer, value.ExpiresOn);
            }
            if (value.BillingGroupIdOption.IsSet && value.BillingGroupId != null)
            {
                writer.WritePropertyName("billing_group_id");
                serializer.Serialize(writer, value.BillingGroupId);
            }
            if (value.EntitlementsOption.IsSet)
            {
                writer.WritePropertyName("entitlements");
                serializer.Serialize(writer, value.Entitlements);
            }

            writer.WriteEndObject();
        }
    }
}