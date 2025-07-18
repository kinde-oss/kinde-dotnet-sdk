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
    [DataContract(Name = "updateApplicationTokens_request")]
    public partial class UpdateApplicationTokensRequest : IEquatable<UpdateApplicationTokensRequest>
    {
        /// <summary>
        /// </summary>
        public UpdateApplicationTokensRequest(int accessTokenLifetime = default(int), int refreshTokenLifetime = default(int), int idTokenLifetime = default(int), int authenticatedSessionLifetime = default(int), bool isHasuraMappingEnabled = default(bool))
        {
            this.AccessTokenLifetime = accessTokenLifetime;
            this.RefreshTokenLifetime = refreshTokenLifetime;
            this.IdTokenLifetime = idTokenLifetime;
            this.AuthenticatedSessionLifetime = authenticatedSessionLifetime;
            this.IsHasuraMappingEnabled = isHasuraMappingEnabled;
        }

        /// <summary>
        /// The lifetime of an access token in seconds.
        /// </summary>
        /// <value>The lifetime of an access token in seconds.</value>
        /// <example>3600</example>
        [DataMember(Name = "access_token_lifetime", EmitDefaultValue = false)]
        public int AccessTokenLifetime { get; set; }

        /// <summary>
        /// The lifetime of a refresh token in seconds.
        /// </summary>
        /// <value>The lifetime of a refresh token in seconds.</value>
        /// <example>86400</example>
        [DataMember(Name = "refresh_token_lifetime", EmitDefaultValue = false)]
        public int RefreshTokenLifetime { get; set; }

        /// <summary>
        /// The lifetime of an ID token in seconds.
        /// </summary>
        /// <value>The lifetime of an ID token in seconds.</value>
        /// <example>3600</example>
        [DataMember(Name = "id_token_lifetime", EmitDefaultValue = false)]
        public int IdTokenLifetime { get; set; }

        /// <summary>
        /// The lifetime of an authenticated session in seconds.
        /// </summary>
        /// <value>The lifetime of an authenticated session in seconds.</value>
        /// <example>86400</example>
        [DataMember(Name = "authenticated_session_lifetime", EmitDefaultValue = false)]
        public int AuthenticatedSessionLifetime { get; set; }

        /// <summary>
        /// Enable or disable Hasura mapping.
        /// </summary>
        /// <value>Enable or disable Hasura mapping.</value>
        /// <example>true</example>
        [DataMember(Name = "is_hasura_mapping_enabled", EmitDefaultValue = true)]
        public bool IsHasuraMappingEnabled { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class UpdateApplicationTokensRequest {\n");
            sb.Append("  AccessTokenLifetime: ").Append(AccessTokenLifetime).Append("\n");
            sb.Append("  RefreshTokenLifetime: ").Append(RefreshTokenLifetime).Append("\n");
            sb.Append("  IdTokenLifetime: ").Append(IdTokenLifetime).Append("\n");
            sb.Append("  AuthenticatedSessionLifetime: ").Append(AuthenticatedSessionLifetime).Append("\n");
            sb.Append("  IsHasuraMappingEnabled: ").Append(IsHasuraMappingEnabled).Append("\n");
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
            return this.Equals(input as UpdateApplicationTokensRequest);
        }

        /// <summary>
        /// </summary>
        /// <returns>Boolean</returns>
        public bool Equals(UpdateApplicationTokensRequest input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.AccessTokenLifetime == input.AccessTokenLifetime ||
                    this.AccessTokenLifetime.Equals(input.AccessTokenLifetime)
                ) && 
                (
                    this.RefreshTokenLifetime == input.RefreshTokenLifetime ||
                    this.RefreshTokenLifetime.Equals(input.RefreshTokenLifetime)
                ) && 
                (
                    this.IdTokenLifetime == input.IdTokenLifetime ||
                    this.IdTokenLifetime.Equals(input.IdTokenLifetime)
                ) && 
                (
                    this.AuthenticatedSessionLifetime == input.AuthenticatedSessionLifetime ||
                    this.AuthenticatedSessionLifetime.Equals(input.AuthenticatedSessionLifetime)
                ) && 
                (
                    this.IsHasuraMappingEnabled == input.IsHasuraMappingEnabled ||
                    this.IsHasuraMappingEnabled.Equals(input.IsHasuraMappingEnabled)
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
                hashCode = (hashCode * 59) + this.AccessTokenLifetime.GetHashCode();
                hashCode = (hashCode * 59) + this.RefreshTokenLifetime.GetHashCode();
                hashCode = (hashCode * 59) + this.IdTokenLifetime.GetHashCode();
                hashCode = (hashCode * 59) + this.AuthenticatedSessionLifetime.GetHashCode();
                hashCode = (hashCode * 59) + this.IsHasuraMappingEnabled.GetHashCode();
                return hashCode;
            }
        }

    }

}
