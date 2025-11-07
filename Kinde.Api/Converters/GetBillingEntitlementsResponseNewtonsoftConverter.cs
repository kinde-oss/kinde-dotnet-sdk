using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetBillingEntitlementsResponse that handles the Option<> structure
    /// </summary>
    public class GetBillingEntitlementsResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetBillingEntitlementsResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetBillingEntitlementsResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetBillingEntitlementsResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? code = null;
            string? message = null;
            bool? hasMore = null;
            List<GetBillingEntitlementsResponseEntitlementsInner> entitlements = null;
            List<GetBillingEntitlementsResponsePlansInner> plans = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string>();
            }

            if (jsonObject["has_more"] != null)
            {
                hasMore = jsonObject["has_more"].ToObject<bool?>();
            }

            if (jsonObject["entitlements"] != null)
            {
                entitlements = jsonObject["entitlements"].ToObject<List<GetBillingEntitlementsResponseEntitlementsInner>>(serializer);
            }

            if (jsonObject["plans"] != null)
            {
                plans = jsonObject["plans"].ToObject<List<GetBillingEntitlementsResponsePlansInner>>(serializer);
            }

            return new GetBillingEntitlementsResponse(
                code: code != null ? new Option<string?>(code) : default, message: message != null ? new Option<string?>(message) : default, hasMore: hasMore != null ? new Option<bool?>(hasMore) : default, entitlements: entitlements != null ? new Option<List<GetBillingEntitlementsResponseEntitlementsInner>?>(entitlements) : default, plans: plans != null ? new Option<List<GetBillingEntitlementsResponsePlansInner>?>(plans) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetBillingEntitlementsResponse value, Newtonsoft.Json.JsonSerializer serializer)
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

            if (value.HasMoreOption.IsSet && value.HasMore != null)
            {
                writer.WritePropertyName("has_more");
                writer.WriteValue(value.HasMore.Value);
            }

            if (value.EntitlementsOption.IsSet && value.Entitlements != null)
            {
                writer.WritePropertyName("entitlements");
                serializer.Serialize(writer, value.Entitlements);
            }

            if (value.PlansOption.IsSet && value.Plans != null)
            {
                writer.WritePropertyName("plans");
                serializer.Serialize(writer, value.Plans);
            }

            writer.WriteEndObject();
        }
    }
}
