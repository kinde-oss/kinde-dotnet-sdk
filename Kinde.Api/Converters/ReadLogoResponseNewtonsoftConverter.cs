using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for ReadLogoResponse that handles the Option<> structure
    /// </summary>
    public class ReadLogoResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<ReadLogoResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override ReadLogoResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, ReadLogoResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? code = null;
            List<ReadLogoResponseLogosInner> logos = null;
            string? message = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["logos"] != null)
            {
                logos = jsonObject["logos"].ToObject<List<ReadLogoResponseLogosInner>>(serializer);
            }

            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string>();
            }

            return new ReadLogoResponse(
                code: code != null ? new Option<string?>(code) : default, logos: logos != null ? new Option<List<ReadLogoResponseLogosInner>?>(logos) : default, message: message != null ? new Option<string?>(message) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, ReadLogoResponse value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.CodeOption.IsSet && value.Code != null)
            {
                writer.WritePropertyName("code");
                serializer.Serialize(writer, value.Code);
            }

            if (value.LogosOption.IsSet && value.Logos != null)
            {
                writer.WritePropertyName("logos");
                serializer.Serialize(writer, value.Logos);
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
