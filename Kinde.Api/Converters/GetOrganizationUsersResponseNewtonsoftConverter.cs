using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetOrganizationUsersResponse that handles the Option<> structure
    /// </summary>
    public class GetOrganizationUsersResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetOrganizationUsersResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetOrganizationUsersResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetOrganizationUsersResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? code = null;
            string? message = null;
            List<OrganizationUser> organizationUsers = null;
            string? nextToken = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string>();
            }

            if (jsonObject["organization_users"] != null)
            {
                organizationUsers = jsonObject["organization_users"].ToObject<List<OrganizationUser>>(serializer);
            }

            if (jsonObject["next_token"] != null)
            {
                nextToken = jsonObject["next_token"].ToObject<string>();
            }

            return new GetOrganizationUsersResponse(
                code: code != null ? new Option<string?>(code) : default, message: message != null ? new Option<string?>(message) : default, organizationUsers: organizationUsers != null ? new Option<List<OrganizationUser>?>(organizationUsers) : default, nextToken: nextToken != null ? new Option<string?>(nextToken) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetOrganizationUsersResponse value, Newtonsoft.Json.JsonSerializer serializer)
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

            if (value.OrganizationUsersOption.IsSet && value.OrganizationUsers != null)
            {
                writer.WritePropertyName("organization_users");
                serializer.Serialize(writer, value.OrganizationUsers);
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
