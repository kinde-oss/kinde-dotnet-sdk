#nullable enable
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Kinde.Api.Agents.Client;
using Xunit;

namespace Kinde.Api.Test.Agents
{
    /// <summary>
    /// Covers the default store, which holds client secrets and refresh tokens on disk.
    /// </summary>
    public class FileKindeAgentStoreTests : IDisposable
    {
        private readonly string _directory =
            Path.Combine(Path.GetTempPath(), "kinde-store-tests", Guid.NewGuid().ToString("N"));

        public void Dispose()
        {
            try
            {
                if (Directory.Exists(_directory))
                {
                    Directory.Delete(_directory, recursive: true);
                }
            }
            catch (IOException)
            {
            }
        }

        private FileKindeAgentStore Create() => new FileKindeAgentStore(_directory);

        [Fact]
        public async Task SetAsync_ThenGetAsync_RoundTripsTheValue()
        {
            var store = Create();

            await store.SetAsync("a-key", "{\"client_id\":\"dyn_123\"}");

            Assert.Equal("{\"client_id\":\"dyn_123\"}", await store.GetAsync("a-key"));
        }

        [Fact]
        public async Task GetAsync_ReturnsNull_ForAnAbsentKey()
        {
            Assert.Null(await Create().GetAsync("never-written"));
        }

        [Fact]
        public async Task SetAsync_ReplacesAnExistingValue()
        {
            var store = Create();

            await store.SetAsync("a-key", "first");
            await store.SetAsync("a-key", "second");

            Assert.Equal("second", await store.GetAsync("a-key"));
        }

        [Fact]
        public async Task RemoveAsync_ForgetsTheValue_AndIsSafeWhenAbsent()
        {
            var store = Create();
            await store.SetAsync("a-key", "value");

            await store.RemoveAsync("a-key");
            await store.RemoveAsync("a-key");

            Assert.Null(await store.GetAsync("a-key"));
        }

        [Fact]
        public async Task KeysAreIsolatedFromEachOther()
        {
            var store = Create();

            await store.SetAsync("first", "one");
            await store.SetAsync("second", "two");

            Assert.Equal("one", await store.GetAsync("first"));
            Assert.Equal("two", await store.GetAsync("second"));
        }

        [Fact]
        public async Task KeysContainingPathCharacters_DoNotEscapeTheDirectory()
        {
            // Keys carry a domain and client id, so used directly they would be a path traversal.
            var store = Create();

            await store.SetAsync("https://example.kinde.com/../../escaped|token", "value");

            Assert.Equal("value", await store.GetAsync("https://example.kinde.com/../../escaped|token"));
            Assert.All(
                Directory.GetFiles(_directory, "*", SearchOption.AllDirectories),
                file => Assert.StartsWith(_directory, Path.GetFullPath(file), StringComparison.Ordinal));
        }

        [Fact]
        public async Task TheDirectoryIsCreatedWhenMissing()
        {
            var nested = Path.Combine(_directory, "deeper", "still");
            var store = new FileKindeAgentStore(nested);

            await store.SetAsync("a-key", "value");

            Assert.True(Directory.Exists(nested));
        }

        [Fact]
        public async Task SecretsAreNotReadableAsPlainTextOnWindows()
        {
            // These files hold client secrets and refresh tokens. On Windows only the profile
            // ACLs would protect them, so the value is encrypted with DPAPI at rest.
            var store = Create();
            const string Secret = "s3cret-refresh-token-value";

            await store.SetAsync("a-key", Secret);

            var onDisk = string.Concat(
                Directory.GetFiles(_directory).Select(File.ReadAllText));

            if (OperatingSystem.IsWindows())
            {
                Assert.DoesNotContain(Secret, onDisk);
            }

            // Either way the value must read back unchanged.
            Assert.Equal(Secret, await store.GetAsync("a-key"));
        }

        [Fact]
        public async Task NoTemporaryFilesAreLeftBehind()
        {
            var store = Create();

            await store.SetAsync("a-key", "value");

            Assert.Empty(Directory.GetFiles(_directory, "*.tmp"));
        }

        [Fact]
        public async Task ConcurrentWritesToOneKey_LeaveAReadableValue()
        {
            var store = Create();

            await Task.WhenAll(Enumerable.Range(0, 20)
                .Select(i => store.SetAsync("a-key", $"value-{i}")));

            var result = await store.GetAsync("a-key");

            Assert.NotNull(result);
            Assert.StartsWith("value-", result);
        }
    }
}
