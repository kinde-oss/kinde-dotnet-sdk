using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Accounts.Model;
using Kinde.Accounts.Client;

namespace Kinde.Api.Accounts.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetUserPropertiesResponseMetadata that handles the Option<> structure
    /// </summary>
    public class GetUserPropertiesResponseMetadataNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetUserPropertiesResponseMetadata>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetUserPropertiesResponseMetadata ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetUserPropertiesResponseMetadata existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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

            return new GetUserPropertiesResponseMetadata(
                hasMore: hasMore != null ? new Option<bool?>(hasMore) : default,                 nextPageStartingAfter: nextPageStartingAfter != null ? new Option<string?>(nextPageStartingAfter) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetUserPropertiesResponseMetadata value, Newtonsoft.Json.JsonSerializer serializer)
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