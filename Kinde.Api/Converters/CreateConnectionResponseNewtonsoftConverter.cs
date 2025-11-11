using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateConnectionResponse that handles the Option<> structure
    /// </summary>
    public class CreateConnectionResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateConnectionResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateConnectionResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateConnectionResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? message = default(string?);
            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string?>();
            }
            string? code = default(string?);
            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string?>();
            }
            CreateConnectionResponseConnection? connection = default(CreateConnectionResponseConnection?);
            if (jsonObject["connection"] != null)
            {
                connection = jsonObject["connection"].ToObject<CreateConnectionResponseConnection?>(serializer);
            }

            return new CreateConnectionResponse(
                message: message != null ? new Option<string?>(message) : default,                 code: code != null ? new Option<string?>(code) : default,                 connection: connection != null ? new Option<CreateConnectionResponseConnection?>(connection) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateConnectionResponse value, Newtonsoft.Json.JsonSerializer serializer)
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
            if (value.ConnectionOption.IsSet && value.Connection != null)
            {
                writer.WritePropertyName("connection");
                serializer.Serialize(writer, value.Connection);
            }

            writer.WriteEndObject();
        }
    }
}