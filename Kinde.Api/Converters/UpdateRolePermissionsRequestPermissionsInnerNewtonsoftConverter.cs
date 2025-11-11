using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for UpdateRolePermissionsRequestPermissionsInner that handles the Option<> structure
    /// </summary>
    public class UpdateRolePermissionsRequestPermissionsInnerNewtonsoftConverter : Newtonsoft.Json.JsonConverter<UpdateRolePermissionsRequestPermissionsInner>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override UpdateRolePermissionsRequestPermissionsInner ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, UpdateRolePermissionsRequestPermissionsInner existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? id = default(string?);
            if (jsonObject["id"] != null)
            {
                id = jsonObject["id"].ToObject<string?>();
            }
            string? operation = default(string?);
            if (jsonObject["operation"] != null)
            {
                operation = jsonObject["operation"].ToObject<string?>();
            }

            return new UpdateRolePermissionsRequestPermissionsInner(
                id: id != null ? new Option<string?>(id) : default,                 operation: operation != null ? new Option<string?>(operation) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, UpdateRolePermissionsRequestPermissionsInner value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IdOption.IsSet && value.Id != null)
            {
                writer.WritePropertyName("id");
                serializer.Serialize(writer, value.Id);
            }
            if (value.OperationOption.IsSet && value.Operation != null)
            {
                writer.WritePropertyName("operation");
                serializer.Serialize(writer, value.Operation);
            }

            writer.WriteEndObject();
        }
    }
}