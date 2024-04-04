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
    /// CreatePropertyRequest
    /// </summary>
    [DataContract(Name = "CreateProperty_request")]
    public partial class CreatePropertyRequest : IEquatable<CreatePropertyRequest>, IValidatableObject
    {
        /// <summary>
        /// The property type.
        /// </summary>
        /// <value>The property type.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum TypeEnum
        {
            /// <summary>
            /// Enum SingleLineText for value: single_line_text
            /// </summary>
            [EnumMember(Value = "single_line_text")]
            SingleLineText = 1,

            /// <summary>
            /// Enum MultiLineText for value: multi_line_text
            /// </summary>
            [EnumMember(Value = "multi_line_text")]
            MultiLineText = 2
        }


        /// <summary>
        /// The property type.
        /// </summary>
        /// <value>The property type.</value>
        [DataMember(Name = "type", IsRequired = true, EmitDefaultValue = true)]
        public TypeEnum Type { get; set; }
        /// <summary>
        /// The context that the property applies to.
        /// </summary>
        /// <value>The context that the property applies to.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ContextEnum
        {
            /// <summary>
            /// Enum Org for value: org
            /// </summary>
            [EnumMember(Value = "org")]
            Org = 1,

            /// <summary>
            /// Enum Usr for value: usr
            /// </summary>
            [EnumMember(Value = "usr")]
            Usr = 2
        }


        /// <summary>
        /// The context that the property applies to.
        /// </summary>
        /// <value>The context that the property applies to.</value>
        [DataMember(Name = "context", IsRequired = true, EmitDefaultValue = true)]
        public ContextEnum Context { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePropertyRequest" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected CreatePropertyRequest() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePropertyRequest" /> class.
        /// </summary>
        /// <param name="name">The name of the property. (required).</param>
        /// <param name="description">Description of the property purpose..</param>
        /// <param name="key">The property identifier to use in code. (required).</param>
        /// <param name="type">The property type. (required).</param>
        /// <param name="context">The context that the property applies to. (required).</param>
        /// <param name="isPrivate">Whether the property can be included in id and access tokens. (required).</param>
        /// <param name="categoryId">Which category the property belongs to. (required).</param>
        public CreatePropertyRequest(string name = default(string), string description = default(string), string key = default(string), TypeEnum type = default(TypeEnum), ContextEnum context = default(ContextEnum), bool isPrivate = default(bool), string categoryId = default(string))
        {
            // to ensure "name" is required (not null)
            if (name == null)
            {
                throw new ArgumentNullException("name is a required property for CreatePropertyRequest and cannot be null");
            }
            this.Name = name;
            // to ensure "key" is required (not null)
            if (key == null)
            {
                throw new ArgumentNullException("key is a required property for CreatePropertyRequest and cannot be null");
            }
            this.Key = key;
            this.Type = type;
            this.Context = context;
            this.IsPrivate = isPrivate;
            // to ensure "categoryId" is required (not null)
            if (categoryId == null)
            {
                throw new ArgumentNullException("categoryId is a required property for CreatePropertyRequest and cannot be null");
            }
            this.CategoryId = categoryId;
            this.Description = description;
        }

        /// <summary>
        /// The name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        [DataMember(Name = "name", IsRequired = true, EmitDefaultValue = true)]
        public string Name { get; set; }

        /// <summary>
        /// Description of the property purpose.
        /// </summary>
        /// <value>Description of the property purpose.</value>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// The property identifier to use in code.
        /// </summary>
        /// <value>The property identifier to use in code.</value>
        [DataMember(Name = "key", IsRequired = true, EmitDefaultValue = true)]
        public string Key { get; set; }

        /// <summary>
        /// Whether the property can be included in id and access tokens.
        /// </summary>
        /// <value>Whether the property can be included in id and access tokens.</value>
        [DataMember(Name = "is_private", IsRequired = true, EmitDefaultValue = true)]
        public bool IsPrivate { get; set; }

        /// <summary>
        /// Which category the property belongs to.
        /// </summary>
        /// <value>Which category the property belongs to.</value>
        [DataMember(Name = "category_id", IsRequired = true, EmitDefaultValue = true)]
        public string CategoryId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class CreatePropertyRequest {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  Key: ").Append(Key).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  Context: ").Append(Context).Append("\n");
            sb.Append("  IsPrivate: ").Append(IsPrivate).Append("\n");
            sb.Append("  CategoryId: ").Append(CategoryId).Append("\n");
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
            return this.Equals(input as CreatePropertyRequest);
        }

        /// <summary>
        /// Returns true if CreatePropertyRequest instances are equal
        /// </summary>
        /// <param name="input">Instance of CreatePropertyRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CreatePropertyRequest input)
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
                    this.Description == input.Description ||
                    (this.Description != null &&
                    this.Description.Equals(input.Description))
                ) && 
                (
                    this.Key == input.Key ||
                    (this.Key != null &&
                    this.Key.Equals(input.Key))
                ) && 
                (
                    this.Type == input.Type ||
                    this.Type.Equals(input.Type)
                ) && 
                (
                    this.Context == input.Context ||
                    this.Context.Equals(input.Context)
                ) && 
                (
                    this.IsPrivate == input.IsPrivate ||
                    this.IsPrivate.Equals(input.IsPrivate)
                ) && 
                (
                    this.CategoryId == input.CategoryId ||
                    (this.CategoryId != null &&
                    this.CategoryId.Equals(input.CategoryId))
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
                if (this.Description != null)
                {
                    hashCode = (hashCode * 59) + this.Description.GetHashCode();
                }
                if (this.Key != null)
                {
                    hashCode = (hashCode * 59) + this.Key.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.Type.GetHashCode();
                hashCode = (hashCode * 59) + this.Context.GetHashCode();
                hashCode = (hashCode * 59) + this.IsPrivate.GetHashCode();
                if (this.CategoryId != null)
                {
                    hashCode = (hashCode * 59) + this.CategoryId.GetHashCode();
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
