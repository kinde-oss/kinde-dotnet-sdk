using Kinde.Authorization.Hashing;
using Kinde.Authorization.Models.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Mocks.Flows
{
    internal class MockPKCEConfiguration:PKCEConfiguration<SHA256CodeVerifier>
    {
        const string state = "112312321232";
        public MockPKCEConfiguration():base("123","123","111111", state)
        {

        }
    }
}
