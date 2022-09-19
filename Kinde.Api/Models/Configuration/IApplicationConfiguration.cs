namespace Kinde.Api.Models.Configuration
{
    public interface IApplicationConfiguration
    {
        public string Domain { get; set; }
        public string ReplyUrl { get; set; }
        public string LogoutUrl { get; set; }
    }
}
