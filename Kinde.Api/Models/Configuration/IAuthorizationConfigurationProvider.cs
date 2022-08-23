namespace Kinde.Api.Models.Configuration
{
    public interface IAuthorizationConfigurationProvider
    {
        IAuthorizationConfiguration Get();
        IAuthorizationConfiguration Get(object identifier);
    }
}
