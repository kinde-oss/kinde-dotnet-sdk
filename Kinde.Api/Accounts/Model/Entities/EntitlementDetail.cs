#nullable enable

using System;
using System.Text;
using System.Text.Json.Serialization;

namespace Kinde.Accounts.Model.Entities
{
    /// <summary>
    /// Represents an entitlement detail
    /// </summary>
    public partial class EntitlementDetail
    {
        [JsonConstructor]
        public EntitlementDetail(string featureKey, string featureName, string id, string priceName, int? entitlementLimitMax = default, int? entitlementLimitMin = default, int? fixedCharge = default, int? unitAmount = default)
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

        [JsonPropertyName("feature_key")]
        public string FeatureKey { get; set; }

        [JsonPropertyName("feature_name")]
        public string FeatureName { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("price_name")]
        public string PriceName { get; set; }

        [JsonPropertyName("entitlement_limit_max")]
        public int? EntitlementLimitMax { get; set; }

        [JsonPropertyName("entitlement_limit_min")]
        public int? EntitlementLimitMin { get; set; }

        [JsonPropertyName("fixed_charge")]
        public int? FixedCharge { get; set; }

        [JsonPropertyName("unit_amount")]
        public int? UnitAmount { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class EntitlementDetail {\n");
            sb.Append("  FeatureKey: ").Append(FeatureKey).Append("\n");
            sb.Append("  FeatureName: ").Append(FeatureName).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}
