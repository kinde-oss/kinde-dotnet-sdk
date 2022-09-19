using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinde.Api.Models.Configuration
{
    public interface IConfigurationProvider<T>
    {
        
        T Get();

        T Get(object identifier);
    }
}
