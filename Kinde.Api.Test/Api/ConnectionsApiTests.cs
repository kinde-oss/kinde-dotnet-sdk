/*
 * Kinde Management API
 *
 *  Provides endpoints to manage your Kinde Businesses.  ## Intro  ## How to use  1. [Set up and authorize a machine-to-machine (M2M) application](https://docs.kinde.com/developer-tools/kinde-api/connect-to-kinde-api/).  2. [Generate a test access token](https://docs.kinde.com/developer-tools/kinde-api/access-token-for-api/)  3. Test request any endpoint using the test token 
 *
 * The version of the OpenAPI document: 1
 * Contact: support@kinde.com
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Xunit;

using Kinde.Api.Client;
using Kinde.Api.Api;
// uncomment below to import models
//using Kinde.Api.Model;

namespace Kinde.Api.Test.Api
{
    /// <summary>
    ///  Class for testing ConnectionsApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class ConnectionsApiTests : IDisposable
    {
        private ConnectionsApi instance;

        public ConnectionsApiTests()
        {
            instance = new ConnectionsApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of ConnectionsApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' ConnectionsApi
            //Assert.IsType<ConnectionsApi>(instance);
        }

        /// <summary>
        /// Test CreateConnection
        /// </summary>
        [Fact]
        public void CreateConnectionTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //CreateConnectionRequest createConnectionRequest = null;
            //var response = instance.CreateConnection(createConnectionRequest);
            //Assert.IsType<CreateConnectionResponse>(response);
        }

        /// <summary>
        /// Test DeleteConnection
        /// </summary>
        [Fact]
        public void DeleteConnectionTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string connectionId = null;
            //var response = instance.DeleteConnection(connectionId);
            //Assert.IsType<SuccessResponse>(response);
        }

        /// <summary>
        /// Test GetConnection
        /// </summary>
        [Fact]
        public void GetConnectionTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string connectionId = null;
            //var response = instance.GetConnection(connectionId);
            //Assert.IsType<Connection>(response);
        }

        /// <summary>
        /// Test GetConnections
        /// </summary>
        [Fact]
        public void GetConnectionsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //int? pageSize = null;
            //string? homeRealmDomain = null;
            //string? startingAfter = null;
            //string? endingBefore = null;
            //var response = instance.GetConnections(pageSize, homeRealmDomain, startingAfter, endingBefore);
            //Assert.IsType<GetConnectionsResponse>(response);
        }

        /// <summary>
        /// Test ReplaceConnection
        /// </summary>
        [Fact]
        public void ReplaceConnectionTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string connectionId = null;
            //ReplaceConnectionRequest replaceConnectionRequest = null;
            //var response = instance.ReplaceConnection(connectionId, replaceConnectionRequest);
            //Assert.IsType<SuccessResponse>(response);
        }

        /// <summary>
        /// Test UpdateConnection
        /// </summary>
        [Fact]
        public void UpdateConnectionTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string connectionId = null;
            //UpdateConnectionRequest updateConnectionRequest = null;
            //var response = instance.UpdateConnection(connectionId, updateConnectionRequest);
            //Assert.IsType<SuccessResponse>(response);
        }
    }
}
