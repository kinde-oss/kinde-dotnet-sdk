using Microsoft.Extensions.Configuration;

namespace Kinde.Api.Models.Configuration
{
    public class DefaultApplicationConfigurationProvider : IApplicationConfigurationProvider
    {
        protected IConfiguration _configurationSource;

        public DefaultApplicationConfigurationProvider(IConfiguration configurationSource)
        {
            _configurationSource = configurationSource;
        }

        public IApplicationConfiguration Get()
        {
            return Get("ApplicationConfiguration");
        }

        public IApplicationConfiguration Get(object identifier)
        {
            return _configurationSource.GetRequiredSection(identifier.ToString()).Get<ApplicationConfiguration>();
        }
    }
}
