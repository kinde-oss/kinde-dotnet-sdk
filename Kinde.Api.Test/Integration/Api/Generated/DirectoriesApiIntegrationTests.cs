using System.Collections.Generic;
using System.Threading.Tasks;
using Kinde.Api.Api;
using Kinde.Api.Model;
using Kinde.Api.Client;
using Kinde.Api.Test.Integration;
using Xunit;
using Xunit.Abstractions;
using KiotaModels = Kinde.Api.Kiota.Management.Models;

namespace Kinde.Api.Test.Integration.Api.Generated
{
    /// <summary>
    /// Integration tests for DirectoriesApi with both mock and real API support.
    /// </summary>
    public class DirectoriesApiIntegrationTests : BaseIntegrationTest, IClassFixture<TestResourceFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly TestResourceFixture _fixture;

        public DirectoriesApiIntegrationTests(ITestOutputHelper output, TestResourceFixture fixture) : base()
        {
            _output = output;
            _fixture = fixture;
        }

        #region GetDirectories Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetDirectories_Mock_ReturnsDirectories()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Get_directories_response
            {
                Code = "200",
                Message = "Directories retrieved",
                HasMore = false,
                Directories = new List<KiotaModels.DirectoryObject>
                {
                    new KiotaModels.DirectoryObject
                    {
                        Id = "directory_1",
                        DirectoryName = "Engineering",
                        OrganizationCode = "org_123",
                        Status = KiotaModels.Directory_status.Active
                    }
                }
            };
            mockHandler.AddKiotaResponse("GET", "/api/v1/directories", kiotaResponse);

            var api = CreateApi((client, config) => new DirectoriesApi(client, config));

            var response = await api.GetDirectoriesAsync();

            Assert.NotNull(response);
            Assert.Equal("200", response.Code);
            Assert.NotNull(response.Directories);
            Assert.Single(response.Directories);
            Assert.Equal("directory_1", response.Directories[0].Id);
            _output.WriteLine($"Mock test successful - Retrieved {response.Directories.Count} directories");
        }

        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetDirectories_Real_Test()
        {
            if (!UseRealApi) return;

            var api = CreateApi((client, config) => new DirectoriesApi(client, config));
            var response = await api.GetDirectoriesAsync();

            Assert.NotNull(response);
            _output.WriteLine($"Real API returned {response.Directories?.Count ?? 0} directories");
        }

        #endregion

        #region CreateDirectory Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task CreateDirectory_Mock_Created()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Create_directory_response
            {
                Code = "201",
                Message = "Directory created",
                Directory = new KiotaModels.DirectoryObject
                {
                    Id = "directory_1",
                    DirectoryName = "Engineering",
                    Status = KiotaModels.Directory_status.Pending
                }
            };
            mockHandler.AddKiotaResponse("POST", "/api/v1/directories", kiotaResponse, System.Net.HttpStatusCode.Created);

            var api = CreateApi((client, config) => new DirectoriesApi(client, config));
            var request = new CreateDirectoryRequest(
                orgCode: "org_123",
                directoryName: "Engineering",
                providerCode: CreateDirectoryRequest.ProviderCodeEnum.Okta);

            var response = await api.CreateDirectoryAsync(request);

            Assert.NotNull(response);
            Assert.Equal("201", response.Code);
            Assert.Equal("directory_1", response.Directory?.Id);
            Assert.True(mockHandler.WasRequestMade("POST", "/api/v1/directories"));
            _output.WriteLine("CreateDirectory completed successfully");
        }

        [Fact]
        [Trait("TestMode", "Real")]
        public async Task CreateDirectory_Real_Test()
        {
            if (!UseRealApi) return;

            var api = CreateApi((client, config) => new DirectoriesApi(client, config));
            var request = new CreateDirectoryRequest(
                orgCode: _fixture.OrganizationCode,
                directoryName: "Test Directory",
                providerCode: CreateDirectoryRequest.ProviderCodeEnum.Okta);

            var response = await api.CreateDirectoryAsync(request);

            Assert.NotNull(response);
            _output.WriteLine($"Real API created directory {response.Directory?.Id}");
        }

        #endregion

        #region GetDirectory Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetDirectory_Mock_ReturnsDirectory()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Get_directory_response
            {
                Code = "200",
                Message = "Directory retrieved",
                Directory = new KiotaModels.DirectoryObject
                {
                    Id = "directory_1",
                    DirectoryName = "Engineering",
                    Status = KiotaModels.Directory_status.Active
                }
            };
            mockHandler.AddKiotaResponse("GET", "/api/v1/directories/{directory_id}", kiotaResponse);

            var api = CreateApi((client, config) => new DirectoriesApi(client, config));

            var response = await api.GetDirectoryAsync("directory_1");

            Assert.NotNull(response);
            Assert.Equal("directory_1", response.Directory?.Id);
            _output.WriteLine("GetDirectory completed successfully");
        }

        [Fact]
        [Trait("TestMode", "Real")]
        public async Task GetDirectory_Real_Test()
        {
            if (!UseRealApi) return;

            // WARNING: Real API test - This operation requires an existing directory_id.
            // No directory is created by TestResourceFixture, so this uses a placeholder
            // and will likely fail (404) without a real resource ID in the target tenant.
            var directory_id = "test-directory_id";

            var api = CreateApi((client, config) => new DirectoriesApi(client, config));
            var response = await api.GetDirectoryAsync(directory_id);

            Assert.NotNull(response);
            _output.WriteLine($"Real API returned directory {response.Directory?.Id}");
        }

        #endregion

        #region UpdateDirectory Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task UpdateDirectory_Mock_Updated()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Update_directory_response
            {
                Code = "200",
                Message = "Directory updated",
                Directory = new KiotaModels.DirectoryObject
                {
                    Id = "directory_1",
                    DirectoryName = "Engineering (renamed)"
                }
            };
            mockHandler.AddKiotaResponse("PATCH", "/api/v1/directories/{directory_id}", kiotaResponse);

            var api = CreateApi((client, config) => new DirectoriesApi(client, config));
            var request = new UpdateDirectoryRequest(directoryName: "Engineering (renamed)");

            var response = await api.UpdateDirectoryAsync("directory_1", request);

            Assert.NotNull(response);
            Assert.Equal("Engineering (renamed)", response.Directory?.DirectoryName);
            _output.WriteLine("UpdateDirectory completed successfully");
        }

        [Fact]
        [Trait("TestMode", "Real")]
        public async Task UpdateDirectory_Real_Test()
        {
            if (!UseRealApi) return;

            // WARNING: Real API test - This operation requires an existing directory_id.
            // No directory is created by TestResourceFixture, so this uses a placeholder
            // and will likely fail (404) without a real resource ID in the target tenant.
            var directory_id = "test-directory_id";
            var request = new UpdateDirectoryRequest(directoryName: "Renamed Directory");

            var api = CreateApi((client, config) => new DirectoriesApi(client, config));
            var response = await api.UpdateDirectoryAsync(directory_id, request);

            Assert.NotNull(response);
            _output.WriteLine("Real API update completed");
        }

        #endregion

        #region DeleteDirectory Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task DeleteDirectory_Mock_Deleted()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            var kiotaResponse = new KiotaModels.Delete_directory_response
            {
                Code = "200",
                Message = "Directory deleted"
            };
            mockHandler.AddKiotaResponse("DELETE", "/api/v1/directories/{directory_id}", kiotaResponse);

            var api = CreateApi((client, config) => new DirectoriesApi(client, config));

            var response = await api.DeleteDirectoryAsync("directory_1");

            Assert.NotNull(response);
            Assert.Equal("200", response.Code);
            _output.WriteLine("DeleteDirectory completed successfully");
        }

        [Fact]
        [Trait("TestMode", "Real")]
        public async Task DeleteDirectory_Real_Test()
        {
            if (!UseRealApi) return;

            // WARNING: Real API test - This operation requires an existing directory_id.
            // No directory is created by TestResourceFixture, so this uses a placeholder
            // and will likely fail (404) without a real resource ID in the target tenant.
            var directory_id = "test-directory_id";

            var api = CreateApi((client, config) => new DirectoriesApi(client, config));
            var response = await api.DeleteDirectoryAsync(directory_id);

            Assert.NotNull(response);
            _output.WriteLine("Real API delete completed");
        }

        #endregion

        #region Error Handling Tests

        [Fact]
        [Trait("TestMode", "Mock")]
        public async Task GetDirectory_Mock_NotFound_ThrowsException()
        {
            if (UseRealApi) return;

            var mockHandler = GetKiotaMockHandler();
            if (mockHandler == null) return;

            mockHandler.AddErrorResponse("GET", "/api/v1/directories/{directory_id}",
                System.Net.HttpStatusCode.NotFound, "not_found", "Directory not found");

            var api = CreateApi((client, config) => new DirectoriesApi(client, config));

            var exception = await Assert.ThrowsAsync<ApiException>(() => api.GetDirectoryAsync("nonexistent"));
            Assert.Equal(404, exception.ErrorCode);
            _output.WriteLine("404 error handled correctly");
        }

        #endregion
    }
}
