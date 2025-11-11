using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateApplicationResponseApplication that handles the Option<> structure
    /// </summary>
    public class CreateApplicationResponseApplicationNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateApplicationResponseApplication>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateApplicationResponseApplication ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateApplicationResponseApplication existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            string? clientId = default(string?);
            if (jsonObject["client_id"] != null)
            {
                clientId = jsonObject["client_id"].ToObject<string?>();
            }
            string? clientSecret = default(string?);
            if (jsonObject["client_secret"] != null)
            {
                clientSecret = jsonObject["client_secret"].ToObject<string?>();
            }

            return new CreateApplicationResponseApplication(
                id: id != null ? new Option<string?>(id) : default,                 clientId: clientId != null ? new Option<string?>(clientId) : default,                 clientSecret: clientSecret != null ? new Option<string?>(clientSecret) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateApplicationResponseApplication value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IdOption.IsSet && value.Id != null)
            {
                writer.WritePropertyName("id");
                serializer.Serialize(writer, value.Id);
            }
            if (value.ClientIdOption.IsSet && value.ClientId != null)
            {
                writer.WritePropertyName("client_id");
                serializer.Serialize(writer, value.ClientId);
            }
            if (value.ClientSecretOption.IsSet && value.ClientSecret != null)
            {
                writer.WritePropertyName("client_secret");
                serializer.Serialize(writer, value.ClientSecret);
            }

            writer.WriteEndObject();
        }
    }
}