using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Accounts.Model;
using Kinde.Accounts.Client;

namespace Kinde.Api.Accounts.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetEntitlementResponse that handles the Option<> structure
    /// </summary>
    public class GetEntitlementResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetEntitlementResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetEntitlementResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetEntitlementResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            GetEntitlementResponseData? data = default(GetEntitlementResponseData?);
            if (jsonObject["data"] != null)
            {
                data = jsonObject["data"].ToObject<GetEntitlementResponseData?>(serializer);
            }
            Object? metadata = default(Object?);
            if (jsonObject["metadata"] != null)
            {
                metadata = jsonObject["metadata"].ToObject<Object?>(serializer);
            }

            return new GetEntitlementResponse(
                data: data != null ? new Option<GetEntitlementResponseData?>(data) : default,                 metadata: metadata != null ? new Option<Object?>(metadata) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetEntitlementResponse value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.DataOption.IsSet && value.Data != null)
            {
                writer.WritePropertyName("data");
                serializer.Serialize(writer, value.Data);
            }
            if (value.MetadataOption.IsSet && value.Metadata != null)
            {
                writer.WritePropertyName("metadata");
                serializer.Serialize(writer, value.Metadata);
            }

            writer.WriteEndObject();
        }
    }
}