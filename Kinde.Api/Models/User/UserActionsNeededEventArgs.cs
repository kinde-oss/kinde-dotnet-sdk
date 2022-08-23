namespace Kinde.Api.Models.User
{
    public class UserActionsNeededEventArgs : EventArgs
    {
        public string RedirectUrl { get; set; }
        public string State { get; set; }
    }
}
