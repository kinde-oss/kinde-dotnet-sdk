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
    ///  Class for testing RolesApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class RolesApiTests : IDisposable
    {
        private RolesApi instance;

        public RolesApiTests()
        {
            instance = new RolesApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of RolesApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' RolesApi
            //Assert.IsType<RolesApi>(instance);
        }

        /// <summary>
        /// Test AddRoleScope
        /// </summary>
        [Fact]
        public void AddRoleScopeTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string roleId = null;
            //AddRoleScopeRequest? addRoleScopeRequest = null;
            //var response = instance.AddRoleScope(roleId, addRoleScopeRequest);
            //Assert.IsType<AddRoleScopeResponse>(response);
        }

        /// <summary>
        /// Test CreateRole
        /// </summary>
        [Fact]
        public void CreateRoleTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //CreateRoleRequest? createRoleRequest = null;
            //var response = instance.CreateRole(createRoleRequest);
            //Assert.IsType<CreateRolesResponse>(response);
        }

        /// <summary>
        /// Test DeleteRole
        /// </summary>
        [Fact]
        public void DeleteRoleTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string roleId = null;
            //var response = instance.DeleteRole(roleId);
            //Assert.IsType<SuccessResponse>(response);
        }

        /// <summary>
        /// Test DeleteRoleScope
        /// </summary>
        [Fact]
        public void DeleteRoleScopeTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string roleId = null;
            //string scopeId = null;
            //var response = instance.DeleteRoleScope(roleId, scopeId);
            //Assert.IsType<DeleteRoleScopeResponse>(response);
        }

        /// <summary>
        /// Test GetRole
        /// </summary>
        [Fact]
        public void GetRoleTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string roleId = null;
            //var response = instance.GetRole(roleId);
            //Assert.IsType<GetRoleResponse>(response);
        }

        /// <summary>
        /// Test GetRolePermissions
        /// </summary>
        [Fact]
        public void GetRolePermissionsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string roleId = null;
            //string? sort = null;
            //int? pageSize = null;
            //string? nextToken = null;
            //var response = instance.GetRolePermissions(roleId, sort, pageSize, nextToken);
            //Assert.IsType<RolePermissionsResponse>(response);
        }

        /// <summary>
        /// Test GetRoleScopes
        /// </summary>
        [Fact]
        public void GetRoleScopesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string roleId = null;
            //var response = instance.GetRoleScopes(roleId);
            //Assert.IsType<RoleScopesResponse>(response);
        }

        /// <summary>
        /// Test GetRoles
        /// </summary>
        [Fact]
        public void GetRolesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string? sort = null;
            //int? pageSize = null;
            //string? nextToken = null;
            //var response = instance.GetRoles(sort, pageSize, nextToken);
            //Assert.IsType<GetRolesResponse>(response);
        }

        /// <summary>
        /// Test RemoveRolePermission
        /// </summary>
        [Fact]
        public void RemoveRolePermissionTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string roleId = null;
            //string permissionId = null;
            //var response = instance.RemoveRolePermission(roleId, permissionId);
            //Assert.IsType<SuccessResponse>(response);
        }

        /// <summary>
        /// Test UpdateRolePermissions
        /// </summary>
        [Fact]
        public void UpdateRolePermissionsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string roleId = null;
            //UpdateRolePermissionsRequest updateRolePermissionsRequest = null;
            //var response = instance.UpdateRolePermissions(roleId, updateRolePermissionsRequest);
            //Assert.IsType<UpdateRolePermissionsResponse>(response);
        }

        /// <summary>
        /// Test UpdateRoles
        /// </summary>
        [Fact]
        public void UpdateRolesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string roleId = null;
            //UpdateRolesRequest? updateRolesRequest = null;
            //var response = instance.UpdateRoles(roleId, updateRolesRequest);
            //Assert.IsType<SuccessResponse>(response);
        }
    }
}
