using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UpdateIdentityRequest that handles the Option<> structure
    /// </summary>
    public class UpdateIdentityRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UpdateIdentityRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UpdateIdentityRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UpdateIdentityRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            bool? isPrimary = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["is_primary"] != null)
            {
                isPrimary = jsonObject["is_primary"].ToObject<bool?>();
            }

            return new UpdateIdentityRequest(
                isPrimary: isPrimary != null ? new Option<bool?>(isPrimary) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UpdateIdentityRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IsPrimaryOption.IsSet && value.IsPrimary != null)
            {
                writer.WritePropertyName("is_primary");
                writer.WriteValue(value.IsPrimary.Value);
            }

            writer.WriteEndObject();
        }
    }
}
