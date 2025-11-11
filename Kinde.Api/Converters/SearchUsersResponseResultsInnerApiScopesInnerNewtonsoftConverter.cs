using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for SearchUsersResponseResultsInnerApiScopesInner that handles the Option<> structure
    /// </summary>
    public class SearchUsersResponseResultsInnerApiScopesInnerNewtonsoftConverter : Newtonsoft.Json.JsonConverter<SearchUsersResponseResultsInnerApiScopesInner>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override SearchUsersResponseResultsInnerApiScopesInner ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, SearchUsersResponseResultsInnerApiScopesInner existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? orgCode = default(string?);
            if (jsonObject["org_code"] != null)
            {
                orgCode = jsonObject["org_code"].ToObject<string?>();
            }
            string? scope = default(string?);
            if (jsonObject["scope"] != null)
            {
                scope = jsonObject["scope"].ToObject<string?>();
            }
            string? apiId = default(string?);
            if (jsonObject["api_id"] != null)
            {
                apiId = jsonObject["api_id"].ToObject<string?>();
            }

            return new SearchUsersResponseResultsInnerApiScopesInner(
                orgCode: orgCode != null ? new Option<string?>(orgCode) : default,                 scope: scope != null ? new Option<string?>(scope) : default,                 apiId: apiId != null ? new Option<string?>(apiId) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, SearchUsersResponseResultsInnerApiScopesInner value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.OrgCodeOption.IsSet && value.OrgCode != null)
            {
                writer.WritePropertyName("org_code");
                serializer.Serialize(writer, value.OrgCode);
            }
            if (value.ScopeOption.IsSet && value.Scope != null)
            {
                writer.WritePropertyName("scope");
                serializer.Serialize(writer, value.Scope);
            }
            if (value.ApiIdOption.IsSet && value.ApiId != null)
            {
                writer.WritePropertyName("api_id");
                serializer.Serialize(writer, value.ApiId);
            }

            writer.WriteEndObject();
        }
    }
}