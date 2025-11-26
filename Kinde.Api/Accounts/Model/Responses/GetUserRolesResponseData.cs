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
    /// GetUserRolesResponseData
    /// </summary>
    public partial class GetUserRolesResponseData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserRolesResponseData" /> class.
        /// </summary>
        /// <param name="orgCode">The organization code the roles are associated with.</param>
        /// <param name="roles">A list of roles</param>
        [JsonConstructor]
        public GetUserRolesResponseData(string orgCode, List<Role> roles)
        {
            OrgCode = orgCode;
            Roles = roles;
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
        /// A list of roles
        /// </summary>
        /// <value>A list of roles</value>
        [JsonPropertyName("roles")]
        public List<Role> Roles { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetUserRolesResponseData {\n");
            sb.Append("  OrgCode: ").Append(OrgCode).Append("\n");
            sb.Append("  Roles: ").Append(Roles).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }

    /// <summary>
    /// A Json converter for type <see cref="GetUserRolesResponseData" />
    /// </summary>
    public class GetUserRolesResponseDataJsonConverter : JsonConverter<GetUserRolesResponseData>
    {
        /// <summary>
        /// Deserializes json to <see cref="GetUserRolesResponseData" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override GetUserRolesResponseData Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            string? orgCode = default;
            List<Role>? roles = default;

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
                        case "roles":
                            if (utf8JsonReader.TokenType != JsonTokenType.Null)
                                roles = JsonSerializer.Deserialize<List<Role>>(ref utf8JsonReader, jsonSerializerOptions);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (orgCode == null)
                throw new ArgumentNullException(nameof(orgCode), "Property is required for class GetUserRolesResponseData.");

            if (roles == null)
                throw new ArgumentNullException(nameof(roles), "Property is required for class GetUserRolesResponseData.");

            return new GetUserRolesResponseData(orgCode, roles);
        }

        /// <summary>
        /// Serializes a <see cref="GetUserRolesResponseData" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getUserRolesResponseData"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, GetUserRolesResponseData getUserRolesResponseData, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(ref writer, getUserRolesResponseData, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="GetUserRolesResponseData" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getUserRolesResponseData"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(ref Utf8JsonWriter writer, GetUserRolesResponseData getUserRolesResponseData, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteString("org_code", getUserRolesResponseData.OrgCode);
            writer.WritePropertyName("roles");
            JsonSerializer.Serialize(writer, getUserRolesResponseData.Roles, jsonSerializerOptions);
        }
    }
}
