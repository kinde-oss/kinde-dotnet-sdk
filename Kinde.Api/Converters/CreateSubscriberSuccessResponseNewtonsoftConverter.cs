using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateSubscriberSuccessResponse that handles the Option<> structure
    /// </summary>
    public class CreateSubscriberSuccessResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateSubscriberSuccessResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateSubscriberSuccessResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateSubscriberSuccessResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            CreateSubscriberSuccessResponseSubscriber? subscriber = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["subscriber"] != null)
            {
                subscriber = jsonObject["subscriber"].ToObject<CreateSubscriberSuccessResponseSubscriber>(serializer);
            }

            return new CreateSubscriberSuccessResponse(
                subscriber: subscriber != null ? new Option<CreateSubscriberSuccessResponseSubscriber?>(subscriber) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateSubscriberSuccessResponse value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.SubscriberOption.IsSet && value.Subscriber != null)
            {
                writer.WritePropertyName("subscriber");
                serializer.Serialize(writer, value.Subscriber);
            }

            writer.WriteEndObject();
        }
    }
}
