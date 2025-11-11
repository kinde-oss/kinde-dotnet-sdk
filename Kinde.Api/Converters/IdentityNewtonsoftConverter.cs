using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for Identity that handles the Option<> structure
    /// </summary>
    public class IdentityNewtonsoftConverter : Newtonsoft.Json.JsonConverter<Identity>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override Identity ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, Identity existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            string? type = default(string?);
            if (jsonObject["type"] != null)
            {
                type = jsonObject["type"].ToObject<string?>();
            }
            bool? isConfirmed = default(bool?);
            if (jsonObject["is_confirmed"] != null)
            {
                isConfirmed = jsonObject["is_confirmed"].ToObject<bool?>(serializer);
            }
            string? createdOn = default(string?);
            if (jsonObject["created_on"] != null)
            {
                createdOn = jsonObject["created_on"].ToObject<string?>();
            }
            string? lastLoginOn = default(string?);
            if (jsonObject["last_login_on"] != null)
            {
                lastLoginOn = jsonObject["last_login_on"].ToObject<string?>();
            }
            int? totalLogins = default(int?);
            if (jsonObject["total_logins"] != null)
            {
                totalLogins = jsonObject["total_logins"].ToObject<int?>(serializer);
            }
            string? name = default(string?);
            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string?>();
            }
            string? email = default(string?);
            if (jsonObject["email"] != null)
            {
                email = jsonObject["email"].ToObject<string?>();
            }
            bool? isPrimary = default(bool?);
            if (jsonObject["is_primary"] != null)
            {
                isPrimary = jsonObject["is_primary"].ToObject<bool?>(serializer);
            }

            return new Identity(
                id: id != null ? new Option<string?>(id) : default,                 type: type != null ? new Option<string?>(type) : default,                 isConfirmed: isConfirmed != null ? new Option<bool?>(isConfirmed) : default,                 createdOn: createdOn != null ? new Option<string?>(createdOn) : default,                 lastLoginOn: lastLoginOn != null ? new Option<string?>(lastLoginOn) : default,                 totalLogins: totalLogins != null ? new Option<int?>(totalLogins) : default,                 name: name != null ? new Option<string?>(name) : default,                 email: email != null ? new Option<string?>(email) : default,                 isPrimary: isPrimary != null ? new Option<bool?>(isPrimary) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, Identity value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IdOption.IsSet && value.Id != null)
            {
                writer.WritePropertyName("id");
                serializer.Serialize(writer, value.Id);
            }
            if (value.TypeOption.IsSet && value.Type != null)
            {
                writer.WritePropertyName("type");
                serializer.Serialize(writer, value.Type);
            }
            if (value.IsConfirmedOption.IsSet && value.IsConfirmed != null)
            {
                writer.WritePropertyName("is_confirmed");
                serializer.Serialize(writer, value.IsConfirmed);
            }
            if (value.CreatedOnOption.IsSet && value.CreatedOn != null)
            {
                writer.WritePropertyName("created_on");
                serializer.Serialize(writer, value.CreatedOn);
            }
            if (value.LastLoginOnOption.IsSet && value.LastLoginOn != null)
            {
                writer.WritePropertyName("last_login_on");
                serializer.Serialize(writer, value.LastLoginOn);
            }
            if (value.TotalLoginsOption.IsSet && value.TotalLogins != null)
            {
                writer.WritePropertyName("total_logins");
                serializer.Serialize(writer, value.TotalLogins);
            }
            if (value.NameOption.IsSet && value.Name != null)
            {
                writer.WritePropertyName("name");
                serializer.Serialize(writer, value.Name);
            }
            if (value.EmailOption.IsSet && value.Email != null)
            {
                writer.WritePropertyName("email");
                serializer.Serialize(writer, value.Email);
            }
            if (value.IsPrimaryOption.IsSet && value.IsPrimary != null)
            {
                writer.WritePropertyName("is_primary");
                serializer.Serialize(writer, value.IsPrimary);
            }

            writer.WriteEndObject();
        }
    }
}