using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Accounts.Model;
using Kinde.Accounts.Client;

namespace Kinde.Api.Accounts.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for TokenErrorResponse that handles the Option<> structure
    /// </summary>
    public class TokenErrorResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<TokenErrorResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override TokenErrorResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, TokenErrorResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? error = default(string?);
            if (jsonObject["error"] != null)
            {
                error = jsonObject["error"].ToObject<string?>();
            }
            string? errorDescription = default(string?);
            if (jsonObject["error_description"] != null)
            {
                errorDescription = jsonObject["error_description"].ToObject<string?>();
            }

            return new TokenErrorResponse(
                error: error != null ? new Option<string?>(error) : default,                 errorDescription: errorDescription != null ? new Option<string?>(errorDescription) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, TokenErrorResponse value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.ErrorOption.IsSet && value.Error != null)
            {
                writer.WritePropertyName("error");
                serializer.Serialize(writer, value.Error);
            }
            if (value.ErrorDescriptionOption.IsSet && value.ErrorDescription != null)
            {
                writer.WritePropertyName("error_description");
                serializer.Serialize(writer, value.ErrorDescription);
            }

            writer.WriteEndObject();
        }
    }
}