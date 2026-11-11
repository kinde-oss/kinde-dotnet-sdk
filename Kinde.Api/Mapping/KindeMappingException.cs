using System;

namespace Kinde.Api.Mapping
{
    /// <summary>
    /// Thrown when a mapping cannot be performed, such as a destination type with no
    /// constructor the mapper can call.
    /// </summary>
    public class KindeMappingException : Exception
    {
        /// <summary>Creates the exception with a message.</summary>
        public KindeMappingException(string message) : base(message) { }

        /// <summary>Creates the exception with a message and the failure that caused it.</summary>
        public KindeMappingException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
