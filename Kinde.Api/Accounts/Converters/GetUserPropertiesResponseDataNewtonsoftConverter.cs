using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Accounts.Model;
using Kinde.Accounts.Client;

namespace Kinde.Api.Accounts.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetUserPropertiesResponseData that handles the Option<> structure
    /// </summary>
    public class GetUserPropertiesResponseDataNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetUserPropertiesResponseData>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetUserPropertiesResponseData ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetUserPropertiesResponseData existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            List<GetUserPropertiesResponseDataPropertiesInner> properties = default(List<GetUserPropertiesResponseDataPropertiesInner>);
            if (jsonObject["properties"] != null)
            {
                properties = jsonObject["properties"].ToObject<List<GetUserPropertiesResponseDataPropertiesInner>>(serializer);
            }

            return new GetUserPropertiesResponseData(
                properties: properties != null ? new Option<List<GetUserPropertiesResponseDataPropertiesInner>?>(properties) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetUserPropertiesResponseData value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.PropertiesOption.IsSet)
            {
                writer.WritePropertyName("properties");
                serializer.Serialize(writer, value.Properties);
            }

            writer.WriteEndObject();
        }
    }
}