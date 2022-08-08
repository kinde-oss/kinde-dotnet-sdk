using Kinde.Authorization.Enums;
using Kinde.Authorization.Models.Tokens;
using Kinde.Authorization.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinde.Authorization.Flows
{
    public interface IAuthorizationFlow
    {
        public bool RequiresRedirection { get; }
        AuthotizationStates AuthotizationState { get; set; }
        IUserActionResolver UserActionsResolver { get; init; } 
        OauthToken Token { get; }
        Task<AuthotizationStates> Authorize(HttpClient httpClient);
        Task Logout(HttpClient httpClient);
        Task Renew(HttpClient httpClient);
        Task AuthorizeRequest(HttpRequestMessage httpRequestMessage);
    }
}
