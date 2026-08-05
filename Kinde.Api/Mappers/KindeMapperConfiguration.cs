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

                // This condition is global: it applies to every IBackedModel destination
                // across both profiles (100+ CreateMap entries), not just the property that
                // caused #85 -- and it can't be scoped narrower than that. The
                // OpenAPI-generated source DTOs (Kinde.Api.Model.*) are plain POCOs with no
                // "explicitly set to null" vs "never touched" distinction: both states are
                // just `null` on the C# property. So "send an explicit null to clear a
                // field" was never a capability this mapping could offer safely -- pre-fix,
                // AutoMapper couldn't tell the two apart either, which is exactly why every
                // untouched optional property was going out as a stray `"foo": null` and
                // 500'ing the API (see #85). Explicit-null-clear via these DTOs is
                // unsupported SDK-wide; there is no narrower condition that would restore it
                // without reintroducing the original bug for every other optional property.
                //
                // cfg.Internal() reaches past AutoMapper's public config surface into
                // AutoMapper.Internal, the same way the ctor lookup below reaches past the
                // public constructor set via reflection. It's version-sensitive in the same
                // way, but riskier: the ctor lookup fails loudly with a clear message if a
                // future AutoMapper version removes the overload it expects, whereas this
                // call is resolved at compile time against whatever AutoMapper version we
                // built against, so a runtime version mismatch (e.g. a consumer's binding
                // redirect resolving a different AutoMapper.Internal shape) could throw an
                // opaque MissingMethodException, or -- worse -- silently stop applying the
                // null-skip condition if the internal API's behavior changes without its
                // signature changing. Wrap it so a version mismatch fails loudly instead of
                // quietly reintroducing the null-forwarding bug this exists to fix.
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
                        "API as explicit JSON nulls, which some endpoints reject (see #85).",
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
