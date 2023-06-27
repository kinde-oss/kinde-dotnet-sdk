namespace Kinde.Api.Flows
{
    public interface ICodeFlow
    {
        public void OnCodeReceived(HttpClient client, string state, string code);
    }
}
