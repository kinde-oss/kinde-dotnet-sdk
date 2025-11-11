using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for SubscribersSubscriber that handles the Option<> structure
    /// </summary>
    public class SubscribersSubscriberNewtonsoftConverter : Newtonsoft.Json.JsonConverter<SubscribersSubscriber>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override SubscribersSubscriber ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, SubscribersSubscriber existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            string? firstName = default(string?);
            if (jsonObject["first_name"] != null)
            {
                firstName = jsonObject["first_name"].ToObject<string?>();
            }
            string? lastName = default(string?);
            if (jsonObject["last_name"] != null)
            {
                lastName = jsonObject["last_name"].ToObject<string?>();
            }

            return new SubscribersSubscriber(
                id: id != null ? new Option<string?>(id) : default,                 email: email != null ? new Option<string?>(email) : default,                 fullName: fullName != null ? new Option<string?>(fullName) : default,                 firstName: firstName != null ? new Option<string?>(firstName) : default,                 lastName: lastName != null ? new Option<string?>(lastName) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, SubscribersSubscriber value, Newtonsoft.Json.JsonSerializer serializer)
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
            if (value.FirstNameOption.IsSet && value.FirstName != null)
            {
                writer.WritePropertyName("first_name");
                serializer.Serialize(writer, value.FirstName);
            }
            if (value.LastNameOption.IsSet && value.LastName != null)
            {
                writer.WritePropertyName("last_name");
                serializer.Serialize(writer, value.LastName);
            }

            writer.WriteEndObject();
        }
    }
}