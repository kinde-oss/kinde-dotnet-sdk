using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Accounts.Model;
using Kinde.Accounts.Client;

namespace Kinde.Api.Accounts.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetEntitlementResponseData that handles the Option<> structure
    /// </summary>
    public class GetEntitlementResponseDataNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetEntitlementResponseData>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetEntitlementResponseData ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetEntitlementResponseData existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            GetEntitlementResponseDataEntitlement? entitlement = default(GetEntitlementResponseDataEntitlement?);
            if (jsonObject["entitlement"] != null)
            {
                entitlement = jsonObject["entitlement"].ToObject<GetEntitlementResponseDataEntitlement?>(serializer);
            }

            return new GetEntitlementResponseData(
                orgCode: orgCode != null ? new Option<string?>(orgCode) : default,                 entitlement: entitlement != null ? new Option<GetEntitlementResponseDataEntitlement?>(entitlement) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetEntitlementResponseData value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.OrgCodeOption.IsSet && value.OrgCode != null)
            {
                writer.WritePropertyName("org_code");
                serializer.Serialize(writer, value.OrgCode);
            }
            if (value.EntitlementOption.IsSet && value.Entitlement != null)
            {
                writer.WritePropertyName("entitlement");
                serializer.Serialize(writer, value.Entitlement);
            }

            writer.WriteEndObject();
        }
    }
}