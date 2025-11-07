using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UpdateWebhookResponse that handles the Option<> structure
    /// </summary>
    public class UpdateWebhookResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UpdateWebhookResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UpdateWebhookResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UpdateWebhookResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? message = null;
            string? code = null;
            UpdateWebhookResponseWebhook? webhook = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string>();
            }

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["webhook"] != null)
            {
                webhook = jsonObject["webhook"].ToObject<UpdateWebhookResponseWebhook>(serializer);
            }

            return new UpdateWebhookResponse(
                message: message != null ? new Option<string?>(message) : default, code: code != null ? new Option<string?>(code) : default, webhook: webhook != null ? new Option<UpdateWebhookResponseWebhook?>(webhook) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UpdateWebhookResponse value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.MessageOption.IsSet && value.Message != null)
            {
                writer.WritePropertyName("message");
                serializer.Serialize(writer, value.Message);
            }

            if (value.CodeOption.IsSet && value.Code != null)
            {
                writer.WritePropertyName("code");
                serializer.Serialize(writer, value.Code);
            }

            if (value.WebhookOption.IsSet && value.Webhook != null)
            {
                writer.WritePropertyName("webhook");
                serializer.Serialize(writer, value.Webhook);
            }

            writer.WriteEndObject();
        }
    }
}
