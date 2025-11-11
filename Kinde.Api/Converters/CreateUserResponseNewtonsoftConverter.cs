using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateUserResponse that handles the Option<> structure
    /// </summary>
    public class CreateUserResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateUserResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateUserResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateUserResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            bool? created = default(bool?);
            if (jsonObject["created"] != null)
            {
                created = jsonObject["created"].ToObject<bool?>(serializer);
            }
            List<UserIdentity> identities = default(List<UserIdentity>);
            if (jsonObject["identities"] != null)
            {
                identities = jsonObject["identities"].ToObject<List<UserIdentity>>(serializer);
            }

            return new CreateUserResponse(
                id: id != null ? new Option<string?>(id) : default,                 created: created != null ? new Option<bool?>(created) : default,                 identities: identities != null ? new Option<List<UserIdentity>?>(identities) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateUserResponse value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IdOption.IsSet && value.Id != null)
            {
                writer.WritePropertyName("id");
                serializer.Serialize(writer, value.Id);
            }
            if (value.CreatedOption.IsSet && value.Created != null)
            {
                writer.WritePropertyName("created");
                serializer.Serialize(writer, value.Created);
            }
            if (value.IdentitiesOption.IsSet)
            {
                writer.WritePropertyName("identities");
                serializer.Serialize(writer, value.Identities);
            }

            writer.WriteEndObject();
        }
    }
}