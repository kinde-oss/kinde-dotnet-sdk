using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UserIdentityResult that handles the Option<> structure
    /// </summary>
    public class UserIdentityResultNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UserIdentityResult>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UserIdentityResult ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UserIdentityResult existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            bool? created = default(bool?);
            if (jsonObject["created"] != null)
            {
                created = jsonObject["created"].ToObject<bool?>(serializer);
            }

            return new UserIdentityResult(
                created: created != null ? new Option<bool?>(created) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UserIdentityResult value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.CreatedOption.IsSet && value.Created != null)
            {
                writer.WritePropertyName("created");
                serializer.Serialize(writer, value.Created);
            }

            writer.WriteEndObject();
        }
    }
}