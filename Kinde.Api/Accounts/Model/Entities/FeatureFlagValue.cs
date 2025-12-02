#nullable enable

using System;
using System.Text;
using System.Text.Json.Serialization;
using OpenAPIClientUtils = Kinde.Accounts.Client.ClientUtils;

namespace Kinde.Accounts.Model.Entities
{
    /// <summary>
    /// Represents a feature flag value (can be string, bool, int, or object)
    /// </summary>
    public partial class FeatureFlagValue
    {
        internal FeatureFlagValue(string varString)
        {
            VarString = varString;
        }

        internal FeatureFlagValue(bool varBool)
        {
            VarBool = varBool;
        }

        internal FeatureFlagValue(int varInt)
        {
            VarInt = varInt;
        }

        internal FeatureFlagValue(Object varObject)
        {
            VarObject = varObject;
        }

        public string? VarString { get; }
        public bool? VarBool { get; }
        public int? VarInt { get; }
        public Object? VarObject { get; }

        public override string ToString()
        {
            if (VarString != null) return VarString;
            if (VarBool.HasValue) return VarBool.Value.ToString();
            if (VarInt.HasValue) return VarInt.Value.ToString();
            if (VarObject != null) return VarObject.ToString();
            return "";
        }
    }
}
