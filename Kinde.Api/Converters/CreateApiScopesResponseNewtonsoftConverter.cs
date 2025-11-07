using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateApiScopesResponse that handles the Option<> structure
    /// </summary>
    public class CreateApiScopesResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateApiScopesResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateApiScopesResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateApiScopesResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? message = null;
            string? code = null;
            CreateApiScopesResponseScope? scope = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string>();
            }

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["scope"] != null)
            {
                scope = jsonObject["scope"].ToObject<CreateApiScopesResponseScope>(serializer);
            }

            return new CreateApiScopesResponse(
                message: message != null ? new Option<string?>(message) : default, code: code != null ? new Option<string?>(code) : default, scope: scope != null ? new Option<CreateApiScopesResponseScope?>(scope) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateApiScopesResponse value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.MessageOption.IsSet && value.Message != null)
            {
                writer.WritePropertyName("message");
                serializer.Serialize(writer, value.Message);
            }

            if (value.CodeOption.IsSet && value.Code != null)
            {
                writer.WritePropertyName("code");
                serializer.Serialize(writer, value.Code);
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
