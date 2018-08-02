/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.PropertyBags
{
    using System;
    using System.Collections.Concurrent;
    using Dolittle.Execution;
    using Dolittle.Reflection;

    /// <summary>
    /// Creates a instance of the immutable type, using a constructor to provide all values
    /// </summary>
    [Singleton]
    public class ImmutableTypeConstructorBasedFactory : ITypeFactory
    {
        ConcurrentDictionary<Type,PropertyBagToTypeInstanceFactory> _factories = new ConcurrentDictionary<Type, PropertyBagToTypeInstanceFactory>();
        private readonly IConstructorProvider _provider;

        /// <summary>
        /// Instantiates an instance of <see cref="ImmutableTypeConstructorBasedFactory" />
        /// </summary>
        /// <param name="provider"></param>
        public ImmutableTypeConstructorBasedFactory(IConstructorProvider provider)
        {
            _provider = provider;
        }

        /// <inheritdoc />
        public object Build(Type type, IObjectFactory objectFactory, PropertyBag source)
        {
            var fac =_factories.GetOrAdd(type, (t) => new PropertyBagToTypeInstanceFactory(_provider.GetFor(type), objectFactory));
            return fac.Build(source);
        }

        /// <inheritdoc />
        public T Build<T>(IObjectFactory objectFactory, PropertyBag source)
        {
            return (T)Build(typeof(T),objectFactory,source);
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