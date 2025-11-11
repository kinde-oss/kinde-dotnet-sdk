using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for OrganizationUserRolePermissions that handles the Option<> structure
    /// </summary>
    public class OrganizationUserRolePermissionsNewtonsoftConverter : Newtonsoft.Json.JsonConverter<OrganizationUserRolePermissions>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override OrganizationUserRolePermissions ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, OrganizationUserRolePermissions existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? id = default(string?);
            if (jsonObject["id"] != null)
            {
                id = jsonObject["id"].ToObject<string?>();
            }
            string? role = default(string?);
            if (jsonObject["role"] != null)
            {
                role = jsonObject["role"].ToObject<string?>();
            }
            OrganizationUserRolePermissionsPermissions? permissions = default(OrganizationUserRolePermissionsPermissions?);
            if (jsonObject["permissions"] != null)
            {
                permissions = jsonObject["permissions"].ToObject<OrganizationUserRolePermissionsPermissions?>(serializer);
            }

            return new OrganizationUserRolePermissions(
                id: id != null ? new Option<string?>(id) : default,                 role: role != null ? new Option<string?>(role) : default,                 permissions: permissions != null ? new Option<OrganizationUserRolePermissionsPermissions?>(permissions) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, OrganizationUserRolePermissions value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IdOption.IsSet && value.Id != null)
            {
                writer.WritePropertyName("id");
                serializer.Serialize(writer, value.Id);
            }
            if (value.RoleOption.IsSet && value.Role != null)
            {
                writer.WritePropertyName("role");
                serializer.Serialize(writer, value.Role);
            }
            if (value.PermissionsOption.IsSet && value.Permissions != null)
            {
                writer.WritePropertyName("permissions");
                serializer.Serialize(writer, value.Permissions);
            }

            writer.WriteEndObject();
        }
    }
}