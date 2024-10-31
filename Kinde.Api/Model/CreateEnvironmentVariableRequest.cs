/*
 * Kinde Management API
 *
 * Provides endpoints to manage your Kinde Businesses
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
    /// CreateEnvironmentVariableRequest
    /// </summary>
    [DataContract(Name = "createEnvironmentVariable_request")]
    public partial class CreateEnvironmentVariableRequest : IEquatable<CreateEnvironmentVariableRequest>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEnvironmentVariableRequest" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected CreateEnvironmentVariableRequest() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEnvironmentVariableRequest" /> class.
        /// </summary>
        /// <param name="key">The name of the environment variable (max 128 characters). (required).</param>
        /// <param name="value">The value of the new environment variable (max 2048 characters). (required).</param>
        /// <param name="isSecret">Whether the environment variable is sensitive. Secrets are not-readable by you or your team after creation..</param>
        public CreateEnvironmentVariableRequest(string key = default(string), string value = default(string), bool isSecret = default(bool))
        {
            // to ensure "key" is required (not null)
            if (key == null)
            {
                throw new ArgumentNullException("key is a required property for CreateEnvironmentVariableRequest and cannot be null");
            }
            this.Key = key;
            // to ensure "value" is required (not null)
            if (value == null)
            {
                throw new ArgumentNullException("value is a required property for CreateEnvironmentVariableRequest and cannot be null");
            }
            this.Value = value;
            this.IsSecret = isSecret;
        }

        /// <summary>
        /// The name of the environment variable (max 128 characters).
        /// </summary>
        /// <value>The name of the environment variable (max 128 characters).</value>
        /// <example>MY_API_KEY</example>
        [DataMember(Name = "key", IsRequired = true, EmitDefaultValue = true)]
        public string Key { get; set; }

        /// <summary>
        /// The value of the new environment variable (max 2048 characters).
        /// </summary>
        /// <value>The value of the new environment variable (max 2048 characters).</value>
        /// <example>some-secret-value</example>
        [DataMember(Name = "value", IsRequired = true, EmitDefaultValue = true)]
        public string Value { get; set; }

        /// <summary>
        /// Whether the environment variable is sensitive. Secrets are not-readable by you or your team after creation.
        /// </summary>
        /// <value>Whether the environment variable is sensitive. Secrets are not-readable by you or your team after creation.</value>
        /// <example>false</example>
        [DataMember(Name = "is_secret", EmitDefaultValue = true)]
        public bool IsSecret { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class CreateEnvironmentVariableRequest {\n");
            sb.Append("  Key: ").Append(Key).Append("\n");
            sb.Append("  Value: ").Append(Value).Append("\n");
            sb.Append("  IsSecret: ").Append(IsSecret).Append("\n");
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
            return this.Equals(input as CreateEnvironmentVariableRequest);
        }

        /// <summary>
        /// Returns true if CreateEnvironmentVariableRequest instances are equal
        /// </summary>
        /// <param name="input">Instance of CreateEnvironmentVariableRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CreateEnvironmentVariableRequest input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.Key == input.Key ||
                    (this.Key != null &&
                    this.Key.Equals(input.Key))
                ) && 
                (
                    this.Value == input.Value ||
                    (this.Value != null &&
                    this.Value.Equals(input.Value))
                ) && 
                (
                    this.IsSecret == input.IsSecret ||
                    this.IsSecret.Equals(input.IsSecret)
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
                if (this.Key != null)
                {
                    hashCode = (hashCode * 59) + this.Key.GetHashCode();
                }
                if (this.Value != null)
                {
                    hashCode = (hashCode * 59) + this.Value.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.IsSecret.GetHashCode();
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
