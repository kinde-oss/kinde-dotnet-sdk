using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UserIdentitiesInner that handles the Option<> structure
    /// </summary>
    public class UserIdentitiesInnerNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UserIdentitiesInner>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UserIdentitiesInner ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UserIdentitiesInner existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            string? identity = default(string?);
            if (jsonObject["identity"] != null)
            {
                identity = jsonObject["identity"].ToObject<string?>();
            }

            return new UserIdentitiesInner(
                type: type != null ? new Option<string?>(type) : default,                 identity: identity != null ? new Option<string?>(identity) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UserIdentitiesInner value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.TypeOption.IsSet && value.Type != null)
            {
                writer.WritePropertyName("type");
                serializer.Serialize(writer, value.Type);
            }
            if (value.IdentityOption.IsSet && value.Identity != null)
            {
                writer.WritePropertyName("identity");
                serializer.Serialize(writer, value.Identity);
            }

            writer.WriteEndObject();
        }
    }
}