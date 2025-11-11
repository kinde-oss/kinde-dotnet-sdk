using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UpdateOrganizationUsersResponse that handles the Option<> structure
    /// </summary>
    public class UpdateOrganizationUsersResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UpdateOrganizationUsersResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UpdateOrganizationUsersResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UpdateOrganizationUsersResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? message = default(string?);
            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string?>();
            }
            string? code = default(string?);
            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string?>();
            }
            List<string> usersAdded = default(List<string>);
            if (jsonObject["users_added"] != null)
            {
                usersAdded = jsonObject["users_added"].ToObject<List<string>>(serializer);
            }
            List<string> usersUpdated = default(List<string>);
            if (jsonObject["users_updated"] != null)
            {
                usersUpdated = jsonObject["users_updated"].ToObject<List<string>>(serializer);
            }
            List<string> usersRemoved = default(List<string>);
            if (jsonObject["users_removed"] != null)
            {
                usersRemoved = jsonObject["users_removed"].ToObject<List<string>>(serializer);
            }

            return new UpdateOrganizationUsersResponse(
                message: message != null ? new Option<string?>(message) : default,                 code: code != null ? new Option<string?>(code) : default,                 usersAdded: usersAdded != null ? new Option<List<string>?>(usersAdded) : default,                 usersUpdated: usersUpdated != null ? new Option<List<string>?>(usersUpdated) : default,                 usersRemoved: usersRemoved != null ? new Option<List<string>?>(usersRemoved) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UpdateOrganizationUsersResponse value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.MessageOption.IsSet && value.Message != null)
            {
                writer.WritePropertyName("message");
                serializer.Serialize(writer, value.Message);
            }
            if (value.CodeOption.IsSet && value.Code != null)
            {
                writer.WritePropertyName("code");
                serializer.Serialize(writer, value.Code);
            }
            if (value.UsersAddedOption.IsSet)
            {
                writer.WritePropertyName("users_added");
                serializer.Serialize(writer, value.UsersAdded);
            }
            if (value.UsersUpdatedOption.IsSet)
            {
                writer.WritePropertyName("users_updated");
                serializer.Serialize(writer, value.UsersUpdated);
            }
            if (value.UsersRemovedOption.IsSet)
            {
                writer.WritePropertyName("users_removed");
                serializer.Serialize(writer, value.UsersRemoved);
            }

            writer.WriteEndObject();
        }
    }
}