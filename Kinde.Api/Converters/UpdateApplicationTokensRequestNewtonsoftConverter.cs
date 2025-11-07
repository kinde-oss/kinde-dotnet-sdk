using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UpdateApplicationTokensRequest that handles the Option<> structure
    /// </summary>
    public class UpdateApplicationTokensRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UpdateApplicationTokensRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UpdateApplicationTokensRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UpdateApplicationTokensRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            int? accessTokenLifetime = null;
            int? refreshTokenLifetime = null;
            int? idTokenLifetime = null;
            int? authenticatedSessionLifetime = null;
            bool? isHasuraMappingEnabled = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["access_token_lifetime"] != null)
            {
                accessTokenLifetime = jsonObject["access_token_lifetime"].ToObject<int?>();
            }

            if (jsonObject["refresh_token_lifetime"] != null)
            {
                refreshTokenLifetime = jsonObject["refresh_token_lifetime"].ToObject<int?>();
            }

            if (jsonObject["id_token_lifetime"] != null)
            {
                idTokenLifetime = jsonObject["id_token_lifetime"].ToObject<int?>();
            }

            if (jsonObject["authenticated_session_lifetime"] != null)
            {
                authenticatedSessionLifetime = jsonObject["authenticated_session_lifetime"].ToObject<int?>();
            }

            if (jsonObject["is_hasura_mapping_enabled"] != null)
            {
                isHasuraMappingEnabled = jsonObject["is_hasura_mapping_enabled"].ToObject<bool?>();
            }

            return new UpdateApplicationTokensRequest(
                accessTokenLifetime: accessTokenLifetime != null ? new Option<int?>(accessTokenLifetime) : default, refreshTokenLifetime: refreshTokenLifetime != null ? new Option<int?>(refreshTokenLifetime) : default, idTokenLifetime: idTokenLifetime != null ? new Option<int?>(idTokenLifetime) : default, authenticatedSessionLifetime: authenticatedSessionLifetime != null ? new Option<int?>(authenticatedSessionLifetime) : default, isHasuraMappingEnabled: isHasuraMappingEnabled != null ? new Option<bool?>(isHasuraMappingEnabled) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UpdateApplicationTokensRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.AccessTokenLifetimeOption.IsSet && value.AccessTokenLifetime != null)
            {
                writer.WritePropertyName("access_token_lifetime");
                writer.WriteValue(value.AccessTokenLifetime.Value);
            }

            if (value.RefreshTokenLifetimeOption.IsSet && value.RefreshTokenLifetime != null)
            {
                writer.WritePropertyName("refresh_token_lifetime");
                writer.WriteValue(value.RefreshTokenLifetime.Value);
            }

            if (value.IdTokenLifetimeOption.IsSet && value.IdTokenLifetime != null)
            {
                writer.WritePropertyName("id_token_lifetime");
                writer.WriteValue(value.IdTokenLifetime.Value);
            }

            if (value.AuthenticatedSessionLifetimeOption.IsSet && value.AuthenticatedSessionLifetime != null)
            {
                writer.WritePropertyName("authenticated_session_lifetime");
                writer.WriteValue(value.AuthenticatedSessionLifetime.Value);
            }

            if (value.IsHasuraMappingEnabledOption.IsSet && value.IsHasuraMappingEnabled != null)
            {
                writer.WritePropertyName("is_hasura_mapping_enabled");
                writer.WriteValue(value.IsHasuraMappingEnabled.Value);
            }

            writer.WriteEndObject();
        }
    }
}
