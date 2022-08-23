using Kinde.Api.Models.Configuration;

namespace UnitTests.Mocks.Flows
{
    internal class MockClientConfiguration : ClientCredentialsConfiguration
    {
        public MockClientConfiguration() : base("123", "123", "123")
        {

        }
    }
}
