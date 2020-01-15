// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using Dolittle.Immutability;
using Dolittle.Reflection;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// A factory that instantiates a type using a constructor then populates other mutable properties.
    /// </summary>
    public class MutableTypeConstructorBasedFactory : ITypeFactory
    {
        readonly ConcurrentDictionary<Type, InstancePropertySetter> _setters = new ConcurrentDictionary<Type, InstancePropertySetter>();
        readonly ConcurrentDictionary<Type, PropertyBagToTypeInstanceFactory> _factories = new ConcurrentDictionary<Type, PropertyBagToTypeInstanceFactory>();
        readonly IConstructorProvider _provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="MutableTypeConstructorBasedFactory"/> class.
        /// </summary>
        /// <param name="provider">An instance of <see cref="IConstructorProvider" /> that provides the constructor to use.</param>
        public MutableTypeConstructorBasedFactory(IConstructorProvider provider)
        {
            _provider = provider;
        }

        /// <inheritdoc />
        public object Build(Type type, IObjectFactory objectFactory, PropertyBag source)
        {
            var fac = _factories.GetOrAdd(type, (t) => new PropertyBagToTypeInstanceFactory(_provider.GetFor(type), objectFactory));
            var instance = fac.Build(source);
            var setter = _setters.GetOrAdd(type, (t) => new InstancePropertySetter(type, objectFactory));
            setter.Populate(instance, source);
            return instance;
        }

        /// <inheritdoc />
        public T Build<T>(IObjectFactory objectFactory, PropertyBag source)
        {
            return (T)Build(typeof(T), objectFactory, source);
        }

        /// <inheritdoc />
        public bool CanBuild(Type type)
        {
            return !type.IsImmutable() && !type.HasDefaultConstructor();
        }

        /// <inheritdoc />
        public bool CanBuild<T>()
        {
            return CanBuild(typeof(T));
        }
    }
}