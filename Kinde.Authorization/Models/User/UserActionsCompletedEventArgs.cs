using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinde.Authorization.Models.User
{
    public class UserActionsCompletedEventArgs: EventArgs
    {
        public string Code { get; set; }
        public string State { get; set; }
    }
}
