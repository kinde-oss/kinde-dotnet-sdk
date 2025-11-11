using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Accounts.Model;
using Kinde.Accounts.Client;

namespace Kinde.Api.Accounts.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UserProfileV2 that handles the Option<> structure
    /// </summary>
    public class UserProfileV2NewtonsoftConverter : Newtonsoft.Json.JsonConverter<UserProfileV2>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UserProfileV2 ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UserProfileV2 existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? sub = default(string?);
            if (jsonObject["sub"] != null)
            {
                sub = jsonObject["sub"].ToObject<string?>();
            }
            string? providedId = default(string?);
            if (jsonObject["provided_id"] != null)
            {
                providedId = jsonObject["provided_id"].ToObject<string?>();
            }
            string? name = default(string?);
            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string?>();
            }
            string? givenName = default(string?);
            if (jsonObject["given_name"] != null)
            {
                givenName = jsonObject["given_name"].ToObject<string?>();
            }
            string? familyName = default(string?);
            if (jsonObject["family_name"] != null)
            {
                familyName = jsonObject["family_name"].ToObject<string?>();
            }
            int? updatedAt = default(int?);
            if (jsonObject["updated_at"] != null)
            {
                updatedAt = jsonObject["updated_at"].ToObject<int?>(serializer);
            }
            string? email = default(string?);
            if (jsonObject["email"] != null)
            {
                email = jsonObject["email"].ToObject<string?>();
            }
            bool? emailVerified = default(bool?);
            if (jsonObject["email_verified"] != null)
            {
                emailVerified = jsonObject["email_verified"].ToObject<bool?>(serializer);
            }
            string? picture = default(string?);
            if (jsonObject["picture"] != null)
            {
                picture = jsonObject["picture"].ToObject<string?>();
            }
            string? preferredUsername = default(string?);
            if (jsonObject["preferred_username"] != null)
            {
                preferredUsername = jsonObject["preferred_username"].ToObject<string?>();
            }
            string? id = default(string?);
            if (jsonObject["id"] != null)
            {
                id = jsonObject["id"].ToObject<string?>();
            }

            return new UserProfileV2(
                sub: sub != null ? new Option<string?>(sub) : default,                 providedId: providedId != null ? new Option<string?>(providedId) : default,                 name: name != null ? new Option<string?>(name) : default,                 givenName: givenName != null ? new Option<string?>(givenName) : default,                 familyName: familyName != null ? new Option<string?>(familyName) : default,                 updatedAt: updatedAt != null ? new Option<int?>(updatedAt) : default,                 email: email != null ? new Option<string?>(email) : default,                 emailVerified: emailVerified != null ? new Option<bool?>(emailVerified) : default,                 picture: picture != null ? new Option<string?>(picture) : default,                 preferredUsername: preferredUsername != null ? new Option<string?>(preferredUsername) : default,                 id: id != null ? new Option<string?>(id) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UserProfileV2 value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.SubOption.IsSet && value.Sub != null)
            {
                writer.WritePropertyName("sub");
                serializer.Serialize(writer, value.Sub);
            }
            if (value.ProvidedIdOption.IsSet && value.ProvidedId != null)
            {
                writer.WritePropertyName("provided_id");
                serializer.Serialize(writer, value.ProvidedId);
            }
            if (value.NameOption.IsSet && value.Name != null)
            {
                writer.WritePropertyName("name");
                serializer.Serialize(writer, value.Name);
            }
            if (value.GivenNameOption.IsSet && value.GivenName != null)
            {
                writer.WritePropertyName("given_name");
                serializer.Serialize(writer, value.GivenName);
            }
            if (value.FamilyNameOption.IsSet && value.FamilyName != null)
            {
                writer.WritePropertyName("family_name");
                serializer.Serialize(writer, value.FamilyName);
            }
            if (value.UpdatedAtOption.IsSet && value.UpdatedAt != null)
            {
                writer.WritePropertyName("updated_at");
                serializer.Serialize(writer, value.UpdatedAt);
            }
            if (value.EmailOption.IsSet && value.Email != null)
            {
                writer.WritePropertyName("email");
                serializer.Serialize(writer, value.Email);
            }
            if (value.EmailVerifiedOption.IsSet && value.EmailVerified != null)
            {
                writer.WritePropertyName("email_verified");
                serializer.Serialize(writer, value.EmailVerified);
            }
            if (value.PictureOption.IsSet && value.Picture != null)
            {
                writer.WritePropertyName("picture");
                serializer.Serialize(writer, value.Picture);
            }
            if (value.PreferredUsernameOption.IsSet && value.PreferredUsername != null)
            {
                writer.WritePropertyName("preferred_username");
                serializer.Serialize(writer, value.PreferredUsername);
            }
            if (value.IdOption.IsSet && value.Id != null)
            {
                writer.WritePropertyName("id");
                serializer.Serialize(writer, value.Id);
            }

            writer.WriteEndObject();
        }
    }
}