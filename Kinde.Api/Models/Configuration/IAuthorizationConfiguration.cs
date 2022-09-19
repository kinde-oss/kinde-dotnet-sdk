using Kinde.Api.Flows;

namespace Kinde.Api.Models.Configuration
{
    public interface IAuthorizationConfiguration
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }
        public string GrantType { get; set; }

        public IAuthorizationFlow CreateAuthorizationFlow(IApplicationConfiguration clientConfiguration);
    }
}
