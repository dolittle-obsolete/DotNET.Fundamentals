// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using Dolittle.Immutability;
using Dolittle.Lifecycle;
using Dolittle.Reflection;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// Creates a instance of the immutable type, using a constructor to provide all values.
    /// </summary>
    [Singleton]
    public class ImmutableTypeConstructorBasedFactory : ITypeFactory
    {
        readonly ConcurrentDictionary<Type, PropertyBagToTypeInstanceFactory> _factories = new ConcurrentDictionary<Type, PropertyBagToTypeInstanceFactory>();
        readonly IConstructorProvider _provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImmutableTypeConstructorBasedFactory"/> class.
        /// </summary>
        /// <param name="provider"><see cref="IConstructorProvider"/>.</param>
        public ImmutableTypeConstructorBasedFactory(IConstructorProvider provider)
        {
            _provider = provider;
        }

        /// <inheritdoc />
        public object Build(Type type, IObjectFactory objectFactory, PropertyBag source)
        {
            var fac = _factories.GetOrAdd(type, _ => new PropertyBagToTypeInstanceFactory(_provider.GetFor(type), objectFactory));
            return fac.Build(source);
        }

        /// <inheritdoc />
        public T Build<T>(IObjectFactory objectFactory, PropertyBag source)
        {
            return (T)Build(typeof(T), objectFactory, source);
        }

        /// <inheritdoc />
        public bool CanBuild(Type type)
        {
            return type.IsImmutable() && type.HasNonDefaultConstructor();
        }

        /// <inheritdoc />
        public bool CanBuild<T>()
        {
            return CanBuild(typeof(T));
        }
    }
}