using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for RedirectCallbackUrls that handles the Option<> structure
    /// </summary>
    public class RedirectCallbackUrlsNewtonsoftConverter : Newtonsoft.Json.JsonConverter<RedirectCallbackUrls>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override RedirectCallbackUrls ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, RedirectCallbackUrls existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            List<string> redirectUrls = default(List<string>);
            if (jsonObject["redirect_urls"] != null)
            {
                redirectUrls = jsonObject["redirect_urls"].ToObject<List<string>>(serializer);
            }

            return new RedirectCallbackUrls(
                redirectUrls: redirectUrls != null ? new Option<List<string>?>(redirectUrls) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, RedirectCallbackUrls value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.RedirectUrlsOption.IsSet)
            {
                writer.WritePropertyName("redirect_urls");
                serializer.Serialize(writer, value.RedirectUrls);
            }

            writer.WriteEndObject();
        }
    }
}