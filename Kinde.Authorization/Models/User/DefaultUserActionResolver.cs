using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinde.Authorization.Models.User
{
    public class DefaultUserActionResolver : IUserActionResolver
    {
        public event EventHandler<EventArgs> OnUserActionsNeeded;
        public event EventHandler<EventArgs> OnUserActionsCompleted;

      
        public Task<string> GetCode(string state)
        {
            return null;
        }

        public async virtual Task<string> GetLoginUrl(string state)
        {
            return null;
        }

        public async virtual Task SetCode(string code, string state)
        {

        }

        public async virtual Task SetLoginUrl(string url, string state)
        {

        }
    }
}
