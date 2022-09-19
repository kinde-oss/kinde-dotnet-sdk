using Kinde.Api.Enums;
using Kinde.Api.Models.Tokens;
using Kinde.Api.Models.User;

namespace Kinde.Api.Flows
{
    public interface IAuthorizationFlow
    {
        public bool RequiresRedirection { get; }
        AuthotizationStates AuthotizationState { get; set; }
        IUserActionResolver UserActionsResolver { get; init; }
        OauthToken Token { get; }
        Task<AuthotizationStates> Authorize(HttpClient httpClient, bool register = false);
        Task Logout(HttpClient httpClient);
        Task Renew(HttpClient httpClient);

        void AuthorizeRequest(HttpRequestMessage httpRequestMessage);
        Task<object> GetUserProfile(HttpClient httpClient);
    }
}
