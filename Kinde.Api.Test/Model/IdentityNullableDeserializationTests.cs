using Kinde.Api.Model;
using Newtonsoft.Json;
using Xunit;

namespace Kinde.Api.Test.Model
{
    public class IdentityNullableDeserializationTests
    {
        [Fact]
        public void Deserialize_IsConfirmedMissing_StaysNull()
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
            Assert.Null(identity.IsConfirmed);
        }

        [Fact]
        public void Deserialize_IsConfirmedNull_StaysNull()
        {
            const string json = """
                {
                  "id": "identity_019617f0cd72460a42192cf37b41084f",
                  "type": "email",
                  "is_confirmed": null,
                  "name": "sally@example.com",
                  "is_primary": true
                }
                """;

            Identity identity = JsonConvert.DeserializeObject<Identity>(json)!;

            Assert.NotNull(identity);
            Assert.Null(identity.IsConfirmed);
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

        [Fact]
        public void Deserialize_IsConfirmedFalse_RoundtripsCorrectly()
        {
            const string json = """
                {
                  "id": "identity_019617f0cd72460a42192cf37b41084f",
                  "type": "email",
                  "is_confirmed": false,
                  "name": "sally@example.com"
                }
                """;

            Identity identity = JsonConvert.DeserializeObject<Identity>(json)!;

            Assert.NotNull(identity);
            Assert.False(identity.IsConfirmed);
        }
    }
}
