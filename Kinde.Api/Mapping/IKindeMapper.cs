using System;

namespace Kinde.Api.Mapping
{
    /// <summary>
    /// Translates between the OpenAPI-generated SDK models and the Kiota-generated models.
    /// </summary>
    /// <remarks>
    /// This replaces the AutoMapper <c>IMapper</c> the SDK previously exposed. The
    /// <see cref="Map{TDestination}(object)"/> signature is deliberately identical to the
    /// AutoMapper overload the SDK used, so existing call sites are unaffected.
    /// </remarks>
    public interface IKindeMapper
    {
        /// <summary>
        /// Maps <paramref name="source"/> to <typeparamref name="TDestination"/>, resolving the
        /// mapping from the source's runtime type. Returns the default value when
        /// <paramref name="source"/> is null.
        /// </summary>
        TDestination Map<TDestination>(object source);

        /// <summary>
        /// Maps <paramref name="source"/> to <paramref name="destinationType"/>. Returns null when
        /// <paramref name="source"/> is null.
        /// </summary>
        object Map(object source, Type destinationType);
    }
}
