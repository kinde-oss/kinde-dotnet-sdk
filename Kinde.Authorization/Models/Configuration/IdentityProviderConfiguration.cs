using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinde.Authorization.Models.Configuration
{
    public class IdentityProviderConfiguration : IIdentityProviderConfiguration
    {
        public string Domain { get; set; }
        public string ReplyUrl { get; set; }

        public IdentityProviderConfiguration(string domain, string replyUrl)
        {
            Domain = domain;
            ReplyUrl = replyUrl;
        }
    }
}
