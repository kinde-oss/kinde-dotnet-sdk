using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for VerifyApiKeyRequest that handles the Option<> structure
    /// </summary>
    public class VerifyApiKeyRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<VerifyApiKeyRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override VerifyApiKeyRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, VerifyApiKeyRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string apiKey = default(string);
            if (jsonObject["api_key"] != null)
            {
                apiKey = jsonObject["api_key"].ToObject<string>();
            }

            return new VerifyApiKeyRequest(
                apiKey: apiKey            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, VerifyApiKeyRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();


            writer.WriteEndObject();
        }
    }
}