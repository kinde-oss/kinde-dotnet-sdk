using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetRedirectCallbackUrlsResponse that handles the Option<> structure
    /// </summary>
    public class GetRedirectCallbackUrlsResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetRedirectCallbackUrlsResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetRedirectCallbackUrlsResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetRedirectCallbackUrlsResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            List<RedirectCallbackUrls> redirectUrls = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["redirect_urls"] != null)
            {
                redirectUrls = jsonObject["redirect_urls"].ToObject<List<RedirectCallbackUrls>>(serializer);
            }

            return new GetRedirectCallbackUrlsResponse(
                redirectUrls: redirectUrls != null ? new Option<List<RedirectCallbackUrls>?>(redirectUrls) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetRedirectCallbackUrlsResponse value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.RedirectUrlsOption.IsSet && value.RedirectUrls != null)
            {
                writer.WritePropertyName("redirect_urls");
                serializer.Serialize(writer, value.RedirectUrls);
            }

            writer.WriteEndObject();
        }
    }
}
