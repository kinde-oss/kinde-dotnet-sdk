/*
 * Kinde Management API
 *
 * Provides endpoints to manage your Kinde Businesses
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
    ///  Class for testing WebhooksApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class WebhooksApiTests : IDisposable
    {
        private WebhooksApi instance;

        public WebhooksApiTests()
        {
            instance = new WebhooksApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of WebhooksApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' WebhooksApi
            //Assert.IsType<WebhooksApi>(instance);
        }

        /// <summary>
        /// Test CreateWebHook
        /// </summary>
        [Fact]
        public void CreateWebHookTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //CreateWebHookRequest createWebHookRequest = null;
            //var response = instance.CreateWebHook(createWebHookRequest);
            //Assert.IsType<CreateWebhookResponse>(response);
        }

        /// <summary>
        /// Test DeleteWebHook
        /// </summary>
        [Fact]
        public void DeleteWebHookTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string webhookId = null;
            //var response = instance.DeleteWebHook(webhookId);
            //Assert.IsType<DeleteWebhookResponse>(response);
        }

        /// <summary>
        /// Test GetEvent
        /// </summary>
        [Fact]
        public void GetEventTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string eventId = null;
            //var response = instance.GetEvent(eventId);
            //Assert.IsType<GetEventResponse>(response);
        }

        /// <summary>
        /// Test GetEventTypes
        /// </summary>
        [Fact]
        public void GetEventTypesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //var response = instance.GetEventTypes();
            //Assert.IsType<GetEventTypesResponse>(response);
        }

        /// <summary>
        /// Test GetWebHooks
        /// </summary>
        [Fact]
        public void GetWebHooksTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //var response = instance.GetWebHooks();
            //Assert.IsType<GetWebhooksResponse>(response);
        }

        /// <summary>
        /// Test UpdateWebHook
        /// </summary>
        [Fact]
        public void UpdateWebHookTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //UpdateWebHookRequest updateWebHookRequest = null;
            //var response = instance.UpdateWebHook(updateWebHookRequest);
            //Assert.IsType<UpdateWebhookResponse>(response);
        }
    }
}
