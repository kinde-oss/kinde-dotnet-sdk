using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetOrganizationFeatureFlagsResponseFeatureFlagsValue that handles the Option<> structure
    /// </summary>
    public class GetOrganizationFeatureFlagsResponseFeatureFlagsValueNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetOrganizationFeatureFlagsResponseFeatureFlagsValue>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetOrganizationFeatureFlagsResponseFeatureFlagsValue ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetOrganizationFeatureFlagsResponseFeatureFlagsValue existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            GetOrganizationFeatureFlagsResponseFeatureFlagsValue.TypeEnum? type = default(GetOrganizationFeatureFlagsResponseFeatureFlagsValue.TypeEnum?);
            if (jsonObject["type"] != null)
            {
                var typeStr = jsonObject["type"].ToObject<string>();
                if (!string.IsNullOrEmpty(typeStr))
                {
                    type = GetOrganizationFeatureFlagsResponseFeatureFlagsValue.TypeEnumFromString(typeStr);
                }
            }
            string? value = default(string?);
            if (jsonObject["value"] != null)
            {
                value = jsonObject["value"].ToObject<string?>();
            }

            return new GetOrganizationFeatureFlagsResponseFeatureFlagsValue(
                type: type != null ? new Option<GetOrganizationFeatureFlagsResponseFeatureFlagsValue.TypeEnum?>(type) : default,                 value: value != null ? new Option<string?>(value) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetOrganizationFeatureFlagsResponseFeatureFlagsValue value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.TypeOption.IsSet && value.Type != null)
            {
                writer.WritePropertyName("type");
                var typeStr = GetOrganizationFeatureFlagsResponseFeatureFlagsValue.TypeEnumToJsonValue(value.Type.Value);
                writer.WriteValue(typeStr);
            }
            if (value.ValueOption.IsSet && value.Value != null)
            {
                writer.WritePropertyName("value");
                serializer.Serialize(writer, value.Value);
            }

            writer.WriteEndObject();
        }
    }
}