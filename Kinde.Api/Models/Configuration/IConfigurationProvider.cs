namespace Kinde.Api.Models.Configuration
{
    public interface IConfigurationProvider<T>
    {
        T Get();
        T Get(object identifier);
    }
}
