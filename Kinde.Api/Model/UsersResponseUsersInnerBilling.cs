using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Kinde.Api.Model
{
    /// <summary>
    /// Placeholder class for UsersResponseUsersInnerBilling
    /// </summary>
    [DataContract(Name = "UsersResponseUsersInnerBilling")]
    public partial class UsersResponseUsersInnerBilling : IEquatable<UsersResponseUsersInnerBilling>
    {
        public UsersResponseUsersInnerBilling()
        {
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public override bool Equals(object obj)
        {
            return obj is UsersResponseUsersInnerBilling other && Equals(other);
        }

        public bool Equals(UsersResponseUsersInnerBilling other)
        {
            return other != null;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
