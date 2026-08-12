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

                // Applies to every IBackedModel destination across both profiles. It can't
                // be scoped narrower: the source DTOs are plain POCOs with no distinction
                // between "explicitly null" and "never touched", so there was never a way to
                // preserve explicit-null-clear for some properties without reintroducing
                // stray `"foo": null` writes for the rest.
                //
                // cfg.Internal() reaches past AutoMapper's public config surface, the same
                // way the ctor lookup below reaches past the public constructor set via
                // reflection -- but riskier, since it's resolved at compile time against
                // whatever AutoMapper version we built against. A runtime version mismatch
                // could throw an opaque MissingMethodException, or silently stop applying
                // the condition if the internal API's behavior changes without its signature
                // changing. Wrap it so a version mismatch fails loudly instead.
                try
                {
                    cfg.Internal().ForAllPropertyMaps(
                        pm => typeof(IBackedModel).IsAssignableFrom(pm.TypeMap.DestinationType),
                        (pm, opt) => opt.Condition((src, dest, srcValue) => srcValue != null));
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(
                        "Could not apply the null-source skip condition via AutoMapper.Internal's " +
                        "ForAllPropertyMaps. This relies on AutoMapper's internal API surface, which " +
                        "may have changed shape in the referenced AutoMapper version. Without this " +
                        "condition, optional properties left unset by the caller are sent to the Kinde " +
                        "API as explicit JSON nulls, which some endpoints reject.",
                        ex);
                }
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
