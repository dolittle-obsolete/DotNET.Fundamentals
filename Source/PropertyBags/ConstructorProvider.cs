// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;
using Dolittle.Immutability;
using Dolittle.Lifecycle;
using Dolittle.Reflection;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// A default implemenatation of <see cref="IConstructorProvider" />.
    /// </summary>
    /// <remarks>
    /// If it is an immutable type, it uses the PropertyBag constuctor if it exists, otherwise it tries
    /// to find the constructor with the most parameters.
    /// If the type is mutable, it will use the default constructor if present, otherwise the constructor with the most parameters.
    /// </remarks>
    [Singleton]
    public class ConstructorProvider : IConstructorProvider
    {
        /// <inheritdoc />
        public ConstructorInfo Get<T>()
        {
            return GetFor(typeof(T));
        }

        /// <inheritdoc />
        public ConstructorInfo GetFor(Type type)
        {
            if (type.IsImmutable())
                return GetCtorForImmutable(type);
            return GetCtorForMutable(type);
        }

        ConstructorInfo GetCtorForImmutable(Type type)
        {
            var propertyBagCtor = type.GetPropertyBagConstructor();
            return propertyBagCtor ?? type.GetNonDefaultConstructorWithGreatestNumberOfParameters();
        }

        ConstructorInfo GetCtorForMutable(Type type)
        {
            if (type.HasDefaultConstructor())
                return type.GetDefaultConstructor();

            return type.GetNonDefaultConstructorWithGreatestNumberOfParameters();
        }
    }
}