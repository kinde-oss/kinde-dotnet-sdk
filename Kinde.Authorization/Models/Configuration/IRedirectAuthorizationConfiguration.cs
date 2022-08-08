using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinde.Authorization.Models.Configuration
{
    public interface IRedirectAuthorizationConfiguration
    {
        public string State { get; set; }
    }
}
