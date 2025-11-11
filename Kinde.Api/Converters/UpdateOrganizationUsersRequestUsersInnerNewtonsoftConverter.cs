using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UpdateOrganizationUsersRequestUsersInner that handles the Option<> structure
    /// </summary>
    public class UpdateOrganizationUsersRequestUsersInnerNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UpdateOrganizationUsersRequestUsersInner>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UpdateOrganizationUsersRequestUsersInner ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UpdateOrganizationUsersRequestUsersInner existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            string? operation = default(string?);
            if (jsonObject["operation"] != null)
            {
                operation = jsonObject["operation"].ToObject<string?>();
            }
            List<string> roles = default(List<string>);
            if (jsonObject["roles"] != null)
            {
                roles = jsonObject["roles"].ToObject<List<string>>(serializer);
            }
            List<string> permissions = default(List<string>);
            if (jsonObject["permissions"] != null)
            {
                permissions = jsonObject["permissions"].ToObject<List<string>>(serializer);
            }

            return new UpdateOrganizationUsersRequestUsersInner(
                id: id != null ? new Option<string?>(id) : default,                 operation: operation != null ? new Option<string?>(operation) : default,                 roles: roles != null ? new Option<List<string>?>(roles) : default,                 permissions: permissions != null ? new Option<List<string>?>(permissions) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UpdateOrganizationUsersRequestUsersInner value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IdOption.IsSet && value.Id != null)
            {
                writer.WritePropertyName("id");
                serializer.Serialize(writer, value.Id);
            }
            if (value.OperationOption.IsSet && value.Operation != null)
            {
                writer.WritePropertyName("operation");
                serializer.Serialize(writer, value.Operation);
            }
            if (value.RolesOption.IsSet)
            {
                writer.WritePropertyName("roles");
                serializer.Serialize(writer, value.Roles);
            }
            if (value.PermissionsOption.IsSet)
            {
                writer.WritePropertyName("permissions");
                serializer.Serialize(writer, value.Permissions);
            }

            writer.WriteEndObject();
        }
    }
}