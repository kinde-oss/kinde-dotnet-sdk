using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Accounts.Model;
using Kinde.Accounts.Client;

namespace Kinde.Api.Accounts.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetUserPermissionsResponse that handles the Option<> structure
    /// </summary>
    public class GetUserPermissionsResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetUserPermissionsResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetUserPermissionsResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetUserPermissionsResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            GetUserPermissionsResponseData? data = default(GetUserPermissionsResponseData?);
            if (jsonObject["data"] != null)
            {
                data = jsonObject["data"].ToObject<GetUserPermissionsResponseData?>(serializer);
            }
            GetUserPermissionsResponseMetadata? metadata = default(GetUserPermissionsResponseMetadata?);
            if (jsonObject["metadata"] != null)
            {
                metadata = jsonObject["metadata"].ToObject<GetUserPermissionsResponseMetadata?>(serializer);
            }

            return new GetUserPermissionsResponse(
                data: data != null ? new Option<GetUserPermissionsResponseData?>(data) : default,                 metadata: metadata != null ? new Option<GetUserPermissionsResponseMetadata?>(metadata) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetUserPermissionsResponse value, Newtonsoft.Json.JsonSerializer serializer)
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