using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Kinde.Api.Model
{
    /// <summary>
    /// </summary>
    [DataContract(Name = "getPortalLink")]
    public partial class GetPortalLink : IEquatable<GetPortalLink>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPortalLink" /> class.
        /// </summary>
        /// <param name="url">The URL to the portal.</param>
        [JsonConstructorAttribute]
        protected GetPortalLink() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPortalLink" /> class.
        /// </summary>
        /// <param name="url">The URL to the portal.</param>
        public GetPortalLink(string url = default(string))
        {
            this.Url = url;
        }

        /// <summary>
        /// The URL to the portal
        /// </summary>
        /// <value>The URL to the portal</value>
        [DataMember(Name = "url", EmitDefaultValue = false)]
        public string Url { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetPortalLink {\n");
            sb.Append("  Url: ").Append(Url).Append("\n");
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
            return this.Equals(input as GetPortalLink);
        }

        /// <summary>
        /// Returns true if GetPortalLink instances are equal
        /// </summary>
        /// <param name="input">Instance of GetPortalLink to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(GetPortalLink input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.Url == input.Url ||
                    (this.Url != null &&
                    this.Url.Equals(input.Url))
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
                if (this.Url != null)
                {
                    hashCode = (hashCode * 59) + this.Url.GetHashCode();
                }
                return hashCode;
            }
        }
    }
} 