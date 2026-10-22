/*
 * Kinde .NET SDK
 * 
 * Custom contract resolver for JSON serialization.
 * This file is NOT generated - it's a manual addition to support proper
 * serialization of [DataMember] attributes from OpenAPI Generator 7.0.1.
 */

using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Kinde.Api.Client
{
    /// <summary>
    /// Contract resolver that respects [DataMember] attribute names for JSON serialization.
    /// 
    /// OpenAPI Generator 7.0.1 generates models with [DataMember(Name = "snake_case")] attributes
    /// (e.g., [DataMember(Name = "is_verified")]), but Newtonsoft.Json's DefaultContractResolver
    /// with CamelCaseNamingStrategy ignores these attributes and converts property names to camelCase
    /// (e.g., "IsVerified" becomes "isVerified" instead of "is_verified").
    /// 
    /// This resolver ensures the DataMember attribute names are used during JSON serialization,
    /// which is required for proper API communication with the Kinde API.
    /// </summary>
    public class DataMemberContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// Creates a JSON property for the given member, using the DataMember attribute name if specified.
        /// </summary>
        /// <param name="member">The member to create a property for.</param>
        /// <param name="memberSerialization">The member's parent serialization mode.</param>
        /// <returns>A JsonProperty for the member with the correct property name.</returns>
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            // Check for DataMember attribute and use its Name if specified
            var dataMemberAttribute = member.GetCustomAttribute<DataMemberAttribute>();
            if (dataMemberAttribute != null && !string.IsNullOrEmpty(dataMemberAttribute.Name))
            {
                property.PropertyName = dataMemberAttribute.Name;
            }

            return property;
        }
    }
}

