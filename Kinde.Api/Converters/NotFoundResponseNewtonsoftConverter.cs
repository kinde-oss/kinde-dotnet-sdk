using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for NotFoundResponse that handles the Option<> structure
    /// </summary>
    public class NotFoundResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<NotFoundResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override NotFoundResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, NotFoundResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            NotFoundResponseErrors? errors = default(NotFoundResponseErrors?);
            if (jsonObject["errors"] != null)
            {
                errors = jsonObject["errors"].ToObject<NotFoundResponseErrors?>(serializer);
            }

            return new NotFoundResponse(
                errors: errors != null ? new Option<NotFoundResponseErrors?>(errors) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, NotFoundResponse value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.ErrorsOption.IsSet && value.Errors != null)
            {
                writer.WritePropertyName("errors");
                serializer.Serialize(writer, value.Errors);
            }

            writer.WriteEndObject();
        }
    }
}