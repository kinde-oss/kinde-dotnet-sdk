using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kinde.Api.Test.Mocks
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private HttpResponseMessage _response;
        public HttpRequestMessage Request { get; private set; }

        /// <summary>
        /// The captured request body, read eagerly during SendAsync. Callers (e.g. Kiota's
        /// HttpClientRequestAdapter) may dispose the request's content stream once the send
        /// completes, so Request.Content can no longer be read after SendAsync returns.
        /// </summary>
        public string RequestBody { get; private set; }

        public HttpResponseMessage Result
        {
            get => _response;
            set => _response = value;
        }

        public MockHttpMessageHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Request = request;
            if (request.Content != null)
            {
                RequestBody = await request.Content.ReadAsStringAsync();
            }
            return _response;
        }
    }
}
