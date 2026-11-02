using System;

namespace Kinde.Api.Mapping
{
    /// <summary>
    /// Thrown when a mapping cannot be performed, for example because the destination type
    /// offers no constructor the mapper can call.
    /// </summary>
    public class KindeMappingException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="KindeMappingException"/> class.</summary>
        public KindeMappingException(string message) : base(message) { }

        /// <summary>Initializes a new instance of the <see cref="KindeMappingException"/> class.</summary>
        public KindeMappingException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
