using System;
using System.Collections.Generic;
using Kinde.Api.Mapping;

namespace Kinde.Api.Mappers
{
    /// <summary>
    /// Provides the mapper the SDK uses to translate between the OpenAPI models and the Kiota
    /// models.
    /// </summary>
    /// <remarks>
    /// This previously exposed an AutoMapper <c>IMapper</c> built from two
    /// <c>AutoMapper.Profile</c> classes. The SDK no longer depends on AutoMapper, so the property
    /// now returns <see cref="IKindeMapper"/>; its <c>Map&lt;TDestination&gt;(object)</c> method
    /// has the same signature the SDK used before, so call sites are unchanged.
    /// </remarks>
    public static class KindeMapperConfiguration
    {
        private static readonly object SyncRoot = new object();
        private static IKindeMapper _mapper;

        /// <summary>
        /// Gets the configured mapper for Kinde model mappings.
        /// </summary>
        public static IKindeMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    lock (SyncRoot)
                    {
                        _mapper = _mapper ?? Build();
                    }
                }

                return _mapper;
            }
        }

        /// <summary>
        /// Builds a mapper that is not shared, so a test can exercise one in isolation.
        /// </summary>
        internal static IKindeMapper CreateIsolated() => Build();

        /// <summary>
        /// Builds the registry on its own, so a test can assert what it covers.
        /// </summary>
        internal static KindeMappingRegistry CreateRegistry()
        {
            var registry = new KindeMappingRegistry();
            KindeManagementMappings.Register(registry);
            KindeAccountsMappings.Register(registry);

            return registry;
        }

        /// <summary>
        /// The source property names a convention mapping between these two types copies. Used by
        /// tests to tell a deliberately unmapped property apart from one dropped by mistake.
        /// </summary>
        internal static IReadOnlyCollection<string> DescribeMappedSourceMembers(
            Type sourceType, Type destinationType)
        {
            return ((KindeMapper)Mapper).DescribeMappedSourceMembers(sourceType, destinationType);
        }

        /// <summary>
        /// Resets the cached mapper instance. Used by tests.
        /// </summary>
        internal static void Reset()
        {
            lock (SyncRoot)
            {
                _mapper = null;
            }
        }

        private static IKindeMapper Build() => new KindeMapper(CreateRegistry());
    }
}
