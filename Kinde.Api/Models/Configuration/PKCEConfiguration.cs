using Kinde.Api.Hashing;
using System.Security.Cryptography;

namespace Kinde.Api.Models.Configuration
{
    public class PKCEConfiguration<T> : AuthorizationCodeConfiguration, IRedirectAuthorizationConfiguration where T : ICodeVerifier<HashAlgorithm>, new()
    {
        public ICodeVerifier<HashAlgorithm> CodeVerifier { get; init; }
        public PKCEConfiguration(string clientId, string scope, string clientSecret, string? state) : base(clientId, scope, clientSecret, state, "PKCE")
        {
            CodeVerifier = new T();
        }
    }
}
