using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UpdateUserResponse that handles the Option<> structure
    /// </summary>
    public class UpdateUserResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UpdateUserResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UpdateUserResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UpdateUserResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            string? email = default(string?);
            if (jsonObject["email"] != null)
            {
                email = jsonObject["email"].ToObject<string?>();
            }
            bool? isSuspended = default(bool?);
            if (jsonObject["is_suspended"] != null)
            {
                isSuspended = jsonObject["is_suspended"].ToObject<bool?>(serializer);
            }
            bool? isPasswordResetRequested = default(bool?);
            if (jsonObject["is_password_reset_requested"] != null)
            {
                isPasswordResetRequested = jsonObject["is_password_reset_requested"].ToObject<bool?>(serializer);
            }
            string? picture = default(string?);
            if (jsonObject["picture"] != null)
            {
                picture = jsonObject["picture"].ToObject<string?>();
            }

            return new UpdateUserResponse(
                id: id != null ? new Option<string?>(id) : default,                 givenName: givenName != null ? new Option<string?>(givenName) : default,                 familyName: familyName != null ? new Option<string?>(familyName) : default,                 email: email != null ? new Option<string?>(email) : default,                 isSuspended: isSuspended != null ? new Option<bool?>(isSuspended) : default,                 isPasswordResetRequested: isPasswordResetRequested != null ? new Option<bool?>(isPasswordResetRequested) : default,                 picture: picture != null ? new Option<string?>(picture) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UpdateUserResponse value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IdOption.IsSet && value.Id != null)
            {
                writer.WritePropertyName("id");
                serializer.Serialize(writer, value.Id);
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
            if (value.EmailOption.IsSet && value.Email != null)
            {
                writer.WritePropertyName("email");
                serializer.Serialize(writer, value.Email);
            }
            if (value.IsSuspendedOption.IsSet && value.IsSuspended != null)
            {
                writer.WritePropertyName("is_suspended");
                serializer.Serialize(writer, value.IsSuspended);
            }
            if (value.IsPasswordResetRequestedOption.IsSet && value.IsPasswordResetRequested != null)
            {
                writer.WritePropertyName("is_password_reset_requested");
                serializer.Serialize(writer, value.IsPasswordResetRequested);
            }
            if (value.PictureOption.IsSet && value.Picture != null)
            {
                writer.WritePropertyName("picture");
                serializer.Serialize(writer, value.Picture);
            }

            writer.WriteEndObject();
        }
    }
}