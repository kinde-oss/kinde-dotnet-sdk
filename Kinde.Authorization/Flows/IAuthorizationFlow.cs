using Kinde.Authorization.Enums;
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
       
        IUserActionResolver UserActionsResolver { get; } 
        Task<AuthotizationStates> Authorize(HttpClient httpClient);
        Task Logout(HttpClient httpClient);
        Task Renew(HttpClient httpClient);
        Task AuthorizeRequest(HttpRequestMessage httpRequestMessage);
    }
}
