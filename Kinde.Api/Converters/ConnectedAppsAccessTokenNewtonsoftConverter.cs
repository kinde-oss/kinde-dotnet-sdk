using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for ConnectedAppsAccessToken that handles the Option<> structure
    /// </summary>
    public class ConnectedAppsAccessTokenNewtonsoftConverter : Newtonsoft.Json.JsonConverter<ConnectedAppsAccessToken>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override ConnectedAppsAccessToken ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, ConnectedAppsAccessToken existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? accessToken = default(string?);
            if (jsonObject["access_token"] != null)
            {
                accessToken = jsonObject["access_token"].ToObject<string?>();
            }
            string? accessTokenExpiry = default(string?);
            if (jsonObject["access_token_expiry"] != null)
            {
                accessTokenExpiry = jsonObject["access_token_expiry"].ToObject<string?>();
            }

            return new ConnectedAppsAccessToken(
                accessToken: accessToken != null ? new Option<string?>(accessToken) : default,                 accessTokenExpiry: accessTokenExpiry != null ? new Option<string?>(accessTokenExpiry) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, ConnectedAppsAccessToken value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.AccessTokenOption.IsSet && value.AccessToken != null)
            {
                writer.WritePropertyName("access_token");
                serializer.Serialize(writer, value.AccessToken);
            }
            if (value.AccessTokenExpiryOption.IsSet && value.AccessTokenExpiry != null)
            {
                writer.WritePropertyName("access_token_expiry");
                serializer.Serialize(writer, value.AccessTokenExpiry);
            }

            writer.WriteEndObject();
        }
    }
}