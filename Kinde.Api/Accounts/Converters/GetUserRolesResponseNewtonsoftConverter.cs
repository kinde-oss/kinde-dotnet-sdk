using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Accounts.Model;
using Kinde.Accounts.Client;

namespace Kinde.Api.Accounts.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetUserRolesResponse that handles the Option<> structure
    /// </summary>
    public class GetUserRolesResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetUserRolesResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetUserRolesResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetUserRolesResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            GetUserRolesResponseData? data = default(GetUserRolesResponseData?);
            if (jsonObject["data"] != null)
            {
                data = jsonObject["data"].ToObject<GetUserRolesResponseData?>(serializer);
            }
            GetUserRolesResponseMetadata? metadata = default(GetUserRolesResponseMetadata?);
            if (jsonObject["metadata"] != null)
            {
                metadata = jsonObject["metadata"].ToObject<GetUserRolesResponseMetadata?>(serializer);
            }

            return new GetUserRolesResponse(
                data: data != null ? new Option<GetUserRolesResponseData?>(data) : default,                 metadata: metadata != null ? new Option<GetUserRolesResponseMetadata?>(metadata) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetUserRolesResponse value, Newtonsoft.Json.JsonSerializer serializer)
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