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
    /// CreateOrganizationRequest
    /// </summary>
    [DataContract(Name = "createOrganization_request")]
    public partial class CreateOrganizationRequest : IEquatable<CreateOrganizationRequest>, IValidatableObject
    {
        /// <summary>
        /// Value of the feature flag.
        /// </summary>
        /// <value>Value of the feature flag.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum InnerEnum
        {
            /// <summary>
            /// Enum Str for value: str
            /// </summary>
            [EnumMember(Value = "str")]
            Str = 1,

            /// <summary>
            /// Enum Int for value: int
            /// </summary>
            [EnumMember(Value = "int")]
            Int = 2,

            /// <summary>
            /// Enum Bool for value: bool
            /// </summary>
            [EnumMember(Value = "bool")]
            Bool = 3
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateOrganizationRequest" /> class.
        /// </summary>
        /// <param name="name">The organization&#39;s name..</param>
        /// <param name="featureFlags">The organization&#39;s feature flag settings..</param>
        /// <param name="externalId">The organization&#39;s ID..</param>
        /// <param name="backgroundColor">The organization&#39;s brand settings - background color..</param>
        /// <param name="buttonColor">The organization&#39;s brand settings - button color..</param>
        /// <param name="buttonTextColor">The organization&#39;s brand settings - button text color..</param>
        /// <param name="linkColor">The organization&#39;s brand settings - link color..</param>
        public CreateOrganizationRequest(string name = default(string), Dictionary<string, InnerEnum> featureFlags = default(Dictionary<string, InnerEnum>), string externalId = default(string), string backgroundColor = default(string), string buttonColor = default(string), string buttonTextColor = default(string), string linkColor = default(string))
        {
            this.Name = name;
            this.FeatureFlags = featureFlags;
            this.ExternalId = externalId;
            this.BackgroundColor = backgroundColor;
            this.ButtonColor = buttonColor;
            this.ButtonTextColor = buttonTextColor;
            this.LinkColor = linkColor;
        }

        /// <summary>
        /// The organization&#39;s name.
        /// </summary>
        /// <value>The organization&#39;s name.</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// The organization&#39;s feature flag settings.
        /// </summary>
        /// <value>The organization&#39;s feature flag settings.</value>
        [DataMember(Name = "feature_flags", EmitDefaultValue = false)]
        public Dictionary<string, CreateOrganizationRequest.InnerEnum> FeatureFlags { get; set; }

        /// <summary>
        /// The organization&#39;s ID.
        /// </summary>
        /// <value>The organization&#39;s ID.</value>
        [DataMember(Name = "external_id", EmitDefaultValue = false)]
        public string ExternalId { get; set; }

        /// <summary>
        /// The organization&#39;s brand settings - background color.
        /// </summary>
        /// <value>The organization&#39;s brand settings - background color.</value>
        [DataMember(Name = "background_color", EmitDefaultValue = false)]
        public string BackgroundColor { get; set; }

        /// <summary>
        /// The organization&#39;s brand settings - button color.
        /// </summary>
        /// <value>The organization&#39;s brand settings - button color.</value>
        [DataMember(Name = "button_color", EmitDefaultValue = false)]
        public string ButtonColor { get; set; }

        /// <summary>
        /// The organization&#39;s brand settings - button text color.
        /// </summary>
        /// <value>The organization&#39;s brand settings - button text color.</value>
        [DataMember(Name = "button_text_color", EmitDefaultValue = false)]
        public string ButtonTextColor { get; set; }

        /// <summary>
        /// The organization&#39;s brand settings - link color.
        /// </summary>
        /// <value>The organization&#39;s brand settings - link color.</value>
        [DataMember(Name = "link_color", EmitDefaultValue = false)]
        public string LinkColor { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class CreateOrganizationRequest {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  FeatureFlags: ").Append(FeatureFlags).Append("\n");
            sb.Append("  ExternalId: ").Append(ExternalId).Append("\n");
            sb.Append("  BackgroundColor: ").Append(BackgroundColor).Append("\n");
            sb.Append("  ButtonColor: ").Append(ButtonColor).Append("\n");
            sb.Append("  ButtonTextColor: ").Append(ButtonTextColor).Append("\n");
            sb.Append("  LinkColor: ").Append(LinkColor).Append("\n");
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
            return this.Equals(input as CreateOrganizationRequest);
        }

        /// <summary>
        /// Returns true if CreateOrganizationRequest instances are equal
        /// </summary>
        /// <param name="input">Instance of CreateOrganizationRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CreateOrganizationRequest input)
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
                    this.FeatureFlags == input.FeatureFlags ||
                    this.FeatureFlags != null &&
                    input.FeatureFlags != null &&
                    this.FeatureFlags.SequenceEqual(input.FeatureFlags)
                ) && 
                (
                    this.ExternalId == input.ExternalId ||
                    (this.ExternalId != null &&
                    this.ExternalId.Equals(input.ExternalId))
                ) && 
                (
                    this.BackgroundColor == input.BackgroundColor ||
                    (this.BackgroundColor != null &&
                    this.BackgroundColor.Equals(input.BackgroundColor))
                ) && 
                (
                    this.ButtonColor == input.ButtonColor ||
                    (this.ButtonColor != null &&
                    this.ButtonColor.Equals(input.ButtonColor))
                ) && 
                (
                    this.ButtonTextColor == input.ButtonTextColor ||
                    (this.ButtonTextColor != null &&
                    this.ButtonTextColor.Equals(input.ButtonTextColor))
                ) && 
                (
                    this.LinkColor == input.LinkColor ||
                    (this.LinkColor != null &&
                    this.LinkColor.Equals(input.LinkColor))
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
                if (this.FeatureFlags != null)
                {
                    hashCode = (hashCode * 59) + this.FeatureFlags.GetHashCode();
                }
                if (this.ExternalId != null)
                {
                    hashCode = (hashCode * 59) + this.ExternalId.GetHashCode();
                }
                if (this.BackgroundColor != null)
                {
                    hashCode = (hashCode * 59) + this.BackgroundColor.GetHashCode();
                }
                if (this.ButtonColor != null)
                {
                    hashCode = (hashCode * 59) + this.ButtonColor.GetHashCode();
                }
                if (this.ButtonTextColor != null)
                {
                    hashCode = (hashCode * 59) + this.ButtonTextColor.GetHashCode();
                }
                if (this.LinkColor != null)
                {
                    hashCode = (hashCode * 59) + this.LinkColor.GetHashCode();
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
