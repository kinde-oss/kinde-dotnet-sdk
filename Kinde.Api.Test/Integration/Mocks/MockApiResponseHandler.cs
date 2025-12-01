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
        private readonly Dictionary<string, (string Content, HttpStatusCode StatusCode)> _responses = new Dictionary<string, (string, HttpStatusCode)>();

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
            _responses[key] = (json, statusCode);
        }

        /// <summary>
        /// Add a mock response with raw JSON
        /// </summary>
        public void AddResponse(string method, string path, string jsonContent, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var key = $"{method}:{path}";
            _responses[key] = (jsonContent, statusCode);
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var method = request.Method.Method;
            var path = request.RequestUri?.PathAndQuery ?? "";

            // Try exact match first
            var key = $"{method}:{path}";
            if (_responses.TryGetValue(key, out var response))
            {
                return Task.FromResult(new HttpResponseMessage(response.StatusCode)
                {
                    Content = new StringContent(response.Content, System.Text.Encoding.UTF8, "application/json")
                });
            }

            // Try path-only match
            key = $"{method}:{request.RequestUri?.AbsolutePath ?? ""}";
            if (_responses.TryGetValue(key, out response))
            {
                return Task.FromResult(new HttpResponseMessage(response.StatusCode)
                {
                    Content = new StringContent(response.Content, System.Text.Encoding.UTF8, "application/json")
                });
            }

            // Return 404 if no match found
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent($"{{\"error\":\"No mock response found for {method} {path}\"}}", System.Text.Encoding.UTF8, "application/json")
            });
        }
    }
}

