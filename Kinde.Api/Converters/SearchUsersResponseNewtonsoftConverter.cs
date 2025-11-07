using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for SearchUsersResponse that handles the Option<> structure
    /// </summary>
    public class SearchUsersResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<SearchUsersResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override SearchUsersResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, SearchUsersResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? code = null;
            string? message = null;
            List<SearchUsersResponseResultsInner> results = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string>();
            }

            if (jsonObject["results"] != null)
            {
                results = jsonObject["results"].ToObject<List<SearchUsersResponseResultsInner>>(serializer);
            }

            return new SearchUsersResponse(
                code: code != null ? new Option<string?>(code) : default, message: message != null ? new Option<string?>(message) : default, results: results != null ? new Option<List<SearchUsersResponseResultsInner>?>(results) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, SearchUsersResponse value, Newtonsoft.Json.JsonSerializer serializer)
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

            if (value.ResultsOption.IsSet && value.Results != null)
            {
                writer.WritePropertyName("results");
                serializer.Serialize(writer, value.Results);
            }

            writer.WriteEndObject();
        }
    }
}
