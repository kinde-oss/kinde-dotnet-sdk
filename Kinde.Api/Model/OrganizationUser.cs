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
    [DataContract(Name = "organization_user")]
    public partial class OrganizationUser : IEquatable<OrganizationUser>
    {
        /// <summary>
        /// </summary>
        /// <param name="id">The unique ID for the user..</param>
        /// <param name="email">The user&#39;s email address..</param>
        /// <param name="fullName">The user&#39;s full name..</param>
        /// <param name="lastName">The user&#39;s last name..</param>
        /// <param name="firstName">The user&#39;s first name..</param>
        /// <param name="picture">The user&#39;s profile picture URL..</param>
        /// <param name="joinedOn">The date the user joined the organization..</param>
        /// <param name="lastAccessedOn">The date the user last accessed the organization..</param>
        /// <param name="roles">The roles the user has in the organization..</param>
        public OrganizationUser(string id = default(string), string email = default(string), string fullName = default(string), string lastName = default(string), string firstName = default(string), string picture = default(string), string joinedOn = default(string), string lastAccessedOn = default(string), List<string> roles = default(List<string>))
        {
            this.Id = id;
            this.Email = email;
            this.FullName = fullName;
            this.LastName = lastName;
            this.FirstName = firstName;
            this.Picture = picture;
            this.JoinedOn = joinedOn;
            this.LastAccessedOn = lastAccessedOn;
            this.Roles = roles;
        }

        /// <summary>
        /// The unique ID for the user.
        /// </summary>
        /// <value>The unique ID for the user.</value>
        [DataMember(Name = "id", EmitDefaultValue = true)]
        public string Id { get; set; }

        /// <summary>
        /// The user&#39;s email address.
        /// </summary>
        /// <value>The user&#39;s email address.</value>
        /// <example>john.snow@example.com</example>
        [DataMember(Name = "email", EmitDefaultValue = true)]
        public string Email { get; set; }

        /// <summary>
        /// The user&#39;s full name.
        /// </summary>
        /// <value>The user&#39;s full name.</value>
        /// <example>John Snow</example>
        [DataMember(Name = "full_name", EmitDefaultValue = false)]
        public string FullName { get; set; }

        /// <summary>
        /// The user&#39;s last name.
        /// </summary>
        /// <value>The user&#39;s last name.</value>
        /// <example>Snow</example>
        [DataMember(Name = "last_name", EmitDefaultValue = true)]
        public string LastName { get; set; }

        /// <summary>
        /// The user&#39;s first name.
        /// </summary>
        /// <value>The user&#39;s first name.</value>
        /// <example>John</example>
        [DataMember(Name = "first_name", EmitDefaultValue = true)]
        public string FirstName { get; set; }

        /// <summary>
        /// The user&#39;s profile picture URL.
        /// </summary>
        /// <value>The user&#39;s profile picture URL.</value>
        /// <example>https://example.com/john_snow.jpg</example>
        [DataMember(Name = "picture", EmitDefaultValue = true)]
        public string Picture { get; set; }

        /// <summary>
        /// The date the user joined the organization.
        /// </summary>
        /// <value>The date the user joined the organization.</value>
        /// <example>2021-01-01T00:00:00Z</example>
        [DataMember(Name = "joined_on", EmitDefaultValue = false)]
        public string JoinedOn { get; set; }

        /// <summary>
        /// The date the user last accessed the organization.
        /// </summary>
        /// <value>The date the user last accessed the organization.</value>
        /// <example>2022-01-01T00:00:00Z</example>
        [DataMember(Name = "last_accessed_on", EmitDefaultValue = true)]
        public string LastAccessedOn { get; set; }

        /// <summary>
        /// The roles the user has in the organization.
        /// </summary>
        /// <value>The roles the user has in the organization.</value>
        [DataMember(Name = "roles", EmitDefaultValue = false)]
        public List<string> Roles { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class OrganizationUser {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Email: ").Append(Email).Append("\n");
            sb.Append("  FullName: ").Append(FullName).Append("\n");
            sb.Append("  LastName: ").Append(LastName).Append("\n");
            sb.Append("  FirstName: ").Append(FirstName).Append("\n");
            sb.Append("  Picture: ").Append(Picture).Append("\n");
            sb.Append("  JoinedOn: ").Append(JoinedOn).Append("\n");
            sb.Append("  LastAccessedOn: ").Append(LastAccessedOn).Append("\n");
            sb.Append("  Roles: ").Append(Roles).Append("\n");
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
            return this.Equals(input as OrganizationUser);
        }

        /// <summary>
        /// </summary>
        /// <returns>Boolean</returns>
        public bool Equals(OrganizationUser input)
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
                    this.Email == input.Email ||
                    (this.Email != null &&
                    this.Email.Equals(input.Email))
                ) && 
                (
                    this.FullName == input.FullName ||
                    (this.FullName != null &&
                    this.FullName.Equals(input.FullName))
                ) && 
                (
                    this.LastName == input.LastName ||
                    (this.LastName != null &&
                    this.LastName.Equals(input.LastName))
                ) && 
                (
                    this.FirstName == input.FirstName ||
                    (this.FirstName != null &&
                    this.FirstName.Equals(input.FirstName))
                ) && 
                (
                    this.Picture == input.Picture ||
                    (this.Picture != null &&
                    this.Picture.Equals(input.Picture))
                ) && 
                (
                    this.JoinedOn == input.JoinedOn ||
                    (this.JoinedOn != null &&
                    this.JoinedOn.Equals(input.JoinedOn))
                ) && 
                (
                    this.LastAccessedOn == input.LastAccessedOn ||
                    (this.LastAccessedOn != null &&
                    this.LastAccessedOn.Equals(input.LastAccessedOn))
                ) && 
                (
                    this.Roles == input.Roles ||
                    this.Roles != null &&
                    input.Roles != null &&
                    this.Roles.SequenceEqual(input.Roles)
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
                if (this.Email != null)
                {
                    hashCode = (hashCode * 59) + this.Email.GetHashCode();
                }
                if (this.FullName != null)
                {
                    hashCode = (hashCode * 59) + this.FullName.GetHashCode();
                }
                if (this.LastName != null)
                {
                    hashCode = (hashCode * 59) + this.LastName.GetHashCode();
                }
                if (this.FirstName != null)
                {
                    hashCode = (hashCode * 59) + this.FirstName.GetHashCode();
                }
                if (this.Picture != null)
                {
                    hashCode = (hashCode * 59) + this.Picture.GetHashCode();
                }
                if (this.JoinedOn != null)
                {
                    hashCode = (hashCode * 59) + this.JoinedOn.GetHashCode();
                }
                if (this.LastAccessedOn != null)
                {
                    hashCode = (hashCode * 59) + this.LastAccessedOn.GetHashCode();
                }
                if (this.Roles != null)
                {
                    hashCode = (hashCode * 59) + this.Roles.GetHashCode();
                }
                return hashCode;
            }
        }

    }

}
