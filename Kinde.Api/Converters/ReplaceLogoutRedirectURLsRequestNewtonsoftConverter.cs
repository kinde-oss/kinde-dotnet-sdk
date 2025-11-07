using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for ReplaceLogoutRedirectURLsRequest that handles the Option<> structure
    /// </summary>
    public class ReplaceLogoutRedirectURLsRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<ReplaceLogoutRedirectURLsRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override ReplaceLogoutRedirectURLsRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, ReplaceLogoutRedirectURLsRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            List<string> urls = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["urls"] != null)
            {
                urls = jsonObject["urls"].ToObject<List<string>>(serializer);
            }

            return new ReplaceLogoutRedirectURLsRequest(
                urls: urls != null ? new Option<List<string>?>(urls) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, ReplaceLogoutRedirectURLsRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.UrlsOption.IsSet && value.Urls != null)
            {
                writer.WritePropertyName("urls");
                serializer.Serialize(writer, value.Urls);
            }

            writer.WriteEndObject();
        }
    }
}
