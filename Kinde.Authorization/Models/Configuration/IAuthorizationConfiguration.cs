using Kinde.Authorization.Flows;

namespace Kinde.Authorization.Models.Configuration
{
    public interface IAuthorizationConfiguration
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }
        public string GrantType { get; set; }

        public IAuthorizationFlow CreateAuthorizationFlow(IIdentityProviderConfiguration clientConfiguration);
    }
}
