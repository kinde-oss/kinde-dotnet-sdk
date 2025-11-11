using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateSubscriberSuccessResponseSubscriber that handles the Option<> structure
    /// </summary>
    public class CreateSubscriberSuccessResponseSubscriberNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateSubscriberSuccessResponseSubscriber>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateSubscriberSuccessResponseSubscriber ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateSubscriberSuccessResponseSubscriber existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? subscriberId = default(string?);
            if (jsonObject["subscriber_id"] != null)
            {
                subscriberId = jsonObject["subscriber_id"].ToObject<string?>();
            }

            return new CreateSubscriberSuccessResponseSubscriber(
                subscriberId: subscriberId != null ? new Option<string?>(subscriberId) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateSubscriberSuccessResponseSubscriber value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.SubscriberIdOption.IsSet && value.SubscriberId != null)
            {
                writer.WritePropertyName("subscriber_id");
                serializer.Serialize(writer, value.SubscriberId);
            }

            writer.WriteEndObject();
        }
    }
}