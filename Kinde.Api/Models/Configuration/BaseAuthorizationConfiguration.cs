using Kinde.Api.Flows;

namespace Kinde.Api.Models.Configuration
{
    public abstract class BaseAuthorizationConfiguration : IAuthorizationConfiguration
    {
        public BaseAuthorizationConfiguration()
        {

        }
        public BaseAuthorizationConfiguration(string clientId, string clientSecret, string grantType, string scope)
        {
            ClientId = clientId;
            GrantType = grantType;
            Scope = scope;
            ClientSecret = clientSecret; 
        }
        public bool IsCreateOrganisation { get; set; }
        public string OrganisationId { get; set; }
        public string ClientId { get; set; }
        public string Audience { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }
        public string GrantType { get; set; }

        public abstract IAuthorizationFlow CreateAuthorizationFlow(IApplicationConfiguration clientConfiguration);

        protected bool IsStateValid(string state)
        {
            return !string.IsNullOrEmpty(state) && state.Length > 10;
        }

    }
}
