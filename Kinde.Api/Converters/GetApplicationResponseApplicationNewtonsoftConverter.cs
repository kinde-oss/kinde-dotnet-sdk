using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetApplicationResponseApplication that handles the Option<> structure
    /// </summary>
    public class GetApplicationResponseApplicationNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetApplicationResponseApplication>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetApplicationResponseApplication ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetApplicationResponseApplication existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            GetApplicationResponseApplication.TypeEnum? type = default(GetApplicationResponseApplication.TypeEnum?);
            if (jsonObject["type"] != null)
            {
                var typeStr = jsonObject["type"].ToObject<string>();
                if (!string.IsNullOrEmpty(typeStr))
                {
                    type = GetApplicationResponseApplication.TypeEnumFromString(typeStr);
                }
            }
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
            string? clientId = default(string?);
            if (jsonObject["client_id"] != null)
            {
                clientId = jsonObject["client_id"].ToObject<string?>();
            }
            string? clientSecret = default(string?);
            if (jsonObject["client_secret"] != null)
            {
                clientSecret = jsonObject["client_secret"].ToObject<string?>();
            }
            string? loginUri = default(string?);
            if (jsonObject["login_uri"] != null)
            {
                loginUri = jsonObject["login_uri"].ToObject<string?>();
            }
            string? homepageUri = default(string?);
            if (jsonObject["homepage_uri"] != null)
            {
                homepageUri = jsonObject["homepage_uri"].ToObject<string?>();
            }
            bool? hasCancelButton = default(bool?);
            if (jsonObject["has_cancel_button"] != null)
            {
                hasCancelButton = jsonObject["has_cancel_button"].ToObject<bool?>(serializer);
            }

            return new GetApplicationResponseApplication(
                type: type != null ? new Option<GetApplicationResponseApplication.TypeEnum?>(type) : default,                 id: id != null ? new Option<string?>(id) : default,                 name: name != null ? new Option<string?>(name) : default,                 clientId: clientId != null ? new Option<string?>(clientId) : default,                 clientSecret: clientSecret != null ? new Option<string?>(clientSecret) : default,                 loginUri: loginUri != null ? new Option<string?>(loginUri) : default,                 homepageUri: homepageUri != null ? new Option<string?>(homepageUri) : default,                 hasCancelButton: hasCancelButton != null ? new Option<bool?>(hasCancelButton) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetApplicationResponseApplication value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.TypeOption.IsSet && value.Type != null)
            {
                writer.WritePropertyName("type");
                var typeStr = GetApplicationResponseApplication.TypeEnumToJsonValue(value.Type.Value);
                writer.WriteValue(typeStr);
            }
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
            if (value.ClientIdOption.IsSet && value.ClientId != null)
            {
                writer.WritePropertyName("client_id");
                serializer.Serialize(writer, value.ClientId);
            }
            if (value.ClientSecretOption.IsSet && value.ClientSecret != null)
            {
                writer.WritePropertyName("client_secret");
                serializer.Serialize(writer, value.ClientSecret);
            }
            if (value.LoginUriOption.IsSet && value.LoginUri != null)
            {
                writer.WritePropertyName("login_uri");
                serializer.Serialize(writer, value.LoginUri);
            }
            if (value.HomepageUriOption.IsSet && value.HomepageUri != null)
            {
                writer.WritePropertyName("homepage_uri");
                serializer.Serialize(writer, value.HomepageUri);
            }
            if (value.HasCancelButtonOption.IsSet && value.HasCancelButton != null)
            {
                writer.WritePropertyName("has_cancel_button");
                serializer.Serialize(writer, value.HasCancelButton);
            }

            writer.WriteEndObject();
        }
    }
}