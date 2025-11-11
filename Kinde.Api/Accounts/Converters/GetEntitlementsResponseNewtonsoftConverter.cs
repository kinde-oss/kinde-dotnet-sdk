using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Accounts.Model;
using Kinde.Accounts.Client;

namespace Kinde.Api.Accounts.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetEntitlementsResponse that handles the Option<> structure
    /// </summary>
    public class GetEntitlementsResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetEntitlementsResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetEntitlementsResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetEntitlementsResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            GetEntitlementsResponseData? data = default(GetEntitlementsResponseData?);
            if (jsonObject["data"] != null)
            {
                data = jsonObject["data"].ToObject<GetEntitlementsResponseData?>(serializer);
            }
            GetEntitlementsResponseMetadata? metadata = default(GetEntitlementsResponseMetadata?);
            if (jsonObject["metadata"] != null)
            {
                metadata = jsonObject["metadata"].ToObject<GetEntitlementsResponseMetadata?>(serializer);
            }

            return new GetEntitlementsResponse(
                data: data != null ? new Option<GetEntitlementsResponseData?>(data) : default,                 metadata: metadata != null ? new Option<GetEntitlementsResponseMetadata?>(metadata) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetEntitlementsResponse value, Newtonsoft.Json.JsonSerializer serializer)
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