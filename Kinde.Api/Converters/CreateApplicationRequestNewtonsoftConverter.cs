using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateApplicationRequest that handles the Option<> structure
    /// </summary>
    public class CreateApplicationRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateApplicationRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateApplicationRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateApplicationRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string? orgCode = default(string?);
            if (jsonObject["org_code"] != null)
            {
                orgCode = jsonObject["org_code"].ToObject<string?>();
            }
            string name = default(string);
            if (jsonObject["name"] != null)
            {
                name = jsonObject["name"].ToObject<string>();
            }
            CreateApplicationRequest.TypeEnum type = default(CreateApplicationRequest.TypeEnum);
            if (jsonObject["type"] != null)
            {
                var typeStr = jsonObject["type"].ToObject<string>();
                if (!string.IsNullOrEmpty(typeStr))
                {
                    type = CreateApplicationRequest.TypeEnumFromString(typeStr);
                }
            }

            return new CreateApplicationRequest(
                orgCode: orgCode != null ? new Option<string?>(orgCode) : default,                 name: name,                 type: type            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateApplicationRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.OrgCodeOption.IsSet && value.OrgCode != null)
            {
                writer.WritePropertyName("org_code");
                serializer.Serialize(writer, value.OrgCode);
            }

            writer.WriteEndObject();
        }
    }
}