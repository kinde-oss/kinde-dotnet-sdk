using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Accounts.Model;
using Kinde.Accounts.Client;

namespace Kinde.Api.Accounts.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetFeatureFlagsResponseData that handles the Option<> structure
    /// </summary>
    public class GetFeatureFlagsResponseDataNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetFeatureFlagsResponseData>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetFeatureFlagsResponseData ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetFeatureFlagsResponseData existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            List<GetFeatureFlagsResponseDataFeatureFlagsInner> featureFlags = default(List<GetFeatureFlagsResponseDataFeatureFlagsInner>);
            if (jsonObject["feature_flags"] != null)
            {
                featureFlags = jsonObject["feature_flags"].ToObject<List<GetFeatureFlagsResponseDataFeatureFlagsInner>>(serializer);
            }

            return new GetFeatureFlagsResponseData(
                featureFlags: featureFlags != null ? new Option<List<GetFeatureFlagsResponseDataFeatureFlagsInner>?>(featureFlags) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetFeatureFlagsResponseData value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.FeatureFlagsOption.IsSet)
            {
                writer.WritePropertyName("feature_flags");
                serializer.Serialize(writer, value.FeatureFlags);
            }

            writer.WriteEndObject();
        }
    }
}