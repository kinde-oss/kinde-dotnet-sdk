using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateCategoryRequest that handles the Option<> structure
    /// </summary>
    public class CreateCategoryRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateCategoryRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateCategoryRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateCategoryRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string name = default(string);
            CreateCategoryRequest.ContextEnum context = default(CreateCategoryRequest.ContextEnum);

            var jsonObject = JObject.Load(reader);

            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string>();
            }

            if (jsonObject["context"] != null)
            {
                var contextStr = jsonObject["context"].ToObject<string>();
                if (!string.IsNullOrEmpty(contextStr))
                {
                    context = CreateCategoryRequest.ContextEnumFromString(contextStr);
                }
            }

            return new CreateCategoryRequest(
                name: name, context: context
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateCategoryRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();


            writer.WriteEndObject();
        }
    }
}
