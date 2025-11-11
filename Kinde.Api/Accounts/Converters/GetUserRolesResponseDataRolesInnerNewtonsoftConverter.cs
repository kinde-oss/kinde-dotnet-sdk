using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Accounts.Model;
using Kinde.Accounts.Client;

namespace Kinde.Api.Accounts.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetUserRolesResponseDataRolesInner that handles the Option<> structure
    /// </summary>
    public class GetUserRolesResponseDataRolesInnerNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetUserRolesResponseDataRolesInner>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetUserRolesResponseDataRolesInner ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetUserRolesResponseDataRolesInner existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            string? name = default(string?);
            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string?>();
            }
            string? key = default(string?);
            if (jsonObject["key"] != null)
            {
                key = jsonObject["key"].ToObject<string?>();
            }

            return new GetUserRolesResponseDataRolesInner(
                id: id != null ? new Option<string?>(id) : default,                 name: name != null ? new Option<string?>(name) : default,                 key: key != null ? new Option<string?>(key) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetUserRolesResponseDataRolesInner value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IdOption.IsSet && value.Id != null)
            {
                writer.WritePropertyName("id");
                serializer.Serialize(writer, value.Id);
            }
            if (value.NameOption.IsSet && value.Name != null)
            {
                writer.WritePropertyName("name");
                serializer.Serialize(writer, value.Name);
            }
            if (value.KeyOption.IsSet && value.Key != null)
            {
                writer.WritePropertyName("key");
                serializer.Serialize(writer, value.Key);
            }

            writer.WriteEndObject();
        }
    }
}