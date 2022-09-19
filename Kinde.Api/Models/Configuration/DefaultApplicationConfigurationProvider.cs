using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return (IApplicationConfiguration)_configurationSource.GetRequiredSection(identifier.ToString()).Get<ApplicationConfiguration>();
        }
    }
}
