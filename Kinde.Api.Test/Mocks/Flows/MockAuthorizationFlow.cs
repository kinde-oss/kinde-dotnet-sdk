using Kinde.Api.Enums;
using Kinde.Api.Flows;
using Kinde.Api.Models.Configuration;
using Kinde.Api.Models.Tokens;
using Kinde.Api.Models.User;

namespace Kinde.Api.Test.Mocks.Flows
{
    internal class MockAuthorizationFlow : IAuthorizationFlow
    {
        public KindeSSOUser User { get; }
        public bool RequiresRedirection => false;
        public AuthorizationStates AuthorizationState { get; set; } = AuthorizationStates.Authorized;
        public IUserActionResolver UserActionsResolver { get; init; } = new DefaultUserActionResolver();
        public OauthToken Token { get; }

        public MockAuthorizationFlow(OauthToken token)
        {
            Token = token;
            User = KindeSSOUser.FromToken(token);
        }

        public Task<AuthorizationStates> Authorize(HttpClient httpClient, bool register = false)
        {
            return Task.FromResult(AuthorizationState);
        }

        public Task Logout(HttpClient httpClient)
        {
            return Task.CompletedTask;
        }

        public Task Renew(HttpClient httpClient)
        {
            return Task.CompletedTask;
        }

        public void AuthorizeRequest(HttpRequestMessage httpRequestMessage, HttpClient httpClient)
        {
            // Mock implementation - add authorization header
            httpRequestMessage.Headers.TryAddWithoutValidation("cache-control", "no-cache");
            httpRequestMessage.Headers.TryAddWithoutValidation("Authorization", "Bearer " + Token.AccessToken);
        }

        public Task<string> GetOrRefreshToken(HttpClient httpClient)
        {
            return Task.FromResult(Token.AccessToken);
        }
    }
} 