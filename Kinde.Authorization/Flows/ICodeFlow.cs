using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinde.Authorization.Flows
{
    public interface ICodeFlow
    {
        public Task OnCodeRecieved( string state,string code);
    }
}
