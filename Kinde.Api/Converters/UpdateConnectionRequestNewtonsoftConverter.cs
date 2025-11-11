using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UpdateConnectionRequest that handles the Option<> structure
    /// </summary>
    public class UpdateConnectionRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UpdateConnectionRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UpdateConnectionRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UpdateConnectionRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            string? displayName = default(string?);
            if (jsonObject["display_name"] != null)
            {
                displayName = jsonObject["display_name"].ToObject<string?>();
            }
            List<string> enabledApplications = default(List<string>);
            if (jsonObject["enabled_applications"] != null)
            {
                enabledApplications = jsonObject["enabled_applications"].ToObject<List<string>>(serializer);
            }
            UpdateConnectionRequestOptions? options = default(UpdateConnectionRequestOptions?);
            if (jsonObject["options"] != null)
            {
                options = jsonObject["options"].ToObject<UpdateConnectionRequestOptions?>(serializer);
            }

            return new UpdateConnectionRequest(
                name: name != null ? new Option<string?>(name) : default,                 displayName: displayName != null ? new Option<string?>(displayName) : default,                 enabledApplications: enabledApplications != null ? new Option<List<string>?>(enabledApplications) : default,                 options: options != null ? new Option<UpdateConnectionRequestOptions?>(options) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UpdateConnectionRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.NameOption.IsSet && value.Name != null)
            {
                writer.WritePropertyName("name");
                serializer.Serialize(writer, value.Name);
            }
            if (value.DisplayNameOption.IsSet && value.DisplayName != null)
            {
                writer.WritePropertyName("display_name");
                serializer.Serialize(writer, value.DisplayName);
            }
            if (value.EnabledApplicationsOption.IsSet)
            {
                writer.WritePropertyName("enabled_applications");
                serializer.Serialize(writer, value.EnabledApplications);
            }
            if (value.OptionsOption.IsSet && value.Options != null)
            {
                writer.WritePropertyName("options");
                serializer.Serialize(writer, value.Options);
            }

            writer.WriteEndObject();
        }
    }
}