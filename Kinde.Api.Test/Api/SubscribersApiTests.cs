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
    ///  Class for testing SubscribersApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class SubscribersApiTests : IDisposable
    {
        private SubscribersApi instance;

        public SubscribersApiTests()
        {
            instance = new SubscribersApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of SubscribersApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' SubscribersApi
            //Assert.IsType<SubscribersApi>(instance);
        }

        /// <summary>
        /// Test CreateSubscriber
        /// </summary>
        [Fact]
        public void CreateSubscriberTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string firstName = null;
            //string lastName = null;
            //string email = null;
            //var response = instance.CreateSubscriber(firstName, lastName, email);
            //Assert.IsType<CreateSubscriberSuccessResponse>(response);
        }

        /// <summary>
        /// Test GetSubscriber
        /// </summary>
        [Fact]
        public void GetSubscriberTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string subscriberId = null;
            //var response = instance.GetSubscriber(subscriberId);
            //Assert.IsType<GetSubscriberResponse>(response);
        }

        /// <summary>
        /// Test GetSubscribers
        /// </summary>
        [Fact]
        public void GetSubscribersTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string sort = null;
            //int? pageSize = null;
            //string nextToken = null;
            //var response = instance.GetSubscribers(sort, pageSize, nextToken);
            //Assert.IsType<GetSubscribersResponse>(response);
        }
    }
}
