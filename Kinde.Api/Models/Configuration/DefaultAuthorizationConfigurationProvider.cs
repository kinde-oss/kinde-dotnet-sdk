using Kinde.Api.Hashing;
using Microsoft.Extensions.Configuration;


namespace Kinde.Api.Models.Configuration
{
    public class DefaultAuthorizationConfigurationProvider : IAuthorizationConfigurationProvider
    {
        protected IConfiguration _configurationSource;
        public DefaultAuthorizationConfigurationProvider(IConfiguration configurationSource)
        {
            _configurationSource =  configurationSource;
        }



        public virtual IAuthorizationConfiguration Get()
        {
            return Get("DefaultAuthorizationConfiguration");
        }

        public virtual IAuthorizationConfiguration Get(object identifier)
        {
            var section = _configurationSource.GetRequiredSection(identifier.ToString());
            var config = section.Get<AuthorizationConfigurationWrapper>();
            var type = Type.GetType(config.ConfigurationType);
            var _authorizationConfiguration = (IAuthorizationConfiguration)section.GetSection("Configuration").Get(type);
            return _authorizationConfiguration;
        }
    }
}
