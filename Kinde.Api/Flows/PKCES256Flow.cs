using Kinde.Api.Enums;
using Kinde.Api.Hashing;
using Kinde.Api.Models.Configuration;
using Kinde.Api.Models.User;

namespace Kinde.Api.Flows
{
    public class PKCESFlow : BaseAuthorizationFlow<PKCEConfiguration<SHA256CodeVerifier>>, IAuthorizationFlow
    {
        public PKCESFlow(IApplicationConfiguration identityProviderConfiguration, PKCEConfiguration<SHA256CodeVerifier> configuration) : base(identityProviderConfiguration, configuration)
        {
            UserActionsResolver = new PKCEUserActionResolver<SHA256CodeVerifier>(identityProviderConfiguration.ReplyUrl, configuration.State);
        }

        public override IUserActionResolver UserActionsResolver { get; init; }

        public override async Task<AuthotizationStates> Authorize(HttpClient httpClient, bool register = false)
        {
            var parameters = CreateBaseRequestParameters(register);
            parameters.Add("response_type", "code");
            parameters.Add("grant_type", "authorization_code");
            parameters.Add("state", Configuration.State);
            parameters.Add("code_challenge", await Configuration.CodeVerifier.Compute(Configuration.State));
            parameters.Add("code_challenge_method", "S256");
            return await base.SendRequest(httpClient, parameters);
        }


        public override void OnCodeRecieved(HttpClient httpClient, string state, string code)
        {

            var parameters = new Dictionary<string, string>();

            parameters.Add("grant_type", "authorization_code");
            parameters.Add("client_id", Configuration.ClientId);
            parameters.Add("client_secret", Configuration.ClientSecret);
            parameters.Add("scope", Configuration.Scope);
            parameters.Add("code", code);
            parameters.Add("code_verifier", state);
            parameters.Add("redirect_uri", IdentityProviderConfiguration.ReplyUrl);

            SendCode(httpClient, parameters);



        }
    }
}
