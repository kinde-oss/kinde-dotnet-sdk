using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetApisResponse that handles the Option<> structure
    /// </summary>
    public class GetApisResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetApisResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetApisResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetApisResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? code = default(string?);
            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string?>();
            }
            string? message = default(string?);
            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string?>();
            }
            string? nextToken = default(string?);
            if (jsonObject["next_token"] != null)
            {
                nextToken = jsonObject["next_token"].ToObject<string?>();
            }
            List<GetApisResponseApisInner> apis = default(List<GetApisResponseApisInner>);
            if (jsonObject["apis"] != null)
            {
                apis = jsonObject["apis"].ToObject<List<GetApisResponseApisInner>>(serializer);
            }

            return new GetApisResponse(
                code: code != null ? new Option<string?>(code) : default,                 message: message != null ? new Option<string?>(message) : default,                 nextToken: nextToken != null ? new Option<string?>(nextToken) : default,                 apis: apis != null ? new Option<List<GetApisResponseApisInner>?>(apis) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetApisResponse value, Newtonsoft.Json.JsonSerializer serializer)
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
            if (value.NextTokenOption.IsSet && value.NextToken != null)
            {
                writer.WritePropertyName("next_token");
                serializer.Serialize(writer, value.NextToken);
            }
            if (value.ApisOption.IsSet)
            {
                writer.WritePropertyName("apis");
                serializer.Serialize(writer, value.Apis);
            }

            writer.WriteEndObject();
        }
    }
}