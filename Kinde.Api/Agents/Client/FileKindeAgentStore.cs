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
                var contents = await File.ReadAllTextAsync(path, Encoding.UTF8, cancellationToken)
                    .ConfigureAwait(false);
#else
                var contents = File.ReadAllText(path, Encoding.UTF8);
#endif
                var value = Unprotect(contents);
                return string.IsNullOrEmpty(value) ? null : value;
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
            // Unique per write: the semaphore only serialises writers inside one process, and a
            // CLI agent may run twice against the same directory. A shared temporary name lets one
            // process truncate the file the other is about to move into place.
            var temporary = $"{path}.{Guid.NewGuid():N}.tmp";
            var gate = LockFor(key);

            await gate.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                Directory.CreateDirectory(_directory);

#if NET8_0_OR_GREATER
                await File.WriteAllTextAsync(temporary, Protect(value), Encoding.UTF8, cancellationToken)
                    .ConfigureAwait(false);

                if (!OperatingSystem.IsWindows())
                {
                    File.SetUnixFileMode(temporary, UnixFileMode.UserRead | UnixFileMode.UserWrite);
                }
#else
                File.WriteAllText(temporary, Protect(value), Encoding.UTF8);
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

        // Marks a value encrypted with DPAPI, so a file written before this existed still reads.
        private const string ProtectedPrefix = "dpapi:";

        /// <summary>
        /// Encrypts the value on Windows with DPAPI, scoped to the current user.
        /// </summary>
        /// <remarks>
        /// These files hold client secrets and refresh tokens. On Unix the 0600 mode keeps them to
        /// the owning user; on Windows only the profile ACLs would, and anything running as that
        /// user could read them in plain text. DPAPI ties the ciphertext to the user account.
        /// </remarks>
        private static string Protect(string value)
        {
#if NET8_0_OR_GREATER
            if (OperatingSystem.IsWindows())
            {
                var encrypted = ProtectedData.Protect(
                    Encoding.UTF8.GetBytes(value), optionalEntropy: null, DataProtectionScope.CurrentUser);

                return ProtectedPrefix + Convert.ToBase64String(encrypted);
            }
#endif
            return value;
        }

        private static string Unprotect(string contents)
        {
            if (!contents.StartsWith(ProtectedPrefix, StringComparison.Ordinal))
            {
                return contents;
            }

#if NET8_0_OR_GREATER
            if (OperatingSystem.IsWindows())
            {
                try
                {
                    var decrypted = ProtectedData.Unprotect(
                        Convert.FromBase64String(contents.Substring(ProtectedPrefix.Length)),
                        optionalEntropy: null,
                        DataProtectionScope.CurrentUser);

                    return Encoding.UTF8.GetString(decrypted);
                }
                catch (CryptographicException)
                {
                    // Written by another user, or the profile's key is gone. Treat it as absent
                    // so the caller registers again rather than failing with a crypto error.
                    return string.Empty;
                }
            }
#endif
            // Encrypted on Windows, now being read elsewhere: unreadable, so treat it as absent.
            return string.Empty;
        }

        /// <summary>
        /// How many per-key gates are kept before they start being shared.
        /// </summary>
        /// <remarks>
        /// One gate per key is ideal, but the key set is not bounded: it includes the session, so a
        /// host authorizing many users against one store would otherwise add a
        /// <see cref="SemaphoreSlim"/> per user and never release one. Past this many keys the
        /// gates are shared, which costs a little contention and nothing else, because a gate only
        /// has to serialise access to the same key, not be unique to it.
        /// </remarks>
        private const int MaxKeyLocks = 512;

        private readonly SemaphoreSlim[] _sharedLocks = CreateSharedLocks();

        private static SemaphoreSlim[] CreateSharedLocks()
        {
            var locks = new SemaphoreSlim[32];
            for (var index = 0; index < locks.Length; index++)
            {
                locks[index] = new SemaphoreSlim(1, 1);
            }

            return locks;
        }

        private SemaphoreSlim LockFor(string key)
        {
            if (_locks.TryGetValue(key, out var existing))
            {
                return existing;
            }

            if (_locks.Count >= MaxKeyLocks)
            {
                // Derived from the key, so two calls for one key still serialise against each other.
                var index = (uint)key.GetHashCode(StringComparison.Ordinal) % (uint)_sharedLocks.Length;
                return _sharedLocks[index];
            }

            return _locks.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));
        }

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
