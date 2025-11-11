using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetApiScopesResponse that handles the Option<> structure
    /// </summary>
    public class GetApiScopesResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetApiScopesResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetApiScopesResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetApiScopesResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? code = default(string?);
            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string?>();
            }
            string? message = default(string?);
            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string?>();
            }
            List<GetApiScopesResponseScopesInner> scopes = default(List<GetApiScopesResponseScopesInner>);
            if (jsonObject["scopes"] != null)
            {
                scopes = jsonObject["scopes"].ToObject<List<GetApiScopesResponseScopesInner>>(serializer);
            }

            return new GetApiScopesResponse(
                code: code != null ? new Option<string?>(code) : default,                 message: message != null ? new Option<string?>(message) : default,                 scopes: scopes != null ? new Option<List<GetApiScopesResponseScopesInner>?>(scopes) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetApiScopesResponse value, Newtonsoft.Json.JsonSerializer serializer)
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
            if (value.ScopesOption.IsSet)
            {
                writer.WritePropertyName("scopes");
                serializer.Serialize(writer, value.Scopes);
            }

            writer.WriteEndObject();
        }
    }
}