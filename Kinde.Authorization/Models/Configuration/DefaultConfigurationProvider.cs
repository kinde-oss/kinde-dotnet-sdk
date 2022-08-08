using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinde.Authorization.Models.Configuration
{
    public class DefaultConfigurationProvider : IAuthorizationConfigurationProvider
    {
        public DefaultConfigurationProvider(IAuthorizationConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IAuthorizationConfiguration Configuration { get; set; }
        public IAuthorizationConfiguration Get()
        {
            return Configuration;
        }
    }
}
