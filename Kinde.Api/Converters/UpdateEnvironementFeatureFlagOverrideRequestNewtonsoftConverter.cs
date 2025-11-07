using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UpdateEnvironementFeatureFlagOverrideRequest that handles the Option<> structure
    /// </summary>
    public class UpdateEnvironementFeatureFlagOverrideRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UpdateEnvironementFeatureFlagOverrideRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UpdateEnvironementFeatureFlagOverrideRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UpdateEnvironementFeatureFlagOverrideRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string value = default(string);

            var jsonObject = JObject.Load(reader);

            if (jsonObject["value"] != null)
            {
                value = jsonObject["value"].ToObject<string>();
            }

            return new UpdateEnvironementFeatureFlagOverrideRequest(
                value: value
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UpdateEnvironementFeatureFlagOverrideRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();


            writer.WriteEndObject();
        }
    }
}
