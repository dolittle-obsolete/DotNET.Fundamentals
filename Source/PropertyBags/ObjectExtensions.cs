/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Collections;
using Dolittle.Concepts;
using Dolittle.Reflection;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// Extensions for object
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Creates a <see cref="PropertyBag"/> from an object.
        /// Maps primitive properties and complex objects to <see cref="PropertyBag"/> recursively
        /// </summary>
        /// <param name="obj">Object to convert</param>
        /// <returns></returns>
        public static PropertyBag ToPropertyBag(this object obj)
        {
            if(obj == null)
                return null;

            INullFreeDictionary<string,object> values = new NullFreeDictionary<string, object>();

            foreach (var property in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var value = property.PropertyType.GetPropertyBagObjectValue(property.GetValue(obj)); 
                values.Add(property.Name, value);
            }
            return new PropertyBag(values);    
        }
        /// <summary>
        /// Constructs the <see cref="object">obj</see> as an object suitable for a <see cref="PropertyBag"/>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        static object GetPropertyBagObjectValue(this Type type, object obj)
        {
            
            return 
                type.IsEnumerable() ? 
                    type.ConstructEnumerable(obj) 
                    : type.IsAPrimitiveType() ? 
                    obj 
                    : type.IsConcept() ? 
                        obj?.GetConceptValue() 
                        : obj.ToPropertyBag();
        }
        /// <summary>
        /// Constructs an <see cref="IEnumerable"/> as an object suitable for a <see cref="PropertyBag"/>
        /// </summary>
        /// <param name="propType"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        static object ConstructEnumerable(this Type propType, object obj)
        {
            if (obj == null) return null;
            if (propType.ImplementsOpenGeneric(typeof(IDictionary<,>)))
                throw new ArgumentException("property type cannot be Dictionary<,>");
            var elementType = propType.GetEnumerableElementType();
            var enumerableObject = obj as IEnumerable;

            if (enumerableObject == null) return null;
            
            var resultList = new List<object>();
            foreach (var element in enumerableObject)
                resultList.Add(elementType.GetPropertyBagObjectValue(element));

            return resultList.ToArray();
        }
    }
}