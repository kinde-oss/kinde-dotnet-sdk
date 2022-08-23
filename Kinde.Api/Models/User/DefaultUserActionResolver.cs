namespace Kinde.Api.Models.User
{
    public class DefaultUserActionResolver : IUserActionResolver
    {
        public virtual event EventHandler<EventArgs> UserActionsNeeded;
        public virtual event EventHandler<EventArgs> UserActionsCompleted;
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
        protected void OnUserActionsCompleted(string code, string state)
        {
            UserActionsCompleted?.Invoke(this, new UserActionsCompletedEventArgs() { Code = code, State = state });
        }

        protected void OnUserActionsNeeded(string url, string state)
        {
            UserActionsNeeded?.Invoke(this, new UserActionsNeededEventArgs() { RedirectUrl = url, State = state });
        }
    }
}
