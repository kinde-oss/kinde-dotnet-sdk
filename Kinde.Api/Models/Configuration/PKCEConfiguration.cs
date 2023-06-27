using Kinde.Api.Hashing;
using System.Security.Cryptography;

namespace Kinde.Api.Models.Configuration
{
    public class PKCEConfiguration<T> : AuthorizationCodeConfiguration, IRedirectAuthorizationConfiguration where T : ICodeVerifier<HashAlgorithm>, new()
    {
        public PKCEConfiguration()
        {
        }

        public ICodeVerifier<HashAlgorithm> CodeVerifier { get; init; }

        public PKCEConfiguration(string clientId, string scope, string clientSecret, string? state, string audience) : base(clientId, scope, clientSecret, state, "PKCE", audience)
        {
            CodeVerifier = new T();
        }
    }
}
