#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Kinde.Accounts.Client
{
    /// <summary>
    /// Provides a non-generic contract for the ApiResponse wrapper.
    /// </summary>
    public interface IApiResponse
    {
        /// <summary>
        /// The type that represents the server's response.
        /// </summary>
        Type ResponseType { get; }

        /// <summary>
        /// Gets or sets the status code (HTTP status code)
        /// </summary>
        /// <value>The status code.</value>
        HttpStatusCode StatusCode { get; }

        /// <summary>
        /// The raw content of this response.
        /// </summary>
        string RawContent { get; }

        /// <summary>
        /// The DateTime when the request was retrieved.
        /// </summary>
        DateTime DownloadedAt { get; }

        /// <summary>
        /// The path used when making the request.
        /// </summary>
        string Path { get; }

        /// <summary>
        /// The Uri used when making the request.
        /// </summary>
        Uri? RequestUri { get; }
    }

    /// <summary>
    /// API Response
    /// </summary>
    public partial class ApiResponse<T> : IApiResponse
    {
        /// <summary>
        /// Gets or sets the status code (HTTP status code)
        /// </summary>
        /// <value>The status code.</value>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// The type that represents the server's response.
        /// </summary>
        public Type ResponseType
        {
            get { return typeof(T); }
        }

        /// <summary>
        /// The raw data
        /// </summary>
        public string RawContent { get; private set; }

        /// <summary>
        /// The IsSuccessStatusCode from the api response
        /// </summary>
        public bool IsSuccessStatusCode { get; }

        /// <summary>
        /// The reason phrase contained in the api response
        /// </summary>
        public string? ReasonPhrase { get; }

        /// <summary>
        /// The headers contained in the api response
        /// </summary>
        public System.Net.Http.Headers.HttpResponseHeaders Headers { get; }

        /// <summary>
        /// The DateTime when the request was retrieved.
        /// </summary>
        public DateTime DownloadedAt { get; } = DateTime.UtcNow;

        /// <summary>
        /// The DateTime when the request was sent.
        /// </summary>
        public DateTime RequestedAt { get; }

        /// <summary>
        /// The path used when making the request.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// The Uri used when making the request.
        /// </summary>
        public Uri? RequestUri { get; }

        /// <summary>
        /// The JsonSerialzierOptions
        /// </summary>
        private System.Text.Json.JsonSerializerOptions _jsonSerializerOptions;

        /// <summary>
        /// Construct the response using an HttpResponseMessage
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <param name="httpResponseMessage"></param>
        /// <param name="rawContent"></param>
        /// <param name="path"></param>
        /// <param name="requestedAt"></param>
        /// <param name="jsonSerializerOptions"></param>
        public ApiResponse(System.Net.Http.HttpRequestMessage httpRequestMessage, System.Net.Http.HttpResponseMessage httpResponseMessage, string rawContent, string path, DateTime requestedAt, System.Text.Json.JsonSerializerOptions jsonSerializerOptions)
        {
            StatusCode = httpResponseMessage.StatusCode;
            Headers = httpResponseMessage.Headers;
            IsSuccessStatusCode = httpResponseMessage.IsSuccessStatusCode;
            ReasonPhrase = httpResponseMessage.ReasonPhrase;
            RawContent = rawContent;
            Path = path;
            RequestUri = httpRequestMessage.RequestUri;
            RequestedAt = requestedAt;
            _jsonSerializerOptions = jsonSerializerOptions;
            OnCreated(httpRequestMessage, httpResponseMessage);
        }

        partial void OnCreated(System.Net.Http.HttpRequestMessage httpRequestMessage, System.Net.Http.HttpResponseMessage httpResponseMessage);

        /// <summary>
        /// Construct the response using pre-mapped data (for Kiota integration)
        /// </summary>
        /// <param name="path">The path used when making the request</param>
        /// <param name="data">The already-mapped response data</param>
        /// <param name="statusCode">The HTTP status code</param>
        /// <param name="requestedAt">When the request was made</param>
        public ApiResponse(string path, T data, HttpStatusCode statusCode, DateTime requestedAt)
        {
            StatusCode = statusCode;
            Headers = new System.Net.Http.HttpResponseMessage().Headers;
            IsSuccessStatusCode = (int)statusCode >= 200 && (int)statusCode < 300;
            ReasonPhrase = statusCode.ToString();
            RawContent = System.Text.Json.JsonSerializer.Serialize(data);
            Path = path;
            RequestUri = null;
            RequestedAt = requestedAt;
            _jsonSerializerOptions = new System.Text.Json.JsonSerializerOptions();
            _preMapppedData = data;
        }

        private T? _preMapppedData;

        /// <summary>
        /// Deserializes the server's response
        /// </summary>
        public T? AsModel()
        {
            // Return pre-mapped data if available (from Kiota integration)
            if (_preMapppedData != null)
                return _preMapppedData;

            // This logic may be modified with the AsModel.mustache template
            return IsSuccessStatusCode
                ? System.Text.Json.JsonSerializer.Deserialize<T>(RawContent, _jsonSerializerOptions)
                : default(T);
        }

        /// <summary>
        /// Returns true when the model can be deserialized
        /// </summary>
        public bool TryToModel(out T? model)
        {
            try
            {
                model = AsModel();
                return model != null;
            }
            catch
            {
                model = default(T);
                return false;
            }
        }
    }
}
