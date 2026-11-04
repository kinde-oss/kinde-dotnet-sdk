using System;

namespace Kinde.Api.Mapping
{
    /// <summary>
    /// Translates between the OpenAPI-generated SDK models and the Kiota-generated models.
    /// </summary>
    public interface IKindeMapper
    {
        /// <summary>
        /// Maps to <typeparamref name="TDestination"/> using the source's runtime type.
        /// Returns the default value when <paramref name="source"/> is null.
        /// </summary>
        TDestination Map<TDestination>(object source);

        /// <summary>
        /// Maps to <paramref name="destinationType"/>. Returns null when
        /// <paramref name="source"/> is null.
        /// </summary>
        object Map(object source, Type destinationType);
    }
}
