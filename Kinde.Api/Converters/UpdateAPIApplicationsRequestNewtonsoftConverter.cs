using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UpdateAPIApplicationsRequest that handles the Option<> structure
    /// </summary>
    public class UpdateAPIApplicationsRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UpdateAPIApplicationsRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UpdateAPIApplicationsRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UpdateAPIApplicationsRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            List<UpdateAPIApplicationsRequestApplicationsInner> applications = default(List<UpdateAPIApplicationsRequestApplicationsInner>);

            var jsonObject = JObject.Load(reader);

            if (jsonObject["applications"] != null)
            {
                applications = jsonObject["applications"].ToObject<List<UpdateAPIApplicationsRequestApplicationsInner>>(serializer);
            }

            return new UpdateAPIApplicationsRequest(
                applications: applications
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UpdateAPIApplicationsRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();


            writer.WriteEndObject();
        }
    }
}
