using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetOrganizationsUserRolesResponse that handles the Option<> structure
    /// </summary>
    public class GetOrganizationsUserRolesResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetOrganizationsUserRolesResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetOrganizationsUserRolesResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetOrganizationsUserRolesResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? code = default(string?);
            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string?>();
            }
            string? message = default(string?);
            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string?>();
            }
            List<OrganizationUserRole> roles = default(List<OrganizationUserRole>);
            if (jsonObject["roles"] != null)
            {
                roles = jsonObject["roles"].ToObject<List<OrganizationUserRole>>(serializer);
            }
            string? nextToken = default(string?);
            if (jsonObject["next_token"] != null)
            {
                nextToken = jsonObject["next_token"].ToObject<string?>();
            }

            return new GetOrganizationsUserRolesResponse(
                code: code != null ? new Option<string?>(code) : default,                 message: message != null ? new Option<string?>(message) : default,                 roles: roles != null ? new Option<List<OrganizationUserRole>?>(roles) : default,                 nextToken: nextToken != null ? new Option<string?>(nextToken) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetOrganizationsUserRolesResponse value, Newtonsoft.Json.JsonSerializer serializer)
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
            if (value.RolesOption.IsSet)
            {
                writer.WritePropertyName("roles");
                serializer.Serialize(writer, value.Roles);
            }
            if (value.NextTokenOption.IsSet && value.NextToken != null)
            {
                writer.WritePropertyName("next_token");
                serializer.Serialize(writer, value.NextToken);
            }

            writer.WriteEndObject();
        }
    }
}