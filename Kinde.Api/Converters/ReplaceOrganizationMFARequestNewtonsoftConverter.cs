using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for ReplaceOrganizationMFARequest that handles the Option<> structure
    /// </summary>
    public class ReplaceOrganizationMFARequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<ReplaceOrganizationMFARequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override ReplaceOrganizationMFARequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, ReplaceOrganizationMFARequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            List<ReplaceOrganizationMFARequest.EnabledFactorsEnum> enabledFactors = default(List<ReplaceOrganizationMFARequest.EnabledFactorsEnum>);

            var jsonObject = JObject.Load(reader);

            if (jsonObject["enabled_factors"] != null)
            {
                enabledFactors = jsonObject["enabled_factors"].ToObject<List<ReplaceOrganizationMFARequest.EnabledFactorsEnum>>(serializer);
            }

            return new ReplaceOrganizationMFARequest(
                enabledFactors: enabledFactors
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, ReplaceOrganizationMFARequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();


            writer.WriteEndObject();
        }
    }
}
