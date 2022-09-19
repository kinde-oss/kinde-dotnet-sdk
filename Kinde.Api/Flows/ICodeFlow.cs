namespace Kinde.Api.Flows
{
    public interface ICodeFlow
    {
        public void OnCodeRecieved(HttpClient client, string state, string code);

    }
}
