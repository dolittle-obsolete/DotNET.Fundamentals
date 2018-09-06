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
    }
}