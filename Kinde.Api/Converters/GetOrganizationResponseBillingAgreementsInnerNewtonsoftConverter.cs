using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetOrganizationResponseBillingAgreementsInner that handles the Option<> structure
    /// </summary>
    public class GetOrganizationResponseBillingAgreementsInnerNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetOrganizationResponseBillingAgreementsInner>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetOrganizationResponseBillingAgreementsInner ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetOrganizationResponseBillingAgreementsInner existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? planCode = default(string?);
            if (jsonObject["plan_code"] != null)
            {
                planCode = jsonObject["plan_code"].ToObject<string?>();
            }
            string? agreementId = default(string?);
            if (jsonObject["agreement_id"] != null)
            {
                agreementId = jsonObject["agreement_id"].ToObject<string?>();
            }

            return new GetOrganizationResponseBillingAgreementsInner(
                planCode: planCode != null ? new Option<string?>(planCode) : default,                 agreementId: agreementId != null ? new Option<string?>(agreementId) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetOrganizationResponseBillingAgreementsInner value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.PlanCodeOption.IsSet && value.PlanCode != null)
            {
                writer.WritePropertyName("plan_code");
                serializer.Serialize(writer, value.PlanCode);
            }
            if (value.AgreementIdOption.IsSet && value.AgreementId != null)
            {
                writer.WritePropertyName("agreement_id");
                serializer.Serialize(writer, value.AgreementId);
            }

            writer.WriteEndObject();
        }
    }
}