using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Kinde.Api.Model
{
    /// <summary>
    /// Placeholder class for UpdateConnectionRequestOptions
    /// </summary>
    [DataContract(Name = "UpdateConnectionRequestOptions")]
    public partial class UpdateConnectionRequestOptions : IEquatable<UpdateConnectionRequestOptions>
    {
        public UpdateConnectionRequestOptions()
        {
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public override bool Equals(object obj)
        {
            return obj is UpdateConnectionRequestOptions other && Equals(other);
        }

        public bool Equals(UpdateConnectionRequestOptions other)
        {
            return other != null;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
