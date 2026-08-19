/*
 * Regression test for a CodeRabbit finding on PR #109: PKCES256Configuration's 5-arg
 * constructor forwarded its arguments to PKCEConfiguration<T>'s base constructor by
 * position without matching parameter order, so every property past ClientId ended up
 * holding the wrong value (Scope got ClientSecret's value, ClientSecret got RedirectUri's
 * value, State got Scope's value, Audience got State's value — RedirectUri was dropped
 * entirely, since this class hierarchy has no property to store it in). Nothing in the
 * codebase called this constructor, so the bug was never exercised until now.
 */

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
                audience: "https://api.example.com",
                state: "explicit-state");

            Assert.Equal("client-id", config.ClientId);
            Assert.Equal("openid profile", config.Scope);
            Assert.Equal("client-secret", config.ClientSecret);
            Assert.Equal("https://api.example.com", config.Audience);
            Assert.Equal("explicit-state", config.State);
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
            // BaseAuthorizationConfiguration generates a random state when none is supplied.
            Assert.False(string.IsNullOrEmpty(config.State));
        }
    }
}
