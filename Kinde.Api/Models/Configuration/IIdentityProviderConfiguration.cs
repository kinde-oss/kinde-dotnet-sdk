namespace Kinde.Api.Models.Configuration
{
    public interface IIdentityProviderConfiguration
    {
        public string Domain { get; set; }
        public string ReplyUrl { get; set; }
    }
}
