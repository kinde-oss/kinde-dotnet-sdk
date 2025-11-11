using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Accounts.Model;
using Kinde.Accounts.Client;

namespace Kinde.Api.Accounts.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetFeatureFlagsResponse that handles the Option<> structure
    /// </summary>
    public class GetFeatureFlagsResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetFeatureFlagsResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetFeatureFlagsResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetFeatureFlagsResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            GetFeatureFlagsResponseData? data = default(GetFeatureFlagsResponseData?);
            if (jsonObject["data"] != null)
            {
                data = jsonObject["data"].ToObject<GetFeatureFlagsResponseData?>(serializer);
            }

            return new GetFeatureFlagsResponse(
                data: data != null ? new Option<GetFeatureFlagsResponseData?>(data) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetFeatureFlagsResponse value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.DataOption.IsSet && value.Data != null)
            {
                writer.WritePropertyName("data");
                serializer.Serialize(writer, value.Data);
            }

            writer.WriteEndObject();
        }
    }
}