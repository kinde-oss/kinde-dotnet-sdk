using Kinde.Authorization.Models.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Mocks
{
    internal class MockIdentityProviderConfiguration : IIdentityProviderConfiguration
    {
        public string Domain { get => "https://test.domain.com"; set => throw new NotImplementedException(); }
        public string ReplyUrl { get => "https://test.domain.com/callback"; set => throw new NotImplementedException(); }
    }
}
