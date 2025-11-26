#nullable enable

using System;
using System.Text;
using System.Text.Json.Serialization;

namespace Kinde.Accounts.Model.Entities
{
    /// <summary>
    /// Represents an entitlement
    /// </summary>
    public partial class Entitlement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Entitlement" /> class.
        /// </summary>
        /// <param name="featureKey">The feature key</param>
        /// <param name="featureName">The feature name</param>
        /// <param name="id">The ID</param>
        /// <param name="priceName">The price name</param>
        /// <param name="entitlementLimitMax">Maximum entitlement limit (optional)</param>
        /// <param name="entitlementLimitMin">Minimum entitlement limit (optional)</param>
        /// <param name="fixedCharge">Fixed charge (optional)</param>
        /// <param name="unitAmount">Unit amount (optional)</param>
        [JsonConstructor]
        public Entitlement(string featureKey, string featureName, string id, string priceName, int? entitlementLimitMax = default, int? entitlementLimitMin = default, int? fixedCharge = default, int? unitAmount = default)
        {
            FeatureKey = featureKey;
            FeatureName = featureName;
            Id = id;
            PriceName = priceName;
            EntitlementLimitMax = entitlementLimitMax;
            EntitlementLimitMin = entitlementLimitMin;
            FixedCharge = fixedCharge;
            UnitAmount = unitAmount;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// The feature key
        /// </summary>
        [JsonPropertyName("feature_key")]
        public string FeatureKey { get; set; }

        /// <summary>
        /// The feature name
        /// </summary>
        [JsonPropertyName("feature_name")]
        public string FeatureName { get; set; }

        /// <summary>
        /// The ID
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The price name
        /// </summary>
        [JsonPropertyName("price_name")]
        public string PriceName { get; set; }

        /// <summary>
        /// Maximum entitlement limit
        /// </summary>
        [JsonPropertyName("entitlement_limit_max")]
        public int? EntitlementLimitMax { get; set; }

        /// <summary>
        /// Minimum entitlement limit
        /// </summary>
        [JsonPropertyName("entitlement_limit_min")]
        public int? EntitlementLimitMin { get; set; }

        /// <summary>
        /// Fixed charge
        /// </summary>
        [JsonPropertyName("fixed_charge")]
        public int? FixedCharge { get; set; }

        /// <summary>
        /// Unit amount
        /// </summary>
        [JsonPropertyName("unit_amount")]
        public int? UnitAmount { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class Entitlement {\n");
            sb.Append("  FeatureKey: ").Append(FeatureKey).Append("\n");
            sb.Append("  FeatureName: ").Append(FeatureName).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  PriceName: ").Append(PriceName).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}
