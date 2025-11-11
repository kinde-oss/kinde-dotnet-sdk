using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UserIdentity that handles the Option<> structure
    /// </summary>
    public class UserIdentityNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UserIdentity>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UserIdentity ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UserIdentity existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? type = default(string?);
            if (jsonObject["type"] != null)
            {
                type = jsonObject["type"].ToObject<string?>();
            }
            UserIdentityResult? result = default(UserIdentityResult?);
            if (jsonObject["result"] != null)
            {
                result = jsonObject["result"].ToObject<UserIdentityResult?>(serializer);
            }

            return new UserIdentity(
                type: type != null ? new Option<string?>(type) : default,                 result: result != null ? new Option<UserIdentityResult?>(result) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UserIdentity value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.TypeOption.IsSet && value.Type != null)
            {
                writer.WritePropertyName("type");
                serializer.Serialize(writer, value.Type);
            }
            if (value.ResultOption.IsSet && value.Result != null)
            {
                writer.WritePropertyName("result");
                serializer.Serialize(writer, value.Result);
            }

            writer.WriteEndObject();
        }
    }
}