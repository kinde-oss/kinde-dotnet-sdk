using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetOrganizationResponseBilling that handles the Option<> structure
    /// </summary>
    public class GetOrganizationResponseBillingNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetOrganizationResponseBilling>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetOrganizationResponseBilling ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetOrganizationResponseBilling existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? billingCustomerId = default(string?);
            if (jsonObject["billing_customer_id"] != null)
            {
                billingCustomerId = jsonObject["billing_customer_id"].ToObject<string?>();
            }
            List<GetOrganizationResponseBillingAgreementsInner> agreements = default(List<GetOrganizationResponseBillingAgreementsInner>);
            if (jsonObject["agreements"] != null)
            {
                agreements = jsonObject["agreements"].ToObject<List<GetOrganizationResponseBillingAgreementsInner>>(serializer);
            }

            return new GetOrganizationResponseBilling(
                billingCustomerId: billingCustomerId != null ? new Option<string?>(billingCustomerId) : default,                 agreements: agreements != null ? new Option<List<GetOrganizationResponseBillingAgreementsInner>?>(agreements) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetOrganizationResponseBilling value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.BillingCustomerIdOption.IsSet && value.BillingCustomerId != null)
            {
                writer.WritePropertyName("billing_customer_id");
                serializer.Serialize(writer, value.BillingCustomerId);
            }
            if (value.AgreementsOption.IsSet)
            {
                writer.WritePropertyName("agreements");
                serializer.Serialize(writer, value.Agreements);
            }

            writer.WriteEndObject();
        }
    }
}