using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Accounts.Model;
using Kinde.Accounts.Client;

namespace Kinde.Api.Accounts.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetUserRolesResponseData that handles the Option<> structure
    /// </summary>
    public class GetUserRolesResponseDataNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetUserRolesResponseData>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetUserRolesResponseData ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetUserRolesResponseData existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            List<GetUserRolesResponseDataRolesInner> roles = default(List<GetUserRolesResponseDataRolesInner>);
            if (jsonObject["roles"] != null)
            {
                roles = jsonObject["roles"].ToObject<List<GetUserRolesResponseDataRolesInner>>(serializer);
            }

            return new GetUserRolesResponseData(
                orgCode: orgCode != null ? new Option<string?>(orgCode) : default,                 roles: roles != null ? new Option<List<GetUserRolesResponseDataRolesInner>?>(roles) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetUserRolesResponseData value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.OrgCodeOption.IsSet && value.OrgCode != null)
            {
                writer.WritePropertyName("org_code");
                serializer.Serialize(writer, value.OrgCode);
            }
            if (value.RolesOption.IsSet)
            {
                writer.WritePropertyName("roles");
                serializer.Serialize(writer, value.Roles);
            }

            writer.WriteEndObject();
        }
    }
}