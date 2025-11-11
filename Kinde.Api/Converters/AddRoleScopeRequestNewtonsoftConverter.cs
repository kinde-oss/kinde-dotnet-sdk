using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for AddRoleScopeRequest that handles the Option<> structure
    /// </summary>
    public class AddRoleScopeRequestNewtonsoftConverter : Newtonsoft.Json.JsonConverter<AddRoleScopeRequest>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override AddRoleScopeRequest ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, AddRoleScopeRequest existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            var jsonObject = JObject.Load(reader);

            string scopeId = default(string);
            if (jsonObject["scope_id"] != null)
            {
                scopeId = jsonObject["scope_id"].ToObject<string>();
            }

            return new AddRoleScopeRequest(
                scopeId: scopeId            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, AddRoleScopeRequest value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();


            writer.WriteEndObject();
        }
    }
}