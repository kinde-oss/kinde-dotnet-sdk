using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UpdateApplicationRequest that handles the Option<> structure
    /// </summary>
    public class UpdateApplicationRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UpdateApplicationRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UpdateApplicationRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UpdateApplicationRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? name = default(string?);
            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string?>();
            }
            string? languageKey = default(string?);
            if (jsonObject["language_key"] != null)
            {
                languageKey = jsonObject["language_key"].ToObject<string?>();
            }
            List<string> logoutUris = default(List<string>);
            if (jsonObject["logout_uris"] != null)
            {
                logoutUris = jsonObject["logout_uris"].ToObject<List<string>>(serializer);
            }
            List<string> redirectUris = default(List<string>);
            if (jsonObject["redirect_uris"] != null)
            {
                redirectUris = jsonObject["redirect_uris"].ToObject<List<string>>(serializer);
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

            return new UpdateApplicationRequest(
                name: name != null ? new Option<string?>(name) : default,                 languageKey: languageKey != null ? new Option<string?>(languageKey) : default,                 logoutUris: logoutUris != null ? new Option<List<string>?>(logoutUris) : default,                 redirectUris: redirectUris != null ? new Option<List<string>?>(redirectUris) : default,                 loginUri: loginUri != null ? new Option<string?>(loginUri) : default,                 homepageUri: homepageUri != null ? new Option<string?>(homepageUri) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UpdateApplicationRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.NameOption.IsSet && value.Name != null)
            {
                writer.WritePropertyName("name");
                serializer.Serialize(writer, value.Name);
            }
            if (value.LanguageKeyOption.IsSet && value.LanguageKey != null)
            {
                writer.WritePropertyName("language_key");
                serializer.Serialize(writer, value.LanguageKey);
            }
            if (value.LogoutUrisOption.IsSet)
            {
                writer.WritePropertyName("logout_uris");
                serializer.Serialize(writer, value.LogoutUris);
            }
            if (value.RedirectUrisOption.IsSet)
            {
                writer.WritePropertyName("redirect_uris");
                serializer.Serialize(writer, value.RedirectUris);
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

            writer.WriteEndObject();
        }
    }
}