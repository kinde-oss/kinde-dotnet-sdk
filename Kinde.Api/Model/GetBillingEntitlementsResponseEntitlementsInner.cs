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
    [DataContract(Name = "get_billing_entitlements_response_entitlements_inner")]
    public partial class GetBillingEntitlementsResponseEntitlementsInner : IEquatable<GetBillingEntitlementsResponseEntitlementsInner>
    {
        /// <summary>
        /// </summary>
        /// <param name="id">The friendly id of an entitlement.</param>
        /// <param name="fixedCharge">The price charged if this is an entitlement for a fixed charged.</param>
        /// <param name="priceName">The name of the price associated with the entitlement.</param>
        /// <param name="unitAmount">The price charged for this entitlement in cents.</param>
        /// <param name="featureCode">The feature code of the feature corresponding to this entitlement.</param>
        /// <param name="featureName">The feature name of the feature corresponding to this entitlement.</param>
        public GetBillingEntitlementsResponseEntitlementsInner(string id = default(string), int fixedCharge = default(int), string priceName = default(string), int unitAmount = default(int), string featureCode = default(string), string featureName = default(string), int entitlementLimitMax = default(int), int entitlementLimitMin = default(int))
        {
            this.Id = id;
            this.FixedCharge = fixedCharge;
            this.PriceName = priceName;
            this.UnitAmount = unitAmount;
            this.FeatureCode = featureCode;
            this.FeatureName = featureName;
            this.EntitlementLimitMax = entitlementLimitMax;
            this.EntitlementLimitMin = entitlementLimitMin;
        }

        /// <summary>
        /// </summary>
        /// <value>The friendly id of an entitlement</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// The price charged if this is an entitlement for a fixed charged
        /// </summary>
        /// <value>The price charged if this is an entitlement for a fixed charged</value>
        /// <example>35</example>
        [DataMember(Name = "fixed_charge", EmitDefaultValue = false)]
        public int FixedCharge { get; set; }

        /// <summary>
        /// </summary>
        /// <value>The name of the price associated with the entitlement</value>
        /// <example>Pro gym</example>
        [DataMember(Name = "price_name", EmitDefaultValue = false)]
        public string PriceName { get; set; }

        /// <summary>
        /// The price charged for this entitlement in cents
        /// </summary>
        /// <value>The price charged for this entitlement in cents</value>
        [DataMember(Name = "unit_amount", EmitDefaultValue = false)]
        public int UnitAmount { get; set; }

        /// <summary>
        /// </summary>
        /// <value>The feature code of the feature corresponding to this entitlement</value>
        /// <example>CcdkvEXpbg6UY</example>
        [DataMember(Name = "feature_code", EmitDefaultValue = false)]
        public string FeatureCode { get; set; }

        /// <summary>
        /// </summary>
        /// <value>The feature name of the feature corresponding to this entitlement</value>
        /// <example>Pro Gym</example>
        [DataMember(Name = "feature_name", EmitDefaultValue = false)]
        public string FeatureName { get; set; }

        /// <summary>
        /// The maximum number of units of the feature the customer is entitled to
        /// </summary>
        /// <value>The maximum number of units of the feature the customer is entitled to</value>
        [DataMember(Name = "entitlement_limit_max", EmitDefaultValue = false)]
        public int EntitlementLimitMax { get; set; }

        /// <summary>
        /// The minimum number of units of the feature the customer is entitled to
        /// </summary>
        /// <value>The minimum number of units of the feature the customer is entitled to</value>
        [DataMember(Name = "entitlement_limit_min", EmitDefaultValue = false)]
        public int EntitlementLimitMin { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetBillingEntitlementsResponseEntitlementsInner {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  FixedCharge: ").Append(FixedCharge).Append("\n");
            sb.Append("  PriceName: ").Append(PriceName).Append("\n");
            sb.Append("  UnitAmount: ").Append(UnitAmount).Append("\n");
            sb.Append("  FeatureCode: ").Append(FeatureCode).Append("\n");
            sb.Append("  FeatureName: ").Append(FeatureName).Append("\n");
            sb.Append("  EntitlementLimitMax: ").Append(EntitlementLimitMax).Append("\n");
            sb.Append("  EntitlementLimitMin: ").Append(EntitlementLimitMin).Append("\n");
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
            return this.Equals(input as GetBillingEntitlementsResponseEntitlementsInner);
        }

        /// <summary>
        /// </summary>
        /// <returns>Boolean</returns>
        public bool Equals(GetBillingEntitlementsResponseEntitlementsInner input)
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
                    this.FixedCharge == input.FixedCharge ||
                    this.FixedCharge.Equals(input.FixedCharge)
                ) && 
                (
                    this.PriceName == input.PriceName ||
                    (this.PriceName != null &&
                    this.PriceName.Equals(input.PriceName))
                ) && 
                (
                    this.UnitAmount == input.UnitAmount ||
                    this.UnitAmount.Equals(input.UnitAmount)
                ) && 
                (
                    this.FeatureCode == input.FeatureCode ||
                    (this.FeatureCode != null &&
                    this.FeatureCode.Equals(input.FeatureCode))
                ) && 
                (
                    this.FeatureName == input.FeatureName ||
                    (this.FeatureName != null &&
                    this.FeatureName.Equals(input.FeatureName))
                ) && 
                (
                    this.EntitlementLimitMax == input.EntitlementLimitMax ||
                    this.EntitlementLimitMax.Equals(input.EntitlementLimitMax)
                ) && 
                (
                    this.EntitlementLimitMin == input.EntitlementLimitMin ||
                    this.EntitlementLimitMin.Equals(input.EntitlementLimitMin)
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
                hashCode = (hashCode * 59) + this.FixedCharge.GetHashCode();
                if (this.PriceName != null)
                {
                    hashCode = (hashCode * 59) + this.PriceName.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.UnitAmount.GetHashCode();
                if (this.FeatureCode != null)
                {
                    hashCode = (hashCode * 59) + this.FeatureCode.GetHashCode();
                }
                if (this.FeatureName != null)
                {
                    hashCode = (hashCode * 59) + this.FeatureName.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.EntitlementLimitMax.GetHashCode();
                hashCode = (hashCode * 59) + this.EntitlementLimitMin.GetHashCode();
                return hashCode;
            }
        }

    }

}
