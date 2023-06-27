using Kinde.Api.Hashing;
using Kinde.Api.Models.Configuration;

namespace Kinde.Api.Test.Mocks.Flows
{
    internal class MockPKCEConfiguration : PKCEConfiguration<SHA256CodeVerifier>
    {
        const string state = "112312321232";
        public MockPKCEConfiguration() : base("123", "123", "111111", state, "https://test.test")
        {

        }
    }
}
