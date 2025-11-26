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
    /// GetEntitlementResponseData
    /// </summary>
    public partial class GetEntitlementResponseData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetEntitlementResponseData" /> class.
        /// </summary>
        /// <param name="entitlement">entitlement</param>
        /// <param name="orgCode">The organization code the entitlements are associated with.</param>
        [JsonConstructor]
        public GetEntitlementResponseData(EntitlementDetail entitlement, string orgCode)
        {
            Entitlement = entitlement;
            OrgCode = orgCode;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// Gets or Sets Entitlement
        /// </summary>
        [JsonPropertyName("entitlement")]
        public EntitlementDetail Entitlement { get; set; }

        /// <summary>
        /// The organization code the entitlements are associated with.
        /// </summary>
        /// <value>The organization code the entitlements are associated with.</value>
        /// <example>org_0195ac80a14e</example>
        [JsonPropertyName("org_code")]
        public string OrgCode { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetEntitlementResponseData {\n");
            sb.Append("  Entitlement: ").Append(Entitlement).Append("\n");
            sb.Append("  OrgCode: ").Append(OrgCode).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }

    /// <summary>
    /// A Json converter for type <see cref="GetEntitlementResponseData" />
    /// </summary>
    public class GetEntitlementResponseDataJsonConverter : JsonConverter<GetEntitlementResponseData>
    {
        /// <summary>
        /// Deserializes json to <see cref="GetEntitlementResponseData" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override GetEntitlementResponseData Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            EntitlementDetail? entitlement = default;
            string? orgCode = default;

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
                        case "entitlement":
                            if (utf8JsonReader.TokenType != JsonTokenType.Null)
                                entitlement = JsonSerializer.Deserialize<EntitlementDetail>(ref utf8JsonReader, jsonSerializerOptions);
                            break;
                        case "org_code":
                            orgCode = utf8JsonReader.GetString();
                            break;
                        default:
                            break;
                    }
                }
            }

            if (entitlement == null)
                throw new ArgumentNullException(nameof(entitlement), "Property is required for class GetEntitlementResponseData.");

            if (orgCode == null)
                throw new ArgumentNullException(nameof(orgCode), "Property is required for class GetEntitlementResponseData.");

            return new GetEntitlementResponseData(entitlement, orgCode);
        }

        /// <summary>
        /// Serializes a <see cref="GetEntitlementResponseData" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getEntitlementResponseData"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, GetEntitlementResponseData getEntitlementResponseData, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(ref writer, getEntitlementResponseData, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="GetEntitlementResponseData" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getEntitlementResponseData"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(ref Utf8JsonWriter writer, GetEntitlementResponseData getEntitlementResponseData, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WritePropertyName("entitlement");
            JsonSerializer.Serialize(writer, getEntitlementResponseData.Entitlement, jsonSerializerOptions);
            writer.WriteString("org_code", getEntitlementResponseData.OrgCode);
        }
    }
}
