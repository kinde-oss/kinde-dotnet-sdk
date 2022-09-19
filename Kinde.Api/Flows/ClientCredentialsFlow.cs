using Kinde.Api.Enums;
using Kinde.Api.Models.Configuration;
using Kinde.Api.Models.User;

namespace Kinde.Api.Flows
{
    public class ClientCredentialsFlow : BaseAuthorizationFlow<ClientCredentialsConfiguration>, IAuthorizationFlow
    {
        public override bool RequiresRedirection => false;
        public override IUserActionResolver UserActionsResolver => new DefaultUserActionResolver();

        public ClientCredentialsFlow(IApplicationConfiguration identityProviderConfiguration, ClientCredentialsConfiguration configuration) : base(identityProviderConfiguration, configuration)
        {

        }
        public override async Task<AuthotizationStates> Authorize(HttpClient httpClient, bool register = false)
        {
            var parameters = CreateBaseRequestParameters(register);
            parameters.Add("grant_type", "client_credentials");
            return await SendRequest(httpClient, parameters);
        }

        public override void OnCodeRecieved(HttpClient httpClient, string key, string value)
        {
            throw new NotImplementedException("Code is not applicable for this flow");
        }
        public override Task<object> GetUserProfile(HttpClient httpClient)
        {
            throw new NotImplementedException("User profile is not applicable for this flow");
        }
    }
}
