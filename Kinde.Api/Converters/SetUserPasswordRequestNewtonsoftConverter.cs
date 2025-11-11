using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for SetUserPasswordRequest that handles the Option<> structure
    /// </summary>
    public class SetUserPasswordRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<SetUserPasswordRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override SetUserPasswordRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, SetUserPasswordRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            SetUserPasswordRequest.HashingMethodEnum? hashingMethod = default(SetUserPasswordRequest.HashingMethodEnum?);
            if (jsonObject["hashing_method"] != null)
            {
                var hashingMethodStr = jsonObject["hashing_method"].ToObject<string>();
                if (!string.IsNullOrEmpty(hashingMethodStr))
                {
                    hashingMethod = SetUserPasswordRequest.HashingMethodEnumFromString(hashingMethodStr);
                }
            }
            SetUserPasswordRequest.SaltPositionEnum? saltPosition = default(SetUserPasswordRequest.SaltPositionEnum?);
            if (jsonObject["salt_position"] != null)
            {
                var saltPositionStr = jsonObject["salt_position"].ToObject<string>();
                if (!string.IsNullOrEmpty(saltPositionStr))
                {
                    saltPosition = SetUserPasswordRequest.SaltPositionEnumFromString(saltPositionStr);
                }
            }
            string? salt = default(string?);
            if (jsonObject["salt"] != null)
            {
                salt = jsonObject["salt"].ToObject<string?>();
            }
            bool? isTemporaryPassword = default(bool?);
            if (jsonObject["is_temporary_password"] != null)
            {
                isTemporaryPassword = jsonObject["is_temporary_password"].ToObject<bool?>(serializer);
            }
            string hashedPassword = default(string);
            if (jsonObject["hashed_password"] != null)
            {
                hashedPassword = jsonObject["hashed_password"].ToObject<string>();
            }

            return new SetUserPasswordRequest(
                hashingMethod: hashingMethod != null ? new Option<SetUserPasswordRequest.HashingMethodEnum?>(hashingMethod) : default,                 saltPosition: saltPosition != null ? new Option<SetUserPasswordRequest.SaltPositionEnum?>(saltPosition) : default,                 salt: salt != null ? new Option<string?>(salt) : default,                 isTemporaryPassword: isTemporaryPassword != null ? new Option<bool?>(isTemporaryPassword) : default,                 hashedPassword: hashedPassword            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, SetUserPasswordRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.HashingMethodOption.IsSet && value.HashingMethod != null)
            {
                writer.WritePropertyName("hashing_method");
                var hashingMethodStr = SetUserPasswordRequest.HashingMethodEnumToJsonValue(value.HashingMethod.Value);
                writer.WriteValue(hashingMethodStr);
            }
            if (value.SaltPositionOption.IsSet && value.SaltPosition != null)
            {
                writer.WritePropertyName("salt_position");
                var saltPositionStr = SetUserPasswordRequest.SaltPositionEnumToJsonValue(value.SaltPosition.Value);
                writer.WriteValue(saltPositionStr);
            }
            if (value.SaltOption.IsSet && value.Salt != null)
            {
                writer.WritePropertyName("salt");
                serializer.Serialize(writer, value.Salt);
            }
            if (value.IsTemporaryPasswordOption.IsSet && value.IsTemporaryPassword != null)
            {
                writer.WritePropertyName("is_temporary_password");
                serializer.Serialize(writer, value.IsTemporaryPassword);
            }

            writer.WriteEndObject();
        }
    }
}