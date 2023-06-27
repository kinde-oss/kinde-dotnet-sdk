namespace Kinde.Api.Client
{
    public partial class ApiClient
    {
        protected HttpClient HttpClient { get => _httpClient; }

        partial void InterceptRequest(HttpRequestMessage req)
        {
            AuthorizeRequest(req, HttpClient);
        }

        protected virtual void AuthorizeRequest(HttpRequestMessage request, HttpClient httpClient)
        {
        }
    }
}