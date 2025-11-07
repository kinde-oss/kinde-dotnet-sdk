using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for AuthorizeAppApiResponse that handles the Option<> structure
    /// </summary>
    public class AuthorizeAppApiResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<AuthorizeAppApiResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override AuthorizeAppApiResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, AuthorizeAppApiResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? message = null;
            string? code = null;
            List<string> applicationsDisconnected = null;
            List<string> applicationsConnected = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string>();
            }

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["applications_disconnected"] != null)
            {
                applicationsDisconnected = jsonObject["applications_disconnected"].ToObject<List<string>>(serializer);
            }

            if (jsonObject["applications_connected"] != null)
            {
                applicationsConnected = jsonObject["applications_connected"].ToObject<List<string>>(serializer);
            }

            return new AuthorizeAppApiResponse(
                message: message != null ? new Option<string?>(message) : default, code: code != null ? new Option<string?>(code) : default, applicationsDisconnected: applicationsDisconnected != null ? new Option<List<string>?>(applicationsDisconnected) : default, applicationsConnected: applicationsConnected != null ? new Option<List<string>?>(applicationsConnected) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, AuthorizeAppApiResponse value, Newtonsoft.Json.JsonSerializer serializer)
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

            if (value.ApplicationsDisconnectedOption.IsSet && value.ApplicationsDisconnected != null)
            {
                writer.WritePropertyName("applications_disconnected");
                serializer.Serialize(writer, value.ApplicationsDisconnected);
            }

            if (value.ApplicationsConnectedOption.IsSet && value.ApplicationsConnected != null)
            {
                writer.WritePropertyName("applications_connected");
                serializer.Serialize(writer, value.ApplicationsConnected);
            }

            writer.WriteEndObject();
        }
    }
}
