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

        /// <inheritdoc />
        /// <remarks>
        /// <para>
        /// The read, the comparison and the delete all happen under one hold of the key's gate, so
        /// a refresh completing on another thread cannot have its result deleted by a cleanup that
        /// decided to delete before that result existed.
        /// </para>
        /// <para>
        /// That gate is per-process. Another process can still replace the file between the
        /// comparison and the delete; .NET offers no portable cross-process compare-and-delete to
        /// close it with. The window is microseconds and its worst case is one unnecessary
        /// re-authorization, which is why it is documented rather than papered over.
        /// </para>
        /// </remarks>
        public async Task<bool> RemoveIfAsync(
            string key, string expectedValue, CancellationToken cancellationToken = default)
        {
            var path = PathFor(key);
            var gate = LockFor(key);

            await gate.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                if (!File.Exists(path))
                {
                    return false;
                }

                var current = Unprotect(await ReadAllTextAsync(path, cancellationToken)
                    .ConfigureAwait(false));

                if (!string.Equals(current, expectedValue, StringComparison.Ordinal))
                {
                    return false;
                }

                File.Delete(path);
                return true;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
            catch (DirectoryNotFoundException)
            {
                return false;
            }
            finally
            {
                gate.Release();
            }
        }

        /// <inheritdoc />
        /// <remarks>
        /// Within a process the gate makes this atomic. Between processes the create is made
        /// atomic by <see cref="FileMode.CreateNew"/>, which fails rather than truncating when the
        /// file already exists, so two agents registering at once converge on one registration
        /// instead of overwriting each other.
        /// </remarks>
        public async Task<string?> SetIfAbsentAsync(
            string key, string value, CancellationToken cancellationToken = default)
        {
            var path = PathFor(key);
            var gate = LockFor(key);

            await gate.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                Directory.CreateDirectory(_directory);

                for (var attempt = 1; ; attempt++)
                {
                    var temporary = $"{path}.{Guid.NewGuid():N}.tmp";

                    try
                    {
                        await WriteAsync(temporary, Protect(value), cancellationToken)
                            .ConfigureAwait(false);

                        File.Move(temporary, path);
                        return null;
                    }
                    catch (IOException) when (attempt < CreateAttempts)
                    {
                        // Another agent got there first, or still holds the file open. Its value
                        // is the one both should use.
                        var current = await TryReadAsync(path, cancellationToken)
                            .ConfigureAwait(false);

                        if (!string.IsNullOrEmpty(current))
                        {
                            return current;
                        }

                        // Could not create and could not read: the winner is mid-write, or the file
                        // has since been removed. Returning null here would claim this value was
                        // stored when it was not, so the create is attempted again instead.
                        await Task.Delay(TimeSpan.FromMilliseconds(20 * attempt), cancellationToken)
                            .ConfigureAwait(false);
                    }
                    finally
                    {
                        TryDelete(temporary);
                    }
                }
            }
            finally
            {
                gate.Release();
            }
        }

        /// <summary>How many times an exclusive create is attempted before giving up.</summary>
        private const int CreateAttempts = 5;

        /// <summary>Writes a file, applying the owner-only mode on Unix.</summary>
        private static async Task WriteAsync(
            string path, string contents, CancellationToken cancellationToken)
        {
#if NET8_0_OR_GREATER
            await File.WriteAllTextAsync(path, contents, Encoding.UTF8, cancellationToken)
                .ConfigureAwait(false);

            if (!OperatingSystem.IsWindows())
            {
                File.SetUnixFileMode(path, UnixFileMode.UserRead | UnixFileMode.UserWrite);
            }
#else
            await Task.CompletedTask.ConfigureAwait(false);
            File.WriteAllText(path, contents, Encoding.UTF8);
#endif
        }

        /// <summary>Removes a leftover temporary file, ignoring a failure to do so.</summary>
        private static void TryDelete(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch (IOException)
            {
            }
            catch (UnauthorizedAccessException)
            {
            }
        }

        /// <summary>
        /// Reads a file that another process may still have open, returning null rather than
        /// throwing when it cannot be read.
        /// </summary>
        private static async Task<string?> TryReadAsync(
            string path, CancellationToken cancellationToken)
        {
            try
            {
                return Unprotect(await ReadAllTextAsync(path, cancellationToken)
                    .ConfigureAwait(false));
            }
            catch (IOException)
            {
                // Still locked by the writer, or gone again. Either way the caller retries.
                return null;
            }
            catch (UnauthorizedAccessException)
            {
                return null;
            }
        }

        private static async Task<string> ReadAllTextAsync(
            string path, CancellationToken cancellationToken)
        {
#if NET8_0_OR_GREATER
            return await File.ReadAllTextAsync(path, Encoding.UTF8, cancellationToken)
                .ConfigureAwait(false);
#else
            await Task.CompletedTask.ConfigureAwait(false);
            return File.ReadAllText(path, Encoding.UTF8);
#endif
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

        /// <remarks>
        /// Not disposed, and the store is deliberately not <see cref="IDisposable"/>. A
        /// <see cref="SemaphoreSlim"/> only holds an unmanaged handle once
        /// <c>AvailableWaitHandle</c> is read, which nothing here does, so there is nothing to
        /// release. Making the store disposable would instead raise a question it currently avoids:
        /// a client that was handed a store does not own it, and disposing one still in use would
        /// fail every later read with ObjectDisposedException.
        /// </remarks>
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
