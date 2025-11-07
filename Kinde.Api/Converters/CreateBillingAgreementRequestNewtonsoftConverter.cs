using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateBillingAgreementRequest that handles the Option<> structure
    /// </summary>
    public class CreateBillingAgreementRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateBillingAgreementRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateBillingAgreementRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateBillingAgreementRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string customerId = default(string);
            string planCode = default(string);
            bool? isInvoiceNow = null;
            bool? isProrate = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["customer_id"] != null)
            {
                customerId = jsonObject["customer_id"].ToObject<string>();
            }

            if (jsonObject["plan_code"] != null)
            {
                planCode = jsonObject["plan_code"].ToObject<string>();
            }

            if (jsonObject["is_invoice_now"] != null)
            {
                isInvoiceNow = jsonObject["is_invoice_now"].ToObject<bool?>();
            }

            if (jsonObject["is_prorate"] != null)
            {
                isProrate = jsonObject["is_prorate"].ToObject<bool?>();
            }

            return new CreateBillingAgreementRequest(
                customerId: customerId, planCode: planCode, isInvoiceNow: isInvoiceNow != null ? new Option<bool?>(isInvoiceNow) : default, isProrate: isProrate != null ? new Option<bool?>(isProrate) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateBillingAgreementRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IsInvoiceNowOption.IsSet && value.IsInvoiceNow != null)
            {
                writer.WritePropertyName("is_invoice_now");
                writer.WriteValue(value.IsInvoiceNow.Value);
            }

            if (value.IsProrateOption.IsSet && value.IsProrate != null)
            {
                writer.WritePropertyName("is_prorate");
                writer.WriteValue(value.IsProrate.Value);
            }

            writer.WriteEndObject();
        }
    }
}
