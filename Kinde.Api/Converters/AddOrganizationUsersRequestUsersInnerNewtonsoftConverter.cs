using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for AddOrganizationUsersRequestUsersInner that handles the Option<> structure
    /// </summary>
    public class AddOrganizationUsersRequestUsersInnerNewtonsoftConverter : Newtonsoft.Json.JsonConverter<AddOrganizationUsersRequestUsersInner>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override AddOrganizationUsersRequestUsersInner ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, AddOrganizationUsersRequestUsersInner existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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

            return new AddOrganizationUsersRequestUsersInner(
                id: id != null ? new Option<string?>(id) : default,                 roles: roles != null ? new Option<List<string>?>(roles) : default,                 permissions: permissions != null ? new Option<List<string>?>(permissions) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, AddOrganizationUsersRequestUsersInner value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IdOption.IsSet && value.Id != null)
            {
                writer.WritePropertyName("id");
                serializer.Serialize(writer, value.Id);
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