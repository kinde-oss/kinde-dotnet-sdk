using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetSubscriberResponse that handles the Option<> structure
    /// </summary>
    public class GetSubscriberResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetSubscriberResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetSubscriberResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetSubscriberResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            string? message = default(string?);
            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string?>();
            }
            List<Subscriber> subscribers = default(List<Subscriber>);
            if (jsonObject["subscribers"] != null)
            {
                subscribers = jsonObject["subscribers"].ToObject<List<Subscriber>>(serializer);
            }

            return new GetSubscriberResponse(
                code: code != null ? new Option<string?>(code) : default,                 message: message != null ? new Option<string?>(message) : default,                 subscribers: subscribers != null ? new Option<List<Subscriber>?>(subscribers) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetSubscriberResponse value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.CodeOption.IsSet && value.Code != null)
            {
                writer.WritePropertyName("code");
                serializer.Serialize(writer, value.Code);
            }
            if (value.MessageOption.IsSet && value.Message != null)
            {
                writer.WritePropertyName("message");
                serializer.Serialize(writer, value.Message);
            }
            if (value.SubscribersOption.IsSet)
            {
                writer.WritePropertyName("subscribers");
                serializer.Serialize(writer, value.Subscribers);
            }

            writer.WriteEndObject();
        }
    }
}