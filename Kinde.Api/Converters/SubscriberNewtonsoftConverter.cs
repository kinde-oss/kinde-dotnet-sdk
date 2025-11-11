using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for Subscriber that handles the Option<> structure
    /// </summary>
    public class SubscriberNewtonsoftConverter : Newtonsoft.Json.JsonConverter<Subscriber>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override Subscriber ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, Subscriber existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            string? preferredEmail = default(string?);
            if (jsonObject["preferred_email"] != null)
            {
                preferredEmail = jsonObject["preferred_email"].ToObject<string?>();
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

            return new Subscriber(
                id: id != null ? new Option<string?>(id) : default,                 preferredEmail: preferredEmail != null ? new Option<string?>(preferredEmail) : default,                 firstName: firstName != null ? new Option<string?>(firstName) : default,                 lastName: lastName != null ? new Option<string?>(lastName) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, Subscriber value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IdOption.IsSet && value.Id != null)
            {
                writer.WritePropertyName("id");
                serializer.Serialize(writer, value.Id);
            }
            if (value.PreferredEmailOption.IsSet && value.PreferredEmail != null)
            {
                writer.WritePropertyName("preferred_email");
                serializer.Serialize(writer, value.PreferredEmail);
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