using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetRoleResponse that handles the Option<> structure
    /// </summary>
    public class GetRoleResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetRoleResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetRoleResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetRoleResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            GetRoleResponseRole? role = default(GetRoleResponseRole?);
            if (jsonObject["role"] != null)
            {
                role = jsonObject["role"].ToObject<GetRoleResponseRole?>(serializer);
            }

            return new GetRoleResponse(
                code: code != null ? new Option<string?>(code) : default,                 message: message != null ? new Option<string?>(message) : default,                 role: role != null ? new Option<GetRoleResponseRole?>(role) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetRoleResponse value, Newtonsoft.Json.JsonSerializer serializer)
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