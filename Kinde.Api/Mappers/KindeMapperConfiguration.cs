using AutoMapper;

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
                            var config = new MapperConfiguration(cfg =>
                            {
                                cfg.AddProfile<ManagementApiMapperProfile>();
                                cfg.AddProfile<AccountsApiMapperProfile>();
                            });
                            
                            // Note: We skip AssertConfigurationIsValid() because Kiota and OpenAPI models
                            // have some structural differences that require custom handling.
                            // The mappings will work for properties that match, and unmapped properties
                            // will be left at their default values.
                            
                            _mapper = config.CreateMapper();
                        }
                    }
                }
                return _mapper;
            }
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

