#nullable enable

using System;
using System.Text;
using System.Text.Json.Serialization;
using OpenAPIClientUtils = Kinde.Accounts.Client.ClientUtils;

namespace Kinde.Accounts.Model.Entities
{
    /// <summary>
    /// Represents a user property value (can be string, bool, or int)
    /// </summary>
    public partial class UserPropertyValue
    {
        internal UserPropertyValue(string varString)
        {
            VarString = varString;
        }

        internal UserPropertyValue(bool varBool)
        {
            VarBool = varBool;
        }

        internal UserPropertyValue(int varInt)
        {
            VarInt = varInt;
        }

        public string? VarString { get; }
        public bool? VarBool { get; }
        public int? VarInt { get; }

        public override string ToString()
        {
            if (VarString != null) return VarString;
            if (VarBool.HasValue) return VarBool.Value.ToString();
            if (VarInt.HasValue) return VarInt.Value.ToString();
            return "";
        }
    }
}
