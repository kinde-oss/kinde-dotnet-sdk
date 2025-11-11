using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for AddAPIsRequest that handles the Option<> structure
    /// </summary>
    public class AddAPIsRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<AddAPIsRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override AddAPIsRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, AddAPIsRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string name = default(string);
            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string>();
            }
            string audience = default(string);
            if (jsonObject["audience"] != null)
            {
                audience = jsonObject["audience"].ToObject<string>();
            }

            return new AddAPIsRequest(
                name: name,                 audience: audience            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, AddAPIsRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();


            writer.WriteEndObject();
        }
    }
}