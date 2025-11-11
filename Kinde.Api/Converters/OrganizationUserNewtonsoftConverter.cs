using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for OrganizationUser that handles the Option<> structure
    /// </summary>
    public class OrganizationUserNewtonsoftConverter : Newtonsoft.Json.JsonConverter<OrganizationUser>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override OrganizationUser ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, OrganizationUser existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? id = default(string?);
            if (jsonObject["id"] != null)
            {
                id = jsonObject["id"].ToObject<string?>();
            }
            string? email = default(string?);
            if (jsonObject["email"] != null)
            {
                email = jsonObject["email"].ToObject<string?>();
            }
            string? fullName = default(string?);
            if (jsonObject["full_name"] != null)
            {
                fullName = jsonObject["full_name"].ToObject<string?>();
            }
            string? lastName = default(string?);
            if (jsonObject["last_name"] != null)
            {
                lastName = jsonObject["last_name"].ToObject<string?>();
            }
            string? firstName = default(string?);
            if (jsonObject["first_name"] != null)
            {
                firstName = jsonObject["first_name"].ToObject<string?>();
            }
            string? picture = default(string?);
            if (jsonObject["picture"] != null)
            {
                picture = jsonObject["picture"].ToObject<string?>();
            }
            string? joinedOn = default(string?);
            if (jsonObject["joined_on"] != null)
            {
                joinedOn = jsonObject["joined_on"].ToObject<string?>();
            }
            string? lastAccessedOn = default(string?);
            if (jsonObject["last_accessed_on"] != null)
            {
                lastAccessedOn = jsonObject["last_accessed_on"].ToObject<string?>();
            }
            List<string> roles = default(List<string>);
            if (jsonObject["roles"] != null)
            {
                roles = jsonObject["roles"].ToObject<List<string>>(serializer);
            }

            return new OrganizationUser(
                id: id != null ? new Option<string?>(id) : default,                 email: email != null ? new Option<string?>(email) : default,                 fullName: fullName != null ? new Option<string?>(fullName) : default,                 lastName: lastName != null ? new Option<string?>(lastName) : default,                 firstName: firstName != null ? new Option<string?>(firstName) : default,                 picture: picture != null ? new Option<string?>(picture) : default,                 joinedOn: joinedOn != null ? new Option<string?>(joinedOn) : default,                 lastAccessedOn: lastAccessedOn != null ? new Option<string?>(lastAccessedOn) : default,                 roles: roles != null ? new Option<List<string>?>(roles) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, OrganizationUser value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IdOption.IsSet && value.Id != null)
            {
                writer.WritePropertyName("id");
                serializer.Serialize(writer, value.Id);
            }
            if (value.EmailOption.IsSet && value.Email != null)
            {
                writer.WritePropertyName("email");
                serializer.Serialize(writer, value.Email);
            }
            if (value.FullNameOption.IsSet && value.FullName != null)
            {
                writer.WritePropertyName("full_name");
                serializer.Serialize(writer, value.FullName);
            }
            if (value.LastNameOption.IsSet && value.LastName != null)
            {
                writer.WritePropertyName("last_name");
                serializer.Serialize(writer, value.LastName);
            }
            if (value.FirstNameOption.IsSet && value.FirstName != null)
            {
                writer.WritePropertyName("first_name");
                serializer.Serialize(writer, value.FirstName);
            }
            if (value.PictureOption.IsSet && value.Picture != null)
            {
                writer.WritePropertyName("picture");
                serializer.Serialize(writer, value.Picture);
            }
            if (value.JoinedOnOption.IsSet && value.JoinedOn != null)
            {
                writer.WritePropertyName("joined_on");
                serializer.Serialize(writer, value.JoinedOn);
            }
            if (value.LastAccessedOnOption.IsSet && value.LastAccessedOn != null)
            {
                writer.WritePropertyName("last_accessed_on");
                serializer.Serialize(writer, value.LastAccessedOn);
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