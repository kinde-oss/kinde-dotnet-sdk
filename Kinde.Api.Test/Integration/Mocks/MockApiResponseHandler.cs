using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Kinde.Api.Test.Integration.Mocks
{
    /// <summary>
    /// HTTP message handler for mocking API responses
    /// </summary>
    public class MockApiResponseHandler : DelegatingHandler
    {
        private readonly Dictionary<string, HttpResponseMessage> _responses = new Dictionary<string, HttpResponseMessage>();

        public MockApiResponseHandler()
        {
        }

        /// <summary>
        /// Add a mock response for a specific request
        /// </summary>
        public void AddResponse(string method, string path, object responseData, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var key = $"{method}:{path}";
            var json = JsonConvert.SerializeObject(responseData);
            var httpResponse = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            };
            _responses[key] = httpResponse;
        }

        /// <summary>
        /// Add a mock response with raw JSON
        /// </summary>
        public void AddResponse(string method, string path, string jsonContent, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var key = $"{method}:{path}";
            var httpResponse = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json")
            };
            _responses[key] = httpResponse;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var method = request.Method.Method;
            var path = request.RequestUri?.PathAndQuery ?? "";
            
            // Try exact match first
            var key = $"{method}:{path}";
            if (_responses.ContainsKey(key))
            {
                return Task.FromResult(_responses[key]);
            }

            // Try path-only match
            key = $"{method}:{request.RequestUri?.AbsolutePath ?? ""}";
            if (_responses.ContainsKey(key))
            {
                return Task.FromResult(_responses[key]);
            }

            // Return 404 if no match found
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent($"{{\"error\":\"No mock response found for {method} {path}\"}}", System.Text.Encoding.UTF8, "application/json")
            });
        }
    }
}

