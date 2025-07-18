/*
 * Kinde Management API
 *
 *  Provides endpoints to manage your Kinde Businesses.  ## Intro  ## How to use  1. [Set up and authorize a machine-to-machine (M2M) application](https://docs.kinde.com/developer-tools/kinde-api/connect-to-kinde-api/).  2. [Generate a test access token](https://docs.kinde.com/developer-tools/kinde-api/access-token-for-api/)  3. Test request any endpoint using the test token 
 *
 * The version of the OpenAPI document: 1
 * Contact: support@kinde.com
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using OpenAPIDateConverter = Kinde.Api.Client.OpenAPIDateConverter;

namespace Kinde.Api.Model
{
    /// <summary>
    /// </summary>
    [DataContract(Name = "create_organization_response")]
    public partial class CreateOrganizationResponse : IEquatable<CreateOrganizationResponse>
    {
        /// <summary>
        /// </summary>
        /// <param name="message">Response message..</param>
        /// <param name="code">Response code..</param>
        /// <param name="organization">organization.</param>
        public CreateOrganizationResponse(string message = default(string), string code = default(string), CreateOrganizationResponseOrganization organization = default(CreateOrganizationResponseOrganization))
        {
            this.Message = message;
            this.Code = code;
            this.Organization = organization;
        }

        /// <summary>
        /// Response message.
        /// </summary>
        /// <value>Response message.</value>
        /// <example>Success</example>
        [DataMember(Name = "message", EmitDefaultValue = false)]
        public string Message { get; set; }

        /// <summary>
        /// Response code.
        /// </summary>
        /// <value>Response code.</value>
        /// <example>OK</example>
        [DataMember(Name = "code", EmitDefaultValue = false)]
        public string Code { get; set; }

        /// <summary>
        /// </summary>
        [DataMember(Name = "organization", EmitDefaultValue = false)]
        public CreateOrganizationResponseOrganization Organization { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class CreateOrganizationResponse {\n");
            sb.Append("  Message: ").Append(Message).Append("\n");
            sb.Append("  Code: ").Append(Code).Append("\n");
            sb.Append("  Organization: ").Append(Organization).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as CreateOrganizationResponse);
        }

        /// <summary>
        /// </summary>
        /// <returns>Boolean</returns>
        public bool Equals(CreateOrganizationResponse input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.Message == input.Message ||
                    (this.Message != null &&
                    this.Message.Equals(input.Message))
                ) && 
                (
                    this.Code == input.Code ||
                    (this.Code != null &&
                    this.Code.Equals(input.Code))
                ) && 
                (
                    this.Organization == input.Organization ||
                    (this.Organization != null &&
                    this.Organization.Equals(input.Organization))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Message != null)
                {
                    hashCode = (hashCode * 59) + this.Message.GetHashCode();
                }
                if (this.Code != null)
                {
                    hashCode = (hashCode * 59) + this.Code.GetHashCode();
                }
                if (this.Organization != null)
                {
                    hashCode = (hashCode * 59) + this.Organization.GetHashCode();
                }
                return hashCode;
            }
        }

    }

}
