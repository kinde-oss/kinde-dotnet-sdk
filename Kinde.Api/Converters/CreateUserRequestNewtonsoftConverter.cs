using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateUserRequest that handles the Option<> structure
    /// </summary>
    public class CreateUserRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateUserRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateUserRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateUserRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            CreateUserRequestProfile? profile = default(CreateUserRequestProfile?);
            if (jsonObject["profile"] != null)
            {
                profile = jsonObject["profile"].ToObject<CreateUserRequestProfile?>(serializer);
            }
            string? organizationCode = default(string?);
            if (jsonObject["organization_code"] != null)
            {
                organizationCode = jsonObject["organization_code"].ToObject<string?>();
            }
            string? providedId = default(string?);
            if (jsonObject["provided_id"] != null)
            {
                providedId = jsonObject["provided_id"].ToObject<string?>();
            }
            List<CreateUserRequestIdentitiesInner> identities = default(List<CreateUserRequestIdentitiesInner>);
            if (jsonObject["identities"] != null)
            {
                identities = jsonObject["identities"].ToObject<List<CreateUserRequestIdentitiesInner>>(serializer);
            }

            return new CreateUserRequest(
                profile: profile != null ? new Option<CreateUserRequestProfile?>(profile) : default,                 organizationCode: organizationCode != null ? new Option<string?>(organizationCode) : default,                 providedId: providedId != null ? new Option<string?>(providedId) : default,                 identities: identities != null ? new Option<List<CreateUserRequestIdentitiesInner>?>(identities) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateUserRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.ProfileOption.IsSet && value.Profile != null)
            {
                writer.WritePropertyName("profile");
                serializer.Serialize(writer, value.Profile);
            }
            if (value.OrganizationCodeOption.IsSet && value.OrganizationCode != null)
            {
                writer.WritePropertyName("organization_code");
                serializer.Serialize(writer, value.OrganizationCode);
            }
            if (value.ProvidedIdOption.IsSet && value.ProvidedId != null)
            {
                writer.WritePropertyName("provided_id");
                serializer.Serialize(writer, value.ProvidedId);
            }
            if (value.IdentitiesOption.IsSet)
            {
                writer.WritePropertyName("identities");
                serializer.Serialize(writer, value.Identities);
            }

            writer.WriteEndObject();
        }
    }
}