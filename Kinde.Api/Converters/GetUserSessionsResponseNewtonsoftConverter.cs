using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetUserSessionsResponse that handles the Option<> structure
    /// </summary>
    public class GetUserSessionsResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetUserSessionsResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetUserSessionsResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetUserSessionsResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            bool? hasMore = default(bool?);
            if (jsonObject["has_more"] != null)
            {
                hasMore = jsonObject["has_more"].ToObject<bool?>(serializer);
            }
            List<GetUserSessionsResponseSessionsInner> sessions = default(List<GetUserSessionsResponseSessionsInner>);
            if (jsonObject["sessions"] != null)
            {
                sessions = jsonObject["sessions"].ToObject<List<GetUserSessionsResponseSessionsInner>>(serializer);
            }

            return new GetUserSessionsResponse(
                code: code != null ? new Option<string?>(code) : default,                 message: message != null ? new Option<string?>(message) : default,                 hasMore: hasMore != null ? new Option<bool?>(hasMore) : default,                 sessions: sessions != null ? new Option<List<GetUserSessionsResponseSessionsInner>?>(sessions) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetUserSessionsResponse value, Newtonsoft.Json.JsonSerializer serializer)
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
            if (value.HasMoreOption.IsSet && value.HasMore != null)
            {
                writer.WritePropertyName("has_more");
                serializer.Serialize(writer, value.HasMore);
            }
            if (value.SessionsOption.IsSet)
            {
                writer.WritePropertyName("sessions");
                serializer.Serialize(writer, value.Sessions);
            }

            writer.WriteEndObject();
        }
    }
}