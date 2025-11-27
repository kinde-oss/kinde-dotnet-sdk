#nullable enable

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kinde.Accounts.Model.Entities;

namespace Kinde.Accounts.Model.Entities
{
    /// <summary>
    /// JSON converter for <see cref="UserProfileV2" />
    /// </summary>
public class UserProfileV2JsonConverter : JsonConverter<UserProfileV2>
    {
        /// <summary>
        /// Deserializes json to <see cref="UserProfileV2" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override UserProfileV2 Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            string? email = default;
            bool? emailVerified = default;
            string? familyName = default;
            string? givenName = default;
            string? id = default;
            string? name = default;
            string? sub = default;
            int? updatedAt = default;
            string? picture = default;
            string? preferredUsername = default;
            string? providedId = default;

            while (utf8JsonReader.Read())
            {
                if (startingTokenType == JsonTokenType.StartObject && utf8JsonReader.TokenType == JsonTokenType.EndObject && currentDepth == utf8JsonReader.CurrentDepth)
                    break;

                if (startingTokenType == JsonTokenType.StartArray && utf8JsonReader.TokenType == JsonTokenType.EndArray && currentDepth == utf8JsonReader.CurrentDepth)
                    break;

                if (utf8JsonReader.TokenType == JsonTokenType.PropertyName && currentDepth == utf8JsonReader.CurrentDepth - 1)
                {
                    string? localVarJsonPropertyName = utf8JsonReader.GetString();
                    utf8JsonReader.Read();

                    switch (localVarJsonPropertyName)
                    {
                        case "email":
                            email = utf8JsonReader.GetString();
                            break;
                        case "email_verified":
                            if (utf8JsonReader.TokenType != JsonTokenType.Null)
                                emailVerified = utf8JsonReader.GetBoolean();
                            break;
                        case "family_name":
                            familyName = utf8JsonReader.GetString();
                            break;
                        case "given_name":
                            givenName = utf8JsonReader.GetString();
                            break;
                        case "id":
                            id = utf8JsonReader.GetString();
                            break;
                        case "name":
                            name = utf8JsonReader.GetString();
                            break;
                        case "sub":
                            sub = utf8JsonReader.GetString();
                            break;
                        case "updated_at":
                            if (utf8JsonReader.TokenType != JsonTokenType.Null)
                                updatedAt = utf8JsonReader.GetInt32();
                            break;
                        case "picture":
                            picture = utf8JsonReader.GetString();
                            break;
                        case "preferred_username":
                            preferredUsername = utf8JsonReader.GetString();
                            break;
                        case "provided_id":
                            providedId = utf8JsonReader.GetString();
                            break;
                        default:
                            break;
                    }
                }
            }

            return new UserProfileV2(email, emailVerified, familyName, givenName, id, name, sub, updatedAt, picture, preferredUsername, providedId);
        }

        /// <summary>
        /// Serializes a <see cref="UserProfileV2" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="userProfileV2"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, UserProfileV2 userProfileV2, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();
            WriteProperties(ref writer, userProfileV2, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="UserProfileV2" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="userProfileV2"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(ref Utf8JsonWriter writer, UserProfileV2 userProfileV2, JsonSerializerOptions jsonSerializerOptions)
        {
            if (userProfileV2.Email != null)
                writer.WriteString("email", userProfileV2.Email);
            if (userProfileV2.EmailVerified.HasValue)
                writer.WriteBoolean("email_verified", userProfileV2.EmailVerified.Value);
            if (userProfileV2.FamilyName != null)
                writer.WriteString("family_name", userProfileV2.FamilyName);
            if (userProfileV2.GivenName != null)
                writer.WriteString("given_name", userProfileV2.GivenName);
            if (userProfileV2.Id != null)
                writer.WriteString("id", userProfileV2.Id);
            if (userProfileV2.Name != null)
                writer.WriteString("name", userProfileV2.Name);
            if (userProfileV2.Sub != null)
                writer.WriteString("sub", userProfileV2.Sub);
            if (userProfileV2.UpdatedAt.HasValue)
                writer.WriteNumber("updated_at", userProfileV2.UpdatedAt.Value);
            if (userProfileV2.Picture != null)
                writer.WriteString("picture", userProfileV2.Picture);
            if (userProfileV2.PreferredUsername != null)
                writer.WriteString("preferred_username", userProfileV2.PreferredUsername);
            if (userProfileV2.ProvidedId != null)
                writer.WriteString("provided_id", userProfileV2.ProvidedId);
        }
    }
}
