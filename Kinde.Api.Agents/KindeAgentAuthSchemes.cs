namespace Kinde.Api.Agents
{
    /// <summary>
    /// The Kinde authentication schemes an application has registered, in registration order.
    /// </summary>
    /// <remarks>
    /// Lets <c>MapKindeProtectedResourceMetadata</c> serve a document for every scheme. Mapping
    /// only one would leave the other schemes' challenges pointing at a path that returns 404,
    /// and which path lost would depend on registration order.
    /// </remarks>
    public class KindeAgentAuthSchemes
    {
        private readonly object _gate = new();
        private readonly List<string> _schemes = new();

        /// <summary>Records a scheme, ignoring a name already present.</summary>
        internal void Add(string authenticationScheme)
        {
            // Locked rather than a concurrent collection: the duplicate check and the append have
            // to happen together, and registration order is part of the contract.
            lock (_gate)
            {
                if (!_schemes.Contains(authenticationScheme, StringComparer.Ordinal))
                {
                    _schemes.Add(authenticationScheme);
                }
            }
        }

        /// <summary>The registered scheme names, in registration order.</summary>
        public IReadOnlyList<string> Names
        {
            get
            {
                lock (_gate)
                {
                    return _schemes.ToArray();
                }
            }
        }
    }
}
