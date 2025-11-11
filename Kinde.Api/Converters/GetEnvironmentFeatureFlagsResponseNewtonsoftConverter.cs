using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetEnvironmentFeatureFlagsResponse that handles the Option<> structure
    /// </summary>
    public class GetEnvironmentFeatureFlagsResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetEnvironmentFeatureFlagsResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetEnvironmentFeatureFlagsResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetEnvironmentFeatureFlagsResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? code = default(string?);
            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string?>();
            }
            string? message = default(string?);
            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string?>();
            }
            Dictionary<string, GetOrganizationFeatureFlagsResponseFeatureFlagsValue> featureFlags = default(Dictionary<string, GetOrganizationFeatureFlagsResponseFeatureFlagsValue>);
            if (jsonObject["feature_flags"] != null)
            {
                featureFlags = jsonObject["feature_flags"].ToObject<Dictionary<string, GetOrganizationFeatureFlagsResponseFeatureFlagsValue>>(serializer);
            }
            string? nextToken = default(string?);
            if (jsonObject["next_token"] != null)
            {
                nextToken = jsonObject["next_token"].ToObject<string?>();
            }

            return new GetEnvironmentFeatureFlagsResponse(
                code: code != null ? new Option<string?>(code) : default,                 message: message != null ? new Option<string?>(message) : default,                 featureFlags: featureFlags != null ? new Option<Dictionary<string, GetOrganizationFeatureFlagsResponseFeatureFlagsValue>>(featureFlags) : default,                 nextToken: nextToken != null ? new Option<string?>(nextToken) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetEnvironmentFeatureFlagsResponse value, Newtonsoft.Json.JsonSerializer serializer)
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
            if (value.FeatureFlagsOption.IsSet)
            {
                writer.WritePropertyName("feature_flags");
                serializer.Serialize(writer, value.FeatureFlags);
            }
            if (value.NextTokenOption.IsSet && value.NextToken != null)
            {
                writer.WritePropertyName("next_token");
                serializer.Serialize(writer, value.NextToken);
            }

            writer.WriteEndObject();
        }
    }
}