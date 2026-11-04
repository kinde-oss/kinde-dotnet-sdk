#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Kinde.Api.Mappers;
using Kinde.Api.Mapping;
using Xunit;
using Xunit.Abstractions;

namespace Kinde.Api.Test.Integration.Mappers
{
    /// <summary>
    /// Guards against a non-nullable enum silently keeping default(TEnum) when a member has no counterpart,
    /// which downstream is indistinguishable from a value the API sent. Both enums are generated, so a regeneration can introduce this.
    /// </summary>
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
        /// Covers the explicitly bridged enums, including those reached only as list elements
        /// (such as EnabledFactors), which the property walk above never sees.
        /// </summary>
        [Fact]
        public void EveryBridgedEnumMember_HasACounterpartInBothDirections()
        {
            var mapper = KindeMapperConfiguration.CreateIsolated();
            var pairs = KindeMapperConfiguration.CreateRegistry().RegisteredConversions
                .Where(pair => pair.Source.IsEnum && pair.Destination.IsEnum)
                .ToList();

            Assert.NotEmpty(pairs);

            var failures = new List<string>();
            foreach (var (source, destination) in pairs)
            {
                foreach (var member in Enum.GetValues(source))
                {
                    try
                    {
                        Assert.NotNull(mapper.Map(member, destination));
                    }
                    catch (KindeMappingException)
                    {
                        failures.Add($"{source.FullName}.{member} -> {destination.FullName}");
                    }
                }
            }

            Assert.True(
                failures.Count == 0,
                "These bridged enum members have no counterpart:"
                + Environment.NewLine
                + string.Join(Environment.NewLine, failures));
        }

        [Fact]
        public void BridgeEnum_WithNoMatchingWireName_Throws()
        {
            Assert.Equal(BridgeTarget.Shared, KindeMappingRegistry.BridgeEnum<BridgeTarget>(BridgeSource.Shared));

            var exception = Assert.Throws<KindeMappingException>(
                () => KindeMappingRegistry.BridgeEnum<BridgeTarget>(BridgeSource.OnlyHere));

            Assert.Contains("OnlyHere", exception.Message);
        }

        private enum BridgeSource
        {
            Shared,
            OnlyHere,
        }

        private enum BridgeTarget
        {
            Shared,
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
