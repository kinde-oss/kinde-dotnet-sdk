using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for RoleScopesResponse that handles the Option<> structure
    /// </summary>
    public class RoleScopesResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<RoleScopesResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override RoleScopesResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, RoleScopesResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? code = null;
            string? message = null;
            List<Scopes> scopes = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string>();
            }

            if (jsonObject["scopes"] != null)
            {
                scopes = jsonObject["scopes"].ToObject<List<Scopes>>(serializer);
            }

            return new RoleScopesResponse(
                code: code != null ? new Option<string?>(code) : default, message: message != null ? new Option<string?>(message) : default, scopes: scopes != null ? new Option<List<Scopes>?>(scopes) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, RoleScopesResponse value, Newtonsoft.Json.JsonSerializer serializer)
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

            if (value.ScopesOption.IsSet && value.Scopes != null)
            {
                writer.WritePropertyName("scopes");
                serializer.Serialize(writer, value.Scopes);
            }

            writer.WriteEndObject();
        }
    }
}
