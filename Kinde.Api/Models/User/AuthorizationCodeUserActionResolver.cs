namespace Kinde.Api.Models.User
{
    public class AuthorizationCodeUserActionResolver : DefaultUserActionResolver, IUserActionResolver
    {
        protected string _loginUrl;
        protected string _state;
        protected string _code;

        public AuthorizationCodeUserActionResolver(string loginUrl, string state)
        {
            _loginUrl = loginUrl;
            _state = state;

        }
        public virtual async Task<string> GetLoginUrl(string state)
        {
            return _loginUrl;
        }
        public virtual async Task<string> GetCode(string state)
        {
            return _code;
        }
        public virtual async Task SetCode(string code, string state)
        {
            if (_state != state) throw new ApplicationException("States are not equal");
            else _code = code;
            OnUserActionsCompleted(code, state);
        }
        public virtual async Task SetLoginUrl(string url, string state)
        {
            _loginUrl = url;
            _state = state;
            OnUserActionsNeeded(url, state);
        }
    }
}
