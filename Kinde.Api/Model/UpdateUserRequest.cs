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
    /// UpdateUserRequest
    /// </summary>
    [DataContract(Name = "updateUser_request")]
    public partial class UpdateUserRequest : IEquatable<UpdateUserRequest>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserRequest" /> class.
        /// </summary>
        /// <param name="givenName">User&#39;s first name..</param>
        /// <param name="familyName">User&#39;s last name..</param>
        /// <param name="picture">The user&#39;s profile picture..</param>
        /// <param name="isSuspended">Whether the user is currently suspended or not..</param>
        /// <param name="isPasswordResetRequested">Prompt the user to change their password on next sign in..</param>
        /// <param name="providedId">An external id to reference the user..</param>
        public UpdateUserRequest(string givenName = default(string), string familyName = default(string), string picture = default(string), bool isSuspended = default(bool), bool isPasswordResetRequested = default(bool), string providedId = default(string))
        {
            this.GivenName = givenName;
            this.FamilyName = familyName;
            this.Picture = picture;
            this.IsSuspended = isSuspended;
            this.IsPasswordResetRequested = isPasswordResetRequested;
            this.ProvidedId = providedId;
        }

        /// <summary>
        /// User&#39;s first name.
        /// </summary>
        /// <value>User&#39;s first name.</value>
        [DataMember(Name = "given_name", EmitDefaultValue = false)]
        public string GivenName { get; set; }

        /// <summary>
        /// User&#39;s last name.
        /// </summary>
        /// <value>User&#39;s last name.</value>
        [DataMember(Name = "family_name", EmitDefaultValue = false)]
        public string FamilyName { get; set; }

        /// <summary>
        /// The user&#39;s profile picture.
        /// </summary>
        /// <value>The user&#39;s profile picture.</value>
        [DataMember(Name = "picture", EmitDefaultValue = false)]
        public string Picture { get; set; }

        /// <summary>
        /// Whether the user is currently suspended or not.
        /// </summary>
        /// <value>Whether the user is currently suspended or not.</value>
        [DataMember(Name = "is_suspended", EmitDefaultValue = true)]
        public bool IsSuspended { get; set; }

        /// <summary>
        /// Prompt the user to change their password on next sign in.
        /// </summary>
        /// <value>Prompt the user to change their password on next sign in.</value>
        [DataMember(Name = "is_password_reset_requested", EmitDefaultValue = true)]
        public bool IsPasswordResetRequested { get; set; }

        /// <summary>
        /// An external id to reference the user.
        /// </summary>
        /// <value>An external id to reference the user.</value>
        [DataMember(Name = "provided_id", EmitDefaultValue = false)]
        public string ProvidedId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class UpdateUserRequest {\n");
            sb.Append("  GivenName: ").Append(GivenName).Append("\n");
            sb.Append("  FamilyName: ").Append(FamilyName).Append("\n");
            sb.Append("  Picture: ").Append(Picture).Append("\n");
            sb.Append("  IsSuspended: ").Append(IsSuspended).Append("\n");
            sb.Append("  IsPasswordResetRequested: ").Append(IsPasswordResetRequested).Append("\n");
            sb.Append("  ProvidedId: ").Append(ProvidedId).Append("\n");
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
            return this.Equals(input as UpdateUserRequest);
        }

        /// <summary>
        /// Returns true if UpdateUserRequest instances are equal
        /// </summary>
        /// <param name="input">Instance of UpdateUserRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(UpdateUserRequest input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.GivenName == input.GivenName ||
                    (this.GivenName != null &&
                    this.GivenName.Equals(input.GivenName))
                ) && 
                (
                    this.FamilyName == input.FamilyName ||
                    (this.FamilyName != null &&
                    this.FamilyName.Equals(input.FamilyName))
                ) && 
                (
                    this.Picture == input.Picture ||
                    (this.Picture != null &&
                    this.Picture.Equals(input.Picture))
                ) && 
                (
                    this.IsSuspended == input.IsSuspended ||
                    this.IsSuspended.Equals(input.IsSuspended)
                ) && 
                (
                    this.IsPasswordResetRequested == input.IsPasswordResetRequested ||
                    this.IsPasswordResetRequested.Equals(input.IsPasswordResetRequested)
                ) && 
                (
                    this.ProvidedId == input.ProvidedId ||
                    (this.ProvidedId != null &&
                    this.ProvidedId.Equals(input.ProvidedId))
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
                if (this.GivenName != null)
                {
                    hashCode = (hashCode * 59) + this.GivenName.GetHashCode();
                }
                if (this.FamilyName != null)
                {
                    hashCode = (hashCode * 59) + this.FamilyName.GetHashCode();
                }
                if (this.Picture != null)
                {
                    hashCode = (hashCode * 59) + this.Picture.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.IsSuspended.GetHashCode();
                hashCode = (hashCode * 59) + this.IsPasswordResetRequested.GetHashCode();
                if (this.ProvidedId != null)
                {
                    hashCode = (hashCode * 59) + this.ProvidedId.GetHashCode();
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
