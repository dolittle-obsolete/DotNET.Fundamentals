// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Dolittle.Collections;
using Dolittle.Reflection;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// Sets public properties on an object given a source.
    /// </summary>
    public class InstancePropertySetter
    {
        readonly List<Func<PropertyBag, object>> _accessors = new List<Func<PropertyBag, object>>();
        readonly List<Action<object, object>> _setters = new List<Action<object, object>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="InstancePropertySetter"/> class.
        /// </summary>
        /// <param name="type">The type the Setter sets properties for.</param>
        /// <param name="factory">An instance of <see cref="IObjectFactory" /> used to build complex objects.</param>
        public InstancePropertySetter(Type type, IObjectFactory factory)
        {
            Type = type;
            var props = Type.GetSettableProperties();

            props.ForEach(pi =>
            {
                _setters.Add(Actions.GetPropertySetter(pi));
                _accessors.Add((pb) => pb.ConstructInstanceOfType(pi, factory));
            });
        }

        /// <summary>
        /// Gets the Type that this sets properties for.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Populates properties on the instance with values from the <see cref="PropertyBag" /> source.
        /// </summary>
        /// <param name="instance">The instance to populate.</param>
        /// <param name="propertyBag">A <see cref="PropertyBag" /> instance that is the source.</param>
        public void Populate(object instance, PropertyBag propertyBag)
        {
            for (int i = 0; i < _setters.Count; i++)
            {
                _setters[i](instance, _accessors[i](propertyBag));
            }
        }
    }
}