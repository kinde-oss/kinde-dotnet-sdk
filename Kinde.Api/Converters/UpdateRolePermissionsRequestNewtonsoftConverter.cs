using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UpdateRolePermissionsRequest that handles the Option<> structure
    /// </summary>
    public class UpdateRolePermissionsRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UpdateRolePermissionsRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UpdateRolePermissionsRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UpdateRolePermissionsRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            List<UpdateRolePermissionsRequestPermissionsInner> permissions = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["permissions"] != null)
            {
                permissions = jsonObject["permissions"].ToObject<List<UpdateRolePermissionsRequestPermissionsInner>>(serializer);
            }

            return new UpdateRolePermissionsRequest(
                permissions: permissions != null ? new Option<List<UpdateRolePermissionsRequestPermissionsInner>?>(permissions) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UpdateRolePermissionsRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.PermissionsOption.IsSet && value.Permissions != null)
            {
                writer.WritePropertyName("permissions");
                serializer.Serialize(writer, value.Permissions);
            }

            writer.WriteEndObject();
        }
    }
}
