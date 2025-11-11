using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Accounts.Model;
using Kinde.Accounts.Client;

namespace Kinde.Api.Accounts.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetUserPermissionsResponseMetadata that handles the Option<> structure
    /// </summary>
    public class GetUserPermissionsResponseMetadataNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetUserPermissionsResponseMetadata>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetUserPermissionsResponseMetadata ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetUserPermissionsResponseMetadata existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            bool? hasMore = default(bool?);
            if (jsonObject["has_more"] != null)
            {
                hasMore = jsonObject["has_more"].ToObject<bool?>(serializer);
            }
            string? nextPageStartingAfter = default(string?);
            if (jsonObject["next_page_starting_after"] != null)
            {
                nextPageStartingAfter = jsonObject["next_page_starting_after"].ToObject<string?>();
            }

            return new GetUserPermissionsResponseMetadata(
                hasMore: hasMore != null ? new Option<bool?>(hasMore) : default,                 nextPageStartingAfter: nextPageStartingAfter != null ? new Option<string?>(nextPageStartingAfter) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetUserPermissionsResponseMetadata value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.HasMoreOption.IsSet && value.HasMore != null)
            {
                writer.WritePropertyName("has_more");
                serializer.Serialize(writer, value.HasMore);
            }
            if (value.NextPageStartingAfterOption.IsSet && value.NextPageStartingAfter != null)
            {
                writer.WritePropertyName("next_page_starting_after");
                serializer.Serialize(writer, value.NextPageStartingAfter);
            }

            writer.WriteEndObject();
        }
    }
}