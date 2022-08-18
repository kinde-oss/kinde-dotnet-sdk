using Kinde.Authorization.Models.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Mocks.Flows
{
    internal class MockClientConfiguration :ClientCredentialsConfiguration
    {
        public MockClientConfiguration(): base("123", "123", "123")
        {

        }
    }
}
