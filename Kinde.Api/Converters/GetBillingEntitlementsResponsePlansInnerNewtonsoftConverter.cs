using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetBillingEntitlementsResponsePlansInner that handles the Option<> structure
    /// </summary>
    public class GetBillingEntitlementsResponsePlansInnerNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetBillingEntitlementsResponsePlansInner>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetBillingEntitlementsResponsePlansInner ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetBillingEntitlementsResponsePlansInner existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            DateTimeOffset? subscribedOn = default(DateTimeOffset?);
            if (jsonObject["subscribed_on"] != null)
            {
                subscribedOn = jsonObject["subscribed_on"].ToObject<DateTimeOffset?>(serializer);
            }

            return new GetBillingEntitlementsResponsePlansInner(
                code: code != null ? new Option<string?>(code) : default,                 subscribedOn: subscribedOn != null ? new Option<DateTimeOffset?>(subscribedOn) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetBillingEntitlementsResponsePlansInner value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.CodeOption.IsSet && value.Code != null)
            {
                writer.WritePropertyName("code");
                serializer.Serialize(writer, value.Code);
            }
            if (value.SubscribedOnOption.IsSet && value.SubscribedOn != null)
            {
                writer.WritePropertyName("subscribed_on");
                serializer.Serialize(writer, value.SubscribedOn);
            }

            writer.WriteEndObject();
        }
    }
}