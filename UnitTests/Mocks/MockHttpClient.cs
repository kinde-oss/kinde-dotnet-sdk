using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Mocks
{
    internal class MockHttpClient:HttpClient
    {
        protected HttpResponseMessage Result;
        public MockHttpClient(HttpResponseMessage result):base() 
        {
            Result = result;

        }

        public override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Result;
        }
        public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Result);
        }
    }
}
