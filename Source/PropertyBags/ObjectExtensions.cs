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
            if (typeof(Dictionary<,>).IsAssignableFrom(propType))
                throw new ArgumentException("property type cannot be Dictionary<,>");
            var elementType = GetEnumerableElementType(propType);
            var enumerableObject = obj as IEnumerable;

            var resultList = new List<object>();
            foreach (var element in enumerableObject)
                resultList.Add(GetPropertyBagObjectValue(element, elementType));

            return resultList.ToArray();
        }

        // https://stackoverflow.com/questions/906499/getting-type-t-from-ienumerablet
        static Type GetEnumerableElementType(Type propType)
        {
            Type elementType;
            if (propType.IsArray)
                elementType = propType.GetElementType();
            else if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                elementType = propType.GetGenericArguments()[0];
            else 
            {
                elementType = propType.GetInterfaces()
                    .Where(t => t.IsGenericType &&
                        t.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                    .Select(t => t.GenericTypeArguments[0]).FirstOrDefault();
            }
            return elementType;
        }
    }
}