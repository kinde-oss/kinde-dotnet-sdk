using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetOrganizationsUserPermissionsResponse that handles the Option<> structure
    /// </summary>
    public class GetOrganizationsUserPermissionsResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetOrganizationsUserPermissionsResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetOrganizationsUserPermissionsResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetOrganizationsUserPermissionsResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? code = null;
            string? message = null;
            List<OrganizationUserPermission> permissions = null;

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
                permissions = jsonObject["permissions"].ToObject<List<OrganizationUserPermission>>(serializer);
            }

            return new GetOrganizationsUserPermissionsResponse(
                code: code != null ? new Option<string?>(code) : default, message: message != null ? new Option<string?>(message) : default, permissions: permissions != null ? new Option<List<OrganizationUserPermission>?>(permissions) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetOrganizationsUserPermissionsResponse value, Newtonsoft.Json.JsonSerializer serializer)
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

            writer.WriteEndObject();
        }
    }
}
