namespace Kinde.Authorization.Flows
{
    public interface ICodeFlow
    {
        public void OnCodeRecieved(HttpClient client, string state, string code);
    }
}
