using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateOrganizationUserPermissionRequest that handles the Option<> structure
    /// </summary>
    public class CreateOrganizationUserPermissionRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateOrganizationUserPermissionRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateOrganizationUserPermissionRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateOrganizationUserPermissionRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? permissionId = default(string?);
            if (jsonObject["permission_id"] != null)
            {
                permissionId = jsonObject["permission_id"].ToObject<string?>();
            }

            return new CreateOrganizationUserPermissionRequest(
                permissionId: permissionId != null ? new Option<string?>(permissionId) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateOrganizationUserPermissionRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.PermissionIdOption.IsSet && value.PermissionId != null)
            {
                writer.WritePropertyName("permission_id");
                serializer.Serialize(writer, value.PermissionId);
            }

            writer.WriteEndObject();
        }
    }
}