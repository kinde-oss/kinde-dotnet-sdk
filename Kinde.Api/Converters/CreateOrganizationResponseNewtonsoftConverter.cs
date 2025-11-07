using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateOrganizationResponse that handles the Option<> structure
    /// </summary>
    public class CreateOrganizationResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateOrganizationResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateOrganizationResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateOrganizationResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? message = null;
            string? code = null;
            CreateOrganizationResponseOrganization? organization = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string>();
            }

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["organization"] != null)
            {
                organization = jsonObject["organization"].ToObject<CreateOrganizationResponseOrganization>(serializer);
            }

            return new CreateOrganizationResponse(
                message: message != null ? new Option<string?>(message) : default, code: code != null ? new Option<string?>(code) : default, organization: organization != null ? new Option<CreateOrganizationResponseOrganization?>(organization) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateOrganizationResponse value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.MessageOption.IsSet && value.Message != null)
            {
                writer.WritePropertyName("message");
                serializer.Serialize(writer, value.Message);
            }

            if (value.CodeOption.IsSet && value.Code != null)
            {
                writer.WritePropertyName("code");
                serializer.Serialize(writer, value.Code);
            }

            if (value.OrganizationOption.IsSet && value.Organization != null)
            {
                writer.WritePropertyName("organization");
                serializer.Serialize(writer, value.Organization);
            }

            writer.WriteEndObject();
        }
    }
}
