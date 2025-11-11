using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetUserMfaResponse that handles the Option<> structure
    /// </summary>
    public class GetUserMfaResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetUserMfaResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetUserMfaResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetUserMfaResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            GetUserMfaResponseMfa? mfa = default(GetUserMfaResponseMfa?);
            if (jsonObject["mfa"] != null)
            {
                mfa = jsonObject["mfa"].ToObject<GetUserMfaResponseMfa?>(serializer);
            }

            return new GetUserMfaResponse(
                message: message != null ? new Option<string?>(message) : default,                 code: code != null ? new Option<string?>(code) : default,                 mfa: mfa != null ? new Option<GetUserMfaResponseMfa?>(mfa) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetUserMfaResponse value, Newtonsoft.Json.JsonSerializer serializer)
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
            if (value.MfaOption.IsSet && value.Mfa != null)
            {
                writer.WritePropertyName("mfa");
                serializer.Serialize(writer, value.Mfa);
            }

            writer.WriteEndObject();
        }
    }
}