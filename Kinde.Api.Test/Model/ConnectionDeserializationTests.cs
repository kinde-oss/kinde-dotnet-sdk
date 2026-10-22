/*
 * Regression tests for GetConnections/Connection deserialization (Kinde.Sdk v2.0.3+).
 * Customer issue: GetConnections() returns correct count but all properties on
 * connection objects are null. The actual API payload can match ConnectionConnection
 * (flat: id, name, display_name, strategy) while the SDK expects Connection
 * (wrapped: code, message, connection). ConnectionConverter fixes both shapes.
 */

using System;
using Kinde.Api.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xunit;

namespace Kinde.Api.Test.Model
{
    /// <summary>
    /// Regression tests: API-shaped JSON must populate Connection.VarConnection.
    /// </summary>
    public class ConnectionDeserializationTests
    {
        /// <summary>
        /// Same JsonSerializerSettings as the API client (CustomJsonCodec in ApiClient.cs).
        /// </summary>
        private static JsonSerializerSettings CreateClientSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy
                    {
                        OverrideSpecifiedNames = false
                    }
                }
            };
        }

        /// <summary>API-shaped JSON: snake_case, nested object under key "connection".</summary>
        private const string GetConnectionsResponseJsonApiShape = @"{
  ""code"": ""200"",
  ""message"": ""OK"",
  ""has_more"": false,
  ""connections"": [
    {
      ""code"": ""200"",
      ""message"": ""OK"",
      ""connection"": {
        ""id"": ""conn_abc123"",
        ""name"": ""my-connection"",
        ""display_name"": ""My Connection"",
        ""strategy"": ""google""
      }
    }
  ]
}";

        private const string SingleConnectionJsonApiShape = @"{
  ""code"": ""200"",
  ""message"": ""OK"",
  ""connection"": {
    ""id"": ""conn_abc123"",
    ""name"": ""my-connection"",
    ""display_name"": ""My Connection"",
    ""strategy"": ""google""
  }
}";

        private const string SingleConnectionJsonParameterName = @"{
  ""code"": ""200"",
  ""message"": ""OK"",
  ""varConnection"": {
    ""id"": ""conn_abc123"",
    ""name"": ""my-connection"",
    ""display_name"": ""My Connection"",
    ""strategy"": ""google""
  }
}";

        /// <summary>Flat payload: connections array items are ConnectionConnection-shaped (no "connection" wrapper).</summary>
        private const string GetConnectionsResponseJsonFlatShape = @"{
  ""code"": ""200"",
  ""message"": ""OK"",
  ""has_more"": false,
  ""connections"": [
    {
      ""id"": ""conn_abc123"",
      ""name"": ""my-connection"",
      ""display_name"": ""My Connection"",
      ""strategy"": ""google""
    }
  ]
}";

        /// <summary>
        /// With API-shaped JSON ("connection" key), VarConnection must be populated (regression test).
        /// </summary>
        [Fact]
        public void Deserialize_GetConnectionsResponse_WithApiShape_VarConnection_IsPopulated()
        {
            var settings = CreateClientSerializerSettings();
            var response = JsonConvert.DeserializeObject<GetConnectionsResponse>(GetConnectionsResponseJsonApiShape, settings);

            Assert.NotNull(response);
            Assert.NotNull(response.Connections);
            Assert.Single(response.Connections);

            var conn = response.Connections[0];
            Assert.NotNull(conn);
            Assert.NotNull(conn.VarConnection);
            Assert.Equal("conn_abc123", conn.VarConnection.Id);
            Assert.Equal("my-connection", conn.VarConnection.Name);
            Assert.Equal("My Connection", conn.VarConnection.DisplayName);
            Assert.Equal("google", conn.VarConnection.Strategy);
        }

        /// <summary>
        /// Single Connection with API "connection" key -> VarConnection must be populated (regression test).
        /// </summary>
        [Fact]
        public void Deserialize_Connection_WithApiShape_ConnectionKey_VarConnection_IsPopulated()
        {
            var settings = CreateClientSerializerSettings();
            var connection = JsonConvert.DeserializeObject<Connection>(SingleConnectionJsonApiShape, settings);

            Assert.NotNull(connection);
            Assert.NotNull(connection.VarConnection);
            Assert.Equal("conn_abc123", connection.VarConnection.Id);
            Assert.Equal("my-connection", connection.VarConnection.Name);
            Assert.Equal("My Connection", connection.VarConnection.DisplayName);
            Assert.Equal("google", connection.VarConnection.Strategy);
        }

        /// <summary>
        /// When JSON uses "varConnection" (constructor param name), deserialization works.
        /// </summary>
        [Fact]
        public void Deserialize_Connection_WithParameterName_VarConnectionKey_VarConnection_IsPopulated()
        {
            var settings = CreateClientSerializerSettings();
            var connection = JsonConvert.DeserializeObject<Connection>(SingleConnectionJsonParameterName, settings);

            Assert.NotNull(connection);
            Assert.NotNull(connection.VarConnection);
            Assert.Equal("conn_abc123", connection.VarConnection.Id);
            Assert.Equal("my-connection", connection.VarConnection.Name);
            Assert.Equal("My Connection", connection.VarConnection.DisplayName);
            Assert.Equal("google", connection.VarConnection.Strategy);
        }

        /// <summary>
        /// When API returns flat payload (ConnectionConnection-shaped items), VarConnection must be populated (ConnectionConverter).
        /// </summary>
        [Fact]
        public void Deserialize_GetConnectionsResponse_WithFlatShape_VarConnection_IsPopulated()
        {
            var settings = CreateClientSerializerSettings();
            var response = JsonConvert.DeserializeObject<GetConnectionsResponse>(GetConnectionsResponseJsonFlatShape, settings);

            Assert.NotNull(response);
            Assert.NotNull(response.Connections);
            Assert.Single(response.Connections);

            var conn = response.Connections[0];
            Assert.NotNull(conn);
            Assert.NotNull(conn.VarConnection);
            Assert.Equal("conn_abc123", conn.VarConnection.Id);
            Assert.Equal("my-connection", conn.VarConnection.Name);
            Assert.Equal("My Connection", conn.VarConnection.DisplayName);
            Assert.Equal("google", conn.VarConnection.Strategy);
        }

        /// <summary>
        /// Top-level and count are correct; only nested "connection" object is missing.
        /// </summary>
        [Fact]
        public void Deserialize_GetConnectionsResponse_WithApiShape_CountAndTopLevelFields_Correct()
        {
            var settings = CreateClientSerializerSettings();
            var response = JsonConvert.DeserializeObject<GetConnectionsResponse>(GetConnectionsResponseJsonApiShape, settings);

            Assert.NotNull(response);
            Assert.Equal("200", response.Code);
            Assert.Equal("OK", response.Message);
            Assert.False(response.HasMore);
            Assert.NotNull(response.Connections);
            Assert.Single(response.Connections);
            Assert.Equal("200", response.Connections[0].Code);
            Assert.Equal("OK", response.Connections[0].Message);
        }
    }
}
