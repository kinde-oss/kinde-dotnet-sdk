using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateApiKeyRequest that handles the Option<> structure
    /// </summary>
    public class CreateApiKeyRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateApiKeyRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateApiKeyRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateApiKeyRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string name = default(string);
            string apiId = default(string);
            List<string> scopeIds = null;
            string? userId = null;
            string? orgCode = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string>();
            }

            if (jsonObject["api_id"] != null)
            {
                apiId = jsonObject["api_id"].ToObject<string>();
            }

            if (jsonObject["scope_ids"] != null)
            {
                scopeIds = jsonObject["scope_ids"].ToObject<List<string>>(serializer);
            }

            if (jsonObject["user_id"] != null)
            {
                userId = jsonObject["user_id"].ToObject<string>();
            }

            if (jsonObject["org_code"] != null)
            {
                orgCode = jsonObject["org_code"].ToObject<string>();
            }

            return new CreateApiKeyRequest(
                name: name, apiId: apiId, scopeIds: scopeIds != null ? new Option<List<string>?>(scopeIds) : default, userId: userId != null ? new Option<string?>(userId) : default, orgCode: orgCode != null ? new Option<string?>(orgCode) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateApiKeyRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.ScopeIdsOption.IsSet && value.ScopeIds != null)
            {
                writer.WritePropertyName("scope_ids");
                serializer.Serialize(writer, value.ScopeIds);
            }

            if (value.UserIdOption.IsSet && value.UserId != null)
            {
                writer.WritePropertyName("user_id");
                serializer.Serialize(writer, value.UserId);
            }

            if (value.OrgCodeOption.IsSet && value.OrgCode != null)
            {
                writer.WritePropertyName("org_code");
                serializer.Serialize(writer, value.OrgCode);
            }

            writer.WriteEndObject();
        }
    }
}
