using Kinde.Api.Model;
using Newtonsoft.Json;
using Xunit;

namespace Kinde.Api.Test.Model
{
    public class IdentityNullableDeserializationTests
    {
        [Fact]
        public void Deserialize_IsConfirmedMissing_DefaultsToFalse()
        {
            const string json = """
                {
                  "id": "identity_019617f0cd72460a42192cf37b41084f",
                  "type": "google",
                  "name": "sally@example.com",
                  "email": "sally@example.com",
                  "is_primary": true
                }
                """;

            Identity identity = JsonConvert.DeserializeObject<Identity>(json)!;

            Assert.NotNull(identity);
            Assert.False(identity.IsConfirmed);
        }

        [Fact]
        public void Deserialize_IsConfirmedTrue_RoundtripsCorrectly()
        {
            const string json = """
                {
                  "id": "identity_019617f0cd72460a42192cf37b41084f",
                  "type": "email",
                  "is_confirmed": true,
                  "name": "sally@example.com",
                  "email": "sally@example.com",
                  "is_primary": true
                }
                """;

            Identity identity = JsonConvert.DeserializeObject<Identity>(json)!;

            Assert.NotNull(identity);
            Assert.True(identity.IsConfirmed);
        }
    }
}
