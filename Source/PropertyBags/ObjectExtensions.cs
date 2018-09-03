/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

            Dictionary<string,object> values = new Dictionary<string, object>();

            foreach (var property in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var value = GetPropertyBagObjectValue(property.GetValue(obj), property.PropertyType); 
                values.Add(property.Name, value);
            }

            return new PropertyBag(values);    
        }

        static object GetPropertyBagObjectValue(object obj, Type type)
        {
            return 
                type.IsEnumerable() ? 
                    ConstructEnumerable(obj, type) 
                    : type.IsAPrimitiveType() ? 
                    obj 
                    : type.IsConcept() ? 
                        obj?.GetConceptValue() 
                        : obj.ToPropertyBag();
        }
        static object ConstructEnumerable(object obj, Type propType)
        {
            if (propType.ImplementsOpenGeneric(typeof(IDictionary<,>)))
                throw new ArgumentException("property type cannot be Dictionary<,>");
            var elementType = propType.GetEnumerableElementType();
            var enumerableObject = obj as IEnumerable;

            var resultList = new List<object>();
            foreach (var element in enumerableObject)
                resultList.Add(GetPropertyBagObjectValue(element, elementType));

            return resultList.ToArray();
        }
    }
}