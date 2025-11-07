using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateWebhookResponse that handles the Option<> structure
    /// </summary>
    public class CreateWebhookResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateWebhookResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateWebhookResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateWebhookResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? code = null;
            string? message = null;
            CreateWebhookResponseWebhook? webhook = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string>();
            }

            if (jsonObject["webhook"] != null)
            {
                webhook = jsonObject["webhook"].ToObject<CreateWebhookResponseWebhook>(serializer);
            }

            return new CreateWebhookResponse(
                code: code != null ? new Option<string?>(code) : default, message: message != null ? new Option<string?>(message) : default, webhook: webhook != null ? new Option<CreateWebhookResponseWebhook?>(webhook) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateWebhookResponse value, Newtonsoft.Json.JsonSerializer serializer)
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

            if (value.WebhookOption.IsSet && value.Webhook != null)
            {
                writer.WritePropertyName("webhook");
                serializer.Serialize(writer, value.Webhook);
            }

            writer.WriteEndObject();
        }
    }
}
