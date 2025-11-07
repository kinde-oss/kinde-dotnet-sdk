using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetIndustriesResponse that handles the Option<> structure
    /// </summary>
    public class GetIndustriesResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetIndustriesResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetIndustriesResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetIndustriesResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? code = null;
            string? message = null;
            List<GetIndustriesResponseIndustriesInner> industries = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string>();
            }

            if (jsonObject["industries"] != null)
            {
                industries = jsonObject["industries"].ToObject<List<GetIndustriesResponseIndustriesInner>>(serializer);
            }

            return new GetIndustriesResponse(
                code: code != null ? new Option<string?>(code) : default, message: message != null ? new Option<string?>(message) : default, industries: industries != null ? new Option<List<GetIndustriesResponseIndustriesInner>?>(industries) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetIndustriesResponse value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.CodeOption.IsSet && value.Code != null)
            {
                writer.WritePropertyName("code");
                serializer.Serialize(writer, value.Code);
            }

            if (value.MessageOption.IsSet && value.Message != null)
            {
                writer.WritePropertyName("message");
                serializer.Serialize(writer, value.Message);
            }

            if (value.IndustriesOption.IsSet && value.Industries != null)
            {
                writer.WritePropertyName("industries");
                serializer.Serialize(writer, value.Industries);
            }

            writer.WriteEndObject();
        }
    }
}
