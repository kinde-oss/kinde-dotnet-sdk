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
    /// Organization
    /// </summary>
    [DataContract(Name = "organization")]
    public partial class Organization : IEquatable<Organization>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Organization" /> class.
        /// </summary>
        /// <param name="code">code.</param>
        /// <param name="name">name.</param>
        /// <param name="isDefault">isDefault.</param>
        /// <param name="externalId">externalId.</param>
        /// <param name="logo">logo.</param>
        /// <param name="linkColor">linkColor.</param>
        /// <param name="buttonColor">buttonColor.</param>
        /// <param name="backgroundColor">backgroundColor.</param>
        /// <param name="buttonTextColor">buttonTextColor.</param>
        /// <param name="isAllowRegistrations">isAllowRegistrations.</param>
        public Organization(string code = default(string), string name = default(string), bool isDefault = default(bool), string externalId = default(string), string logo = default(string), string linkColor = default(string), string buttonColor = default(string), string backgroundColor = default(string), string buttonTextColor = default(string), bool isAllowRegistrations = default(bool))
        {
            this.Code = code;
            this.Name = name;
            this.IsDefault = isDefault;
            this.ExternalId = externalId;
            this.Logo = logo;
            this.LinkColor = linkColor;
            this.ButtonColor = buttonColor;
            this.BackgroundColor = backgroundColor;
            this.ButtonTextColor = buttonTextColor;
            this.IsAllowRegistrations = isAllowRegistrations;
        }

        /// <summary>
        /// Gets or Sets Code
        /// </summary>
        [DataMember(Name = "code", EmitDefaultValue = false)]
        public string Code { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets IsDefault
        /// </summary>
        [DataMember(Name = "is_default", EmitDefaultValue = true)]
        public bool IsDefault { get; set; }

        /// <summary>
        /// Gets or Sets ExternalId
        /// </summary>
        [DataMember(Name = "external_id", EmitDefaultValue = false)]
        public string ExternalId { get; set; }

        /// <summary>
        /// Gets or Sets Logo
        /// </summary>
        [DataMember(Name = "logo", EmitDefaultValue = false)]
        public string Logo { get; set; }

        /// <summary>
        /// Gets or Sets LinkColor
        /// </summary>
        [DataMember(Name = "link_color", EmitDefaultValue = false)]
        public string LinkColor { get; set; }

        /// <summary>
        /// Gets or Sets ButtonColor
        /// </summary>
        [DataMember(Name = "button_color", EmitDefaultValue = false)]
        public string ButtonColor { get; set; }

        /// <summary>
        /// Gets or Sets BackgroundColor
        /// </summary>
        [DataMember(Name = "background_color", EmitDefaultValue = false)]
        public string BackgroundColor { get; set; }

        /// <summary>
        /// Gets or Sets ButtonTextColor
        /// </summary>
        [DataMember(Name = "button_text_color", EmitDefaultValue = false)]
        public string ButtonTextColor { get; set; }

        /// <summary>
        /// Gets or Sets IsAllowRegistrations
        /// </summary>
        [DataMember(Name = "is_allow_registrations", EmitDefaultValue = true)]
        public bool IsAllowRegistrations { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class Organization {\n");
            sb.Append("  Code: ").Append(Code).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  IsDefault: ").Append(IsDefault).Append("\n");
            sb.Append("  ExternalId: ").Append(ExternalId).Append("\n");
            sb.Append("  Logo: ").Append(Logo).Append("\n");
            sb.Append("  LinkColor: ").Append(LinkColor).Append("\n");
            sb.Append("  ButtonColor: ").Append(ButtonColor).Append("\n");
            sb.Append("  BackgroundColor: ").Append(BackgroundColor).Append("\n");
            sb.Append("  ButtonTextColor: ").Append(ButtonTextColor).Append("\n");
            sb.Append("  IsAllowRegistrations: ").Append(IsAllowRegistrations).Append("\n");
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
            return this.Equals(input as Organization);
        }

        /// <summary>
        /// Returns true if Organization instances are equal
        /// </summary>
        /// <param name="input">Instance of Organization to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Organization input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.Code == input.Code ||
                    (this.Code != null &&
                    this.Code.Equals(input.Code))
                ) && 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.IsDefault == input.IsDefault ||
                    this.IsDefault.Equals(input.IsDefault)
                ) && 
                (
                    this.ExternalId == input.ExternalId ||
                    (this.ExternalId != null &&
                    this.ExternalId.Equals(input.ExternalId))
                ) && 
                (
                    this.Logo == input.Logo ||
                    (this.Logo != null &&
                    this.Logo.Equals(input.Logo))
                ) && 
                (
                    this.LinkColor == input.LinkColor ||
                    (this.LinkColor != null &&
                    this.LinkColor.Equals(input.LinkColor))
                ) && 
                (
                    this.ButtonColor == input.ButtonColor ||
                    (this.ButtonColor != null &&
                    this.ButtonColor.Equals(input.ButtonColor))
                ) && 
                (
                    this.BackgroundColor == input.BackgroundColor ||
                    (this.BackgroundColor != null &&
                    this.BackgroundColor.Equals(input.BackgroundColor))
                ) && 
                (
                    this.ButtonTextColor == input.ButtonTextColor ||
                    (this.ButtonTextColor != null &&
                    this.ButtonTextColor.Equals(input.ButtonTextColor))
                ) && 
                (
                    this.IsAllowRegistrations == input.IsAllowRegistrations ||
                    this.IsAllowRegistrations.Equals(input.IsAllowRegistrations)
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
                if (this.Code != null)
                {
                    hashCode = (hashCode * 59) + this.Code.GetHashCode();
                }
                if (this.Name != null)
                {
                    hashCode = (hashCode * 59) + this.Name.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.IsDefault.GetHashCode();
                if (this.ExternalId != null)
                {
                    hashCode = (hashCode * 59) + this.ExternalId.GetHashCode();
                }
                if (this.Logo != null)
                {
                    hashCode = (hashCode * 59) + this.Logo.GetHashCode();
                }
                if (this.LinkColor != null)
                {
                    hashCode = (hashCode * 59) + this.LinkColor.GetHashCode();
                }
                if (this.ButtonColor != null)
                {
                    hashCode = (hashCode * 59) + this.ButtonColor.GetHashCode();
                }
                if (this.BackgroundColor != null)
                {
                    hashCode = (hashCode * 59) + this.BackgroundColor.GetHashCode();
                }
                if (this.ButtonTextColor != null)
                {
                    hashCode = (hashCode * 59) + this.ButtonTextColor.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.IsAllowRegistrations.GetHashCode();
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
