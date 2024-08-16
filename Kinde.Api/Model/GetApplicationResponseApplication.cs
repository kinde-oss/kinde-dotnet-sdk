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
    /// GetApplicationResponseApplication
    /// </summary>
    [DataContract(Name = "get_application_response_application")]
    public partial class GetApplicationResponseApplication : IEquatable<GetApplicationResponseApplication>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetApplicationResponseApplication" /> class.
        /// </summary>
        /// <param name="id">The application&#39;s identifier..</param>
        /// <param name="name">The application&#39;s name..</param>
        /// <param name="type">The application&#39;s type..</param>
        /// <param name="clientId">The application&#39;s client id..</param>
        /// <param name="clientSecret">The application&#39;s client secret..</param>
        /// <param name="loginUri">The default login route for resolving session issues..</param>
        /// <param name="homepageUri">The homepage link to your application..</param>
        public GetApplicationResponseApplication(string id = default(string), string name = default(string), string type = default(string), string clientId = default(string), string clientSecret = default(string), string loginUri = default(string), string homepageUri = default(string))
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
            this.LoginUri = loginUri;
            this.HomepageUri = homepageUri;
        }

        /// <summary>
        /// The application&#39;s identifier.
        /// </summary>
        /// <value>The application&#39;s identifier.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// The application&#39;s name.
        /// </summary>
        /// <value>The application&#39;s name.</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// The application&#39;s type.
        /// </summary>
        /// <value>The application&#39;s type.</value>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <summary>
        /// The application&#39;s client id.
        /// </summary>
        /// <value>The application&#39;s client id.</value>
        [DataMember(Name = "client_id", EmitDefaultValue = false)]
        public string ClientId { get; set; }

        /// <summary>
        /// The application&#39;s client secret.
        /// </summary>
        /// <value>The application&#39;s client secret.</value>
        [DataMember(Name = "client_secret", EmitDefaultValue = false)]
        public string ClientSecret { get; set; }

        /// <summary>
        /// The default login route for resolving session issues.
        /// </summary>
        /// <value>The default login route for resolving session issues.</value>
        [DataMember(Name = "login_uri", EmitDefaultValue = false)]
        public string LoginUri { get; set; }

        /// <summary>
        /// The homepage link to your application.
        /// </summary>
        /// <value>The homepage link to your application.</value>
        [DataMember(Name = "homepage_uri", EmitDefaultValue = false)]
        public string HomepageUri { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetApplicationResponseApplication {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  ClientId: ").Append(ClientId).Append("\n");
            sb.Append("  ClientSecret: ").Append(ClientSecret).Append("\n");
            sb.Append("  LoginUri: ").Append(LoginUri).Append("\n");
            sb.Append("  HomepageUri: ").Append(HomepageUri).Append("\n");
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
            return this.Equals(input as GetApplicationResponseApplication);
        }

        /// <summary>
        /// Returns true if GetApplicationResponseApplication instances are equal
        /// </summary>
        /// <param name="input">Instance of GetApplicationResponseApplication to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(GetApplicationResponseApplication input)
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
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.Type == input.Type ||
                    (this.Type != null &&
                    this.Type.Equals(input.Type))
                ) && 
                (
                    this.ClientId == input.ClientId ||
                    (this.ClientId != null &&
                    this.ClientId.Equals(input.ClientId))
                ) && 
                (
                    this.ClientSecret == input.ClientSecret ||
                    (this.ClientSecret != null &&
                    this.ClientSecret.Equals(input.ClientSecret))
                ) && 
                (
                    this.LoginUri == input.LoginUri ||
                    (this.LoginUri != null &&
                    this.LoginUri.Equals(input.LoginUri))
                ) && 
                (
                    this.HomepageUri == input.HomepageUri ||
                    (this.HomepageUri != null &&
                    this.HomepageUri.Equals(input.HomepageUri))
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
                if (this.Name != null)
                {
                    hashCode = (hashCode * 59) + this.Name.GetHashCode();
                }
                if (this.Type != null)
                {
                    hashCode = (hashCode * 59) + this.Type.GetHashCode();
                }
                if (this.ClientId != null)
                {
                    hashCode = (hashCode * 59) + this.ClientId.GetHashCode();
                }
                if (this.ClientSecret != null)
                {
                    hashCode = (hashCode * 59) + this.ClientSecret.GetHashCode();
                }
                if (this.LoginUri != null)
                {
                    hashCode = (hashCode * 59) + this.LoginUri.GetHashCode();
                }
                if (this.HomepageUri != null)
                {
                    hashCode = (hashCode * 59) + this.HomepageUri.GetHashCode();
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
