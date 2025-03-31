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
using System.ComponentModel.DataAnnotations;
using FileParameter = Kinde.Api.Client.FileParameter;
using OpenAPIDateConverter = Kinde.Api.Client.OpenAPIDateConverter;

namespace Kinde.Api.Model
{
    /// <summary>
    /// ReplaceConnectionRequest
    /// </summary>
    [DataContract(Name = "ReplaceConnection_request")]
    public partial class ReplaceConnectionRequest : IEquatable<ReplaceConnectionRequest>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceConnectionRequest" /> class.
        /// </summary>
        /// <param name="name">The internal name of the connection..</param>
        /// <param name="displayName">The public-facing name of the connection..</param>
        /// <param name="enabledApplications">Client IDs of applications in which this connection is to be enabled..</param>
        /// <param name="options">options.</param>
        public ReplaceConnectionRequest(string name = default(string), string displayName = default(string), List<string> enabledApplications = default(List<string>), ReplaceConnectionRequestOptions options = default(ReplaceConnectionRequestOptions))
        {
            this.Name = name;
            this.DisplayName = displayName;
            this.EnabledApplications = enabledApplications;
            this.Options = options;
        }

        /// <summary>
        /// The internal name of the connection.
        /// </summary>
        /// <value>The internal name of the connection.</value>
        /// <example>ConnectionA</example>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// The public-facing name of the connection.
        /// </summary>
        /// <value>The public-facing name of the connection.</value>
        /// <example>Connection</example>
        [DataMember(Name = "display_name", EmitDefaultValue = false)]
        public string DisplayName { get; set; }

        /// <summary>
        /// Client IDs of applications in which this connection is to be enabled.
        /// </summary>
        /// <value>Client IDs of applications in which this connection is to be enabled.</value>
        /// <example>[&quot;c647dbe20f5944e28af97c9184fded22&quot;,&quot;20bbffaa4c5e492a962273039d4ae18b&quot;]</example>
        [DataMember(Name = "enabled_applications", EmitDefaultValue = false)]
        public List<string> EnabledApplications { get; set; }

        /// <summary>
        /// Gets or Sets Options
        /// </summary>
        [DataMember(Name = "options", EmitDefaultValue = false)]
        public ReplaceConnectionRequestOptions Options { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class ReplaceConnectionRequest {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  DisplayName: ").Append(DisplayName).Append("\n");
            sb.Append("  EnabledApplications: ").Append(EnabledApplications).Append("\n");
            sb.Append("  Options: ").Append(Options).Append("\n");
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
            return this.Equals(input as ReplaceConnectionRequest);
        }

        /// <summary>
        /// Returns true if ReplaceConnectionRequest instances are equal
        /// </summary>
        /// <param name="input">Instance of ReplaceConnectionRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ReplaceConnectionRequest input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.DisplayName == input.DisplayName ||
                    (this.DisplayName != null &&
                    this.DisplayName.Equals(input.DisplayName))
                ) && 
                (
                    this.EnabledApplications == input.EnabledApplications ||
                    this.EnabledApplications != null &&
                    input.EnabledApplications != null &&
                    this.EnabledApplications.SequenceEqual(input.EnabledApplications)
                ) && 
                (
                    this.Options == input.Options ||
                    (this.Options != null &&
                    this.Options.Equals(input.Options))
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
                if (this.Name != null)
                {
                    hashCode = (hashCode * 59) + this.Name.GetHashCode();
                }
                if (this.DisplayName != null)
                {
                    hashCode = (hashCode * 59) + this.DisplayName.GetHashCode();
                }
                if (this.EnabledApplications != null)
                {
                    hashCode = (hashCode * 59) + this.EnabledApplications.GetHashCode();
                }
                if (this.Options != null)
                {
                    hashCode = (hashCode * 59) + this.Options.GetHashCode();
                }
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
