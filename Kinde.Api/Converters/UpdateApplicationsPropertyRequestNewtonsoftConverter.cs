using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UpdateApplicationsPropertyRequest that handles the Option<> structure
    /// </summary>
    public class UpdateApplicationsPropertyRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UpdateApplicationsPropertyRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UpdateApplicationsPropertyRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UpdateApplicationsPropertyRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            UpdateApplicationsPropertyRequestValue value = default(UpdateApplicationsPropertyRequestValue);

            var jsonObject = JObject.Load(reader);

            if (jsonObject["value"] != null)
            {
                value = jsonObject["value"].ToObject<UpdateApplicationsPropertyRequestValue>(serializer);
            }

            return new UpdateApplicationsPropertyRequest(
                value: value
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UpdateApplicationsPropertyRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();


            writer.WriteEndObject();
        }
    }
}
