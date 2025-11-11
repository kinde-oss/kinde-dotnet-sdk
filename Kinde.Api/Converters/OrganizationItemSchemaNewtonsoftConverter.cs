using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for OrganizationItemSchema that handles the Option<> structure
    /// </summary>
    public class OrganizationItemSchemaNewtonsoftConverter : Newtonsoft.Json.JsonConverter<OrganizationItemSchema>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override OrganizationItemSchema ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, OrganizationItemSchema existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            string? name = default(string?);
            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string?>();
            }
            string? handle = default(string?);
            if (jsonObject["handle"] != null)
            {
                handle = jsonObject["handle"].ToObject<string?>();
            }
            bool? isDefault = default(bool?);
            if (jsonObject["is_default"] != null)
            {
                isDefault = jsonObject["is_default"].ToObject<bool?>(serializer);
            }
            string? externalId = default(string?);
            if (jsonObject["external_id"] != null)
            {
                externalId = jsonObject["external_id"].ToObject<string?>();
            }
            bool? isAutoMembershipEnabled = default(bool?);
            if (jsonObject["is_auto_membership_enabled"] != null)
            {
                isAutoMembershipEnabled = jsonObject["is_auto_membership_enabled"].ToObject<bool?>(serializer);
            }

            return new OrganizationItemSchema(
                code: code != null ? new Option<string?>(code) : default,                 name: name != null ? new Option<string?>(name) : default,                 handle: handle != null ? new Option<string?>(handle) : default,                 isDefault: isDefault != null ? new Option<bool?>(isDefault) : default,                 externalId: externalId != null ? new Option<string?>(externalId) : default,                 isAutoMembershipEnabled: isAutoMembershipEnabled != null ? new Option<bool?>(isAutoMembershipEnabled) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, OrganizationItemSchema value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.CodeOption.IsSet && value.Code != null)
            {
                writer.WritePropertyName("code");
                serializer.Serialize(writer, value.Code);
            }
            if (value.NameOption.IsSet && value.Name != null)
            {
                writer.WritePropertyName("name");
                serializer.Serialize(writer, value.Name);
            }
            if (value.HandleOption.IsSet && value.Handle != null)
            {
                writer.WritePropertyName("handle");
                serializer.Serialize(writer, value.Handle);
            }
            if (value.IsDefaultOption.IsSet && value.IsDefault != null)
            {
                writer.WritePropertyName("is_default");
                serializer.Serialize(writer, value.IsDefault);
            }
            if (value.ExternalIdOption.IsSet && value.ExternalId != null)
            {
                writer.WritePropertyName("external_id");
                serializer.Serialize(writer, value.ExternalId);
            }
            if (value.IsAutoMembershipEnabledOption.IsSet && value.IsAutoMembershipEnabled != null)
            {
                writer.WritePropertyName("is_auto_membership_enabled");
                serializer.Serialize(writer, value.IsAutoMembershipEnabled);
            }

            writer.WriteEndObject();
        }
    }
}