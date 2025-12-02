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
    /// GetUserPermissionsResponseData
    /// </summary>
    public partial class GetUserPermissionsResponseData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserPermissionsResponseData" /> class.
        /// </summary>
        /// <param name="orgCode">The organization code the roles are associated with.</param>
        /// <param name="permissions">A list of permissions</param>
        [JsonConstructor]
        public GetUserPermissionsResponseData(string orgCode, List<Permission> permissions)
        {
            OrgCode = orgCode;
            Permissions = permissions;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// The organization code the roles are associated with.
        /// </summary>
        /// <value>The organization code the roles are associated with.</value>
        /// <example>org_0195ac80a14e</example>
        [JsonPropertyName("org_code")]
        public string OrgCode { get; set; }

        /// <summary>
        /// A list of permissions
        /// </summary>
        /// <value>A list of permissions</value>
        [JsonPropertyName("permissions")]
        public List<Permission> Permissions { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetUserPermissionsResponseData {\n");
            sb.Append("  OrgCode: ").Append(OrgCode).Append("\n");
            sb.Append("  Permissions: ").Append(Permissions).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }

    /// <summary>
    /// A Json converter for type <see cref="GetUserPermissionsResponseData" />
    /// </summary>
    public class GetUserPermissionsResponseDataJsonConverter : JsonConverter<GetUserPermissionsResponseData>
    {
        /// <summary>
        /// Deserializes json to <see cref="GetUserPermissionsResponseData" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override GetUserPermissionsResponseData Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            string? orgCode = default;
            List<Permission>? permissions = default;

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
                        case "org_code":
                            orgCode = utf8JsonReader.GetString();
                            break;
                        case "permissions":
                            if (utf8JsonReader.TokenType != JsonTokenType.Null)
                                permissions = JsonSerializer.Deserialize<List<Permission>>(ref utf8JsonReader, jsonSerializerOptions);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (orgCode == null)
                throw new ArgumentNullException(nameof(orgCode), "Property is required for class GetUserPermissionsResponseData.");

            if (permissions == null)
                throw new ArgumentNullException(nameof(permissions), "Property is required for class GetUserPermissionsResponseData.");

            return new GetUserPermissionsResponseData(orgCode, permissions);
        }

        /// <summary>
        /// Serializes a <see cref="GetUserPermissionsResponseData" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getUserPermissionsResponseData"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, GetUserPermissionsResponseData getUserPermissionsResponseData, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(ref writer, getUserPermissionsResponseData, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="GetUserPermissionsResponseData" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getUserPermissionsResponseData"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(ref Utf8JsonWriter writer, GetUserPermissionsResponseData getUserPermissionsResponseData, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteString("org_code", getUserPermissionsResponseData.OrgCode);
            writer.WritePropertyName("permissions");
            JsonSerializer.Serialize(writer, getUserPermissionsResponseData.Permissions, jsonSerializerOptions);
        }
    }
}
