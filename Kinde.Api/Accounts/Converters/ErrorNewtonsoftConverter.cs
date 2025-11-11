using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Accounts.Model;
using Kinde.Accounts.Client;

namespace Kinde.Api.Accounts.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for Error that handles the Option<> structure
    /// </summary>
    public class ErrorNewtonsoftConverter : Newtonsoft.Json.JsonConverter<Error>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override Error ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, Error existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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

            return new Error(
                code: code != null ? new Option<string?>(code) : default,                 message: message != null ? new Option<string?>(message) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, Error value, Newtonsoft.Json.JsonSerializer serializer)
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

            writer.WriteEndObject();
        }
    }
}