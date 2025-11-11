using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Accounts.Model;
using Kinde.Accounts.Client;

namespace Kinde.Api.Accounts.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetUserPermissionsResponseData that handles the Option<> structure
    /// </summary>
    public class GetUserPermissionsResponseDataNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetUserPermissionsResponseData>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetUserPermissionsResponseData ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetUserPermissionsResponseData existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? orgCode = default(string?);
            if (jsonObject["org_code"] != null)
            {
                orgCode = jsonObject["org_code"].ToObject<string?>();
            }
            List<GetUserPermissionsResponseDataPermissionsInner> permissions = default(List<GetUserPermissionsResponseDataPermissionsInner>);
            if (jsonObject["permissions"] != null)
            {
                permissions = jsonObject["permissions"].ToObject<List<GetUserPermissionsResponseDataPermissionsInner>>(serializer);
            }

            return new GetUserPermissionsResponseData(
                orgCode: orgCode != null ? new Option<string?>(orgCode) : default,                 permissions: permissions != null ? new Option<List<GetUserPermissionsResponseDataPermissionsInner>?>(permissions) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetUserPermissionsResponseData value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.OrgCodeOption.IsSet && value.OrgCode != null)
            {
                writer.WritePropertyName("org_code");
                serializer.Serialize(writer, value.OrgCode);
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