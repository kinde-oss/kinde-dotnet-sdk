using Kinde.Api.Models.Configuration;

namespace Kinde.Api.Test.Mocks.Flows
{
    internal class MockClientConfiguration : ClientCredentialsConfiguration
    {
        public MockClientConfiguration() : base("123", "123", "123", "https://test.test")
        {

        }
    }
}
