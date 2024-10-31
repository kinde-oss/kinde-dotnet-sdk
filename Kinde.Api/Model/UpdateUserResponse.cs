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
    /// UpdateUserResponse
    /// </summary>
    [DataContract(Name = "update_user_response")]
    public partial class UpdateUserResponse : IEquatable<UpdateUserResponse>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserResponse" /> class.
        /// </summary>
        /// <param name="id">Unique ID of the user in Kinde..</param>
        /// <param name="givenName">User&#39;s first name..</param>
        /// <param name="familyName">User&#39;s last name..</param>
        /// <param name="email">User&#39;s preferred email..</param>
        /// <param name="isSuspended">Whether the user is currently suspended or not..</param>
        /// <param name="isPasswordResetRequested">Whether a password reset has been requested..</param>
        /// <param name="picture">User&#39;s profile picture URL..</param>
        public UpdateUserResponse(string id = default(string), string givenName = default(string), string familyName = default(string), string email = default(string), bool isSuspended = default(bool), bool isPasswordResetRequested = default(bool), string picture = default(string))
        {
            this.Id = id;
            this.GivenName = givenName;
            this.FamilyName = familyName;
            this.Email = email;
            this.IsSuspended = isSuspended;
            this.IsPasswordResetRequested = isPasswordResetRequested;
            this.Picture = picture;
        }

        /// <summary>
        /// Unique ID of the user in Kinde.
        /// </summary>
        /// <value>Unique ID of the user in Kinde.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

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
        /// User&#39;s preferred email.
        /// </summary>
        /// <value>User&#39;s preferred email.</value>
        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }

        /// <summary>
        /// Whether the user is currently suspended or not.
        /// </summary>
        /// <value>Whether the user is currently suspended or not.</value>
        [DataMember(Name = "is_suspended", EmitDefaultValue = true)]
        public bool IsSuspended { get; set; }

        /// <summary>
        /// Whether a password reset has been requested.
        /// </summary>
        /// <value>Whether a password reset has been requested.</value>
        [DataMember(Name = "is_password_reset_requested", EmitDefaultValue = true)]
        public bool IsPasswordResetRequested { get; set; }

        /// <summary>
        /// User&#39;s profile picture URL.
        /// </summary>
        /// <value>User&#39;s profile picture URL.</value>
        [DataMember(Name = "picture", EmitDefaultValue = true)]
        public string Picture { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class UpdateUserResponse {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  GivenName: ").Append(GivenName).Append("\n");
            sb.Append("  FamilyName: ").Append(FamilyName).Append("\n");
            sb.Append("  Email: ").Append(Email).Append("\n");
            sb.Append("  IsSuspended: ").Append(IsSuspended).Append("\n");
            sb.Append("  IsPasswordResetRequested: ").Append(IsPasswordResetRequested).Append("\n");
            sb.Append("  Picture: ").Append(Picture).Append("\n");
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
            return this.Equals(input as UpdateUserResponse);
        }

        /// <summary>
        /// Returns true if UpdateUserResponse instances are equal
        /// </summary>
        /// <param name="input">Instance of UpdateUserResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(UpdateUserResponse input)
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
                    this.Email == input.Email ||
                    (this.Email != null &&
                    this.Email.Equals(input.Email))
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
                    this.Picture == input.Picture ||
                    (this.Picture != null &&
                    this.Picture.Equals(input.Picture))
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
                if (this.GivenName != null)
                {
                    hashCode = (hashCode * 59) + this.GivenName.GetHashCode();
                }
                if (this.FamilyName != null)
                {
                    hashCode = (hashCode * 59) + this.FamilyName.GetHashCode();
                }
                if (this.Email != null)
                {
                    hashCode = (hashCode * 59) + this.Email.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.IsSuspended.GetHashCode();
                hashCode = (hashCode * 59) + this.IsPasswordResetRequested.GetHashCode();
                if (this.Picture != null)
                {
                    hashCode = (hashCode * 59) + this.Picture.GetHashCode();
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
