using System;
using System.Reflection;
using AutoMapper;
using AutoMapper.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Kiota.Abstractions.Store;

namespace Kinde.Api.Mappers
{
    /// <summary>
    /// Provides centralized AutoMapper configuration for mapping between OpenAPI models and Kiota models.
    /// </summary>
    public static class KindeMapperConfiguration
    {
        private static IMapper? _mapper;
        private static readonly object _lock = new object();

        /// <summary>
        /// Gets the configured AutoMapper instance for Kinde model mappings.
        /// </summary>
        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    lock (_lock)
                    {
                        if (_mapper == null)
                        {
                            _mapper = BuildMapper();
                        }
                    }
                }
                return _mapper;
            }
        }

        private static IMapper BuildMapper()
        {
            Action<IMapperConfigurationExpression> cfgAction = cfg =>
            {
                cfg.AddProfile<ManagementApiMapperProfile>();
                cfg.AddProfile<AccountsApiMapperProfile>();

                // Kiota models keep their state in a backing store that distinguishes
                // "never assigned" from "assigned null", and serializes the latter as an
                // explicit JSON null. AutoMapper assigns every member, so an optional
                // property the caller left unset would go on the wire as `"foo": null`
                // rather than being omitted -- which the Kinde API rejects (e.g. PATCH
                // /organizations/{org_code}/users answers 500 for `"operation": null`).
                // Skip null sources so unset properties stay untouched, and therefore unsent.
                cfg.Internal().ForAllPropertyMaps(
                    pm => typeof(IBackedModel).IsAssignableFrom(pm.TypeMap.DestinationType),
                    (pm, opt) => opt.Condition((src, dest, srcValue) => srcValue != null));
            };

            var configType = typeof(MapperConfiguration);
            var ctorWithLogger = configType.GetConstructor(new[]
            {
                typeof(Action<IMapperConfigurationExpression>),
                typeof(ILoggerFactory),
            });
            var ctorSimple = configType.GetConstructor(new[]
            {
                typeof(Action<IMapperConfigurationExpression>),
            });

            MapperConfiguration config;
            if (ctorWithLogger != null)
            {
                config = (MapperConfiguration)ctorWithLogger.Invoke(new object[]
                {
                    cfgAction,
                    NullLoggerFactory.Instance,
                });
            }
            else if (ctorSimple != null)
            {
                config = (MapperConfiguration)ctorSimple.Invoke(new object[] { cfgAction });
            }
            else
            {
                throw new InvalidOperationException(
                    "Could not find a compatible AutoMapper.MapperConfiguration constructor. " +
                    "Reference AutoMapper 13.x (single-argument constructor) or 14.x/15.x/16.x " +
                    "(constructor with ILoggerFactory).");
            }

            return config.CreateMapper();
        }

        /// <summary>
        /// Resets the mapper instance. Useful for testing purposes.
        /// </summary>
        internal static void Reset()
        {
            lock (_lock)
            {
                _mapper = null;
            }
        }
    }
}
