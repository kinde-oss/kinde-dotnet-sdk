using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetApplicationResponse that handles the Option<> structure
    /// </summary>
    public class GetApplicationResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetApplicationResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetApplicationResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetApplicationResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            GetApplicationResponseApplication? application = default(GetApplicationResponseApplication?);
            if (jsonObject["application"] != null)
            {
                application = jsonObject["application"].ToObject<GetApplicationResponseApplication?>(serializer);
            }

            return new GetApplicationResponse(
                code: code != null ? new Option<string?>(code) : default,                 message: message != null ? new Option<string?>(message) : default,                 application: application != null ? new Option<GetApplicationResponseApplication?>(application) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetApplicationResponse value, Newtonsoft.Json.JsonSerializer serializer)
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