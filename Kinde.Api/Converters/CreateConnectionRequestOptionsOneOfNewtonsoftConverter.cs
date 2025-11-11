using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateConnectionRequestOptionsOneOf that handles the Option<> structure
    /// </summary>
    public class CreateConnectionRequestOptionsOneOfNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateConnectionRequestOptionsOneOf>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateConnectionRequestOptionsOneOf ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateConnectionRequestOptionsOneOf existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

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
            bool? isUseCustomDomain = default(bool?);
            if (jsonObject["is_use_custom_domain"] != null)
            {
                isUseCustomDomain = jsonObject["is_use_custom_domain"].ToObject<bool?>(serializer);
            }

            return new CreateConnectionRequestOptionsOneOf(
                clientId: clientId != null ? new Option<string?>(clientId) : default,                 clientSecret: clientSecret != null ? new Option<string?>(clientSecret) : default,                 isUseCustomDomain: isUseCustomDomain != null ? new Option<bool?>(isUseCustomDomain) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateConnectionRequestOptionsOneOf value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

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
            if (value.IsUseCustomDomainOption.IsSet && value.IsUseCustomDomain != null)
            {
                writer.WritePropertyName("is_use_custom_domain");
                serializer.Serialize(writer, value.IsUseCustomDomain);
            }

            writer.WriteEndObject();
        }
    }
}