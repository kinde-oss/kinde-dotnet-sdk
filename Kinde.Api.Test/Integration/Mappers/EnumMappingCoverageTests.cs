#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Xunit;
using Xunit.Abstractions;

namespace Kinde.Api.Test.Integration.Mappers
{
    /// <summary>
    /// Guards the one way the mapper can silently produce a wrong value rather than no value.
    /// </summary>
    /// <remarks>
    /// <para>
    /// When an enum member has no counterpart on the other side, the mapper leaves the destination
    /// property alone. On a nullable destination that reads as "unset", which is correct and
    /// honest. On a <em>non-nullable</em> destination there is no such thing as unset: the property
    /// keeps <c>default(TEnum)</c>, which is a real member - or worse, an undefined value when the
    /// enum does not start at zero. Nothing downstream can tell that apart from a value the API
    /// actually sent.
    /// </para>
    /// <para>
    /// Both sides are generated, so a future regeneration can introduce exactly this without anyone
    /// noticing. This test fails the build instead.
    /// </para>
    /// </remarks>
    public class EnumMappingCoverageTests
    {
        private readonly ITestOutputHelper _output;

        public EnumMappingCoverageTests(ITestOutputHelper output) => _output = output;

        [Fact]
        public void NoEnumMemberIsSilentlyMappedToAWrongValue()
        {
            var dangerous = new List<string>();
            var tolerated = new List<string>();

            foreach (var (source, destination) in ManagementMappingManifest.Pairs
                         .Concat(AccountsMappingManifest.Pairs)
                         .Distinct())
            {
                var sourceProperties = source
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(property => property.GetIndexParameters().Length == 0)
                    .Where(property => property.GetGetMethod() != null)
                    .GroupBy(property => property.Name, StringComparer.OrdinalIgnoreCase)
                    .ToDictionary(group => group.Key, group => group.First(), StringComparer.OrdinalIgnoreCase);

                foreach (var destinationProperty in destination
                             .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                             .Where(property => property.GetIndexParameters().Length == 0)
                             .Where(property => property.GetSetMethod() != null))
                {
                    if (!sourceProperties.TryGetValue(destinationProperty.Name, out var sourceProperty))
                    {
                        continue;
                    }

                    var sourceEnum = Nullable.GetUnderlyingType(sourceProperty.PropertyType)
                                     ?? sourceProperty.PropertyType;
                    var destinationEnum = Nullable.GetUnderlyingType(destinationProperty.PropertyType)
                                          ?? destinationProperty.PropertyType;

                    if (sourceEnum == destinationEnum || !sourceEnum.IsEnum || !destinationEnum.IsEnum)
                    {
                        continue;
                    }

                    var unmatched = Enum.GetNames(sourceEnum)
                        .Where(name => !HasCounterpart(name, sourceEnum, destinationEnum))
                        .ToList();

                    if (unmatched.Count == 0)
                    {
                        continue;
                    }

                    var entry = $"{source.Name}.{sourceProperty.Name} -> "
                                + $"{destination.Name}.{destinationProperty.Name} "
                                + $"({destinationEnum.Name}): {string.Join(", ", unmatched)}";

                    if (Nullable.GetUnderlyingType(destinationProperty.PropertyType) != null)
                    {
                        tolerated.Add(entry);
                    }
                    else
                    {
                        dangerous.Add(entry);
                    }
                }
            }

            foreach (var entry in tolerated.Distinct().OrderBy(line => line))
            {
                _output.WriteLine("[unset, destination is nullable] " + entry);
            }

            Assert.True(
                dangerous.Count == 0,
                "These enum members have no counterpart and would land on a non-nullable property, "
                + "which keeps default(TEnum) and so reports a value the API never sent:"
                + Environment.NewLine
                + string.Join(Environment.NewLine, dangerous.Distinct().OrderBy(line => line)));
        }

        /// <summary>
        /// Mirrors <c>KindeValueConverter</c>: match on member name, then on the
        /// <see cref="EnumMemberAttribute"/> wire value, which is how the two generators agree.
        /// </summary>
        private static bool HasCounterpart(string sourceName, Type sourceEnum, Type destinationEnum)
        {
            if (Enum.GetNames(destinationEnum)
                .Any(name => string.Equals(name, sourceName, StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }

            var wire = WireName(sourceEnum, sourceName);
            if (wire == null)
            {
                return false;
            }

            return Enum.GetNames(destinationEnum).Any(name =>
                string.Equals(WireName(destinationEnum, name), wire, StringComparison.OrdinalIgnoreCase)
                || string.Equals(name, wire, StringComparison.OrdinalIgnoreCase));
        }

        private static string? WireName(Type enumType, string memberName) =>
            enumType.GetMember(memberName).FirstOrDefault()
                ?.GetCustomAttribute<EnumMemberAttribute>()
                ?.Value;
    }
}
