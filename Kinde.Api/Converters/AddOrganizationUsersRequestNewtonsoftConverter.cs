using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for AddOrganizationUsersRequest that handles the Option<> structure
    /// </summary>
    public class AddOrganizationUsersRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<AddOrganizationUsersRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override AddOrganizationUsersRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, AddOrganizationUsersRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            List<AddOrganizationUsersRequestUsersInner> users = default(List<AddOrganizationUsersRequestUsersInner>);
            if (jsonObject["users"] != null)
            {
                users = jsonObject["users"].ToObject<List<AddOrganizationUsersRequestUsersInner>>(serializer);
            }

            return new AddOrganizationUsersRequest(
                users: users != null ? new Option<List<AddOrganizationUsersRequestUsersInner>?>(users) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, AddOrganizationUsersRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.UsersOption.IsSet)
            {
                writer.WritePropertyName("users");
                serializer.Serialize(writer, value.Users);
            }

            writer.WriteEndObject();
        }
    }
}