using Kinde.Api.Flows;

namespace Kinde.Api.Models.Configuration
{
    public interface IAuthorizationConfiguration
    {
        public string Audience { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }
        public string GrantType { get; set; }
        public bool IsCreateOrganisation { get; set; }
        public string OrganisationId { get; set; }
        public IAuthorizationFlow CreateAuthorizationFlow(IApplicationConfiguration clientConfiguration);
    }
}
