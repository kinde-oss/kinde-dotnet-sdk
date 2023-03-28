using Kinde.Api.Enums;
using Kinde.Api.Models.Tokens;
using Kinde.Api.Models.User;

namespace Kinde.Api.Flows
{
    public interface IAuthorizationFlow
    {

        public KindeSSOUser User { get; }
        public bool RequiresRedirection { get; }
        AuthorizationStates AuthotizationState { get; set; }
        IUserActionResolver UserActionsResolver { get; init; }
        OauthToken Token { get; }
        Task<AuthorizationStates> Authorize(HttpClient httpClient, bool register = false);
        Task Logout(HttpClient httpClient);
        Task Renew(HttpClient httpClient);

        void AuthorizeRequest(HttpRequestMessage httpRequestMessage);
        Task<object> GetUserProfile(HttpClient httpClient);
    }
}
