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
        public ApplicationConfiguration(string domain, string replyUrl, string logoutUrl)
        {
            Domain = domain;
            ReplyUrl = replyUrl;
            LogoutUrl = logoutUrl;
        }
    }
}
