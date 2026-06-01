using Kinde.Api.Model;
using Newtonsoft.Json;
using Xunit;

namespace Kinde.Api.Test.Model
{
    // Contract: per the updated OpenAPI spec, Identity.is_confirmed is non-nullable
    // (modelled in C# as `bool`). A missing field deserializes to the default (false);
    // an explicit boolean roundtrips cleanly.
    //
    // History: the previous regression test asserted that is_confirmed could be null
    // (Google social identities used to return null). That contract is no longer
    // honoured by the spec. If the production API resumes returning null, this test
    // will start failing with JsonSerializationException — at which point re-add the
    // NULLABLE_BOOL_OVERRIDES entry in scripts/post-process-generated-code.py.
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
