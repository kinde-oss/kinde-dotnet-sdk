using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Kinde.Api.Client
{
    /// <summary>
    /// Honors [DataMember(Name = ...)] names for JSON properties, which the default
    /// CamelCaseNamingStrategy would otherwise override with camelCase.
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

            var dataMemberAttribute = member.GetCustomAttribute<DataMemberAttribute>();
            if (dataMemberAttribute != null && !string.IsNullOrEmpty(dataMemberAttribute.Name))
            {
                property.PropertyName = dataMemberAttribute.Name;
            }

            return property;
        }
    }
}

