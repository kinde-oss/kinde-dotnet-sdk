using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetEnvironmentVariablesResponse that handles the Option<> structure
    /// </summary>
    public class GetEnvironmentVariablesResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetEnvironmentVariablesResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetEnvironmentVariablesResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetEnvironmentVariablesResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            List<EnvironmentVariable> environmentVariables = default(List<EnvironmentVariable>);
            if (jsonObject["environment_variables"] != null)
            {
                environmentVariables = jsonObject["environment_variables"].ToObject<List<EnvironmentVariable>>(serializer);
            }

            return new GetEnvironmentVariablesResponse(
                code: code != null ? new Option<string?>(code) : default,                 message: message != null ? new Option<string?>(message) : default,                 hasMore: hasMore != null ? new Option<bool?>(hasMore) : default,                 environmentVariables: environmentVariables != null ? new Option<List<EnvironmentVariable>?>(environmentVariables) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetEnvironmentVariablesResponse value, Newtonsoft.Json.JsonSerializer serializer)
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
            if (value.EnvironmentVariablesOption.IsSet)
            {
                writer.WritePropertyName("environment_variables");
                serializer.Serialize(writer, value.EnvironmentVariables);
            }

            writer.WriteEndObject();
        }
    }
}