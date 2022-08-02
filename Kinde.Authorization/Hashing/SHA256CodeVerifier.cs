using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Kinde.Authorization.Hashing
{
    public class SHA256CodeVerifier : BaseCodeVerifier<SHA256>, ICodeVerifier<HashAlgorithm>
    {
        public SHA256CodeVerifier() : base(SHA256.Create())
        {

        }
    }
}
