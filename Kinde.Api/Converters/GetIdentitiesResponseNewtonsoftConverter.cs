using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetIdentitiesResponse that handles the Option<> structure
    /// </summary>
    public class GetIdentitiesResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetIdentitiesResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetIdentitiesResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetIdentitiesResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? code = null;
            string? message = null;
            List<Identity> identities = null;
            bool? hasMore = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string>();
            }

            if (jsonObject["identities"] != null)
            {
                identities = jsonObject["identities"].ToObject<List<Identity>>(serializer);
            }

            if (jsonObject["has_more"] != null)
            {
                hasMore = jsonObject["has_more"].ToObject<bool?>();
            }

            return new GetIdentitiesResponse(
                code: code != null ? new Option<string?>(code) : default, message: message != null ? new Option<string?>(message) : default, identities: identities != null ? new Option<List<Identity>?>(identities) : default, hasMore: hasMore != null ? new Option<bool?>(hasMore) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetIdentitiesResponse value, Newtonsoft.Json.JsonSerializer serializer)
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

            if (value.IdentitiesOption.IsSet && value.Identities != null)
            {
                writer.WritePropertyName("identities");
                serializer.Serialize(writer, value.Identities);
            }

            if (value.HasMoreOption.IsSet && value.HasMore != null)
            {
                writer.WritePropertyName("has_more");
                writer.WriteValue(value.HasMore.Value);
            }

            writer.WriteEndObject();
        }
    }
}
