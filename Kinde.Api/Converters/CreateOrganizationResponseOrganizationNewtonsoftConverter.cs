using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateOrganizationResponseOrganization that handles the Option<> structure
    /// </summary>
    public class CreateOrganizationResponseOrganizationNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateOrganizationResponseOrganization>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateOrganizationResponseOrganization ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateOrganizationResponseOrganization existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? code = default(string?);
            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string?>();
            }
            string? billingCustomerId = default(string?);
            if (jsonObject["billing_customer_id"] != null)
            {
                billingCustomerId = jsonObject["billing_customer_id"].ToObject<string?>();
            }

            return new CreateOrganizationResponseOrganization(
                code: code != null ? new Option<string?>(code) : default,                 billingCustomerId: billingCustomerId != null ? new Option<string?>(billingCustomerId) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateOrganizationResponseOrganization value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.CodeOption.IsSet && value.Code != null)
            {
                writer.WritePropertyName("code");
                serializer.Serialize(writer, value.Code);
            }
            if (value.BillingCustomerIdOption.IsSet && value.BillingCustomerId != null)
            {
                writer.WritePropertyName("billing_customer_id");
                serializer.Serialize(writer, value.BillingCustomerId);
            }

            writer.WriteEndObject();
        }
    }
}