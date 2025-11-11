using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for ApiResult that handles the Option<> structure
    /// </summary>
    public class ApiResultNewtonsoftConverter : Newtonsoft.Json.JsonConverter<ApiResult>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override ApiResult ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, ApiResult existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? result = default(string?);
            if (jsonObject["result"] != null)
            {
                result = jsonObject["result"].ToObject<string?>();
            }

            return new ApiResult(
                result: result != null ? new Option<string?>(result) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, ApiResult value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.ResultOption.IsSet && value.Result != null)
            {
                writer.WritePropertyName("result");
                serializer.Serialize(writer, value.Result);
            }

            writer.WriteEndObject();
        }
    }
}