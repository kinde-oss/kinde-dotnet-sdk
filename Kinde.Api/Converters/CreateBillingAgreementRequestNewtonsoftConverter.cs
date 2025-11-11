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

            var jsonObject = JObject.Load(reader);

            bool? isInvoiceNow = default(bool?);
            if (jsonObject["is_invoice_now"] != null)
            {
                isInvoiceNow = jsonObject["is_invoice_now"].ToObject<bool?>(serializer);
            }
            bool? isProrate = default(bool?);
            if (jsonObject["is_prorate"] != null)
            {
                isProrate = jsonObject["is_prorate"].ToObject<bool?>(serializer);
            }
            string customerId = default(string);
            if (jsonObject["customer_id"] != null)
            {
                customerId = jsonObject["customer_id"].ToObject<string>();
            }
            string planCode = default(string);
            if (jsonObject["plan_code"] != null)
            {
                planCode = jsonObject["plan_code"].ToObject<string>();
            }

            return new CreateBillingAgreementRequest(
                isInvoiceNow: isInvoiceNow != null ? new Option<bool?>(isInvoiceNow) : default,                 isProrate: isProrate != null ? new Option<bool?>(isProrate) : default,                 customerId: customerId,                 planCode: planCode            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateBillingAgreementRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IsInvoiceNowOption.IsSet && value.IsInvoiceNow != null)
            {
                writer.WritePropertyName("is_invoice_now");
                serializer.Serialize(writer, value.IsInvoiceNow);
            }
            if (value.IsProrateOption.IsSet && value.IsProrate != null)
            {
                writer.WritePropertyName("is_prorate");
                serializer.Serialize(writer, value.IsProrate);
            }

            writer.WriteEndObject();
        }
    }
}