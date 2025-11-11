using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UserBilling that handles the Option<> structure
    /// </summary>
    public class UserBillingNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UserBilling>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UserBilling ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UserBilling existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? customerId = default(string?);
            if (jsonObject["customer_id"] != null)
            {
                customerId = jsonObject["customer_id"].ToObject<string?>();
            }

            return new UserBilling(
                customerId: customerId != null ? new Option<string?>(customerId) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UserBilling value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.CustomerIdOption.IsSet && value.CustomerId != null)
            {
                writer.WritePropertyName("customer_id");
                serializer.Serialize(writer, value.CustomerId);
            }

            writer.WriteEndObject();
        }
    }
}