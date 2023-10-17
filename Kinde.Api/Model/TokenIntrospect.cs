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
    /// TokenIntrospect
    /// </summary>
    [DataContract(Name = "token_introspect")]
    public partial class TokenIntrospect : IEquatable<TokenIntrospect>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenIntrospect" /> class.
        /// </summary>
        /// <param name="active">Indicates the status of the token..</param>
        /// <param name="aud">Array of intended token recipients..</param>
        /// <param name="clientId">Identifier for the requesting client..</param>
        /// <param name="exp">Token expiration timestamp..</param>
        /// <param name="iat">Token issuance timestamp..</param>
        public TokenIntrospect(bool active = default(bool), List<string> aud = default(List<string>), string clientId = default(string), string exp = default(string), string iat = default(string))
        {
            this.Active = active;
            this.Aud = aud;
            this.ClientId = clientId;
            this.Exp = exp;
            this.Iat = iat;
        }

        /// <summary>
        /// Indicates the status of the token.
        /// </summary>
        /// <value>Indicates the status of the token.</value>
        [DataMember(Name = "active", EmitDefaultValue = true)]
        public bool Active { get; set; }

        /// <summary>
        /// Array of intended token recipients.
        /// </summary>
        /// <value>Array of intended token recipients.</value>
        [DataMember(Name = "aud", EmitDefaultValue = false)]
        public List<string> Aud { get; set; }

        /// <summary>
        /// Identifier for the requesting client.
        /// </summary>
        /// <value>Identifier for the requesting client.</value>
        [DataMember(Name = "client_id", EmitDefaultValue = false)]
        public string ClientId { get; set; }

        /// <summary>
        /// Token expiration timestamp.
        /// </summary>
        /// <value>Token expiration timestamp.</value>
        [DataMember(Name = "exp", EmitDefaultValue = false)]
        public string Exp { get; set; }

        /// <summary>
        /// Token issuance timestamp.
        /// </summary>
        /// <value>Token issuance timestamp.</value>
        [DataMember(Name = "iat", EmitDefaultValue = false)]
        public string Iat { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class TokenIntrospect {\n");
            sb.Append("  Active: ").Append(Active).Append("\n");
            sb.Append("  Aud: ").Append(Aud).Append("\n");
            sb.Append("  ClientId: ").Append(ClientId).Append("\n");
            sb.Append("  Exp: ").Append(Exp).Append("\n");
            sb.Append("  Iat: ").Append(Iat).Append("\n");
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
            return this.Equals(input as TokenIntrospect);
        }

        /// <summary>
        /// Returns true if TokenIntrospect instances are equal
        /// </summary>
        /// <param name="input">Instance of TokenIntrospect to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TokenIntrospect input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.Active == input.Active ||
                    this.Active.Equals(input.Active)
                ) && 
                (
                    this.Aud == input.Aud ||
                    this.Aud != null &&
                    input.Aud != null &&
                    this.Aud.SequenceEqual(input.Aud)
                ) && 
                (
                    this.ClientId == input.ClientId ||
                    (this.ClientId != null &&
                    this.ClientId.Equals(input.ClientId))
                ) && 
                (
                    this.Exp == input.Exp ||
                    (this.Exp != null &&
                    this.Exp.Equals(input.Exp))
                ) && 
                (
                    this.Iat == input.Iat ||
                    (this.Iat != null &&
                    this.Iat.Equals(input.Iat))
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
                hashCode = (hashCode * 59) + this.Active.GetHashCode();
                if (this.Aud != null)
                {
                    hashCode = (hashCode * 59) + this.Aud.GetHashCode();
                }
                if (this.ClientId != null)
                {
                    hashCode = (hashCode * 59) + this.ClientId.GetHashCode();
                }
                if (this.Exp != null)
                {
                    hashCode = (hashCode * 59) + this.Exp.GetHashCode();
                }
                if (this.Iat != null)
                {
                    hashCode = (hashCode * 59) + this.Iat.GetHashCode();
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
