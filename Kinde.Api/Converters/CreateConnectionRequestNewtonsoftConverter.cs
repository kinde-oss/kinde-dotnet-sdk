using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateConnectionRequest that handles the Option<> structure
    /// </summary>
    public class CreateConnectionRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateConnectionRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateConnectionRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateConnectionRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            CreateConnectionRequest.StrategyEnum? strategy = null;
            string? name = null;
            string? displayName = null;
            List<string> enabledApplications = null;
            string? organizationCode = null;
            CreateConnectionRequestOptions? options = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["strategy"] != null)
            {
                var strategyStr = jsonObject["strategy"].ToObject<string>();
                if (!string.IsNullOrEmpty(strategyStr))
                {
                    strategy = CreateConnectionRequest.StrategyEnumFromString(strategyStr);
                }
            }

            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string>();
            }

            if (jsonObject["display_name"] != null)
            {
                displayName = jsonObject["display_name"].ToObject<string>();
            }

            if (jsonObject["enabled_applications"] != null)
            {
                enabledApplications = jsonObject["enabled_applications"].ToObject<List<string>>(serializer);
            }

            if (jsonObject["organization_code"] != null)
            {
                organizationCode = jsonObject["organization_code"].ToObject<string>();
            }

            if (jsonObject["options"] != null)
            {
                options = jsonObject["options"].ToObject<CreateConnectionRequestOptions>(serializer);
            }

            return new CreateConnectionRequest(
                strategy: strategy != null ? new Option<CreateConnectionRequest.StrategyEnum?>(strategy) : default, name: name != null ? new Option<string?>(name) : default, displayName: displayName != null ? new Option<string?>(displayName) : default, enabledApplications: enabledApplications != null ? new Option<List<string>?>(enabledApplications) : default, organizationCode: organizationCode != null ? new Option<string?>(organizationCode) : default, options: options != null ? new Option<CreateConnectionRequestOptions?>(options) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateConnectionRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.StrategyOption.IsSet && value.Strategy != null)
            {
                writer.WritePropertyName("strategy");
                var strategyStr = CreateConnectionRequest.StrategyEnumToJsonValue(value.Strategy.Value);
                writer.WriteValue(strategyStr);
            }

            if (value.NameOption.IsSet && value.Name != null)
            {
                writer.WritePropertyName("name");
                serializer.Serialize(writer, value.Name);
            }

            if (value.DisplayNameOption.IsSet && value.DisplayName != null)
            {
                writer.WritePropertyName("display_name");
                serializer.Serialize(writer, value.DisplayName);
            }

            if (value.EnabledApplicationsOption.IsSet && value.EnabledApplications != null)
            {
                writer.WritePropertyName("enabled_applications");
                serializer.Serialize(writer, value.EnabledApplications);
            }

            if (value.OrganizationCodeOption.IsSet && value.OrganizationCode != null)
            {
                writer.WritePropertyName("organization_code");
                serializer.Serialize(writer, value.OrganizationCode);
            }

            if (value.OptionsOption.IsSet && value.Options != null)
            {
                writer.WritePropertyName("options");
                serializer.Serialize(writer, value.Options);
            }

            writer.WriteEndObject();
        }
    }
}
