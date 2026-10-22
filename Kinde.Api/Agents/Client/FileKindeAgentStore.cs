#nullable enable
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kinde.Api.Agents.Client
{
    /// <summary>
    /// The default store: one file per key under a directory you choose. Survives restarts, so a
    /// registration made once is not made again.
    /// </summary>
    /// <remarks>
    /// Chosen as the default because an SDK cannot assume a database, and because the alternative
    /// -- an in-memory or cache-backed store -- silently loses registrations and forces the user
    /// through consent again. On Windows the directory inherits the user profile's permissions; on
    /// Unix it is created as 0700. Values are written through a temporary file and moved into
    /// place, so a crash mid-write cannot leave a half-written registration behind.
    /// </remarks>
    public class FileKindeAgentStore : IKindeAgentStore
    {
        private readonly string _directory;
        private readonly ConcurrentDictionary<string, SemaphoreSlim> _locks = new();

        /// <summary>Creates a store under <paramref name="directory"/>, creating it if needed.</summary>
        public FileKindeAgentStore(string directory)
        {
            if (string.IsNullOrWhiteSpace(directory))
            {
                throw new ArgumentException("A directory is required.", nameof(directory));
            }

            _directory = directory;
            Directory.CreateDirectory(_directory);

#if NET8_0_OR_GREATER
            if (!OperatingSystem.IsWindows())
            {
                // Tokens and client secrets live here, so keep it to the owning user.
                File.SetUnixFileMode(
                    _directory,
                    UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute);
            }
#endif
        }

        /// <summary>
        /// A store under the current user's local application data, which is the sensible default
        /// for a desktop or CLI agent.
        /// </summary>
        public static FileKindeAgentStore ForCurrentUser(string applicationName = "Kinde")
        {
            var root = Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData,
                Environment.SpecialFolderOption.Create);

            return new FileKindeAgentStore(Path.Combine(root, applicationName, "agents"));
        }

        /// <inheritdoc />
        public async Task<string?> GetAsync(string key, CancellationToken cancellationToken = default)
        {
            var path = PathFor(key);
            var gate = LockFor(key);

            await gate.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                if (!File.Exists(path))
                {
                    return null;
                }

#if NET8_0_OR_GREATER
                return await File.ReadAllTextAsync(path, Encoding.UTF8, cancellationToken)
                    .ConfigureAwait(false);
#else
                return File.ReadAllText(path, Encoding.UTF8);
#endif
            }
            catch (FileNotFoundException)
            {
                // Removed between the check and the read.
                return null;
            }
            catch (DirectoryNotFoundException)
            {
                return null;
            }
            finally
            {
                gate.Release();
            }
        }

        /// <inheritdoc />
        public async Task SetAsync(
            string key, string value, CancellationToken cancellationToken = default)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var path = PathFor(key);
            var temporary = path + ".tmp";
            var gate = LockFor(key);

            await gate.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                Directory.CreateDirectory(_directory);

#if NET8_0_OR_GREATER
                await File.WriteAllTextAsync(temporary, value, Encoding.UTF8, cancellationToken)
                    .ConfigureAwait(false);

                if (!OperatingSystem.IsWindows())
                {
                    File.SetUnixFileMode(temporary, UnixFileMode.UserRead | UnixFileMode.UserWrite);
                }
#else
                File.WriteAllText(temporary, value, Encoding.UTF8);
#endif

                // Replaces atomically, so a reader never sees a partial value.
                File.Move(temporary, path, overwrite: true);
            }
            finally
            {
                if (File.Exists(temporary))
                {
                    try
                    {
                        File.Delete(temporary);
                    }
                    catch (IOException)
                    {
                        // A leftover temporary file is harmless; the next write replaces it.
                    }
                }

                gate.Release();
            }
        }

        /// <inheritdoc />
        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            var path = PathFor(key);
            var gate = LockFor(key);

            await gate.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch (FileNotFoundException)
            {
            }
            finally
            {
                gate.Release();
            }
        }

        private SemaphoreSlim LockFor(string key) =>
            _locks.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));

        /// <summary>
        /// Hashes the key into the file name. A key contains a Kinde domain and client id, so using
        /// it directly would produce path separators and, on a case-insensitive file system,
        /// collisions between keys that differ only in case.
        /// </summary>
        private string PathFor(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("A key is required.", nameof(key));
            }

            var hash = SHA256.HashData(Encoding.UTF8.GetBytes(key));
            var name = Convert.ToHexString(hash).ToLowerInvariant();

            return Path.Combine(_directory, name + ".json");
        }
    }
}
