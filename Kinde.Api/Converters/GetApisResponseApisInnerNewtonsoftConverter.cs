using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for GetApisResponseApisInner that handles the Option<> structure
    /// </summary>
    public class GetApisResponseApisInnerNewtonsoftConverter : Newtonsoft.Json.JsonConverter<GetApisResponseApisInner>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override GetApisResponseApisInner ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, GetApisResponseApisInner existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
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
            string? name = default(string?);
            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string?>();
            }
            string? audience = default(string?);
            if (jsonObject["audience"] != null)
            {
                audience = jsonObject["audience"].ToObject<string?>();
            }
            bool? isManagementApi = default(bool?);
            if (jsonObject["is_management_api"] != null)
            {
                isManagementApi = jsonObject["is_management_api"].ToObject<bool?>(serializer);
            }
            List<GetApisResponseApisInnerScopesInner> scopes = default(List<GetApisResponseApisInnerScopesInner>);
            if (jsonObject["scopes"] != null)
            {
                scopes = jsonObject["scopes"].ToObject<List<GetApisResponseApisInnerScopesInner>>(serializer);
            }

            return new GetApisResponseApisInner(
                id: id != null ? new Option<string?>(id) : default,                 name: name != null ? new Option<string?>(name) : default,                 audience: audience != null ? new Option<string?>(audience) : default,                 isManagementApi: isManagementApi != null ? new Option<bool?>(isManagementApi) : default,                 scopes: scopes != null ? new Option<List<GetApisResponseApisInnerScopesInner>?>(scopes) : default            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, GetApisResponseApisInner value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IdOption.IsSet && value.Id != null)
            {
                writer.WritePropertyName("id");
                serializer.Serialize(writer, value.Id);
            }
            if (value.NameOption.IsSet && value.Name != null)
            {
                writer.WritePropertyName("name");
                serializer.Serialize(writer, value.Name);
            }
            if (value.AudienceOption.IsSet && value.Audience != null)
            {
                writer.WritePropertyName("audience");
                serializer.Serialize(writer, value.Audience);
            }
            if (value.IsManagementApiOption.IsSet && value.IsManagementApi != null)
            {
                writer.WritePropertyName("is_management_api");
                serializer.Serialize(writer, value.IsManagementApi);
            }
            if (value.ScopesOption.IsSet)
            {
                writer.WritePropertyName("scopes");
                serializer.Serialize(writer, value.Scopes);
            }

            writer.WriteEndObject();
        }
    }
}