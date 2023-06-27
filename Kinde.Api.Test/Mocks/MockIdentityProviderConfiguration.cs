using Kinde.Api.Models.Configuration;

namespace Kinde.Api.Test.Mocks
{
    internal class MockIdentityProviderConfiguration : IApplicationConfiguration
    {
        public string Domain { get => "https://test.domain.com"; set => throw new NotImplementedException(); }
        public string ReplyUrl { get => "https://test.domain.com/callback"; set => throw new NotImplementedException(); }
        public string LogoutUrl { get => "https://test.domain.com/callback"; set => throw new NotImplementedException(); }

    }
}
