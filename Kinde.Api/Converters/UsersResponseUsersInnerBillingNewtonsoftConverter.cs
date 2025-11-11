using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UsersResponseUsersInnerBilling that handles the Option<> structure
    /// </summary>
    public class UsersResponseUsersInnerBillingNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UsersResponseUsersInnerBilling>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UsersResponseUsersInnerBilling ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UsersResponseUsersInnerBilling existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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

            return new UsersResponseUsersInnerBilling(
                customerId: customerId != null ? new Option<string?>(customerId) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UsersResponseUsersInnerBilling value, Newtonsoft.Json.JsonSerializer serializer)
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