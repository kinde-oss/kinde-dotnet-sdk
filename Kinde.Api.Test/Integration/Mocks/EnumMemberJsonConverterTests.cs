using System.Text.Json;
using Xunit;
using KiotaModels = Kinde.Api.Kiota.Management.Models;

namespace Kinde.Api.Test.Integration.Mocks
{
    /// <summary>
    /// Regression tests for EnumMemberJsonConverterFactory: KiotaMockHttpHandler must serialize enums as their [EnumMember] string,
    /// not the underlying int. Get_application_response_application_type is used because its values ("m2m") differ from the member names (M2m).
    /// </summary>
    public class EnumMemberJsonConverterTests
    {
        private static readonly JsonSerializerOptions Options = KiotaJsonOptions.Create();

        [Fact]
        public void Write_UsesEnumMemberValue_WhenDifferentFromCSharpName()
        {
            var json = JsonSerializer.Serialize(KiotaModels.Get_application_response_application_type.M2m, Options);

            Assert.Equal("\"m2m\"", json);
        }

        [Fact]
        public void Read_MapsEnumMemberValueBackToEnumMember()
        {
            var value = JsonSerializer.Deserialize<KiotaModels.Get_application_response_application_type>("\"m2m\"", Options);

            Assert.Equal(KiotaModels.Get_application_response_application_type.M2m, value);
        }

        [Fact]
        public void Read_UnknownValue_ReturnsDefaultEnumMember()
        {
            var value = JsonSerializer.Deserialize<KiotaModels.Get_application_response_application_type>("\"not_a_real_type\"", Options);

            Assert.Equal(default(KiotaModels.Get_application_response_application_type), value);
        }
    }
}
