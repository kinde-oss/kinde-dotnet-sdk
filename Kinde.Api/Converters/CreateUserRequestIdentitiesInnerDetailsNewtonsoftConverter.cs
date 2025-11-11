using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateUserRequestIdentitiesInnerDetails that handles the Option<> structure
    /// </summary>
    public class CreateUserRequestIdentitiesInnerDetailsNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateUserRequestIdentitiesInnerDetails>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateUserRequestIdentitiesInnerDetails ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateUserRequestIdentitiesInnerDetails existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? email = default(string?);
            if (jsonObject["email"] != null)
            {
                email = jsonObject["email"].ToObject<string?>();
            }
            string? phone = default(string?);
            if (jsonObject["phone"] != null)
            {
                phone = jsonObject["phone"].ToObject<string?>();
            }
            string? phoneCountryId = default(string?);
            if (jsonObject["phone_country_id"] != null)
            {
                phoneCountryId = jsonObject["phone_country_id"].ToObject<string?>();
            }
            string? username = default(string?);
            if (jsonObject["username"] != null)
            {
                username = jsonObject["username"].ToObject<string?>();
            }

            return new CreateUserRequestIdentitiesInnerDetails(
                email: email != null ? new Option<string?>(email) : default,                 phone: phone != null ? new Option<string?>(phone) : default,                 phoneCountryId: phoneCountryId != null ? new Option<string?>(phoneCountryId) : default,                 username: username != null ? new Option<string?>(username) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateUserRequestIdentitiesInnerDetails value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.EmailOption.IsSet && value.Email != null)
            {
                writer.WritePropertyName("email");
                serializer.Serialize(writer, value.Email);
            }
            if (value.PhoneOption.IsSet && value.Phone != null)
            {
                writer.WritePropertyName("phone");
                serializer.Serialize(writer, value.Phone);
            }
            if (value.PhoneCountryIdOption.IsSet && value.PhoneCountryId != null)
            {
                writer.WritePropertyName("phone_country_id");
                serializer.Serialize(writer, value.PhoneCountryId);
            }
            if (value.UsernameOption.IsSet && value.Username != null)
            {
                writer.WritePropertyName("username");
                serializer.Serialize(writer, value.Username);
            }

            writer.WriteEndObject();
        }
    }
}