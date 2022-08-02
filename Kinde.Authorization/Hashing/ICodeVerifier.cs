using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinde.Authorization.Hashing
{
    public interface ICodeVerifier<HashMethod> where HashMethod : System.Security.Cryptography.HashAlgorithm
    {

        public Task<bool> VerifyCode(string original, string hash);

        public Task<string> Compute(string code);
    }
}
