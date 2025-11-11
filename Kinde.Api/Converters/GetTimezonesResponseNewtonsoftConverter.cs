using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetTimezonesResponse that handles the Option<> structure
    /// </summary>
    public class GetTimezonesResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetTimezonesResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetTimezonesResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetTimezonesResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            List<GetTimezonesResponseTimezonesInner> timezones = default(List<GetTimezonesResponseTimezonesInner>);
            if (jsonObject["timezones"] != null)
            {
                timezones = jsonObject["timezones"].ToObject<List<GetTimezonesResponseTimezonesInner>>(serializer);
            }

            return new GetTimezonesResponse(
                code: code != null ? new Option<string?>(code) : default,                 message: message != null ? new Option<string?>(message) : default,                 timezones: timezones != null ? new Option<List<GetTimezonesResponseTimezonesInner>?>(timezones) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetTimezonesResponse value, Newtonsoft.Json.JsonSerializer serializer)
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
            if (value.TimezonesOption.IsSet)
            {
                writer.WritePropertyName("timezones");
                serializer.Serialize(writer, value.Timezones);
            }

            writer.WriteEndObject();
        }
    }
}