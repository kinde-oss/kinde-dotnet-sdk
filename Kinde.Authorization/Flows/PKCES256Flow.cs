using Kinde.Authorization.Enums;
using Kinde.Authorization.Hashing;
using Kinde.Authorization.Models.Configuration;
using Kinde.Authorization.Models.User;

namespace Kinde.Authorization.Flows
{
    public class PKCESFlow : BaseAuthorizationFlow<PKCEConfiguration<SHA256CodeVerifier>>, IAuthorizationFlow
    {
        public PKCESFlow(IIdentityProviderConfiguration identityProviderConfiguration, PKCEConfiguration<SHA256CodeVerifier> configuration) : base(identityProviderConfiguration, configuration)
        {
        }

        public override IUserActionResolver UserActionsResolver { get; init; } = new PKCEUserActionResolver<SHA256CodeVerifier>();

        public override async Task<AuthotizationStates> Authorize(HttpClient httpClient)
        {
            var parameters = CreateBaseRequestParameters();
            parameters.Add("response_type", "code");
            parameters.Add("grant_type", "authorization_code");
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
