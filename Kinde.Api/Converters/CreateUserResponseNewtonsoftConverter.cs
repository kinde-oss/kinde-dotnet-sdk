using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Kinde.Api.Model;
using Kinde.Api.Client;

namespace Kinde.Api.Converters
{
    /// <summary>
    /// Newtonsoft.Json converter for CreateUserResponse that handles the Option<List<UserIdentity>> structure
    /// </summary>
    public class CreateUserResponseNewtonsoftConverter : Newtonsoft.Json.JsonConverter<CreateUserResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override CreateUserResponse ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CreateUserResponse existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
            {
                throw new Newtonsoft.Json.JsonException($"Expected StartObject, got {reader.TokenType}");
            }

            string? id = null;
            bool? created = null;
            List<UserIdentity>? identities = null;

            while (reader.Read())
            {
                if (reader.TokenType == Newtonsoft.Json.JsonToken.EndObject)
                    break;

                if (reader.TokenType == Newtonsoft.Json.JsonToken.PropertyName)
                {
                    string? propertyName = reader.Value?.ToString();
                    reader.Read();

                    switch (propertyName)
                    {
                        case "id":
                            id = reader.Value?.ToString();
                            break;
                        case "created":
                            created = reader.Value != null ? Convert.ToBoolean(reader.Value) : null;
                            break;
                        case "identities":
                            if (reader.TokenType == Newtonsoft.Json.JsonToken.StartArray)
                            {
                                identities = new List<UserIdentity>();
                                while (reader.Read() && reader.TokenType != Newtonsoft.Json.JsonToken.EndArray)
                                {
                                    if (reader.TokenType == Newtonsoft.Json.JsonToken.StartObject)
                                    {
                                        var identity = serializer.Deserialize<UserIdentity>(reader);
                                        if (identity != null)
                                        {
                                            identities.Add(identity);
                                        }
                                    }
                                }
                            }
                            break;
                        default:
                            reader.Skip();
                            break;
                    }
                }
            }

            return new CreateUserResponse(
                id: id != null ? new Option<string?>(id) : default,
                created: created != null ? new Option<bool?>(created) : default,
                identities: identities != null ? new Option<List<UserIdentity>?>(identities) : default
            );
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, CreateUserResponse value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value.IdOption.IsSet && value.Id != null)
            {
                writer.WritePropertyName("id");
                writer.WriteValue(value.Id);
            }

            if (value.CreatedOption.IsSet && value.Created != null)
            {
                writer.WritePropertyName("created");
                writer.WriteValue(value.Created.Value);
            }

            if (value.IdentitiesOption.IsSet && value.Identities != null)
            {
                writer.WritePropertyName("identities");
                serializer.Serialize(writer, value.Identities);
            }

            writer.WriteEndObject();
        }
    }
}
