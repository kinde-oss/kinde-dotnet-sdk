using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kinde.Api.Enums
{
    /// <summary>
    /// Portal page options
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PortalPage
    {
        /// <summary>
        /// Profile page
        /// </summary>
        [EnumMember(Value = "profile")]
        Profile = 1,

        /// <summary>
        /// Billing page
        /// </summary>
        [EnumMember(Value = "billing")]
        Billing = 2,

        /// <summary>
        /// Settings page
        /// </summary>
        [EnumMember(Value = "settings")]
        Settings = 3
    }
} 