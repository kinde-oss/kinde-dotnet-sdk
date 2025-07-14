using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kinde.Api.Test.Mocks
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private HttpResponseMessage _response;
        public HttpRequestMessage Request { get; private set; }

        public HttpResponseMessage Result
        {
            get => _response;
            set => _response = value;
        }

        public MockHttpMessageHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Request = request;
            return Task.FromResult(_response);
        }
    }
}
