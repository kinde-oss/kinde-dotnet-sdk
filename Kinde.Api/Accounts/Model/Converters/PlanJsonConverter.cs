#nullable enable

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kinde.Accounts.Model.Entities;

namespace Kinde.Accounts.Model.Entities
{
    /// <summary>
    /// JSON converter for <see cref="Plan" />
    /// </summary>
    public class PlanJsonConverter : JsonConverter<Plan>
    {
        /// <summary>
        /// The format to use to serialize SubscribedOn
        /// </summary>
        public static string SubscribedOnFormat { get; set; }

        /// <summary>
        /// Deserializes json to <see cref="Plan" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override Plan Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            string? key = default;
            string? name = default;
            DateTime? subscribedOn = default;

            while (utf8JsonReader.Read())
            {
                if (startingTokenType == JsonTokenType.StartObject && utf8JsonReader.TokenType == JsonTokenType.EndObject && currentDepth == utf8JsonReader.CurrentDepth)
                    break;

                if (startingTokenType == JsonTokenType.StartArray && utf8JsonReader.TokenType == JsonTokenType.EndArray && currentDepth == utf8JsonReader.CurrentDepth)
                    break;

                if (utf8JsonReader.TokenType == JsonTokenType.PropertyName && currentDepth == utf8JsonReader.CurrentDepth - 1)
                {
                    string? localVarJsonPropertyName = utf8JsonReader.GetString();
                    utf8JsonReader.Read();

                    switch (localVarJsonPropertyName)
                    {
                        case "key":
                            key = utf8JsonReader.GetString();
                            break;
                        case "name":
                            name = utf8JsonReader.GetString();
                            break;
                        case "subscribed_on":
                            if (utf8JsonReader.TokenType != JsonTokenType.Null)
                                subscribedOn = JsonSerializer.Deserialize<DateTime>(ref utf8JsonReader, jsonSerializerOptions);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (key == null)
                throw new ArgumentNullException(nameof(key), "Property is required for class Plan.");

            if (name == null)
                throw new ArgumentNullException(nameof(name), "Property is required for class Plan.");

            if (subscribedOn == null)
                throw new ArgumentNullException(nameof(subscribedOn), "Property is required for class Plan.");

            return new Plan(key, name, subscribedOn.Value);
        }

        /// <summary>
        /// Serializes a <see cref="Plan" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="plan"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, Plan plan, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();
            WriteProperties(ref writer, plan, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="Plan" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="plan"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(ref Utf8JsonWriter writer, Plan plan, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteString("key", plan.Key);
            writer.WriteString("name", plan.Name);
            writer.WriteString("subscribed_on", plan.SubscribedOn.ToString(SubscribedOnFormat));
        }
    }
}
