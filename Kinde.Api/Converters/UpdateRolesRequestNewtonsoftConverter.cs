using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UpdateRolesRequest that handles the Option<> structure
    /// </summary>
    public class UpdateRolesRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UpdateRolesRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UpdateRolesRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UpdateRolesRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string name = default(string);
            string key = default(string);
            string? description = null;
            bool? isDefaultRole = null;
            Guid? assignmentPermissionId = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string>();
            }

            if (jsonObject["key"] != null)
            {
                key = jsonObject["key"].ToObject<string>();
            }

            if (jsonObject["description"] != null)
            {
                description = jsonObject["description"].ToObject<string>();
            }

            if (jsonObject["is_default_role"] != null)
            {
                isDefaultRole = jsonObject["is_default_role"].ToObject<bool?>();
            }

            if (jsonObject["assignment_permission_id"] != null)
            {
                assignmentPermissionId = jsonObject["assignment_permission_id"].ToObject<Guid>(serializer);
            }

            return new UpdateRolesRequest(
                name: name, key: key, description: description != null ? new Option<string?>(description) : default, isDefaultRole: isDefaultRole != null ? new Option<bool?>(isDefaultRole) : default, assignmentPermissionId: assignmentPermissionId != null ? new Option<Guid?>(assignmentPermissionId) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UpdateRolesRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.DescriptionOption.IsSet && value.Description != null)
            {
                writer.WritePropertyName("description");
                serializer.Serialize(writer, value.Description);
            }

            if (value.IsDefaultRoleOption.IsSet && value.IsDefaultRole != null)
            {
                writer.WritePropertyName("is_default_role");
                writer.WriteValue(value.IsDefaultRole.Value);
            }

            if (value.AssignmentPermissionIdOption.IsSet && value.AssignmentPermissionId != null)
            {
                writer.WritePropertyName("assignment_permission_id");
                serializer.Serialize(writer, value.AssignmentPermissionId);
            }

            writer.WriteEndObject();
        }
    }
}
