namespace Kinde.Api.Models.User
{
    public interface IUserActionResolver
    {
        public event EventHandler<EventArgs> UserActionsCompleted;
        public event EventHandler<EventArgs> UserActionsNeeded;
        public Task<string> GetLoginUrl(string state);
        public Task<string> GetCode(string state);
        public Task SetCode(string code, string state);

        public Task SetLoginUrl(string url, string state);
    }
}
