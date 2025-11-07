using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetOrganizationsResponse that handles the Option<> structure
    /// </summary>
    public class GetOrganizationsResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetOrganizationsResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetOrganizationsResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetOrganizationsResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? code = null;
            string? message = null;
            List<OrganizationItemSchema> organizations = null;
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

            if (jsonObject["organizations"] != null)
            {
                organizations = jsonObject["organizations"].ToObject<List<OrganizationItemSchema>>(serializer);
            }

            if (jsonObject["next_token"] != null)
            {
                nextToken = jsonObject["next_token"].ToObject<string>();
            }

            return new GetOrganizationsResponse(
                code: code != null ? new Option<string?>(code) : default, message: message != null ? new Option<string?>(message) : default, organizations: organizations != null ? new Option<List<OrganizationItemSchema>?>(organizations) : default, nextToken: nextToken != null ? new Option<string?>(nextToken) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetOrganizationsResponse value, Newtonsoft.Json.JsonSerializer serializer)
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

            if (value.OrganizationsOption.IsSet && value.Organizations != null)
            {
                writer.WritePropertyName("organizations");
                serializer.Serialize(writer, value.Organizations);
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
