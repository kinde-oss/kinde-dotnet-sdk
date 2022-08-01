using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinde.Authorization.Models.User
{
    public class AuthorizationCodeUserActionResolver : DefaultUserActionResolver, IUserActionResolver
    {
        protected string _loginUrl;
        protected string _state;
        protected string _code;
        public event EventHandler<UserActionsNeededEventArgs> OnUserActionsNeeded;
        public event EventHandler<UserActionsCompletedEventArgs> OnUserActionsCompleted;
        public  async Task<string> GetLoginUrl(string state)
        {
            return _loginUrl;
        }
        public  async Task<string> GetCode(string state)
        {
            return _code;
        }
        public  async Task SetCode(string code, string state)
        {
            if (_state != state) throw new ApplicationException("States are not equal");
            else _code = code;
            OnUserActionsCompleted?.Invoke(this, new UserActionsCompletedEventArgs() { Code = code, State = state});
        }
        public  async Task SetLoginUrl(string url, string state)
        {
            _loginUrl = url;
            _state = state;
            OnUserActionsNeeded?.Invoke(this, new UserActionsNeededEventArgs() { RedirectUrl = url, State =state});
        }
    }
}
