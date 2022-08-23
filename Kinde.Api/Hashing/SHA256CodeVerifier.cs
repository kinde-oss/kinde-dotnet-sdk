using System.Security.Cryptography;

namespace Kinde.Api.Hashing
{
    public class SHA256CodeVerifier : BaseCodeVerifier<SHA256>, ICodeVerifier<HashAlgorithm>
    {
        public SHA256CodeVerifier() : base(SHA256.Create())
        {

        }
    }
}
