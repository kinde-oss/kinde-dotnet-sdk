using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Accounts.Model;
using Kinde.Accounts.Client;

namespace Kinde.Api.Accounts.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetEntitlementsResponseDataPlansInner that handles the Option<> structure
    /// </summary>
    public class GetEntitlementsResponseDataPlansInnerNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetEntitlementsResponseDataPlansInner>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetEntitlementsResponseDataPlansInner ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetEntitlementsResponseDataPlansInner existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? key = default(string?);
            if (jsonObject["key"] != null)
            {
                key = jsonObject["key"].ToObject<string?>();
            }
            string? name = default(string?);
            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string?>();
            }
            DateTimeOffset? subscribedOn = default(DateTimeOffset?);
            if (jsonObject["subscribed_on"] != null)
            {
                subscribedOn = jsonObject["subscribed_on"].ToObject<DateTimeOffset?>(serializer);
            }

            return new GetEntitlementsResponseDataPlansInner(
                key: key != null ? new Option<string?>(key) : default,                 name: name != null ? new Option<string?>(name) : default,                 subscribedOn: subscribedOn != null ? new Option<DateTimeOffset?>(subscribedOn) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetEntitlementsResponseDataPlansInner value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.KeyOption.IsSet && value.Key != null)
            {
                writer.WritePropertyName("key");
                serializer.Serialize(writer, value.Key);
            }
            if (value.NameOption.IsSet && value.Name != null)
            {
                writer.WritePropertyName("name");
                serializer.Serialize(writer, value.Name);
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