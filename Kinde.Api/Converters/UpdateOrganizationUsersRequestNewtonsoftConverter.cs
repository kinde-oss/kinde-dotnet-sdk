using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UpdateOrganizationUsersRequest that handles the Option<> structure
    /// </summary>
    public class UpdateOrganizationUsersRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UpdateOrganizationUsersRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UpdateOrganizationUsersRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UpdateOrganizationUsersRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            List<UpdateOrganizationUsersRequestUsersInner> users = default(List<UpdateOrganizationUsersRequestUsersInner>);
            if (jsonObject["users"] != null)
            {
                users = jsonObject["users"].ToObject<List<UpdateOrganizationUsersRequestUsersInner>>(serializer);
            }

            return new UpdateOrganizationUsersRequest(
                users: users != null ? new Option<List<UpdateOrganizationUsersRequestUsersInner>?>(users) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UpdateOrganizationUsersRequest value, Newtonsoft.Json.JsonSerializer serializer)
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