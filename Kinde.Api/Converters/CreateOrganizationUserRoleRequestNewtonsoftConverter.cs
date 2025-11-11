using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateOrganizationUserRoleRequest that handles the Option<> structure
    /// </summary>
    public class CreateOrganizationUserRoleRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateOrganizationUserRoleRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateOrganizationUserRoleRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateOrganizationUserRoleRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? roleId = default(string?);
            if (jsonObject["role_id"] != null)
            {
                roleId = jsonObject["role_id"].ToObject<string?>();
            }

            return new CreateOrganizationUserRoleRequest(
                roleId: roleId != null ? new Option<string?>(roleId) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateOrganizationUserRoleRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.RoleIdOption.IsSet && value.RoleId != null)
            {
                writer.WritePropertyName("role_id");
                serializer.Serialize(writer, value.RoleId);
            }

            writer.WriteEndObject();
        }
    }
}