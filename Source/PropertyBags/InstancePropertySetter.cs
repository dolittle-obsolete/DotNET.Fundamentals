/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.PropertyBags
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Dolittle.Reflection;
    using Dolittle.Collections;
    using Dolittle.Concepts;
    using System.Collections.Concurrent;
    using System.Collections;
    using System.Reflection;

    /// <summary>
    /// Sets public properties on an object given a source
    /// </summary>
    public class InstancePropertySetter
    {
        List<Func<PropertyBag,object>> _accessors = new List<Func<PropertyBag, object>>();
        List<Action<object,object>> _setters = new List<Action<object, object>>();

        /// <summary>
        /// Instantiates an instance of <see cref="InstancePropertySetter" />
        /// </summary>
        /// <param name="type">The type the Setter sets properties for</param>
        /// <param name="factory">An instance of <see cref="IObjectFactory" /> used to build complex objects</param>
        public InstancePropertySetter(Type type, IObjectFactory factory )
        {
            Type = type;
            var props = Type.GetSettableProperties();

            props.ForEach(pi => {
                _setters.Add(Actions.GetPropertySetter(type,pi));
                _accessors.Add((pb) => 
                {
                    if (!pb.ContainsKey(pi.Name))
                        return null;
                    
                    var value = pb[pi.Name];
                    if(value == null)
                        return value;
                    if(pi.PropertyType.IsAPrimitiveType() || pi.PropertyType == typeof(PropertyBag))
                        return value;

                    if(pi.PropertyType.IsConcept())
                        return ConceptFactory.CreateConceptInstance(pi.PropertyType,value);

                    if (pi.PropertyType.IsEnumerable())
                        return pi.PropertyType.ConstructEnumerable(factory, value);
                    

                    return factory.Build(pi.PropertyType, value as PropertyBag);
                });
            });
        }

        /// <summary>
        /// The Type that this sets properties for
        /// </summary>
        /// <value></value>
        public Type Type { get; }

        /// <summary>
        /// Populates properties on the instance with values from the <see cref="PropertyBag" /> source
        /// </summary>
        /// <param name="instance">The instance to populate</param>
        /// <param name="propertyBag">A <see cref="PropertyBag" /> instance that is the source</param>
        public void Populate(object instance, PropertyBag propertyBag)
        {
            for(int i = 0; i < _setters.Count(); i++)
            {
                _setters[i](instance,_accessors[i](propertyBag));
            }
        }
    }
}