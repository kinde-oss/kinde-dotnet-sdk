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

            string? code = null;
            string? message = null;
            bool? hasMore = null;
            List<EnvironmentVariable> environmentVariables = null;

            var jsonObject = JObject.Load(reader);

            if (jsonObject["code"] != null)
            {
                code = jsonObject["code"].ToObject<string>();
            }

            if (jsonObject["message"] != null)
            {
                message = jsonObject["message"].ToObject<string>();
            }

            if (jsonObject["has_more"] != null)
            {
                hasMore = jsonObject["has_more"].ToObject<bool?>();
            }

            if (jsonObject["environment_variables"] != null)
            {
                environmentVariables = jsonObject["environment_variables"].ToObject<List<EnvironmentVariable>>(serializer);
            }

            return new GetEnvironmentVariablesResponse(
                code: code != null ? new Option<string?>(code) : default, message: message != null ? new Option<string?>(message) : default, hasMore: hasMore != null ? new Option<bool?>(hasMore) : default, environmentVariables: environmentVariables != null ? new Option<List<EnvironmentVariable>?>(environmentVariables) : default
            );
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
                writer.WriteValue(value.HasMore.Value);
            }

            if (value.EnvironmentVariablesOption.IsSet && value.EnvironmentVariables != null)
            {
                writer.WritePropertyName("environment_variables");
                serializer.Serialize(writer, value.EnvironmentVariables);
            }

            writer.WriteEndObject();
        }
    }
}
