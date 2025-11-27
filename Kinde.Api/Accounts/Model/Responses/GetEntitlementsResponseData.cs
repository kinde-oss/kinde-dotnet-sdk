#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kinde.Accounts.Model.Entities;

namespace Kinde.Accounts.Model.Responses
{
    /// <summary>
    /// GetEntitlementsResponseData
    /// </summary>
    public partial class GetEntitlementsResponseData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetEntitlementsResponseData" /> class.
        /// </summary>
        /// <param name="entitlements">A list of entitlements</param>
        /// <param name="orgCode">The organization code the entitlements are associated with.</param>
        /// <param name="plans">A list of plans the user is subscribed to</param>
        [JsonConstructor]
        public GetEntitlementsResponseData(List<Entitlement> entitlements, string orgCode, List<Plan> plans)
        {
            Entitlements = entitlements;
            OrgCode = orgCode;
            Plans = plans;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// A list of entitlements
        /// </summary>
        /// <value>A list of entitlements</value>
        [JsonPropertyName("entitlements")]
        public List<Entitlement> Entitlements { get; set; }

        /// <summary>
        /// The organization code the entitlements are associated with.
        /// </summary>
        /// <value>The organization code the entitlements are associated with.</value>
        /// <example>org_0195ac80a14e</example>
        [JsonPropertyName("org_code")]
        public string OrgCode { get; set; }

        /// <summary>
        /// A list of plans the user is subscribed to
        /// </summary>
        /// <value>A list of plans the user is subscribed to</value>
        [JsonPropertyName("plans")]
        public List<Plan> Plans { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetEntitlementsResponseData {\n");
            sb.Append("  Entitlements: ").Append(Entitlements).Append("\n");
            sb.Append("  OrgCode: ").Append(OrgCode).Append("\n");
            sb.Append("  Plans: ").Append(Plans).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }

    /// <summary>
    /// A Json converter for type <see cref="GetEntitlementsResponseData" />
    /// </summary>
    public class GetEntitlementsResponseDataJsonConverter : JsonConverter<GetEntitlementsResponseData>
    {
        /// <summary>
        /// Deserializes json to <see cref="GetEntitlementsResponseData" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override GetEntitlementsResponseData Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            List<Entitlement>? entitlements = default;
            string? orgCode = default;
            List<Plan>? plans = default;

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
                        case "entitlements":
                            if (utf8JsonReader.TokenType != JsonTokenType.Null)
                                entitlements = JsonSerializer.Deserialize<List<Entitlement>>(ref utf8JsonReader, jsonSerializerOptions);
                            break;
                        case "org_code":
                            orgCode = utf8JsonReader.GetString();
                            break;
                        case "plans":
                            if (utf8JsonReader.TokenType != JsonTokenType.Null)
                                plans = JsonSerializer.Deserialize<List<Plan>>(ref utf8JsonReader, jsonSerializerOptions);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (entitlements == null)
                throw new ArgumentNullException(nameof(entitlements), "Property is required for class GetEntitlementsResponseData.");

            if (orgCode == null)
                throw new ArgumentNullException(nameof(orgCode), "Property is required for class GetEntitlementsResponseData.");

            if (plans == null)
                throw new ArgumentNullException(nameof(plans), "Property is required for class GetEntitlementsResponseData.");

            return new GetEntitlementsResponseData(entitlements, orgCode, plans);
        }

        /// <summary>
        /// Serializes a <see cref="GetEntitlementsResponseData" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getEntitlementsResponseData"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, GetEntitlementsResponseData getEntitlementsResponseData, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(ref writer, getEntitlementsResponseData, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="GetEntitlementsResponseData" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getEntitlementsResponseData"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(ref Utf8JsonWriter writer, GetEntitlementsResponseData getEntitlementsResponseData, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WritePropertyName("entitlements");
            JsonSerializer.Serialize(writer, getEntitlementsResponseData.Entitlements, jsonSerializerOptions);
            writer.WriteString("org_code", getEntitlementsResponseData.OrgCode);
            writer.WritePropertyName("plans");
            JsonSerializer.Serialize(writer, getEntitlementsResponseData.Plans, jsonSerializerOptions);
        }
    }
}
