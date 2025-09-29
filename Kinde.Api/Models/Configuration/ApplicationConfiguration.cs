namespace Kinde.Api.Models.Configuration
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        public ApplicationConfiguration()
        {
        }

        public string Domain { get; set; }

        public string ReplyUrl { get; set; }

        public string LogoutUrl { get; set; }

        public bool ForceApi { get; set; } = false;

        public ApplicationConfiguration(string domain, string replyUrl, string logoutUrl, bool forceApi = false)
        {
            Domain = domain;
            ReplyUrl = replyUrl;
            LogoutUrl = logoutUrl;
            ForceApi = forceApi;
        }
    }
}
