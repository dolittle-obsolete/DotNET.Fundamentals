// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Reflection;
using Dolittle.Collections;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// Creates an instance of a type using the provided constructor and <see cref="PropertyBag" /> source.
    /// </summary>
    public class PropertyBagToTypeInstanceFactory
    {
        readonly InstanceActivator _activator;
        readonly List<Func<PropertyBag, object>> _parameterAccessors = new List<Func<PropertyBag, object>>();
        readonly IObjectFactory _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyBagToTypeInstanceFactory"/> class.
        /// </summary>
        /// <param name="ctor">The constructor to use to construct an instance.</param>
        /// <param name="factory">A factory to create complex objects.</param>
        /// <remarks>
        /// TODO: strategy for selecting constructor
        /// TODO: strategy for determining ctr param => property name.
        /// </remarks>
        public PropertyBagToTypeInstanceFactory(ConstructorInfo ctor, IObjectFactory factory)
        {
            ConstructorInfo = ctor;
            _activator = Instantiator.GetInstanceActivator(ctor);
            var parameters = ConstructorInfo.GetParameters();

            if (parameters.Length == 1 && parameters[0].ParameterType == typeof(PropertyBag))
            {
                _parameterAccessors.Add((pb) => pb);
            }
            else
            {
                parameters.ForEach(pi => _parameterAccessors.Add((pb) => pb.ConstructInstanceOfType(pi, factory)));
            }

            _factory = factory;
        }

        /// <summary>
        /// Gets the Constructor to be used to instantiate the instance.
        /// </summary>
        public ConstructorInfo ConstructorInfo { get; }

        /// <summary>
        /// Gets the type that the factory can build.
        /// </summary>
        public Type Type => ConstructorInfo.DeclaringType;

        /// <summary>
        /// Builds an instance of the type populating with the values from the <see cref="PropertyBag" />.
        /// </summary>
        /// <param name="propertyBag">Source for the values to instantiate the instance with.</param>
        /// <returns>A populated instantiated instance of the type.</returns>
        public object Build(PropertyBag propertyBag)
        {
            return _activator(ToCtorArgs(propertyBag));
        }

        object[] ToCtorArgs(PropertyBag propertyBag)
        {
            var args = new object[_parameterAccessors.Count];
            for (int i = 0; i < _parameterAccessors.Count; i++)
            {
                args[i] = _parameterAccessors[i](propertyBag);
            }

            return args;
        }
    }
}