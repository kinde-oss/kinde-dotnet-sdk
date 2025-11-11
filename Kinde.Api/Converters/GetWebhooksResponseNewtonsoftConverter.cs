using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetWebhooksResponse that handles the Option<> structure
    /// </summary>
    public class GetWebhooksResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetWebhooksResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetWebhooksResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetWebhooksResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            List<Webhook> webhooks = default(List<Webhook>);
            if (jsonObject["webhooks"] != null)
            {
                webhooks = jsonObject["webhooks"].ToObject<List<Webhook>>(serializer);
            }

            return new GetWebhooksResponse(
                code: code != null ? new Option<string?>(code) : default,                 message: message != null ? new Option<string?>(message) : default,                 webhooks: webhooks != null ? new Option<List<Webhook>?>(webhooks) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetWebhooksResponse value, Newtonsoft.Json.JsonSerializer serializer)
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
            if (value.WebhooksOption.IsSet)
            {
                writer.WritePropertyName("webhooks");
                serializer.Serialize(writer, value.Webhooks);
            }

            writer.WriteEndObject();
        }
    }
}