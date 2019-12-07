// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using Dolittle.Immutability;
using Dolittle.Reflection;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// Creates a mutable object using the default constructor then setting all the mutable properties.
    /// </summary>
    public class MutableTypeSetterBasedFactory : ITypeFactory
    {
        readonly ConcurrentDictionary<Type, InstancePropertySetter> _setters = new ConcurrentDictionary<Type, InstancePropertySetter>();

        /// <inheritdoc />
        public object Build(Type type, IObjectFactory objectFactory, PropertyBag source)
        {
            var setter = _setters.GetOrAdd(type, (t) => new InstancePropertySetter(type, objectFactory));
            var instance = Activator.CreateInstance(type);
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
            return !type.IsImmutable() && type.HasDefaultConstructor();
        }

        /// <inheritdoc />
        public bool CanBuild<T>()
        {
            return CanBuild(typeof(T));
        }
    }
}