using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for VerifyApiKeyResponse that handles the Option<> structure
    /// </summary>
    public class VerifyApiKeyResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<VerifyApiKeyResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override VerifyApiKeyResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, VerifyApiKeyResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? code = default(string?);
            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string?>();
            }
            string? message = default(string?);
            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string?>();
            }
            bool? isValid = default(bool?);
            if (jsonObject["is_valid"] != null)
            {
                isValid = jsonObject["is_valid"].ToObject<bool?>(serializer);
            }
            string? keyId = default(string?);
            if (jsonObject["key_id"] != null)
            {
                keyId = jsonObject["key_id"].ToObject<string?>();
            }
            string? status = default(string?);
            if (jsonObject["status"] != null)
            {
                status = jsonObject["status"].ToObject<string?>();
            }
            List<string> scopes = default(List<string>);
            if (jsonObject["scopes"] != null)
            {
                scopes = jsonObject["scopes"].ToObject<List<string>>(serializer);
            }
            string? orgCode = default(string?);
            if (jsonObject["org_code"] != null)
            {
                orgCode = jsonObject["org_code"].ToObject<string?>();
            }
            string? userId = default(string?);
            if (jsonObject["user_id"] != null)
            {
                userId = jsonObject["user_id"].ToObject<string?>();
            }
            DateTimeOffset? lastVerifiedOn = default(DateTimeOffset?);
            if (jsonObject["last_verified_on"] != null)
            {
                lastVerifiedOn = jsonObject["last_verified_on"].ToObject<DateTimeOffset?>(serializer);
            }
            int? verificationCount = default(int?);
            if (jsonObject["verification_count"] != null)
            {
                verificationCount = jsonObject["verification_count"].ToObject<int?>(serializer);
            }

            return new VerifyApiKeyResponse(
                code: code != null ? new Option<string?>(code) : default,                 message: message != null ? new Option<string?>(message) : default,                 isValid: isValid != null ? new Option<bool?>(isValid) : default,                 keyId: keyId != null ? new Option<string?>(keyId) : default,                 status: status != null ? new Option<string?>(status) : default,                 scopes: scopes != null ? new Option<List<string>?>(scopes) : default,                 orgCode: orgCode != null ? new Option<string?>(orgCode) : default,                 userId: userId != null ? new Option<string?>(userId) : default,                 lastVerifiedOn: lastVerifiedOn != null ? new Option<DateTimeOffset?>(lastVerifiedOn) : default,                 verificationCount: verificationCount != null ? new Option<int?>(verificationCount) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, VerifyApiKeyResponse value, Newtonsoft.Json.JsonSerializer serializer)
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
            if (value.IsValidOption.IsSet && value.IsValid != null)
            {
                writer.WritePropertyName("is_valid");
                serializer.Serialize(writer, value.IsValid);
            }
            if (value.KeyIdOption.IsSet && value.KeyId != null)
            {
                writer.WritePropertyName("key_id");
                serializer.Serialize(writer, value.KeyId);
            }
            if (value.StatusOption.IsSet && value.Status != null)
            {
                writer.WritePropertyName("status");
                serializer.Serialize(writer, value.Status);
            }
            if (value.ScopesOption.IsSet)
            {
                writer.WritePropertyName("scopes");
                serializer.Serialize(writer, value.Scopes);
            }
            if (value.OrgCodeOption.IsSet && value.OrgCode != null)
            {
                writer.WritePropertyName("org_code");
                serializer.Serialize(writer, value.OrgCode);
            }
            if (value.UserIdOption.IsSet && value.UserId != null)
            {
                writer.WritePropertyName("user_id");
                serializer.Serialize(writer, value.UserId);
            }
            if (value.LastVerifiedOnOption.IsSet && value.LastVerifiedOn != null)
            {
                writer.WritePropertyName("last_verified_on");
                serializer.Serialize(writer, value.LastVerifiedOn);
            }
            if (value.VerificationCountOption.IsSet && value.VerificationCount != null)
            {
                writer.WritePropertyName("verification_count");
                serializer.Serialize(writer, value.VerificationCount);
            }

            writer.WriteEndObject();
        }
    }
}