using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetSubscribersResponse that handles the Option<> structure
    /// </summary>
    public class GetSubscribersResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetSubscribersResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetSubscribersResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetSubscribersResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? code = null;
            string? message = null;
            List<SubscribersSubscriber> subscribers = null;
            string? nextToken = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string>();
            }

            if (jsonObject["subscribers"] != null)
            {
                subscribers = jsonObject["subscribers"].ToObject<List<SubscribersSubscriber>>(serializer);
            }

            if (jsonObject["next_token"] != null)
            {
                nextToken = jsonObject["next_token"].ToObject<string>();
            }

            return new GetSubscribersResponse(
                code: code != null ? new Option<string?>(code) : default, message: message != null ? new Option<string?>(message) : default, subscribers: subscribers != null ? new Option<List<SubscribersSubscriber>?>(subscribers) : default, nextToken: nextToken != null ? new Option<string?>(nextToken) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetSubscribersResponse value, Newtonsoft.Json.JsonSerializer serializer)
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

            if (value.SubscribersOption.IsSet && value.Subscribers != null)
            {
                writer.WritePropertyName("subscribers");
                serializer.Serialize(writer, value.Subscribers);
            }

            if (value.NextTokenOption.IsSet && value.NextToken != null)
            {
                writer.WritePropertyName("next_token");
                serializer.Serialize(writer, value.NextToken);
            }

            writer.WriteEndObject();
        }
    }
}
