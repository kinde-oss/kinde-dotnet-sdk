using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for ReplaceMFARequest that handles the Option<> structure
    /// </summary>
    public class ReplaceMFARequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<ReplaceMFARequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override ReplaceMFARequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, ReplaceMFARequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            ReplaceMFARequest.PolicyEnum policy = default(ReplaceMFARequest.PolicyEnum);
            List<ReplaceMFARequest.EnabledFactorsEnum> enabledFactors = default(List<ReplaceMFARequest.EnabledFactorsEnum>);

            var jsonObject = JObject.Load(reader);

            if (jsonObject["policy"] != null)
            {
                var policyStr = jsonObject["policy"].ToObject<string>();
                if (!string.IsNullOrEmpty(policyStr))
                {
                    policy = ReplaceMFARequest.PolicyEnumFromString(policyStr);
                }
            }

            if (jsonObject["enabled_factors"] != null)
            {
                enabledFactors = jsonObject["enabled_factors"].ToObject<List<ReplaceMFARequest.EnabledFactorsEnum>>(serializer);
            }

            return new ReplaceMFARequest(
                policy: policy, enabledFactors: enabledFactors
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, ReplaceMFARequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();


            writer.WriteEndObject();
        }
    }
}
