using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateUserRequestProfile that handles the Option<> structure
    /// </summary>
    public class CreateUserRequestProfileNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateUserRequestProfile>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateUserRequestProfile ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateUserRequestProfile existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? givenName = default(string?);
            if (jsonObject["given_name"] != null)
            {
                givenName = jsonObject["given_name"].ToObject<string?>();
            }
            string? familyName = default(string?);
            if (jsonObject["family_name"] != null)
            {
                familyName = jsonObject["family_name"].ToObject<string?>();
            }
            string? picture = default(string?);
            if (jsonObject["picture"] != null)
            {
                picture = jsonObject["picture"].ToObject<string?>();
            }

            return new CreateUserRequestProfile(
                givenName: givenName != null ? new Option<string?>(givenName) : default,                 familyName: familyName != null ? new Option<string?>(familyName) : default,                 picture: picture != null ? new Option<string?>(picture) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateUserRequestProfile value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.GivenNameOption.IsSet && value.GivenName != null)
            {
                writer.WritePropertyName("given_name");
                serializer.Serialize(writer, value.GivenName);
            }
            if (value.FamilyNameOption.IsSet && value.FamilyName != null)
            {
                writer.WritePropertyName("family_name");
                serializer.Serialize(writer, value.FamilyName);
            }
            if (value.PictureOption.IsSet && value.Picture != null)
            {
                writer.WritePropertyName("picture");
                serializer.Serialize(writer, value.Picture);
            }

            writer.WriteEndObject();
        }
    }
}