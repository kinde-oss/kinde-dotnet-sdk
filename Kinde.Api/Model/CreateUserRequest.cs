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
    [DataContract(Name = "createUser_request")]
    public partial class CreateUserRequest : IEquatable<CreateUserRequest>
    {
        /// <summary>
        /// </summary>
        /// <param name="profile">profile.</param>
        /// <param name="providedId">An external id to reference the user..</param>
        /// <param name="identities">Array of identities to assign to the created user.</param>
        public CreateUserRequest(CreateUserRequestProfile profile = default(CreateUserRequestProfile), string organizationCode = default(string), string providedId = default(string), List<CreateUserRequestIdentitiesInner> identities = default(List<CreateUserRequestIdentitiesInner>))
        {
            this.Profile = profile;
            this.OrganizationCode = organizationCode;
            this.ProvidedId = providedId;
            this.Identities = identities;
        }

        /// <summary>
        /// Gets or Sets Profile
        /// </summary>
        [DataMember(Name = "profile", EmitDefaultValue = false)]
        public CreateUserRequestProfile Profile { get; set; }

        /// <summary>
        /// The unique code associated with the organization you want the user to join.
        /// </summary>
        /// <value>The unique code associated with the organization you want the user to join.</value>
        [DataMember(Name = "organization_code", EmitDefaultValue = false)]
        public string OrganizationCode { get; set; }

        /// <summary>
        /// An external id to reference the user.
        /// </summary>
        /// <value>An external id to reference the user.</value>
        [DataMember(Name = "provided_id", EmitDefaultValue = false)]
        public string ProvidedId { get; set; }

        /// <summary>
        /// Array of identities to assign to the created user
        /// </summary>
        /// <value>Array of identities to assign to the created user</value>
        /// <example>[{&quot;type&quot;:&quot;email&quot;,&quot;is_verified&quot;:true,&quot;details&quot;:{&quot;email&quot;:&quot;email@email.com&quot;}},{&quot;type&quot;:&quot;phone&quot;,&quot;is_verified&quot;:false,&quot;details&quot;:{&quot;phone&quot;:&quot;+61426148233&quot;,&quot;phone_country_id&quot;:&quot;au&quot;}},{&quot;type&quot;:&quot;username&quot;,&quot;details&quot;:{&quot;username&quot;:&quot;myusername&quot;}}]</example>
        [DataMember(Name = "identities", EmitDefaultValue = false)]
        public List<CreateUserRequestIdentitiesInner> Identities { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class CreateUserRequest {\n");
            sb.Append("  Profile: ").Append(Profile).Append("\n");
            sb.Append("  OrganizationCode: ").Append(OrganizationCode).Append("\n");
            sb.Append("  ProvidedId: ").Append(ProvidedId).Append("\n");
            sb.Append("  Identities: ").Append(Identities).Append("\n");
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
            return this.Equals(input as CreateUserRequest);
        }

        /// <summary>
        /// </summary>
        /// <returns>Boolean</returns>
        public bool Equals(CreateUserRequest input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.Profile == input.Profile ||
                    (this.Profile != null &&
                    this.Profile.Equals(input.Profile))
                ) && 
                (
                    this.OrganizationCode == input.OrganizationCode ||
                    (this.OrganizationCode != null &&
                    this.OrganizationCode.Equals(input.OrganizationCode))
                ) && 
                (
                    this.ProvidedId == input.ProvidedId ||
                    (this.ProvidedId != null &&
                    this.ProvidedId.Equals(input.ProvidedId))
                ) && 
                (
                    this.Identities == input.Identities ||
                    this.Identities != null &&
                    input.Identities != null &&
                    this.Identities.SequenceEqual(input.Identities)
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
                if (this.Profile != null)
                {
                    hashCode = (hashCode * 59) + this.Profile.GetHashCode();
                }
                if (this.OrganizationCode != null)
                {
                    hashCode = (hashCode * 59) + this.OrganizationCode.GetHashCode();
                }
                if (this.ProvidedId != null)
                {
                    hashCode = (hashCode * 59) + this.ProvidedId.GetHashCode();
                }
                if (this.Identities != null)
                {
                    hashCode = (hashCode * 59) + this.Identities.GetHashCode();
                }
                return hashCode;
            }
        }

    }

}
