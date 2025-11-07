using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateApplicationResponse that handles the Option<> structure
    /// </summary>
    public class CreateApplicationResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateApplicationResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateApplicationResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateApplicationResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? code = null;
            string? message = null;
            CreateApplicationResponseApplication? application = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string>();
            }

            if (jsonObject["application"] != null)
            {
                application = jsonObject["application"].ToObject<CreateApplicationResponseApplication>(serializer);
            }

            return new CreateApplicationResponse(
                code: code != null ? new Option<string?>(code) : default, message: message != null ? new Option<string?>(message) : default, application: application != null ? new Option<CreateApplicationResponseApplication?>(application) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateApplicationResponse value, Newtonsoft.Json.JsonSerializer serializer)
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

            if (value.ApplicationOption.IsSet && value.Application != null)
            {
                writer.WritePropertyName("application");
                serializer.Serialize(writer, value.Application);
            }

            writer.WriteEndObject();
        }
    }
}
