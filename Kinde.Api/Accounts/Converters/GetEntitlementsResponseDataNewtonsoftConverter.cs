using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Accounts.Model;
using Kinde.Accounts.Client;

namespace Kinde.Api.Accounts.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetEntitlementsResponseData that handles the Option<> structure
    /// </summary>
    public class GetEntitlementsResponseDataNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetEntitlementsResponseData>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetEntitlementsResponseData ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetEntitlementsResponseData existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? orgCode = default(string?);
            if (jsonObject["org_code"] != null)
            {
                orgCode = jsonObject["org_code"].ToObject<string?>();
            }
            List<GetEntitlementsResponseDataPlansInner> plans = default(List<GetEntitlementsResponseDataPlansInner>);
            if (jsonObject["plans"] != null)
            {
                plans = jsonObject["plans"].ToObject<List<GetEntitlementsResponseDataPlansInner>>(serializer);
            }
            List<GetEntitlementsResponseDataEntitlementsInner> entitlements = default(List<GetEntitlementsResponseDataEntitlementsInner>);
            if (jsonObject["entitlements"] != null)
            {
                entitlements = jsonObject["entitlements"].ToObject<List<GetEntitlementsResponseDataEntitlementsInner>>(serializer);
            }

            return new GetEntitlementsResponseData(
                orgCode: orgCode != null ? new Option<string?>(orgCode) : default,                 plans: plans != null ? new Option<List<GetEntitlementsResponseDataPlansInner>?>(plans) : default,                 entitlements: entitlements != null ? new Option<List<GetEntitlementsResponseDataEntitlementsInner>?>(entitlements) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetEntitlementsResponseData value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.OrgCodeOption.IsSet && value.OrgCode != null)
            {
                writer.WritePropertyName("org_code");
                serializer.Serialize(writer, value.OrgCode);
            }
            if (value.PlansOption.IsSet)
            {
                writer.WritePropertyName("plans");
                serializer.Serialize(writer, value.Plans);
            }
            if (value.EntitlementsOption.IsSet)
            {
                writer.WritePropertyName("entitlements");
                serializer.Serialize(writer, value.Entitlements);
            }

            writer.WriteEndObject();
        }
    }
}