// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Reflection;
using Dolittle.Types;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// Represents an implementation of <see cref="IObjectFactory"/>.
    /// </summary>
    public class ObjectFactory : IObjectFactory
    {
        readonly IInstancesOf<ITypeFactory> _factories;
        readonly List<ITypeFactory> _userDefinedFactories = new List<ITypeFactory>();
        readonly List<ITypeFactory> _systemFactories = new List<ITypeFactory>();
        readonly ConcurrentDictionary<Type, Lazy<ITypeFactory>> _cachedFactories = new ConcurrentDictionary<Type, Lazy<ITypeFactory>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectFactory"/> class.
        /// </summary>
        /// <param name="factories">Instance of <see cref="ITypeFactory"/>.</param>
        public ObjectFactory(IInstancesOf<ITypeFactory> factories)
        {
            _factories = factories;
            foreach (var f in factories)
            {
                if (IsUserDefined(f))
                {
                    _userDefinedFactories.Add(f);
                }
                else
                {
                    _systemFactories.Add(f);
                }
            }
        }

        /// <inheritdoc />
        public object Build(Type type, PropertyBag source)
        {
            if (source == null)
                return null;
            var lazyFactory = _cachedFactories.GetOrAdd(type, (t) => new Lazy<ITypeFactory>(() => GetTypeFactoryForType(t)));
            return lazyFactory.Value.Build(type, this, source);
        }

        /// <inheritdoc />
        public T Build<T>(PropertyBag source)
        {
            return (T)Build(typeof(T), source);
        }

        bool IsUserDefined(ITypeFactory instance)
        {
            return instance.GetType().ImplementsOpenGeneric(typeof(IUserDefinedTypeFactory<>));
        }

        ITypeFactory GetTypeFactoryForType(Type type)
        {
            ITypeFactory typeFactory;
            try
            {
                typeFactory = _userDefinedFactories.SingleOrDefault(f => f.CanBuild(type));
                if (typeFactory != null)
                {
                    return typeFactory;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new MultipleFactoriesForType($"{type.FullName} has multiple user defined factories to build it.  A type can only have one factory defined.", ex);
            }

            try
            {
                typeFactory = _systemFactories.SingleOrDefault(f => f.CanBuild(type));
                if (typeFactory != null)
                    return typeFactory;

                throw new NoFactoriesForType($"{type.FullName} has no factories to build it.");
            }
            catch (InvalidOperationException ex)
            {
                throw new MultipleFactoriesForType($"{type.FullName} has multiple built in factories to build it.  Check your type definition.", ex);
            }
        }
    }
}