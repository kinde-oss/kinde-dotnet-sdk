using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for RolePermissionsResponse that handles the Option<> structure
    /// </summary>
    public class RolePermissionsResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<RolePermissionsResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override RolePermissionsResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, RolePermissionsResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? code = null;
            string? message = null;
            List<Permissions> permissions = null;
            string? nextToken = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string>();
            }

            if (jsonObject["permissions"] != null)
            {
                permissions = jsonObject["permissions"].ToObject<List<Permissions>>(serializer);
            }

            if (jsonObject["next_token"] != null)
            {
                nextToken = jsonObject["next_token"].ToObject<string>();
            }

            return new RolePermissionsResponse(
                code: code != null ? new Option<string?>(code) : default, message: message != null ? new Option<string?>(message) : default, permissions: permissions != null ? new Option<List<Permissions>?>(permissions) : default, nextToken: nextToken != null ? new Option<string?>(nextToken) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, RolePermissionsResponse value, Newtonsoft.Json.JsonSerializer serializer)
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

            if (value.PermissionsOption.IsSet && value.Permissions != null)
            {
                writer.WritePropertyName("permissions");
                serializer.Serialize(writer, value.Permissions);
            }

            if (value.NextTokenOption.IsSet && value.NextToken != null)
            {
                writer.WritePropertyName("next_token");
                serializer.Serialize(writer, value.NextToken);
            }

            writer.WriteEndObject();
        }
    }
}
