namespace Kinde.Authorization.Models.Configuration
{
    public interface IAuthorizationConfigurationProvider
    {
        IAuthorizationConfiguration Get();
        IAuthorizationConfiguration Get(object identifier);
    }
}
