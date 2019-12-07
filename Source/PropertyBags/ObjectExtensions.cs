// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;
using Dolittle.Collections;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// Extensions for object.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Creates a <see cref="PropertyBag"/> from an object.
        /// Maps primitive properties and complex objects to <see cref="PropertyBag"/> recursively.
        /// </summary>
        /// <param name="source">Object to convert.</param>
        /// <returns>Converted <see cref="PropertyBag"/>.</returns>
        public static PropertyBag ToPropertyBag(this object source)
        {
            if (source == null)
                return null;

            NullFreeDictionary<string, object> values = new NullFreeDictionary<string, object>();

            foreach (var property in source.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var value = property.PropertyType.GetPropertyBagObjectValue(property.GetValue(source));
                values.Add(property.Name, value);
            }

            return new PropertyBag(values);
        }
    }
}