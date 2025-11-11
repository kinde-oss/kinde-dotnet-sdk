using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Accounts.Model;
using Kinde.Accounts.Client;

namespace Kinde.Api.Accounts.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for TokenIntrospect that handles the Option<> structure
    /// </summary>
    public class TokenIntrospectNewtonsoftConverter : Newtonsoft.Json.JsonConverter<TokenIntrospect>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override TokenIntrospect ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, TokenIntrospect existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            bool? active = default(bool?);
            if (jsonObject["active"] != null)
            {
                active = jsonObject["active"].ToObject<bool?>(serializer);
            }
            List<string> aud = default(List<string>);
            if (jsonObject["aud"] != null)
            {
                aud = jsonObject["aud"].ToObject<List<string>>(serializer);
            }
            string? clientId = default(string?);
            if (jsonObject["client_id"] != null)
            {
                clientId = jsonObject["client_id"].ToObject<string?>();
            }
            int? exp = default(int?);
            if (jsonObject["exp"] != null)
            {
                exp = jsonObject["exp"].ToObject<int?>(serializer);
            }
            int? iat = default(int?);
            if (jsonObject["iat"] != null)
            {
                iat = jsonObject["iat"].ToObject<int?>(serializer);
            }

            return new TokenIntrospect(
                active: active != null ? new Option<bool?>(active) : default,                 aud: aud != null ? new Option<List<string>?>(aud) : default,                 clientId: clientId != null ? new Option<string?>(clientId) : default,                 exp: exp != null ? new Option<int?>(exp) : default,                 iat: iat != null ? new Option<int?>(iat) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, TokenIntrospect value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.ActiveOption.IsSet && value.Active != null)
            {
                writer.WritePropertyName("active");
                serializer.Serialize(writer, value.Active);
            }
            if (value.AudOption.IsSet)
            {
                writer.WritePropertyName("aud");
                serializer.Serialize(writer, value.Aud);
            }
            if (value.ClientIdOption.IsSet && value.ClientId != null)
            {
                writer.WritePropertyName("client_id");
                serializer.Serialize(writer, value.ClientId);
            }
            if (value.ExpOption.IsSet && value.Exp != null)
            {
                writer.WritePropertyName("exp");
                serializer.Serialize(writer, value.Exp);
            }
            if (value.IatOption.IsSet && value.Iat != null)
            {
                writer.WritePropertyName("iat");
                serializer.Serialize(writer, value.Iat);
            }

            writer.WriteEndObject();
        }
    }
}