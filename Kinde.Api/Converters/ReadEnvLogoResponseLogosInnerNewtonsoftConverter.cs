using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for ReadEnvLogoResponseLogosInner that handles the Option<> structure
    /// </summary>
    public class ReadEnvLogoResponseLogosInnerNewtonsoftConverter : Newtonsoft.Json.JsonConverter<ReadEnvLogoResponseLogosInner>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override ReadEnvLogoResponseLogosInner ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, ReadEnvLogoResponseLogosInner existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? type = default(string?);
            if (jsonObject["type"] != null)
            {
                type = jsonObject["type"].ToObject<string?>();
            }
            string? fileName = default(string?);
            if (jsonObject["file_name"] != null)
            {
                fileName = jsonObject["file_name"].ToObject<string?>();
            }

            return new ReadEnvLogoResponseLogosInner(
                type: type != null ? new Option<string?>(type) : default,                 fileName: fileName != null ? new Option<string?>(fileName) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, ReadEnvLogoResponseLogosInner value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.TypeOption.IsSet && value.Type != null)
            {
                writer.WritePropertyName("type");
                serializer.Serialize(writer, value.Type);
            }
            if (value.FileNameOption.IsSet && value.FileName != null)
            {
                writer.WritePropertyName("file_name");
                serializer.Serialize(writer, value.FileName);
            }

            writer.WriteEndObject();
        }
    }
}