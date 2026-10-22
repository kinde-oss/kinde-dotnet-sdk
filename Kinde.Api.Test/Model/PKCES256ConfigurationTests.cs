using Kinde.Api.Models.Configuration;
using Xunit;

namespace Kinde.Api.Test.Model
{
    public class PKCES256ConfigurationTests
    {
        [Fact]
        public void Constructor_ForwardsEachArgumentToTheMatchingProperty()
        {
            var config = new PKCES256Configuration(
                clientId: "client-id",
                scope: "openid profile",
                clientSecret: "client-secret",
                state: "explicit-state",
                audience: "https://api.example.com");

            Assert.Equal("client-id", config.ClientId);
            Assert.Equal("openid profile", config.Scope);
            Assert.Equal("client-secret", config.ClientSecret);
            Assert.Equal("explicit-state", config.State);
            Assert.Equal("https://api.example.com", config.Audience);
        }

        [Fact]
        public void Constructor_CalledPositionally_KeepsThe2xArgumentOrder()
        {
            var config = new PKCES256Configuration("client-id", "openid profile", "client-secret", "explicit-state", "https://api.example.com");

            Assert.Equal("client-id", config.ClientId);
            Assert.Equal("openid profile", config.Scope);
            Assert.Equal("client-secret", config.ClientSecret);
            Assert.Equal("explicit-state", config.State);
            Assert.Equal("https://api.example.com", config.Audience);
        }

        [Fact]
        public void Constructor_WithoutState_IsCallableWithFourArguments()
        {
            var config = new PKCES256Configuration(
                clientId: "client-id",
                scope: "openid profile",
                clientSecret: "client-secret",
                audience: "https://api.example.com");

            Assert.Equal("client-id", config.ClientId);
            Assert.Equal("openid profile", config.Scope);
            Assert.Equal("client-secret", config.ClientSecret);
            Assert.Equal("https://api.example.com", config.Audience);
            Assert.False(string.IsNullOrEmpty(config.State));
        }
    }
}
