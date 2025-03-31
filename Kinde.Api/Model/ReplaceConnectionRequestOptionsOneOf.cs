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
using System.ComponentModel.DataAnnotations;
using FileParameter = Kinde.Api.Client.FileParameter;
using OpenAPIDateConverter = Kinde.Api.Client.OpenAPIDateConverter;

namespace Kinde.Api.Model
{
    /// <summary>
    /// Azure AD connection options.
    /// </summary>
    [DataContract(Name = "ReplaceConnection_request_options_oneOf")]
    public partial class ReplaceConnectionRequestOptionsOneOf : IEquatable<ReplaceConnectionRequestOptionsOneOf>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceConnectionRequestOptionsOneOf" /> class.
        /// </summary>
        /// <param name="clientId">Client ID..</param>
        /// <param name="clientSecret">Client secret..</param>
        /// <param name="homeRealmDomains">List of domains to limit authentication..</param>
        /// <param name="entraIdDomain">Domain for Entra ID..</param>
        /// <param name="isUseCommonEndpoint">Use https://login.windows.net/common instead of a default endpoint..</param>
        /// <param name="isSyncUserProfileOnLogin">Sync user profile data with IDP..</param>
        /// <param name="isRetrieveProviderUserGroups">Include user group info from MS Entra ID..</param>
        /// <param name="isExtendedAttributesRequired">Include additional user profile information..</param>
        public ReplaceConnectionRequestOptionsOneOf(string clientId = default(string), string clientSecret = default(string), List<string> homeRealmDomains = default(List<string>), string entraIdDomain = default(string), bool isUseCommonEndpoint = default(bool), bool isSyncUserProfileOnLogin = default(bool), bool isRetrieveProviderUserGroups = default(bool), bool isExtendedAttributesRequired = default(bool))
        {
            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
            this.HomeRealmDomains = homeRealmDomains;
            this.EntraIdDomain = entraIdDomain;
            this.IsUseCommonEndpoint = isUseCommonEndpoint;
            this.IsSyncUserProfileOnLogin = isSyncUserProfileOnLogin;
            this.IsRetrieveProviderUserGroups = isRetrieveProviderUserGroups;
            this.IsExtendedAttributesRequired = isExtendedAttributesRequired;
        }

        /// <summary>
        /// Client ID.
        /// </summary>
        /// <value>Client ID.</value>
        /// <example>hji7db2146af332akfldfded22</example>
        [DataMember(Name = "client_id", EmitDefaultValue = false)]
        public string ClientId { get; set; }

        /// <summary>
        /// Client secret.
        /// </summary>
        /// <value>Client secret.</value>
        /// <example>19fkjdalg521l23fassf3039d4ae18b</example>
        [DataMember(Name = "client_secret", EmitDefaultValue = false)]
        public string ClientSecret { get; set; }

        /// <summary>
        /// List of domains to limit authentication.
        /// </summary>
        /// <value>List of domains to limit authentication.</value>
        /// <example>[&quot;@kinde.com&quot;,&quot;@kinde.io&quot;]</example>
        [DataMember(Name = "home_realm_domains", EmitDefaultValue = false)]
        public List<string> HomeRealmDomains { get; set; }

        /// <summary>
        /// Domain for Entra ID.
        /// </summary>
        /// <value>Domain for Entra ID.</value>
        /// <example>kinde.com</example>
        [DataMember(Name = "entra_id_domain", EmitDefaultValue = false)]
        public string EntraIdDomain { get; set; }

        /// <summary>
        /// Use https://login.windows.net/common instead of a default endpoint.
        /// </summary>
        /// <value>Use https://login.windows.net/common instead of a default endpoint.</value>
        /// <example>true</example>
        [DataMember(Name = "is_use_common_endpoint", EmitDefaultValue = true)]
        public bool IsUseCommonEndpoint { get; set; }

        /// <summary>
        /// Sync user profile data with IDP.
        /// </summary>
        /// <value>Sync user profile data with IDP.</value>
        /// <example>true</example>
        [DataMember(Name = "is_sync_user_profile_on_login", EmitDefaultValue = true)]
        public bool IsSyncUserProfileOnLogin { get; set; }

        /// <summary>
        /// Include user group info from MS Entra ID.
        /// </summary>
        /// <value>Include user group info from MS Entra ID.</value>
        /// <example>true</example>
        [DataMember(Name = "is_retrieve_provider_user_groups", EmitDefaultValue = true)]
        public bool IsRetrieveProviderUserGroups { get; set; }

        /// <summary>
        /// Include additional user profile information.
        /// </summary>
        /// <value>Include additional user profile information.</value>
        /// <example>true</example>
        [DataMember(Name = "is_extended_attributes_required", EmitDefaultValue = true)]
        public bool IsExtendedAttributesRequired { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class ReplaceConnectionRequestOptionsOneOf {\n");
            sb.Append("  ClientId: ").Append(ClientId).Append("\n");
            sb.Append("  ClientSecret: ").Append(ClientSecret).Append("\n");
            sb.Append("  HomeRealmDomains: ").Append(HomeRealmDomains).Append("\n");
            sb.Append("  EntraIdDomain: ").Append(EntraIdDomain).Append("\n");
            sb.Append("  IsUseCommonEndpoint: ").Append(IsUseCommonEndpoint).Append("\n");
            sb.Append("  IsSyncUserProfileOnLogin: ").Append(IsSyncUserProfileOnLogin).Append("\n");
            sb.Append("  IsRetrieveProviderUserGroups: ").Append(IsRetrieveProviderUserGroups).Append("\n");
            sb.Append("  IsExtendedAttributesRequired: ").Append(IsExtendedAttributesRequired).Append("\n");
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
            return this.Equals(input as ReplaceConnectionRequestOptionsOneOf);
        }

        /// <summary>
        /// Returns true if ReplaceConnectionRequestOptionsOneOf instances are equal
        /// </summary>
        /// <param name="input">Instance of ReplaceConnectionRequestOptionsOneOf to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ReplaceConnectionRequestOptionsOneOf input)
        {
            if (input == null)
            {
                return false;
            }
            return 
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
                    this.HomeRealmDomains == input.HomeRealmDomains ||
                    this.HomeRealmDomains != null &&
                    input.HomeRealmDomains != null &&
                    this.HomeRealmDomains.SequenceEqual(input.HomeRealmDomains)
                ) && 
                (
                    this.EntraIdDomain == input.EntraIdDomain ||
                    (this.EntraIdDomain != null &&
                    this.EntraIdDomain.Equals(input.EntraIdDomain))
                ) && 
                (
                    this.IsUseCommonEndpoint == input.IsUseCommonEndpoint ||
                    this.IsUseCommonEndpoint.Equals(input.IsUseCommonEndpoint)
                ) && 
                (
                    this.IsSyncUserProfileOnLogin == input.IsSyncUserProfileOnLogin ||
                    this.IsSyncUserProfileOnLogin.Equals(input.IsSyncUserProfileOnLogin)
                ) && 
                (
                    this.IsRetrieveProviderUserGroups == input.IsRetrieveProviderUserGroups ||
                    this.IsRetrieveProviderUserGroups.Equals(input.IsRetrieveProviderUserGroups)
                ) && 
                (
                    this.IsExtendedAttributesRequired == input.IsExtendedAttributesRequired ||
                    this.IsExtendedAttributesRequired.Equals(input.IsExtendedAttributesRequired)
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
                if (this.ClientId != null)
                {
                    hashCode = (hashCode * 59) + this.ClientId.GetHashCode();
                }
                if (this.ClientSecret != null)
                {
                    hashCode = (hashCode * 59) + this.ClientSecret.GetHashCode();
                }
                if (this.HomeRealmDomains != null)
                {
                    hashCode = (hashCode * 59) + this.HomeRealmDomains.GetHashCode();
                }
                if (this.EntraIdDomain != null)
                {
                    hashCode = (hashCode * 59) + this.EntraIdDomain.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.IsUseCommonEndpoint.GetHashCode();
                hashCode = (hashCode * 59) + this.IsSyncUserProfileOnLogin.GetHashCode();
                hashCode = (hashCode * 59) + this.IsRetrieveProviderUserGroups.GetHashCode();
                hashCode = (hashCode * 59) + this.IsExtendedAttributesRequired.GetHashCode();
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
