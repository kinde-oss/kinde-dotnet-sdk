using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Accounts.Model;
using Kinde.Accounts.Client;

namespace Kinde.Api.Accounts.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetUserPropertiesResponse that handles the Option<> structure
    /// </summary>
    public class GetUserPropertiesResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetUserPropertiesResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetUserPropertiesResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetUserPropertiesResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            GetUserPropertiesResponseData? data = default(GetUserPropertiesResponseData?);
            if (jsonObject["data"] != null)
            {
                data = jsonObject["data"].ToObject<GetUserPropertiesResponseData?>(serializer);
            }
            GetUserPropertiesResponseMetadata? metadata = default(GetUserPropertiesResponseMetadata?);
            if (jsonObject["metadata"] != null)
            {
                metadata = jsonObject["metadata"].ToObject<GetUserPropertiesResponseMetadata?>(serializer);
            }

            return new GetUserPropertiesResponse(
                data: data != null ? new Option<GetUserPropertiesResponseData?>(data) : default,                 metadata: metadata != null ? new Option<GetUserPropertiesResponseMetadata?>(metadata) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetUserPropertiesResponse value, Newtonsoft.Json.JsonSerializer serializer)
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