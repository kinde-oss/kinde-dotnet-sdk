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
    [DataContract(Name = "get_identities_response")]
    public partial class GetIdentitiesResponse : IEquatable<GetIdentitiesResponse>
    {
        /// <summary>
        /// </summary>
        /// <param name="code">Response code..</param>
        /// <param name="message">Response message..</param>
        /// <param name="identities">identities.</param>
        /// <param name="hasMore">Whether more records exist..</param>
        public GetIdentitiesResponse(string code = default(string), string message = default(string), List<Identity> identities = default(List<Identity>), bool hasMore = default(bool))
        {
            this.Code = code;
            this.Message = message;
            this.Identities = identities;
            this.HasMore = hasMore;
        }

        /// <summary>
        /// Response code.
        /// </summary>
        /// <value>Response code.</value>
        [DataMember(Name = "code", EmitDefaultValue = false)]
        public string Code { get; set; }

        /// <summary>
        /// Response message.
        /// </summary>
        /// <value>Response message.</value>
        [DataMember(Name = "message", EmitDefaultValue = false)]
        public string Message { get; set; }

        /// <summary>
        /// </summary>
        [DataMember(Name = "identities", EmitDefaultValue = false)]
        public List<Identity> Identities { get; set; }

        /// <summary>
        /// Whether more records exist.
        /// </summary>
        /// <value>Whether more records exist.</value>
        [DataMember(Name = "has_more", EmitDefaultValue = true)]
        public bool HasMore { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetIdentitiesResponse {\n");
            sb.Append("  Code: ").Append(Code).Append("\n");
            sb.Append("  Message: ").Append(Message).Append("\n");
            sb.Append("  Identities: ").Append(Identities).Append("\n");
            sb.Append("  HasMore: ").Append(HasMore).Append("\n");
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
            return this.Equals(input as GetIdentitiesResponse);
        }

        /// <summary>
        /// </summary>
        /// <returns>Boolean</returns>
        public bool Equals(GetIdentitiesResponse input)
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
                    this.Message == input.Message ||
                    (this.Message != null &&
                    this.Message.Equals(input.Message))
                ) && 
                (
                    this.Identities == input.Identities ||
                    this.Identities != null &&
                    input.Identities != null &&
                    this.Identities.SequenceEqual(input.Identities)
                ) && 
                (
                    this.HasMore == input.HasMore ||
                    this.HasMore.Equals(input.HasMore)
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
                if (this.Message != null)
                {
                    hashCode = (hashCode * 59) + this.Message.GetHashCode();
                }
                if (this.Identities != null)
                {
                    hashCode = (hashCode * 59) + this.Identities.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.HasMore.GetHashCode();
                return hashCode;
            }
        }

    }

}
