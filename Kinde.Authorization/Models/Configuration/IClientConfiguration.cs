using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinde.Authorization.Models.Configuration
{
    public interface IClientConfiguration
    {
        public string Domain { get; set; }
        public string ReplyUrl { get; set; }
    }
}
