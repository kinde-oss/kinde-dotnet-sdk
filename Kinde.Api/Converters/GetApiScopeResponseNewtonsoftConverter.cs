using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetApiScopeResponse that handles the Option<> structure
    /// </summary>
    public class GetApiScopeResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetApiScopeResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetApiScopeResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetApiScopeResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? code = null;
            string? message = null;
            GetApiScopesResponseScopesInner? scope = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string>();
            }

            if (jsonObject["scope"] != null)
            {
                scope = jsonObject["scope"].ToObject<GetApiScopesResponseScopesInner>(serializer);
            }

            return new GetApiScopeResponse(
                code: code != null ? new Option<string?>(code) : default, message: message != null ? new Option<string?>(message) : default, scope: scope != null ? new Option<GetApiScopesResponseScopesInner?>(scope) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetApiScopeResponse value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.CodeOption.IsSet && value.Code != null)
            {
                writer.WritePropertyName("code");
                serializer.Serialize(writer, value.Code);
            }

            if (value.MessageOption.IsSet && value.Message != null)
            {
                writer.WritePropertyName("message");
                serializer.Serialize(writer, value.Message);
            }

            if (value.ScopeOption.IsSet && value.Scope != null)
            {
                writer.WritePropertyName("scope");
                serializer.Serialize(writer, value.Scope);
            }

            writer.WriteEndObject();
        }
    }
}
