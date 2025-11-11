using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetPermissionsResponse that handles the Option<> structure
    /// </summary>
    public class GetPermissionsResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetPermissionsResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetPermissionsResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetPermissionsResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            List<Permissions> permissions = default(List<Permissions>);
            if (jsonObject["permissions"] != null)
            {
                permissions = jsonObject["permissions"].ToObject<List<Permissions>>(serializer);
            }
            string? nextToken = default(string?);
            if (jsonObject["next_token"] != null)
            {
                nextToken = jsonObject["next_token"].ToObject<string?>();
            }

            return new GetPermissionsResponse(
                code: code != null ? new Option<string?>(code) : default,                 message: message != null ? new Option<string?>(message) : default,                 permissions: permissions != null ? new Option<List<Permissions>?>(permissions) : default,                 nextToken: nextToken != null ? new Option<string?>(nextToken) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetPermissionsResponse value, Newtonsoft.Json.JsonSerializer serializer)
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
            if (value.PermissionsOption.IsSet)
            {
                writer.WritePropertyName("permissions");
                serializer.Serialize(writer, value.Permissions);
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