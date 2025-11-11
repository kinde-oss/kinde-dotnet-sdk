using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UpdateAPIApplicationsRequestApplicationsInner that handles the Option<> structure
    /// </summary>
    public class UpdateAPIApplicationsRequestApplicationsInnerNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UpdateAPIApplicationsRequestApplicationsInner>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UpdateAPIApplicationsRequestApplicationsInner ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UpdateAPIApplicationsRequestApplicationsInner existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? operation = default(string?);
            if (jsonObject["operation"] != null)
            {
                operation = jsonObject["operation"].ToObject<string?>();
            }
            string id = default(string);
            if (jsonObject["id"] != null)
            {
                id = jsonObject["id"].ToObject<string>();
            }

            return new UpdateAPIApplicationsRequestApplicationsInner(
                operation: operation != null ? new Option<string?>(operation) : default,                 id: id            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UpdateAPIApplicationsRequestApplicationsInner value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.OperationOption.IsSet && value.Operation != null)
            {
                writer.WritePropertyName("operation");
                serializer.Serialize(writer, value.Operation);
            }

            writer.WriteEndObject();
        }
    }
}