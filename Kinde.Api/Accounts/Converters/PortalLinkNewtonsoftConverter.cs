using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Accounts.Model;
using Kinde.Accounts.Client;

namespace Kinde.Api.Accounts.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for PortalLink that handles the Option<> structure
    /// </summary>
    public class PortalLinkNewtonsoftConverter : Newtonsoft.Json.JsonConverter<PortalLink>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override PortalLink ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, PortalLink existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? url = default(string?);
            if (jsonObject["url"] != null)
            {
                url = jsonObject["url"].ToObject<string?>();
            }

            return new PortalLink(
                url: url != null ? new Option<string?>(url) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, PortalLink value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.UrlOption.IsSet && value.Url != null)
            {
                writer.WritePropertyName("url");
                serializer.Serialize(writer, value.Url);
            }

            writer.WriteEndObject();
        }
    }
}