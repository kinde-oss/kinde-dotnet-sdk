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
    /// Scopes
    /// </summary>
    [DataContract(Name = "scopes")]
    public partial class Scopes : IEquatable<Scopes>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Scopes" /> class.
        /// </summary>
        /// <param name="id">Scope ID..</param>
        /// <param name="key">Scope key..</param>
        /// <param name="description">Description of scope..</param>
        /// <param name="apiId">API ID..</param>
        public Scopes(string id = default(string), string key = default(string), string description = default(string), string apiId = default(string))
        {
            this.Id = id;
            this.Key = key;
            this.Description = description;
            this.ApiId = apiId;
        }

        /// <summary>
        /// Scope ID.
        /// </summary>
        /// <value>Scope ID.</value>
        /// <example>api_scope_019541f3fa0c874fc47b3ae73585b21c</example>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// Scope key.
        /// </summary>
        /// <value>Scope key.</value>
        /// <example>create:users</example>
        [DataMember(Name = "key", EmitDefaultValue = false)]
        public string Key { get; set; }

        /// <summary>
        /// Description of scope.
        /// </summary>
        /// <value>Description of scope.</value>
        /// <example>Create users</example>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// API ID.
        /// </summary>
        /// <value>API ID.</value>
        /// <example>3635b4431f174de6b789c67481bd0c7a</example>
        [DataMember(Name = "api_id", EmitDefaultValue = false)]
        public string ApiId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class Scopes {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Key: ").Append(Key).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  ApiId: ").Append(ApiId).Append("\n");
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
            return this.Equals(input as Scopes);
        }

        /// <summary>
        /// Returns true if Scopes instances are equal
        /// </summary>
        /// <param name="input">Instance of Scopes to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Scopes input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.Key == input.Key ||
                    (this.Key != null &&
                    this.Key.Equals(input.Key))
                ) && 
                (
                    this.Description == input.Description ||
                    (this.Description != null &&
                    this.Description.Equals(input.Description))
                ) && 
                (
                    this.ApiId == input.ApiId ||
                    (this.ApiId != null &&
                    this.ApiId.Equals(input.ApiId))
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
                if (this.Id != null)
                {
                    hashCode = (hashCode * 59) + this.Id.GetHashCode();
                }
                if (this.Key != null)
                {
                    hashCode = (hashCode * 59) + this.Key.GetHashCode();
                }
                if (this.Description != null)
                {
                    hashCode = (hashCode * 59) + this.Description.GetHashCode();
                }
                if (this.ApiId != null)
                {
                    hashCode = (hashCode * 59) + this.ApiId.GetHashCode();
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
