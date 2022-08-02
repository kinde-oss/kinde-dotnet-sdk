using Kinde.Authorization.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kinde.Authorization.Models.Configuration
{
    public class PKCEConfiguration<T>:AuthorizationCodeConfiguration where T : ICodeVerifier<HashAlgorithm>, new()
    {
        public ICodeVerifier<HashAlgorithm> CodeVerifier { get; init; }
        public PKCEConfiguration(string clientId, string scope, string clientSecret, string? state):base(clientId, scope, clientSecret, state)
        {
            CodeVerifier = new T();
        }
    }
}
