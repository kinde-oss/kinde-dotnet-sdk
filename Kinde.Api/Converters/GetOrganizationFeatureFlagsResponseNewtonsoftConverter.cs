using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetOrganizationFeatureFlagsResponse that handles the Option<> structure
    /// </summary>
    public class GetOrganizationFeatureFlagsResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetOrganizationFeatureFlagsResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetOrganizationFeatureFlagsResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetOrganizationFeatureFlagsResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? code = null;
            string? message = null;
            Dictionary<string, GetOrganizationFeatureFlagsResponseFeatureFlagsValue> featureFlags = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string>();
            }

            if (jsonObject["feature_flags"] != null)
            {
                featureFlags = jsonObject["feature_flags"].ToObject<Dictionary<string, GetOrganizationFeatureFlagsResponseFeatureFlagsValue>>(serializer);
            }

            return new GetOrganizationFeatureFlagsResponse(
                code: code != null ? new Option<string?>(code) : default, message: message != null ? new Option<string?>(message) : default, featureFlags: featureFlags != null ? new Option<Dictionary<string, GetOrganizationFeatureFlagsResponseFeatureFlagsValue>?>(featureFlags) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetOrganizationFeatureFlagsResponse value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.CodeOption.IsSet && value.Code != null)
            {
                writer.WritePropertyName("code");
                serializer.Serialize(writer, value.Code);
            }

            if (value.MessageOption.IsSet && value.Message != null)
            {
                writer.WritePropertyName("message");
                serializer.Serialize(writer, value.Message);
            }

            if (value.FeatureFlagsOption.IsSet && value.FeatureFlags != null)
            {
                writer.WritePropertyName("feature_flags");
                serializer.Serialize(writer, value.FeatureFlags);
            }

            writer.WriteEndObject();
        }
    }
}
