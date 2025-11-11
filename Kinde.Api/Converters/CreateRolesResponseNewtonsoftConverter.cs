using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateRolesResponse that handles the Option<> structure
    /// </summary>
    public class CreateRolesResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateRolesResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateRolesResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateRolesResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            CreateRolesResponseRole? role = default(CreateRolesResponseRole?);
            if (jsonObject["role"] != null)
            {
                role = jsonObject["role"].ToObject<CreateRolesResponseRole?>(serializer);
            }

            return new CreateRolesResponse(
                code: code != null ? new Option<string?>(code) : default,                 message: message != null ? new Option<string?>(message) : default,                 role: role != null ? new Option<CreateRolesResponseRole?>(role) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateRolesResponse value, Newtonsoft.Json.JsonSerializer serializer)
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
            if (value.RoleOption.IsSet && value.Role != null)
            {
                writer.WritePropertyName("role");
                serializer.Serialize(writer, value.Role);
            }

            writer.WriteEndObject();
        }
    }
}