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
    ///  Class for testing UpdateWebHookRequest
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the model.
    /// </remarks>
    public class UpdateWebHookRequestTests : IDisposable
    {
        // TODO uncomment below to declare an instance variable for UpdateWebHookRequest
        //private UpdateWebHookRequest instance;

        public UpdateWebHookRequestTests()
        {
            // TODO uncomment below to create an instance of UpdateWebHookRequest
            //instance = new UpdateWebHookRequest();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of UpdateWebHookRequest
        /// </summary>
        [Fact]
        public void UpdateWebHookRequestInstanceTest()
        {
            // TODO uncomment below to test "IsType" UpdateWebHookRequest
            //Assert.IsType<UpdateWebHookRequest>(instance);
        }

        /// <summary>
        /// Test the property 'EventTypes'
        /// </summary>
        [Fact]
        public void EventTypesTest()
        {
            // TODO unit test for the property 'EventTypes'
        }

        /// <summary>
        /// Test the property 'Name'
        /// </summary>
        [Fact]
        public void NameTest()
        {
            // TODO unit test for the property 'Name'
        }

        /// <summary>
        /// Test the property 'Description'
        /// </summary>
        [Fact]
        public void DescriptionTest()
        {
            // TODO unit test for the property 'Description'
        }
    }
}
