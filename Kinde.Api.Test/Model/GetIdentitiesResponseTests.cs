/*
 * Kinde Management API
 *
 *  Provides endpoints to manage your Kinde Businesses.  ## Intro  ## How to use  1. [Set up and authorize a machine-to-machine (M2M) application](https://docs.kinde.com/developer-tools/kinde-api/connect-to-kinde-api/).  2. [Generate a test access token](https://docs.kinde.com/developer-tools/kinde-api/access-token-for-api/)  3. Test request any endpoint using the test token 
 *
 * The version of the OpenAPI document: 1
 * Contact: support@kinde.com
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using Xunit;

using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Kinde.Api.Model;
using Kinde.Api.Client;
using System.Reflection;
using Newtonsoft.Json;

namespace Kinde.Api.Test.Model
{
    /// <summary>
    ///  Class for testing GetIdentitiesResponse
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the model.
    /// </remarks>
    public class GetIdentitiesResponseTests : IDisposable
    {
        // TODO uncomment below to declare an instance variable for GetIdentitiesResponse
        //private GetIdentitiesResponse instance;

        public GetIdentitiesResponseTests()
        {
            // TODO uncomment below to create an instance of GetIdentitiesResponse
            //instance = new GetIdentitiesResponse();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of GetIdentitiesResponse
        /// </summary>
        [Fact]
        public void GetIdentitiesResponseInstanceTest()
        {
            // TODO uncomment below to test "IsType" GetIdentitiesResponse
            //Assert.IsType<GetIdentitiesResponse>(instance);
        }

        /// <summary>
        /// Test the property 'Code'
        /// </summary>
        [Fact]
        public void CodeTest()
        {
            // TODO unit test for the property 'Code'
        }

        /// <summary>
        /// Test the property 'Message'
        /// </summary>
        [Fact]
        public void MessageTest()
        {
            // TODO unit test for the property 'Message'
        }

        /// <summary>
        /// Test the property 'Identities'
        /// </summary>
        [Fact]
        public void IdentitiesTest()
        {
            // TODO unit test for the property 'Identities'
        }

        /// <summary>
        /// Test the property 'HasMore'
        /// </summary>
        [Fact]
        public void HasMoreTest()
        {
            // TODO unit test for the property 'HasMore'
        }
    }
}
