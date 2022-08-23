using Kinde.Authorization.Hashing;
using Kinde.Authorization.Models.Configuration;

namespace UnitTests.Mocks.Flows
{
    internal class MockPKCEConfiguration : PKCEConfiguration<SHA256CodeVerifier>
    {
        const string state = "112312321232";
        public MockPKCEConfiguration() : base("123", "123", "111111", state)
        {

        }
    }
}
