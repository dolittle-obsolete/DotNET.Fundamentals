/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.PropertyBags
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;
    using Dolittle.Collections;
    using Dolittle.Strings;
    using Dolittle.Reflection;

    /// <summary>
    /// Creates an instance of a type using the provided constructor and <see cref="PropertyBag" /> source
    /// </summary>
    public class PropertyBagToTypeInstanceFactory
    {
        InstanceActivator _activator;
        List<Func<PropertyBag,object>> _parameterAccessors = new List<Func<PropertyBag, object>>();
        private readonly IObjectFactory _factory;

        //TODO: strategy for selecting constructor
        //TODO: strategy for determining ctr param => property name 
        /// <summary>
        /// Instantiates an instance of <see cref="PropertyBagToTypeInstanceFactory" />
        /// </summary>
        /// <param name="ctor">The constructor to use to construct an instance</param>
        /// <param name="factory">A factory to create complex objects</param>
        public PropertyBagToTypeInstanceFactory(ConstructorInfo ctor, IObjectFactory factory )
        {
            ConstructorInfo = ctor;
            _activator = Instantiator.GetInstanceActivator(ctor, factory);
            var parameters = ConstructorInfo.GetParameters();

            if(parameters.Length == 1 && parameters.First().ParameterType == typeof(PropertyBag))
            {
                _parameterAccessors.Add((pb)=> (PropertyBag)pb);
            } 
            else 
            {
                parameters.ForEach(pi => {
                    _parameterAccessors.Add((pb) => 
                    {
                        if (! pb.ContainsKey(pi.Name.ToPascalCase()))
                            return null;
                        if (pi.ParameterType.IsEnumerable())
                            return pi.ParameterType.ConstructEnumerableForPropertyBag(pb[pi.Name.ToPascalCase()]);
                        return pb[pi.Name.ToPascalCase()];
                    });
                });
            }

            _factory = factory;
        }

        /// <summary>
        /// The Constructor to be used to instantiate the instance
        /// </summary>
        /// <value></value>
        public ConstructorInfo ConstructorInfo { get; }
        /// <summary>
        /// The type that the factory can build
        /// </summary>
        public Type Type => ConstructorInfo.DeclaringType;

        /// <summary>
        /// Builds an instance of the type populating with the values from the <see cref="PropertyBag" />
        /// </summary>
        /// <param name="propertyBag">Source for the values to instantiate the instance with</param>
        /// <returns>A populated instantiated instance of the type</returns>
        public object Build(PropertyBag propertyBag)
        {
            return _activator(ToCtorArgs(propertyBag));
        }

        object[] ToCtorArgs(PropertyBag propertyBag)
        {
            var args = new object[_parameterAccessors.Count];
            for(int i = 0; i < _parameterAccessors.Count; i++)
            {
                args[i] = _parameterAccessors[i](propertyBag);
            }
            return args;
        }
    }   
}