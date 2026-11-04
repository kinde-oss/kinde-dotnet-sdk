using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Kinde.Api.Mapping;
using Kinde.Api.Mappers;
using Kinde.Api.Model;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Serialization.Json;
using Xunit;
using Xunit.Abstractions;

namespace Kinde.Api.Test.Integration.Mappers
{
    /// <summary>
    /// Serializes mapped request bodies as Kiota sends them and asserts an unset property is absent from the JSON, not an explicit null.
    /// Kiota already omits nulls, so this is a backstop; the damage a stray null does to schema defaults is covered in ModelMappingTests.
    /// </summary>
    public class RequestSerializationTests
    {
        private readonly ITestOutputHelper _output;
        private readonly IKindeMapper _mapper = KindeMapperConfiguration.Mapper;

        public RequestSerializationTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void CreateUserRequest_PartiallyFilled_OmitsUnsetProperties()
        {
            var request = new CreateUserRequest(profile: new CreateUserRequestProfile(
                givenName: "Ada",
                familyName: "Lovelace"));

            var json = MapAndSerialize<global::Kinde.Api.Kiota.Management.Api.V1.User.UserPostRequestBody>(request);

            AssertNoExplicitNulls(json);
            Assert.Contains("given_name", json);
        }

        [Fact]
        public void UpdateUserRequest_OnlyOneFieldSet_SendsOnlyThatField()
        {
            var request = new UpdateUserRequest(givenName: "Grace");

            var json = MapAndSerialize<global::Kinde.Api.Kiota.Management.Api.V1.User.UserPatchRequestBody>(request);

            AssertNoExplicitNulls(json);
            Assert.Contains("given_name", json);

            // A PATCH that carried family_name: null would clear the surname on the live account.
            Assert.DoesNotContain("family_name", json);
        }

        [Fact]
        public void CreateOrganizationRequest_PartiallyFilled_OmitsUnsetProperties()
        {
            var request = new CreateOrganizationRequest(name: "Acme");

            var json = MapAndSerialize<global::Kinde.Api.Kiota.Management.Api.V1.Organization.OrganizationPostRequestBody>(request);

            AssertNoExplicitNulls(json);
            Assert.Contains("name", json);
        }

        [Fact]
        public void UpdateOrganizationRequest_PartiallyFilled_OmitsUnsetProperties()
        {
            var request = new UpdateOrganizationRequest(name: "Acme Renamed");

            var json = MapAndSerialize<global::Kinde.Api.Kiota.Management.Api.V1.Organization.Item.WithOrg_codePatchRequestBody>(request);

            AssertNoExplicitNulls(json);
            Assert.Contains("name", json);
        }

        [Fact]
        public void CreateRoleRequest_PartiallyFilled_OmitsUnsetProperties()
        {
            var request = new CreateRoleRequest(name: "Reviewer", key: "reviewer");

            var json = MapAndSerialize<global::Kinde.Api.Kiota.Management.Api.V1.Roles.RolesPostRequestBody>(request);

            AssertNoExplicitNulls(json);
            Assert.Contains("reviewer", json);
        }

        [Fact]
        public void CreatePermissionRequest_PartiallyFilled_OmitsUnsetProperties()
        {
            var request = new CreatePermissionRequest(name: "Read reports", key: "read:reports");

            var json = MapAndSerialize<global::Kinde.Api.Kiota.Management.Api.V1.Permissions.PermissionsPostRequestBody>(request);

            AssertNoExplicitNulls(json);
            Assert.Contains("read:reports", json);
        }

        [Fact]
        public void CreateApplicationRequest_PartiallyFilled_OmitsUnsetProperties()
        {
            var request = new CreateApplicationRequest(name: "Demo app");

            var json = MapAndSerialize<global::Kinde.Api.Kiota.Management.Api.V1.Applications.ApplicationsPostRequestBody>(request);

            AssertNoExplicitNulls(json);
            Assert.Contains("Demo app", json);
        }

        [Fact]
        public void CreateEnvironmentVariableRequest_PartiallyFilled_OmitsUnsetProperties()
        {
            var request = new CreateEnvironmentVariableRequest(key: "FEATURE_X", value: "on");

            var json = MapAndSerialize<global::Kinde.Api.Kiota.Management.Api.V1.Environment_variables.Environment_variablesPostRequestBody>(request);

            AssertNoExplicitNulls(json);
            Assert.Contains("FEATURE_X", json);
        }

        [Fact]
        public void UpdateBusinessRequest_PartiallyFilled_OmitsUnsetProperties()
        {
            var request = new UpdateBusinessRequest(businessName: "Acme Ltd");

            var json = MapAndSerialize<global::Kinde.Api.Kiota.Management.Api.V1.Business.BusinessPatchRequestBody>(request);

            AssertNoExplicitNulls(json);
        }

        [Fact]
        public void CreateWebHookRequest_PartiallyFilled_OmitsUnsetProperties()
        {
            var request = new CreateWebHookRequest(
                endpoint: "https://example.test/hook",
                eventTypes: new List<string> { "user.created" },
                name: "Demo hook");

            var json = MapAndSerialize<global::Kinde.Api.Kiota.Management.Api.V1.Webhooks.WebhooksPostRequestBody>(request);

            AssertNoExplicitNulls(json);
            Assert.Contains("user.created", json);
        }

        /// <summary>
        /// An explicitly set value must still be sent. The null-skip rule must not be so eager that
        /// it drops real data - a test that only proved nulls are absent would also pass if the
        /// mapper sent nothing at all.
        /// </summary>
        [Fact]
        public void SetValues_AreStillSent_NullSkipDoesNotDropRealData()
        {
            var request = new UpdateUserRequest(
                givenName: "Grace",
                familyName: "Hopper",
                isSuspended: true);

            var json = MapAndSerialize<global::Kinde.Api.Kiota.Management.Api.V1.User.UserPatchRequestBody>(request);

            _output.WriteLine(json);

            AssertNoExplicitNulls(json);
            Assert.Contains("Grace", json);
            Assert.Contains("Hopper", json);
            Assert.Contains("is_suspended", json);
        }

        /// <summary>
        /// Maps an SDK request model to its Kiota counterpart and serializes it exactly as the
        /// request pipeline would.
        /// </summary>
        private string MapAndSerialize<TKiota>(object request)
            where TKiota : IParsable
        {
            var mapped = _mapper.Map<TKiota>(request);
            Assert.NotNull(mapped);

            var writerFactory = new JsonSerializationWriterFactory();
            using var writer = writerFactory.GetSerializationWriter("application/json");
            writer.WriteObjectValue(null, mapped);

            using var stream = writer.GetSerializedContent();
            using var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();

            _output.WriteLine($"{typeof(TKiota).Name}:");
            _output.WriteLine(json);

            return json;
        }

        /// <summary>
        /// Fails if any property in the payload is an explicit JSON null.
        /// </summary>
        private static void AssertNoExplicitNulls(string json)
        {
            using var document = JsonDocument.Parse(json);

            var offenders = new List<string>();
            Walk(document.RootElement, path: "$", offenders);

            Assert.True(
                offenders.Count == 0,
                "These properties were serialized as explicit nulls and would be sent to Kinde as "
                + "\"property\": null, which some endpoints reject:"
                + Environment.NewLine
                + string.Join(Environment.NewLine, offenders)
                + Environment.NewLine + Environment.NewLine
                + "Payload:" + Environment.NewLine + json);
        }

        private static void Walk(JsonElement element, string path, List<string> offenders)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.Object:
                    foreach (var property in element.EnumerateObject())
                    {
                        var childPath = $"{path}.{property.Name}";
                        if (property.Value.ValueKind == JsonValueKind.Null)
                        {
                            offenders.Add(childPath);
                            continue;
                        }

                        Walk(property.Value, childPath, offenders);
                    }

                    break;

                case JsonValueKind.Array:
                    var index = 0;
                    foreach (var item in element.EnumerateArray())
                    {
                        Walk(item, $"{path}[{index++}]", offenders);
                    }

                    break;
            }
        }
    }
}
