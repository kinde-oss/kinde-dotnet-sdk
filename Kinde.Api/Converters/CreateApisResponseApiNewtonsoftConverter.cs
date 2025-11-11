using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateApisResponseApi that handles the Option<> structure
    /// </summary>
    public class CreateApisResponseApiNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateApisResponseApi>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateApisResponseApi ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateApisResponseApi existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? id = default(string?);
            if (jsonObject["id"] != null)
            {
                id = jsonObject["id"].ToObject<string?>();
            }

            return new CreateApisResponseApi(
                id: id != null ? new Option<string?>(id) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateApisResponseApi value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IdOption.IsSet && value.Id != null)
            {
                writer.WritePropertyName("id");
                serializer.Serialize(writer, value.Id);
            }

            writer.WriteEndObject();
        }
    }
}