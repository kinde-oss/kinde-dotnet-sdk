using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for OrganizationUserRolePermissionsPermissions that handles the Option<> structure
    /// </summary>
    public class OrganizationUserRolePermissionsPermissionsNewtonsoftConverter : Newtonsoft.Json.JsonConverter<OrganizationUserRolePermissionsPermissions>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override OrganizationUserRolePermissionsPermissions ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, OrganizationUserRolePermissionsPermissions existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? key = default(string?);
            if (jsonObject["key"] != null)
            {
                key = jsonObject["key"].ToObject<string?>();
            }

            return new OrganizationUserRolePermissionsPermissions(
                key: key != null ? new Option<string?>(key) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, OrganizationUserRolePermissionsPermissions value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.KeyOption.IsSet && value.Key != null)
            {
                writer.WritePropertyName("key");
                serializer.Serialize(writer, value.Key);
            }

            writer.WriteEndObject();
        }
    }
}