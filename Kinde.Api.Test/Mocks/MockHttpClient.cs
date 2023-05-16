namespace Kinde.Api.Test.Mocks
{
    public class MockHttpClient : HttpClient
    {
        public HttpResponseMessage Result { get; set; }

        public HttpRequestMessage Request { get; private set; }

        public MockHttpClient(HttpResponseMessage result) : base()
        {
            Result = result;
        }

        public override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Request = request;
            return Result;
        }

        public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Request = request;
            return Task.FromResult(Result);
        }
    }
}
