using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetApplicationsResponse that handles the Option<> structure
    /// </summary>
    public class GetApplicationsResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetApplicationsResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetApplicationsResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetApplicationsResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            List<Applications> applications = default(List<Applications>);
            if (jsonObject["applications"] != null)
            {
                applications = jsonObject["applications"].ToObject<List<Applications>>(serializer);
            }
            string? nextToken = default(string?);
            if (jsonObject["next_token"] != null)
            {
                nextToken = jsonObject["next_token"].ToObject<string?>();
            }

            return new GetApplicationsResponse(
                code: code != null ? new Option<string?>(code) : default,                 message: message != null ? new Option<string?>(message) : default,                 applications: applications != null ? new Option<List<Applications>?>(applications) : default,                 nextToken: nextToken != null ? new Option<string?>(nextToken) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetApplicationsResponse value, Newtonsoft.Json.JsonSerializer serializer)
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
            if (value.ApplicationsOption.IsSet)
            {
                writer.WritePropertyName("applications");
                serializer.Serialize(writer, value.Applications);
            }
            if (value.NextTokenOption.IsSet && value.NextToken != null)
            {
                writer.WritePropertyName("next_token");
                serializer.Serialize(writer, value.NextToken);
            }

            writer.WriteEndObject();
        }
    }
}