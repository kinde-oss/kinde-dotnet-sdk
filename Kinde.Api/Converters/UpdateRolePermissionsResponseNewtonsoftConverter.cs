using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UpdateRolePermissionsResponse that handles the Option<> structure
    /// </summary>
    public class UpdateRolePermissionsResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UpdateRolePermissionsResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UpdateRolePermissionsResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UpdateRolePermissionsResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? code = null;
            string? message = null;
            List<string> permissionsAdded = null;
            List<string> permissionsRemoved = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string>();
            }

            if (jsonObject["permissions_added"] != null)
            {
                permissionsAdded = jsonObject["permissions_added"].ToObject<List<string>>(serializer);
            }

            if (jsonObject["permissions_removed"] != null)
            {
                permissionsRemoved = jsonObject["permissions_removed"].ToObject<List<string>>(serializer);
            }

            return new UpdateRolePermissionsResponse(
                code: code != null ? new Option<string?>(code) : default, message: message != null ? new Option<string?>(message) : default, permissionsAdded: permissionsAdded != null ? new Option<List<string>?>(permissionsAdded) : default, permissionsRemoved: permissionsRemoved != null ? new Option<List<string>?>(permissionsRemoved) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UpdateRolePermissionsResponse value, Newtonsoft.Json.JsonSerializer serializer)
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

            if (value.PermissionsAddedOption.IsSet && value.PermissionsAdded != null)
            {
                writer.WritePropertyName("permissions_added");
                serializer.Serialize(writer, value.PermissionsAdded);
            }

            if (value.PermissionsRemovedOption.IsSet && value.PermissionsRemoved != null)
            {
                writer.WritePropertyName("permissions_removed");
                serializer.Serialize(writer, value.PermissionsRemoved);
            }

            writer.WriteEndObject();
        }
    }
}
