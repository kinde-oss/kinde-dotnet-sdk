using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UpdateOrganizationPropertiesRequest that handles the Option<> structure
    /// </summary>
    public class UpdateOrganizationPropertiesRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UpdateOrganizationPropertiesRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UpdateOrganizationPropertiesRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UpdateOrganizationPropertiesRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            Object properties = default(Object);

            var jsonObject = JObject.Load(reader);

            if (jsonObject["properties"] != null)
            {
                properties = jsonObject["properties"].ToObject<Object>(serializer);
            }

            return new UpdateOrganizationPropertiesRequest(
                properties: properties
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UpdateOrganizationPropertiesRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();


            writer.WriteEndObject();
        }
    }
}
