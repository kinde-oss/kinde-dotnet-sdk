using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetApiKeysResponseApiKeysInner that handles the Option<> structure
    /// </summary>
    public class GetApiKeysResponseApiKeysInnerNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetApiKeysResponseApiKeysInner>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetApiKeysResponseApiKeysInner ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetApiKeysResponseApiKeysInner existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            string? name = default(string?);
            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string?>();
            }
            string? type = default(string?);
            if (jsonObject["type"] != null)
            {
                type = jsonObject["type"].ToObject<string?>();
            }
            string? status = default(string?);
            if (jsonObject["status"] != null)
            {
                status = jsonObject["status"].ToObject<string?>();
            }
            string? keyPrefix = default(string?);
            if (jsonObject["key_prefix"] != null)
            {
                keyPrefix = jsonObject["key_prefix"].ToObject<string?>();
            }
            string? keySuffix = default(string?);
            if (jsonObject["key_suffix"] != null)
            {
                keySuffix = jsonObject["key_suffix"].ToObject<string?>();
            }
            DateTimeOffset? createdOn = default(DateTimeOffset?);
            if (jsonObject["created_on"] != null)
            {
                createdOn = jsonObject["created_on"].ToObject<DateTimeOffset?>(serializer);
            }
            DateTimeOffset? lastVerifiedOn = default(DateTimeOffset?);
            if (jsonObject["last_verified_on"] != null)
            {
                lastVerifiedOn = jsonObject["last_verified_on"].ToObject<DateTimeOffset?>(serializer);
            }
            string? lastVerifiedIp = default(string?);
            if (jsonObject["last_verified_ip"] != null)
            {
                lastVerifiedIp = jsonObject["last_verified_ip"].ToObject<string?>();
            }
            string? createdBy = default(string?);
            if (jsonObject["created_by"] != null)
            {
                createdBy = jsonObject["created_by"].ToObject<string?>();
            }
            List<string> apiIds = default(List<string>);
            if (jsonObject["api_ids"] != null)
            {
                apiIds = jsonObject["api_ids"].ToObject<List<string>>(serializer);
            }
            List<string> scopes = default(List<string>);
            if (jsonObject["scopes"] != null)
            {
                scopes = jsonObject["scopes"].ToObject<List<string>>(serializer);
            }

            return new GetApiKeysResponseApiKeysInner(
                id: id != null ? new Option<string?>(id) : default,                 name: name != null ? new Option<string?>(name) : default,                 type: type != null ? new Option<string?>(type) : default,                 status: status != null ? new Option<string?>(status) : default,                 keyPrefix: keyPrefix != null ? new Option<string?>(keyPrefix) : default,                 keySuffix: keySuffix != null ? new Option<string?>(keySuffix) : default,                 createdOn: createdOn != null ? new Option<DateTimeOffset?>(createdOn) : default,                 lastVerifiedOn: lastVerifiedOn != null ? new Option<DateTimeOffset?>(lastVerifiedOn) : default,                 lastVerifiedIp: lastVerifiedIp != null ? new Option<string?>(lastVerifiedIp) : default,                 createdBy: createdBy != null ? new Option<string?>(createdBy) : default,                 apiIds: apiIds != null ? new Option<List<string>?>(apiIds) : default,                 scopes: scopes != null ? new Option<List<string>?>(scopes) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetApiKeysResponseApiKeysInner value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IdOption.IsSet && value.Id != null)
            {
                writer.WritePropertyName("id");
                serializer.Serialize(writer, value.Id);
            }
            if (value.NameOption.IsSet && value.Name != null)
            {
                writer.WritePropertyName("name");
                serializer.Serialize(writer, value.Name);
            }
            if (value.TypeOption.IsSet && value.Type != null)
            {
                writer.WritePropertyName("type");
                serializer.Serialize(writer, value.Type);
            }
            if (value.StatusOption.IsSet && value.Status != null)
            {
                writer.WritePropertyName("status");
                serializer.Serialize(writer, value.Status);
            }
            if (value.KeyPrefixOption.IsSet && value.KeyPrefix != null)
            {
                writer.WritePropertyName("key_prefix");
                serializer.Serialize(writer, value.KeyPrefix);
            }
            if (value.KeySuffixOption.IsSet && value.KeySuffix != null)
            {
                writer.WritePropertyName("key_suffix");
                serializer.Serialize(writer, value.KeySuffix);
            }
            if (value.CreatedOnOption.IsSet && value.CreatedOn != null)
            {
                writer.WritePropertyName("created_on");
                serializer.Serialize(writer, value.CreatedOn);
            }
            if (value.LastVerifiedOnOption.IsSet && value.LastVerifiedOn != null)
            {
                writer.WritePropertyName("last_verified_on");
                serializer.Serialize(writer, value.LastVerifiedOn);
            }
            if (value.LastVerifiedIpOption.IsSet && value.LastVerifiedIp != null)
            {
                writer.WritePropertyName("last_verified_ip");
                serializer.Serialize(writer, value.LastVerifiedIp);
            }
            if (value.CreatedByOption.IsSet && value.CreatedBy != null)
            {
                writer.WritePropertyName("created_by");
                serializer.Serialize(writer, value.CreatedBy);
            }
            if (value.ApiIdsOption.IsSet)
            {
                writer.WritePropertyName("api_ids");
                serializer.Serialize(writer, value.ApiIds);
            }
            if (value.ScopesOption.IsSet)
            {
                writer.WritePropertyName("scopes");
                serializer.Serialize(writer, value.Scopes);
            }

            writer.WriteEndObject();
        }
    }
}