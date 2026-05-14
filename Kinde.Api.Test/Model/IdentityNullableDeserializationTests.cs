using Kinde.Api.Model;
using Newtonsoft.Json;
using Xunit;

namespace Kinde.Api.Test.Model
{
    // Regression: Social identities can return is_confirmed:null. Identity.IsConfirmed
    // must accept null without throwing during deserialization.
    public class IdentityNullableDeserializationTests
    {
        [Fact]
        public void Deserialize_IsConfirmedNull_DoesNotThrow()
        {
            const string json = """
                {
                  "id": "identity_019617f0cd72460a42192cf37b41084f",
                  "type": "google",
                  "is_confirmed": null,
                  "name": "sally@example.com",
                  "email": "sally@example.com",
                  "is_primary": true
                }
                """;

            Identity identity = JsonConvert.DeserializeObject<Identity>(json)!;

            Assert.NotNull(identity);
            Assert.Null(identity.IsConfirmed);
        }
    }
}
