#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;

namespace Kinde.Api.Agents
{
    /// <summary>
    /// Reads claims out of a bag whose values may be strings, JSON arrays, or
    /// <see cref="JsonElement"/> instances, depending on whether they came from introspection or
    /// from local JWT validation.
    /// </summary>
    /// <remarks>
    /// This exists because the two verification paths produce different CLR types for the same
    /// claim. JwtSecurityTokenHandler hands back a <see cref="JsonElement"/> for an array claim such
    /// as <c>permissions</c>; treating it as a string yields one permission whose name is the whole
    /// JSON text, which then fails every permission check.
    /// </remarks>
    internal static class KindeClaimReader
    {
        private static readonly char[] ScopeSeparators = { ' ' };

        /// <summary>Reads a single string claim, taking the first value if the claim is a list.</summary>
        public static string? GetString(IReadOnlyDictionary<string, object> claims, string name) =>
            claims.TryGetValue(name, out var raw) ? ToScalarString(raw) : null;

        /// <summary>
        /// Reads a space-delimited claim such as <c>scope</c>, which Kinde may also send as an array.
        /// </summary>
        public static IReadOnlyList<string> GetSpaceDelimited(
            IReadOnlyDictionary<string, object> claims, string name)
        {
            if (!claims.TryGetValue(name, out var raw))
            {
                return Array.Empty<string>();
            }

            return Flatten(raw)
                .SelectMany(v => v.Split(ScopeSeparators, StringSplitOptions.RemoveEmptyEntries))
                .Where(v => v.Length > 0)
                .Distinct(StringComparer.Ordinal)
                .ToArray();
        }

        /// <summary>
        /// Reads the granted scopes, whichever name they arrived under.
        /// </summary>
        /// <remarks>
        /// Introspection returns a space-delimited <c>scope</c>; the access token itself carries
        /// an <c>scp</c> array. Reading only one leaves the scope list empty for half the
        /// verification paths, so every scope check silently fails.
        /// </remarks>
        public static IReadOnlyList<string> GetScopes(IReadOnlyDictionary<string, object> claims)
        {
            var scopes = GetSpaceDelimited(claims, "scope");
            return scopes.Count > 0 ? scopes : GetSpaceDelimited(claims, "scp");
        }

        /// <summary>
        /// Reads the client the token was issued to, whichever name it arrived under.
        /// </summary>
        /// <remarks>
        /// Introspection returns <c>client_id</c>; the access token carries <c>azp</c>. For a CIMD
        /// client either is a URL rather than an opaque id.
        /// </remarks>
        public static string? GetClientId(IReadOnlyDictionary<string, object> claims) =>
            GetString(claims, "client_id") ?? GetString(claims, "azp");

        /// <summary>Reads a list claim such as <c>permissions</c> or <c>aud</c>.</summary>
        public static IReadOnlyList<string> GetList(
            IReadOnlyDictionary<string, object> claims, string name)
        {
            if (!claims.TryGetValue(name, out var raw))
            {
                return Array.Empty<string>();
            }

            return Flatten(raw)
                .Where(v => v.Length > 0)
                .Distinct(StringComparer.Ordinal)
                .ToArray();
        }

        /// <summary>Reads a Unix-seconds claim such as <c>exp</c>.</summary>
        public static DateTimeOffset? GetUnixTime(IReadOnlyDictionary<string, object> claims, string name)
        {
            if (!claims.TryGetValue(name, out var raw))
            {
                return null;
            }

            var seconds = AsLong(raw);
            return seconds.HasValue ? DateTimeOffset.FromUnixTimeSeconds(seconds.Value) : null;
        }

        /// <summary>
        /// Reads the <c>act.sub</c> claim of a delegated token. The <c>act</c> claim is a nested
        /// object, so it arrives as a JsonElement rather than as a flattened claim.
        /// </summary>
        public static string? GetActorSubject(IReadOnlyDictionary<string, object> claims)
        {
            if (!claims.TryGetValue("act", out var raw) || raw is null)
            {
                return null;
            }

            if (raw is JsonElement { ValueKind: JsonValueKind.Object } element)
            {
                return element.TryGetProperty("sub", out var sub) && sub.ValueKind == JsonValueKind.String
                    ? sub.GetString()
                    : null;
            }

            if (raw is IReadOnlyDictionary<string, object> nested)
            {
                return nested.TryGetValue("sub", out var value) ? ToScalarString(value) : null;
            }

            // Introspection may return the object as raw JSON text.
            var text = ToScalarString(raw);
            if (string.IsNullOrWhiteSpace(text) || !text!.TrimStart().StartsWith("{", StringComparison.Ordinal))
            {
                return null;
            }

            try
            {
                using var document = JsonDocument.Parse(text);
                return document.RootElement.TryGetProperty("sub", out var sub)
                       && sub.ValueKind == JsonValueKind.String
                    ? sub.GetString()
                    : null;
            }
            catch (JsonException)
            {
                return null;
            }
        }

        /// <summary>
        /// Reduces a claim value of any shape to a flat sequence of strings.
        /// </summary>
        private static IEnumerable<string> Flatten(object? raw)
        {
            switch (raw)
            {
                case null:
                    yield break;

                case string single:
                    yield return single;
                    yield break;

                // A JSON array claim from local JWT validation. JsonElement is a struct, so it is
                // neither a string nor an IEnumerable and would otherwise fall through to ToString().
                case JsonElement { ValueKind: JsonValueKind.Array } array:
                    foreach (var item in array.EnumerateArray())
                    {
                        foreach (var value in Flatten(item))
                        {
                            yield return value;
                        }
                    }
                    yield break;

                case JsonElement element:
                    var scalar = ToScalarString(element);
                    if (!string.IsNullOrEmpty(scalar))
                    {
                        yield return scalar!;
                    }
                    yield break;

                // A list of claim values, whose items may themselves be JsonElement arrays.
                case IEnumerable sequence:
                    foreach (var item in sequence)
                    {
                        foreach (var value in Flatten(item))
                        {
                            yield return value;
                        }
                    }
                    yield break;

                default:
                    var text = raw.ToString();
                    if (!string.IsNullOrEmpty(text))
                    {
                        yield return text!;
                    }
                    yield break;
            }
        }

        private static string? ToScalarString(object? raw) => raw switch
        {
            null => null,
            string single => single,
            JsonElement { ValueKind: JsonValueKind.String } element => element.GetString(),
            JsonElement { ValueKind: JsonValueKind.Null } => null,
            JsonElement { ValueKind: JsonValueKind.Array } array =>
                array.EnumerateArray().Select(e => ToScalarString(e)).FirstOrDefault(v => !string.IsNullOrEmpty(v)),
            JsonElement element => element.ToString(),
            IEnumerable<string> strings => strings.FirstOrDefault(),
            IEnumerable sequence => Flatten(sequence).FirstOrDefault(),
            _ => raw.ToString()
        };

        private static long? AsLong(object? raw)
        {
            switch (raw)
            {
                case null:
                    return null;
                case long value:
                    return value;
                case int value:
                    return value;
                case double value:
                    return (long)value;
                case JsonElement { ValueKind: JsonValueKind.Number } element:
                    return element.TryGetInt64(out var number) ? number : null;
            }

            var text = ToScalarString(raw);
            return long.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsed)
                ? parsed
                : null;
        }
    }
}
