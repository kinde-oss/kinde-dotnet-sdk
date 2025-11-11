using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UpdateUserRequest that handles the Option<> structure
    /// </summary>
    public class UpdateUserRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UpdateUserRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UpdateUserRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UpdateUserRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

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
            string? picture = default(string?);
            if (jsonObject["picture"] != null)
            {
                picture = jsonObject["picture"].ToObject<string?>();
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
            string? providedId = default(string?);
            if (jsonObject["provided_id"] != null)
            {
                providedId = jsonObject["provided_id"].ToObject<string?>();
            }

            return new UpdateUserRequest(
                givenName: givenName != null ? new Option<string?>(givenName) : default,                 familyName: familyName != null ? new Option<string?>(familyName) : default,                 picture: picture != null ? new Option<string?>(picture) : default,                 isSuspended: isSuspended != null ? new Option<bool?>(isSuspended) : default,                 isPasswordResetRequested: isPasswordResetRequested != null ? new Option<bool?>(isPasswordResetRequested) : default,                 providedId: providedId != null ? new Option<string?>(providedId) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UpdateUserRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

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
            if (value.PictureOption.IsSet && value.Picture != null)
            {
                writer.WritePropertyName("picture");
                serializer.Serialize(writer, value.Picture);
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
            if (value.ProvidedIdOption.IsSet && value.ProvidedId != null)
            {
                writer.WritePropertyName("provided_id");
                serializer.Serialize(writer, value.ProvidedId);
            }

            writer.WriteEndObject();
        }
    }
}