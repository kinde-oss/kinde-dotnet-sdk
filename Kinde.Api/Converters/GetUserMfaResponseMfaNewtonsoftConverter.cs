using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetUserMfaResponseMfa that handles the Option<> structure
    /// </summary>
    public class GetUserMfaResponseMfaNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetUserMfaResponseMfa>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetUserMfaResponseMfa ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetUserMfaResponseMfa existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? id = default(string?);
            if (jsonObject["id"] != null)
            {
                id = jsonObject["id"].ToObject<string?>();
            }
            string? type = default(string?);
            if (jsonObject["type"] != null)
            {
                type = jsonObject["type"].ToObject<string?>();
            }
            DateTimeOffset? createdOn = default(DateTimeOffset?);
            if (jsonObject["created_on"] != null)
            {
                createdOn = jsonObject["created_on"].ToObject<DateTimeOffset?>(serializer);
            }
            string? name = default(string?);
            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string?>();
            }
            bool? isVerified = default(bool?);
            if (jsonObject["is_verified"] != null)
            {
                isVerified = jsonObject["is_verified"].ToObject<bool?>(serializer);
            }
            int? usageCount = default(int?);
            if (jsonObject["usage_count"] != null)
            {
                usageCount = jsonObject["usage_count"].ToObject<int?>(serializer);
            }
            DateTimeOffset? lastUsedOn = default(DateTimeOffset?);
            if (jsonObject["last_used_on"] != null)
            {
                lastUsedOn = jsonObject["last_used_on"].ToObject<DateTimeOffset?>(serializer);
            }

            return new GetUserMfaResponseMfa(
                id: id != null ? new Option<string?>(id) : default,                 type: type != null ? new Option<string?>(type) : default,                 createdOn: createdOn != null ? new Option<DateTimeOffset?>(createdOn) : default,                 name: name != null ? new Option<string?>(name) : default,                 isVerified: isVerified != null ? new Option<bool?>(isVerified) : default,                 usageCount: usageCount != null ? new Option<int?>(usageCount) : default,                 lastUsedOn: lastUsedOn != null ? new Option<DateTimeOffset?>(lastUsedOn) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetUserMfaResponseMfa value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IdOption.IsSet && value.Id != null)
            {
                writer.WritePropertyName("id");
                serializer.Serialize(writer, value.Id);
            }
            if (value.TypeOption.IsSet && value.Type != null)
            {
                writer.WritePropertyName("type");
                serializer.Serialize(writer, value.Type);
            }
            if (value.CreatedOnOption.IsSet && value.CreatedOn != null)
            {
                writer.WritePropertyName("created_on");
                serializer.Serialize(writer, value.CreatedOn);
            }
            if (value.NameOption.IsSet && value.Name != null)
            {
                writer.WritePropertyName("name");
                serializer.Serialize(writer, value.Name);
            }
            if (value.IsVerifiedOption.IsSet && value.IsVerified != null)
            {
                writer.WritePropertyName("is_verified");
                serializer.Serialize(writer, value.IsVerified);
            }
            if (value.UsageCountOption.IsSet && value.UsageCount != null)
            {
                writer.WritePropertyName("usage_count");
                serializer.Serialize(writer, value.UsageCount);
            }
            if (value.LastUsedOnOption.IsSet && value.LastUsedOn != null)
            {
                writer.WritePropertyName("last_used_on");
                serializer.Serialize(writer, value.LastUsedOn);
            }

            writer.WriteEndObject();
        }
    }
}