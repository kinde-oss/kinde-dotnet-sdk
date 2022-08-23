using System.Security.Cryptography;
using System.Text;

namespace Kinde.Api.Hashing
{
    public abstract class BaseCodeVerifier<HashMethod> : ICodeVerifier<HashMethod> where HashMethod : HashAlgorithm
    {
        protected HashMethod hasher;

        public BaseCodeVerifier(HashMethod hashMethod)
        {
            hasher = hashMethod;
        }

        public async Task<string> Compute(string code)
        {

            return Convert.ToBase64String(hasher.ComputeHash(Encoding.UTF8.GetBytes(code)))
                .TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }

        public async Task<bool> VerifyCode(string original, string hash)
        {
            return await Compute(original) == hash;
        }
    }
}
