using Kinde.Api.Models.Configuration;

namespace Kinde.Api.Test.Mocks.Flows
{
    internal class MockAuthCodeConfiguration : AuthorizationCodeConfiguration
    {
        public MockAuthCodeConfiguration() : base("123", "123", "123", "1111111111111111", "https://test.test")
        {

        }
    }
}
