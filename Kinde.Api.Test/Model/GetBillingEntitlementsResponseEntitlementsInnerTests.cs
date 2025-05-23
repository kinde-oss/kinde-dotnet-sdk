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
    ///  Class for testing GetBillingEntitlementsResponseEntitlementsInner
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the model.
    /// </remarks>
    public class GetBillingEntitlementsResponseEntitlementsInnerTests : IDisposable
    {
        // TODO uncomment below to declare an instance variable for GetBillingEntitlementsResponseEntitlementsInner
        //private GetBillingEntitlementsResponseEntitlementsInner instance;

        public GetBillingEntitlementsResponseEntitlementsInnerTests()
        {
            // TODO uncomment below to create an instance of GetBillingEntitlementsResponseEntitlementsInner
            //instance = new GetBillingEntitlementsResponseEntitlementsInner();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of GetBillingEntitlementsResponseEntitlementsInner
        /// </summary>
        [Fact]
        public void GetBillingEntitlementsResponseEntitlementsInnerInstanceTest()
        {
            // TODO uncomment below to test "IsType" GetBillingEntitlementsResponseEntitlementsInner
            //Assert.IsType<GetBillingEntitlementsResponseEntitlementsInner>(instance);
        }

        /// <summary>
        /// Test the property 'Id'
        /// </summary>
        [Fact]
        public void IdTest()
        {
            // TODO unit test for the property 'Id'
        }

        /// <summary>
        /// Test the property 'FixedCharge'
        /// </summary>
        [Fact]
        public void FixedChargeTest()
        {
            // TODO unit test for the property 'FixedCharge'
        }

        /// <summary>
        /// Test the property 'PriceName'
        /// </summary>
        [Fact]
        public void PriceNameTest()
        {
            // TODO unit test for the property 'PriceName'
        }

        /// <summary>
        /// Test the property 'UnitAmount'
        /// </summary>
        [Fact]
        public void UnitAmountTest()
        {
            // TODO unit test for the property 'UnitAmount'
        }

        /// <summary>
        /// Test the property 'FeatureCode'
        /// </summary>
        [Fact]
        public void FeatureCodeTest()
        {
            // TODO unit test for the property 'FeatureCode'
        }

        /// <summary>
        /// Test the property 'FeatureName'
        /// </summary>
        [Fact]
        public void FeatureNameTest()
        {
            // TODO unit test for the property 'FeatureName'
        }

        /// <summary>
        /// Test the property 'EntitlementLimitMax'
        /// </summary>
        [Fact]
        public void EntitlementLimitMaxTest()
        {
            // TODO unit test for the property 'EntitlementLimitMax'
        }

        /// <summary>
        /// Test the property 'EntitlementLimitMin'
        /// </summary>
        [Fact]
        public void EntitlementLimitMinTest()
        {
            // TODO unit test for the property 'EntitlementLimitMin'
        }
    }
}
